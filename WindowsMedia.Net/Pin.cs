using System;
using System.Collections.Generic;

using WindowsMedia.Platform;

namespace WindowsMedia
{
    /// <summary>
    /// Describe <see cref="IPin"/> wrapper class
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class Pin : IDisposable
    {
        private readonly Lazy<string> _pinName;

        private readonly Lazy<PinDirection> _pinDirection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pin"/> class.
        /// </summary>
        /// <param name="sourcePin">The source pin.</param>
        public Pin(IPin sourcePin)
        {
            Object = sourcePin;
            _pinName = new Lazy<string>(
                () =>
                    {
                        var result = "pin";
                        if (Object.QueryPinInfo(out var pinInfo) == 0)
                        {
                            try
                            {
                                result = pinInfo.name;
                            }
                            finally
                            {
                                DsUtils.FreePinInfo(pinInfo);
                            }
                        }

                        return result;
                    });

            _pinDirection = new Lazy<PinDirection>(
                () =>
                    {
                        var hr = Object.QueryDirection(out var pinDirection);
                        DsError.ThrowExceptionForHR(hr);
                        return pinDirection;
                    });
        }

        ~Pin()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Pin"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized => Object != null;

        /// <summary>
        /// Gets the pin name.
        /// </summary>
        public string Name => Initialized ? _pinName.Value : string.Empty;

        /// <summary>
        /// Gets the pin direction.
        /// </summary>
        public PinDirection Direction => Initialized ? _pinDirection.Value : PinDirection.Input;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Pin"/> is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if connected; otherwise, <c>false</c>.
        /// </value>
        public bool Connected
        {
            get
            {
                var mType = new AMMediaType();
                try
                {
                    return Object.ConnectionMediaType(mType) == 0;
                }
                finally
                {
                    DsUtils.FreeAMMediaType(mType);
                }
            }
        }

        /// <summary>
        /// Gets the pin identifier.
        /// </summary>
        public string Id => Object.QueryId(out var result) == 0 ? result : string.Empty;

        /// <summary>
        /// Gets an internal object to access via native methods.
        /// </summary>
        public IPin Object { get; private set; }

        /// <summary>
        /// Connects the specified receive pin.
        /// </summary>
        /// <param name="receivePin">The receive pin.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns>Returns <b>true</b> in case success pin was connected successfully; else returns <b>false</b>.</returns>
        public bool Connect(Pin receivePin, AMMediaType mediaType)
        {
            return Object.Connect(receivePin.Object, mediaType) == 0;
        }

        /// <summary>
        /// Disconnects this pin.
        /// </summary>
        /// <returns>Returns <b>true</b> in case success pin was connected successfully; else returns <b>false</b>.</returns>
        public bool Disconnect()
        {
            return Object.Disconnect() == 0;
        }

        /// <summary>
        /// Retrieves what pin is connected to.
        /// </summary>
        /// <returns>Returns <see cref="Pin"/> instance if pin is connected; elsewhere returns <b>null</b>.</returns>
        public Pin ConnectedTo()
        {
            var hr = Object.ConnectedTo(out var ppPin);
            if (hr != 0)
            {
                return null;
            }

            var result = new Pin(ppPin);
            return result;
        }

        /// <summary>
        /// Retrieves what pin is connected to.
        /// </summary>
        /// <returns>Returns <see cref="BaseFilter"/> instance if pin is connected; elsewhere returns <b>null</b>.</returns>
        public BaseFilter ConnectedToFilter()
        {
            var other = ConnectedTo();
            if (other != null)
            {
                var info = new PinInfo();
                BaseFilter result = null;
                try
                {
                    if (other.Object.QueryPinInfo(out info) == 0)
                    {
                        result = new BaseFilter(info.filter);
                    }
                }
                finally
                {
                    DsUtils.FreePinInfo(info);
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// Gets the pin media types.
        /// </summary>
        /// <returns>Returns collection of supported media types.</returns>
        public IEnumerable<AMMediaType> GetMediaTypes()
        {
            IEnumMediaTypes mediaTypes = null;
            try
            {
                var hr = Object.EnumMediaTypes(out mediaTypes);
                DsError.ThrowExceptionForHR(hr);
                var currentMediaType = new AMMediaType[1];
                while (mediaTypes.Next(1, currentMediaType, out _) == 0)
                {
                    try
                    {
                        yield return currentMediaType[0];
                    }
                    finally
                    {
                        DsUtils.FreeAMMediaType(currentMediaType[0]);
                    }
                }
            }
            finally
            {
                if (mediaTypes != null)
                {
                    DsUtils.ReleaseComObject(mediaTypes);
                }
            }
        }

        /// <summary>
        /// Disconnects the specified recursive.
        /// </summary>
        /// <param name="recursive">if set to <c>true</c> if operation id recursive.</param>
        /// <returns>Returns <b>true</b> in case success pin was disconnected successfully; else returns <b>false</b>.</returns>
        public bool Disconnect(bool recursive)
        {
            if (!recursive)
            {
                return Disconnect();
            }

            var result = true;
            using (var filter = ConnectedToFilter())
            {
                using (var connectedPin = ConnectedTo())
                {
                    if (connectedPin != null)
                    {
                        result &= connectedPin.Disconnect();
                    }
                }

                if (filter != null)
                {
                    result &= filter.DisconnectAllPins();
                }
            }

            result &= Disconnect();
            return result;
        }

        /// <summary>
        /// Connections the type of the media.
        /// </summary>
        /// <returns></returns>
        public AMMediaType ConnectionMediaType()
        {
            var result = new AMMediaType();
            Object.ConnectionMediaType(result);
            return result;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposed)
        {
            if (Object != null)
            {
                DsUtils.ReleaseComObject(Object);
                Object = null;
            }
        }
    }
}