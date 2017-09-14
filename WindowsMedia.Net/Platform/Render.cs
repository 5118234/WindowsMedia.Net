using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsMedia.Platform
{
    #region Declarations

    /// <summary>
    /// Bit mask containing one or more of the following DirectShow DirectDraw Surface (AMDDS) surface types.
    /// </summary>
    [Flags]
    public enum DirectDrawSwitches
    {
        /// <summary>
        /// No use for DCI/DirectDraw.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Use DCI primary surface.
        /// </summary>
        DCIPS = 0x01,

        /// <summary>
        /// Use DirectDraw primary surface.
        /// </summary>
        PS = 0x02,

        /// <summary>
        /// RGB overlay surfaces.
        /// </summary>
        RGBOVR = 0x04,

        /// <summary>
        /// YUV overlay surfaces.
        /// </summary>
        YUVOVR = 0x08,

        /// <summary>
        /// RGB off-screen surfaces.
        /// </summary>
        RGBOFF = 0x10,

        /// <summary>
        /// YUV off-screen surfaces.
        /// </summary>
        YUVOFF = 0x20,

        /// <summary>
        /// RGB flipping surfaces.
        /// </summary>
        RGBFLP = 0x40,

        /// <summary>
        /// YUV flipping surfaces.
        /// </summary>
        YUVFLP = 0x80,

        /// <summary>
        /// All the previous flags.
        /// </summary>
        All = 0xFF,

        /// <summary>
        /// Use all available surfaces.
        /// </summary>
        DEFAULT = (YUV | RGB | Primary),

        /// <summary>
        /// Use all YUV surfaces.
        /// </summary>
        YUV = (YUVOFF | YUVOVR | YUVFLP),

        /// <summary>
        /// Use all RGB surfaces.
        /// </summary>
        RGB = (RGBOFF | RGBOVR | RGBFLP),

        /// <summary>
        /// Use all primary surfaces.
        /// </summary>
        Primary = (DCIPS | PS)
    }

    /// <summary>
    /// From AM_PROPERTY_FRAMESTEP
    /// </summary>
    public enum PropertyFrameStep
    {
        Step = 0x01,
        Cancel = 0x02,
        CanStep = 0x03,
        CanStepMultiple = 0x04
    }

    /// <summary>
    /// From AM_FRAMESTEP_STEP
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FrameStepStep
    {
        public int dwFramesToStep;
    }

    /// <summary>
    /// From MPEG1VIDEOINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MPEG1VideoInfo
    {
        public VideoInfoHeader hdr;
        public int dwStartTimeCode;
        public int cbSequenceHeader;
        public byte bSequenceHeader;
    }

    /// <summary>
    /// From ANALOGVIDEOINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AnalogVideoInfo
    {
        public Rectangle rcSource;
        public Rectangle rcTarget;
        public int dwActiveWidth;
        public int dwActiveHeight;
        public long AvgTimePerFrame;
    }

    /// <summary>
    /// From VMRPresentationFlags
    /// </summary>
    [Flags]
    public enum VMRPresentationFlags
    {
        None = 0,
        SyncPoint = 0x00000001,
        Preroll = 0x00000002,
        Discontinuity = 0x00000004,
        TimeValid = 0x00000008,
        SrcDstRectsValid = 0x00000010
    }

    /// <summary>
    /// From VMRSurfaceAllocationFlags
    /// </summary>
    [Flags]
    public enum VMRSurfaceAllocationFlags
    {
        None = 0,
        PixelFormatValid = 0x01,
        ThreeDTarget = 0x02,
        AllowSysMem = 0x04,
        ForceSysMem = 0x08,
        DirectedFlip = 0x10,
        DXVATarget = 0x20
    }

    /// <summary>
    /// From VMRPRESENTATIONINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMRPresentationInfo
    {
        public VMRPresentationFlags dwFlags;
        public IntPtr lpSurf; //LPDIRECTDRAWSURFACE7
        public long rtStart;
        public long rtEnd;
        public Size szAspectRatio;
        public DsRect rcSrc;
        public DsRect rcDst;
        public int dwTypeSpecificFlags;
        public int dwInterlaceFlags;
    }

    /// <summary>
    /// From VMRALLOCATIONINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMRAllocationInfo
    {
        public VMRSurfaceAllocationFlags dwFlags;
        //    public BitmapInfoHeader lpHdr;
        //    public DDPixelFormat lpPixFmt;
        public IntPtr lpHdr;
        public IntPtr lpPixFmt;
        public Size szAspectRatio;
        public int dwMinBuffers;
        public int dwMaxBuffers;
        public int dwInterlaceFlags;
        public Size szNativeSize;
    }

    /// <summary>
    /// From VMRDeinterlaceTech
    /// </summary>
    [Flags]
    public enum VMRDeinterlaceTech
    {
        Unknown = 0x0000,
        BOBLineReplicate = 0x0001,
        BOBVerticalStretch = 0x0002,
        MedianFiltering = 0x0004,
        EdgeFiltering = 0x0010,
        FieldAdaptive = 0x0020,
        PixelAdaptive = 0x0040,
        MotionVectorSteered = 0x0080
    }

    /// <summary>
    /// From VMRBITMAP_* defines
    /// </summary>
    [Flags]
    public enum VMRBitmap
    {
        None = 0,
        Disable = 0x00000001,
        Hdc = 0x00000002,
        EntireDDS = 0x00000004,
        SRCColorKey = 0x00000008,
        SRCRect = 0x00000010
    }


    /// <summary>
    /// From VMRDeinterlacePrefs
    /// </summary>
    [Flags]
    public enum VMRDeinterlacePrefs
    {
        None = 0,
        NextBest = 0x01,
        BOB = 0x02,
        Weave = 0x04,
        Mask = 0x07
    }

    /// <summary>
    /// From VMRMixerPrefs
    /// </summary>
    [Flags]
    public enum VMRMixerPrefs
    {
        None = 0,
        NoDecimation = 0x00000001,
        DecimateOutput = 0x00000002,
        ARAdjustXorY = 0x00000004,
        DecimationReserved = 0x00000008,
        DecimateMask = 0x0000000F,

        BiLinearFiltering = 0x00000010,
        PointFiltering = 0x00000020,
        FilteringMask = 0x000000F0,

        RenderTargetRGB = 0x00000100,
        RenderTargetYUV = 0x00001000,

        RenderTargetYUV420 = 0x00000200,
        RenderTargetYUV422 = 0x00000400,
        RenderTargetYUV444 = 0x00000800,
        RenderTargetReserved = 0x0000E000,
        RenderTargetMask = 0x0000FF00,

        DynamicSwitchToBOB = 0x00010000,
        DynamicDecimateBy2 = 0x00020000,

        DynamicReserved = 0x000C0000,
        DynamicMask = 0x000F0000
    }

    /// <summary>
    /// From VMRRenderPrefs
    /// </summary>
    [Flags]
    public enum VMRRenderPrefs
    {
        RestrictToInitialMonitor = 0x00000000,
        ForceOffscreen = 0x00000001,
        ForceOverlays = 0x00000002,
        AllowOverlays = 0x00000000,
        AllowOffscreen = 0x00000000,
        DoNotRenderColorKeyAndBorder = 0x00000008,
        Reserved = 0x00000010,
        PreferAGPMemWhenMixing = 0x00000020,

        Mask = 0x0000003f,
    }

    /// <summary>
    /// From VMRMode
    /// </summary>
    [Flags]
    public enum VMRMode
    {
        None = 0,
        Windowed = 0x00000001,
        Windowless = 0x00000002,
        Renderless = 0x00000004,
    }

    /// <summary>
    /// From VMR_ASPECT_RATIO_MODE
    /// </summary>
    public enum VMRAspectRatioMode
    {
        None,
        LetterBox
    }

    /// <summary>
    /// From VMRALPHABITMAP
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMRAlphaBitmap
    {
        public VMRBitmap dwFlags;
        public IntPtr hdc; // HDC
        public IntPtr pDDS; //LPDIRECTDRAWSURFACE7
        public DsRect rSrc;
        public NormalizedRect rDest;
        public float fAlpha;
        public int clrSrcKey;
    }


    /// <summary>
    /// From VMRDeinterlaceCaps
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMRDeinterlaceCaps
    {
        public int dwSize;
        public int dwNumPreviousOutputFrames;
        public int dwNumForwardRefSamples;
        public int dwNumBackwardRefSamples;
        public VMRDeinterlaceTech DeinterlaceTechnology;
    }

    /// <summary>
    /// From VMRFrequency
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMRFrequency
    {
        public int dwNumerator;
        public int dwDenominator;
    }

    /// <summary>
    /// From VMRVideoDesc
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VMRVideoDesc
    {
        public int dwSize;
        public int dwSampleWidth;
        public int dwSampleHeight;
        [MarshalAs(UnmanagedType.Bool)] public bool SingleFieldPerSample;
        public int dwFourCC;
        public VMRFrequency InputSampleFreq;
        public VMRFrequency OutputFrameFreq;
    }

    /// <summary>
    /// From VMRVIDEOSTREAMINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMRVideoStreamInfo
    {
        public IntPtr pddsVideoSurface;
        public int dwWidth;
        public int dwHeight;
        public int dwStrmID;
        public float fAlpha;
        public DDColorKey ddClrKey;
        public NormalizedRect rNormal;
    }

    /// <summary>
    /// From DDCOLORKEY
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DDColorKey
    {
        public int dw1;
        public int dw2;
    }

    /// <summary>
    /// From VMRMONITORINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct VMRMonitorInfo
    {
        public VMRGuid guid;
        public DsRect rcMonitor;
        public IntPtr hMon; // HMONITOR
        public int dwFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szDevice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string szDescription;
        public long liDriverVersion;
        public int dwVendorId;
        public int dwDeviceId;
        public int dwSubSysId;
        public int dwRevision;
    }

    /// <summary>
    /// From VMRGUID
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMRGuid
    {
        public IntPtr pGUID; // GUID *
        public Guid GUID;
    }

    /// <summary>
    /// From VMR9PresentationFlags
    /// </summary>
    [Flags]
    public enum VMR9PresentationFlags
    {
        None = 0,
        SyncPoint = 0x00000001,
        Preroll = 0x00000002,
        Discontinuity = 0x00000004,
        TimeValid = 0x00000008,
        SrcDstRectsValid = 0x00000010
    }

    /// <summary>
    /// From VMR9SurfaceAllocationFlags
    /// </summary>
    [Flags]
    public enum VMR9SurfaceAllocationFlags
    {
        None = 0,
        ThreeDRenderTarget = 0x0001,
        DXVATarget = 0x0002,
        TextureSurface = 0x0004,
        OffscreenSurface = 0x0008,
        RGBDynamicSwitch = 0x0010,
        UsageReserved = 0x00e0,
        UsageMask = 0x00FF
    }

    /// <summary>
    /// From VMR9ProcAmpControlFlags
    /// </summary>
    [Flags]
    public enum VMR9ProcAmpControlFlags
    {
        None = 0,
        Brightness = 0x00000001,
        Contrast = 0x00000002,
        Hue = 0x00000004,
        Saturation = 0x00000008,
        Mask = 0x0000000F
    }


    /// <summary>
    /// From VMR9MixerPrefs
    /// </summary>
    [Flags]
    public enum VMR9MixerPrefs
    {
        None = 0,
        NoDecimation = 0x00000001, // No decimation - full size
        DecimateOutput = 0x00000002, // decimate output by 2 in x & y
        ARAdjustXorY = 0x00000004, // adjust the aspect ratio in x or y
        NonSquareMixing = 0x00000008, // assume AP can handle non-square mixing, avoids intermediate scales
        DecimateMask = 0x0000000F,

        BiLinearFiltering = 0x00000010, // use bi-linear filtering
        PointFiltering = 0x00000020, // use point filtering
        AnisotropicFiltering = 0x00000040, //
        PyramidalQuadFiltering = 0x00000080, // 4-sample tent
        GaussianQuadFiltering = 0x00000100, // 4-sample gaussian
        FilteringReserved = 0x00000E00, // bits reserved for future use.
        FilteringMask = 0x00000FF0, // OR of all above flags

        RenderTargetRGB = 0x00001000,
        RenderTargetYUV = 0x00002000, // Uses DXVA to perform mixing
        RenderTargetReserved = 0x000FC000, // bits reserved for future use.
        RenderTargetMask = 0x000FF000, // OR of all above flags

        DynamicSwitchToBOB = 0x00100000,
        DynamicDecimateBy2 = 0x00200000,

        DynamicReserved = 0x00C00000,
        DynamicMask = 0x00F00000
    }

    /// <summary>
    /// From VMR9DeinterlaceTech
    /// </summary>
    [Flags]
    public enum VMR9DeinterlaceTech
    {
        Unknown = 0x0000,
        BOBLineReplicate = 0x0001,
        BOBVerticalStretch = 0x0002,
        MedianFiltering = 0x0004,
        EdgeFiltering = 0x0010,
        FieldAdaptive = 0x0020,
        PixelAdaptive = 0x0040,
        MotionVectorSteered = 0x0080
    }

    /// <summary>
    /// From VMR9AlphaBitmapFlags
    /// </summary>
    [Flags]
    public enum VMR9AlphaBitmapFlags
    {
        None = 0,
        Disable = 0x00000001,
        hDC = 0x00000002,
        EntireDDS = 0x00000004,
        SrcColorKey = 0x00000008,
        SrcRect = 0x00000010,
        FilterMode = 0x00000020
    }

    /// <summary>
    /// From VMR9DeinterlacePrefs
    /// </summary>
    [Flags]
    public enum VMR9DeinterlacePrefs
    {
        None = 0,
        NextBest = 0x01,
        BOB = 0x02,
        Weave = 0x04,
        Mask = 0x07
    }

    /// <summary>
    /// From VMR9RenderPrefs
    /// </summary>
    [Flags]
    public enum VMR9RenderPrefs
    {
        None = 0,
        DoNotRenderBorder = 0x00000001, // app paints color keys
        Mask = 0x00000001, // OR of all above flags
    }

    /// <summary>
    /// From VMR9Mode
    /// </summary>
    [Flags]
    public enum VMR9Mode
    {
        None = 0,
        Windowed = 0x00000001,
        Windowless = 0x00000002,
        Renderless = 0x00000004,
        Mask = 0x00000007
    }

    /// <summary>
    /// From VMR9AspectRatioMode
    /// </summary>
    public enum VMR9AspectRatioMode
    {
        None,
        LetterBox,
    }

    /// <summary>
    /// From VMR9_SampleFormat
    /// </summary>
    public enum VMR9SampleFormat
    {
        None = 0,
        Reserved = 1,
        ProgressiveFrame = 2,
        FieldInterleavedEvenFirst = 3,
        FieldInterleavedOddFirst = 4,
        FieldSingleEven = 5,
        FieldSingleOdd = 6
    }

    /// <summary>
    /// From VMR9PresentationInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9PresentationInfo
    {
        public VMR9PresentationFlags dwFlags;
        public IntPtr lpSurf; //IDirect3DSurface9
        public long rtStart;
        public long rtEnd;
        public Size szAspectRatio;
        public DsRect rcSrc;
        public DsRect rcDst;
        public int dwReserved1;
        public int dwReserved2;
    }

    /// <summary>
    /// From VMR9AllocationInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9AllocationInfo
    {
        public VMR9SurfaceAllocationFlags dwFlags;
        public int dwWidth;
        public int dwHeight;
        public int Format; // D3DFORMAT
        public int Pool; // D3DPOOL
        public int MinBuffers;
        public Size szAspectRatio;
        public Size szNativeSize;
    }

    /// <summary>
    /// From VMR9ProcAmpControl
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9ProcAmpControl
    {
        public int dwSize; // should be 24
        public VMR9ProcAmpControlFlags dwFlags;
        public float Brightness;
        public float Contrast;
        public float Hue;
        public float Saturation;
    }

    /// <summary>
    /// From VMR9MonitorInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct VMR9MonitorInfo
    {
        public int uDevID;
        public DsRect rcMonitor;
        public IntPtr hMon;
        public int dwFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szDevice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)] public string szDescription;
        public long liDriverVersion;
        public int dwVendorId;
        public int dwDeviceId;
        public int dwSubSysId;
        public int dwRevision;
    }

    /// <summary>
    /// From VMR9DeinterlaceCaps
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9DeinterlaceCaps
    {
        public int dwSize;
        public int dwNumPreviousOutputFrames;
        public int dwNumForwardRefSamples;
        public int dwNumBackwardRefSamples;
        public VMR9DeinterlaceTech DeinterlaceTechnology;
    }

    /// <summary>
    /// From VMR9VideoStreamInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9VideoStreamInfo
    {
        public IntPtr pddsVideoSurface; // IDirect3DSurface9
        public int dwWidth;
        public int dwHeight;
        public int dwStrmID;
        public float fAlpha;
        public NormalizedRect rNormal;
        public long rtStart;
        public long rtEnd;
        public VMR9SampleFormat SampleFormat;
    }

    /// <summary>
    /// From VMR9VideoDesc
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9VideoDesc
    {
        public int dwSize;
        public int dwSampleWidth;
        public int dwSampleHeight;
        public VMR9SampleFormat SampleFormat;
        public int dwFourCC;
        public VMR9Frequency InputSampleFreq;
        public VMR9Frequency OutputFrameFreq;
    }

    /// <summary>
    /// From VMR9Frequency
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9Frequency
    {
        public int dwNumerator;
        public int dwDenominator;
    }

    /// <summary>
    /// From VMR9AlphaBitmap
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9AlphaBitmap
    {
        public VMR9AlphaBitmapFlags dwFlags;
        public IntPtr hdc; // HDC
        public IntPtr pDDS; // IDirect3DSurface9
        public DsRect rSrc;
        public NormalizedRect rDest;
        public float fAlpha;
        public int clrSrcKey;
        public VMRMixerPrefs dwFilterMode;
    }

    /// <summary>
    /// From VMR9ProcAmpControlRange
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9ProcAmpControlRange
    {
        public int dwSize; // should be 24
        public VMR9ProcAmpControlFlags dwProperty;
        public float MinValue;
        public float MaxValue;
        public float DefaultValue;
        public float StepSize;
    }

    #endregion

    #region Interfaces

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("dfc581a1-6e1f-4c3a-8d0a-5e9792ea2afc"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurface9
    {
        [PreserveSig]
        int IsSurfaceLocked();

        [PreserveSig]
        int LockSurface([Out] out IntPtr lpSurface); // BYTE**

        [PreserveSig]
        int UnlockSurface();

        [PreserveSig]
        int GetSurface([Out, MarshalAs(UnmanagedType.IUnknown)] out object lplpSurface);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("69188c61-12a3-40f0-8ffc-342e7b433fd7"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImagePresenter9
    {
        [PreserveSig]
        int StartPresenting([In] IntPtr dwUserID);

        [PreserveSig]
        int StopPresenting([In] IntPtr dwUserID);

        [PreserveSig]
        int PresentImage([In] IntPtr dwUserID, [In] ref VMR9PresentationInfo lpPresInfo);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("6de9a68a-a928-4522-bf57-655ae3866456"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurfaceAllocatorEx9 : IVMRSurfaceAllocator9
    {
        #region IVMRSurfaceAllocator9 Methods

        [PreserveSig]
        new int InitializeDevice(
            [In] IntPtr dwUserID,
            [In] ref VMR9AllocationInfo lpAllocInfo,
            [In, Out] ref int lpNumBuffers
        );

        [PreserveSig]
        new int TerminateDevice([In] IntPtr dwID);

        [PreserveSig]
        new int GetSurface(
            [In] IntPtr dwUserID,
            [In] int SurfaceIndex,
            [In] int SurfaceFlags,
            [Out] out IntPtr lplpSurface
        );

        [PreserveSig]
        new int AdviseNotify([In] IVMRSurfaceAllocatorNotify9 lpIVMRSurfAllocNotify);

        #endregion

        [PreserveSig]
        int GetSurfaceEx(
            [In] IntPtr dwUserID,
            [In] int SurfaceIndex,
            [In] int SurfaceFlags,
            [Out] out IntPtr lplpSurface,
            [Out] out DsRect lprcDst
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("dca3f5df-bb3a-4d03-bd81-84614bfbfa0c"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurfaceAllocatorNotify9
    {
        [PreserveSig]
        int AdviseSurfaceAllocator(
            [In] IntPtr dwUserID,
            [In] IVMRSurfaceAllocator9 lpIVRMSurfaceAllocator
        );

        [PreserveSig]
        int SetD3DDevice(
            [In] IntPtr lpD3DDevice,
            [In] IntPtr hMonitor
        );

        [PreserveSig]
        int ChangeD3DDevice(
            [In] IntPtr lpD3DDevice,
            [In] IntPtr hMonitor
        );

        [PreserveSig]
        int AllocateSurfaceHelper(
            [In] ref VMR9AllocationInfo lpAllocInfo,
            [In, Out] ref int lpNumBuffers,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt)] IntPtr[] lplpSurface
        );

        [PreserveSig]
        int NotifyEvent(
            [In] EventCode EvCode,
            [In] IntPtr Param1,
            [In] IntPtr Param2
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("8d5148ea-3f5d-46cf-9df1-d1b896eedb1f"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurfaceAllocator9
    {
        [PreserveSig]
        int InitializeDevice(
            [In] IntPtr dwUserID,
            [In] ref VMR9AllocationInfo lpAllocInfo,
            [In, Out] ref int lpNumBuffers
        );

        [PreserveSig]
        int TerminateDevice([In] IntPtr dwID);

        [PreserveSig]
        int GetSurface(
            [In] IntPtr dwUserID,
            [In] int SurfaceIndex,
            [In] int SurfaceFlags,
            [Out] out IntPtr lplpSurface
        );

        [PreserveSig]
        int AdviseNotify([In] IVMRSurfaceAllocatorNotify9 lpIVMRSurfAllocNotify);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("5a804648-4f66-4867-9c43-4f5c822cf1b8"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRFilterConfig9
    {
        [PreserveSig]
        int SetImageCompositor([In] IVMRImageCompositor9 lpVMRImgCompositor);

        [PreserveSig]
        int SetNumberOfStreams([In] int dwMaxStreams);

        [PreserveSig]
        int GetNumberOfStreams([Out] out int pdwMaxStreams);

        [PreserveSig]
        int SetRenderingPrefs([In] VMR9RenderPrefs dwRenderFlags);

        [PreserveSig]
        int GetRenderingPrefs([Out] out VMR9RenderPrefs pdwRenderFlags);

        [PreserveSig]
        int SetRenderingMode([In] VMR9Mode Mode);

        [PreserveSig]
        int GetRenderingMode([Out] out VMR9Mode Mode);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("8f537d09-f85e-4414-b23b-502e54c79927"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRWindowlessControl9
    {
        int GetNativeVideoSize(
            [Out] out int lpWidth,
            [Out] out int lpHeight,
            [Out] out int lpARWidth,
            [Out] out int lpARHeight
        );

        int GetMinIdealVideoSize(
            [Out] out int lpWidth,
            [Out] out int lpHeight
        );

        int GetMaxIdealVideoSize(
            [Out] out int lpWidth,
            [Out] out int lpHeight
        );

        int SetVideoPosition(
            [In] DsRect lpSRCRect,
            [In] DsRect lpDSTRect
        );

        int GetVideoPosition(
            [Out] DsRect lpSRCRect,
            [Out] DsRect lpDSTRect
        );

        int GetAspectRatioMode([Out] out VMR9AspectRatioMode lpAspectRatioMode);

        int SetAspectRatioMode([In] VMR9AspectRatioMode AspectRatioMode);

        int SetVideoClippingWindow([In] IntPtr hwnd); // HWND

        int RepaintVideo(
            [In] IntPtr hwnd, // HWND
            [In] IntPtr hdc // HDC
        );

        int DisplayModeChanged();

        int GetCurrentImage([Out] out IntPtr lpDib); // BYTE**

        int SetBorderColor([In] int Clr);

        int GetBorderColor([Out] out int lpClr);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("00d96c29-bbde-4efc-9901-bb5036392146"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRAspectRatioControl9
    {
        [PreserveSig]
        int GetAspectRatioMode([Out] out VMRAspectRatioMode lpdwARMode);

        [PreserveSig]
        int SetAspectRatioMode([In] VMRAspectRatioMode lpdwARMode);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("a215fb8d-13c2-4f7f-993c-003d6271a459"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRDeinterlaceControl9
    {
        [PreserveSig]
        int GetNumberOfDeinterlaceModes(
            [In] ref VMR9VideoDesc lpVideoDescription,
            [In, Out] ref int lpdwNumDeinterlaceModes,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] lpDeinterlaceModes
        );

        [PreserveSig]
        int GetDeinterlaceModeCaps(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid lpDeinterlaceMode,
            [In] ref VMR9VideoDesc lpVideoDescription,
            [In, Out] ref VMR9DeinterlaceCaps lpDeinterlaceCaps
        );

        [PreserveSig]
        int GetDeinterlaceMode(
            [In] int dwStreamID,
            [Out] out Guid lpDeinterlaceMode
        );

        [PreserveSig]
        int SetDeinterlaceMode(
            [In] int dwStreamID,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid lpDeinterlaceMode
        );

        [PreserveSig]
        int GetDeinterlacePrefs([Out] out VMR9DeinterlacePrefs lpdwDeinterlacePrefs);

        [PreserveSig]
        int SetDeinterlacePrefs([In] VMR9DeinterlacePrefs lpdwDeinterlacePrefs);

        [PreserveSig]
        int GetActualDeinterlaceMode(
            [In] int dwStreamID,
            [Out] out Guid lpDeinterlaceMode
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("4a5c89eb-df51-4654-ac2a-e48e02bbabf6"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImageCompositor9
    {
        [PreserveSig]
        int InitCompositionDevice([In] IntPtr pD3DDevice);

        [PreserveSig]
        int TermCompositionDevice([In] IntPtr pD3DDevice);

        [PreserveSig]
        int SetStreamMediaType(
            [In] int dwStrmID,
            [In] AMMediaType pmt,
            [In, MarshalAs(UnmanagedType.Bool)] bool fTexture
        );

        [PreserveSig]
        int CompositeImage(
            [In] IntPtr pD3DDevice,
            [In] IntPtr pddsRenderTarget, // IDirect3DSurface9
            [In] AMMediaType pmtRenderTarget,
            [In] long rtStart,
            [In] long rtEnd,
            [In] int dwClrBkGnd,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 7)] VMR9VideoStreamInfo[] pVideoStreamInfo,
            [In] int cStreams
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("45c15cab-6e22-420a-8043-ae1f0ac02c7d"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImagePresenterConfig9
    {
        [PreserveSig]
        int SetRenderingPrefs([In] VMR9RenderPrefs dwRenderFlags);

        [PreserveSig]
        int GetRenderingPrefs([Out] out VMR9RenderPrefs dwRenderFlags);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("ced175e5-1935-4820-81bd-ff6ad00c9108"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMixerBitmap9
    {
        [PreserveSig]
        int SetAlphaBitmap([In] ref VMR9AlphaBitmap pBmpParms);

        [PreserveSig]
        int UpdateAlphaBitmapParameters([In] ref VMR9AlphaBitmap pBmpParms);

        [PreserveSig]
        int GetAlphaBitmapParameters([Out] out VMR9AlphaBitmap pBmpParms);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("1a777eaa-47c8-4930-b2c9-8fee1c1b0f3b"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMixerControl9
    {
        [PreserveSig]
        int SetAlpha(
            [In] int dwStreamID,
            [In] float Alpha
        );

        [PreserveSig]
        int GetAlpha(
            [In] int dwStreamID,
            [Out] out float Alpha
        );

        [PreserveSig]
        int SetZOrder(
            [In] int dwStreamID,
            [In] int dwZ
        );

        [PreserveSig]
        int GetZOrder(
            [In] int dwStreamID,
            [Out] out int dwZ
        );

        [PreserveSig]
        int SetOutputRect(
            [In] int dwStreamID,
            [In] ref NormalizedRect pRect
        );

        [PreserveSig]
        int GetOutputRect(
            [In] int dwStreamID,
            [Out] out NormalizedRect pRect
        );

        [PreserveSig]
        int SetBackgroundClr([In] int ClrBkg);

        [PreserveSig]
        int GetBackgroundClr([Out] out int ClrBkg);

        [PreserveSig]
        int SetMixingPrefs([In] VMR9MixerPrefs dwMixerPrefs);

        [PreserveSig]
        int GetMixingPrefs([Out] out VMR9MixerPrefs dwMixerPrefs);

        [PreserveSig]
        int SetProcAmpControl(
            [In] int dwStreamID,
            [In] ref VMR9ProcAmpControl lpClrControl
        );

        [PreserveSig]
        int GetProcAmpControl(
            [In] int dwStreamID,
            [In, Out] ref VMR9ProcAmpControl lpClrControl
        );

        [PreserveSig]
        int GetProcAmpControlRange(
            [In] int dwStreamID,
            [In, Out] ref VMR9ProcAmpControlRange lpClrControl
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("46c2e457-8ba0-4eef-b80b-0680f0978749"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMonitorConfig9
    {
        [PreserveSig]
        int SetMonitor([In] int uDev);

        [PreserveSig]
        int GetMonitor([Out] out int uDev);

        [PreserveSig]
        int SetDefaultMonitor([In] int uDev);

        [PreserveSig]
        int GetDefaultMonitor([Out] out int uDev);

        [PreserveSig]
        int GetAvailableMonitors(
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] VMR9MonitorInfo[] pInfo,
            [In] int dwMaxInfoArraySize,
            [Out] out int pdwNumDevices
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("d0cfe38b-93e7-4772-8957-0400c49a4485"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRVideoStreamControl9
    {
        [PreserveSig]
        int SetStreamActiveState([In, MarshalAs(UnmanagedType.Bool)] bool fActive);

        [PreserveSig]
        int GetStreamActiveState([Out, MarshalAs(UnmanagedType.Bool)] out bool fActive);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("CE704FE7-E71E-41fb-BAA2-C4403E1182F5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImagePresenter
    {
        [PreserveSig]
        int StartPresenting([In] IntPtr dwUserID);

        [PreserveSig]
        int StopPresenting([In] IntPtr dwUserID);

        [PreserveSig]
        int PresentImage(
            [In] IntPtr dwUserID,
            [In] ref VMRPresentationInfo lpPresInfo
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("31ce832e-4484-458b-8cca-f4d7e3db0b52"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurfaceAllocator
    {
        [PreserveSig]
        int AllocateSurface(
            [In] IntPtr dwUserID,
            [In] ref VMRAllocationInfo lpAllocInfo,
            [Out] out int lpdwActualBuffers,
            [In, Out] ref IntPtr lplpSurface // LPDIRECTDRAWSURFACE7
            );

        [PreserveSig]
        int FreeSurface([In] IntPtr dwID);

        [PreserveSig]
        int PrepareSurface(
            [In] IntPtr dwUserID,
            [In] IntPtr lplpSurface, // LPDIRECTDRAWSURFACE7
            [In] int dwSurfaceFlags
            );

        [PreserveSig]
        int AdviseNotify([In] IVMRSurfaceAllocatorNotify lpIVMRSurfAllocNotify);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("aada05a8-5a4e-4729-af0b-cea27aed51e2"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurfaceAllocatorNotify
    {
        [PreserveSig]
        int AdviseSurfaceAllocator(
            [In] IntPtr dwUserID,
            [In] IVMRSurfaceAllocator lpIVRMSurfaceAllocator
            );

        [PreserveSig]
        int SetDDrawDevice(
            [In] IntPtr lpDDrawDevice, // LPDIRECTDRAW7
            [In] IntPtr hMonitor // HMONITOR
            );

        [PreserveSig]
        int ChangeDDrawDevice(
            [In] IntPtr lpDDrawDevice, // LPDIRECTDRAW7
            [In] IntPtr hMonitor // HMONITOR
            );

        [PreserveSig]
        int RestoreDDrawSurfaces();

        [PreserveSig]
        int NotifyEvent(
            [In] int EventCode,
            [In] IntPtr Param1,
            [In] IntPtr Param2
            );

        [PreserveSig]
        int SetBorderColor([In] int clrBorder);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("a9849bbe-9ec8-4263-b764-62730f0d15d0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurface
    {
        [PreserveSig]
        int IsSurfaceLocked();

        [PreserveSig]
        int LockSurface([Out] out IntPtr lpSurface); // BYTE**

        [PreserveSig]
        int UnlockSurface();

        [PreserveSig]
        int GetSurface([Out, MarshalAs(UnmanagedType.Interface)] out object lplpSurface);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("e6f7ce40-4673-44f1-8f77-5499d68cb4ea"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImagePresenterExclModeConfig : IVMRImagePresenterConfig
    {
    #region IVMRImagePresenterConfig Methods

        [PreserveSig]
        new int SetRenderingPrefs([In] VMRRenderPrefs dwRenderFlags);

        [PreserveSig]
        new int GetRenderingPrefs([Out] out VMRRenderPrefs dwRenderFlags);

    #endregion

        [PreserveSig]
        int SetXlcModeDDObjAndPrimarySurface(
            [In] IntPtr lpDDObj,
            [In] IntPtr lpPrimarySurf
            );

        [PreserveSig]
        int GetXlcModeDDObjAndPrimarySurface(
            [Out] out IntPtr lpDDObj,
            [Out] out IntPtr lpPrimarySurf
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("9e5530c5-7034-48b4-bb46-0b8a6efc8e36"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRFilterConfig
    {
        [PreserveSig]
        int SetImageCompositor([In] IVMRImageCompositor lpVMRImgCompositor);

        [PreserveSig]
        int SetNumberOfStreams([In] int dwMaxStreams);

        [PreserveSig]
        int GetNumberOfStreams([Out] out int pdwMaxStreams);

        [PreserveSig]
        int SetRenderingPrefs([In] VMRRenderPrefs dwRenderFlags);

        [PreserveSig]
        int GetRenderingPrefs([Out] out VMRRenderPrefs pdwRenderFlags);

        [PreserveSig]
        int SetRenderingMode([In] VMRMode Mode);

        [PreserveSig]
        int GetRenderingMode([Out] out VMRMode Mode);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("0eb1088c-4dcd-46f0-878f-39dae86a51b7"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRWindowlessControl
    {
        [PreserveSig]
        int GetNativeVideoSize(
            [Out] out int lpWidth,
            [Out] out int lpHeight,
            [Out] out int lpARWidth,
            [Out] out int lpARHeight
        );

        [PreserveSig]
        int GetMinIdealVideoSize(
            [Out] out int lpWidth,
            [Out] out int lpHeight
        );

        [PreserveSig]
        int GetMaxIdealVideoSize(
            [Out] out int lpWidth,
            [Out] out int lpHeight
        );

        [PreserveSig]
        int SetVideoPosition(
            [In] DsRect lpSRCRect,
            [In] DsRect lpDSTRect
        );

        [PreserveSig]
        int GetVideoPosition(
            [Out] DsRect lpSRCRect,
            [Out] DsRect lpDSTRect
        );

        [PreserveSig]
        int GetAspectRatioMode([Out] out VMRAspectRatioMode lpAspectRatioMode);

        [PreserveSig]
        int SetAspectRatioMode([In] VMRAspectRatioMode AspectRatioMode);

        [PreserveSig]
        int SetVideoClippingWindow([In] IntPtr hwnd); // HWND

        [PreserveSig]
        int RepaintVideo(
            [In] IntPtr hwnd, // HWND
            [In] IntPtr hdc // HDC
        );

        [PreserveSig]
        int DisplayModeChanged();

        /// <summary>
        /// the caller is responsible for free the returned memory by calling CoTaskMemFree.
        /// </summary>
        [PreserveSig]
        int GetCurrentImage([Out] out IntPtr lpDib); // BYTE**

        [PreserveSig]
        int SetBorderColor([In] int Clr);

        [PreserveSig]
        int GetBorderColor([Out] out int lpClr);

        [PreserveSig]
        int SetColorKey([In] int Clr);

        [PreserveSig]
        int GetColorKey([Out] out int lpClr);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("ede80b5c-bad6-4623-b537-65586c9f8dfd"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRAspectRatioControl
    {
        [PreserveSig]
        int GetAspectRatioMode([Out] out VMRAspectRatioMode lpdwARMode);

        [PreserveSig]
        int SetAspectRatioMode([In] VMRAspectRatioMode lpdwARMode);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("bb057577-0db8-4e6a-87a7-1a8c9a505a0f"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRDeinterlaceControl
    {
        [PreserveSig]
        int GetNumberOfDeinterlaceModes(
            [In] ref VMRVideoDesc lpVideoDescription,
            [In, Out] ref int lpdwNumDeinterlaceModes,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] lpDeinterlaceModes
        );

        [PreserveSig]
        int GetDeinterlaceModeCaps(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid lpDeinterlaceMode,
            [In] ref VMRVideoDesc lpVideoDescription,
            [In, Out] ref VMRDeinterlaceCaps lpDeinterlaceCaps
        );

        [PreserveSig]
        int GetDeinterlaceMode(
            [In] int dwStreamID,
            [Out] out Guid lpDeinterlaceMode
        );

        [PreserveSig]
        int SetDeinterlaceMode(
            [In] int dwStreamID,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid lpDeinterlaceMode
        );

        [PreserveSig]
        int GetDeinterlacePrefs([Out] out VMRDeinterlacePrefs lpdwDeinterlacePrefs);

        [PreserveSig]
        int SetDeinterlacePrefs([In] VMRDeinterlacePrefs lpdwDeinterlacePrefs);

        [PreserveSig]
        int GetActualDeinterlaceMode(
            [In] int dwStreamID,
            [Out] out Guid lpDeinterlaceMode
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("7a4fb5af-479f-4074-bb40-ce6722e43c82"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImageCompositor
    {
        [PreserveSig]
        int InitCompositionTarget(
            [In] IntPtr pD3DDevice,
            [In] IntPtr pddsRenderTarget
        );

        [PreserveSig]
        int TermCompositionTarget(
            [In] IntPtr pD3DDevice,
            [In] IntPtr pddsRenderTarget
        );

        [PreserveSig]
        int SetStreamMediaType(
            [In] int dwStrmID,
            [In] AMMediaType pmt,
            [In, MarshalAs(UnmanagedType.Bool)] bool fTexture
        );

        [PreserveSig]
        int CompositeImage(
            [In] IntPtr pD3DDevice,
            [In] IntPtr pddsRenderTarget,
            [In] AMMediaType pmtRenderTarget,
            [In] long rtStart,
            [In] long rtEnd,
            [In] int dwClrBkGnd,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 7)] VMRVideoStreamInfo[] pVideoStreamInfo,
            [In] int cStreams
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("9f3a1c85-8555-49ba-935f-be5b5b29d178"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImagePresenterConfig
    {
        [PreserveSig]
        int SetRenderingPrefs([In] VMRRenderPrefs dwRenderFlags);

        [PreserveSig]
        int GetRenderingPrefs([Out] out VMRRenderPrefs dwRenderFlags);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("1E673275-0257-40aa-AF20-7C608D4A0428"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMixerBitmap
    {
        [PreserveSig]
        int SetAlphaBitmap([In] ref VMRAlphaBitmap pBmpParms);

        [PreserveSig]
        int UpdateAlphaBitmapParameters([In] ref VMRAlphaBitmap pBmpParms);

        [PreserveSig]
        int GetAlphaBitmapParameters([Out] out VMRAlphaBitmap pBmpParms);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("9cf0b1b6-fbaa-4b7f-88cf-cf1f130a0dce"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMonitorConfig
    {
        [PreserveSig]
        int SetMonitor([In] ref VMRGuid pGUID);

        [PreserveSig]
        int GetMonitor([Out] out VMRGuid pGUID);

        [PreserveSig]
        int SetDefaultMonitor([In] ref VMRGuid pGUID);

        [PreserveSig]
        int GetDefaultMonitor([Out] out VMRGuid pGUID);

        [PreserveSig]
        int GetAvailableMonitors(
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] VMRMonitorInfo[] pInfo,
            [In] int dwMaxInfoArraySize,
            [Out] out int pdwNumDevices
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("058d1f11-2a54-4bef-bd54-df706626b727"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRVideoStreamControl
    {
        [PreserveSig]
        int SetColorKey([In] ref DDColorKey lpClrKey);

        [PreserveSig]
        int GetColorKey([Out] out DDColorKey lpClrKey);

        [PreserveSig]
        int SetStreamActiveState([In, MarshalAs(UnmanagedType.Bool)] bool fActive);

        [PreserveSig]
        int GetStreamActiveState([Out, MarshalAs(UnmanagedType.Bool)] out bool fActive);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("1c1a17b0-bed0-415d-974b-dc6696131599"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMixerControl
    {
        [PreserveSig]
        int SetAlpha(
            [In] int dwStreamID,
            [In] float Alpha
        );

        [PreserveSig]
        int GetAlpha(
            [In] int dwStreamID,
            [Out] out float Alpha
        );

        [PreserveSig]
        int SetZOrder(
            [In] int dwStreamID,
            [In] int dwZ
        );

        [PreserveSig]
        int GetZOrder(
            [In] int dwStreamID,
            [Out] out int dwZ
        );

        [PreserveSig]
        int SetOutputRect(
            [In] int dwStreamID,
            [In] ref NormalizedRect pRect
        );

        [PreserveSig]
        int GetOutputRect(
            [In] int dwStreamID,
            [Out] out NormalizedRect pRect
        );

        [PreserveSig]
        int SetBackgroundClr([In] int ClrBkg);

        [PreserveSig]
        int GetBackgroundClr([Out] out int ClrBkg);

        [PreserveSig]
        int SetMixingPrefs([In] VMRMixerPrefs dwMixerPrefs);

        [PreserveSig]
        int GetMixingPrefs([Out] out VMRMixerPrefs dwMixerPrefs);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("aac18c18-e186-46d2-825d-a1f8dc8e395a"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVPManager
    {
        [PreserveSig]
        int SetVideoPortIndex([In] int dwVideoPortIndex);

        [PreserveSig]
        int GetVideoPortIndex([Out] out int dwVideoPortIndex);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("36d39eb0-dd75-11ce-bf0e-00aa0055595a"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDirectDrawVideo
    {
        [PreserveSig]
        int GetSwitches(out int pSwitches);

        [PreserveSig]
        int SetSwitches(int Switches);

        [PreserveSig]
        int GetCaps(out IntPtr pCaps); // DDCAPS

        [PreserveSig]
        int GetEmulatedCaps(out IntPtr pCaps); // DDCAPS

        [PreserveSig]
        int GetSurfaceDesc(out IntPtr pSurfaceDesc); // DDSURFACEDESC

        [PreserveSig]
        int GetFourCCCodes(out int pCount, out int pCodes);

        [PreserveSig]
        int SetDirectDraw(IntPtr pDirectDraw); // LPDIRECTDRAW

        [PreserveSig]
        int GetDirectDraw(out IntPtr ppDirectDraw); // LPDIRECTDRAW

        [PreserveSig]
        int GetSurfaceType(out DirectDrawSwitches pSurfaceType);

        [PreserveSig]
        int SetDefault();

        [PreserveSig]
        int UseScanLine(int UseScanLine);

        [PreserveSig]
        int CanUseScanLine(out int UseScanLine);

        [PreserveSig]
        int UseOverlayStretch(int UseOverlayStretch);

        [PreserveSig]
        int CanUseOverlayStretch(out int UseOverlayStretch);

        [PreserveSig]
        int UseWhenFullScreen(int UseWhenFullScreen);

        [PreserveSig]
        int WillUseFullScreen(out int UseWhenFullScreen);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("dd1d7110-7836-11cf-bf47-00aa0055595a"),
     Obsolete("This interface has been deprecated.", false),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFullScreenVideo
    {
        [PreserveSig]
        int CountModes(out int pModes);

        [PreserveSig]
        int GetModeInfo(int Mode, out int pWidth, out int pHeight, out int pDepth);

        [PreserveSig]
        int GetCurrentMode(out int pMode);

        [PreserveSig]
        int IsModeAvailable(int Mode);

        [PreserveSig]
        int IsModeEnabled(int Mode);

        [PreserveSig]
        int SetEnabled(int Mode, int bEnabled);

        [PreserveSig]
        int GetClipFactor(out int pClipFactor);

        [PreserveSig]
        int SetClipFactor(int ClipFactor);

        [PreserveSig]
        int SetMessageDrain(IntPtr hwnd);

        [PreserveSig]
        int GetMessageDrain(out IntPtr hwnd);

        [PreserveSig]
        int SetMonitor(int Monitor);

        [PreserveSig]
        int GetMonitor(out int Monitor);

        [PreserveSig]
        int HideOnDeactivate(int Hide);

        [PreserveSig]
        int IsHideOnDeactivate();

        [PreserveSig]
        int SetCaption([MarshalAs(UnmanagedType.BStr)] string strCaption);

        [PreserveSig]
        int GetCaption([MarshalAs(UnmanagedType.BStr)] out string pstrCaption);

        [PreserveSig]
        int SetDefault();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("53479470-f1dd-11cf-bc42-00aa00ac74f6"),
     Obsolete("This interface has been deprecated.", false),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFullScreenVideoEx : IFullScreenVideo
    {
        #region IFullScreenVideo methods

        [PreserveSig]
        new int CountModes(out int pModes);

        [PreserveSig]
        new int GetModeInfo(int Mode, out int pWidth, out int pHeight, out int pDepth);

        [PreserveSig]
        new int GetCurrentMode(out int pMode);

        [PreserveSig]
        new int IsModeAvailable(int Mode);

        [PreserveSig]
        new int IsModeEnabled(int Mode);

        [PreserveSig]
        new int SetEnabled(int Mode, int bEnabled);

        [PreserveSig]
        new int GetClipFactor(out int pClipFactor);

        [PreserveSig]
        new int SetClipFactor(int ClipFactor);

        [PreserveSig]
        new int SetMessageDrain(IntPtr hwnd);

        [PreserveSig]
        new int GetMessageDrain(out IntPtr hwnd);

        [PreserveSig]
        new int SetMonitor(int Monitor);

        [PreserveSig]
        new int GetMonitor(out int Monitor);

        [PreserveSig]
        new int HideOnDeactivate(int Hide);

        [PreserveSig]
        new int IsHideOnDeactivate();

        [PreserveSig]
        new int SetCaption([MarshalAs(UnmanagedType.BStr)] string strCaption);

        [PreserveSig]
        new int GetCaption([MarshalAs(UnmanagedType.BStr)] out string pstrCaption);

        [PreserveSig]
        new int SetDefault();

        #endregion

        [PreserveSig]
        int SetAcceleratorTable(IntPtr hwnd, IntPtr hAccel); // HACCEL

        [PreserveSig]
        int GetAcceleratorTable(out IntPtr phwnd, out IntPtr phAccel); // HACCEL

        [PreserveSig]
        int KeepPixelAspectRatio(int KeepAspect);

        [PreserveSig]
        int IsKeepPixelAspectRatio(out int pKeepAspect);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("61ded640-e912-11ce-a099-00aa00479a58"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseVideoMixer
    {
        [PreserveSig]
        int SetLeadPin(int iPin);

        [PreserveSig]
        int GetLeadPin(out int piPin);

        [PreserveSig]
        int GetInputPinCount(out int piPinCount);

        [PreserveSig]
        int IsUsingClock(out int pbValue);

        [PreserveSig]
        int SetUsingClock(int bValue);

        [PreserveSig]
        int GetClockPeriod(out int pbValue);

        [PreserveSig]
        int SetClockPeriod(int bValue);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("1bd0ecb0-f8e2-11ce-aac6-0020af0b99a3"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IQualProp
    {
        [PreserveSig]
        int get_FramesDroppedInRenderer(out int pcFrames);

        [PreserveSig]
        int get_FramesDrawn(out int pcFramesDrawn);

        [PreserveSig]
        int get_AvgFrameRate(out int piAvgFrameRate);

        [PreserveSig]
        int get_Jitter(out int iJitter);

        [PreserveSig]
        int get_AvgSyncOffset(out int piAvg);

        [PreserveSig]
        int get_DevSyncOffset(out int piDev);
    }

    #endregion
}