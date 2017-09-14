using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading;

using WindowsMedia.Platform;

namespace WindowsMedia
{
    /// <summary>
    /// Describes DirectShow filter wrapper
    /// </summary>
    /// <seealso cref="IFilter" />
    /// <seealso cref="IDisposable" />
    /// <seealso cref="IComparable" />
    public class BaseFilter : IFilter, IDisposable, IComparable
    {
        private const string DeviceDmoPattern = @"@device:dmo";
        private Lazy<Guid> _classId;
        private readonly Lazy<string> _name;
        private static IMoniker[] staticMonikers;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilter"/> class.
        /// </summary>
        /// <param name="filter">The instance of the DirectShow filter interface.</param>
        public BaseFilter(IBaseFilter filter)
        {
            Object = filter;
            _classId = new Lazy<Guid>(() => Object.GetClassID(out var result) == 0 ? result : Guid.Empty);
            _name = new Lazy<string>(() => GetFilterName(Object));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilter"/> class.
        /// </summary>
        /// <param name="monikerString">The moniker string.</param>
        public BaseFilter(string monikerString)
        {
            var s = monikerString;
            IsDmoFilter = s.StartsWith(DeviceDmoPattern);
            _classId = new Lazy<Guid>(() => IsDmoFilter ? GetDmoGuid(s) : Guid.Empty);
            _name = new Lazy<string>(() => GetName(s), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        /// Create a new filter from its moniker
        /// </summary>
        /// <param name="moniker">The moniker.</param>
        internal BaseFilter(IMoniker moniker)
        {
            var monikerString = GetMonikerString(moniker);
            IsDmoFilter = monikerString.StartsWith(DeviceDmoPattern);
            _classId = new Lazy<Guid>(() => IsDmoFilter ? GetDmoGuid(monikerString) : GetMonikerGuid(moniker));
            _name = new Lazy<string>(() => GetName(monikerString), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        ~BaseFilter()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public IBaseFilter Object { get; private set; }

        /// <inheritdoc />
        public Guid ClassId => _classId.Value;

        /// <inheritdoc />
        public bool IsDmoFilter { get; }

        /// <inheritdoc />
        public Guid DmoCategoryIdentifier { get; private set; }

        /// <inheritdoc />
        public IEnumerable<Pin> GetPins()
        {
            IEnumPins pins = null;
            try
            {
                var hr = Object.EnumPins(out pins);
                DsError.ThrowExceptionForHR(hr);
                hr = pins.Reset();
                DsError.ThrowExceptionForHR(hr);
                var pin = new IPin[1];
                while (pins.Next(1, pin, out _) == 0)
                {
                    yield return new Pin(pin[0]);
                }
            }
            finally
            {
                if (pins != null)
                {
                    Marshal.ReleaseComObject(pins);
                }
            }
        }

        /// <inheritdoc />
        public Pin FindPinByName(string name, PinDirection direction)
        {
            foreach (var pin in GetPins())
            {
                if (pin.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && pin.Direction == direction)
                {
                    return pin;
                }

                pin.Dispose();
            }

            return null;
        }

        /// <inheritdoc />
        public IEnumerable<Pin> GetFreePins(string name, PinDirection direction)
        {
            foreach (var pin in GetPins())
            {
                if (pin.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && pin.Direction == direction)
                {
                    yield return pin;
                }
                else
                {
                    pin.Dispose();
                }
            }
        }

        /// <inheritdoc />
        public Pin FindPinByName(string name)
        {
            var hr = Object.FindPin(name, out var result);
            return hr == 0 ? new Pin(result) : null;
        }

        /// <inheritdoc />
        public bool DisconnectAllPins(bool recursive = true)
        {
            var result = true;

            foreach (var pin in GetPins())
            {
                using (pin)
                {
                    if (pin.Direction == PinDirection.Output)
                    {
                        if (!pin.Disconnect(recursive))
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        /// <inheritdoc />
        public Pin GetPinByDirection(PinDirection direction, int index)
        {
            var result = DsFindPin.ByDirection(Object, direction, index);
            return result != null ? new Pin(result) : null;
        }

        /// <inheritdoc />
        public string Name => _name.Value;

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposable)
        {
            if (Object != null)
            {
                DsUtils.ReleaseComObject(Object);
                Object = null;
            }
        }

        private static string GetFilterName(IBaseFilter filter)
        {
            var result = string.Empty;
            if (filter.QueryFilterInfo(out var info) == 0)
            {
                try
                {
                    result = info.achName;
                }
                finally
                {
                    DsUtils.FreeFilterInfo(info);
                }
            }

#if NET35
            var clid = Guid.Empty;
            if (clid.TryParse(result, out clid))
#else            
            if (Guid.TryParse(result, out var clid))
#endif
            {
                result = Filters.LegacyFilters.GetByGuid(clid)?.Name ?? result;
            }

            return result;
        }

        /// <summary>
        /// Gets the moniker unique identifier.
        /// </summary>
        /// <param name="moniker">The moniker.</param>
        /// <returns>Returns moniker identifier</returns>
        public static Guid GetMonikerGuid(IMoniker moniker)
        {
            Guid ret;
            object bagObj = null;

            try
            {
                var bagId = typeof(IPropertyBag).GUID;
                moniker.BindToStorage(null, null, ref bagId, out bagObj);
                var bag = (IPropertyBag)bagObj;

                var hr = bag.Read("clsid", out var val, null);
                ret = hr >= 0 ? (val is string s ? new Guid(s) : Guid.Empty) : Guid.Empty;
            }
            catch
            {
                ret = Guid.Empty;
            }
            finally
            {
                if (bagObj != null)
                {
                    Marshal.ReleaseComObject(bagObj);
                }
            }

            return ret;
        }

        private Guid GetDmoGuid(string gn)
        {
            var m = Regex.Match(
                gn,
                @"\@device\:dmo\:(?<destguid>\{[0-9A-Fa-f]{8}\-[0-9A-Fa-f]{4}\-[0-9A-Fa-f]{4}\-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}\})(?<catguid>\{[0-9A-Fa-f]{8}\-[0-9A-Fa-f]{4}\-[0-9A-Fa-f]{4}\-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}\})");

            if (!m.Success)
            {
                return Guid.Empty;
            }

            DmoCategoryIdentifier = new Guid(m.Groups["catguid"].Value);
            return new Guid(m.Groups["destguid"].Value);
        }

        /// <summary>
        /// Retrieve the a moniker's display name (i.e. it's unique string)
        /// </summary>
        private static string GetMonikerString(IMoniker moniker)
        {
            moniker.GetDisplayName(null, null, out var s);
            return s;
        }

        /// <summary> Retrieve the human-readable name of the filter </summary>
        private string GetName(IMoniker moniker)
        {
            try
            {
                var bagId = typeof(IPropertyBag).GUID;
                moniker.BindToStorage(null, null, ref bagId, out var bagObj);
                using (var bag = new ComWrapper<IPropertyBag>((IPropertyBag)bagObj))
                {
                    var hr = bag.Object.Read("FriendlyName", out var val, null);
                    if (hr != 0)
                    {
                        return string.Empty;
                    }

                    var ret = val as string;
                    hr = bag.Object.Read("CLSID", out val, null);
                    if (hr == 0)
                    {
                        _classId = new Lazy<Guid>(() => new Guid(val.ToString()));
                    }

                    return ret ?? string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary> Get a moniker's human-readable name based on a moniker string. </summary>
        private string GetName(string monikerString)
        {
            var parser = GetAnyMoniker();
            parser.ParseDisplayName(null, null, monikerString, out _, out var moniker);
            using (new ComWrapper<IMoniker>(moniker))
            {
                return GetName(parser);
            }
        }

        /// <summary>
        ///  This method gets a System.Runtime.InteropServices.ComTypes.IMoniker object.
        /// 
        ///  HACK: The only way to create a System.Runtime.InteropServices.ComTypes.IMoniker from a moniker 
        ///  string is to use System.Runtime.InteropServices.ComTypes.IMoniker.ParseDisplayName(). So I 
        ///  need ANY System.Runtime.InteropServices.ComTypes.IMoniker object so that I can call 
        ///  ParseDisplayName(). Does anyone have a better solution?
        /// 
        ///  This assumes there is at least one video compressor filter
        ///  installed on the system.
        /// </summary>
        private static IMoniker GetAnyMoniker()
        {
            var category = FilterCategory.VideoCompressorCategory;
            if (staticMonikers != null)
            {
                return staticMonikers[0];
            }

            staticMonikers = new IMoniker[1];
            // Get the system device enumerator
            using (var enumDev = new CreateDevEnum((ICreateDevEnum)new Platform.CreateDevEnum()))
            {
                staticMonikers[0] = enumDev.GetClasses(category).FirstOrDefault();
            }

            return staticMonikers[0];
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var f = (BaseFilter)obj;
            return string.Compare(Name, f.Name, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}