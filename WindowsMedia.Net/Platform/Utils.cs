using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;

using JetBrains.Annotations;

namespace WindowsMedia.Platform
{
    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public static class DsUtils
    {
        /// <summary>
        /// Returns the PinCategory of the specified pin.  Usually a member of PinCategory.  Not all pins have a category.
        /// </summary>
        /// <param name="pPin"></param>
        /// <returns>Guid indicating pin category or Guid.Empty on no category.  Usually a member of PinCategory</returns>
        public static Guid GetPinCategory(IPin pPin)
        {
            Guid guidRet = Guid.Empty;

            // Memory to hold the returned guid
            int iSize = Marshal.SizeOf(typeof(Guid));
            IntPtr ipOut = Marshal.AllocCoTaskMem(iSize);

            try
            {
                int hr;
                int cbBytes;
                var g = PropSetID.Pin;

                // Get an IKsPropertySet from the pin
                var pKs = pPin as IKsPropertySet;

                if (pKs != null)
                {
                    // Query for the Category
                    hr = pKs.Get(g, (int)AMPropertyPin.Category, IntPtr.Zero, 0, ipOut, iSize, out cbBytes);
                    DsError.ThrowExceptionForHR(hr);

                    // Marshal it to the return variable
                    guidRet = (Guid)Marshal.PtrToStructure(ipOut, typeof(Guid));
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(ipOut);
            }

            return guidRet;
        }

        /// <summary>
        ///  Free the nested structures and release any
        ///  COM objects within an AMMediaType struct.
        /// </summary>
        public static void FreeAMMediaType(AMMediaType mediaType)
        {
            if (mediaType != null)
            {
                if (mediaType.formatSize != 0)
                {
                    Marshal.FreeCoTaskMem(mediaType.formatPtr);
                    mediaType.formatSize = 0;
                    mediaType.formatPtr = IntPtr.Zero;
                }

                if (mediaType.unkPtr != IntPtr.Zero)
                {
                    Marshal.Release(mediaType.unkPtr);
                    mediaType.unkPtr = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        ///  Free the nested interfaces within a PinInfo structure.
        /// </summary>
        public static void FreePinInfo(PinInfo pinInfo)
        {
            if (pinInfo.filter != null)
            {
                Marshal.ReleaseComObject(pinInfo.filter);
                pinInfo.filter = null;
            }
        }

        /// <summary>
        /// Releases Filter information
        /// </summary>
        /// <param name="filterInfo"></param>
        public static void FreeFilterInfo(FilterInfo filterInfo)
        {
            if (filterInfo.pGraph != null)
            {
                ReleaseComObject(filterInfo.pGraph);
                filterInfo.pGraph = null;
            }
        }

        /// <summary>
        /// Releases COM object
        /// </summary>
        /// <param name="obj"></param>
        public static void ReleaseComObject(object obj)
        {
            if (obj != null)
            {
                Marshal.ReleaseComObject(obj);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public class DsROTEntry : IDisposable
    {
        [Flags]
        private enum ROTFlags
        {
            RegistrationKeepsAlive = 0x1,

            AllowAnyClient = 0x2
        }

        private int _cookie = 0;

        #region APIs

        [DllImport("ole32.dll", ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        private static extern int GetRunningObjectTable(int r, out IRunningObjectTable pprot);

        [DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        private static extern int CreateItemMoniker(string delim, string item, out IMoniker ppmk);

        #endregion

        /// <summary>
        /// Initializes new instance of the <see cref="DsROTEntry"/> with graph
        /// </summary>
        /// <param name="graph">The filter graph</param>
        public DsROTEntry(IFilterGraph graph)
        {
            int hr = 0;
            IRunningObjectTable rot = null;
            IMoniker mk = null;
            try
            {
                // First, get a pointer to the running object table
                hr = GetRunningObjectTable(0, out rot);
                DsError.ThrowExceptionForHR(hr);

                // Build up the object to add to the table
                int id = Process.GetCurrentProcess().Id;
                IntPtr iuPtr = Marshal.GetIUnknownForObject(graph);
                string s;
                try
                {
                    s = iuPtr.ToString("x");
                }
                catch
                {
                    s = "";
                }
                finally
                {
                    Marshal.Release(iuPtr);
                }
                string item = string.Format("FilterGraph {0} pid {1:x8}", s, id);
                hr = CreateItemMoniker("!", item, out mk);
                DsError.ThrowExceptionForHR(hr);

                // Add the object to the table
                _cookie = rot.Register((int)ROTFlags.RegistrationKeepsAlive, graph, mk);
            }
            finally
            {
                DsUtils.ReleaseComObject(mk);
                DsUtils.ReleaseComObject(rot);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ~DsROTEntry()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_cookie != 0)
            {
                GC.SuppressFinalize(this);
                IRunningObjectTable rot;

                // Get a pointer to the running object table
                int hr = GetRunningObjectTable(0, out rot);
                DsError.ThrowExceptionForHR(hr);

                try
                {
                    // Remove our entry
                    rot.Revoke(_cookie);
                    _cookie = 0;
                }
                finally
                {
                    DsUtils.ReleaseComObject(rot);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public class DsDevice : IDisposable
    {
        private IMoniker _moniker;
        private string _name;

        /// <summary>
        /// Initializes new instance of the <see cref="DsDevice"/> class
        /// </summary>
        /// <param name="Mon"></param>
        public DsDevice(IMoniker Mon)
        {
            _moniker = Mon;
            _name = null;
        }

        /// <summary>
        /// Gets the device moniker
        /// </summary>
        public IMoniker Mon
        {
            get { return _moniker; }
        }

        /// <summary>
        /// Gets the device name
        /// </summary>
        public string Name => _name ?? (_name = GetPropBagValue("FriendlyName"));

        /// <summary>
        /// Gets a unique identifier for a device
        /// </summary>
        public string DevicePath
        {
            get
            {
                string s = null;

                try
                {
                    _moniker.GetDisplayName(null, null, out s);
                }
                catch
                {
                }

                return s;
            }
        }

        /// <summary>
        /// Gets the ClassID for a device
        /// </summary>
        public Guid ClassID
        {
            get
            {
                Guid g;

                _moniker.GetClassID(out g);

                return g;
            }
        }


        /// <summary>
        /// Returns an array of DsDevices of type devcat.
        /// </summary>
        /// <param name="filterCategory">Any one of FilterCategory</param>
        public static DsDevice[] GetDevicesOfCat(Guid filterCategory)
        {
            // Use arrayList to build the retun list since it is easily resizable
            DsDevice[] devret;
            ArrayList devs = new ArrayList();
            IEnumMoniker enumMon;

            ICreateDevEnum enumDev = (ICreateDevEnum)new CreateDevEnum();
            var hr = enumDev.CreateClassEnumerator(filterCategory, out enumMon, 0);
            DsError.ThrowExceptionForHR(hr);

            // CreateClassEnumerator returns null for enumMon if there are no entries
            if (hr != 1)
            {
                try
                {
                    try
                    {
                        IMoniker[] mon = new IMoniker[1];
                        while ((enumMon.Next(1, mon, IntPtr.Zero) == 0))
                        {
                            try
                            {
                                // The devs array now owns this object.  Don't
                                // release it if we are going to be successfully
                                // returning the devret array
                                devs.Add(new DsDevice(mon[0]));
                            }
                            catch
                            {
                                DsUtils.ReleaseComObject(mon[0]);
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        DsUtils.ReleaseComObject(enumMon);
                    }

                    // Copy the ArrayList to the DsDevice[]
                    devret = new DsDevice[devs.Count];
                    devs.CopyTo(devret);
                }
                catch
                {
                    foreach (DsDevice d in devs)
                    {
                        d.Dispose();
                    }
                    throw;
                }
            }
            else
            {
                devret = new DsDevice[0];
            }

            return devret;
        }

        /// <summary>
        /// Get a specific PropertyBag value from a moniker
        /// </summary>
        /// <param name="sPropName">The name of the value to retrieve</param>
        /// <returns>String or null on error</returns>
        public string GetPropBagValue(string sPropName)
        {
            IPropertyBag bag = null;
            string ret = null;
            object bagObj = null;
            object val = null;

            try
            {
                Guid bagId = typeof(IPropertyBag).GUID;
                _moniker.BindToStorage(null, null, ref bagId, out bagObj);

                bag = (IPropertyBag)bagObj;

                int hr = bag.Read(sPropName, out val, null);
                DsError.ThrowExceptionForHR(hr);

                ret = val as string;
            }
            catch
            {
                ret = null;
            }
            finally
            {
                bag = null;
                if (bagObj != null)
                {
                    DsUtils.ReleaseComObject(bagObj);
                    bagObj = null;
                }
            }

            return ret;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (Mon != null)
            {
                DsUtils.ReleaseComObject(Mon);
                _moniker = null;
                GC.SuppressFinalize(this);
            }
            _name = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public static class DsFindPin
    {
        /// <summary>
        /// Scans a filter's pins looking for a pin in the specified direction
        /// </summary>
        /// <param name="vSource">The filter to scan</param>
        /// <param name="vDir">The direction to find</param>
        /// <param name="iIndex">Zero based index (ie 2 will return the third pin in the specified direction)</param>
        /// <returns>The matching pin, or null if not found</returns>
        public static IPin ByDirection(IBaseFilter vSource, PinDirection vDir, int iIndex)
        {
            int hr;
            IEnumPins ppEnum;
            PinDirection ppindir;
            IPin pRet = null;
            IPin[] pPins = new IPin[1];

            if (vSource == null)
            {
                return null;
            }

            // Get the pin enumerator
            hr = vSource.EnumPins(out ppEnum);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                // Walk the pins looking for a match
                int fetched;
                while (ppEnum.Next(1, pPins, out fetched) >= 0 && (fetched == 1))
                {
                    // Read the direction
                    hr = pPins[0].QueryDirection(out ppindir);
                    DsError.ThrowExceptionForHR(hr);

                    // Is it the right direction?
                    if (ppindir == vDir)
                    {
                        // Is is the right index?
                        if (iIndex == 0)
                        {
                            pRet = pPins[0];
                            break;
                        }
                        iIndex--;
                    }
                    DsUtils.ReleaseComObject(pPins[0]);
                }
            }
            finally
            {
                DsUtils.ReleaseComObject(ppEnum);
            }

            return pRet;
        }

        /// <summary>
        /// Scans a filter's pins looking for a pin with the specified name
        /// </summary>
        /// <param name="vSource">The filter to scan</param>
        /// <param name="vPinName">The pin name to find</param>
        /// <returns>The matching pin, or null if not found</returns>
        public static IPin ByName(IBaseFilter vSource, string vPinName)
        {
            int hr;
            IEnumPins ppEnum;
            PinInfo ppinfo;
            IPin pRet = null;
            IPin[] pPins = new IPin[1];

            if (vSource == null)
            {
                return null;
            }

            // Get the pin enumerator
            hr = vSource.EnumPins(out ppEnum);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                // Walk the pins looking for a match
                int fetched;
                while (ppEnum.Next(1, pPins, out fetched) >= 0 && fetched == 1)
                {
                    // Read the info
                    hr = pPins[0].QueryPinInfo(out ppinfo);
                    DsError.ThrowExceptionForHR(hr);

                    // Is it the right name?
                    if (ppinfo.name == vPinName)
                    {
                        DsUtils.FreePinInfo(ppinfo);
                        pRet = pPins[0];
                        break;
                    }
                    DsUtils.ReleaseComObject(pPins[0]);
                    DsUtils.FreePinInfo(ppinfo);
                }
            }
            finally
            {
                DsUtils.ReleaseComObject(ppEnum);
            }

            return pRet;
        }

        /// <summary>
        /// Scan's a filter's pins looking for a pin with the specified category
        /// </summary>
        /// <param name="vSource">The filter to scan</param>
        /// <param name="guidPinCat">The guid from PinCategory to scan for</param>
        /// <param name="iIndex">Zero based index (ie 2 will return the third pin of the specified category)</param>
        /// <returns>The matching pin, or null if not found</returns>
        public static IPin ByCategory(IBaseFilter vSource, Guid PinCategory, int iIndex)
        {
            int hr;
            IEnumPins ppEnum;
            IPin pRet = null;
            IPin[] pPins = new IPin[1];

            if (vSource == null)
            {
                return null;
            }

            // Get the pin enumerator
            hr = vSource.EnumPins(out ppEnum);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                // Walk the pins looking for a match
                int fetched;
                while (ppEnum.Next(1, pPins, out fetched) >= 0 && fetched == 1)
                {
                    // Is it the right category?
                    if (DsUtils.GetPinCategory(pPins[0]) == PinCategory)
                    {
                        // Is is the right index?
                        if (iIndex == 0)
                        {
                            pRet = pPins[0];
                            break;
                        }
                        iIndex--;
                    }
                    DsUtils.ReleaseComObject(pPins[0]);
                }
            }
            finally
            {
                DsUtils.ReleaseComObject(ppEnum);
            }

            return pRet;
        }

        /// <summary>
        /// Scans a filter's pins looking for a pin with the specified connection status
        /// </summary>
        /// <param name="vSource">The filter to scan</param>
        /// <param name="vStat">The status to find (connected/unconnected)</param>
        /// <param name="iIndex">Zero based index (ie 2 will return the third pin with the specified status)</param>
        /// <returns>The matching pin, or null if not found</returns>
        public static IPin ByConnectionStatus(IBaseFilter vSource, PinConnectedStatus vStat, int iIndex)
        {
            int hr;
            IEnumPins ppEnum;
            IPin pRet = null;
            IPin pOutPin;
            IPin[] pPins = new IPin[1];

            if (vSource == null)
            {
                return null;
            }

            // Get the pin enumerator
            hr = vSource.EnumPins(out ppEnum);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                // Walk the pins looking for a match
                int fetched;
                while (ppEnum.Next(1, pPins, out fetched) >= 0 && fetched == 1)
                {
                    // Read the connected status
                    hr = pPins[0].ConnectedTo(out pOutPin);

                    // Check for VFW_E_NOT_CONNECTED.  Anything else is bad.
                    if (hr != DsResults.E_NotConnected)
                    {
                        DsError.ThrowExceptionForHR(hr);

                        // The ConnectedTo call succeeded, release the interface
                        DsUtils.ReleaseComObject(pOutPin);
                    }

                    // Is it the right status?
                    if ((hr == 0 && vStat == PinConnectedStatus.Connected)
                        || (hr == DsResults.E_NotConnected && vStat == PinConnectedStatus.Unconnected))
                    {
                        // Is is the right index?
                        if (iIndex == 0)
                        {
                            pRet = pPins[0];
                            break;
                        }
                        iIndex--;
                    }
                    DsUtils.ReleaseComObject(pPins[0]);
                }
            }
            finally
            {
                DsUtils.ReleaseComObject(ppEnum);
            }

            return pRet;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public static class DsToString
    {
        /// <summary>
        /// Produces a usable string that describes the MediaType object
        /// </summary>
        /// <returns>Concatenation of MajorType + SubType + FormatType + Fixed + Temporal + SampleSize.ToString</returns>
        public static string AMMediaTypeToString(AMMediaType pmt)
        {
            return string.Format(
                "{0} {1} {2} {3} {4} {5}",
                MediaTypeToString(pmt.majorType),
                MediaSubTypeToString(pmt.subType),
                MediaFormatTypeToString(pmt.formatType),
                (pmt.fixedSizeSamples ? "FixedSamples" : "NotFixedSamples"),
                (pmt.temporalCompression ? "temporalCompression" : "NottemporalCompression"),
                pmt.sampleSize);
        }

        /// <summary>
        /// Converts AMMediaType.MajorType Guid to a readable string
        /// </summary>
        /// <returns>MajorType Guid as a readable string or Guid if unrecognized</returns>
        public static string MediaTypeToString(Guid guid)
        {
            // Walk the MediaSubType class looking for a match
            return WalkClass(typeof(MediaType), guid);
        }

        /// <summary>
        /// Converts the AMMediaType.SubType Guid to a readable string
        /// </summary>
        /// <returns>SubType Guid as a readable string or Guid if unrecognized</returns>
        public static string MediaSubTypeToString(Guid guid)
        {
            // Walk the MediaSubType class looking for a match
            string s = WalkClass(typeof(MediaSubType), guid);

            // There is a special set of Guids that contain the FourCC code
            // as part of the Guid.  Check to see if it is one of those.
            if (s.Length == 36 && s.Substring(8).ToUpper() == "-0000-0010-8000-00AA00389B71")
            {
                // Parse out the FourCC code
                byte[] asc =
                    {
                        Convert.ToByte(s.Substring(6, 2), 16), Convert.ToByte(s.Substring(4, 2), 16),
                        Convert.ToByte(s.Substring(2, 2), 16), Convert.ToByte(s.Substring(0, 2), 16)
                    };
                s = Encoding.ASCII.GetString(asc);
            }

            return s;
        }

        /// <summary>
        /// Converts the AMMediaType.FormatType Guid to a readable string
        /// </summary>
        /// <returns>FormatType Guid as a readable string or Guid if unrecognized</returns>
        public static string MediaFormatTypeToString(Guid guid)
        {
            // Walk the FormatType class looking for a match
            return WalkClass(typeof(FormatType), guid);
        }

        /// <summary>
        /// Use reflection to walk a class looking for a property containing a specified guid
        /// </summary>
        /// <param name="MyType">Class to scan</param>
        /// <param name="guid">Guid to scan for</param>
        /// <returns>String representing property name that matches, or Guid.ToString() for no match</returns>
        private static string WalkClass(Type MyType, Guid guid)
        {
            object o = null;

            // Read the fields from the class
            FieldInfo[] Fields = MyType.GetFields();

            // Walk the returned array
            foreach (FieldInfo m in Fields)
            {
                // Read the value of the property.  The parameter is ignored.
                o = m.GetValue(o);

                // Compare it with the sought value
                if ((Guid)o == guid)
                {
                    return m.Name;
                }
            }

            return guid.ToString();
        }
    }


    /// <summary>
    /// This abstract class contains definitions for use in implementing a custom marshaler.
    ///
    /// MarshalManagedToNative() gets called before the COM method, and MarshalNativeToManaged() gets
    /// called after.  This allows for allocating a correctly sized memory block for the COM call,
    /// then to break up the memory block and build an object that c# can digest.
    /// </summary>
    [PublicAPI]
    internal abstract class DsMarshaler : ICustomMarshaler
    {
        #region Data Members

        /// <summary>
        /// The cookie isn't currently being used.
        /// </summary>
        protected string m_cookie;

        /// <summary>
        /// The managed object passed in to MarshalManagedToNative, and modified in MarshalNativeToManaged
        /// </summary>
        protected object m_obj;

        #endregion

        /// <summary>
        /// Initializes new instance of the <see cref="DsMarshaler"/> class
        /// </summary>
        /// <param name="cookie"></param>
        public DsMarshaler(string cookie)
        {
            // If we get a cookie, save it.
            m_cookie = cookie;
        }

        /// <summary>
        /// Called just before invoking the COM method.  The returned IntPtr is what goes on the stack
        /// for the COM call.  The input arg is the parameter that was passed to the method.
        /// </summary>
        /// <param name="managedObj"></param>
        /// <returns></returns>
        public virtual IntPtr MarshalManagedToNative(object managedObj)
        {
            // Save off the passed-in value.  Safe since we just checked the type.
            m_obj = managedObj;

            // Create an appropriately sized buffer, blank it, and send it to the marshaler to
            // make the COM call with.
            int iSize = GetNativeDataSize() + 3;
            IntPtr p = Marshal.AllocCoTaskMem(iSize);

            for (int x = 0; x < iSize / 4; x++)
            {
                Marshal.WriteInt32(p, x * 4, 0);
            }

            return p;
        }

        /// <summary>
        /// Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        /// from MarshalManagedToNative.  The return value is unused.
        /// </summary>
        /// <param name="pNativeData"></param>
        /// <returns></returns>
        public virtual object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return m_obj;
        }

        /// <summary>
        /// Release the (now unused) buffer
        /// </summary>
        /// <param name="pNativeData"></param>
        public virtual void CleanUpNativeData(IntPtr pNativeData)
        {
            if (pNativeData != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(pNativeData);
            }
        }

        /// <summary>
        /// Release the (now unused) managed object
        /// </summary>
        /// <param name="managedObj"></param>
        public virtual void CleanUpManagedData(object managedObj)
        {
            m_obj = null;
        }

        /// <summary>
        /// This routine is (apparently) never called by the marshaler.  However it can be useful.
        /// </summary>
        /// <returns></returns>
        public abstract int GetNativeDataSize();

        // GetInstance is called by the marshaler in preparation to doing custom marshaling.  The (optional)
        // cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.

        // It is commented out in this abstract class, but MUST be implemented in derived classes
        //public static ICustomMarshaler GetInstance(string cookie)
    }

    /// <summary>
    /// c# does not correctly marshal arrays of pointers.
    /// </summary>
    [PublicAPI]
    internal class EMTMarshaler : DsMarshaler
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EMTMarshaler"/> class
        /// </summary>
        /// <param name="cookie"></param>
        public EMTMarshaler(string cookie)
            : base(cookie)
        {
        }

        /// <summary>
        /// Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        /// from MarshalManagedToNative.  The return value is unused.
        /// </summary>
        /// <param name="pNativeData"></param>
        /// <returns></returns>
        public override object MarshalNativeToManaged(IntPtr pNativeData)
        {
            AMMediaType[] emt = m_obj as AMMediaType[];

            for (int x = 0; x < emt.Length; x++)
            {
                // Copy in the value, and advance the pointer
                IntPtr p = Marshal.ReadIntPtr(pNativeData, x * IntPtr.Size);
                if (p != IntPtr.Zero)
                {
                    emt[x] = (AMMediaType)Marshal.PtrToStructure(p, typeof(AMMediaType));
                }
                else
                {
                    emt[x] = null;
                }
            }

            return null;
        }

        /// <summary>
        /// The number of bytes to marshal out
        /// </summary>
        /// <returns></returns>
        public override int GetNativeDataSize()
        {
            // Get the array size
            int i = ((Array)m_obj).Length;

            // Multiply that times the size of a pointer
            int j = i * IntPtr.Size;

            return j;
        }

        /// <summary>
        /// This method is called by interop to create the custom marshaler.  The (optional)
        /// cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static ICustomMarshaler GetInstance(string cookie)
        {
            return new EMTMarshaler(cookie);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public static class DsError
    {
        /// <summary>
        /// The AMGetErrorText function retrieves the error message for a given return code, using the current language setting.
        /// </summary>
        /// <param name="hr">HRESULT value.</param>
        /// <param name="buf">Pointer to a character buffer that receives the error message.</param>
        /// <param name="max">Number of characters in <paramref name="buf"/>.</param>
        /// <returns>Returns the number of characters returned in the buffer, or zero if an error occurred.</returns>
        [DllImport("quartz.dll", CharSet = CharSet.Unicode, ExactSpelling = true, EntryPoint = "AMGetErrorTextW"),
         SuppressUnmanagedCodeSecurity]
        public static extern int AMGetErrorText(int hr, StringBuilder buf, int max);

        /// <summary>
        /// If hr has a "failed" status code (E_*), throw an exception.  Note that status
        /// messages (S_*) are not considered failure codes.  If DirectShow error text
        /// is available, it is used to build the exception, otherwise a generic com error
        /// is thrown.
        /// </summary>
        /// <param name="hr">The HRESULT to check</param>
        public static void ThrowExceptionForHR(int hr)
        {
            // If a severe error has occurred
            if (hr < 0)
            {
                var s = GetErrorText(hr);

                // If a string is returned, build a com error from it
                if (s != null)
                {
                    throw new COMException(s, hr);
                }

                // No string, just use standard com error
                Marshal.ThrowExceptionForHR(hr);
            }
        }

        /// <summary>
        /// Returns a string describing a DS error.  Works for both error codes
        /// (values &lt; 0) and Status codes (values >= 0)
        /// </summary>
        /// <param name="hr">HRESULT for which to get description</param>
        /// <returns>The string, or null if no error text can be found</returns>
        public static string GetErrorText(int hr)
        {
            const int MAX_ERROR_TEXT_LEN = 160;

            // Make a buffer to hold the string
            var buf = new StringBuilder(MAX_ERROR_TEXT_LEN, MAX_ERROR_TEXT_LEN);

            // If a string is returned, build a com error from it
            return AMGetErrorText(hr, buf, MAX_ERROR_TEXT_LEN) > 0 ? buf.ToString() : null;
        }
    }

    /// <summary>
    /// c# does not correctly create structures that contain ByValArrays of structures (or enums!).  Instead
    /// of allocating enough room for the ByValArray of structures, it only reserves room for a ref,
    /// even when decorated with ByValArray and SizeConst.  Needless to say, if DirectShow tries to
    /// write to this too-short buffer, bad things will happen.
    ///
    /// To work around this for the DvdTitleAttributes structure, use this custom marshaler
    /// by declaring the parameter DvdTitleAttributes as:
    ///
    ///    [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(DTAMarshaler))]
    ///   DvdTitleAttributes pTitle
    ///
    /// See DsMarshaler for more info on custom marshalers
    /// </summary>
    [PublicAPI]
    internal class DTAMarshaler : DsMarshaler
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DTAMarshaler"/> class.
        /// </summary>
        /// <param name="cookie"></param>
        public DTAMarshaler(string cookie)
            : base(cookie)
        {
        }

        /// <summary>
        /// Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        /// from MarshalManagedToNative.  The return value is unused.
        /// </summary>
        /// <param name="pNativeData"></param>
        /// <returns></returns>
        public override object MarshalNativeToManaged(IntPtr pNativeData)
        {
            DvdTitleAttributes dta = m_obj as DvdTitleAttributes;

            // Copy in the value, and advance the pointer
            dta.AppMode = (DvdTitleAppMode)Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(int)));

            // Copy in the value, and advance the pointer
            dta.VideoAttributes = (DvdVideoAttributes)Marshal.PtrToStructure(pNativeData, typeof(DvdVideoAttributes));
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdVideoAttributes)));

            // Copy in the value, and advance the pointer
            dta.ulNumberOfAudioStreams = Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(int)));

            // Allocate a large enough array to hold all the returned structs.
            dta.AudioAttributes = new DvdAudioAttributes[8];
            for (int x = 0; x < 8; x++)
            {
                // Copy in the value, and advance the pointer
                dta.AudioAttributes[x] =
                    (DvdAudioAttributes)Marshal.PtrToStructure(pNativeData, typeof(DvdAudioAttributes));
                pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdAudioAttributes)));
            }

            // Allocate a large enough array to hold all the returned structs.
            dta.MultichannelAudioAttributes = new DvdMultichannelAudioAttributes[8];
            for (int x = 0; x < 8; x++)
            {
                // MultichannelAudioAttributes has nested ByValArrays.  They need to be individually copied.

                dta.MultichannelAudioAttributes[x].Info = new DvdMUAMixingInfo[8];

                for (int y = 0; y < 8; y++)
                {
                    // Copy in the value, and advance the pointer
                    dta.MultichannelAudioAttributes[x].Info[y] =
                        (DvdMUAMixingInfo)Marshal.PtrToStructure(pNativeData, typeof(DvdMUAMixingInfo));
                    pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdMUAMixingInfo)));
                }

                dta.MultichannelAudioAttributes[x].Coeff = new DvdMUACoeff[8];

                for (int y = 0; y < 8; y++)
                {
                    // Copy in the value, and advance the pointer
                    dta.MultichannelAudioAttributes[x].Coeff[y] =
                        (DvdMUACoeff)Marshal.PtrToStructure(pNativeData, typeof(DvdMUACoeff));
                    pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdMUACoeff)));
                }
            }

            // The DvdMultichannelAudioAttributes needs to be 16 byte aligned
            pNativeData = (IntPtr)(pNativeData.ToInt64() + 4);

            // Copy in the value, and advance the pointer
            dta.ulNumberOfSubpictureStreams = Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(int)));

            // Allocate a large enough array to hold all the returned structs.
            dta.SubpictureAttributes = new DvdSubpictureAttributes[32];
            for (int x = 0; x < 32; x++)
            {
                // Copy in the value, and advance the pointer
                dta.SubpictureAttributes[x] =
                    (DvdSubpictureAttributes)Marshal.PtrToStructure(pNativeData, typeof(DvdSubpictureAttributes));
                pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdSubpictureAttributes)));
            }

            // Note that 4 bytes (more alignment) are unused at the end

            return null;
        }

        /// <summary>
        /// The number of bytes to marshal out
        /// </summary>
        /// <returns></returns>
        public override int GetNativeDataSize()
        {
            // This is the actual size of a DvdTitleAttributes structure
            return 3208;
        }

        /// <summary>
        /// This method is called by interop to create the custom marshaler.  The (optional)
        /// cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static ICustomMarshaler GetInstance(string cookie)
        {
            return new DTAMarshaler(cookie);
        }
    }

    /// <summary>
    /// See DTAMarshaler for an explanation of the problem.  This class is for marshaling
    /// a DvdKaraokeAttributes structure.
    /// </summary>
    internal class DKAMarshaler : DsMarshaler
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DKAMarshaler"/> class.
        /// </summary>
        /// <param name="cookie"></param>
        // The constructor.  This is called from GetInstance (below)
        public DKAMarshaler(string cookie)
            : base(cookie)
        {
        }

        /// <summary>
        /// Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        /// from MarshalManagedToNative.  The return value is unused.
        /// </summary>
        /// <param name="pNativeData"></param>
        /// <returns></returns>
        public override object MarshalNativeToManaged(IntPtr pNativeData)
        {
            DvdKaraokeAttributes dka = m_obj as DvdKaraokeAttributes;

            // Copy in the value, and advance the pointer
            dka.bVersion = (byte)Marshal.ReadByte(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(byte)));

            // DWORD Align
            pNativeData = (IntPtr)(pNativeData.ToInt64() + 3);

            // Copy in the value, and advance the pointer
            dka.fMasterOfCeremoniesInGuideVocal1 = Marshal.ReadInt32(pNativeData) != 0;
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(bool)));

            // Copy in the value, and advance the pointer
            dka.fDuet = Marshal.ReadInt32(pNativeData) != 0;
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(bool)));

            // Copy in the value, and advance the pointer
            dka.ChannelAssignment = (DvdKaraokeAssignment)Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64()
                                   + Marshal.SizeOf(
                                       DvdKaraokeAssignment.GetUnderlyingType(typeof(DvdKaraokeAssignment))));

            // Allocate a large enough array to hold all the returned structs.
            dka.wChannelContents = new DvdKaraokeContents[8];
            for (int x = 0; x < 8; x++)
            {
                // Copy in the value, and advance the pointer
                dka.wChannelContents[x] = (DvdKaraokeContents)Marshal.ReadInt16(pNativeData);
                pNativeData = (IntPtr)(pNativeData.ToInt64()
                                       + Marshal.SizeOf(
                                           DvdKaraokeContents.GetUnderlyingType(typeof(DvdKaraokeContents))));
            }

            return null;
        }

        /// <summary>
        /// The number of bytes to marshal out
        /// </summary>
        /// <returns></returns>
        public override int GetNativeDataSize()
        {
            // This is the actual size of a DvdKaraokeAttributes structure.
            return 32;
        }

        /// <summary>
        /// This method is called by interop to create the custom marshaler.  The (optional)
        /// cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static ICustomMarshaler GetInstance(string cookie)
        {
            return new DKAMarshaler(cookie);
        }
    }

}