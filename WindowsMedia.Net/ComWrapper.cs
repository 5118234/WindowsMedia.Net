using System;
using System.Runtime.InteropServices;

namespace WindowsMedia
{
    public class ComWrapper<T> : IDisposable where T : class
    {
        private T _object;

        public ComWrapper(T @object) => _object = @object;

        ~ComWrapper()
        {
            Dispose(false);
        }

        public T Object => _object;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposable)
        {
            if (_object != null)
            {
                Marshal.ReleaseComObject(_object);
                _object = null;
            }
        }
    }
}