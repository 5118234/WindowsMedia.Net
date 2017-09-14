using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

using WindowsMedia.Platform;

#pragma warning disable 618

namespace WindowsMedia
{
    /// <summary>
    ///	 A collection of Filter objects (DirectShow filters) to provide
    ///	 lists of capture devices and compression filters. This class
    ///	 cannot be created directly.
    /// </summary>
    public class FilterCollection : CollectionBase
    {
        private class ReverserComparer : IComparer
        {
            /// <inheritdoc />
            int IComparer.Compare(object x, object y)
            {
                return -(new CaseInsensitiveComparer().Compare(y?.ToString(), x?.ToString()));
            }

        }

        private static readonly IComparer Comparer = new ReverserComparer();
        private readonly Dictionary<string, BaseFilter> _names = new Dictionary<string, BaseFilter>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<Guid, BaseFilter> _guids = new Dictionary<Guid, BaseFilter>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCollection"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="resolveNames">if set to <c>true</c> [resolve names].</param>
        public FilterCollection(Guid category, bool resolveNames = true)
        {
            GetFilters(category);
            if (resolveNames)
                Resolve();
        }

        private void Resolve()
        {
            for (var i = 0; i < InnerList.Count; )
            {
                if (InnerList[i] is BaseFilter f)
                {
                    if (!_guids.ContainsKey(f.ClassId))
                    {
                        _guids.Add(f.ClassId, f);
                    }

                    if (!_names.ContainsKey(f.Name))
                    {
                        _names.Add(f.Name, f);
                        i++;
                    }
                    else
                    {
                        InnerList.RemoveAt(i);
                    }
                }
                else
                    i++;
            }

            InnerList.Sort(Comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCollection"/> class.
        /// </summary>
        public FilterCollection()
        {
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="resolveNames">if set to <c>true</c> if need resolve names on add.</param>
        public void AddRange(FilterCollection collection, bool resolveNames = true)
        {
            InnerList.AddRange(collection.InnerList);
            if (resolveNames)
            {
                Resolve();
            }
        }

        /// <summary> 
        /// Populate the InnerList with a list of filters from a particular category 
        /// </summary>
        protected void GetFilters(Guid category)
        {
            using (var enumDev = new CreateDevEnum((ICreateDevEnum)new Platform.CreateDevEnum()))
            {
                foreach (var moniker in enumDev.GetClasses(category))
                {
                    using (new ComWrapper<IMoniker>(moniker))
                    {
                        InnerList.Add(new BaseFilter(moniker));
                    }
                }
            }
        }

        /// <summary> 
        /// Gets the filter at the specified index. 
        /// </summary>
        public BaseFilter this[int index]
        {
            get
            {
                if (index >= InnerList.Count)
                {
                    return null;
                }

                return (BaseFilter)InnerList[index];
            }
        }

        /// <summary>Gets filter by name</summary>
        /// <param name="name">filter name</param>
        /// <returns>Filter interface</returns>
        public BaseFilter GetByName(string name)
        {
            return _names.ContainsKey(name) ? _names[name] : null;
        }

        /// <summary>Gets filter by guid</summary>
        /// <param name="identifier">System identifier</param>
        /// <returns>Filter interface</returns>
        public BaseFilter GetByGuid(Guid identifier)
        {
            return _guids.ContainsKey(identifier) ? _guids[identifier] : null;
        }

        /// <summary>Gets filter by identifier</summary>
        /// <param name="identifier">System identifier</param>
        /// <returns>Returns <see cref="BaseFilter"/> instance if filter was installed in the system; elsewhere returns <b>null</b>.</returns>
        public BaseFilter GetByGuid(string identifier)
        {
            try
            {
                var filterGuid = new Guid(identifier);
                return _guids.ContainsKey(filterGuid) ? _guids[filterGuid] : null;
            }
            catch
            {
                return null;
            }
        }
    }
}