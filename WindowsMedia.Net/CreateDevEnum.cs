using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using WindowsMedia.Platform;

namespace WindowsMedia
{
    public class CreateDevEnum : IDisposable
    {
        private ICreateDevEnum _object;

        public CreateDevEnum(ICreateDevEnum @object) => _object = @object;

        ~CreateDevEnum()
        {
            Dispose(false);
        }

        public IEnumerable<IMoniker> GetClasses(Guid category)
        {
            IEnumMoniker enumMon = null;
            try
            {
                var hr = _object.CreateClassEnumerator(category, out enumMon, 0);
                if (hr != 0)
                {
                    throw new NotSupportedException("No devices of the category");
                }

                var result = new IMoniker[1];
                while (enumMon.Next(1, result, IntPtr.Zero) == 0)
                {
                    yield return result[0];
                }
            }
            finally
            {
                if (enumMon != null)
                {
                    Marshal.ReleaseComObject(enumMon);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_object != null)
            {
                Marshal.ReleaseComObject(_object);
                _object = null;
            }
        }
    }
}