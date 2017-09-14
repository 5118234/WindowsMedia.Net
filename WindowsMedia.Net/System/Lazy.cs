using System.Threading;

namespace System
{
#if NET35

    namespace Threading
    {
        /// <summary>
        /// Specifies how a <see cref="Lazy{T}"/> instance synchronizes access among multiple threads.
        /// </summary>
        public enum LazyThreadSafetyMode
        {
            /// <summary>
            /// The <see cref="Lazy{T}"/> instance is not thread safe; if the instance is accessed from multiple threads, its behavior is undefined. 
            /// Use this mode only when high performance is crucial and the <see cref="Lazy{T}"/> instance is guaranteed never to be initialized from more than one thread. 
            /// </summary>
            None,

            /// <summary>
            /// When multiple threads try to initialize a <see cref="Lazy{T}"/> instance simultaneously, all threads are allowed to run the initialization method 
            /// (or the default constructor, if there is no initialization method). The first thread to complete initialization sets the value of the <see cref="Lazy{T}"/> instance.
            /// </summary>
            PublicationOnly,

            /// <summary>
            /// Locks are used to ensure that only a single thread can initialize a <see cref="Lazy{T}"/> instance in a thread-safe manner. 
            /// </summary>
            ExecutionAndPublication,
        }
    }

    /// <summary>
    /// Provides support for lazy initialization
    /// </summary>
    /// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
    public class Lazy<T>
    {
        private readonly Func<T> _factoryInstance;
        private bool _initialized;
        private readonly LazyThreadSafetyMode _mode;
        private readonly object _lockObject = new object();
        private T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lazy{T}"/> class that uses the specified initialization function and thread-safety mode.
        /// </summary>
        /// <param name="factoryInstance">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        /// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
        public Lazy(Func<T> factoryInstance, LazyThreadSafetyMode mode = LazyThreadSafetyMode.None)
        {
            _factoryInstance = factoryInstance;
            _mode = mode;
        }

        /// <summary>
        /// Gets the lazily initialized value of the current <see cref="Lazy{T}"/> instance.
        /// </summary>
        /// <value>
        /// The lazily initialized value of the current <see cref="Lazy{T}"/> instance.
        /// </value>
        public T Value
        {
            get
            {
                if (_initialized)
                {
                    return _value;
                }

                if (_mode == LazyThreadSafetyMode.None)
                {
                    _value = _factoryInstance();
                    _initialized = true;
                }
                else
                {
                    lock (_lockObject)
                    {
                        if (!_initialized)
                        {
                            _value = _factoryInstance();
                            _initialized = true;
                        }
                    }
                }

                return _value;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether a value has been created for this <see cref="Lazy{T}"/> instance.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is value created; otherwise, <c>false</c>.
        /// </value>
        public bool IsValueCreated => _initialized;
    }
#endif
}