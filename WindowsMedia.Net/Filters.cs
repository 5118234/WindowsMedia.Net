using WindowsMedia.Platform;

using JetBrains.Annotations;

namespace WindowsMedia
{
    /// <summary>
    ///  Provides collections of devices and compression codecs
    ///  installed on the system. 
    /// </summary>
    /// <example>
    ///  Devices and compression codecs are implemented in DirectShow 
    ///  as filters, see the <see cref="BaseFilter"/> class for more 
    ///  information. To list the available video devices:
    ///  <code><div style="background-color:whitesmoke;">
    ///   foreach (var f in Filters.VideoInputDevices)
    ///   {
    ///		  Debug.WriteLine(f.Name);
    ///   }
    ///  </div></code>
    ///  <seealso cref="BaseFilter"/>
    /// </example>
    [PublicAPI]
    public static class Filters
    {
        #region Properties

        /// <summary>
        /// Gets a collection of available video capture devices.
        /// </summary>
        public static FilterCollection VideoInputDevices { get; }

        /// <summary>
        /// Gets a collection of available audio capture devices.
        /// </summary>
        public static FilterCollection AudioInputDevices { get; }

        /// <summary>
        /// Gets a collection of available video compressors.
        /// </summary>
        public static FilterCollection VideoCompressors { get; }

        /// <summary>
        /// Gets a collection of available audio compressors.
        /// </summary>
        public static FilterCollection AudioCompressors { get; }

        /// <summary>
        /// Gets a collection of all legacy filters installed in the system.
        /// </summary>
        public static FilterCollection LegacyFilters { get; }

        /// <summary>
        /// Gets a collection of all audio renders installed in the system.
        /// </summary>
        public static FilterCollection AudioRenderers { get; }

        /// <summary>
        /// Gets a collection of WM encoders installed in the system.
        /// </summary>
        public static FilterCollection WdmEncoders { get; }

        /// <summary>
        /// Gets a collection of WM crossbars installed in the system.
        /// </summary>
        public static FilterCollection WdmCrossbars { get; }

        /// <summary>
        /// Gets a collection of WM TV tuners installed in the system.
        /// </summary>
        public static FilterCollection WdmTvTuners { get; }

        /// <summary>
        /// Gets a collection of BDA recievers installed in the system.
        /// </summary>
        public static FilterCollection BdaReceivers { get; }

        /// <summary>
        /// Gets a collection of all filters installed in the system.
        /// </summary>
        public static FilterCollection AllFilters { get; }

        /// <summary>
        /// Gets a collection of legacy filers and audio renders installed in the system.
        /// </summary>
        public static FilterCollection CompleteFilters { get; }

        #endregion

        static Filters()
        {
            VideoInputDevices = new FilterCollection(FilterCategory.VideoInputDevice);
            AudioInputDevices = new FilterCollection(FilterCategory.AudioInputDevice);
            VideoCompressors = new FilterCollection(FilterCategory.VideoCompressorCategory);
            AudioCompressors = new FilterCollection(FilterCategory.AudioCompressorCategory);
            LegacyFilters = new FilterCollection(FilterCategory.LegacyAmFilterCategory);
            AudioRenderers = new FilterCollection(FilterCategory.AudioRendererCategory);
            WdmEncoders = new FilterCollection(FilterCategory.WDMStreamingEncoderDevices);
            WdmCrossbars = new FilterCollection(FilterCategory.AMKSCrossbar);
            WdmTvTuners = new FilterCollection(FilterCategory.AMKSTVTuner);
            BdaReceivers = new FilterCollection(FilterCategory.BDAReceiverComponentsCategory);
            AllFilters = new FilterCollection(FilterCategory.ActiveMovieCategories);
            CompleteFilters = new FilterCollection();
            CompleteFilters.AddRange(LegacyFilters, false);
            CompleteFilters.AddRange(AudioRenderers);
        }
    }
}