using System;
using System.Drawing;
using System.Runtime.InteropServices;

using JetBrains.Annotations;

namespace WindowsMedia.Platform
{
    #region Declarations

    public enum EventCode
    {
        // EvCod.h
        Complete = 0x01, // EC_COMPLETE
        UserAbort = 0x02, // EC_USERABORT
        ErrorAbort = 0x03, // EC_ERRORABORT
        Time = 0x04, // EC_TIME
        Repaint = 0x05, // EC_REPAINT
        StErrStopped = 0x06, // EC_STREAM_ERROR_STOPPED
        StErrStPlaying = 0x07, // EC_STREAM_ERROR_STILLPLAYING
        ErrorStPlaying = 0x08, // EC_ERROR_STILLPLAYING
        PaletteChanged = 0x09, // EC_PALETTE_CHANGED
        VideoSizeChanged = 0x0a, // EC_VIDEO_SIZE_CHANGED
        QualityChange = 0x0b, // EC_QUALITY_CHANGE
        ShuttingDown = 0x0c, // EC_SHUTTING_DOWN
        ClockChanged = 0x0d, // EC_CLOCK_CHANGED
        Paused = 0x0e, // EC_PAUSED
        OpeningFile = 0x10, // EC_OPENING_FILE
        BufferingData = 0x11, // EC_BUFFERING_DATA
        FullScreenLost = 0x12, // EC_FULLSCREEN_LOST
        Activate = 0x13, // EC_ACTIVATE
        NeedRestart = 0x14, // EC_NEED_RESTART
        WindowDestroyed = 0x15, // EC_WINDOW_DESTROYED
        DisplayChanged = 0x16, // EC_DISPLAY_CHANGED
        Starvation = 0x17, // EC_STARVATION
        OleEvent = 0x18, // EC_OLE_EVENT
        NotifyWindow = 0x19, // EC_NOTIFY_WINDOW
        StreamControlStopped = 0x1A, // EC_STREAM_CONTROL_STOPPED
        StreamControlStarted = 0x1B, // EC_STREAM_CONTROL_STARTED
        EndOfSegment = 0x1C, // EC_END_OF_SEGMENT
        SegmentStarted = 0x1D, // EC_SEGMENT_STARTED
        LengthChanged = 0x1E, // EC_LENGTH_CHANGED
        DeviceLost = 0x1f, // EC_DEVICE_LOST
        SampleNeeded = 0x20, // EC_SAMPLE_NEEDED
        ProcessingLatency = 0x21, // EC_PROCESSING_LATENCY
        SampleLatency = 0x22, // EC_SAMPLE_LATENCY
        ScrubTime = 0x23, // EC_SCRUB_TIME
        StepComplete = 0x24, // EC_STEP_COMPLETE
        SkipFrames = 0x25, // EC_SKIP_FRAMES

        TimeCodeAvailable = 0x30, // EC_TIMECODE_AVAILABLE
        ExtDeviceModeChange = 0x31, // EC_EXTDEVICE_MODE_CHANGE
        StateChange = 0x32, // EC_STATE_CHANGE

        PleaseReOpen = 0x40, // EC_PLEASE_REOPEN
        Status = 0x41, // EC_STATUS
        MarkerHit = 0x42, // EC_MARKER_HIT
        LoadStatus = 0x43, // EC_LOADSTATUS
        FileClosed = 0x44, // EC_FILE_CLOSED
        ErrorAbortEx = 0x45, // EC_ERRORABORTEX
        EOSSoon = 0x046, // EC_EOS_SOON
        ContentPropertyChanged = 0x47, // EC_CONTENTPROPERTY_CHANGED
        BandwidthChange = 0x48, // EC_BANDWIDTHCHANGE
        VideoFrameReady = 0x49, // EC_VIDEOFRAMEREADY

        GraphChanged = 0x50, // EC_GRAPH_CHANGED
        ClockUnset = 0x51, // EC_CLOCK_UNSET
        VMRRenderDeviceSet = 0x53, // EC_VMR_RENDERDEVICE_SET
        VMRSurfaceFlipped = 0x54, // EC_VMR_SURFACE_FLIPPED
        VMRReconnectionFailed = 0x55, // EC_VMR_RECONNECTION_FAILED
        PreprocessComplete = 0x56, // EC_PREPROCESS_COMPLETE
        CodecApiEvent = 0x57, // EC_CODECAPI_EVENT

        // DVDevCod.h
        DvdDomainChange = 0x101, // EC_DVD_DOMAIN_CHANGE
        DvdTitleChange = 0x102, // EC_DVD_TITLE_CHANGE
        DvdChapterStart = 0x103, // EC_DVD_CHAPTER_START
        DvdAudioStreamChange = 0x104, // EC_DVD_AUDIO_STREAM_CHANGE
        DvdSubPicictureStreamChange = 0x105, // EC_DVD_SUBPICTURE_STREAM_CHANGE
        DvdAngleChange = 0x106, // EC_DVD_ANGLE_CHANGE
        DvdButtonChange = 0x107, // EC_DVD_BUTTON_CHANGE
        DvdValidUopsChange = 0x108, // EC_DVD_VALID_UOPS_CHANGE
        DvdStillOn = 0x109, // EC_DVD_STILL_ON
        DvdStillOff = 0x10a, // EC_DVD_STILL_OFF
        DvdCurrentTime = 0x10b, // EC_DVD_CURRENT_TIME
        DvdError = 0x10c, // EC_DVD_ERROR
        DvdWarning = 0x10d, // EC_DVD_WARNING
        DvdChapterAutoStop = 0x10e, // EC_DVD_CHAPTER_AUTOSTOP
        DvdNoFpPgc = 0x10f, // EC_DVD_NO_FP_PGC
        DvdPlaybackRateChange = 0x110, // EC_DVD_PLAYBACK_RATE_CHANGE
        DvdParentalLevelChange = 0x111, // EC_DVD_PARENTAL_LEVEL_CHANGE
        DvdPlaybackStopped = 0x112, // EC_DVD_PLAYBACK_STOPPED
        DvdAnglesAvailable = 0x113, // EC_DVD_ANGLES_AVAILABLE
        DvdPlayPeriodAutoStop = 0x114, // EC_DVD_PLAYPERIOD_AUTOSTOP
        DvdButtonAutoActivated = 0x115, // EC_DVD_BUTTON_AUTO_ACTIVATED
        DvdCmdStart = 0x116, // EC_DVD_CMD_START
        DvdCmdEnd = 0x117, // EC_DVD_CMD_END
        DvdDiscEjected = 0x118, // EC_DVD_DISC_EJECTED
        DvdDiscInserted = 0x119, // EC_DVD_DISC_INSERTED
        DvdCurrentHmsfTime = 0x11a, // EC_DVD_CURRENT_HMSF_TIME
        DvdKaraokeMode = 0x11b, // EC_DVD_KARAOKE_MODE
        DvdProgramCellChange = 0x11c, // EC_DVD_PROGRAM_CELL_CHANGE
        DvdTitleSetChange = 0x11d, // EC_DVD_TITLE_SET_CHANGE
        DvdProgramChainChange = 0x11e, // EC_DVD_PROGRAM_CHAIN_CHANGE
        DvdVOBU_Offset = 0x11f, // EC_DVD_VOBU_Offset
        DvdVOBU_Timestamp = 0x120, // EC_DVD_VOBU_Timestamp
        DvdGPRM_Change = 0x121, // EC_DVD_GPRM_Change
        DvdSPRM_Change = 0x122, // EC_DVD_SPRM_Change
        DvdBeginNavigationCommands = 0x123, // EC_DVD_BeginNavigationCommands
        DvdNavigationCommand = 0x124, // EC_DVD_NavigationCommand

        // AudEvCod.h
        SNDDEVInError = 0x200, // EC_SNDDEV_IN_ERROR
        SNDDEVOutError = 0x201, // EC_SNDDEV_OUT_ERROR

        WMTIndexEvent = 0x0251, // EC_WMT_INDEX_EVENT
        WMTEvent = 0x0252, // EC_WMT_EVENT

        Built = 0x300, // EC_BUILT
        Unbuilt = 0x301, // EC_UNBUILT

        // Sbe.h
        StreamBufferTimeHole = 0x0326, // STREAMBUFFER_EC_TIMEHOLE
        StreamBufferStaleDataRead = 0x0327, // STREAMBUFFER_EC_STALE_DATA_READ
        StreamBufferStaleFileDeleted = 0x0328, // STREAMBUFFER_EC_STALE_FILE_DELETED
        StreamBufferContentBecomingStale = 0x0329, // STREAMBUFFER_EC_CONTENT_BECOMING_STALE
        StreamBufferWriteFailure = 0x032a, // STREAMBUFFER_EC_WRITE_FAILURE
        StreamBufferReadFailure = 0x032b, // STREAMBUFFER_EC_READ_FAILURE
        StreamBufferRateChanged = 0x032c, // STREAMBUFFER_EC_RATE_CHANGED
    }

    /// <summary>
    /// From CDEF_CLASS_* defines
    /// </summary>
    [Flags]
    public enum CDef
    {
        None = 0,
        ClassDefault = 0x0001,
        BypassClassManager = 0x0002,
        ClassLegacy = 0x0004,
        MeritAboveDoNotUse = 0x0008,
        DevmonCMGRDevice = 0x0010,
        DevmonDMO = 0x0020,
        DevmonPNPDevice = 0x0040,
        DevmonFilter = 0x0080,
        DevmonSelectiveMask = 0x00f0
    }

    /// <summary>
    /// From define AM_MEDIAEVENT_NONOTIFY
    /// </summary>
    [Flags]
    public enum NotifyFlags
    {
        None,
        NoNotify
    }

    /// <summary>
    /// From #define OATRUE/OAFALSE
    /// </summary>
    public enum OABool
    {
        False = 0,
        True = -1 // bools in .NET use 1, not -1
    }

    /// <summary>
    /// From WS_* defines
    /// </summary>
    [Flags]
    public enum WindowStyle
    {
        Overlapped = 0x00000000,
        Popup = unchecked((int)0x80000000), // enum can't be uint for VB
        Child = 0x40000000,
        Minimize = 0x20000000,
        Visible = 0x10000000,
        Disabled = 0x08000000,
        ClipSiblings = 0x04000000,
        ClipChildren = 0x02000000,
        Maximize = 0x01000000,
        Caption = 0x00C00000,
        Border = 0x00800000,
        DlgFrame = 0x00400000,
        VScroll = 0x00200000,
        HScroll = 0x00100000,
        SysMenu = 0x00080000,
        ThickFrame = 0x00040000,
        Group = 0x00020000,
        TabStop = 0x00010000,
        MinimizeBox = 0x00020000,
        MaximizeBox = 0x00010000
    }

    /// <summary>
    /// From WS_EX_* defines
    /// </summary>
    [Flags]
    public enum WindowStyleEx
    {
        DlgModalFrame = 0x00000001,
        NoParentNotify = 0x00000004,
        Topmost = 0x00000008,
        AcceptFiles = 0x00000010,
        Transparent = 0x00000020,
        MDIChild = 0x00000040,
        ToolWindow = 0x00000080,
        WindowEdge = 0x00000100,
        ClientEdge = 0x00000200,
        ContextHelp = 0x00000400,
        Right = 0x00001000,
        Left = 0x00000000,
        RTLReading = 0x00002000,
        LTRReading = 0x00000000,
        LeftScrollBar = 0x00004000,
        RightScrollBar = 0x00000000,
        ControlParent = 0x00010000,
        StaticEdge = 0x00020000,
        APPWindow = 0x00040000,
        Layered = 0x00080000,
        NoInheritLayout = 0x00100000,
        LayoutRTL = 0x00400000,
        Composited = 0x02000000,
        NoActivate = 0x08000000
    }

    /// <summary>
    /// From SW_* defines
    /// </summary>
    public enum WindowState
    {
        Hide = 0,
        Normal,
        ShowMinimized,
        ShowMaximized,
        ShowNoActivate,
        Show,
        Minimize,
        ShowMinNoActive,
        ShowNA,
        Restore,
        ShowDefault,
        ForceMinimize
    }


    /// <summary>
    /// From DISPATCH_* defines
    /// </summary>
    [Flags]
    public enum DispatchFlags : short
    {
        None = 0x0,
        Method = 0x1,
        PropertyGet = 0x2,
        PropertyPut = 0x4,
        PropertyPutRef = 0x8
    }

    /// <summary>
    /// From KSMULTIPLE_ITEM - Note that data is returned in the memory IMMEDIATELY following this struct.
    /// The Size parm indicates ths size of the KSMultipleItem plus the extra bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class KSMultipleItem
    {
        public int Size;
        public int Count;
    }

