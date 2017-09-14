using System;
using System.Collections.Generic;

using WindowsMedia.Platform;

using MediaPortal.DShowNET;

namespace WindowsMedia
{
    /// <summary>
    /// Managed interface to access to platform <see cref="IBaseFilter"/> interface
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Gets an internal object to access via native methods.
        /// </summary>
        IBaseFilter Object { get; }

        /// <summary>
        /// Gets a filter class id.
        /// </summary>
        Guid ClassId { get; }

        /// <summary>
        /// Gets a filter name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether filter is DMO.
        /// </summary>
        /// <value>
        ///   <c>true</c> if filter is DMO; otherwise, <c>false</c>.
        /// </value>
        bool IsDmoFilter { get; }

        /// <summary>
        /// Gets the DMO category identifier.
        /// </summary>
        /// <value>
        /// The DMO category identifier.
        /// </value>
        Guid DmoCategoryIdentifier { get; }

        /// <summary>
        /// Extracts all filter pins
        /// </summary>
        /// <returns>Returns enumeration of the <see cref="Pin"/>.</returns>
        IEnumerable<Pin> GetPins();

        /// <summary>
        /// Finds filter pin by specified name and direction.
        /// </summary>
        /// <param name="name">The pin name.</param>
        /// <param name="direction">The pin direction.</param>
        /// <returns>Returns <see cref="Pin"/> instance if found; elsewhere returns <b>null</b>.</returns>
        Pin FindPinByName(string name, PinDirection direction);

        /// <summary>
        /// Scans all pins in the filter and return all not connected pins
        /// </summary>
        /// <param name="name"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IEnumerable<Pin> GetFreePins(string name, PinDirection direction);

        /// <summary>
        /// Finds filter pin by specified name.
        /// </summary>
        /// <param name="name">The pin name.</param>
        /// <returns>Returns <see cref="Pin"/> instance if found; elsewhere returns <b>null</b>.</returns>
        Pin FindPinByName(string name);

        /// <summary>
        /// Finds filter pin by specified direction and index.
        /// </summary>
        /// <param name="direction">The pin direction.</param>
        /// <param name="index">The pin index.</param>
        /// <returns>Returns <see cref="Pin"/> instance if found; elsewhere returns <b>null</b>.</returns>
        Pin GetPinByDirection(PinDirection direction, int index);

        /// <summary>
        /// Disconnects all pins.
        /// </summary>
        /// <param name="recursive">if set to <c>true</c> the operation is recursive.</param>
        /// <returns>Returns <b>true</b> if operation success; otherwise returns <b>false</b>.</returns>
        bool DisconnectAllPins(bool recursive = true);
    }
}