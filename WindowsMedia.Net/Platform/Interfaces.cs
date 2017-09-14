using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;

namespace WindowsMedia.Platform
{
    #region Interfaces

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("68961E68-832B-41ea-BC91-63593F3E70E3"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample2Config
    {
        [PreserveSig]
        int GetSurface(
            [MarshalAs(UnmanagedType.IUnknown)] out object ppDirect3DSurface9
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("36b73885-c2c8-11cf-8b46-00805f6cef60"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClock2 : IReferenceClock
    {
        #region IReferenceClock Methods

        [PreserveSig]
        new int GetTime([Out] out long pTime);

        [PreserveSig]
        new int AdviseTime(
            [In] long baseTime,
            [In] long streamTime,
            [In] IntPtr hEvent, // System.Threading.WaitHandle?
            [Out] out int pdwAdviseCookie
        );

        [PreserveSig]
        new int AdvisePeriodic(
            [In] long startTime,
            [In] long periodTime,
            [In] IntPtr hSemaphore, // System.Threading.WaitHandle?
            [Out] out int pdwAdviseCookie
        );

        [PreserveSig]
        new int Unadvise([In] int dwAdviseCookie);

        #endregion
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a8689d-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemInputPin
    {
        [PreserveSig]
        int GetAllocator([Out] out IMemAllocator ppAllocator);

        [PreserveSig]
        int NotifyAllocator(
            [In] IMemAllocator pAllocator,
            [In, MarshalAs(UnmanagedType.Bool)] bool bReadOnly
        );

        [PreserveSig]
        int GetAllocatorRequirements([Out] out AllocatorProperties pProps);

        [PreserveSig]
        int Receive([In] IMediaSample pSample);

        [PreserveSig]
        int ReceiveMultiple(
            [In] IntPtr pSamples, // IMediaSample[]
            [In] int nSamples,
            [Out] out int nSamplesProcessed
        );

        [PreserveSig]
        int ReceiveCanBlock();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("a3d8cec0-7e5a-11cf-bbc5-00805f6cef20"),
     Obsolete("This interface has been deprecated.", false),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMovieSetup
    {
        [PreserveSig]
        int Register();

        [PreserveSig]
        int Unregister();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a86891-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPin
    {
        [PreserveSig]
        int Connect(
            [In] IPin pReceivePin,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
        );

        [PreserveSig]
        int ReceiveConnection(
            [In] IPin pReceivePin,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
        );

        [PreserveSig]
        int Disconnect();

        [PreserveSig]
        int ConnectedTo(
            [Out] out IPin ppPin);

        /// <summary>
        /// Release returned parameter with DsUtils.FreeAMMediaType
        /// </summary>
        [PreserveSig]
        int ConnectionMediaType(
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        /// <summary>
        /// Release returned parameter with DsUtils.FreePinInfo
        /// </summary>
        [PreserveSig]
        int QueryPinInfo([Out] out PinInfo pInfo);

        [PreserveSig]
        int QueryDirection(out PinDirection pPinDir);

        [PreserveSig]
        int QueryId([Out, MarshalAs(UnmanagedType.LPWStr)] out string id);

        [PreserveSig]
        int QueryAccept([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        [PreserveSig]
        int EnumMediaTypes([Out] out IEnumMediaTypes @enum);

        [PreserveSig]
        int QueryInternalConnections(
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] IPin[] pins,
            [In, Out] ref int numberOfPins
        );

        [PreserveSig]
        int EndOfStream();

        [PreserveSig]
        int BeginFlush();

        [PreserveSig]
        int EndFlush();

        [PreserveSig]
        int NewSegment(
            [In] long tStart,
            [In] long tStop,
            [In] double dRate
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("36b73880-c2c8-11cf-8b46-00805f6cef60"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSeeking
    {
        [PreserveSig]
        int GetCapabilities([Out] out AMSeekingSeekingCapabilities pCapabilities);

        [PreserveSig]
        int CheckCapabilities([In, Out] ref AMSeekingSeekingCapabilities pCapabilities);

        [PreserveSig]
        int IsFormatSupported([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);

        [PreserveSig]
        int QueryPreferredFormat([Out] out Guid format);

        [PreserveSig]
        int GetTimeFormat([Out] out Guid format);

        [PreserveSig]
        int IsUsingTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid format);

        [PreserveSig]
        int SetTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid format);

        [PreserveSig]
        int GetDuration([Out] out long duration);

        [PreserveSig]
        int GetStopPosition([Out] out long stop);

        [PreserveSig]
        int GetCurrentPosition([Out] out long current);

        [PreserveSig]
        int ConvertTimeFormat(
            [Out] out long target,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid targetFormat,
            [In] long source,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid sourceFormat
        );

        [PreserveSig]
        int SetPositions(
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pCurrent,
            [In] AMSeekingSeekingFlags dwCurrentFlags,
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pStop,
            [In] AMSeekingSeekingFlags dwStopFlags
        );

        [PreserveSig]
        int GetPositions(
            [Out] out long pCurrent,
            [Out] out long pStop
        );

        [PreserveSig]
        int GetAvailable(
            [Out] out long pEarliest,
            [Out] out long pLatest
        );

        [PreserveSig]
        int SetRate([In] double dRate);

        [PreserveSig]
        int GetRate([Out] out double pdRate);

        [PreserveSig]
        int GetPreroll([Out] out long pllPreroll);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a8689a-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample
    {
        [PreserveSig]
        int GetPointer([Out] out IntPtr ppBuffer); // BYTE **

        [PreserveSig]
        int GetSize();

        [PreserveSig]
        int GetTime(
            [Out] out long pTimeStart,
            [Out] out long pTimeEnd
        );

        [PreserveSig]
        int SetTime(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeStart,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeEnd
        );

        [PreserveSig]
        int IsSyncPoint();

        [PreserveSig]
        int SetSyncPoint([In, MarshalAs(UnmanagedType.Bool)] bool bIsSyncPoint);

        [PreserveSig]
        int IsPreroll();

        [PreserveSig]
        int SetPreroll([In, MarshalAs(UnmanagedType.Bool)] bool bIsPreroll);

        [PreserveSig]
        int GetActualDataLength();

        [PreserveSig]
        int SetActualDataLength([In] int len);

        /// <summary>
        /// Returned object must be released with DsUtils.FreeAMMediaType()
        /// </summary>
        [PreserveSig]
        int GetMediaType([Out, MarshalAs(UnmanagedType.LPStruct)] out AMMediaType ppMediaType);

        [PreserveSig]
        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pMediaType);

        [PreserveSig]
        int IsDiscontinuity();

        [PreserveSig]
        int SetDiscontinuity([In, MarshalAs(UnmanagedType.Bool)] bool bDiscontinuity);

        [PreserveSig]
        int GetMediaTime(
            [Out] out long pTimeStart,
            [Out] out long pTimeEnd
        );

        [PreserveSig]
        int SetMediaTime(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeStart,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeEnd
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a86899-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaFilter : IPersist
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID(
            [Out] out Guid classId);

        #endregion

        [PreserveSig]
        int Stop();

        [PreserveSig]
        int Pause();

        [PreserveSig]
        int Run([In] long startTime);

        [PreserveSig]
        int GetState(
            [In] int dwMilliSecsTimeout,
            [Out] out FilterState filtState
        );

        [PreserveSig]
        int SetSyncSource([In] IReferenceClock pClock);

        [PreserveSig]
        int GetSyncSource([Out] out IReferenceClock pClock);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("FA993888-4383-415A-A930-DD472A8CF6F7")]
    public interface IMFGetService
    {
        void GetService(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppvObject
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a86895-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseFilter : IMediaFilter
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID(
            [Out] out Guid classId);

        #endregion

        #region IMediaFilter Methods

        [PreserveSig]
        new int Stop();

        [PreserveSig]
        new int Pause();

        [PreserveSig]
        new int Run(long startTime);

        [PreserveSig]
        new int GetState([In] int milliSecsTimeout, [Out] out FilterState filtState);

        [PreserveSig]
        new int SetSyncSource([In] IReferenceClock clock);

        [PreserveSig]
        new int GetSyncSource([Out] out IReferenceClock clock);

        #endregion

        [PreserveSig]
        int EnumPins([Out] out IEnumPins @enum);

        [PreserveSig]
        int FindPin(
            [In, MarshalAs(UnmanagedType.LPWStr)] string id,
            [Out] out IPin pin
        );

        [PreserveSig]
        int QueryFilterInfo([Out] out FilterInfo info);

        [PreserveSig]
        int JoinFilterGraph(
            [In] IFilterGraph graph,
            [In, MarshalAs(UnmanagedType.LPWStr)] string name
        );

        [PreserveSig]
        int QueryVendorInfo([Out, MarshalAs(UnmanagedType.LPWStr)] out string vendorInfo);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph
    {
        [PreserveSig]
        int AddFilter(
            [In] IBaseFilter filter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string name
        );

        [PreserveSig]
        int RemoveFilter([In] IBaseFilter pFilter);

        [PreserveSig]
        int EnumFilters([Out] out IEnumFilters ppEnum);

        [PreserveSig]
        int FindFilterByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [Out] out IBaseFilter ppFilter
        );

        [PreserveSig]
        int ConnectDirect(
            [In] IPin ppinOut,
            [In] IPin ppinIn,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
        );

        [PreserveSig]
        [Obsolete("This method is obsolete; use the IFilterGraph2.ReconnectEx method instead.")]
        int Reconnect([In] IPin ppin);

        [PreserveSig]
        int Disconnect([In] IPin ppin);

        [PreserveSig]
        int SetDefaultSyncSource();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a86893-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumFilters
    {
        [PreserveSig]
        int Next(
            [In] int cFilters,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IBaseFilter[] ppFilter,
            [Out] out int pcFetched
        );

        [PreserveSig]
        int Skip([In] int cFilters);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumFilters ppEnum);
    }

    /// <summary>
    /// Enumerates pins on a filter.
    /// </summary>
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a86892-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumPins
    {
        /// <summary>
        /// The Next method retrieves a specified number of pins in the enumeration sequence.
        /// </summary>
        /// <param name="cPins">Number of pins to retrieve.</param>
        /// <param name="ppPins">Array of size cPins that is filled with IPin pointers. The caller must release the interfaces.</param>
        /// <param name="pcFetched">Pointer to a variable that receives the number of pins retrieved.</param>
        /// <returns>Returns one of the following HRESULT values.
        /// <b>S_FALSE</b> - Did not retrieve as many pins as requested.
        /// <b>S_OK</b> - Success.
        /// <b>E_INVALIDARG</b> - Invalid argument.
        /// <b>E_POINTER</b> - <b>null</b> pointer argument.
        /// <b>VFW_E_ENUM_OUT_OF_SYNC</b> - The filter's state has changed and is now inconsistent with the enumerator.
        /// </returns>
        /// <remarks>
        /// This method retrieves pointers to the specified number of pins, starting at the current position in the enumeration, and places them in the 
        /// specified array. If the method succeeds, the IPin pointers all have outstanding reference counts.Be sure to release them when you are done.
        /// If the number of pins changes, the enumerator is no longer consistent with the filter, and the method returns VFW_E_ENUM_OUT_OF_SYNC.
        /// Discard any data obtained from previous calls to the enumerator, because it might be invalid.Update the enumerator by calling the 
        /// <see cref="IEnumPins.Reset"/> method. You can then call the Next method safely.
        /// </remarks>
        [PreserveSig]
        int Next(
            [In] int cPins,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IPin[] ppPins,
            [Out] out int pcFetched
        );

        /// <summary>
        /// The Skip method skips over a specified number of pins.
        /// </summary>
        /// <param name="cPins">Number of pins to skip.</param>
        /// <returns>Returns one of the following HRESULT
        /// <b>S_FALSE</b> - Skipped past the end of the sequence.
        /// <b>S_OK</b> - Success.
        /// <b>VFW_E_ENUM_OUT_OF_SYNC</b> - The filter's state has changed and is now inconsistent with the enumerator.
        /// </returns>
        [PreserveSig]
        int Skip([In] int cPins);

        /// <summary>
        /// The Reset method resets the enumeration sequence to the beginning.
        /// </summary>
        /// <returns>Returns S_OK.</returns>
        [PreserveSig]
        int Reset();

        /// <summary>
        /// The Clone method makes a copy of the enumerator with the same enumeration state.
        /// </summary>
        /// <param name="ppEnum">Receives a pointer to the <see cref="IEnumPins"/> interface of the new enumerator. The caller must release the interface.</param>
        /// <returns>Returns one of the following HRESULT
        /// <b>S_FALSE</b> - Did not retrieve as many pins as requested.
        /// <b>S_OK</b> - Success.
        /// <b>E_OUTOFMEMORY</b> - Insufficient memory.
        /// <b>E_POINTER</b> - <b>null</b> pointer argument.
        /// <b>VFW_E_ENUM_OUT_OF_SYNC</b> - The filter's state has changed and is now inconsistent with the enumerator.
        /// </returns>
        /// <remarks>
        /// If the number of pins changes, the enumerator is no longer consistent with the filter, and the method returns VFW_E_ENUM_OUT_OF_SYNC. 
        /// Discard any data obtained from previous calls to the enumerator, because it might be invalid. Update the enumerator by calling the 
        /// <see cref="IEnumPins.Reset"/> method. You can then call the Clone method safely.</remarks>
        [PreserveSig]
        int Clone([Out] out IEnumPins ppEnum);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a86897-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClock
    {
        [PreserveSig]
        int GetTime([Out] out long pTime);

        [PreserveSig]
        int AdviseTime(
            [In] long baseTime,
            [In] long streamTime,
            [In] IntPtr hEvent, // System.Threading.WaitHandle?
            [Out] out int pdwAdviseCookie
        );

        [PreserveSig]
        int AdvisePeriodic(
            [In] long startTime,
            [In] long periodTime,
            [In] IntPtr hSemaphore, // System.Threading.WaitHandle?
            [Out] out int pdwAdviseCookie
        );

        [PreserveSig]
        int Unadvise([In] int dwAdviseCookie);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("89c31040-846b-11ce-97d3-00aa0055595a"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumMediaTypes
    {
        [PreserveSig]
        int Next(
            [In] int cMediaTypes,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EMTMarshaler), SizeParamIndex = 0)] AMMediaType[] ppMediaTypes,
            [Out] out int pcFetched
        );

        [PreserveSig]
        int Skip([In] int cMediaTypes);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumMediaTypes ppEnum);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("36b73884-c2c8-11cf-8b46-00805f6cef60"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample2 : IMediaSample
    {
        #region IMediaSample Methods

        [PreserveSig]
        new int GetPointer([Out] out IntPtr ppBuffer); // BYTE **

        [PreserveSig]
        new int GetSize();

        [PreserveSig]
        new int GetTime(
            [Out] out long pTimeStart,
            [Out] out long pTimeEnd
        );

        [PreserveSig]
        new int SetTime(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeStart,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeEnd
        );

        [PreserveSig]
        new int IsSyncPoint();

        [PreserveSig]
        new int SetSyncPoint([In, MarshalAs(UnmanagedType.Bool)] bool bIsSyncPoint);

        [PreserveSig]
        new int IsPreroll();

        [PreserveSig]
        new int SetPreroll([In, MarshalAs(UnmanagedType.Bool)] bool bIsPreroll);

        [PreserveSig]
        new int GetActualDataLength();

        [PreserveSig]
        new int SetActualDataLength([In] int len);

        [PreserveSig]
        new int GetMediaType([Out] out AMMediaType ppMediaType);

        [PreserveSig]
        new int SetMediaType([In] AMMediaType pMediaType);

        [PreserveSig]
        new int IsDiscontinuity();

        [PreserveSig]
        new int SetDiscontinuity([In, MarshalAs(UnmanagedType.Bool)] bool bDiscontinuity);

        [PreserveSig]
        new int GetMediaTime(
            [Out] out long pTimeStart,
            [Out] out long pTimeEnd
        );

        [PreserveSig]
        new int SetMediaTime(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeStart,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeEnd
        );

        #endregion

        [PreserveSig]
        int GetProperties(
            [In] int cbProperties,
            [In] IntPtr pbProperties // BYTE *
        );

        [PreserveSig]
        int SetProperties(
            [In] int cbProperties,
            [In] IntPtr pbProperties // BYTE *
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("92980b30-c1de-11d2-abf5-00a0c905f375"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemAllocatorNotifyCallbackTemp
    {
        [PreserveSig]
        int NotifyRelease();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("379a0cf0-c1de-11d2-abf5-00a0c905f375"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemAllocatorCallbackTemp : IMemAllocator
    {
        #region IMemAllocator Methods

        [PreserveSig]
        new int SetProperties(
            [In] AllocatorProperties pRequest,
            [Out, MarshalAs(UnmanagedType.LPStruct)] AllocatorProperties pActual
        );

        [PreserveSig]
        new int GetProperties([Out] AllocatorProperties pProps);

        [PreserveSig]
        new int Commit();

        [PreserveSig]
        new int Decommit();

        [PreserveSig]
        new int GetBuffer(
            [Out] out IMediaSample ppBuffer,
            [In] long pStartTime,
            [In] long pEndTime,
            [In] AMGBF dwFlags
        );

        [PreserveSig]
        new int ReleaseBuffer([In] IMediaSample pBuffer);

        #endregion

        [PreserveSig]
        int SetNotify([In] IMemAllocatorNotifyCallbackTemp pNotify);

        [PreserveSig]
        int GetFreeCount([Out] out int plBuffersFree);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("56a8689c-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemAllocator
    {
        [PreserveSig]
        int SetProperties(
            [In, MarshalAs(UnmanagedType.LPStruct)] AllocatorProperties pRequest,
            [Out, MarshalAs(UnmanagedType.LPStruct)] AllocatorProperties pActual
        );

        [PreserveSig]
        int GetProperties(
            [Out, MarshalAs(UnmanagedType.LPStruct)] AllocatorProperties pProps
        );

        [PreserveSig]
        int Commit();

        [PreserveSig]
        int Decommit();

        [PreserveSig]
        int GetBuffer(
            [Out] out IMediaSample ppBuffer,
            [In] long pStartTime,
            [In] long pEndTime,
            [In] AMGBF dwFlags
        );

        [PreserveSig]
        int ReleaseBuffer(
            [In] IMediaSample pBuffer
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("ebec459c-2eca-4d42-a8af-30df557614b8"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClockTimerControl
    {
        [PreserveSig]
        int SetDefaultTimerResolution(
            long timerResolution
        );

        [PreserveSig]
        int GetDefaultTimerResolution(
            out long pTimerResolution
        );
    }
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("6B652FFF-11FE-4fce-92AD-0266B5D7C78F"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabber
    {
        [PreserveSig]
        int SetOneShot(
            [In, MarshalAs(UnmanagedType.Bool)] bool oneShot);

        [PreserveSig]
        int SetMediaType(
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        [PreserveSig]
        int GetConnectedMediaType(
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        [PreserveSig]
        int SetBufferSamples(
            [In, MarshalAs(UnmanagedType.Bool)] bool bufferThem);

        [PreserveSig]
        int GetCurrentBuffer(ref int pBufferSize, IntPtr pBuffer);

        [PreserveSig]
        int GetCurrentSample(out IMediaSample sample);

        [PreserveSig]
        int SetCallback(ISampleGrabberCB callback, int whichMethodToCallback);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("0579154A-2B53-4994-B0D0-E773148EFF85"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabberCB
    {
        /// <summary>
        /// When called, callee must release pSample
        /// </summary>
        [PreserveSig]
        int SampleCB(double sampleTime, IMediaSample sample);

        [PreserveSig]
        int BufferCB(double sampleTime, IntPtr buffer, int bufferLen);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("D8D715A0-6E5E-11D0-B3F0-00AA003761C5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMVfwCaptureDialogs
    {
        [PreserveSig]
        int HasDialog([In] VfwCaptureDialogs iDialog);

        [PreserveSig]
        int ShowDialog(
            [In] VfwCaptureDialogs iDialog,
            [In] IntPtr hwnd // HWND *
            );

        [PreserveSig]
        int SendDriverMessage(
            [In] VfwCaptureDialogs iDialog,
            [In] int uMsg,
            [In] int dw1,
            [In] int dw2
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a4-0ad4-11ce-b03a-0020af0ba770"),
    Obsolete("This interface has been deprecated.  Use IFilterMapper2::EnumMatchingFilters", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumRegFilters
    {
        [PreserveSig]
        int Next(
            [In] int cFilters,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] RegFilter[] apRegFilter,
            [In] IntPtr pcFetched
            );

        [PreserveSig]
        int Skip([In] int cFilters);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumRegFilters ppEnum);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a3-0ad4-11ce-b03a-0020af0ba770"),
    Obsolete("This interface has been deprecated.  Use IFilterMapper2.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterMapper
    {
        [PreserveSig]
        int RegisterFilter(
            [In] Guid clsid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Name,
            [In] Merit dwMerit
            );

        [PreserveSig]
        int RegisterFilterInstance(
            [In] Guid clsid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Name,
            [Out] out Guid MRId
            );

        [PreserveSig]
        int RegisterPin(
            [In] Guid Filter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Name,
            [In, MarshalAs(UnmanagedType.Bool)] bool bRendered,
            [In, MarshalAs(UnmanagedType.Bool)] bool bOutput,
            [In, MarshalAs(UnmanagedType.Bool)] bool bZero,
            [In, MarshalAs(UnmanagedType.Bool)] bool bMany,
            [In] Guid ConnectsToFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string ConnectsToPin
            );

        [PreserveSig]
        int RegisterPinType(
            [In] Guid clsFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strName,
            [In] Guid clsMajorType,
            [In] Guid clsSubType
            );

        [PreserveSig]
        int UnregisterFilter([In] Guid Filter);

        [PreserveSig]
        int UnregisterFilterInstance([In] Guid MRId);

        [PreserveSig]
        int UnregisterPin(
            [In] Guid Filter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Name
            );

        [PreserveSig]
        int EnumMatchingFilters(
            [Out] out IEnumRegFilters ppEnum,
            [In] Merit dwMerit,
            [In, MarshalAs(UnmanagedType.Bool)] bool bInputNeeded,
            [In] Guid clsInMaj,
            [In] Guid clsInSub,
            [In, MarshalAs(UnmanagedType.Bool)] bool bRender,
            [In, MarshalAs(UnmanagedType.Bool)] bool bOututNeeded,
            [In] Guid clsOutMaj,
            [In] Guid clsOutSub
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a0-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOverlayNotify
    {
        [PreserveSig]
        int OnPaletteChange(
            [In] int dwColors,
            [In] IntPtr pPalette // PALETTEENTRY *
            );

        [PreserveSig]
        int OnClipChange(
            [In] Rectangle pSourceRect,
            [In] Rectangle pDestinationRect,
            [In] RgnData pRgnData
            );

        [PreserveSig]
        int OnColorKeyChange([In] ColorKey pColorKey);

        [PreserveSig]
        int OnPositionChange(
            [In] Rectangle pSourceRect,
            [In] Rectangle pDestinationRect
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("680EFA10-D535-11D1-87C8-00A0C9223196"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOverlayNotify2 : IOverlayNotify
    {
        #region IOverlayNotify Methods

        [PreserveSig]
        new int OnPaletteChange(
            [In] int dwColors,
            [In] IntPtr pPalette // PALETTEENTRY *
            );

        [PreserveSig]
        new int OnClipChange(
            [In] Rectangle pSourceRect,
            [In] Rectangle pDestinationRect,
            [In] RgnData pRgnData
            );

        [PreserveSig]
        new int OnColorKeyChange([In] ColorKey pColorKey);

        [PreserveSig]
        new int OnPositionChange(
            [In] Rectangle pSourceRect,
            [In] Rectangle pDestinationRect
            );

        #endregion

        [PreserveSig]
        int OnDisplayChange(IntPtr hMonitor); // HMONITOR
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a1-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOverlay
    {
        [PreserveSig]
        int GetPalette(
            [Out] out int pdwColors,
            [Out] out IntPtr ppPalette // PALETTEENTRY **
            );

        [PreserveSig]
        int SetPalette(
            [In] int dwColors,
            [In] IntPtr pPalette // PALETTEENTRY *
            );

        [PreserveSig]
        int GetDefaultColorKey([Out] out ColorKey pColorKey);

        [PreserveSig]
        int GetColorKey([Out] ColorKey pColorKey);

        [PreserveSig]
        int SetColorKey([In] ref ColorKey pColorKey);

        [PreserveSig]
        int GetWindowHandle([Out] out IntPtr pHwnd); // HWND *

        [PreserveSig]
        int GetClipList(
            [Out] out Rectangle pSourceRect,
            [Out] out Rectangle pDestinationRect,
            [Out] out RgnData ppRgnData
            );

        [PreserveSig]
        int GetVideoPosition(
            [Out] out Rectangle pSourceRect,
            [Out] out Rectangle pDestinationRect
            );

        [PreserveSig]
        int Advise(
            [In] IOverlayNotify pOverlayNotify,
            [In] Advise dwInterests
            );

        [PreserveSig]
        int Unadvise();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("bf87b6e0-8c27-11d0-b3f0-00aa003761c5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Obsolete("The ICaptureGraphBuilder interface is deprecated. Use ICaptureGraphBuilder2 instead.", false)]
    public interface ICaptureGraphBuilder
    {
        [PreserveSig]
        int SetFiltergraph([In] IGraphBuilder pfg);

        [PreserveSig]
        int GetFiltergraph([Out] out IGraphBuilder ppfg);

        [PreserveSig]
        int SetOutputFileName(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pType,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile,
            [Out] out IBaseFilter ppbf,
            [Out] out IFileSinkFilter ppSink
            );

        [PreserveSig]
        int FindInterface(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pCategory,
            [In] IBaseFilter pf,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppint
            );

        [PreserveSig]
        int RenderStream(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pCategory,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pSource,
            [In] IBaseFilter pfCompressor,
            [In] IBaseFilter pfRenderer
            );

        [PreserveSig]
        int ControlStream(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pCategory,
            [In] IBaseFilter pFilter,
            [In] long pstart,
            [In] long pstop,
            [In] short wStartCookie,
            [In] short wStopCookie
            );

        [PreserveSig]
        int AllocCapFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile,
            [In] long dwlSize
            );

        [PreserveSig]
        int CopyCaptureFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrOld,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrNew,
            [In] int fAllowEscAbort,
            [In] IAMCopyCaptureFileProgress pFilter
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868bf-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBuilder
    {
        [PreserveSig]
        int Render(
            [In] IPin ppinOut,
            [In] IGraphBuilder pGraph
            );

        [PreserveSig]
        int Backout(
            [In] IPin ppinOut,
            [In] IGraphBuilder pGraph
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868ad-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IResourceConsumer
    {
        [PreserveSig]
        int AcquireResource([In] int idResource);

        [PreserveSig]
        int ReleaseResource([In] int idResource);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868ac-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IResourceManager
    {
        [PreserveSig]
        int Register(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In] int cResource,
            [Out] out int plToken
            );

        [PreserveSig]
        int RegisterGroup(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In] int cResource,
            [In] IntPtr palTokens, // int *
            [Out] out int plToken
            );

        [PreserveSig]
        int RequestResource(
            [In] int idResource,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pFocusObject,
            [In] IResourceConsumer pConsumer
            );

        [PreserveSig]
        int NotifyAcquire(
            [In] int idResource,
            [In] IResourceConsumer pConsumer,
            [In] int hr
            );

        [PreserveSig]
        int NotifyRelease(
            [In] int idResource,
            [In] IResourceConsumer pConsumer,
            [In, MarshalAs(UnmanagedType.Bool)] bool bStillWant
            );

        [PreserveSig]
        int CancelRequest(
            [In] int idResource,
            [In] IResourceConsumer pConsumer
            );

        [PreserveSig]
        int SetFocus([In, MarshalAs(UnmanagedType.IUnknown)] object pFocusObject);

        [PreserveSig]
        int ReleaseFocus([In, MarshalAs(UnmanagedType.IUnknown)] object pFocusObject);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868af-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDistributorNotify
    {
        [PreserveSig]
        int Stop();

        [PreserveSig]
        int Pause();

        [PreserveSig]
        int Run(long tStart);

        [PreserveSig]
        int SetSyncSource([In] IReferenceClock pClock);

        [PreserveSig]
        int NotifyGraphChange();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("211A8765-03AC-11d1-8D13-00AA00BD8339"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBPCSatelliteTuner : IAMTuner
    {
        #region IAMTuner Methods

        [PreserveSig]
        new int put_Channel(
            [In] int lChannel,
            [In] AMTunerSubChannel lVideoSubChannel,
            [In] AMTunerSubChannel lAudioSubChannel
            );

        [PreserveSig]
        new int get_Channel(
            [Out] out int plChannel,
            [Out] out AMTunerSubChannel plVideoSubChannel,
            [Out] out AMTunerSubChannel plAudioSubChannel
            );

        [PreserveSig]
        new int ChannelMinMax(
            [Out] out int lChannelMin,
            [Out] out int lChannelMax
            );

        [PreserveSig]
        new int put_CountryCode([In] int lCountryCode);

        [PreserveSig]
        new int get_CountryCode([Out] out int plCountryCode);

        [PreserveSig]
        new int put_TuningSpace([In] int lTuningSpace);

        [PreserveSig]
        new int get_TuningSpace([Out] out int plTuningSpace);

        [PreserveSig]
        new int Logon([In] IntPtr hCurrentUser); // HANDLE

        [PreserveSig]
        new int Logout();

        [PreserveSig]
        new int SignalPresent([Out] out AMTunerSignalStrength plSignalStrength);

        [PreserveSig]
        new int put_Mode([In] AMTunerModeType lMode);

        [PreserveSig]
        new int get_Mode([Out] out AMTunerModeType plMode);

        [PreserveSig]
        new int GetAvailableModes([Out] out AMTunerModeType plModes);

        [PreserveSig]
        new int RegisterNotificationCallBack(
            [In] IAMTunerNotification pNotify,
            [In] AMTunerEventType lEvents
            );

        [PreserveSig]
        new int UnRegisterNotificationCallBack([In] IAMTunerNotification pNotify);

        #endregion

        [PreserveSig]
        int get_DefaultSubChannelTypes(
            [Out] out int plDefaultVideoType,
            [Out] out int plDefaultAudioType
            );

        [PreserveSig]
        int put_DefaultSubChannelTypes(
            [In] int lDefaultVideoType,
            [In] int lDefaultAudioType
            );

        [PreserveSig]
        int IsTapingPermitted();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("83EC1C33-23D1-11d1-99E6-00A0C9560266"),
    Obsolete("This interface has been deprecated.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTVAudioNotification
    {
        [PreserveSig]
        int OnEvent([In] AMTVAudioEventType Event);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E133B0-30AC-11d0-A18C-00A0C9118956"),
    Obsolete("This interface has been deprecated.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMAnalogVideoEncoder
    {
        [PreserveSig]
        int get_AvailableTVFormats([Out] out AnalogVideoStandard lAnalogVideoStandard);

        [PreserveSig]
        int put_TVFormat([In] AnalogVideoStandard lAnalogVideoStandard);

        [PreserveSig]
        int get_TVFormat([Out] out AnalogVideoStandard plAnalogVideoStandard);

        [PreserveSig]
        int put_CopyProtection([In] VideoCopyProtectionType lVideoCopyProtection);

        [PreserveSig]
        int get_CopyProtection([Out] out VideoCopyProtectionType lVideoCopyProtection);


        [PreserveSig]
        int put_CCEnable([In] int lCCEnable);

        [PreserveSig]
        int get_CCEnable([Out] out int lCCEnable);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("F938C991-3029-11cf-8C44-00AA006B6814"),
    Obsolete("This interface has been deprecated.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMPhysicalPinInfo
    {
        [PreserveSig]
        int GetPhysicalType(
            [Out] out PhysicalConnectorType pType,
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszType
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("9B496CE1-811B-11cf-8C77-00AA006B6814"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimecodeReader
    {
        [PreserveSig]
        int GetTCRMode(
            [In] int Param,
            [Out] out int pValue
            );

        [PreserveSig]
        int SetTCRMode(
            [In] int Param,
            [In] int Value
            );

        [PreserveSig]
        int put_VITCLine([In] int Line);

        [PreserveSig]
        int get_VITCLine([Out] out int pLine);

        [PreserveSig]
        int GetTimecode([Out] out TimeCodeSample pTimecodeSample);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("9B496CE0-811B-11cf-8C77-00AA006B6814"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimecodeGenerator
    {
        [PreserveSig]
        int GetTCGMode(
            [In] int Param,
            [Out] out int pValue
            );

        [PreserveSig]
        int SetTCGMode(
            [In] int Param,
            [In] int Value
            );

        [PreserveSig]
        int put_VITCLine([In] int Line);

        [PreserveSig]
        int get_VITCLine([Out] out int pLine);

        [PreserveSig]
        int SetTimecode([In] TimeCodeSample pTimecodeSample);


        [PreserveSig]
        int GetTimecode([Out] TimeCodeSample pTimecodeSample);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("9B496CE2-811B-11cf-8C77-00AA006B6814"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimecodeDisplay
    {
        [PreserveSig]
        int GetTCDisplayEnable([Out] out int pState);

        [PreserveSig]
        int SetTCDisplayEnable([In] int State);

        [PreserveSig]
        int GetTCDisplay(
            [In] int Param,
            [Out] out int pValue
            );

        [PreserveSig]
        int SetTCDisplay(
            [In] int Param,
            [In] int Value
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("c6545bf0-e76b-11d0-bd52-00a0c911ce86"),
    Obsolete("This interface has been deprecated.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMDevMemoryAllocator
    {
        [PreserveSig]
        int GetInfo(
            [Out] out int pdwcbTotalFree,
            [Out] out int pdwcbLargestFree,
            [Out] out int pdwcbTotalMemory,
            [Out] out int pdwcbMinimumChunk
            );

        [PreserveSig]
        int CheckMemory([In] IntPtr pBuffer); // BYTE *

        [PreserveSig]
        int Alloc(
            [Out] out IntPtr ppBuffer, // BYTE **
            [In, Out] ref int pdwcbBuffer
            );

        [PreserveSig]
        int Free([In] IntPtr pBuffer); // BYTE *

        [PreserveSig]
        int GetDevMemoryObject(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnkInnner,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("c6545bf1-e76b-11d0-bd52-00a0c911ce86"),
    Obsolete("This interface has been deprecated.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMDevMemoryControl
    {
        [PreserveSig]
        int QueryWriteSync();

        [PreserveSig]
        int WriteSync();

        [PreserveSig]
        int GetDevId([Out] out int pdwDevId);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("48efb120-ab49-11d2-aed2-00a0c995e8d5"),
    Obsolete("This interface has been deprecated.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDrawVideoImage
    {
        [PreserveSig]
        int DrawVideoImageBegin();

        [PreserveSig]
        int DrawVideoImageEnd();

        [PreserveSig]
        int DrawVideoImageDraw(
            [In] IntPtr hdc, // HDC
            [In] Rectangle lprcSrc,
            [In] Rectangle lprcDst
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("2e5ea3e0-e924-11d2-b6da-00a0c995e8df"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDecimateVideoImage
    {
        [PreserveSig]
        int SetDecimationImageSize(
            [In] int lWidth,
            [In] int lHeight
            );

        [PreserveSig]
        int ResetDecimationImageSize();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("F185FE76-E64E-11d2-B76E-00C04FB6BD3D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMPushSource
    {
        [PreserveSig]
        int GetPushSourceFlags([Out] out AMPushSourceFlags pFlags);

        [PreserveSig]
        int SetPushSourceFlags([In] AMPushSourceFlags Flags);

        [PreserveSig]
        int SetStreamOffset([In] long rtOffset);

        [PreserveSig]
        int GetStreamOffset([Out] out long prtOffset);

        [PreserveSig]
        int GetMaxStreamOffset([Out] out long prtMaxOffset);

        [PreserveSig]
        int SetMaxStreamOffset([In] long rtMaxOffset);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("f90a6130-b658-11d2-ae49-0000f8754b99"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMDeviceRemoval
    {
        [PreserveSig]
        int DeviceInfo(
            [Out] out Guid pclsidInterfaceClass,
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string pwszSymbolicLink
            );

        [PreserveSig]
        int Reassociate();

        [PreserveSig]
        int Disassociate();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("62EA93BA-EC62-11d2-B770-00C04FB6BD3D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMLatency
    {
        [PreserveSig]
        int GetLatency(out long prtLatency);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("945C1566-6202-46fc-96C7-D87F289C6534"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumStreamIdMap
    {
        [PreserveSig]
        int Next(
            [In] int cRequest,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] StreamIdMap[] pStreamIdMap, // STREAM_ID_MAP *
            [In] IntPtr pcReceived
            );

        [PreserveSig]
        int Skip([In] int cRecords);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumStreamIdMap ppIEnumStreamIdMap);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("901db4c7-31ce-41a2-85dc-8fa0bf41b8da"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICodecAPI
    {
        [PreserveSig]
        int IsSupported(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api
            );

        [PreserveSig]
        int IsModifiable(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api
            );

        [PreserveSig]
        int GetParameterRange(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object ValueMin,
            [Out] out object ValueMax,
            [Out] out object SteppingDelta
            );

        [PreserveSig]
        int GetParameterValues(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object[] Values,
            [Out] out int ValuesCount
            );

        [PreserveSig]
        int GetDefaultValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object Value
            );

        [PreserveSig]
        int GetValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object Value
            );

        [PreserveSig]
        int SetValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [In] ref object Value
            );

        [PreserveSig]
        int RegisterForEvent(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [In] IntPtr userData
            );

        [PreserveSig]
        int UnregisterForEvent(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api
            );

        [PreserveSig]
        int SetAllDefaults();

        [PreserveSig]
        int SetValueWithNotify(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [In] object Value,
            [Out] out Guid[] ChangedParam,
            [Out] out int ChangedParamCount
            );

        [PreserveSig]
        int SetAllDefaultsWithNotify(
            [Out] out Guid[] ChangedParam,
            [Out] out int ChangedParamCount
            );

        [PreserveSig]
        int GetAllSettings([In] IStream pStream);

        [PreserveSig]
        int SetAllSettings([In] IStream pStream);

        [PreserveSig]
        int SetAllSettingsWithNotify(
            [In] IStream pStream,
            [Out, MarshalAs(UnmanagedType.LPStruct)] out Guid[] ChangedParam,
            [Out] out int ChangedParamCount
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("70423839-6ACC-4b23-B079-21DBF08156A5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Obsolete("This interface is deprecated and is maintained for backward compatibility only. New applications and drivers should use the ICodecAPI interface.")]
    public interface IEncoderAPI
    {
        [PreserveSig]
        int IsSupported([In, MarshalAs(UnmanagedType.LPStruct)] Guid Api);

        [PreserveSig]
        int IsAvailable([In, MarshalAs(UnmanagedType.LPStruct)] Guid Api);

        [PreserveSig]
        int GetParameterRange(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object ValueMin,
            [Out] out object ValueMax,
            [Out] out object SteppingDelta
            );

        [PreserveSig]
        int GetParameterValues(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object[] Values,
            [Out] out int ValuesCount
            );

        [PreserveSig]
        int GetDefaultValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object Value
            );

        [PreserveSig]
        int GetValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object Value
            );

        [PreserveSig]
        int SetValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [In] ref object Value
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("02997C3B-8E1B-460e-9270-545E0DE9563E"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#pragma warning disable 612,618
    public interface IVideoEncoder : IEncoderAPI
#pragma warning restore 612,618
    {
        #region IEncoderAPI Methods

        [PreserveSig]
        new int IsSupported([In, MarshalAs(UnmanagedType.LPStruct)] Guid Api);

        [PreserveSig]
        new int IsAvailable([In, MarshalAs(UnmanagedType.LPStruct)] Guid Api);

        [PreserveSig]
        new int GetParameterRange(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object ValueMin,
            [Out] out object ValueMax,
            [Out] out object SteppingDelta
            );

        [PreserveSig]
        new int GetParameterValues(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object[] Values,
            [Out] out int ValuesCount
            );

        [PreserveSig]
        new int GetDefaultValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object Value
            );

        [PreserveSig]
        new int GetValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [Out] out object Value
            );

        [PreserveSig]
        new int SetValue(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Api,
            [In] ref object Value
            );

        #endregion
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("6feded3e-0ff1-4901-a2f1-43f7012c8515"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMCertifiedOutputProtection
    {
        [PreserveSig]
        int KeyExchange(
            [Out] out Guid pRandom,
            [Out] out IntPtr VarLenCertGH, // BYTE **
            [Out] out int pdwLengthCertGH
            );

        [PreserveSig]
        int SessionSequenceStart([In, MarshalAs(UnmanagedType.LPStruct)] AMCOPPSignature pSig);

        [PreserveSig]
        int ProtectionCommand([In, MarshalAs(UnmanagedType.LPStruct)] AMCOPPCommand cmd);

        [PreserveSig]
        int ProtectionStatus(
            [In] AMCOPPStatusInput pStatusInput,
            [Out] out AMCOPPStatusOutput pStatusOutput
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868ab-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphVersion
    {
        [PreserveSig]
        int QueryVersion([Out] out int pVersion);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("b8e8bd60-0bfe-11d0-af91-00aa00b67a42"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IIPDVDec
    {
        [PreserveSig]
        int get_IPDisplay([Out] out DVDecoderResolution displayPix);

        [PreserveSig]
        int put_IPDisplay([In] DVDecoderResolution displayPix);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("92a3a302-da7c-4a1f-ba7e-1802bb5d2d02"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDVSplitter
    {
        [PreserveSig]
        int DiscardAlternateVideoFrames([In, MarshalAs(UnmanagedType.Bool)] bool nDiscard);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("58473A19-2BC8-4663-8012-25F81BABDDD1"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDVRGB219
    {
        [PreserveSig]
        int SetRGB219([In, MarshalAs(UnmanagedType.Bool)] bool bState);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("36b73881-c2c8-11cf-8b46-00805f6cef60"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMStreamControl
    {
        [PreserveSig]
        int StartAt(
            [In] DsLong ptStart,
            [In] int dwCookie
            );

        [PreserveSig]
        int StopAt(
            [In] DsLong ptStop,
            [In, MarshalAs(UnmanagedType.Bool)] bool bSendExtra,
            [In] int dwCookie
            );

        [PreserveSig]
        int GetInfo([Out] out AMStreamInfo pInfo);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("8E1C39A1-DE53-11cf-AA63-0080C744528D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMOpenProgress
    {
        [PreserveSig]
        int QueryProgress(
            [Out] out long pllTotal,
            [Out] out long pllCurrent
            );

        [PreserveSig]
        int AbortOperation();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("2dd74950-a890-11d1-abe8-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMFilterMiscFlags
    {
        [PreserveSig]
        int GetMiscFlags();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("5738E040-B67F-11d0-BD4D-00A0C911CE86"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistMediaPropertyBag : IPersist
    {
        #region IPersist

        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);

        #endregion

        [PreserveSig]
        int InitNew();

        [PreserveSig]
        int Load(
            [In] IMediaPropertyBag pPropBag,
            [In] IErrorLog pErrorLog
            );

        [PreserveSig]
        int Save(
            IMediaPropertyBag pPropBag,
            [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty,
            [In, MarshalAs(UnmanagedType.Bool)] bool fSaveAllProperties
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("6025A880-C0D5-11d0-BD4E-00A0C911CE86"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaPropertyBag : IPropertyBag
    {
        #region IPropertyBag Methods

        [PreserveSig]
        new int Read(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [Out, MarshalAs(UnmanagedType.Struct)] out object pVar,
            [In] IErrorLog pErrorLog
            );

        [PreserveSig]
        new int Write(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In] ref object pVar
            );

        #endregion

        [PreserveSig]
        int EnumProperty(
            [In] int iProperty,
            [Out] out object pvarPropertyName,
            [Out] out object pvarPropertyValue
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("632105FA-072E-11d3-8AF9-00C04FB6BD3D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMGraphStreams
    {
        [PreserveSig]
        int FindUpstreamInterface(
            [In] IPin pPin,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppvInterface,
            [In] AMIntfSearchFlags dwFlags
            );

        [PreserveSig]
        int SyncUsingStreamOffset([In, MarshalAs(UnmanagedType.Bool)] bool bUseStreamOffset);

        [PreserveSig]
        int SetMaxGraphLatency([In] long rtMaxGraphLatency);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a9-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphBuilder : IFilterGraph
    {
        #region IFilterGraph Methods

        [PreserveSig]
        new int AddFilter(
            [In] IBaseFilter pFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName
            );

        [PreserveSig]
        new int RemoveFilter([In] IBaseFilter pFilter);

        [PreserveSig]
        new int EnumFilters([Out] out IEnumFilters ppEnum);

        [PreserveSig]
        new int FindFilterByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        new int ConnectDirect(
            [In] IPin ppinOut,
            [In] IPin ppinIn,
            [In, MarshalAs(UnmanagedType.LPStruct)]
            AMMediaType pmt
            );

        [PreserveSig]
        new int Reconnect([In] IPin ppin);

        [PreserveSig]
        new int Disconnect([In] IPin ppin);

        [PreserveSig]
        new int SetDefaultSyncSource();

        #endregion

        [PreserveSig]
        int Connect(
            [In] IPin ppinOut,
            [In] IPin ppinIn
            );

        [PreserveSig]
        int Render([In] IPin ppinOut);

        [PreserveSig]
        int RenderFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFile,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrPlayList
            );

        [PreserveSig]
        int AddSourceFilter(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFileName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        int SetLogFile(IntPtr hFile); // DWORD_PTR

        [PreserveSig]
        int Abort();

        [PreserveSig]
        int ShouldOperationContinue();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("36b73882-c2c8-11cf-8b46-00805f6cef60"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph2 : IGraphBuilder
    {
        #region IFilterGraph Methods

        [PreserveSig]
        new int AddFilter(
            [In] IBaseFilter pFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName
            );

        [PreserveSig]
        new int RemoveFilter([In] IBaseFilter pFilter);

        [PreserveSig]
        new int EnumFilters([Out] out IEnumFilters ppEnum);

        [PreserveSig]
        new int FindFilterByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        new int ConnectDirect(
            [In] IPin ppinOut,
            [In] IPin ppinIn,
            [In, MarshalAs(UnmanagedType.LPStruct)]
            AMMediaType pmt
            );

        [PreserveSig]
        new int Reconnect([In] IPin ppin);

        [PreserveSig]
        new int Disconnect([In] IPin ppin);

        [PreserveSig]
        new int SetDefaultSyncSource();

        #endregion

        #region IGraphBuilder Method

        [PreserveSig]
        new int Connect(
            [In] IPin ppinOut,
            [In] IPin ppinIn
            );

        [PreserveSig]
        new int Render([In] IPin ppinOut);

        [PreserveSig]
        new int RenderFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFile,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrPlayList
            );

        [PreserveSig]
        new int AddSourceFilter(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFileName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        new int SetLogFile(IntPtr hFile); // DWORD_PTR

        [PreserveSig]
        new int Abort();

        [PreserveSig]
        new int ShouldOperationContinue();

        #endregion

        [PreserveSig]
        int AddSourceFilterForMoniker(
            [In] IMoniker pMoniker,
            [In] IBindCtx pCtx,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        int ReconnectEx(
            [In] IPin ppin,
            [In] AMMediaType pmt
            );

        [PreserveSig]
        int RenderEx(
            [In] IPin pPinOut,
            [In] AMRenderExFlags dwFlags,
            [In] IntPtr pvContext // DWORD *
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("5ACD6AA0-F482-11ce-8B67-00AA00A3F1A6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IConfigAviMux
    {
        [PreserveSig]
        int SetMasterStream([In] int iStream);

        [PreserveSig]
        int GetMasterStream([Out] out int pStream);

        [PreserveSig]
        int SetOutputCompatibilityIndex([In, MarshalAs(UnmanagedType.Bool)] bool fOldIndex);

        [PreserveSig]
        int GetOutputCompatibilityIndex([Out, MarshalAs(UnmanagedType.Bool)] out bool pfOldIndex);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("BEE3D220-157B-11d0-BD23-00A0C911CE86"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IConfigInterleaving
    {
        [PreserveSig]
        int put_Mode([In] InterleavingMode mode);

        [PreserveSig]
        int get_Mode([Out] out InterleavingMode pMode);

        [PreserveSig]
        int put_Interleaving(
            [In] ref long prtInterleave,
            [In] ref long prtPreroll
            );

        [PreserveSig]
        int get_Interleaving(
            [Out] out long prtInterleave,
            [Out] out long prtPreroll
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("a2104830-7c70-11cf-8bce-00aa00a3f1a6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileSinkFilter
    {
        [PreserveSig]
        int SetFileName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        [PreserveSig]
        int GetCurFile(
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string pszFileName,
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("00855B90-CE1B-11d0-BD4F-00A0C911CE86"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileSinkFilter2 : IFileSinkFilter
    {
        #region IFileSinkFilter Methods

        [PreserveSig]
        new int SetFileName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        [PreserveSig]
        new int GetCurFile(
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string pszFileName,
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        #endregion

        [PreserveSig]
        int SetMode([In] AMFileSinkFlags dwFlags);

        [PreserveSig]
        int GetMode([Out] out AMFileSinkFlags dwFlags);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a6-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileSourceFilter
    {
        [PreserveSig]
        int Load(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        [PreserveSig]
        int GetCurFile(
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string pszFileName,
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("e46a9787-2b71-444d-a4b5-1fab7b708d6a"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVideoFrameStep
    {
        [PreserveSig]
        int Step(
            [In] int dwFrames,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pStepObject
            );

        [PreserveSig]
        int CanStep(
            [In] int bMultiple,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pStepObject
            );

        [PreserveSig]
        int CancelStep();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("31EFAC30-515C-11d0-A9AA-00AA0061BE93"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IKsPropertySet
    {
        [PreserveSig]
        int Set(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidPropSet,
            [In] int dwPropID,
            [In] IntPtr pInstanceData,
            [In] int cbInstanceData,
            [In] IntPtr pPropData,
            [In] int cbPropData
            );

        [PreserveSig]
        int Get(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidPropSet,
            [In] int dwPropID,
            [In] IntPtr pInstanceData,
            [In] int cbInstanceData,
            [In, Out] IntPtr pPropData,
            [In] int cbPropData,
            [Out] out int pcbReturned
            );

        [PreserveSig]
        int QuerySupported(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidPropSet,
            [In] int dwPropID,
            [Out] out KSPropertySupport pTypeSupport
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("211A8761-03AC-11d1-8D13-00AA00BD8339"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTuner
    {
        [PreserveSig]
        int put_Channel(
            [In] int lChannel,
            [In] AMTunerSubChannel lVideoSubChannel,
            [In] AMTunerSubChannel lAudioSubChannel
            );

        [PreserveSig]
        int get_Channel(
            [Out] out int plChannel,
            [Out] out AMTunerSubChannel plVideoSubChannel,
            [Out] out AMTunerSubChannel plAudioSubChannel
            );

        [PreserveSig]
        int ChannelMinMax(
            [Out] out int lChannelMin,
            [Out] out int lChannelMax
            );

        [PreserveSig]
        int put_CountryCode([In] int lCountryCode);

        [PreserveSig]
        int get_CountryCode([Out] out int plCountryCode);

        [PreserveSig]
        int put_TuningSpace([In] int lTuningSpace);

        [PreserveSig]
        int get_TuningSpace([Out] out int plTuningSpace);

        [PreserveSig]
        int Logon([In] IntPtr hCurrentUser); // HANDLE

        [PreserveSig]
        int Logout();

        [PreserveSig]
        int SignalPresent([Out] out AMTunerSignalStrength plSignalStrength);

        [PreserveSig]
        int put_Mode([In] AMTunerModeType lMode);

        [PreserveSig]
        int get_Mode([Out] out AMTunerModeType plMode);

        [PreserveSig]
        int GetAvailableModes([Out] out AMTunerModeType plModes);

        [PreserveSig]
        int RegisterNotificationCallBack(
            [In] IAMTunerNotification pNotify,
            [In] AMTunerEventType lEvents
            );

        [PreserveSig]
        int UnRegisterNotificationCallBack([In] IAMTunerNotification pNotify);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("211A8760-03AC-11d1-8D13-00AA00BD8339"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTunerNotification
    {
        [PreserveSig]
        int OnEvent([In] AMTunerEventType Event);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("211A8766-03AC-11d1-8D13-00AA00BD8339"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTVTuner : IAMTuner
    {
        #region IAMTuner

        [PreserveSig]
        new int put_Channel(
            [In] int lChannel,
            [In] AMTunerSubChannel lVideoSubChannel,
            [In] AMTunerSubChannel lAudioSubChannel
            );

        [PreserveSig]
        new int get_Channel(
            [Out] out int plChannel,
            [Out] out AMTunerSubChannel plVideoSubChannel,
            [Out] out AMTunerSubChannel plAudioSubChannel
            );

        [PreserveSig]
        new int ChannelMinMax(
            [Out] out int lChannelMin,
            [Out] out int lChannelMax
            );

        [PreserveSig]
        new int put_CountryCode([In] int lCountryCode);

        [PreserveSig]
        new int get_CountryCode([Out] out int plCountryCode);

        [PreserveSig]
        new int put_TuningSpace([In] int lTuningSpace);

        [PreserveSig]
        new int get_TuningSpace([Out] out int plTuningSpace);

        [PreserveSig]
        new int Logon([In] IntPtr hCurrentUser); // HANDLE

        [PreserveSig]
        new int Logout();

        [PreserveSig]
        new int SignalPresent([Out] out AMTunerSignalStrength plSignalStrength);

        [PreserveSig]
        new int put_Mode([In] AMTunerModeType lMode);

        [PreserveSig]
        new int get_Mode([Out] out AMTunerModeType plMode);

        [PreserveSig]
        new int GetAvailableModes([Out] out AMTunerModeType plModes);

        [PreserveSig]
        new int RegisterNotificationCallBack(
            [In] IAMTunerNotification pNotify,
            [In] AMTunerEventType lEvents
            );

        [PreserveSig]
        new int UnRegisterNotificationCallBack([In] IAMTunerNotification pNotify);

        #endregion

        [PreserveSig]
        int get_AvailableTVFormats([Out] out AnalogVideoStandard lAnalogVideoStandard);

        [PreserveSig]
        int get_TVFormat([Out] out AnalogVideoStandard lAnalogVideoStandard);

        [PreserveSig]
        int AutoTune(
            [In] int lChannel,
            [Out] out int plFoundSignal
            );

        [PreserveSig]
        int StoreAutoTune();

        [PreserveSig]
        int get_NumInputConnections([Out] out int plNumInputConnections);

        [PreserveSig]
        int put_InputType(
            [In] int lIndex,
            [In] TunerInputType inputType
            );

        [PreserveSig]
        int get_InputType(
            [In] int lIndex,
            [Out] out TunerInputType inputType
            );

        [PreserveSig]
        int put_ConnectInput([In] int lIndex);

        [PreserveSig]
        int get_ConnectInput([Out] out int lIndex);

        [PreserveSig]
        int get_VideoFrequency([Out] out int lFreq);

        [PreserveSig]
        int get_AudioFrequency([Out] out int lFreq);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("6a2e0670-28e4-11d0-a18c-00a0c9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMVideoControl
    {
        [PreserveSig]
        int GetCaps(
            [In] IPin pPin,
            [Out] out VideoControlFlags pCapsFlags
            );

        [PreserveSig]
        int SetMode(
            [In] IPin pPin,
            [In] VideoControlFlags Mode
            );

        [PreserveSig]
        int GetMode(
            [In] IPin pPin,
            [Out] out VideoControlFlags Mode
            );

        [PreserveSig]
        int GetCurrentActualFrameRate(
            [In] IPin pPin,
            [Out] out long ActualFrameRate
            );

        [PreserveSig]
        int GetMaxAvailableFrameRate(
            [In] IPin pPin,
            [In] int iIndex,
            [In] Size Dimensions,
            [Out] out long MaxAvailableFrameRate
            );

        [PreserveSig]
        int GetFrameRateList(
            [In] IPin pPin,
            [In] int iIndex,
            [In] Size Dimensions,
            [Out] out int ListSize,
            [Out] out IntPtr FrameRates
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E13350-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMAnalogVideoDecoder
    {
        [PreserveSig]
        int get_AvailableTVFormats([Out] out AnalogVideoStandard lAnalogVideoStandard);

        [PreserveSig]
        int put_TVFormat([In] AnalogVideoStandard lAnalogVideoStandard);

        [PreserveSig]
        int get_TVFormat([Out] out AnalogVideoStandard plAnalogVideoStandard);

        [PreserveSig]
        int get_HorizontalLocked([Out, MarshalAs(UnmanagedType.Bool)] out bool plLocked);

        [PreserveSig]
        int put_VCRHorizontalLocking([In, MarshalAs(UnmanagedType.Bool)] bool lVCRHorizontalLocking);

        [PreserveSig]
        int get_VCRHorizontalLocking([Out, MarshalAs(UnmanagedType.Bool)] out bool plVCRHorizontalLocking);

        [PreserveSig]
        int get_NumberOfLines([Out] out int plNumberOfLines);

        [PreserveSig]
        int put_OutputEnable([In, MarshalAs(UnmanagedType.Bool)] bool lOutputEnable);

        [PreserveSig]
        int get_OutputEnable([Out, MarshalAs(UnmanagedType.Bool)] out bool plOutputEnable);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E13360-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMVideoProcAmp
    {
        [PreserveSig]
        int GetRange(
            [In] VideoProcAmpProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out VideoProcAmpFlags pCapsFlags
            );

        [PreserveSig]
        int Set(
            [In] VideoProcAmpProperty Property,
            [In] int lValue,
            [In] VideoProcAmpFlags Flags
            );

        [PreserveSig]
        int Get(
            [In] VideoProcAmpProperty Property,
            [Out] out int lValue,
            [Out] out VideoProcAmpFlags Flags
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("54C39221-8380-11d0-B3F0-00AA003761C5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMAudioInputMixer
    {
        [PreserveSig]
        int put_Enable([In, MarshalAs(UnmanagedType.Bool)] bool fEnable);

        [PreserveSig]
        int get_Enable([Out, MarshalAs(UnmanagedType.Bool)] out bool pfEnable);

        [PreserveSig]
        int put_Mono([In, MarshalAs(UnmanagedType.Bool)] bool fMono);

        [PreserveSig]
        int get_Mono([Out, MarshalAs(UnmanagedType.Bool)] out bool pfMono);

        [PreserveSig]
        int put_MixLevel([In] double Level);

        [PreserveSig]
        int get_MixLevel([Out] out double pLevel);

        [PreserveSig]
        int put_Pan([In] double Pan);

        [PreserveSig]
        int get_Pan([Out] out double pPan);

        [PreserveSig]
        int put_Loudness([In, MarshalAs(UnmanagedType.Bool)] bool fLoudness);

        [PreserveSig]
        int get_Loudness([Out, MarshalAs(UnmanagedType.Bool)] out bool pfLoudness);

        [PreserveSig]
        int put_Treble([In] double Treble);

        [PreserveSig]
        int get_Treble([Out] out double pTreble);

        [PreserveSig]
        int get_TrebleRange([Out] out double pRange);

        [PreserveSig]
        int put_Bass([In] double Bass);

        [PreserveSig]
        int get_Bass([Out] out double pBass);

        [PreserveSig]
        int get_BassRange([Out] out double pRange);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("670d1d20-a068-11d0-b3f0-00aa003761c5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMCopyCaptureFileProgress
    {
        [PreserveSig]
        int Progress(int iProgress);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E13380-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMCrossbar
    {
        [PreserveSig]
        int get_PinCounts(
            [Out] out int OutputPinCount,
            [Out] out int InputPinCount
            );

        [PreserveSig]
        int CanRoute(
            [In] int OutputPinIndex,
            [In] int InputPinIndex
            );

        [PreserveSig]
        int Route(
            [In] int OutputPinIndex,
            [In] int InputPinIndex
            );

        [PreserveSig]
        int get_IsRoutedTo(
            [In] int OutputPinIndex,
            [Out] out int InputPinIndex
            );

        [PreserveSig]
        int get_CrossbarPinInfo(
            [In, MarshalAs(UnmanagedType.Bool)] bool IsInputPin,
            [In] int PinIndex,
            [Out] out int PinIndexRelated,
            [Out] out PhysicalConnectorType PhysicalType
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E13344-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMDroppedFrames
    {
        [PreserveSig]
        int GetNumDropped([Out] out int plDropped);

        [PreserveSig]
        int GetNumNotDropped([Out] out int plNotDropped);

        [PreserveSig]
        int GetDroppedInfo(
            [In] int lSize,
            [Out, MarshalAs(UnmanagedType.LPArray)] out int[] plArray,
            [Out] out int plNumCopied
            );

        [PreserveSig]
        int GetAverageFrameSize([Out] out int plAverageSize);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("83EC1C30-23D1-11d1-99E6-00A0C9560266"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTVAudio
    {
        [PreserveSig]
        int GetHardwareSupportedTVAudioModes([Out] out TVAudioMode plModes);

        [PreserveSig]
        int GetAvailableTVAudioModes([Out] out TVAudioMode plModes);

        [PreserveSig]
        int get_TVAudioMode([Out] out TVAudioMode plMode);

        [PreserveSig]
        int put_TVAudioMode([In] TVAudioMode lMode);

        [PreserveSig]
        int RegisterNotificationCallBack(
            [In] IAMTunerNotification pNotify,
            [In] AMTVAudioEventType lEvents
            );

        [PreserveSig]
        int UnRegisterNotificationCallBack([In] IAMTunerNotification pNotify);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("D8D715A3-6E5E-11D0-B3F0-00AA003761C5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMVfwCompressDialogs
    {
        [PreserveSig]
        int ShowDialog(
            [In] VfwCompressDialogs iDialog,
            [In] IntPtr hwnd
            );

        [PreserveSig]
        int GetState(
            [In] IntPtr pState,
            [In, Out] ref int pcbState
            );

        [PreserveSig]
        int SetState(
            [In] IntPtr pState,
            [In] int pcbState
            );

        [PreserveSig]
        int SendDriverMessage(
            [In] int uMsg,
            [In] int dw1,
            [In] int dw2
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E13343-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMVideoCompression
    {
        [PreserveSig]
        int put_KeyFrameRate([In] int KeyFrameRate);

        [PreserveSig]
        int get_KeyFrameRate([Out] out int pKeyFrameRate);

        [PreserveSig]
        int put_PFramesPerKeyFrame([In] int PFramesPerKeyFrame);

        [PreserveSig]
        int get_PFramesPerKeyFrame([Out] out int pPFramesPerKeyFrame);

        [PreserveSig]
        int put_Quality([In] double Quality);

        [PreserveSig]
        int get_Quality([Out] out double pQuality);

        [PreserveSig]
        int put_WindowSize([In] long WindowSize);

        [PreserveSig]
        int get_WindowSize([Out] out long pWindowSize);

        [PreserveSig]
        int GetInfo(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszVersion, // WCHAR *
            [Out] out int pcbVersion,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDescription, // LPWSTR
            [Out] out int pcbDescription,
            [Out] out int pDefaultKeyFrameRate,
            [Out] out int pDefaultPFramesPerKey,
            [Out] out double pDefaultQuality,
            [Out] out CompressionCaps pCapabilities
            );

        [PreserveSig]
        int OverrideKeyFrame([In] int FrameNumber);

        [PreserveSig]
        int OverrideFrameSize(
            [In] int FrameNumber,
            [In] int Size
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICaptureGraphBuilder2
    {
        [PreserveSig]
        int SetFiltergraph([In] IGraphBuilder pfg);

        [PreserveSig]
        int GetFiltergraph([Out] out IGraphBuilder ppfg);

        [PreserveSig]
        int SetOutputFileName(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pType,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile,
            [Out] out IBaseFilter ppbf,
            [Out] out IFileSinkFilter ppSink
            );

        [PreserveSig]
        int FindInterface(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pCategory,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pType,
            [In] IBaseFilter pbf,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppint
            );

        [PreserveSig]
        int RenderStream(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid PinCategory,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid MediaType,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pSource,
            [In] IBaseFilter pfCompressor,
            [In] IBaseFilter pfRenderer
            );

        [PreserveSig]
        int ControlStream(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid PinCategory,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid MediaType,
            [In, MarshalAs(UnmanagedType.Interface)] IBaseFilter pFilter,
            [In] DsLong pstart,
            [In] DsLong pstop,
            [In] short wStartCookie,
            [In] short wStopCookie
            );

        [PreserveSig]
        int AllocCapFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile,
            [In] long dwlSize
            );

        [PreserveSig]
        int CopyCaptureFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrOld,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrNew,
            [In, MarshalAs(UnmanagedType.Bool)] bool fAllowEscAbort,
            [In] IAMCopyCaptureFileProgress pFilter
            );

        [PreserveSig]
        int FindPin(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pSource,
            [In] PinDirection pindir,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid PinCategory,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid MediaType,
            [In, MarshalAs(UnmanagedType.Bool)] bool fUnconnected,
            [In] int ZeroBasedIndex,
            [Out, MarshalAs(UnmanagedType.Interface)] out IPin ppPin
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E13340-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMStreamConfig
    {
        [PreserveSig]
        int SetFormat([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        [PreserveSig]
        int GetFormat([Out] out AMMediaType pmt);

        [PreserveSig]
        int GetNumberOfCapabilities(out int piCount, out int piSize);

        [PreserveSig]
        int GetStreamCaps(
            [In] int iIndex,
            [Out] out AMMediaType ppmt,
            [In] IntPtr pSCC
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("d18e17a0-aacb-11d0-afb0-00aa00b67a42"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDVEnc
    {
        [PreserveSig]
        int get_IFormatResolution(
            [Out] out DVEncoderVideoFormat VideoFormat,
            [Out] out DVEncoderFormat DVFormat,
            [Out] out DVEncoderResolution Resolution,
            [In] OABool fDVInfo,
            [Out] out DVInfo sDVInfo
            );

        [PreserveSig]
        int put_IFormatResolution(
            [In] DVEncoderVideoFormat VideoFormat,
            [In] DVEncoderFormat DVFormat,
            [In] DVEncoderResolution Resolution,
            [In] OABool fDVInfo,
            [In] DVInfo sDVInfo
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a2-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaEventSink
    {
        [PreserveSig]
        int Notify(
            [In] EventCode evCode,
            [In] IntPtr EventParam1,
            [In] IntPtr EventParam2
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("9FD52741-176D-4b36-8F51-CA8F933223BE"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMClockSlave
    {
        [PreserveSig]
        int SetErrorTolerance([In] int dwTolerance);

        [PreserveSig]
        int GetErrorTolerance([Out] out int pdwTolerance);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("c0dff467-d499-4986-972b-e1d9090fa941"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMDecoderCaps
    {
        [PreserveSig]
        int GetDecoderCaps(
            [In] AMQueryDecoder dwCapIndex,
            [Out] out DecoderCap lpdwCap
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("62fae250-7e65-4460-bfc9-6398b322073c"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMOverlayFX
    {
        [PreserveSig]
        int QueryOverlayFXCaps([Out] out AMOverlayFX lpdwOverlayFXCaps);

        [PreserveSig]
        int SetOverlayFX([In] AMOverlayFX dwOverlayFX);

        [PreserveSig]
        int GetOverlayFX([Out] out AMOverlayFX lpdwOverlayFX);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("8389d2d0-77d7-11d1-abe6-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMResourceControl
    {
        [PreserveSig]
        int Reserve(
            [In] AMResCtlReserveFlags dwFlags,
            [In] IntPtr pvReserved // PVOID
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("c1960960-17f5-11d1-abe1-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMStreamSelect
    {
        [PreserveSig]
        int Count([Out] out int pcStreams);

        [PreserveSig]
        int Info(
            [In] int lIndex,
            [Out] out AMMediaType ppmt,
            [Out] out AMStreamSelectInfoFlags pdwFlags,
            [Out] out int plcid,
            [Out] out int pdwGroup,
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszName,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppObject,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk
            );

        [PreserveSig]
        int Enable(
            [In] int lIndex,
            [In] AMStreamSelectEnableFlags dwFlags
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("60d32930-13da-11d3-9ec6-c4fcaef5c7be"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMVideoDecimationProperties
    {
        [PreserveSig]
        int QueryDecimationUsage([Out] out DecimationUsage lpUsage);

        [PreserveSig]
        int SetDecimationUsage([In] DecimationUsage Usage);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("b79bb0b0-33c1-11d1-abe1-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterMapper2
    {
        [PreserveSig]
        int CreateCategory(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid clsidCategory,
            [In] Merit dwCategoryMerit,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Description
            );

        [PreserveSig]
        int UnregisterFilter(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid clsidCategory,
            [In, MarshalAs(UnmanagedType.LPWStr)] string szInstance,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid Filter
            );

        [PreserveSig]
        [Obsolete("This method has not been tested.", false)]
        int RegisterFilter(
            [In] Guid clsidFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Name,
            [In, Out] IMoniker ppMoniker,
            [In] DsGuid pclsidCategory,
            [In, MarshalAs(UnmanagedType.LPWStr)] string szInstance,
            [In] RegFilter2 prf2);

        [PreserveSig]
        int EnumMatchingFilters(
            [Out] out IEnumMoniker ppEnum,
            [In] int dwFlags,
            [In, MarshalAs(UnmanagedType.Bool)] bool bExactMatch,
            [In] Merit dwMerit,
            [In, MarshalAs(UnmanagedType.Bool)] bool bInputNeeded,
            [In] int cInputTypes,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] pInputTypes, // GUID *
            [In] RegPinMedium pMedIn,
            [In] DsGuid pPinCategoryIn,
            [In, MarshalAs(UnmanagedType.Bool)] bool bRender,
            [In, MarshalAs(UnmanagedType.Bool)] bool bOutputNeeded,
            [In] int cOutputTypes,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] pOutputTypes, // GUID *
            [In] RegPinMedium pMedOut,
            [In] DsGuid pPinCategoryOut
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("b79bb0b1-33c1-11d1-abe1-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterMapper3 : IFilterMapper2
    {
        #region IFilterMapper2 Methods

        [PreserveSig]
        new int CreateCategory(
            [In] Guid clsidCategory,
            [In] Merit dwCategoryMerit,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Description
            );

        [PreserveSig]
        new int UnregisterFilter(
            [In] Guid clsidCategory,
            [In, MarshalAs(UnmanagedType.LPWStr)] string szInstance,
            [In] Guid Filter
            );

        [PreserveSig]
        [Obsolete("This method has not been tested.", false)]
        new int RegisterFilter(
            [In] Guid clsidFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string Name,
            [In, Out] IMoniker ppMoniker,
            [In] DsGuid pclsidCategory,
            [In, MarshalAs(UnmanagedType.LPWStr)] string szInstance,
            [In] RegFilter2 prf2);

        [PreserveSig]
        new int EnumMatchingFilters(
            [Out] out IEnumMoniker ppEnum,
            [In] int dwFlags,
            [In, MarshalAs(UnmanagedType.Bool)] bool bExactMatch,
            [In] Merit dwMerit,
            [In, MarshalAs(UnmanagedType.Bool)] bool bInputNeeded,
            [In] int cInputTypes,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] pInputTypes, // GUID *
            [In] RegPinMedium pMedIn,
            [In] DsGuid pPinCategoryIn,
            [In, MarshalAs(UnmanagedType.Bool)] bool bRender,
            [In, MarshalAs(UnmanagedType.Bool)] bool bOutputNeeded,
            [In] int cOutputTypes,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] pOutputTypes, // GUID *
            [In] RegPinMedium pMedOut,
            [In] DsGuid pPinCategoryOut
            );

        #endregion

        [PreserveSig]
        int GetICreateDevEnum([Out] out ICreateDevEnum ppEnum);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("436eee9c-264f-4242-90e1-4e330c107512"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMpeg2Demultiplexer
    {
        [PreserveSig]
        int CreateOutputPin(
            [In] AMMediaType pMediaType,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPinName,
            [Out] out IPin ppIPin
            );

        [PreserveSig]
        int SetOutputPinMediaType(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPinName,
            [In] AMMediaType pMediaType
            );

        [PreserveSig]
        int DeleteOutputPin([In, MarshalAs(UnmanagedType.LPWStr)] string pszPinName);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("B5730A90-1A2C-11cf-8C23-00AA006B6814"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMExtDevice
    {
        [PreserveSig]
        int GetCapability(
            [In] ExtDeviceCaps Capability,
            [Out] out ExtDeviceCaps pValue,
            [Out] out double pdblValue
            );

        [PreserveSig]
        int get_ExternalDeviceID([Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszData);

        [PreserveSig]
        int get_ExternalDeviceVersion([Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszData);

        [PreserveSig]
        int put_DevicePower([In] ExtDeviceCaps PowerMode);

        [PreserveSig]
        int get_DevicePower([Out] out ExtDeviceCaps pPowerMode);

        [PreserveSig]
        int Calibrate(
            [In] IntPtr hEvent, // HEVENT
            [In] ExtTransportEdit Mode, //Active / Inactive
            [Out] out int pStatus
            );

        [PreserveSig]
        int put_DevicePort([In] ExtDevicePort DevicePort);

        [PreserveSig]
        int get_DevicePort([Out] out ExtDevicePort pDevicePort);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("D0E04C47-25B8-4369-925A-362A01D95444"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMPEG2StreamIdMap
    {
        [PreserveSig]
        int MapStreamId(
            [In] int ulStreamId,
            [In] MPEG2Program MediaSampleContent,
            [In] int ulSubstreamFilterValue,
            [In] int iDataOffset
            );

        [PreserveSig]
        int UnmapStreamId(
            [In] int culStreamId,
            [In, MarshalAs(UnmanagedType.LPArray)] int[] pulStreamId
            );

        [PreserveSig,
        Obsolete("Because of bug in DS 9.0c, you can't get the StreamId map from .NET", false)]
        int EnumStreamIdMap([Out] out IEnumStreamIdMap ppIEnumStreamIdMap);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("A03CD5F0-3045-11cf-8C44-00AA006B6814"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMExtTransport
    {
        [PreserveSig]
        int GetCapability(
            [In] ExtTransportCaps Capability,
            [Out] out int pValue,
            [Out] out double pdblValue
            );

        [PreserveSig]
        int put_MediaState([In] ExtTransportMediaStates State);

        [PreserveSig]
        int get_MediaState([Out] out ExtTransportMediaStates pState);

        [PreserveSig]
        int put_LocalControl([In] int State);

        [PreserveSig]
        int get_LocalControl([Out] out int pState);

        [PreserveSig]
        int GetStatus(
            [In] ExtTransportStatus StatusItem,
            [Out] out int pValue
            );

        [PreserveSig]
        int GetTransportBasicParameters(
            [In] ExtTransportParameters Param,
            [Out] out int pValue,
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszData
            );

        [PreserveSig]
        int SetTransportBasicParameters(
            [In] ExtTransportParameters Param,
            [In] int Value,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszData
            );

        [PreserveSig]
        int GetTransportVideoParameters(
            [In] ExtTransportParameters Param,
            [Out] out int pValue
            );

        [PreserveSig]
        int SetTransportVideoParameters(
            [In] ExtTransportParameters Param,
            [In] int Value
            );

        [PreserveSig]
        int GetTransportAudioParameters(
            [In] ExtTransportParameters Param,
            [Out] out int pValue
            );

        [PreserveSig]
        int SetTransportAudioParameters(
            [In] ExtTransportParameters Param,
            [In] ExtTransportAudio Value
            );

        [PreserveSig]
        int put_Mode([In] ExtTransportModes Mode);

        [PreserveSig]
        int get_Mode([Out] out ExtTransportModes pMode);

        [PreserveSig]
        int put_Rate([In] double dblRate);

        [PreserveSig]
        int get_Rate([Out] out double pdblRate);

        [PreserveSig]
        int GetChase(
            [Out] out int pEnabled,
            [Out] out int pOffset,
            [Out] out IntPtr phEvent // HEVENT
            );

        [PreserveSig]
        int SetChase(
            [In] int Enable,
            [In] int Offset,
            [In] IntPtr hEvent // HEVENT
            );

        [PreserveSig]
        int GetBump(
            [Out] out int pSpeed,
            [Out] out int pDuration
            );

        [PreserveSig]
        int SetBump(
            [In] int Speed,
            [In] int Duration
            );

        [PreserveSig]
        int get_AntiClogControl([Out] out int pEnabled);

        [PreserveSig]
        int put_AntiClogControl([In] int Enable);

        [PreserveSig]
        int GetEditPropertySet(
            [In] int EditID,
            [Out] out ExtTransportEdit pState
            );

        [PreserveSig]
        int SetEditPropertySet(
            [In, Out] ref int pEditID,
            [In] ExtTransportEdit State
            );

        [PreserveSig]
        int GetEditProperty(
            [In] int EditID,
            [In] ExtTransportEdit Param,
            [Out] out int pValue
            );

        [PreserveSig]
        int SetEditProperty(
            [In] int EditID,
            [In] ExtTransportEdit Param,
            [In] int Value
            );

        [PreserveSig]
        int get_EditStart([Out] out int pValue);

        [PreserveSig]
        int put_EditStart([In] int Value);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("4995f511-9ddb-4f12-bd3b-f04611807b79"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMGraphBuilderCallback
    {
        [PreserveSig]
        int SelectedFilter([In] IMoniker pMon);

        [PreserveSig]
        int CreatedFilter([In] IBaseFilter pFil);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868a5-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IQualityControl
    {
        [PreserveSig]
        int Notify(
            [In] IBaseFilter pSelf,
            [In] Quality q
            );

        [PreserveSig]
        int SetSink([In] IQualityControl piqc);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56ED71A0-AF5F-11D0-B3F0-00AA003761C5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMBufferNegotiation
    {
        [PreserveSig]
        int SuggestAllocatorProperties([In] AllocatorProperties pprop);

        [PreserveSig]
        int GetAllocatorProperties([Out] AllocatorProperties pprop);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("22320CB2-D41A-11d2-BF7C-D7CB9DF0BF93"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMAudioRendererStats
    {
        [PreserveSig]
        int GetStatParam(
            [In] AMAudioRendererStatParam dwParam,
            [Out] out int pdwParam1,
            [Out] out int pdwParam2
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("7B3A2F01-0751-48DD-B556-004785171C54"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRegisterServiceProvider
    {
        [PreserveSig]
        int RegisterService(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidService,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkObject
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("36b73883-c2c8-11cf-8b46-00805f6cef60"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISeekingPassThru
    {
        int Init(
            [In, MarshalAs(UnmanagedType.Bool)] bool bSupportRendering,
            [In] IPin pPin
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("4d5466b0-a49c-11d1-abe8-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMClockAdjust
    {
        [PreserveSig]
        int SetClockDelta([In] long rtDelta);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868aa-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAsyncReader
    {
        [PreserveSig]
        int RequestAllocator(
            [In] IMemAllocator pPreferred,
            [In, MarshalAs(UnmanagedType.LPStruct)] AllocatorProperties pProps,
            [Out] out IMemAllocator ppActual
            );

        [PreserveSig]
        int Request(
            [In] IMediaSample pSample,
            [In] IntPtr dwUser
            );

        [PreserveSig]
        int WaitForNext(
            [In] int dwTimeout,
            [Out] out IMediaSample ppSample,
            [Out] out IntPtr pdwUser
            );

        [PreserveSig]
        int SyncReadAligned(
            [In] IMediaSample pSample
            );

        [PreserveSig]
        int SyncRead(
            [In] long llPosition,
            [In] int lLength,
            [Out] IntPtr pBuffer // BYTE *
            );

        [PreserveSig]
        int Length(
            [Out] out long pTotal,
            [Out] out long pAvailable
            );

        [PreserveSig]
        int BeginFlush();

        [PreserveSig]
        int EndFlush();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("C6E13370-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMCameraControl
    {
        [PreserveSig]
        int GetRange(
            [In] CameraControlProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out CameraControlFlags pCapsFlags
            );

        [PreserveSig]
        int Set(
            [In] CameraControlProperty Property,
            [In] int lValue,
            [In] CameraControlFlags Flags
            );

        [PreserveSig]
        int Get(
            [In] CameraControlProperty Property,
            [Out] out int lValue,
            [Out] out CameraControlFlags Flags
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("aaf38154-b80b-422f-91e6-b66467509a07"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph3 : IFilterGraph2
    {
        #region IFilterGraph Methods

        [PreserveSig]
        new int AddFilter(
            [In] IBaseFilter pFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName
            );

        [PreserveSig]
        new int RemoveFilter([In] IBaseFilter pFilter);

        [PreserveSig]
        new int EnumFilters([Out] out IEnumFilters ppEnum);

        [PreserveSig]
        new int FindFilterByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        new int ConnectDirect(
            [In] IPin ppinOut,
            [In] IPin ppinIn,
            [In, MarshalAs(UnmanagedType.LPStruct)]
            AMMediaType pmt
            );

        [PreserveSig]
        new int Reconnect([In] IPin ppin);

        [PreserveSig]
        new int Disconnect([In] IPin ppin);

        [PreserveSig]
        new int SetDefaultSyncSource();

        #endregion

        #region IGraphBuilder Method

        [PreserveSig]
        new int Connect(
            [In] IPin ppinOut,
            [In] IPin ppinIn
            );

        [PreserveSig]
        new int Render([In] IPin ppinOut);

        [PreserveSig]
        new int RenderFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFile,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrPlayList
            );

        [PreserveSig]
        new int AddSourceFilter(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFileName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        new int SetLogFile(IntPtr hFile); // DWORD_PTR

        [PreserveSig]
        new int Abort();

        [PreserveSig]
        new int ShouldOperationContinue();

        #endregion

        #region IFilterGraph2 methods

        [PreserveSig]
        new int AddSourceFilterForMoniker(
            [In] IMoniker pMoniker,
            [In] IBindCtx pCtx,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
            [Out] out IBaseFilter ppFilter);

        [PreserveSig]
        new int ReconnectEx(
            [In] IPin ppin,
            [In] AMMediaType pmt
            );

        [PreserveSig]
        new int RenderEx(
            [In] IPin pPinOut,
            [In] AMRenderExFlags dwFlags,
            [In] IntPtr pvContext // DWORD *
            );

        #endregion

        [PreserveSig]
        int SetSyncSourceEx(
            IReferenceClock pClockForMostOfFilterGraph,
            IReferenceClock pClockForFilter,
            IBaseFilter pFilter
            );

    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("0e26a181-f40c-4635-8786-976284b52981"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMPluginControl
    {
        [PreserveSig]
        int GetPreferredClsid(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid subType,
            out Guid clsid
            );

        [PreserveSig]
        int GetPreferredClsidByIndex(
            int index,
            out Guid subType,
            out Guid clsid
            );

        [PreserveSig]
        int SetPreferredClsid(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid subType,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid clsid
            );

        [PreserveSig]
        int IsDisabled(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid clsid
            );

        [PreserveSig]
        int GetDisabledByIndex(
            int index,
            out Guid clsid
            );

        [PreserveSig]
        int SetDisabled(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid clsid,
            bool disabled
            );

        [PreserveSig]
        int IsLegacyDisabled(
            [MarshalAs(UnmanagedType.LPWStr)] string dllName
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("cf7b26fc-9a00-485b-8147-3e789d5e8f67"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMAsyncReaderTimestampScaling
    {
        [PreserveSig]
        int GetTimestampMode(
            [MarshalAs(UnmanagedType.Bool)] out bool pfRaw
            );

        [PreserveSig]
        int SetTimestampMode(
            [MarshalAs(UnmanagedType.Bool)] bool fRaw
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
   Guid("00000109-0000-0000-C000-000000000046"),
   InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistStream : IPersist
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);

        #endregion

        [PreserveSig]
        int IsDirty();

        [PreserveSig]
        int Load([In] IStream pStm);

        [PreserveSig]
        int Save(
            [In] IStream pStm,
            [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

        [PreserveSig]
        int GetSizeMax([Out] out long pcbSize);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("0000010c-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersist
    {
        [PreserveSig]
        int GetClassID([Out] out Guid pClassID);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("b61178d1-a2d9-11cf-9e53-00aa00a216a1"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IKsPin
    {
        /// <summary>
        /// The caller must free the returned structures, using the CoTaskMemFree function
        /// </summary>
        [PreserveSig]
        int KsQueryMediums(
            out IntPtr ip);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("B196B28B-BAB4-101A-B69C-00AA00341D07"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISpecifyPropertyPages
    {
        [PreserveSig]
        int GetPages(out DsCAUUID pPages);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("55272A00-42CB-11CE-8135-00AA004BB851"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyBag
    {
        [PreserveSig]
        int Read(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [Out, MarshalAs(UnmanagedType.Struct)] out object pVar,
            [In] IErrorLog pErrorLog
            );

        [PreserveSig]
        int Write(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In, MarshalAs(UnmanagedType.Struct)] ref object pVar
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("3127CA40-446E-11CE-8135-00AA004BB851"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IErrorLog
    {
        [PreserveSig]
        int AddError(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In] System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b9-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IAMCollection
    {
        [PreserveSig]
        int get_Count([Out] out int plCount);

        [PreserveSig]
        int Item(
            [In] int lItem,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk
            );

        [PreserveSig]
        int get__NewEnum([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868ba-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IFilterInfo
    {
        [PreserveSig]
        int FindPin(
            [In, MarshalAs(UnmanagedType.BStr)] string strPinID,
            [Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk
            );

        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string strName);

        [PreserveSig]
        int get_VendorInfo([Out, MarshalAs(UnmanagedType.BStr)] string strVendorInfo);

        [PreserveSig]
        int get_Filter([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_Pins([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

        [PreserveSig]
        int get_IsFileSource([Out] out int pbIsSource);

        [PreserveSig]
        int get_Filename([Out, MarshalAs(UnmanagedType.BStr)] out string pstrFilename);

        [PreserveSig]
        int put_Filename([In, MarshalAs(UnmanagedType.BStr)] string strFilename);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868bb-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IRegFilterInfo
    {
        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string strName);

        [PreserveSig]
        int Filter([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868bc-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaTypeInfo
    {
        [PreserveSig]
        int get_Type([Out, MarshalAs(UnmanagedType.BStr)] out string strType);

        [PreserveSig]
        int get_Subtype([Out, MarshalAs(UnmanagedType.BStr)] out string strType);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868bd-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPinInfo
    {
        [PreserveSig]
        int get_Pin([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_ConnectedTo([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

        [PreserveSig]
        int get_ConnectionMediaType([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_FilterInfo([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string ppUnk);

        [PreserveSig]
        int get_Direction([Out] int ppDirection);

        [PreserveSig]
        int get_PinID([Out, MarshalAs(UnmanagedType.BStr)] out string strPinID);

        [PreserveSig]
        int get_MediaTypes([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int Connect([In, MarshalAs(UnmanagedType.IUnknown)] object pPin);

        [PreserveSig]
        int ConnectDirect([In, MarshalAs(UnmanagedType.IUnknown)] object pPin);

        [PreserveSig]
        int ConnectWithType(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pPin,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pMediaType
            );

        [PreserveSig]
        int Disconnect();

        [PreserveSig]
        int Render();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("bc9bcf80-dcd2-11d2-abf6-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IAMStats
    {
        [PreserveSig]
        int Reset();

        [PreserveSig]
        int get_Count([Out] out int plCount);

        [PreserveSig]
        int GetValueByIndex(
            [In] int lIndex,
            [Out, MarshalAs(UnmanagedType.BStr)] out string szName,
            [Out] out int lCount,
            [Out] out double dLast,
            [Out] out double dAverage,
            [Out] out double dStdDev,
            [Out] out double dMin,
            [Out] out double dMax
            );

        [PreserveSig]
        int GetValueByName(
            [In, MarshalAs(UnmanagedType.BStr)] string szName,
            [Out] out int lIndex,
            [Out] out int lCount,
            [Out] out double dLast,
            [Out] out double dAverage,
            [Out] out double dStdDev,
            [Out] out double dMin,
            [Out] out double dMax
            );

        [PreserveSig]
        int GetIndex(
            [In, MarshalAs(UnmanagedType.BStr)] string szName,
            [In, MarshalAs(UnmanagedType.Bool)] bool lCreate,
            [Out] out int plIndex
            );

        [PreserveSig]
        int AddValue(
            [In] int lIndex,
            [In] double dValue
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b4-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IVideoWindow
    {
        [PreserveSig]
        int put_Caption([In, MarshalAs(UnmanagedType.BStr)] string caption);

        [PreserveSig]
        int get_Caption([Out, MarshalAs(UnmanagedType.BStr)] out string caption);

        [PreserveSig]
        int put_WindowStyle([In] WindowStyle windowStyle);

        [PreserveSig]
        int get_WindowStyle([Out] out WindowStyle windowStyle);

        [PreserveSig]
        int put_WindowStyleEx([In] WindowStyleEx windowStyleEx);

        [PreserveSig]
        int get_WindowStyleEx([Out] out WindowStyleEx windowStyleEx);

        [PreserveSig]
        int put_AutoShow([In] OABool autoShow);

        [PreserveSig]
        int get_AutoShow([Out] out OABool autoShow);

        [PreserveSig]
        int put_WindowState([In] WindowState windowState);

        [PreserveSig]
        int get_WindowState([Out] out WindowState windowState);

        [PreserveSig]
        int put_BackgroundPalette([In] OABool backgroundPalette);

        [PreserveSig]
        int get_BackgroundPalette([Out] out OABool backgroundPalette);

        [PreserveSig]
        int put_Visible([In] OABool visible);

        [PreserveSig]
        int get_Visible([Out] out OABool visible);

        [PreserveSig]
        int put_Left([In] int left);

        [PreserveSig]
        int get_Left([Out] out int left);

        [PreserveSig]
        int put_Width([In] int width);

        [PreserveSig]
        int get_Width([Out] out int width);

        [PreserveSig]
        int put_Top([In] int top);

        [PreserveSig]
        int get_Top([Out] out int top);

        [PreserveSig]
        int put_Height([In] int height);

        [PreserveSig]
        int get_Height([Out] out int height);

        [PreserveSig]
        int put_Owner([In] IntPtr owner);

        [PreserveSig]
        int get_Owner([Out] out IntPtr owner);

        [PreserveSig]
        int put_MessageDrain([In] IntPtr drain);

        [PreserveSig]
        int get_MessageDrain([Out] out IntPtr drain);

        // Use ColorTranslator to break out RGB
        [PreserveSig]
        int get_BorderColor([Out] out int color);

        // Use ColorTranslator to break out RGB
        [PreserveSig]
        int put_BorderColor([In] int color);

        [PreserveSig]
        int get_FullScreenMode([Out] out OABool fullScreenMode);

        [PreserveSig]
        int put_FullScreenMode([In] OABool fullScreenMode);

        [PreserveSig]
        int SetWindowForeground([In] OABool focus);

        [PreserveSig]
        int NotifyOwnerMessage(
            [In] IntPtr hwnd, // HWND *
            [In] int msg,
            [In] IntPtr wParam, // WPARAM
            [In] IntPtr lParam // LPARAM
            );

        [PreserveSig]
        int SetWindowPosition(
            [In] int left,
            [In] int top,
            [In] int width,
            [In] int height
            );

        [PreserveSig]
        int GetWindowPosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        int GetMinIdealImageSize(
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        int GetMaxIdealImageSize(
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        int GetRestorePosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        int HideCursor([In] OABool hideCursor);

        [PreserveSig]
        int IsCursorHidden([Out] out OABool hideCursor);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b3-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBasicAudio
    {
        [PreserveSig]
        int put_Volume([In] int lVolume);

        [PreserveSig]
        int get_Volume([Out] out int plVolume);

        [PreserveSig]
        int put_Balance([In] int lBalance);

        [PreserveSig]
        int get_Balance([Out] out int plBalance);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b5-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBasicVideo
    {
        [PreserveSig]
        int get_AvgTimePerFrame([Out] out double pAvgTimePerFrame);

        [PreserveSig]
        int get_BitRate([Out] out int pBitRate);

        [PreserveSig]
        int get_BitErrorRate([Out] out int pBitRate);

        [PreserveSig]
        int get_VideoWidth([Out] out int pVideoWidth);

        [PreserveSig]
        int get_VideoHeight([Out] out int pVideoHeight);

        [PreserveSig]
        int put_SourceLeft([In] int SourceLeft);

        [PreserveSig]
        int get_SourceLeft([Out] out int pSourceLeft);

        [PreserveSig]
        int put_SourceWidth([In] int SourceWidth);

        [PreserveSig]
        int get_SourceWidth([Out] out int pSourceWidth);

        [PreserveSig]
        int put_SourceTop([In] int SourceTop);

        [PreserveSig]
        int get_SourceTop([Out] out int pSourceTop);

        [PreserveSig]
        int put_SourceHeight([In] int SourceHeight);

        [PreserveSig]
        int get_SourceHeight([Out] out int pSourceHeight);

        [PreserveSig]
        int put_DestinationLeft([In] int DestinationLeft);

        [PreserveSig]
        int get_DestinationLeft([Out] out int pDestinationLeft);

        [PreserveSig]
        int put_DestinationWidth([In] int DestinationWidth);

        [PreserveSig]
        int get_DestinationWidth([Out] out int pDestinationWidth);

        [PreserveSig]
        int put_DestinationTop([In] int DestinationTop);

        [PreserveSig]
        int get_DestinationTop([Out] out int pDestinationTop);

        [PreserveSig]
        int put_DestinationHeight([In] int DestinationHeight);

        [PreserveSig]
        int get_DestinationHeight([Out] out int pDestinationHeight);

        [PreserveSig]
        int SetSourcePosition(
            [In] int left,
            [In] int top,
            [In] int width,
            [In] int height
            );

        [PreserveSig]
        int GetSourcePosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        int SetDefaultSourcePosition();

        [PreserveSig]
        int SetDestinationPosition(
            [In] int left,
            [In] int top,
            [In] int width,
            [In] int height
            );

        [PreserveSig]
        int GetDestinationPosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        int SetDefaultDestinationPosition();

        [PreserveSig]
        int GetVideoSize(
            [Out] out int pWidth,
            [Out] out int pHeight
            );

        [PreserveSig]
        int GetVideoPaletteEntries(
            [In] int StartIndex,
            [In] int Entries,
            [Out] out int pRetrieved,
            [Out] out int[] pPalette
            );

        [PreserveSig]
        int GetCurrentImage(
            [In, Out] ref int pBufferSize,
            [Out] IntPtr pDIBImage // int *
            );

        [PreserveSig]
        int IsUsingDefaultSource();

        [PreserveSig]
        int IsUsingDefaultDestination();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("329bb360-f6ea-11d1-9038-00a0c9697298"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBasicVideo2 : IBasicVideo
    {
        #region IBasicVideo Methods

        [PreserveSig]
        new int get_AvgTimePerFrame([Out] out double pAvgTimePerFrame);

        [PreserveSig]
        new int get_BitRate([Out] out int pBitRate);

        [PreserveSig]
        new int get_BitErrorRate([Out] out int pBitRate);

        [PreserveSig]
        new int get_VideoWidth([Out] out int pVideoWidth);

        [PreserveSig]
        new int get_VideoHeight([Out] out int pVideoHeight);

        [PreserveSig]
        new int put_SourceLeft([In] int SourceLeft);

        [PreserveSig]
        new int get_SourceLeft([Out] out int pSourceLeft);

        [PreserveSig]
        new int put_SourceWidth([In] int SourceWidth);

        [PreserveSig]
        new int get_SourceWidth([Out] out int pSourceWidth);

        [PreserveSig]
        new int put_SourceTop([In] int SourceTop);

        [PreserveSig]
        new int get_SourceTop([Out] out int pSourceTop);

        [PreserveSig]
        new int put_SourceHeight([In] int SourceHeight);

        [PreserveSig]
        new int get_SourceHeight([Out] out int pSourceHeight);

        [PreserveSig]
        new int put_DestinationLeft([In] int DestinationLeft);

        [PreserveSig]
        new int get_DestinationLeft([Out] out int pDestinationLeft);

        [PreserveSig]
        new int put_DestinationWidth([In] int DestinationWidth);

        [PreserveSig]
        new int get_DestinationWidth([Out] out int pDestinationWidth);

        [PreserveSig]
        new int put_DestinationTop([In] int DestinationTop);

        [PreserveSig]
        new int get_DestinationTop([Out] out int pDestinationTop);

        [PreserveSig]
        new int put_DestinationHeight([In] int DestinationHeight);

        [PreserveSig]
        new int get_DestinationHeight([Out] out int pDestinationHeight);

        [PreserveSig]
        new int SetSourcePosition(
            [In] int left,
            [In] int top,
            [In] int width,
            [In] int height
            );

        [PreserveSig]
        new int GetSourcePosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        new int SetDefaultSourcePosition();

        [PreserveSig]
        new int SetDestinationPosition(
            [In] int left,
            [In] int top,
            [In] int width,
            [In] int height
            );

        [PreserveSig]
        new int GetDestinationPosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        new int SetDefaultDestinationPosition();

        [PreserveSig]
        new int GetVideoSize(
            [Out] out int pWidth,
            [Out] out int pHeight
            );

        [PreserveSig]
        new int GetVideoPaletteEntries(
            [In] int StartIndex,
            [In] int Entries,
            [Out] out int pRetrieved,
            [Out] out int[] pPalette
            );

        [PreserveSig]
        new int GetCurrentImage(
            [In, Out] ref int pBufferSize,
            [Out] IntPtr pDIBImage // int *
            );

        [PreserveSig]
        new int IsUsingDefaultSource();

        [PreserveSig]
        new int IsUsingDefaultDestination();

        #endregion

        [PreserveSig]
        int GetPreferredAspectRatio(
            [Out] out int plAspectX,
            [Out] out int plAspectY
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b6-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEvent
    {
        [PreserveSig]
        int GetEventHandle([Out] out IntPtr hEvent); // HEVENT

        [PreserveSig]
        int GetEvent(
            [Out] out EventCode lEventCode,
            [Out] out IntPtr lParam1,
            [Out] out IntPtr lParam2,
            [In] int msTimeout
            );

        [PreserveSig]
        int WaitForCompletion(
            [In] int msTimeout,
            [Out] out EventCode pEvCode
            );

        [PreserveSig]
        int CancelDefaultHandling([In] EventCode lEvCode);

        [PreserveSig]
        int RestoreDefaultHandling([In] EventCode lEvCode);

        [PreserveSig]
        int FreeEventParams(
            [In] EventCode lEvCode,
            [In] IntPtr lParam1,
            [In] IntPtr lParam2
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868c0-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEventEx : IMediaEvent
    {
        #region IMediaEvent Methods

        [PreserveSig]
        new int GetEventHandle([Out] out IntPtr hEvent); // HEVENT

        [PreserveSig]
        new int GetEvent(
            [Out] out EventCode lEventCode,
            [Out] out IntPtr lParam1,
            [Out] out IntPtr lParam2,
            [In] int msTimeout
            );

        [PreserveSig]
        new int WaitForCompletion(
            [In] int msTimeout,
            [Out] out EventCode pEvCode
            );

        [PreserveSig]
        new int CancelDefaultHandling([In] EventCode lEvCode);

        [PreserveSig]
        new int RestoreDefaultHandling([In] EventCode lEvCode);

        [PreserveSig]
        new int FreeEventParams(
            [In] EventCode lEvCode,
            [In] IntPtr lParam1,
            [In] IntPtr lParam2
            );

        #endregion

        [PreserveSig]
        int SetNotifyWindow(
            [In] IntPtr hwnd, // HWND *
            [In] int lMsg,
            [In] IntPtr lInstanceData // PVOID
            );

        [PreserveSig]
        int SetNotifyFlags([In] NotifyFlags lNoNotifyFlags);

        [PreserveSig]
        int GetNotifyFlags([Out] out NotifyFlags lplNoNotifyFlags);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b2-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaPosition
    {
        [PreserveSig]
        int get_Duration([Out] out double pLength);

        [PreserveSig]
        int put_CurrentPosition([In] double llTime);

        [PreserveSig]
        int get_CurrentPosition([Out] out double pllTime);

        [PreserveSig]
        int get_StopTime([Out] out double pllTime);

        [PreserveSig]
        int put_StopTime([In] double llTime);

        [PreserveSig]
        int get_PrerollTime([Out] out double pllTime);

        [PreserveSig]
        int put_PrerollTime([In] double llTime);

        [PreserveSig]
        int put_Rate([In] double dRate);

        [PreserveSig]
        int get_Rate([Out] out double pdRate);

        [PreserveSig]
        int CanSeekForward([Out] out OABool pCanSeekForward);

        [PreserveSig]
        int CanSeekBackward([Out] out OABool pCanSeekBackward);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b1-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaControl
    {
        [PreserveSig]
        int Run();

        [PreserveSig]
        int Pause();

        [PreserveSig]
        int Stop();

        [PreserveSig]
        int GetState([In] int msTimeout, [Out] out FilterState pfs);

        [PreserveSig]
        int RenderFile([In, MarshalAs(UnmanagedType.BStr)] string strFilename);

        [PreserveSig,
        Obsolete("Automation interface, for pre-.NET VB.  Use IGraphBuilder::AddSourceFilter instead", false)]
        int AddSourceFilter(
            [In, MarshalAs(UnmanagedType.BStr)] string strFilename,
            [Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk
            );

        [PreserveSig,
        Obsolete("Automation interface, for pre-.NET VB.  Use IFilterGraph::EnumFilters instead", false)]
        int get_FilterCollection([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

        [PreserveSig,
        Obsolete("Automation interface, for pre-.NET VB.  Use IFilterMapper2::EnumMatchingFilters instead", false)]
        int get_RegFilterCollection([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

        [PreserveSig]
        int StopWhenReady();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b7-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IQueueCommand
    {
        [PreserveSig]
        int InvokeAtStreamTime(
            [Out] out IDeferredCommand pCmd,
            [In] double time,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid iid,
            [In] int dispidMethod,
            [In] DispatchFlags wFlags,
            [In] int cArgs,
            [In] object[] pDispParams,
            [In] IntPtr pvarResult,
            [Out] out short puArgErr
            );

        int InvokeAtPresentationTime(
            [Out] out IDeferredCommand pCmd,
            [In] double time,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid iid,
            [In] int dispidMethod,
            [In] DispatchFlags wFlags,
            [In] int cArgs,
            [In] object[] pDispParams,
            [In] IntPtr pvarResult,
            [Out] out short puArgErr
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("56a868b8-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDeferredCommand
    {
        [PreserveSig]
        int Cancel();

        [PreserveSig]
        int Confidence([Out] out int pConfidence);

        [PreserveSig]
        int Postpone([In] double newtime);

        [PreserveSig]
        int GetHResult([Out] out int phrResult);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICreateDevEnum
    {
        [PreserveSig]
        int CreateClassEnumerator(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pType,
            [Out] out IEnumMoniker ppEnumMoniker,
            [In] CDef dwFlags);
    }

    #endregion
}