    /// <summary>
    /// From AM_GBF_* defines
    /// </summary>
    [Flags]
    public enum AMGBF
    {
        None = 0,
        PrevFrameSkipped = 1,
        NotAsyncPoint = 2,
        NoWait = 4,
        NoDDSurfaceLock = 8
    }

    /// <summary>
    /// From AM_VIDEO_FLAG_* defines
    /// </summary>
    [Flags]
    public enum AMVideoFlag
    {
        FieldMask = 0x0003,
        InterleavedFrame = 0x0000,
        Field1 = 0x0001,
        Field2 = 0x0002,
        Field1First = 0x0004,
        Weave = 0x0008,
        IPBMask = 0x0030,
        ISample = 0x0000,
        PSample = 0x0010,
        BSample = 0x0020,
        RepeatField = 0x0040
    }

    /// <summary>
    /// From AM_SAMPLE_PROPERTY_FLAGS
    /// </summary>
    [Flags]
    public enum AMSamplePropertyFlags
    {
        SplicePoint = 0x01,
        PreRoll = 0x02,
        DataDiscontinuity = 0x04,
        TypeChanged = 0x08,
        TimeValid = 0x10,
        MediaTimeValid = 0x20,
        TimeDiscontinuity = 0x40,
        FlushOnPause = 0x80,
        StopValid = 0x100,
        EndOfStream = 0x200,
        Media = 0,
        Control = 1
    }

    /// <summary>
    /// From PIN_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PinInfo
    {
        [MarshalAs(UnmanagedType.Interface)] public IBaseFilter filter;
        public PinDirection dir;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string name;
    }

    /// <summary>
    /// From AM_MEDIA_TYPE - When you are done with an instance of this class,
    /// it should be released with FreeAMMediaType() to avoid leaking
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AMMediaType
    {
        public Guid majorType;
        public Guid subType;
        [MarshalAs(UnmanagedType.Bool)] public bool fixedSizeSamples;
        [MarshalAs(UnmanagedType.Bool)] public bool temporalCompression;
        public int sampleSize;
        public Guid formatType;
        public IntPtr unkPtr; // IUnknown Pointer
        public int formatSize;
        public IntPtr formatPtr; // Pointer to a buff determined by formatType

        public override string ToString()
        {
            return "{" + majorType + "},{" + subType + "}";
        }
    }

    /// <summary>
    /// From PIN_DIRECTION
    /// </summary>
    public enum PinDirection
    {
        Input,
        Output
    }

    /// <summary>
    /// From AM_SEEKING_SeekingCapabilities
    /// </summary>
    [Flags]
    public enum AMSeekingSeekingCapabilities
    {
        None = 0,
        CanSeekAbsolute = 0x001,
        CanSeekForwards = 0x002,
        CanSeekBackwards = 0x004,
        CanGetCurrentPos = 0x008,
        CanGetStopPos = 0x010,
        CanGetDuration = 0x020,
        CanPlayBackwards = 0x040,
        CanDoSegments = 0x080,
        Source = 0x100
    }

    /// <summary>
    /// From FILTER_STATE
    /// </summary>
    public enum FilterState
    {
        Stopped,
        Paused,
        Running
    }

