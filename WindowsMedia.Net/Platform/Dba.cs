using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace WindowsMedia.Platform
{
    #region Declarations

    /// <summary>
    /// From ATSC_ETM_LOCATION_*
    /// </summary>
    public enum AtscEtmLocation
    {
        /// <summary>
        /// ATSC_ETM_LOCATION_NOT_PRESENT
        /// </summary>
        NotPresent = 0x00,

        /// <summary>
        /// ATSC_ETM_LOCATION_IN_PTC_FOR_PSIP
        /// </summary>
        InPtcForPsip = 0x01,

        /// <summary>
        /// ATSC_ETM_LOCATION_IN_PTC_FOR_EVENT
        /// </summary>
        InPtcForEvent = 0x02,

        /// <summary>
        /// ATSC_ETM_LOCATION_RESERVED
        /// </summary>
        Reserved = 0x03,
    }

    /// <summary>
    /// From BDA_EVENT_ID
    /// </summary>
    public enum BDAEventID
    {
        SignalLoss = 0,
        SignalLock,
        DataStart,
        DataStop,
        ChannelAcquired,
        ChannelLost,
        ChannelSourceChanged,
        ChannelActivated,
        ChannelDeactivated,
        SubChannelAcquired,
        SubChannelLost,
        SubChannelSourceChanged,
        SubChannelActivated,
        SubChannelDeactivated,
        AccessGranted,
        AccessDenied,
        OfferExtended,
        PurchaseCompleted,
        SmartCardInserted,
        SmartCardRemoved
    }

    /// <summary>
    /// From BDA_TEMPLATE_PIN_JOINT
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BDATemplatePinJoint
    {
        public int uliTemplateConnection;
        public int ulcInstancesMax;
    }

    /// <summary>
    /// From KS_BDA_FRAME_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KSBDAFrameInfo
    {
        public int ExtendedHeaderSize; // Size of this extended header
        public int dwFrameFlags; //
        public int ulEvent; //
        public int ulChannelNumber; //
        public int ulSubchannelNumber; //
        public int ulReason; //
    }

    /// <summary>
    /// From MPEG2_TRANSPORT_STRIDE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MPEG2TransportStride
    {
        public int dwOffset;
        public int dwPacketLength;
        public int dwStride;
    }

    /// <summary>
    /// From ScanModulationTypes
    /// </summary>
    [Flags]
    public enum ScanModulationTypes
    {
        ScanMod16QAM = 0x00000001,
        ScanMod32QAM = 0x00000002,
        ScanMod64QAM = 0x00000004,
        ScanMod80QAM = 0x00000008,
        ScanMod96QAM = 0x00000010,
        ScanMod112QAM = 0x00000020,
        ScanMod128QAM = 0x00000040,
        ScanMod160QAM = 0x00000080,
        ScanMod192QAM = 0x00000100,
        ScanMod224QAM = 0x00000200,
        ScanMod256QAM = 0x00000400,
        ScanMod320QAM = 0x00000800,
        ScanMod384QAM = 0x00001000,
        ScanMod448QAM = 0x00002000,
        ScanMod512QAM = 0x00004000,
        ScanMod640QAM = 0x00008000,
        ScanMod768QAM = 0x00010000,
        ScanMod896QAM = 0x00020000,
        ScanMod1024QAM = 0x00040000,
        ScanModQPSK = 0x00080000,
        ScanModBPSK = 0x00100000,
        ScanModOQPSK = 0x00200000,
        ScanMod8VSB = 0x00400000,
        ScanMod16VSB = 0x00800000,
        ScanModAM_RADIO = 0x01000000,
        ScanModFM_RADIO = 0x02000000,
        ScanMod8PSK = 0x04000000,
        ScanModRF = 0x08000000,
        MCEDigitalCable = ModulationType.Mod640Qam | ModulationType.Mod256Qam,
        MCETerrestrialATSC = ModulationType.Mod8Vsb,
        MCEAnalogTv = ModulationType.ModRF,
        MCEAll_TV = unchecked((int)0xffffffff),
    }

    /// <summary>
    /// From RollOff
    /// </summary>
    public enum RollOff
    {
        NotSet = -1,
        NotDefined = 0,
        Twenty = 1,
        TwentyFive,
        ThirtyFive,
        Max
    }

    /// <summary>
    /// From Pilot
    /// </summary>
    public enum Pilot
    {
        NotSet = -1,
        NotDefined = 0,
        Off = 1,
        On,
        Max
    }

    /// <summary>
    /// From ApplicationTypeType
    /// </summary>
    public enum ApplicationTypeType
    {
        SCTE28ConditionalAccess = 0,
        SCTE28PODHostBindingInformation,
        SCTE28IPService,
        SCTE28NetworkInterfaceSCTE55_2,
        SCTE28NetworkInterfaceSCTE55_1,
        SCTE28CopyProtection,
        SCTE28Diagnostic,
        SCTE28Undesignated,
        SCTE28Reserved,
    }

    /// <summary>
    /// From FECMethod
    /// </summary>
    public enum FECMethod
    {
        MethodNotSet = -1,
        MethodNotDefined = 0,
        Viterbi = 1, // FEC is a Viterbi Binary Convolution.
        RS204_188, // The FEC is Reed-Solomon 204/188 (outer FEC)
        Ldpc,
        Bch,
        RS147_130,
        Max,
    }

    /// <summary>
    /// From BinaryConvolutionCodeRate
    /// </summary>
    public enum BinaryConvolutionCodeRate
    {
        RateNotSet = -1,
        RateNotDefined = 0,
        Rate1_2 = 1, // 1/2
        Rate2_3, // 2/3
        Rate3_4, // 3/4
        Rate3_5,
        Rate4_5,
        Rate5_6, // 5/6
        Rate5_11,
        Rate7_8, // 7/8
        Rate1_4,
        Rate1_3,
        Rate2_5,
        Rate6_7,
        Rate8_9,
        Rate9_10,
        RateMax
    }

    /// <summary>
    /// From Polarisation
    /// </summary>
    public enum Polarisation
    {
        NotSet = -1,
        NotDefined = 0,
        LinearH = 1, // Linear horizontal polarisation
        LinearV, // Linear vertical polarisation
        CircularL, // Circular left polarisation
        CircularR, // Circular right polarisation
        Max,
    }

    /// <summary>
    /// From SpectralInversion
    /// </summary>
    public enum SpectralInversion
    {
        NotSet = -1,
        NotDefined = 0,
        Automatic = 1,
        Normal,
        Inverted,
        Max
    }

    /// <summary>
    /// From ModulationType
    /// </summary>
    public enum ModulationType
    {
        ModNotSet = -1,
        ModNotDefined = 0,
        Mod16Qam = 1,
        Mod32Qam,
        Mod64Qam,
        Mod80Qam,
        Mod96Qam,
        Mod112Qam,
        Mod128Qam,
        Mod160Qam,
        Mod192Qam,
        Mod224Qam,
        Mod256Qam,
        Mod320Qam,
        Mod384Qam,
        Mod448Qam,
        Mod512Qam,
        Mod640Qam,
        Mod768Qam,
        Mod896Qam,
        Mod1024Qam,
        ModQpsk,
        ModBpsk,
        ModOqpsk,
        Mod8Vsb,
        Mod16Vsb,
        ModAnalogAmplitude, // std am
        ModAnalogFrequency, // std fm
        Mod8Psk,
        ModRF,
        Mod16Apsk,
        Mod32Apsk,
        ModNbcQpsk,
        ModNbc8Psk,
        ModDirectTv,
        ModMax
    }

    /// <summary>
    /// From DVBSystemType
    /// </summary>
    public enum DVBSystemType
    {
        Cable,
        Terrestrial,
        Satellite,
    }

    /// <summary>
    /// From HierarchyAlpha
    /// </summary>
    public enum HierarchyAlpha
    {
        HAlphaNotSet = -1,
        HAlphaNotDefined = 0,
        HAlpha1 = 1, // Hierarchy alpha is 1.
        HAlpha2, // Hierarchy alpha is 2.
        HAlpha4, // Hierarchy alpha is 4.
        HAlphaMax,
    }

    /// <summary>
    /// From GuardInterval
    /// </summary>
    public enum GuardInterval
    {
        GuardNotSet = -1,
        GuardNotDefined = 0,
        Guard1_32 = 1, // Guard interval is 1/32
        Guard1_16, // Guard interval is 1/16
        Guard1_8, // Guard interval is 1/8
        Guard1_4, // Guard interval is 1/4
        GuardMax,
    }

    /// <summary>
    /// From TransmissionMode
    /// </summary>
    public enum TransmissionMode
    {
        ModeNotSet = -1,
        ModeNotDefined = 0,
        Mode2K = 1, // Transmission uses 1705 carriers (use a 2K FFT)
        Mode8K, // Transmission uses 6817 carriers (use an 8K FFT)
        Mode4K,
        Mode2KInterleaved,
        Mode4KInterleaved,
        ModeMax,
    }

    /// <summary>
    /// From ComponentStatus
    /// </summary>
    public enum ComponentStatus
    {
        Active,
        Inactive,
        Unavailable
    }

    /// <summary>
    /// From ComponentCategory
    /// </summary>
    public enum ComponentCategory
    {
        NotSet = -1,
        Other = 0,
        Video,
        Audio,
        Text,
        Data
    }

    /// <summary>
    /// From MPEG2StreamType
    /// </summary>
    public enum MPEG2StreamType
    {
        BdaUninitializedMpeg2StreamType = -1,
        Reserved1 = 0x00,
        IsoIec11172_2_Video = 0x01,
        IsoIec13818_2_Video = 0x02,
        IsoIec11172_3_Audio = 0x03,
        IsoIec13818_3_Audio = 0x04,
        IsoIec13818_1_PrivateSection = 0x05,
        IsoIec13818_1_Pes = 0x06,
        IsoIec13522_Mheg = 0x07,
        AnnexADsmCC = 0x08,
        ItuTRecH222_1 = 0x09,
        IsoIec13818_6_TypeA = 0x0a,
        IsoIec13818_6_TypeB = 0x0b,
        IsoIec13818_6_TypeC = 0x0c,
        IsoIec13818_6_TypeD = 0x0d,
        IsoIec13818_1_Auxiliary = 0x0e,
        IsoIec13818_1_Reserved = 0x0f,
        UserPrivate = 0x10,
        IsoIecUserPrivate = 0x80,
        DolbyAc3Audio = 0x81
    }

    /// <summary>
    /// From ATSCComponentTypeFlags
    /// </summary>
    [Flags]
    public enum ATSCComponentTypeFlags
    {
        None = 0x0,
        ATSCCT_AC3 = 0x00000001
    }

    /// <summary>
    /// From BDA_TEMPLATE_CONNECTION
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BDATemplateConnection
    {
        public int FromNodeType;
        public int FromNodePinType;
        public int ToNodeType;
        public int ToNodePinType;
    }

    /// <summary>
    /// From BDA_Comp_Flags
    /// </summary>
    [Flags]
    public enum BDACompFlags
    {
        NotDefined = 0x00000000, // BDACOMP_NOT_DEFINED
        ExcludeTSFromTR = 0x00000001, // BDACOMP_EXCLUDE_TS_FROM_TR
        IncludeLocatorInTR = 0x00000002, // BDACOMP_INCLUDE_LOCATOR_IN_TR
    }

    /// <summary>
    /// From CLSID_TIFLoad
    /// </summary>
    [ComImport, Guid("14EB8748-1753-4393-95AE-4F7E7A87AAD6")]
    public class TIFLoad
    {
    }

    #endregion

    #region Interfaces

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("DFEF4A68-EE61-415f-9CCB-CD95F2F98A3A"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_TIF_REGISTRATION
    {
        [PreserveSig]
        int RegisterTIFEx(
          [In] IPin pTIFInputPin,
          [Out] out int ppvRegistrationContext,
          [Out, MarshalAs(UnmanagedType.Interface)] out object ppMpeg2DataControl
          );

        [PreserveSig]
        int UnregisterTIF([In] int pvRegistrationContext);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("F9BAC2F9-4149-4916-B2EF-FAA202326862"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMPEG2_TIF_CONTROL
    {
        [PreserveSig]
        int RegisterTIF(
            [In, MarshalAs(UnmanagedType.Interface)] object pUnkTIF,
            [In, Out] ref int ppvRegistrationContext
            );

        [PreserveSig]
        int UnregisterTIF([In] int pvRegistrationContext);

        [PreserveSig]
        int AddPIDs(
            [In] int ulcPIDs,
            [In] ref int pulPIDs
            );

        [PreserveSig]
        int DeletePIDs(
            [In] int ulcPIDs,
            [In] ref int pulPIDs
            );

        [PreserveSig]
        int GetPIDCount([Out] out int pulcPIDs);

        [PreserveSig]
        int GetPIDs(
            [Out] out int pulcPIDs,
            [Out] out int pulPIDs
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("A3B152DF-7A90-4218-AC54-9830BEE8C0B6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITuneRequestInfo
    {
        [PreserveSig]
        int GetLocatorData([In] ITuneRequest Request);

        [PreserveSig]
        int GetComponentData([In] ITuneRequest CurrentRequest);

        [PreserveSig]
        int CreateComponentList([In] ITuneRequest CurrentRequest);

        [PreserveSig]
        int GetNextProgram(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        int GetPreviousProgram(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        int GetNextLocator(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        int GetPreviousLocator(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("EFDA0C80-F395-42c3-9B3C-56B37DEC7BB7"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideDataEvent
    {
        [PreserveSig]
        int GuideDataAcquired();

        [PreserveSig]
        int ProgramChanged([In] object varProgramDescriptionID);

        [PreserveSig]
        int ServiceChanged([In] object varProgramDescriptionID);

        [PreserveSig]
        int ScheduleEntryChanged([In] object varProgramDescriptionID);

        [PreserveSig]
        int ProgramDeleted([In] object varProgramDescriptionID);

        [PreserveSig]
        int ServiceDeleted([In] object varProgramDescriptionID);

        [PreserveSig]
        int ScheduleDeleted([In] object varProgramDescriptionID);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("88EC5E58-BB73-41d6-99CE-66C524B8B591"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideDataProperty
    {
        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string pbstrName);

        [PreserveSig]
        int get_Language([Out] out int idLang);

        [PreserveSig]
        int get_Value([Out] out object pvar);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("AE44423B-4571-475c-AD2C-F40A771D80EF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumGuideDataProperties
    {
        [PreserveSig]
        int Next(
            [In] int celt,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IGuideDataProperty[] ppprop,
            [In] IntPtr pcelt
            );

        [PreserveSig]
        int Skip([In] int celt);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumGuideDataProperties ppenum);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("1993299C-CED6-4788-87A3-420067DCE0C7"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumTuneRequests
    {
        [PreserveSig]
        int Next(
            [In] int celt,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ITuneRequest[] ppprop,
            [In] IntPtr pcelt
            );

        [PreserveSig]
        int Skip([In] int celt);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumTuneRequests ppenum);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("61571138-5B01-43cd-AEAF-60B784A0BF93"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideData
    {
        [PreserveSig]
        int GetServices([Out] out IEnumTuneRequests ppEnumTuneRequests);

        [PreserveSig]
        int GetServiceProperties(
            [In] ITuneRequest pTuneRequest,
            [Out] out IEnumGuideDataProperties ppEnumProperties
            );

        [PreserveSig]
        int GetGuideProgramIDs([Out] out IEnumVARIANT pEnumPrograms);

        [PreserveSig]
        int GetProgramProperties(
            [In] object varProgramDescriptionID,
            [Out] out IEnumGuideDataProperties ppEnumProperties
            );

        [PreserveSig]
        int GetScheduleEntryIDs([Out] out IEnumVARIANT pEnumScheduleEntries);

        [PreserveSig]
        int GetScheduleEntryProperties(
            [In] object varScheduleEntryDescriptionID,
            [Out] out IEnumGuideDataProperties ppEnumProperties
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("4764ff7c-fa95-4525-af4d-d32236db9e38"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideDataLoader
    {
        [PreserveSig]
        int Init([In] IGuideData pGuideStore);

        [PreserveSig]
        int Terminate();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("EE957C52-B0D0-4e78-8DD1-B87A08BFD893"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITuneRequestInfoEx : ITuneRequestInfo
    {
        #region ITuneRequestInfo Methods

        [PreserveSig]
        new int GetLocatorData(
            [In] ITuneRequest Request
            );

        [PreserveSig]
        new int GetComponentData(
            [In] ITuneRequest CurrentRequest
            );

        [PreserveSig]
        new int CreateComponentList(
            [In] ITuneRequest CurrentRequest
            );

        [PreserveSig]
        new int GetNextProgram(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        new int GetPreviousProgram(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        new int GetNextLocator(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        new int GetPreviousLocator(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        #endregion

        [PreserveSig]
        int CreateComponentListEx(
            ITuneRequest CurrentRequest,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppCurPMT
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("7E47913A-5A89-423d-9A2B-E15168858934"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISIInbandEPGEvent
    {
        [PreserveSig]
        int SIObjectEvent(
            IDVB_EIT2 pIDVB_EIT,
            int dwTable_ID,
            int dwService_ID
            );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("F90AD9D0-B854-4b68-9CC1-B2CC96119D85"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISIInbandEPG
    {
        [PreserveSig]
        int StartSIEPGScan();

        [PreserveSig]
        int StopSIEPGScan();

        [PreserveSig]
        int IsSIEPGScanRunning(
            [MarshalAs(UnmanagedType.Bool)] out bool bRunning
            );
    }

    /// <summary>
    /// The <see cref="IAtscPsipParser"/> interface retrieves ATSC Program and System Information Protocol (PSIP) tables.
    /// </summary>
    [ComImport, SuppressUnmanagedCodeSecurity, Guid("B2C98995-5EB2-4fb1-B406-F3E8E2026A9A"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAtscPsipParser
    {
        /// <summary>
        /// Initializes the ATSC PSIP parser.
        /// </summary>
        /// <param name="punkMpeg2Data">Pointer to the <see cref="IUnknown"/> interface of the MPEG-2 Sections and Tables Filter or another object that implements 
        /// the <see cref="IMpeg2Data"/> interface.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_NOINTERFACE</b> - The <paramref name="punkMpeg2Data"/> pointer does not expose the <see cref="IMpeg2Data"/> interface.</item>
        /// <item><b>E_POINTER</b> - <b>null</b> reference.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>Until this method is called, all other methods on this interface fail.</remarks>
        [PreserveSig]
        int Initialize([In] IMpeg2Data punkMpeg2Data);

        /// <summary>
        /// Retrieves the program association table (PAT).
        /// </summary>
        /// <param name="ppPat">Receives an <see cref="IPAT"/> interface pointer. The caller must release the interface.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_OUTOFMEMORY</b> - Insufficient memory.</item>
        /// <item><b>E_POINTER</b> - <b>null</b> reference.</item>
        /// <item><b>MPEG2_E_SECTION_NOT_FOUND</b> - The filter did not receive the table in the allotted time.</item>
        /// <item><b>MPEG2_E_UNINITIALIZED</b> - The <see cref="Initialize"/> method was not called.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>The method fails if the filter does not receive a matching table within a predetermined length of time.</remarks>
        [PreserveSig]
        int GetPAT([Out] out IPAT ppPat);

        [PreserveSig]
        int GetCAT([In] int dwTimeout, [Out] out ICAT ppCAT);

        [PreserveSig]
        int GetPMT([In] short pid, [In] IntPtr pwProgramNumber, [Out] out IPMT ppPMT);

        [PreserveSig]
        int GetTSDT([Out] out ITSDT ppTSDT);

        [PreserveSig]
        int GetMGT([Out] out IATSC_MGT ppMGT);

        [PreserveSig]
        int GetVCT(
            [In] byte tableId,
            [In, MarshalAs(UnmanagedType.Bool)] bool fGetNextTable,
            [Out] out IATSC_VCT ppVCT);

        [PreserveSig]
        int GetEIT([In] short pid, [In] IntPtr pwSourceId, [In] int dwTimeout, [Out] out IATSC_EIT ppEIT);

        [PreserveSig]
        int GetETT([In] short pid, [In] IntPtr wSourceId, [In] IntPtr pwEventId, [Out] out IATSC_ETT ppETT);

        [PreserveSig]
        int GetSTT([Out] out IATSC_STT ppSTT);

        [PreserveSig]
        int GetEAS([In] short pid, [Out] out ISCTE_EAS ppEAS);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("8877dabd-c137-4073-97e3-779407a5d87a"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IATSC_MGT
    {
        [PreserveSig]
        int Initialize([In] ISectionList pSectionList, [In] IMpeg2Data pMPEGData);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetProtocolVersion([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfRecords([Out] out int pdwVal);

        [PreserveSig]
        int GetRecordType([In] int dwRecordIndex, [Out] out short pwVal);

        [PreserveSig]
        int GetRecordTypePid([In] int dwRecordIndex, [Out] out short ppidVal);

        [PreserveSig]
        int GetRecordVersionNumber([In] int dwRecordIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordCountOfDescriptors([In] int dwRecordIndex, [Out] out int pdwVal);

        [PreserveSig]
        int GetRecordDescriptorByIndex(
            [In] int dwRecordIndex,
            [In] int dwIndex,
            [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetRecordDescriptorByTag(
            [In] int dwRecordIndex,
            [In] byte bTag,
            [In, Out] DsInt pdwCookie,
            [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetCountOfTableDescriptors([In, Out] ref int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex([In] int dwIndex, [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetTableDescriptorByTag([In] byte bTag, [In] IntPtr pdwCookie, [Out] out IGenericDescriptor ppDescriptor);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("26879a18-32f9-46c6-91f0-fb6479270e8c"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IATSC_VCT
    {
        [PreserveSig]
        int Initialize([In] ISectionList pSectionList, [In] IMpeg2Data pMPEGData);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetTransportStreamId([Out] out short pwVal);

        [PreserveSig]
        int GetProtocolVersion([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfRecords([Out] out int pdwVal);

        [PreserveSig]
        int GetRecordName([In] int dwRecordIndex, [Out, MarshalAs(UnmanagedType.LPWStr)] out string pwsName);

        [PreserveSig]
        int GetRecordMajorChannelNumber([In] int dwRecordIndex, [Out] out short pwVal);

        [PreserveSig]
        int GetRecordMinorChannelNumber([In] int dwRecordIndex, [Out] out short pwVal);

        [PreserveSig]
        int GetRecordModulationMode([In] int dwRecordIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordCarrierFrequency([In] int dwRecordIndex, [Out] out int pdwVal);

        [PreserveSig]
        int GetRecordTransportStreamId([In] int dwRecordIndex, [Out] out short pwVal);

        [PreserveSig]
        int GetRecordProgramNumber([In] int dwRecordIndex, [Out] out short pwVal);

        [PreserveSig]
        int GetRecordEtmLocation([In] int dwRecordIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordIsAccessControlledBitSet(
            [In] int dwRecordIndex,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pfVal);

        [PreserveSig]
        int GetRecordIsHiddenBitSet([In] int dwRecordIndex, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfVal);

        [PreserveSig]
        int GetRecordIsPathSelectBitSet([In] int dwRecordIndex, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfVal);

        [PreserveSig]
        int GetRecordIsOutOfBandBitSet([In] int dwRecordIndex, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfVal);

        [PreserveSig]
        int GetRecordIsHideGuideBitSet([In] int dwRecordIndex, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfVal);

        [PreserveSig]
        int GetRecordServiceType([In] int dwRecordIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordSourceId([In] int dwRecordIndex, [Out] out short pwVal);

        [PreserveSig]
        int GetRecordCountOfDescriptors([In] int dwRecordIndex, [Out] out int pdwVal);

        [PreserveSig]
        int GetRecordDescriptorByIndex(
            [In] int dwRecordIndex,
            [In] int dwIndex,
            [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetRecordDescriptorByTag(
            [In] int dwRecordIndex,
            [In] byte bTag,
            [In, Out] DsInt pdwCookie,
            [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetCountOfTableDescriptors([In, Out] ref int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex([In] int dwIndex, [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetTableDescriptorByTag([In] byte bTag, [In] IntPtr pdwCookie, [Out] out IGenericDescriptor ppDescriptor);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("d7c212d7-76a2-4b4b-aa56-846879a80096"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IATSC_EIT
    {
        [PreserveSig]
        int Initialize([In] ISectionList pSectionList, [In] IMpeg2Data pMPEGData);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetSourceId([Out] out short pwVal);

        [PreserveSig]
        int GetProtocolVersion([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfRecords([Out] out int pdwVal);

        [PreserveSig]
        int GetRecordEventId([In] int dwRecordIndex, [Out] out short pwVal);

        [PreserveSig]
        int GetRecordStartTime([In] int dwRecordIndex, [Out] out MpegDateAndTime pmdtVal);

        [PreserveSig]
        int GetRecordEtmLocation([In] int dwRecordIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordDuration([In] int dwRecordIndex, [Out] out MpegDuration pmdVal);

        [PreserveSig]
        int GetRecordTitleText([In] int dwRecordIndex, [Out] out int pdwLength, [Out] out IntPtr ppText);

        [PreserveSig]
        int GetRecordCountOfDescriptors([In] int dwRecordIndex, [Out] out int pdwVal);

        [PreserveSig]
        int GetRecordDescriptorByIndex(
            [In] int dwRecordIndex,
            [In] int dwIndex,
            [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetRecordDescriptorByTag(
            [In] int dwRecordIndex,
            [In] byte bTag,
            [In, Out] DsInt pdwCookie,
            [Out] out IGenericDescriptor ppDescriptor);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("5a142cc9-b8cf-4a86-a040-e9cadf3ef3e7"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IATSC_ETT
    {
        [PreserveSig]
        int Initialize([In] ISectionList pSectionList, [In] IMpeg2Data pMPEGData);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetProtocolVersion([Out] out byte pbVal);

        [PreserveSig]
        int GetEtmId([Out] out int pdwVal);

        int GetExtendedMessageText([Out] out int pdwLength, [Out] out IntPtr ppText);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("6bf42423-217d-4d6f-81e1-3a7b360ec896"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IATSC_STT
    {
        [PreserveSig]
        int Initialize([In] ISectionList pSectionList, [In] IMpeg2Data pMPEGData);

        [PreserveSig]
        int GetProtocolVersion([Out] out byte pbVal);

        [PreserveSig]
        int GetSystemTime([Out] out MpegDateAndTime pmdtSystemTime);

        [PreserveSig]
        int GetGpsUtcOffset([Out] out byte pbVal);

        [PreserveSig]
        int GetDaylightSavings([Out] out short pwVal);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex([In] int dwIndex, [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetTableDescriptorByTag([In] byte bTag, [In] IntPtr pdwCookie, [Out] out IGenericDescriptor ppDescriptor);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("1FF544D6-161D-4fae-9FAA-4F9F492AE999"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISCTE_EAS
    {
        [PreserveSig]
        int Initialize([In] ISectionList pSectionList, [In] IMpeg2Data pMPEGData);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetSequencyNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetProtocolVersion([Out] out byte pbVal);

        [PreserveSig]
        int GetEASEventID([Out] out short pwVal);

        [PreserveSig]
        int GetOriginatorCode([Out] out byte pbVal);

        [PreserveSig]
        int GetEASEventCodeLen([Out] out byte pbVal);

        [PreserveSig]
        int GetEASEventCode([Out] out byte pbVal);

        [PreserveSig]
        int GetRawNatureOfActivationTextLen([Out] out byte pbVal);

        [PreserveSig]
        int GetRawNatureOfActivationText([Out] out byte pbVal);

        [PreserveSig]
        int GetNatureOfActivationText(
            [In, MarshalAs(UnmanagedType.BStr)] string bstrIS0639code,
            [Out, MarshalAs(UnmanagedType.BStr)] out string pbstrString);

        [PreserveSig]
        int GetTimeRemaining([Out] out byte pbVal);

        [PreserveSig]
        int GetStartTime([Out] out int pdwVal);

        [PreserveSig]
        int GetDuration([Out] out short pwVal);

        [PreserveSig]
        int GetAlertPriority([Out] out byte pbVal);

        [PreserveSig]
        int GetDetailsOOBSourceID([Out] out short pwVal);

        [PreserveSig]
        int GetDetailsMajor([Out] out short pwVal);

        [PreserveSig]
        int GetDetailsMinor([Out] out short pwVal);

        [PreserveSig]
        int GetDetailsAudioOOBSourceID([Out] out short pwVal);

        [PreserveSig]
        int GetAlertText(
            [In, MarshalAs(UnmanagedType.BStr)] string bstrIS0639code,
            [Out, MarshalAs(UnmanagedType.BStr)] out string pbstrString);

        [PreserveSig]
        int GetRawAlertTextLen([Out] out short pwVal);

        [PreserveSig]
        int GetRawAlertText([Out] out byte pbVal);

        [PreserveSig]
        int GetLocationCount([Out] out byte pbVal);

        [PreserveSig]
        int GetLocationCodes(
            [In] byte bIndex,
            [Out] out byte pbState,
            [Out] out byte pbCountySubdivision,
            [Out] out short pwCounty);

        [PreserveSig]
        int GetExceptionCount([Out] out byte pbVal);

        [PreserveSig]
        int GetExceptionService(
            [In] byte bIndex,
            [Out] out byte pbIBRef,
            [Out] out byte pwFirst,
            [Out] out short pwSecond);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex([In] int dwIndex, [Out] out IGenericDescriptor ppDescriptor);

        [PreserveSig]
        int GetTableDescriptorByTag([In] byte bTag, [In] IntPtr pdwCookie, [Out] out IGenericDescriptor ppDescriptor);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("FF76E60C-0283-43ea-BA32-B422238547EE"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAtscContentAdvisoryDescriptor
    {
        [PreserveSig]
        int GetTag([Out] out byte pbVal);

        [PreserveSig]
        int GetLength([Out] out byte pbVal);

        [PreserveSig]
        int GetRatingRegionCount([Out] out byte pbVal);

        [PreserveSig]
        int GetRecordRatingRegion([In] byte bIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordRatedDimensions([In] byte bIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordRatingDimension([In] byte bIndexOuter, [In] byte bIndexInner, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordRatingValue([In] byte bIndexOuter, [In] byte bIndexInner, [Out] out byte pbVal);

        [PreserveSig]
        int GetRecordRatingDescriptionText([In] byte bIndex, [Out] out byte pbLength, [Out] out IntPtr ppText);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("40834007-6834-46f0-BD45-D5F6A6BE258C"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICaptionServiceDescriptor
    {
        [PreserveSig]
        int GetNumberOfServices([Out] out byte pbVal);

        [PreserveSig]
        int GetLanguageCode(
            [In] byte bIndex,
            [Out] out int LangCode // probably a byte[3]
        );

        [PreserveSig]
        int GetCaptionServiceNumber([In] byte bIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetCCType([In] byte bIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetEasyReader([In] byte bIndex, [Out] out byte pbVal);

        [PreserveSig]
        int GetWideAspectRatio([In] byte bIndex, [Out] out byte pbVal);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, Guid("58C3C827-9D91-4215-BFF3-820A49F0904C"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IServiceLocationDescriptor
    {
        [PreserveSig]
        int GetPCR_PID(out short pwVal);

        [PreserveSig]
        int GetNumberOfElements(out byte pbVal);

        [PreserveSig]
        int GetElementStreamType(byte bIndex, out byte pbVal);

        [PreserveSig]
        int GetElementPID(byte bIndex, out short pwVal);

        [PreserveSig]
        int GetElementLanguageCode(byte bIndex, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] out byte[] LangCode);

    }

    #endregion
}