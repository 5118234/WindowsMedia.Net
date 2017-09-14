using System;
using System.Collections.Generic;
using System.Linq;

using WindowsMedia.Platform;

namespace WindowsMedia
{
    /// <summary>
    /// Implementation of <see cref="IFilter"/> to test.
    /// </summary>
    /// <seealso cref="IFilter" />
    public sealed class EmptyFilter : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyFilter"/> class.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        public EmptyFilter(string filterName = "Empty filter")
        {
            ClassId = Guid.NewGuid();
            Name = filterName;
            IsDmoFilter = false;
            DmoCategoryIdentifier = Guid.Empty;
        }

        /// <inheritdoc />
        public IBaseFilter Object => null;

        /// <inheritdoc />
        public Guid ClassId { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool IsDmoFilter { get; }

        /// <inheritdoc />
        public Guid DmoCategoryIdentifier { get; }

        /// <inheritdoc />
        public IEnumerable<Pin> GetPins()
        {
            return Enumerable.Empty<Pin>();
        }

        /// <inheritdoc />
        public Pin FindPinByName(string name, PinDirection direction)
        {
            return null;
        }

        /// <inheritdoc />
        public IEnumerable<Pin> GetFreePins(string name, PinDirection direction)
        {
            return Enumerable.Empty<Pin>();
        }

        /// <inheritdoc />
        public Pin FindPinByName(string name)
        {
            return null;
        }

        /// <inheritdoc />
        public Pin GetPinByDirection(PinDirection direction, int index)
        {
            return null;
        }

        /// <inheritdoc />
        public bool DisconnectAllPins(bool recursive = true)
        {
            return true;
        }
    }
}