    /// <summary>
    /// From FILTER_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FilterInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string achName;
        [MarshalAs(UnmanagedType.Interface)] public IFilterGraph pGraph;
    }

    /// <summary>
    /// From AM_SEEKING_SeekingFlags
    /// </summary>
    [Flags]
    public enum AMSeekingSeekingFlags
    {
        NoPositioning = 0x00,
        AbsolutePositioning = 0x01,
        RelativePositioning = 0x02,
        IncrementalPositioning = 0x03,
        PositioningBitsMask = 0x03,
        SeekToKeyFrame = 0x04,
        ReturnTime = 0x08,
        Segment = 0x10,
        NoFlush = 0x20
    }

    /// <summary>
    /// From ALLOCATOR_PROPERTIES
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AllocatorProperties
    {
        public int cBuffers;
        public int cbBuffer;
        public int cbAlign;
        public int cbPrefix;
    }

    /// <summary>
    /// From AM_SAMPLE2_PROPERTIES
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AMSample2Properties
    {
        public int cbData;
        public AMVideoFlag dwTypeSpecificFlags;
        public AMSamplePropertyFlags dwSampleFlags;
        public int lActual;
        public long tStart;
        public long tStop;
        public int dwStreamId;
        public IntPtr pMediaType;
        public IntPtr pbBuffer; // BYTE *
        public int cbBuffer;
    }

    /// <summary>
    /// From AMINTERLACE_*
    /// </summary>
    [Flags]
    public enum AMInterlace
    {
        None = 0,
        IsInterlaced = 0x00000001,
        OneFieldPerSample = 0x00000002,
        Field1First = 0x00000004,
        Unused = 0x00000008,
        FieldPatternMask = 0x00000030,
        FieldPatField1Only = 0x00000000,
        FieldPatField2Only = 0x00000010,
        FieldPatBothRegular = 0x00000020,
        FieldPatBothIrregular = 0x00000030,
        DisplayModeMask = 0x000000c0,
        DisplayModeBobOnly = 0x00000000,
        DisplayModeWeaveOnly = 0x00000040,
        DisplayModeBobOrWeave = 0x00000080,
    }

    /// <summary>
    /// From AMCOPYPROTECT_*
    /// </summary>
    public enum AMCopyProtect
    {
        None = 0,
        RestrictDuplication = 0x00000001
    }

    /// <summary>
    /// From AMCONTROL_*
    /// </summary>
    [Flags]
    public enum AMControl
    {
        None = 0,
        Used = 0x00000001,
        PadTo4x3 = 0x00000002,
        PadTo16x9 = 0x00000004,
    }

    /// <summary>
    /// From VIDEOINFOHEADER
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class VideoInfoHeader
    {
        public DsRect SrcRect;
        public DsRect TargetRect;
        public int BitRate;
        public int BitErrorRate;
        public long AvgTimePerFrame;
        public BitmapInfoHeader BmiHeader;
    }

    /// <summary>
    /// From VIDEOINFOHEADER2
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class VideoInfoHeader2
    {
        public DsRect SrcRect;
        public DsRect TargetRect;
        public int BitRate;
        public int BitErrorRate;
        public long AvgTimePerFrame;
        public AMInterlace InterlaceFlags;
        public AMCopyProtect CopyProtectFlags;
        public int PictAspectRatioX;
        public int PictAspectRatioY;
        public AMControl ControlFlags;
        public int Reserved2;
        public BitmapInfoHeader BmiHeader;
    }

    /// <summary>
    /// From WAVEFORMATEX
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class WaveFormatEx
    {
        public short wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;
    }

    #endregion

    #region Declarations

    /// <summary>
    /// Not from DirectShow
    /// </summary>
    public enum PinConnectedStatus
    {
        /// <summary>
        /// Pin is not connected
        /// </summary>
        Unconnected,

        /// <summary>
        /// Pin is connected
        /// </summary>
        Connected
    }

    /// <summary>
    /// The structure defines the dimensions and color information for a DIB.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [PublicAPI]
    public struct BitmapInfo
    {
        /// <summary>
        /// The structure that contains information about the dimensions of color format.
        /// </summary>
        public BitmapInfoHeader bmiHeader;

        /// <summary>
        /// The bmiColors member contains one of the following:
        /// <list type="bullet">
        /// <item><description>An array of RGBQUAD. The elements of the array that make up the color table.</description></item>
        /// <item><description>An array of 16-bit unsigned integers that specifies indexes into the currently 
        /// realized logical palette. This use of bmiColors is allowed for functions that use DIBs. 
        /// When bmiColors elements contain indexes to a realized logical palette, they must also call the following bitmap functions:
        /// <list type="bullet">
        /// <item><description><b>CreateDIBitmap</b></description></item>
        /// <item><description><b>CreateDIBPatternBrush</b></description></item>
        /// <item><description><b>CreateDIBSection</b></description></item>
        /// </list>
        /// </description></item>
        /// </list>
        /// </summary>
        public int[] bmiColors;
    }

    /// <summary>
    /// The structure contains information about the dimensions and color format of a DIB.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    [PublicAPI]
    public class BitmapInfoHeader
    {
        /// <summary>
        /// The number of bytes required by the structure.
        /// </summary>
        public int Size;

        /// <summary>
        /// The width of the bitmap, in pixels.
        /// If <see cref="Compression"/> is <b>BI_JPEG</b> or <b>BI_PNG</b>, the <see cref="Width"/> member specifies the width of the 
        /// decompressed JPEG or PNG image file, respectively.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of the bitmap, in pixels.If <see cref="Height"/> is positive, the bitmap is a bottom-up DIB and its origin is the 
        /// lower-left corner.If <see cref="Height"/> is negative, the bitmap is a top-down DIB and its origin is the upper-left corner.
        /// If <see cref="Height"/> is negative, indicating a top-down DIB, biCompression must be either <b>BI_RGB</b> or <b>BI_BITFIELDS</b>.
        /// Top-down DIBs cannot be compressed.
        /// If <see cref="Compression"/> is <b>BI_JPEG</b> or <b>BI_PNG</b>, the <see cref="Height"/> member specifies the height of the 
        /// decompressed JPEG or PNG image file, respectively.
        /// </summary>
        public int Height;

        /// <summary>
        /// The number of planes for the target device. This value must be set to 1.
        /// </summary>
        public short Planes;

        /// <summary>
        /// The number of bits-per-pixel. The <see cref="BitCount"/> member of the <see cref="BitmapInfoHeader"/> structure determines the 
        /// number of bits that define each pixel and the maximum number of colors in the bitmap. This member must be one of the following values.
        /// <list type="bullet">
        /// <item><description>0 - The number of bits-per-pixel is specified or is implied by the JPEG or PNG format.</description></item>
        /// <item><description>1 - 	The bitmap is monochrome, and the <see cref="BitmapInfo.bmiColors"/> member of BITMAPINFO contains two entries. 
        /// Each bit in the bitmap array represents a pixel. If the bit is clear, the pixel is displayed with the color of the first 
        /// entry in the bmiColors table; if the bit is set, the pixel has the color of the second entry in the table.</description></item>
        /// <item><description>4 - The bitmap has a maximum of 16 colors, and the <see cref="BitmapInfo.bmiColors"/> member of BITMAPINFO 
        /// contains up to 16 entries. Each pixel in the bitmap is represented by a 4-bit index into the color table. For example, if the 
        /// first byte in the bitmap is 0x1F, the byte represents two pixels. The first pixel contains the color in the second table entry, 
        /// and the second pixel contains the color in the sixteenth table entry.</description></item>
        /// <item><description>8 - The bitmap has a maximum of 256 colors, and the <see cref="BitmapInfo.bmiColors"/> member of BITMAPINFO contains up to 256 entries. 
        /// In this case, each byte in the array represents a single pixel.</description></item>
        /// </list>
        /// </summary>
        public short BitCount;

        /// <summary>
        /// The type of compression for a compressed bottom-up bitmap (top-down DIBs cannot be compressed). This member can be one of the following values.
        /// <list type="bullet">
        /// <item><description>BI_RGB - An uncompressed format.</description></item>
        /// <item><description>BI_RLE8 - A run-length encoded (RLE) format for bitmaps with 8 bpp. The compression format is a 2-byte format consisting of a count byte followed by a byte containing a color index.</description></item>
        /// <item><description>BI_RLE4 - An RLE format for bitmaps with 4 bpp. The compression format is a 2-byte format consisting of a count byte followed by two word-length color indexes.</description></item>
        /// <item><description>BI_BITFIELDS - Specifies that the bitmap is not compressed and that the color table consists of three DWORD color masks that specify the red, green, and blue components, respectively, of each pixel. This is valid when used with 16- and 32-bpp bitmaps.</description></item>
        /// <item><description>BI_JPEG - Indicates that the image is a JPEG image.</description></item>
        /// <item><description>BI_PNG - Indicates that the image is a PNG image.</description></item>
        /// </list>
        /// </summary>
        public int Compression;

        /// <summary>
        /// The size, in bytes, of the image. This may be set to zero for BI_RGB bitmaps.
        /// </summary>
        public int ImageSize;

        /// <summary>
        /// The horizontal resolution, in pixels-per-meter, of the target device for the bitmap. 
        /// An application can use this value to select a bitmap from a resource group that best matches the characteristics of the 
        /// current device.
        /// </summary>
        public int XPelsPerMeter;

        /// <summary>
        /// The vertical resolution, in pixels-per-meter, of the target device for the bitmap.
        /// </summary>
        public int YPelsPerMeter;

        /// <summary>
        /// The number of color indexes in the color table that are actually used by the bitmap. If this value is zero, the bitmap uses 
        /// the maximum number of colors corresponding to the value of the <see cref="BitCount"/> member for the compression mode specified 
        /// by <see cref="Compression"/>.
        /// If <see cref="ClrUsed"/> is nonzero and the biBitCount member is less than 16, the <see cref="ClrUsed"/> member specifies the 
        /// actual number of colors the graphics engine or device driver accesses. If <see cref="BitCount"/> is 16 or greater, the <see cref="ClrUsed"/> 
        /// member specifies the size of the color table used to optimize performance of the system color palettes.If <see cref="BitCount"/> equals 
        /// 16 or 32, the optimal color palette starts immediately following the three DWORD masks.
        /// When the bitmap array immediately follows the <see cref="BitmapInfo"/> structure, it is a packed bitmap.Packed bitmaps are referenced by a 
        /// single pointer. Packed bitmaps require that the <see cref="ClrUsed"/> member must be either zero or the actual size of the color table.
        /// </summary>
        public int ClrUsed;

        /// <summary>
        /// The number of color indexes that are required for displaying the bitmap. If this value is zero, all colors are required.
        /// </summary>
        public int ClrImportant;
    }

    /// <summary>
    /// The structure describes the pixel format of a DirectDrawSurface object. 
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [PublicAPI]
    public struct DDPixelFormat
    {
        /// <summary>
        /// Specifies the size in bytes of the <see cref="DDPixelFormat"/> structure. The driver must initialize this member before the structure is used.
        /// </summary>
        [FieldOffset(0)]
        public int dwSize;

        /// <summary>
        /// Indicates a set of flags that specify optional control flags. This member is a bitwise OR of any of the following values:
        /// </summary>
        [FieldOffset(4)]
        public int dwFlags;

        /// <summary>
        /// Specifies a surface format code including any of the codes in the D3DFORMAT enumerated type. Some FOURCC codes are part of 
        /// D3DFORMAT. For more information about D3DFORMAT, see the SDK documentation. Hardware vendors can also define and supply format 
        /// codes that are specific to their hardware. 
        /// </summary>
        [FieldOffset(8)]
        public int dwFourCC;

        /// <summary>
        /// Specifies the number of RGB bits per pixel (4, 8, 16, 24, or 32). 
        /// </summary>
        [FieldOffset(12)]
        public int dwRGBBitCount;

        /// <summary>
        /// Specifies the number of YUV bits per pixel. 
        /// </summary>
        [FieldOffset(12)]
        public int dwYUVBitCount;

        /// <summary>
        /// Specifies the Z-buffer bit depth (8, 16, 24, or 32 bits). 
        /// </summary>
        [FieldOffset(12)]
        public int dwZBufferBitDepth;

        /// <summary>
        /// Specifies the Alpha channel bit depth. 
        /// </summary>
        [FieldOffset(12)]
        public int dwAlphaBitDepth;

        /// <summary>
        /// Specifies the mask for red bits.
        /// </summary>
        [FieldOffset(16)]
        public int dwRBitMask;

        /// <summary>
        /// Specifies the mask for Y bits.
        /// </summary>
        [FieldOffset(16)]
        public int dwYBitMask;

        /// <summary>
        /// Specifies the mask for green bits. 
        /// </summary>
        [FieldOffset(20)]
        public int dwGBitMask;

        /// <summary>
        /// Specifies the mask for U bits. 
        /// </summary>
        [FieldOffset(20)]
        public int dwUBitMask;

        /// <summary>
        /// Specifies the mask for blue bits. 
        /// </summary>
        [FieldOffset(24)]
        public int dwBBitMask;

        /// <summary>
        /// Specifies the mask for V bits. 
        /// </summary>
        [FieldOffset(24)]
        public int dwVBitMask;

        /// <summary>
        /// Specify the masks for alpha channel. 
        /// </summary>
        [FieldOffset(28)]
        public int dwRGBAlphaBitMask;

        /// <summary>
        /// Specify the masks for alpha channel. 
        /// </summary>
        [FieldOffset(28)]
        public int dwYUVAlphaBitMask;

        /// <summary>
        /// Specifies the masks for the z channel. 
        /// </summary>
        [FieldOffset(28)]
        public int dwRGBZBitMask;

        /// <summary>
        /// Specifies the masks for the z channel. 
        /// </summary>
        [FieldOffset(28)]
        public int dwYUVZBitMask;
    }

    /// <summary>
    /// From CAUUID
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [PublicAPI]
    public struct DsCAUUID
    {
        /// <summary>
        /// 
        /// </summary>
        public int cElems;

        /// <summary>
        /// 
        /// </summary>
        public IntPtr pElems;

        /// <summary>
        /// Perform a manual marshaling of pElems to retrieve an array of System.Guid.
        /// Assume this structure has been already filled by the ISpecifyPropertyPages.GetPages() method.
        /// </summary>
        /// <returns>A managed representation of pElems (cElems items)</returns>
        public Guid[] ToGuidArray()
        {
            Guid[] retval = new Guid[this.cElems];

            for (int i = 0; i < this.cElems; i++)
            {
                IntPtr ptr = new IntPtr(this.pElems.ToInt64() + (Marshal.SizeOf(typeof(Guid)) * i));
                retval[i] = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
            }

            return retval;
        }
    }

    /// <summary>
    /// DirectShowLib.DsLong is a wrapper class around a <see cref="System.Int64"/> value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null paramters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [PublicAPI]
    public class DsLong
    {
        private readonly long _value;

        /// <summary>
        /// Constructor
        /// Initialize a new instance of DirectShowLib.DsLong with the Value parameter
        /// </summary>
        /// <param name="value">Value to assign to this new instance</param>
        public DsLong(long value)
        {
            _value = value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsLong Instance.
        /// </summary>
        /// <returns>A string representing this instance</returns>
        public override string ToString()
        {
            return _value.ToString();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsLong and System.Int64 for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="ToInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new DsLong instance
        ///   DsLong dsL = new DsLong(9876543210);
        ///   // Do implicit cast between DsLong and Int64
        ///   long l = dsL;
        ///
        ///   Console.WriteLine(l.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">DirectShowLib.DsLong to be cast</param>
        /// <returns>A casted System.Int64</returns>
        public static implicit operator long(DsLong value)
        {
            return value._value;
        }

        /// <summary>
        /// Define implicit cast between System.Int64 and DirectShowLib.DsLong for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="FromInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new Int64 instance
        ///   long l = 9876543210;
        ///   // Do implicit cast between Int64 and DsLong
        ///   DsLong dsl = l;
        ///
        ///   Console.WriteLine(dsl.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">System.Int64 to be cast</param>
        /// <returns>A casted DirectShowLib.DsLong</returns>
        public static implicit operator DsLong(long value)
        {
            return new DsLong(value);
        }

        /// <summary>
        /// Get the System.Int64 equivalent to this DirectShowLib.DsLong instance.
        /// </summary>
        /// <returns>A System.Int64</returns>
        public long ToInt64()
        {
            return _value;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsLong instance for a given System.Int64
        /// </summary>
        /// <param name="value">The System.Int64 to wrap into a DirectShowLib.DsLong</param>
        /// <returns>A new instance of DirectShowLib.DsLong</returns>
        public static DsLong FromInt64(long value)
        {
            return new DsLong(value);
        }
    }

    /// <summary>
    /// DirectShowLib.DsGuid is a wrapper class around a System.Guid value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null parameters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    [PublicAPI]
    public class DsGuid
    {
        [FieldOffset(0)]
        private Guid _guid;

        public static readonly DsGuid Empty = Guid.Empty;

        /// <summary>
        /// Empty constructor. 
        /// Initialize it with System.Guid.Empty
        /// </summary>
        public DsGuid()
        {
            _guid = Guid.Empty;
        }

        /// <summary>
        /// Constructor.
        /// Initialize this instance with a given System.Guid string representation.
        /// </summary>
        /// <param name="value">A valid System.Guid as string</param>
        public DsGuid(string value)
        {
            _guid = new Guid(value);
        }

        /// <summary>
        /// Constructor.
        /// Initialize this instance with a given System.Guid.
        /// </summary>
        /// <param name="value">A System.Guid value type</param>
        public DsGuid(Guid value)
        {
            _guid = value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsGuid Instance.
        /// </summary>
        /// <returns>A string representing this instance</returns>
        public override string ToString()
        {
            return _guid.ToString();
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsGuid Instance with a specific format.
        /// </summary>
        /// <param name="format"><see cref="Guid.ToString()"/> for a description of the format parameter.</param>
        /// <returns>A string representing this instance according to the format parameter</returns>
        public string ToString(string format)
        {
            return _guid.ToString(format);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsGuid and System.Guid for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="ToGuid"/> for similar functionality.
        /// <code>
        ///   // Define a new DsGuid instance
        ///   DsGuid dsG = new DsGuid("{33D57EBF-7C9D-435e-A15E-D300B52FBD91}");
        ///   // Do implicit cast between DsGuid and Guid
        ///   Guid g = dsG;
        ///
        ///   Console.WriteLine(g.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">DirectShowLib.DsGuid to be cast</param>
        /// <returns>A casted System.Guid</returns>
        public static implicit operator Guid(DsGuid value)
        {
            return value._guid;
        }

        /// <summary>
        /// Define implicit cast between System.Guid and DirectShowLib.DsGuid for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="FromGuid"/> for similar functionality.
        /// <code>
        ///   // Define a new Guid instance
        ///   Guid g = new Guid("{B9364217-366E-45f8-AA2D-B0ED9E7D932D}");
        ///   // Do implicit cast between Guid and DsGuid
        ///   DsGuid dsG = g;
        ///
        ///   Console.WriteLine(dsG.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">System.Guid to be cast</param>
        /// <returns>A casted DirectShowLib.DsGuid</returns>
        public static implicit operator DsGuid(Guid value)
        {
            return new DsGuid(value);
        }

        /// <summary>
        /// Get the System.Guid equivalent to this DirectShowLib.DsGuid instance.
        /// </summary>
        /// <returns>A System.Guid</returns>
        public Guid ToGuid()
        {
            return _guid;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsGuid instance for a given System.Guid
        /// </summary>
        /// <param name="value">The System.Guid to wrap into a DirectShowLib.DsGuid</param>
        /// <returns>A new instance of DirectShowLib.DsGuid</returns>
        public static DsGuid FromGuid(Guid value)
        {
            return new DsGuid(value);
        }
    }

    /// <summary>
    /// DirectShowLib.DsInt is a wrapper class around a <see cref="Int32"/> value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null parameters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [PublicAPI]
    public class DsInt
    {
        private readonly int _value;

        /// <summary>
        /// Constructor
        /// Initialize a new instance of DirectShowLib.DsInt with the Value parameter
        /// </summary>
        /// <param name="value">Value to assign to this new instance</param>
        public DsInt(int value)
        {
            _value = value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsInt Instance.
        /// </summary>
        /// <returns>A string representing this instance</returns>
        public override string ToString()
        {
            return _value.ToString();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsInt and System.Int64 for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="ToInt32"/> for similar functionality.
        /// <code>
        ///   // Define a new DsInt instance
        ///   DsInt dsI = new DsInt(0x12345678);
        ///   // Do implicit cast between DsInt and Int32
        ///   int i = dsI;
        ///
        ///   Console.WriteLine(i.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">DirectShowLib.DsInt to be cast</param>
        /// <returns>A casted System.Int32</returns>
        public static implicit operator int(DsInt value)
        {
            return value._value;
        }

        /// <summary>
        /// Define implicit cast between System.Int32 and DirectShowLib.DsInt for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="FromInt32"/> for similar functionality.
        /// <code>
        ///   // Define a new Int32 instance
        ///   int i = 0x12345678;
        ///   // Do implicit cast between Int64 and DsInt
        ///   DsInt dsI = i;
        ///
        ///   Console.WriteLine(dsI.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">System.Int32 to be cast</param>
        /// <returns>A casted DirectShowLib.DsInt</returns>
        public static implicit operator DsInt(int value)
        {
            return new DsInt(value);
        }

        /// <summary>
        /// Get the System.Int32 equivalent to this DirectShowLib.DsInt instance.
        /// </summary>
        /// <returns>A System.Int32</returns>
        public int ToInt32()
        {
            return _value;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsInt instance for a given System.Int32
        /// </summary>
        /// <param name="value">The System.Int32 to wrap into a DirectShowLib.DsInt</param>
        /// <returns>A new instance of DirectShowLib.DsInt</returns>
        public static DsInt FromInt32(int value)
        {
            return new DsInt(value);
        }
    }

    /// <summary>
    /// DirectShowLib.DsShort is a wrapper class around a <see cref="System.Int16"/> value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null parameters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [PublicAPI]
    public class DsShort
    {
        private readonly short _value;

        /// <summary>
        /// Constructor
        /// Initialize a new instance of DirectShowLib.DsShort with the Value parameter
        /// </summary>
        /// <param name="value">Value to assign to this new instance</param>
        public DsShort(short value)
        {
            _value = value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsShort Instance.
        /// </summary>
        /// <returns>A string representing this instance</returns>
        public override string ToString()
        {
            return _value.ToString();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsShort and System.Int16 for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DsShort.ToInt16"/> for similar functionality.
        /// <code>
        ///   // Define a new DsShort instance
        ///   DsShort dsS = new DsShort(0x1234);
        ///   // Do implicit cast between DsShort and Int16
        ///   short s = dsS;
        ///
        ///   Console.WriteLine(s.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">DirectShowLib.DsShort to be cast</param>
        /// <returns>A casted System.Int16</returns>
        public static implicit operator short(DsShort value)
        {
            return value._value;
        }

        /// <summary>
        /// Define implicit cast between System.Int16 and DirectShowLib.DsShort for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="FromInt16"/> for similar functionality.
        /// <code>
        ///   // Define a new Int16 instance
        ///   short s = 0x1234;
        ///   // Do implicit cast between Int64 and DsShort
        ///   DsShort dsS = s;
        ///
        ///   Console.WriteLine(dsS.ToString());
        /// </code>
        /// </summary>
        /// <param name="value">System.Int16 to be cast</param>
        /// <returns>A casted DirectShowLib.DsShort</returns>
        public static implicit operator DsShort(short value)
        {
            return new DsShort(value);
        }

        /// <summary>
        /// Get the System.Int16 equivalent to this DirectShowLib.DsShort instance.
        /// </summary>
        /// <returns>A System.Int16</returns>
        public short ToInt16()
        {
            return _value;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsShort instance for a given System.Int64
        /// </summary>
        /// <param name="value">The System.Int16 to wrap into a DirectShowLib.DsShort</param>
        /// <returns>A new instance of DirectShowLib.DsShort</returns>
        public static DsShort FromInt16(short value)
        {
            return new DsShort(value);
        }
    }

    /// <summary>
    /// DirectShowLib.DsRect is a managed representation of the Win32 RECT structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [PublicAPI]
    public class DsRect
    {
        /// <summary>
        /// Left coordinate
        /// </summary>
        public int left;

        /// <summary>
        /// Top coordinate
        /// </summary>
        public int top;

        /// <summary>
        /// Right coordinate
        /// </summary>
        public int right;

        /// <summary>
        /// Bottom coordinate
        /// </summary>
        public int bottom;

        /// <summary>
        /// Empty contructor. Initialize all fields to 0
        /// </summary>
        public DsRect()
        {
            left = 0;
            top = 0;
            right = 0;
            bottom = 0;
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with given values.
        /// </summary>
        /// <param name="left">the left value</param>
        /// <param name="top">the top value</param>
        /// <param name="right">the right value</param>
        /// <param name="bottom">the bottom value</param>
        public DsRect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with a given <see cref="System.Drawing.Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">A <see cref="System.Drawing.Rectangle"/></param>
        /// <remarks>
        /// Warning, DsRect define a rectangle by defining two of his corners and <see cref="System.Drawing.Rectangle"/> define a rectangle with his upper/left corner, his width and his height.
        /// </remarks>
        public DsRect(Rectangle rectangle)
        {
            left = rectangle.Left;
            top = rectangle.Top;
            right = rectangle.Right;
            bottom = rectangle.Bottom;
        }

        /// <summary>
        /// Provide de string representation of this DsRect instance
        /// </summary>
        /// <returns>A string formated like this : [left, top - right, bottom]</returns>
        public override string ToString()
        {
            return string.Format("[{0}, {1} - {2}, {3}]", left, top, right, bottom);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return left.GetHashCode() | top.GetHashCode() | right.GetHashCode()
                   | bottom.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var cmpRect = obj as DsRect;
            if (cmpRect != null)
            {
                return right == cmpRect.right && bottom == cmpRect.bottom && left == cmpRect.left && top == cmpRect.top;
            }

            if (obj is Rectangle)
            {
                Rectangle cmp = (Rectangle)obj;

                return right == cmp.Right && bottom == cmp.Bottom && left == cmp.Left && top == cmp.Top;
            }

            return false;
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsRect and System.Drawing.Rectangle for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="ToRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new Rectangle instance
        ///   Rectangle r = new Rectangle(0, 0, 100, 100);
        ///   // Do implicit cast between Rectangle and DsRect
        ///   DsRect dsR = r;
        ///
        ///   Console.WriteLine(dsR.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">a DsRect to be cast</param>
        /// <returns>A casted System.Drawing.Rectangle</returns>
        public static implicit operator Rectangle(DsRect r)
        {
            return r.ToRectangle();
        }

        /// <summary>
        /// Define implicit cast between System.Drawing.Rectangle and DirectShowLib.DsRect for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="FromRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new DsRect instance
        ///   DsRect dsR = new DsRect(0, 0, 100, 100);
        ///   // Do implicit cast between DsRect and Rectangle
        ///   Rectangle r = dsR;
        ///
        ///   Console.WriteLine(r.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">A System.Drawing.Rectangle to be cast</param>
        /// <returns>A casted DsRect</returns>
        public static implicit operator DsRect(Rectangle r)
        {
            return new DsRect(r);
        }

        /// <summary>
        /// Get the System.Drawing.Rectangle equivalent to this DirectShowLib.DsRect instance.
        /// </summary>
        /// <returns>A System.Drawing.Rectangle</returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Get a new DirectShowLib.DsRect instance for a given <see cref="System.Drawing.Rectangle"/>
        /// </summary>
        /// <param name="r">The <see cref="System.Drawing.Rectangle"/> used to initialize this new DirectShowLib.DsGuid</param>
        /// <returns>A new instance of DirectShowLib.DsGuid</returns>
        public static DsRect FromRectangle(Rectangle r)
        {
            return new DsRect(r);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    [PublicAPI]
    public struct NormalizedRect
    {
        /// <summary>
        /// Left coordinate
        /// </summary>
        public float left;

        /// <summary>
        /// Top coordinate
        /// </summary>
        public float top;

        /// <summary>
        /// Right coordinate
        /// </summary>
        public float right;

        /// <summary>
        /// Bottom coordinate
        /// </summary>
        public float bottom;

        /// <summary>
        /// Initializes new instance of the <see cref="NormalizedRect"/> structure.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="t"></param>
        /// <param name="r"></param>
        /// <param name="b"></param>
        public NormalizedRect(float l, float t, float r, float b)
        {
            left = l;
            top = t;
            right = r;
            bottom = b;
        }

        public NormalizedRect(RectangleF r)
        {
            left = r.Left;
            top = r.Top;
            right = r.Right;
            bottom = r.Bottom;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("[{0}, {1} - {2}, {3}]", left, top, right, bottom);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return left.GetHashCode() | top.GetHashCode() | right.GetHashCode()
                   | bottom.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public static implicit operator RectangleF(NormalizedRect r)
        {
            return r.ToRectangleF();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public static implicit operator NormalizedRect(Rectangle r)
        {
            return new NormalizedRect(r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator ==(NormalizedRect r1, NormalizedRect r2)
        {
            return ((r1.left == r2.left) && (r1.top == r2.top) && (r1.right == r2.right) && (r1.bottom == r2.bottom));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator !=(NormalizedRect r1, NormalizedRect r2)
        {
            return ((r1.left != r2.left) || (r1.top != r2.top) || (r1.right != r2.right) || (r1.bottom != r2.bottom));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is NormalizedRect)) return false;

            NormalizedRect other = (NormalizedRect)obj;
            return (this == other);
        }

        /// <summary>
        /// Converts to <see cref="RectangleF"/> class
        /// </summary>
        /// <returns></returns>
        public RectangleF ToRectangleF()
        {
            return new RectangleF(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Converts from <see cref="RectangleF"/> class
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static NormalizedRect FromRectangle(RectangleF r)
        {
            return new NormalizedRect(r);
        }
    }

    #endregion

    #region Utility Classes

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public static class DsResults
    {
        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidMediaType = unchecked((int)0x80040200);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidSubType = unchecked((int)0x80040201);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NeedOwner = unchecked((int)0x80040202);

        /// <summary>
        /// 
        /// </summary>
        public const int E_EnumOutOfSync = unchecked((int)0x80040203);

        /// <summary>
        /// 
        /// </summary>
        public const int E_AlreadyConnected = unchecked((int)0x80040204);

        /// <summary>
        /// 
        /// </summary>
        public const int E_FilterActive = unchecked((int)0x80040205);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoTypes = unchecked((int)0x80040206);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoAcceptableTypes = unchecked((int)0x80040207);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidDirection = unchecked((int)0x80040208);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotConnected = unchecked((int)0x80040209);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoAllocator = unchecked((int)0x8004020A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_RunTimeError = unchecked((int)0x8004020B);

        /// <summary>
        /// 
        /// </summary>
        public const int E_BufferNotSet = unchecked((int)0x8004020C);

        /// <summary>
        /// 
        /// </summary>
        public const int E_BufferOverflow = unchecked((int)0x8004020D);

        /// <summary>
        /// 
        /// </summary>
        public const int E_BadAlign = unchecked((int)0x8004020E);

        /// <summary>
        /// 
        /// </summary>
        public const int E_AlreadyCommitted = unchecked((int)0x8004020F);

        /// <summary>
        /// 
        /// </summary>
        public const int E_BuffersOutstanding = unchecked((int)0x80040210);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotCommitted = unchecked((int)0x80040211);

        /// <summary>
        /// 
        /// </summary>
        public const int E_SizeNotSet = unchecked((int)0x80040212);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoClock = unchecked((int)0x80040213);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoSink = unchecked((int)0x80040214);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoInterface = unchecked((int)0x80040215);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotFound = unchecked((int)0x80040216);

        /// <summary>
        /// 
        /// </summary>
        public const int E_CannotConnect = unchecked((int)0x80040217);

        /// <summary>
        /// 
        /// </summary>
        public const int E_CannotRender = unchecked((int)0x80040218);

        /// <summary>
        /// 
        /// </summary>
        public const int E_ChangingFormat = unchecked((int)0x80040219);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoColorKeySet = unchecked((int)0x8004021A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotOverlayConnection = unchecked((int)0x8004021B);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotSampleConnection = unchecked((int)0x8004021C);

        /// <summary>
        /// 
        /// </summary>
        public const int E_PaletteSet = unchecked((int)0x8004021D);

        /// <summary>
        /// 
        /// </summary>
        public const int E_ColorKeySet = unchecked((int)0x8004021E);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoColorKeyFound = unchecked((int)0x8004021F);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoPaletteAvailable = unchecked((int)0x80040220);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoDisplayPalette = unchecked((int)0x80040221);

        /// <summary>
        /// 
        /// </summary>
        public const int E_TooManyColors = unchecked((int)0x80040222);

        /// <summary>
        /// 
        /// </summary>
        public const int E_StateChanged = unchecked((int)0x80040223);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotStopped = unchecked((int)0x80040224);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotPaused = unchecked((int)0x80040225);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotRunning = unchecked((int)0x80040226);

        /// <summary>
        /// 
        /// </summary>
        public const int E_WrongState = unchecked((int)0x80040227);

        /// <summary>
        /// 
        /// </summary>
        public const int E_StartTimeAfterEnd = unchecked((int)0x80040228);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidRect = unchecked((int)0x80040229);

        /// <summary>
        /// 
        /// </summary>
        public const int E_TypeNotAccepted = unchecked((int)0x8004022A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_SampleRejected = unchecked((int)0x8004022B);

        /// <summary>
        /// 
        /// </summary>
        public const int E_SampleRejectedEOS = unchecked((int)0x8004022C);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DuplicateName = unchecked((int)0x8004022D);

        /// <summary>
        /// 
        /// </summary>
        public const int S_DuplicateName = unchecked((int)0x0004022D);

        /// <summary>
        /// 
        /// </summary>
        public const int E_Timeout = unchecked((int)0x8004022E);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidFileFormat = unchecked((int)0x8004022F);

        /// <summary>
        /// 
        /// </summary>
        public const int E_EnumOutOfRange = unchecked((int)0x80040230);

        /// <summary>
        /// 
        /// </summary>
        public const int E_CircularGraph = unchecked((int)0x80040231);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotAllowedToSave = unchecked((int)0x80040232);

        /// <summary>
        /// 
        /// </summary>
        public const int E_TimeAlreadyPassed = unchecked((int)0x80040233);

        /// <summary>
        /// 
        /// </summary>
        public const int E_AlreadyCancelled = unchecked((int)0x80040234);

        /// <summary>
        /// 
        /// </summary>
        public const int E_CorruptGraphFile = unchecked((int)0x80040235);

        /// <summary>
        /// 
        /// </summary>
        public const int E_AdviseAlreadySet = unchecked((int)0x80040236);

        /// <summary>
        /// 
        /// </summary>
        public const int S_StateIntermediate = unchecked((int)0x00040237);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoModexAvailable = unchecked((int)0x80040238);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoAdviseSet = unchecked((int)0x80040239);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoFullScreen = unchecked((int)0x8004023A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InFullScreenMode = unchecked((int)0x8004023B);

        /// <summary>
        /// 
        /// </summary>
        public const int E_UnknownFileType = unchecked((int)0x80040240);

        /// <summary>
        /// 
        /// </summary>
        public const int E_CannotLoadSourceFilter = unchecked((int)0x80040241);

        /// <summary>
        /// 
        /// </summary>
        public const int S_PartialRender = unchecked((int)0x00040242);

        /// <summary>
        /// 
        /// </summary>
        public const int E_FileTooShort = unchecked((int)0x80040243);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidFileVersion = unchecked((int)0x80040244);

        /// <summary>
        /// 
        /// </summary>
        public const int S_SomeDataIgnored = unchecked((int)0x00040245);

        /// <summary>
        /// 
        /// </summary>
        public const int S_ConnectionsDeferred = unchecked((int)0x00040246);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidCLSID = unchecked((int)0x80040247);

        /// <summary>
        /// 
        /// </summary>
        public const int E_InvalidMediaType2 = unchecked((int)0x80040248);

        /// <summary>
        /// 
        /// </summary>
        public const int E_BabKey = unchecked((int)0x800403F2);

        /// <summary>
        /// 
        /// </summary>
        public const int S_NoMoreItems = unchecked((int)0x00040103);

        /// <summary>
        /// 
        /// </summary>
        public const int E_SampleTimeNotSet = unchecked((int)0x80040249);

        /// <summary>
        /// 
        /// </summary>
        public const int S_ResourceNotNeeded = unchecked((int)0x00040250);

        /// <summary>
        /// 
        /// </summary>
        public const int E_MediaTimeNotSet = unchecked((int)0x80040251);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoTimeFormatSet = unchecked((int)0x80040252);

        /// <summary>
        /// 
        /// </summary>
        public const int E_MonoAudioHW = unchecked((int)0x80040253);

        /// <summary>
        /// 
        /// </summary>
        public const int S_MediaTypeIgnored = unchecked((int)0x00040254);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoDecompressor = unchecked((int)0x80040255);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoAudioHardware = unchecked((int)0x80040256);

        /// <summary>
        /// 
        /// </summary>
        public const int S_VideoNotRendered = unchecked((int)0x00040257);

        /// <summary>
        /// 
        /// </summary>
        public const int S_AudioNotRendered = unchecked((int)0x00040258);

        /// <summary>
        /// 
        /// </summary>
        public const int E_RPZA = unchecked((int)0x80040259);

        /// <summary>
        /// 
        /// </summary>
        public const int S_RPZA = unchecked((int)0x0004025A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_ProcessorNotSuitable = unchecked((int)0x8004025B);

        /// <summary>
        /// 
        /// </summary>
        public const int E_UnsupportedAudio = unchecked((int)0x8004025C);

        /// <summary>
        /// 
        /// </summary>
        public const int E_UnsupportedVideo = unchecked((int)0x8004025D);

        /// <summary>
        /// 
        /// </summary>
        public const int E_MPEGNotConstrained = unchecked((int)0x8004025E);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NotInGraph = unchecked((int)0x8004025F);

        /// <summary>
        /// 
        /// </summary>
        public const int S_Estimated = unchecked((int)0x00040260);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoTimeFormat = unchecked((int)0x80040261);

        /// <summary>
        /// 
        /// </summary>
        public const int E_ReadOnly = unchecked((int)0x80040262);

        /// <summary>
        /// 
        /// </summary>
        public const int S_Reserved = unchecked((int)0x00040263);

        /// <summary>
        /// 
        /// </summary>
        public const int E_BufferUnderflow = unchecked((int)0x80040264);

        /// <summary>
        /// 
        /// </summary>
        public const int E_UnsupportedStream = unchecked((int)0x80040265);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoTransport = unchecked((int)0x80040266);

        /// <summary>
        /// 
        /// </summary>
        public const int S_StreamOff = unchecked((int)0x00040267);

        /// <summary>
        /// 
        /// </summary>
        public const int S_CantCue = unchecked((int)0x00040268);

        /// <summary>
        /// 
        /// </summary>
        public const int E_BadVideoCD = unchecked((int)0x80040269);

        /// <summary>
        /// 
        /// </summary>
        public const int S_NoStopTime = unchecked((int)0x00040270);

        /// <summary>
        /// 
        /// </summary>
        public const int E_OutOfVideoMemory = unchecked((int)0x80040271);

        /// <summary>
        /// 
        /// </summary>
        public const int E_VPNegotiationFailed = unchecked((int)0x80040272);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DDrawCapsNotSuitable = unchecked((int)0x80040273);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoVPHardware = unchecked((int)0x80040274);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoCaptureHardware = unchecked((int)0x80040275);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDOperationInhibited = unchecked((int)0x80040276);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDInvalidDomain = unchecked((int)0x80040277);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDNoButton = unchecked((int)0x80040278);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDGraphNotReady = unchecked((int)0x80040279);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDRenderFail = unchecked((int)0x8004027A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDDecNotEnough = unchecked((int)0x8004027B);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DDrawVersionNotSuitable = unchecked((int)0x8004027C);

        /// <summary>
        /// 
        /// </summary>
        public const int E_CopyProtFailed = unchecked((int)0x8004027D);

        /// <summary>
        /// 
        /// </summary>
        public const int S_NoPreviewPin = unchecked((int)0x0004027E);

        /// <summary>
        /// 
        /// </summary>
        public const int E_TimeExpired = unchecked((int)0x8004027F);

        /// <summary>
        /// 
        /// </summary>
        public const int S_DVDNonOneSequential = unchecked((int)0x00040280);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDWrongSpeed = unchecked((int)0x80040281);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDMenuDoesNotExist = unchecked((int)0x80040282);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDCmdCancelled = unchecked((int)0x80040283);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDStateWrongVersion = unchecked((int)0x80040284);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDStateCorrupt = unchecked((int)0x80040285);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDStateWrongDisc = unchecked((int)0x80040286);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDIncompatibleRegion = unchecked((int)0x80040287);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDNoAttributes = unchecked((int)0x80040288);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDNoGoupPGC = unchecked((int)0x80040289);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDLowParentalLevel = unchecked((int)0x8004028A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDNotInKaraokeMode = unchecked((int)0x8004028B);

        /// <summary>
        /// 
        /// </summary>
        public const int S_DVDChannelContentsNotAvailable = unchecked((int)0x0004028C);

        /// <summary>
        /// 
        /// </summary>
        public const int S_DVDNotAccurate = unchecked((int)0x0004028D);

        /// <summary>
        /// 
        /// </summary>
        public const int E_FrameStepUnsupported = unchecked((int)0x8004028E);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDStreamDisabled = unchecked((int)0x8004028F);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDTitleUnknown = unchecked((int)0x80040290);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDInvalidDisc = unchecked((int)0x80040291);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDNoResumeInformation = unchecked((int)0x80040292);

        /// <summary>
        /// 
        /// </summary>
        public const int E_PinAlreadyBlockedOnThisThread = unchecked((int)0x80040293);

        /// <summary>
        /// 
        /// </summary>
        public const int E_PinAlreadyBlocked = unchecked((int)0x80040294);

        /// <summary>
        /// 
        /// </summary>
        public const int E_CertificationFailure = unchecked((int)0x80040295);

        /// <summary>
        /// 
        /// </summary>
        public const int E_VMRNotInMixerMode = unchecked((int)0x80040296);

        /// <summary>
        /// 
        /// </summary>
        public const int E_VMRNoApSupplied = unchecked((int)0x80040297);

        /// <summary>
        /// 
        /// </summary>
        public const int E_VMRNoDeinterlace_HW = unchecked((int)0x80040298);

        /// <summary>
        /// 
        /// </summary>
        public const int E_VMRNoProcAMPHW = unchecked((int)0x80040299);

        /// <summary>
        /// 
        /// </summary>
        public const int E_DVDVMR9IncompatibleDec = unchecked((int)0x8004029A);

        /// <summary>
        /// 
        /// </summary>
        public const int E_NoCOPPHW = unchecked((int)0x8004029B);
    }

    /// <summary>
    /// From VideoCopyProtectionType
    /// </summary>
    public enum VideoCopyProtectionType
    {
        MacrovisionBasic,
        MacrovisionCBI
    }

    /// <summary>
    /// From _AM_PUSHSOURCE_FLAGS
    /// </summary>
    [Flags]
    public enum AMPushSourceFlags
    {
        None = 0,
        InternalRM = 0x00000001,
        NotLive = 0x00000002,
        PrivateClock = 0x00000004,
        UseStreamClock = 0x00010000,
        UseClockChain = 0x00020000,
    }

    /// <summary>
    /// From _DVResolution
    /// </summary>
    public enum DVResolution
    {
        Full = 1000,
        Half = 1001,
        Quarter = 1002,
        Dc = 1003
    }

    /// <summary>
    /// From VIDEOENCODER_BITRATE_MODE
    /// </summary>
    public enum VideoEncoderBitrateMode
    {
        ConstantBitRate = 0,
        VariableBitRateAverage,
        VariableBitRatePeak
    }

    /// <summary>
    /// From unnamed enum (REG_PINFLAG_B_*)
    /// </summary>
    [Flags]
    public enum RegPinFlag
    {
        None = 0,
        Zero = 0x1,
        Renderer = 0x2,
        Many = 0x4,
        Output = 0x8
    }

    /// <summary>
    /// From unnamed enum (ADVISE_*)
    /// </summary>
    [Flags]
    public enum Advise
    {
        None = 0x0,
        Clipping = 0x1,
        Palette = 0x2,
        ColorKey = 0x4,
        Position = 0x8,
        DisplayChange = 0x10,
        All = Clipping | Palette | ColorKey | Position,
        All2 = All | DisplayChange
    }

    // ------------------------------------------------------------------------

    /// <summary>
    /// From REGFILTER
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RegFilter
    {
        public Guid Clsid;
        [MarshalAs(UnmanagedType.LPWStr)] public string Name;
    }

    /// <summary>
    /// From REGPINTYPES
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RegPinTypes
    {
        public Guid clsMajorType;
        public Guid clsMinorType;
    }

    /// <summary>
    /// From REGFILTERPINS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RegFilterPins
    {
        [MarshalAs(UnmanagedType.LPWStr)] public string strName;
        [MarshalAs(UnmanagedType.Bool)] public bool bRendered;
        [MarshalAs(UnmanagedType.Bool)] public bool bOutput;
        [MarshalAs(UnmanagedType.Bool)] public bool bZero;
        [MarshalAs(UnmanagedType.Bool)] public bool bMany;
        public Guid clsConnectsToFilter;
        [MarshalAs(UnmanagedType.LPWStr)] public string strConnectsToPin;
        public int nMediaTypes;
        [MarshalAs(UnmanagedType.ByValArray)] public RegPinTypes[] lpMediaType;
    }

    /// <summary>
    /// From REGFILTERPINS2
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RegFilterPins2
    {
        public RegPinFlag dwFlags;
        public int cInstances;
        public int nMediaTypes;
        public RegPinTypes[] lpMediaType;
        public int nMediums;
        public RegPinMedium[] lpMedium;
        public Guid clsPinCategory;
    }

    /// <summary>
    /// From REGFILTER2
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct RegFilter2Union
    {
        [FieldOffset(0)] public RegFilterPins[] rgPins;
        [FieldOffset(0)] public RegFilterPins2[] rgPins2;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct RegFilter2
    {
        public int dwVersion;
        public int dwMerit;
        public int cPins;
        public RegFilter2Union rgPins;
    }

    /// <summary>
    /// From RGNDATAHEADER
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RgnDataHeader
    {
        public int dwSize;
        public int iType;
        public int nCount;
        public int nRgnSize;
        public Rectangle rcBound;
    }

    /// <summary>
    /// From RGNDATA
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RgnData
    {
        public RgnDataHeader rdh;
        public IntPtr Buffer;
    }

    /// <summary>
    /// From TIMECODE
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TimeCode
    {
        public short wFrameRate;
        public short wFrameFract;
        public int dwFrames;
    }

    /// <summary>
    /// From TIMECODE_SAMPLE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TimeCodeSample
    {
        public long qwTick;
        public TimeCode timecode;
        public int dwUser;
        public int dwFlags;
    }

    /// <summary>
    /// From STREAM_ID_MAP
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct StreamIdMap
    {
        public int stream_id;
        public int dwMediaSampleContent;
        public int ulSubstreamFilterValue;
        public int iDataOffset;
    }

    /// <summary>
    /// From CodecAPIEventData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CodecAPIEventData
    {
        public Guid guid;
        public int dataLength;
        public int reserved1;
        public int reserved2;
        public int reserved3;
    }

    /// <summary>
    /// From AMCOPPSignature
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AMCOPPSignature
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 256)] public byte[] Signature;
    }

    /// <summary>
    /// From AMCOPPCommand
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AMCOPPCommand
    {
        public Guid macKDI;
        public Guid guidCommandID;
        public int dwSequence;
        public int cbSizeData;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 4056)] public byte[] CommandData;
    }

    /// <summary>
    /// From AMCOPPStatusInput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AMCOPPStatusInput
    {
        public Guid rApp;
        public Guid guidStatusRequestID;
        public int dwSequence;
        public int cbSizeData;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 4056)] public byte[] StatusData;
    }

    /// <summary>
    /// From AMCOPPStatusOutput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AMCOPPStatusOutput
    {
        public Guid macKDI;
        public int cbSizeData;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 4076)] public byte[] COPPStatus;
    }

    /// <summary>
    /// From _AM_FILTER_MISC_FLAGS
    /// </summary>
    [Flags]
    public enum AMFilterMiscFlags
    {
        None = 0,
        IsRenderer = 0x00000001,
        IsSource = 0x00000002
    }

    /// <summary>
    /// From AM_STREAM_INFO_FLAGS
    /// </summary>
    [Flags]
    public enum AMStreamInfoFlags
    {
        None = 0x00000000,
        StartDefined = 0x00000001,
        StopDefined = 0x00000002,
        Discarding = 0x00000004,
        StopSendExtra = 0x00000010
    }

    /// <summary>
    /// From MPEG2_PROGRAM_* defines
    /// </summary>
    public enum MPEG2Program
    {
        StreamMap = 0x00000000,
        ElementaryStream = 0x00000001,
        DirecoryPesPacket = 0x00000002,
        PackHeader = 0x00000003,
        PesSteam = 0x00000004,
        SystemHeader = 0x00000005,
    }

    /// <summary>
    /// From _AM_AUDIO_RENDERER_STAT_PARAM
    /// </summary>
    public enum AMAudioRendererStatParam
    {
        BreakCount = 1,
        SlaveMode,
        SilenceDur,
        LastBufferDur,
        Discontinuities,
        SlaveRate,
        SlaveDropWriteDur,
        SlaveHighLowError,
        SlaveLastHighLowError,
        SlaveAccumError,
        BufferFullness,
        Jitter
    }

    /// <summary>
    /// From AM_STREAM_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AMStreamInfo
    {
        public long tStart;
        public long tStop;
        public int dwStartCookie;
        public int dwStopCookie;
        public AMStreamInfoFlags dwFlags;
    }

    /// <summary>
    /// From _DVDECODERRESOLUTION
    /// </summary>
    public enum DVDecoderResolution
    {
        r720x480 = 1000,
        r360x240 = 1001,
        r180x120 = 1002,
        r88x60 = 1003
    }

    /// <summary>
    /// From _AM_INTF_SEARCH_FLAGS
    /// </summary>
    [Flags]
    public enum AMIntfSearchFlags
    {
        None = 0x00000000,
        InputPin = 0x00000001,
        OutputPin = 0x00000002,
        Filter = 0x00000004
    }

    /// <summary>
    /// From AM_QUERY_DECODER_* defines
    /// </summary>
    public enum AMQueryDecoder
    {
        VMRSupport = 0x00000001,
        DXVA_1Support = 0x00000002,
        DVDSupport = 0x00000003,
        ATSC_SDSupport = 0x00000004,
        ATSC_HDSupport = 0x00000005,
        VMR9Support = 0x00000006
    }

    /// <summary>
    /// From DECODER_CAP_* defines
    /// </summary>
    public enum DecoderCap
    {
        NotSupported = 0x00000000,
        Supported = 0x00000001
    }

    /// <summary>
    /// From DECIMATION_USAGE
    /// </summary>
    public enum DecimationUsage
    {
        Legacy,
        UseDecoderOnly,
        UseVideoPortOnly,
        UseOverlayOnly,
        Default
    }

    /// <summary>
    /// From AMOVERLAYFX
    /// </summary>
    [Flags]
    public enum AMOverlayFX
    {
        NoFX = 0x00000000,
        MirrorLeftRight = 0x00000002,
        MirrorUpDown = 0x00000004,
        Deinterlace = 0x00000008
    }

    /// <summary>
    /// From _AMRESCTL_RESERVEFLAGS
    /// </summary>
    [Flags]
    public enum AMResCtlReserveFlags
    {
        Reserve = 0x00,
        UnReserve = 0x01
    }

    /// <summary>
    /// From _AMSTREAMSELECTINFOFLAGS
    /// </summary>
    [Flags]
    public enum AMStreamSelectInfoFlags
    {
        Disabled = 0x0,
        Enabled = 0x01,
        Exclusive = 0x02
    }

    /// <summary>
    /// From _AMSTREAMSELECTENABLEFLAGS
    /// </summary>
    [Flags]
    public enum AMStreamSelectEnableFlags
    {
        DisableAll = 0x0,
        Enable = 0x01,
        EnableAll = 0x02
    }

    /// <summary>
    /// unnamed enum
    /// </summary>
    [Flags]
    public enum Merit
    {
        None = 0,
        Preferred = 0x800000,
        Normal = 0x600000,
        Unlikely = 0x400000,
        DoNotUse = 0x200000,
        SWCompressor = 0x100000,
        HWCompressor = 0x100050
    }

    /// <summary>
    /// From COLORKEY
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class ColorKey
    {
        public int KeyType;
        public int PaletteIndex;
        public int LowColorValue;
        public int HighColorValue;
    }

    /// <summary>
    /// From REGPINMEDIUM
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class RegPinMedium
    {
        public Guid clsMedium;
        public int dw1;
        public int dw2;
    }

    /// <summary>
    /// From DVINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DVInfo
    {
        public int dwDVAAuxSrc;
        public int dwDVAAuxCtl;
        public int dwDVAAuxSrc1;
        public int dwDVAAuxCtl1;
        public int dwDVVAuxSrc;
        public int dwDVVAuxCtl;
        public int dwDVReserved1;
        public int dwDVReserved2;
    }

    /// <summary>
    /// From _DVENCODERRESOLUTION
    /// </summary>
    public enum DVEncoderResolution
    {
        r720x480 = 2012,
        r360x240 = 2013,
        r180x120 = 2014,
        r88x60 = 2015
    }

    /// <summary>
    /// From _DVENCODERFORMAT
    /// </summary>
    public enum DVEncoderFormat
    {
        DVSD = 2007,
        DVHD = 2008,
        DVSL = 2009
    }

    /// <summary>
    /// From _DVENCODERVIDEOFORMAT
    /// </summary>
    public enum DVEncoderVideoFormat
    {
        NTSC = 2000,
        PAL = 2001
    }

    /// <summary>
    /// From _AM_RENSDEREXFLAGS
    /// </summary>
    [Flags]
    public enum AMRenderExFlags
    {
        None = 0,
        RenderToExistingRenderers = 1
    }

    /// <summary>
    /// From InterleavingMode
    /// </summary>
    public enum InterleavingMode
    {
        None,
        Capture,
        Full,
        NoneBuffered
    }

    /// <summary>
    /// From AM_FILESINK_FLAGS
    /// </summary>
    [Flags]
    public enum AMFileSinkFlags
    {
        None = 0,
        OverWrite = 0x00000001
    }

    /// <summary>
    /// From KSPROPERTY_SUPPORT_* defines
    /// </summary>
    public enum KSPropertySupport
    {
        Get = 1,
        Set = 2
    }

    /// <summary>
    /// From AMPROPERTY_PIN
    /// </summary>
    public enum AMPropertyPin
    {
        Category,
        Medium
    }

    /// <summary>
    /// From AMTunerSubChannel
    /// </summary>
    public enum AMTunerSubChannel
    {
        NoTune = -2,
        Default = -1
    }

    /// <summary>
    /// From AMTunerSignalStrength
    /// </summary>
    public enum AMTunerSignalStrength
    {
        HasNoSignalStrength = -1,
        NoSignal = 0,
        SignalPresent = 1
    }

    /// <summary>
    /// From AMTunerModeType
    /// </summary>
    [Flags]
    public enum AMTunerModeType
    {
        Default = 0x0000,
        TV = 0x0001,
        FMRadio = 0x0002,
        AMRadio = 0x0004,
        Dss = 0x0008,
        DTV = 0x0010
    }

    /// <summary>
    /// From AMTunerEventType
    /// </summary>
    public enum AMTunerEventType
    {
        Changed = 0x0001
    }

    /// <summary>
    /// From AnalogVideoStandard
    /// </summary>
    [Flags]
    public enum AnalogVideoStandard
    {
        None = 0x00000000,
        NTSC_M = 0x00000001,
        NTSC_M_J = 0x00000002,
        NTSC_433 = 0x00000004,
        PAL_B = 0x00000010,
        PAL_D = 0x00000020,
        PAL_G = 0x00000040,
        PAL_H = 0x00000080,
        PAL_I = 0x00000100,
        PAL_M = 0x00000200,
        PAL_N = 0x00000400,
        PAL_60 = 0x00000800,
        SECAM_B = 0x00001000,
        SECAM_D = 0x00002000,
        SECAM_G = 0x00004000,
        SECAM_H = 0x00008000,
        SECAM_K = 0x00010000,
        SECAM_K1 = 0x00020000,
        SECAM_L = 0x00040000,
        SECAM_L1 = 0x00080000,
        PAL_N_COMBO = 0x00100000,

        NTSCMask = 0x00000007,
        PALMask = 0x00100FF0,
        SECAMMask = 0x000FF000
    }

    /// <summary>
    /// From TunerInputType
    /// </summary>
    public enum TunerInputType
    {
        Cable,
        Antenna
    }

    /// <summary>
    /// From VideoControlFlags
    /// </summary>
    [Flags]
    public enum VideoControlFlags
    {
        None = 0x0,
        FlipHorizontal = 0x0001,
        FlipVertical = 0x0002,
        ExternalTriggerEnable = 0x0004,
        Trigger = 0x0008
    }

    /// <summary>
    /// From TVAudioMode
    /// </summary>
    [Flags]
    public enum TVAudioMode
    {
        None = 0,
        Mono = 0x0001,
        Stereo = 0x0002,
        LangA = 0x0010,
        LangB = 0x0020,
        LangC = 0x0040,
    }

    /// <summary>
    /// From VideoProcAmpProperty
    /// </summary>
    public enum VideoProcAmpProperty
    {
        Brightness,
        Contrast,
        Hue,
        Saturation,
        Sharpness,
        Gamma,
        ColorEnable,
        WhiteBalance,
        BacklightCompensation,
        Gain
    }

    /// <summary>
    /// From VideoProcAmpFlags
    /// </summary>
    [Flags]
    public enum VideoProcAmpFlags
    {
        None = 0,
        Auto = 0x0001,
        Manual = 0x0002
    }

    /// <summary>
    /// From PhysicalConnectorType
    /// </summary>
    public enum PhysicalConnectorType
    {
        Video_Tuner = 1,
        Video_Composite,
        Video_SVideo,
        Video_RGB,
        Video_YRYBY,
        Video_SerialDigital,
        Video_ParallelDigital,
        Video_SCSI,
        Video_AUX,
        Video_1394,
        Video_USB,
        Video_VideoDecoder,
        Video_VideoEncoder,
        Video_SCART,
        Video_Black,

        Audio_Tuner = 0x1000,
        Audio_Line,
        Audio_Mic,
        Audio_AESDigital,
        Audio_SPDIFDigital,
        Audio_SCSI,
        Audio_AUX,
        Audio_1394,
        Audio_USB,
        Audio_AudioDecoder,
    }

    /// <summary>
    /// From AMTVAudioEventType
    /// </summary>
    [Flags]
    public enum AMTVAudioEventType
    {
        None = 0,
        Changed = 0x0001
    }

    /// <summary>
    /// From CompressionCaps
    /// </summary>
    [Flags]
    public enum CompressionCaps
    {
        None = 0x0,
        CanQuality = 0x01,
        CanCrunch = 0x02,
        CanKeyFrame = 0x04,
        CanBFrame = 0x08,
        CanWindow = 0x10
    }

    /// <summary>
    /// From VfwCompressDialogs
    /// </summary>
    [Flags]
    public enum VfwCompressDialogs
    {
        None = 0,
        Config = 0x01,
        About = 0x02,
        QueryConfig = 0x04,
        QueryAbout = 0x08
    }

    /// <summary>
    /// From VfwCaptureDialogs
    /// </summary>
    [Flags]
    public enum VfwCaptureDialogs
    {
        None = 0x00,
        Source = 0x01,
        Format = 0x02,
        Display = 0x04
    }

    /// <summary>
    /// From DEV_PORT_*
    /// </summary>
    public enum ExtDevicePort
    {
        Sim = 1,
        Com1 = 2,
        Com2 = 3,
        Com3 = 4,
        Com4 = 5,
        Diaq = 6,
        Arti = 7,
        FireWire1394 = 8,
        Usb = 9,
        Min = Sim,
        Max = Usb
    }

    public enum ExtDeviceBase
    {
        Base = 0x1000
    }

    /// <summary>
    /// From ED_DEVCAP*, ED_DEVTYPE* etc.
    /// </summary>
    public enum ExtDeviceCaps
    {
        None = 0,
        CanRecord = ExtDeviceBase.Base + 1,
        CanRecordStrobe = ExtDeviceBase.Base + 2,
        HasAudio = ExtDeviceBase.Base + 3,
        HasVideo = ExtDeviceBase.Base + 4,
        UsesFiles = ExtDeviceBase.Base + 5,
        CanSave = ExtDeviceBase.Base + 6,
        DeviceType = ExtDeviceBase.Base + 7,
        VCR = ExtDeviceBase.Base + 8,
        LaserDisk = ExtDeviceBase.Base + 9,
        ATR = ExtDeviceBase.Base + 10,
        DDR = ExtDeviceBase.Base + 11,
        Router = ExtDeviceBase.Base + 12,
        Keyer = ExtDeviceBase.Base + 13,
        MixerVideo = ExtDeviceBase.Base + 14,
        DVE = ExtDeviceBase.Base + 15,
        WipeGen = ExtDeviceBase.Base + 16,
        MixerAudio = ExtDeviceBase.Base + 17,
        CG = ExtDeviceBase.Base + 18,
        TBC = ExtDeviceBase.Base + 19,
        TCG = ExtDeviceBase.Base + 20,
        GPI = ExtDeviceBase.Base + 21,
        Joystick = ExtDeviceBase.Base + 22,
        Keyboard = ExtDeviceBase.Base + 3,
        ExternalDeviceID = ExtDeviceBase.Base + 24,
        TimeCodeRead = ExtDeviceBase.Base + 25,
        TimeCodeWrite = ExtDeviceBase.Base + 26,
        CtlTrkRead = ExtDeviceBase.Base + 27,
        IndexRead = ExtDeviceBase.Base + 28,
        PreRoll = ExtDeviceBase.Base + 29,
        PostRoll = ExtDeviceBase.Base + 30,
        SyncAccuracy = ExtDeviceBase.Base + 31,
        Precise = ExtDeviceBase.Base + 32,
        Frame = ExtDeviceBase.Base + 33,
        Rough = ExtDeviceBase.Base + 34,
        NormalRate = ExtDeviceBase.Base + 35,
        Rate24 = ExtDeviceBase.Base + 36,
        Rate25 = ExtDeviceBase.Base + 37,
        Rate2997 = ExtDeviceBase.Base + 38,
        Rate30 = ExtDeviceBase.Base + 39,
        CanPreview = ExtDeviceBase.Base + 40,
        CanMonitorSources = ExtDeviceBase.Base + 41,
        CanTest = ExtDeviceBase.Base + 42,
        VideoInputs = ExtDeviceBase.Base + 43,
        AudioInputs = ExtDeviceBase.Base + 44,
        NeedsCalibrating = ExtDeviceBase.Base + 45,
        SeekType = ExtDeviceBase.Base + 46,
        Perfect = ExtDeviceBase.Base + 47,
        Fast = ExtDeviceBase.Base + 48,
        Slow = ExtDeviceBase.Base + 49,
        On = ExtDeviceBase.Base + 50,
        Off = ExtDeviceBase.Base + 51,
        Standby = ExtDeviceBase.Base + 52,
        All = ExtDeviceBase.Base + 55,
        Test = ExtDeviceBase.Base + 56,
        DeviceTypeCamera = ExtDeviceBase.Base + 900,
        DeviceTypeTuner = ExtDeviceBase.Base + 901,
        DeviceTypeDvhs = ExtDeviceBase.Base + 902,
        DeviceTypeUnknown = ExtDeviceBase.Base + 903,
        CapabilityUnknown = ExtDeviceBase.Base + 910,
    }

    /// <summary>
    /// From ED_TRANSCAP*
    /// </summary>
    public enum ExtTransportCaps
    {
        None = 0,
        CanEject = ExtDeviceBase.Base + 100,
        CanBumpPlay = ExtDeviceBase.Base + 101,
        CanPlayBackwards = ExtDeviceBase.Base + 102,
        CanSetEE = ExtDeviceBase.Base + 103,
        CanSetPB = ExtDeviceBase.Base + 104,
        CanDelayVideoIn = ExtDeviceBase.Base + 105,
        CanDelayVideoOut = ExtDeviceBase.Base + 106,
        CanDelayAudioIn = ExtDeviceBase.Base + 107,
        CanDelayAudioOut = ExtDeviceBase.Base + 108,
        FwdVariableMax = ExtDeviceBase.Base + 109,
        FwdVariableMin = ExtDeviceBase.Base + 800,
        RevVariableMax = ExtDeviceBase.Base + 110,
        RevVariableMin = ExtDeviceBase.Base + 801,
        FwdShuttleMax = ExtDeviceBase.Base + 802,
        FwdShuttleMin = ExtDeviceBase.Base + 803,
        RevShuttleMax = ExtDeviceBase.Base + 804,
        RevShuttleMin = ExtDeviceBase.Base + 805,
        NumAudioTracks = ExtDeviceBase.Base + 111,
        LTCTrack = ExtDeviceBase.Base + 112,
        NeedsTBC = ExtDeviceBase.Base + 113,
        NeedsCueing = ExtDeviceBase.Base + 114,
        CanInsert = ExtDeviceBase.Base + 115,
        CanAssemble = ExtDeviceBase.Base + 116,
        FieldStep = ExtDeviceBase.Base + 117,
        ClockIncRate = ExtDeviceBase.Base + 118,
        CanDetechLength = ExtDeviceBase.Base + 119,
        CanFreeze = ExtDeviceBase.Base + 120,
        HasTuner = ExtDeviceBase.Base + 121,
        HasTimer = ExtDeviceBase.Base + 122,
        HasClock = ExtDeviceBase.Base + 123,
        MultipleEdits = ExtDeviceBase.Base + 806,
        IsMaster = ExtDeviceBase.Base + 807,
        HasDT = ExtDeviceBase.Base + 814
    }

    /// <summary>
    /// From ED_MEDIA*
    /// </summary>
    public enum ExtTransportMediaStates
    {
        None = 0,
        SpinUp = ExtDeviceBase.Base + 130,
        SpinDown = ExtDeviceBase.Base + 131,
        Unload = ExtDeviceBase.Base + 132
    }

    /// <summary>
    /// From ED_MODE*
    /// </summary>
    public enum ExtTransportModes
    {
        None = 0,
        Play = ExtDeviceBase.Base + 200,
        Stop = ExtDeviceBase.Base + 201,
        Freeze = ExtDeviceBase.Base + 202,
        Thaw = ExtDeviceBase.Base + 203,
        FF = ExtDeviceBase.Base + 204,
        Rew = ExtDeviceBase.Base + 205,
        Record = ExtDeviceBase.Base + 206,
        RecordStrobe = ExtDeviceBase.Base + 207,
        RecordFreeze = ExtDeviceBase.Base + 808,
        Step = ExtDeviceBase.Base + 208,
        StepFwd = Step,
        StepRew = ExtDeviceBase.Base + 809,
        Shuttle = ExtDeviceBase.Base + 209,
        EditCue = ExtDeviceBase.Base + 210,
        VarSpeed = ExtDeviceBase.Base + 211,
        Perform = ExtDeviceBase.Base + 212,
        LinkOn = ExtDeviceBase.Base + 280,
        LinkOff = ExtDeviceBase.Base + 281,
        NotifyEnable = ExtDeviceBase.Base + 810,
        NotifyDisable = ExtDeviceBase.Base + 811,
        ShotSearch = ExtDeviceBase.Base + 812,
        PlayFastestFwd = ExtDeviceBase.Base + 933,
        PlaySlowestFwd = ExtDeviceBase.Base + 934,
        PlayFastestRev = ExtDeviceBase.Base + 935,
        PlaySlowestRev = ExtDeviceBase.Base + 936,
        Wind = ExtDeviceBase.Base + 937,
        RewFastest = ExtDeviceBase.Base + 938,
        RevPlay = ExtDeviceBase.Base + 939
    }

    /// <summary>
    /// From ED_MEDIA* etc.
    /// </summary>
    public enum ExtTransportStatus
    {
        None = 0,
        Mode = ExtDeviceBase.Base + 500,
        Error = ExtDeviceBase.Base + 501,
        Local = ExtDeviceBase.Base + 502,
        RecordInhibit = ExtDeviceBase.Base + 503,
        ServoLock = ExtDeviceBase.Base + 504,
        MediaPresent = ExtDeviceBase.Base + 505,
        MediaLength = ExtDeviceBase.Base + 506,
        MediaSize = ExtDeviceBase.Base + 507,
        MediaTrackCount = ExtDeviceBase.Base + 508,
        MediaTrackLength = ExtDeviceBase.Base + 509,
        MediaSide = ExtDeviceBase.Base + 510,
        MediaType = ExtDeviceBase.Base + 511,
        MediaVhs = ExtDeviceBase.Base + 512,
        MediaSvhs = ExtDeviceBase.Base + 513,
        MediaHi8 = ExtDeviceBase.Base + 514,
        MediaUmatic = ExtDeviceBase.Base + 515,
        MediaDvc = ExtDeviceBase.Base + 516,
        Media1Inch = ExtDeviceBase.Base + 517,
        MediaD1 = ExtDeviceBase.Base + 518,
        MediaD2 = ExtDeviceBase.Base + 519,
        MediaD3 = ExtDeviceBase.Base + 520,
        MediaD5 = ExtDeviceBase.Base + 521,
        MediaDBeta = ExtDeviceBase.Base + 522,
        MediaBeta = ExtDeviceBase.Base + 523,
        Media8mm = ExtDeviceBase.Base + 524,
        MediaDdr = ExtDeviceBase.Base + 525,
        MediaSx = ExtDeviceBase.Base + 813,
        MediaOther = ExtDeviceBase.Base + 526,
        MediaClv = ExtDeviceBase.Base + 527,
        MediaCav = ExtDeviceBase.Base + 528,
        MediaPosition = ExtDeviceBase.Base + 529,
        MediaNeo = ExtDeviceBase.Base + 531,
        MediaVhsc = ExtDeviceBase.Base + 925,
        MediaUnknown = ExtDeviceBase.Base + 926,
        MediaNotPresent = ExtDeviceBase.Base + 927,
        LinkMode = ExtDeviceBase.Base + 530,
        DevRemovedHeventGet = ExtDeviceBase.Base + 960,
        DevRemovedHeventRelease = ExtDeviceBase.Base + 961,
        ModeChangeNotify = ExtDeviceBase.Base + 932,
        ControlHeventGet = ExtDeviceBase.Base + 928,
        ControlHeventRelease = ExtDeviceBase.Base + 929,
        NotifyHeventGet = ExtDeviceBase.Base + 930,
        NotifyHeventRelease = ExtDeviceBase.Base + 931
    }

    /// <summary>
    /// From ED_TRANSBASIC* etc.
    /// </summary>
    public enum ExtTransportParameters
    {
        None = 0,
        TimeFormat = ExtDeviceBase.Base + 540,
        TimeFormatMilliseconds = ExtDeviceBase.Base + 541,
        TimeFormatFrames = ExtDeviceBase.Base + 542,
        TimeFormatReferenceTime = ExtDeviceBase.Base + 543,
        TimeFormatHmsf = ExtDeviceBase.Base + 547,
        TimeFormatTmsf = ExtDeviceBase.Base + 548,
        TimeReference = ExtDeviceBase.Base + 549,
        TimeReferenceTimeCode = ExtDeviceBase.Base + 550,
        TimeReferenceControlTrack = ExtDeviceBase.Base + 551,
        TimeReferenceIndex = ExtDeviceBase.Base + 552,
        TimeReferenceAtn = ExtDeviceBase.Base + 958,
        SuperImpose = ExtDeviceBase.Base + 553,
        EndStopAction = ExtDeviceBase.Base + 554,
        RecordFormat = ExtDeviceBase.Base + 555,
        RecordFormatSp = ExtDeviceBase.Base + 556,
        RecordFormatLp = ExtDeviceBase.Base + 557,
        RecordFormatEp = ExtDeviceBase.Base + 558,
        StepCount = ExtDeviceBase.Base + 559,
        StepUnit = ExtDeviceBase.Base + 560,
        StepField = ExtDeviceBase.Base + 561,
        StepFrame = ExtDeviceBase.Base + 562,
        Step3_2 = ExtDeviceBase.Base + 563,
        PreRoll = ExtDeviceBase.Base + 564,
        RecPreRoll = ExtDeviceBase.Base + 565,
        PostRoll = ExtDeviceBase.Base + 566,
        EditDelay = ExtDeviceBase.Base + 567,
        PlayTcDelay = ExtDeviceBase.Base + 568,
        RecTcDelay = ExtDeviceBase.Base + 569,
        EditField = ExtDeviceBase.Base + 570,
        FrameServo = ExtDeviceBase.Base + 571,
        CfServo = ExtDeviceBase.Base + 572,
        ServoRef = ExtDeviceBase.Base + 573,
        ServoRefExternal = ExtDeviceBase.Base + 574,
        ServoRefInput = ExtDeviceBase.Base + 575,
        ServoRefInternal = ExtDeviceBase.Base + 576,
        ServoRefAuto = ExtDeviceBase.Base + 577,
        WarnGl = ExtDeviceBase.Base + 578,
        SetTracking = ExtDeviceBase.Base + 579,
        SetTrackingPlus = ExtDeviceBase.Base + 580,
        SetTrackingMinus = ExtDeviceBase.Base + 581,
        SetTrackingReset = ExtDeviceBase.Base + 582,
        SetFreezeTimeout = ExtDeviceBase.Base + 583,
        VolumeName = ExtDeviceBase.Base + 584,
        Ballistic_1 = ExtDeviceBase.Base + 585,
        Ballistic_2 = ExtDeviceBase.Base + 586,
        Ballistic_3 = ExtDeviceBase.Base + 587,
        Ballistic_4 = ExtDeviceBase.Base + 588,
        Ballistic_5 = ExtDeviceBase.Base + 589,
        Ballistic_6 = ExtDeviceBase.Base + 590,
        Ballistic_7 = ExtDeviceBase.Base + 591,
        Ballistic_8 = ExtDeviceBase.Base + 592,
        Ballistic_9 = ExtDeviceBase.Base + 593,
        Ballistic_10 = ExtDeviceBase.Base + 594,
        Ballistic_11 = ExtDeviceBase.Base + 595,
        Ballistic_12 = ExtDeviceBase.Base + 596,
        Ballistic_13 = ExtDeviceBase.Base + 597,
        Ballistic_14 = ExtDeviceBase.Base + 598,
        Ballistic_15 = ExtDeviceBase.Base + 599,
        Ballistic_16 = ExtDeviceBase.Base + 600,
        Ballistic_17 = ExtDeviceBase.Base + 601,
        Ballistic_18 = ExtDeviceBase.Base + 602,
        Ballistic_19 = ExtDeviceBase.Base + 603,
        Ballistic_20 = ExtDeviceBase.Base + 604,
        SetClock = ExtDeviceBase.Base + 605,
        SetCounterFormat = ExtDeviceBase.Base + 606,
        SetCounterValue = ExtDeviceBase.Base + 607,
        SetTunerChUp = ExtDeviceBase.Base + 608,
        SetTunerChDn = ExtDeviceBase.Base + 609,
        SetTunerSkUp = ExtDeviceBase.Base + 610,
        SetTunerSkDn = ExtDeviceBase.Base + 611,
        SetTunerCh = ExtDeviceBase.Base + 612,
        SetTunerNum = ExtDeviceBase.Base + 613,
        SetTimerEvent = ExtDeviceBase.Base + 614,
        SetTimerStartDay = ExtDeviceBase.Base + 615,
        SetTimerStartTime = ExtDeviceBase.Base + 616,
        SetTimerStopDay = ExtDeviceBase.Base + 617,
        SetTimerStopTime = ExtDeviceBase.Base + 618,
        VideoSetOutput = ExtDeviceBase.Base + 630,
        E2E = ExtDeviceBase.Base + 631,
        Playback = ExtDeviceBase.Base + 632,
        Off = ExtDeviceBase.Base + 633,
        VideoSetSource = ExtDeviceBase.Base + 634,
        AudioEnableOutput = ExtDeviceBase.Base + 640,
        AudioEnableRecord = ExtDeviceBase.Base + 642,
        AudioEnableSelsync = ExtDeviceBase.Base + 643,
        AudioSetSource = ExtDeviceBase.Base + 644,
        AudioSetMonitor = ExtDeviceBase.Base + 645,
        RawExtDeviceCommand = ExtDeviceBase.Base + 920,
        InputSignal = ExtDeviceBase.Base + 940,
        OutputSignal = ExtDeviceBase.Base + 941,
        Signal_525_60_SD = ExtDeviceBase.Base + 942,
        Signal_525_60_SDL = ExtDeviceBase.Base + 943,
        Signal_625_50_SD = ExtDeviceBase.Base + 944,
        Signal_625_50_SDL = ExtDeviceBase.Base + 945,
        Signal_MPEG2TS = ExtDeviceBase.Base + 946,
        Signal_625_60_HD = ExtDeviceBase.Base + 947,
        Signal_625_50_HD = ExtDeviceBase.Base + 948,
        Signal_2500_60_MPEG = ExtDeviceBase.Base + 980,
        Signal_1250_60_MPEG = ExtDeviceBase.Base + 981,
        Signal_0625_60_MPEG = ExtDeviceBase.Base + 982,
        Signal_2500_50_MPEG = ExtDeviceBase.Base + 985,
        Signal_1250_50_MPEG = ExtDeviceBase.Base + 986,
        Signal_0625_50_MPEG = ExtDeviceBase.Base + 987,
        SignalUnknown = ExtDeviceBase.Base + 990
    }

    /// <summary>
    /// From ED_AUDIO*
    /// </summary>
    [Flags]
    public enum ExtTransportAudio
    {
        None = 0,
        AudioAll = 0x10000000,  //  or any of the following OR'd together
        Audio1 = 0x0000001,
        Audio2 = 0x0000002,
        Audio3 = 0x0000004,
        Audio4 = 0x0000008,
        Audio5 = 0x0000010,
        Audio6 = 0x0000020,
        Audio7 = 0x0000040,
        Audio8 = 0x0000080,
        Audio9 = 0x0000100,
        Audio10 = 0x0000200,
        Audio11 = 0x0000400,
        Audio12 = 0x0000800,
        Audio13 = 0x0001000,
        Audio14 = 0x0002000,
        Audio15 = 0x0004000,
        Audio16 = 0x0008000,
        Audio17 = 0x0010000,
        Audio18 = 0x0020000,
        Audio19 = 0x0040000,
        Audio20 = 0x0080000,
        Audio21 = 0x0100000,
        Audio22 = 0x0200000,
        Audio23 = 0x0400000,
        Audio24 = 0x0800000,
        Video = 0x2000000
    }

    /// <summary>
    /// From ED_EDIT*
    /// </summary>
    public enum ExtTransportEdit
    {
        Invalid = ExtDeviceBase.Base + 652,
        Executing = ExtDeviceBase.Base + 653,
        Active = ExtDeviceBase.Base + 53,
        Inactive = ExtDeviceBase.Base + 54,
        Register = ExtDeviceBase.Base + 654,
        Delete = ExtDeviceBase.Base + 655,
        Hevent = ExtDeviceBase.Base + 656,
        Test = ExtDeviceBase.Base + 657,
        Immediate = ExtDeviceBase.Base + 658,
        Mode = ExtDeviceBase.Base + 659,
        ModeAssemble = ExtDeviceBase.Base + 660,
        ModeInsert = ExtDeviceBase.Base + 661,
        ModeCrashRecord = ExtDeviceBase.Base + 662,
        ModeBookmarkTime = ExtDeviceBase.Base + 663,
        ModeBookmarkChapter = ExtDeviceBase.Base + 664,
        Master = ExtDeviceBase.Base + 666,
        Track = ExtDeviceBase.Base + 667,
        SourceInPoint = ExtDeviceBase.Base + 668,
        SourceOutPoint = ExtDeviceBase.Base + 669,
        RecInPoint = ExtDeviceBase.Base + 670,
        RecOutPoint = ExtDeviceBase.Base + 671,
        RehearseMode = ExtDeviceBase.Base + 672,
        BVB = ExtDeviceBase.Base + 673,
        VBV = ExtDeviceBase.Base + 674,
        VVV = ExtDeviceBase.Base + 675,
        Perform = ExtDeviceBase.Base + 676,
        Abort = ExtDeviceBase.Base + 677,
        TimeOut = ExtDeviceBase.Base + 678,
        Seek = ExtDeviceBase.Base + 679,
        SeekMode = ExtDeviceBase.Base + 680,
        SeekEditIn = ExtDeviceBase.Base + 681,
        SeekEditOut = ExtDeviceBase.Base + 682,
        SeekPreRoll = ExtDeviceBase.Base + 683,
        SeekPreRollCt = ExtDeviceBase.Base + 684,
        SeekBookmark = ExtDeviceBase.Base + 685,
        Offset = ExtDeviceBase.Base + 686,
        PreRead = ExtDeviceBase.Base + 815,
    }

    /// <summary>
    /// From VIDEO_STREAM_CONFIG_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class VideoStreamConfigCaps
    {
        public Guid guid;
        public AnalogVideoStandard VideoStandard;
        public Size InputSize;
        public Size MinCroppingSize;
        public Size MaxCroppingSize;
        public int CropGranularityX;
        public int CropGranularityY;
        public int CropAlignX;
        public int CropAlignY;
        public Size MinOutputSize;
        public Size MaxOutputSize;
        public int OutputGranularityX;
        public int OutputGranularityY;
        public int StretchTapsX;
        public int StretchTapsY;
        public int ShrinkTapsX;
        public int ShrinkTapsY;
        public long MinFrameInterval;
        public long MaxFrameInterval;
        public int MinBitsPerSecond;
        public int MaxBitsPerSecond;
    }

    /// <summary>
    /// From AUDIO_STREAM_CONFIG_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AudioStreamConfigCaps
    {
        public Guid guid;
        public int MinimumChannels;
        public int MaximumChannels;
        public int ChannelsGranularity;
        public int MinimumBitsPerSample;
        public int MaximumBitsPerSample;
        public int BitsPerSampleGranularity;
        public int MinimumSampleFrequency;
        public int MaximumSampleFrequency;
        public int SampleFrequencyGranularity;
    }

    /// <summary>
    /// From Quality
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Quality
    {
        public QualityMessageType Type;
        public int Proportion;
        public long Late;
        public long TimeStamp;
    }

    /// <summary>
    /// From QualityMessageType
    /// </summary>
    public enum QualityMessageType
    {
        Famine,
        Flood
    }

    /// <summary>
    /// From CameraControlProperty
    /// </summary>
    public enum CameraControlProperty
    {
        Pan = 0,
        Tilt,
        Roll,
        Zoom,
        Exposure,
        Iris,
        Focus
    }

    /// <summary>
    /// From CameraControlFlags
    /// </summary>
    [Flags]
    public enum CameraControlFlags
    {
        None = 0x0,
        Auto = 0x0001,
        Manual = 0x0002
    }

    #endregion
}