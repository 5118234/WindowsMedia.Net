using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsMedia.Platform
{
    #region Declarations

    /// <summary>
    /// From PID_BITS & PID_BITS_MIDL
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct PidBits
    {
        public short Bits;

        public short Reserved => (short)((int)Bits & 0x0007);

        public short ProgramId => (short)(((int)Bits & 0xfff8) >> 3);
    }

    /// <summary>
    /// The MPEG_HEADER_BITS structure contains the first 16 bits that follow the table_id in a generic MPEG-2 section header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct MpegHeaderBits
    {
        /// <summary>
        /// The value
        /// </summary>
        public short Bits;

        /// <summary>
        /// Gets the length of the section, in bytes. 
        /// </summary>
        /// <value>
        /// The length of the section, in bytes. 
        /// </value>
        public short SectionLength => (short)((int)Bits & 0x0fff);

        /// <summary>
        /// Gets two reserved bits. 
        /// </summary>
        /// <value>
        /// Two reserved bits.
        /// </value>
        public short Reserved => (short)(((int)Bits & 0x3000) >> 12);

        /// <summary>
        /// Gets the private_indicator bit. 
        /// </summary>
        /// <value>
        /// The private_indicator bit. 
        /// </value>
        public short PrivateIndicator => (short)(((int)Bits & 0x4000) >> 14);

        /// <summary>
        /// Gets the section_syntax_indicator bit. 
        /// </summary>
        /// <value>
        /// The section_syntax_indicator bit. 
        /// </value>
        public short SectionSyntaxIndicator => (short)(((int)Bits & 0x8000) >> 15);
    }

    /// <summary>
    /// From MPEG_HEADER_VERSION_BITS & MPEG_HEADER_VERSION_BITS_MIDL
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MpegHeaderVersionBits
    {
        public byte Bits;

        public byte CurrentNextIndicator
        {
            get { return (byte)((int)Bits & 0x1); }
        }

        public byte VersionNumber
        {
            get { return (byte)(((int)Bits & 0x3e) >> 1); }
        }

        public byte Reserved
        {
            get { return (byte)(((int)Bits & 0xc0) >> 6); }
        }
    }

    /// <summary>
    /// From MPEG_CURRENT_NEXT_BIT, MPEG_SECTION_IS_*
    /// </summary>
    public enum MpegSectionIs
    {
        Next = 0,
        Current = 1
    }

    /// <summary>
    /// From TID_EXTENSION
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct TidExtension
    {
        public short wTidExt;
        public short wCount;
    }

    /// <summary>
    /// The SECTION structure represents a short header from an MPEG-2 table section.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Section
    {
        /// <summary>
        /// Specifies the table identifier (TID) of the section.
        /// </summary>
        public short TableId;

        /// <summary>
        /// A union that contains header bits. See <see cref="MpegHeaderBits"/> union.
        /// </summary>
        public MpegHeaderBits Header;

        /// <summary>
        /// Contains the section data, as a byte array. The length of the array is given by the Header.W.SectionLength field.
        /// </summary>
        public byte SectionData; // Must be marshalled manually
    }

    /// <summary>
    /// From LONG_SECTION
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LongSection
    {
        public short TableId;
        public MpegHeaderBits Header;
        public short TableIdExtension;
        public MpegHeaderVersionBits Version;
        public byte SectionNumber;
        public byte LastSectionNumber;
        public byte RemainingData; // Must be marshalled manually
    }

    /// <summary>
    /// From DSMCC_SECTION
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DsmccSection
    {
        public short TableId;
        public MpegHeaderBits Header;
        public short TableIdExtension;
        public MpegHeaderVersionBits Version;
        public byte SectionNumber;
        public byte LastSectionNumber;
        public byte ProtocolDiscriminator;
        public byte DsmccType;
        public short MessageId;
        public int TransactionId;
        public byte Reserved;
        public byte AdaptationLength;
        public short MessageLength;
        public byte RemainingData; // Must be marshalled manually
    }

    /// <summary>
    /// The MPEG_RQST_PACKET structure defines a buffer to receive MPEG-2 section data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MpegRqstPacket
    {
        /// <summary>
        /// Specifies the length of the buffer that pSection points to. The minimum size for section data is 4096 bytes.
        /// </summary>
        public int dwLength;

        /// <summary>
        /// Pointer to a buffer that receives the section data. The pointer is typed as a <see cref="Section"/> structure. The first bytes in the section contain header 
        /// fields that are defined in the SECTION structure. The SectionData member of the <see cref="Section"/> structure is an array of bytes, 
        /// containing the body of the section after the header bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStruct)]
        public Section pSection;
    }

    /// <summary>
    /// From MPEG_DATE
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MpegDate
    {
        public byte Date;
        public byte Month;
        public short Year;

        public DateTime ToDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Date);
        }
    }

    /// <summary>
    /// From MPEG_SERVICE_REQUEST
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MpegServiceRequest
    {
        public MPEGRequestType Type;
        public MPEGContext Context;
        public short Pid;
        public byte TableId;
        public MPEG2Filter Filter;
        public int Flags;
    }

    /// <summary>
    /// From MPEG_SERVICE_RESPONSE
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class MpegServiceResponse
    {
        public int IPAddress;
        public short Port;
    }

    /// <summary>
    /// From MPEG_STREAM_FILTER
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MpegStreamFilter
    {
        public short wPidValue;
        public int dwFilterSize;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fCrcEnabled;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
        public byte[] rgchFilter;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
        public byte[] rgchMask;
    }

    /// <summary>
    /// From MPEG_DURATION & MPEG_TIME
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MpegDuration
    {
        public byte Hours;
        public byte Minutes;
        public byte Seconds;

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(this.Hours, this.Minutes, this.Seconds);
        }
    }

    /// <summary>
    /// From MPEG_DATE_AND_TIME
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MpegDateAndTime
    {
        //public MpegDate D;
        //public MpegTime T;
        // Marshaling is faster like that...
        public byte Date;
        public byte Month;
        public short Year;
        public byte Hours;
        public byte Minutes;
        public byte Seconds;

        public DateTime ToDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Date, this.Hours, this.Minutes, this.Seconds);
        }
    }

    /// <summary>
    /// From DSMCC_ELEMENT
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class DsmccElement
    {
        public short pid;
        public byte bComponentTag;
        public int dwCarouselId;
        public int dwTransactionId;
        public DsmccElement pNext;
    }

    /// <summary>
    /// From MPE_ELEMENT
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MpeElement
    {
        public short pid;
        public byte bComponentTag;
        public MpeElement pNext;
    }

    /// <summary>
    /// From ProgramElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct ProgramElement
    {
        public short wProgramNumber;
        public short wProgramMapPID;
    }

    /// <summary>
    /// The MPEG_REQUEST_TYPE enumeration type specifies a request for MPEG-2 data.
    /// </summary>
    public enum MPEGRequestType
    {
        // Fields
        /// <summary>
        /// Unknown request type. Do not use this value.
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// Get one table section. (Synchronous call.)
        /// </summary>
        SECTION = 1,

        /// <summary>
        /// Get one table section. (Asynchronous call.)
        /// </summary>
        SECTION_ASYNC = 2,

        /// <summary>
        /// Get a complete table. (Synchronous call.)
        /// </summary>
        TABLE = 3,

        /// <summary>
        /// Get a complete table. (Asynchronous call.)
        /// </summary>
        TABLE_ASYNC = 4,

        /// <summary>
        /// Get a stream of sections.
        /// </summary>
        SECTIONS_STREAM = 5,

        /// <summary>
        /// Get a stream of packetized elementary stream (PES) packets.
        /// </summary>
        PES_STREAM = 6,

        /// <summary>
        /// Get a stream of transport stream (TS) packets.
        /// </summary>
        TS_STREAM = 7,

        /// <summary>
        /// Get a stream of multi-protocol encapsulation (MPE) packets.
        /// </summary>
        START_MPE_STREAM = 8,
    }

    /// <summary>
    /// The MPEG_CONTEXT_TYPE enumeration type identifies the source of an MPEG-2 data stream.
    /// </summary>
    public enum MPEGContextType
    {
        /// <summary>
        /// Indicates that the source is a DirectShow filter graph using the MPEG-2 Demultiplexer filter.
        /// </summary>
        BCSDeMux = 0,

        /// <summary>
        /// Reserved. Do not use.
        /// </summary>
        WinSock = 1
    }

    /// <summary>
    /// The MPEG_PACKET_LIST structure contains a list of MPEG-2 sections.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MPEGPacketList
    {
        /// <summary>
        /// Specifies the size of the PacketList array.
        /// </summary>
        public short wPacketCount;

        /// <summary>
        /// Specifies a pointer to an array of <see cref="MpegRqstPacket"/> structures, which themselves contain pointers to buffers that hold the sectioned data.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStruct)]
        public MpegRqstPacket PacketList;
    }

    /// <summary>
    /// The DSMCC_FILTER_OPTIONS structure specifies additional filtering criteria for the DSM-CC portions of the section header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DSMCCFilterOptions
    {
        /// <summary>
        /// If this flag is TRUE, the protocolDiscriminator field in the header must match the value of the <see cref="DSMCCFilterOptions.Protocol"/> structure member. 
        /// Otherwise, the protocolDiscriminator field is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyProtocol;

        /// <summary>
        /// Specifies a value for the protocolDiscriminator field. For MPEG-2 DSM-CC messages, this field must equal 0x11.
        /// </summary>
        public byte Protocol;

        /// <summary>
        /// If this field is <b>true</b>, the dsmccType field in the header must match the value of the <see cref="DSMCCFilterOptions.Type"/> structure member. 
        /// Otherwise, the dsmccType field is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyType;

        /// <summary>
        /// Specifies a value for the dsmccType field, which defines the DSM-CC message type.
        /// </summary>
        public byte Type;

        /// <summary>
        /// If this flag is TRUE, the messageId field in the header must match the value of the <see cref="DSMCCFilterOptions.MessageId"/> structure member. 
        /// Otherwise, the messageId field is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyMessageId;

        /// <summary>
        /// Specifies a value for the messageId field, which defines the DSM-CC message within the scope of the message type.
        /// </summary>
        public short MessageId;

        /// <summary>
        /// If this flag is TRUE, the transactionId (or downloadId) field in the header must match the value of the <see cref="DSMCCFilterOptions.TransactionId"/> structure member. 
        /// Otherwise, the transactionId/downloadId field is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyTransactionId;

        /// <summary>
        /// If this flag is <b>true</b>, the transactionId bits are masked so that the following subfields are ignored:
        /// <list type="bullet">
        /// <item>Updated flag</item>
        /// <item>Version</item>
        /// </list>
        /// The following subfields are matched against the <see cref="DSMCCFilterOptions.TransactionId"/> structure member:
        /// <list type="bullet">
        /// <item>Identification</item>
        /// <item>Originator</item>
        /// </list>
        /// <remarks>For more information about the subfields within the transactionId, see section 4.6.5 of TR 101 202, Digital Video Broadcasting (DVB); 
        /// Implementation Guidelines for Data Broadcasting. (This resource may not be available in some languages and countries.)</remarks>
        /// This flag is ignored if <see cref="DSMCCFilterOptions.fSpecifyTransactionId"/> is <b>false</b>.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fUseTrxIdMessageIdMask;

        /// <summary>
        /// Specifies a value for the transactionId field.
        /// </summary>
        public int TransactionId;

        /// <summary>
        /// If this flag is <b>true</b>, the moduleVersion field in the header must match the value of the <see cref="DSMCCFilterOptions.ModuleVersion"/> structure member. 
        /// Otherwise, the moduleVersion field is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyModuleVersion;

        /// <summary>
        /// Specifies a value for the moduleVersion field.
        /// </summary>
        public byte ModuleVersion;

        /// <summary>
        /// If this flag is <b>true</b>, the blockNumber field in the header must match the value of the <see cref="DSMCCFilterOptions.BlockNumber"/> structure member. 
        /// Otherwise, the moduleVersion field is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyBlockNumber;

        /// <summary>
        /// Specifies a value for the blockNumber field.
        /// </summary>
        public short BlockNumber;

        /// <summary>
        /// If this flag is <b>true</b>, the <see cref="DSMCCFilterOptions.NumberOfBlocksInModule"/> structure member specifies the number of blocks in the module. 
        /// Applies only to download data block (DDB) messages.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fGetModuleCall;

        /// <summary>
        /// Specifies the number of blocks in the module. Applies only to DDB messages.
        /// </summary>
        public short NumberOfBlocksInModule;
    }

    /// <summary>
    /// The ATSC_FILTER_OPTIONS structure specifies additional criteria for matching ATSC section headers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ATSCFilterOptions
    {
        /// <summary>
        /// If this flag is <b>true</b>, the ETM_id field in the header must match the value of the <see cref="EtmId"/> structure member. 
        /// Otherwise, the ETM_id field is ignored.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyEtmId;

        /// <summary>
        /// Specifies a value for the ETM_id field.
        /// </summary>
        public int EtmId;
    }

    /// <summary>
    /// The MPEG2_FILTER structure specifies criteria for matching MPEG-2 section headers. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MPEG2Filter
    {
        /// <summary>
        /// Specifies the version number of the structure. This value must be 1 or higher. 
        /// </summary>
        public byte bVersionNumber;

        /// <summary>
        /// Specifies the size of the structure data, excluding any padding bytes. Set this field equal to the constant MPEG2_FILTER_VERSION_1_SIZE. 
        /// </summary>
        public short wFilterSize;

        /// <summary>
        /// If <b>true</b>, the <see cref="MPEG2Filter.Filter"/> and <see cref="MPEG2Filter.Mask"/> members specify the filtering criteria as a pair of bit masks, 
        /// and the remaining members of this structure are ignored. If this field is <b>false</b>, the <see cref="MPEG2Filter.Filter"/> and <see cref="MPEG2Filter.Mask"/> 
        /// members are ignored, and the other structure members contain the filtering criteria. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fUseRawFilteringBits;

        /// <summary>
        /// Specifies a 16-byte bit mask, which contains the bit values to match in the section header. 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Filter;

        /// <summary>
        /// Specifies a 16-byte bit mask. Set any "don't care" bits equal to 1, and all other bits to 0. In other words, for each bit, if the value in Mask is 0, 
        /// the corresponding bit in <see cref="MPEG2Filter.Filter"/> will be matched against that bit in the section header. 
        /// If the value in <see cref="MPEG2Filter.Mask"/> is 1, that bit in the section header is ignored. 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Mask;

        /// <summary>
        /// If TRUE, the table_ID_extension field in the header must match the value of the <see cref="MPEG2Filter.TableIdExtension"/> structure member. 
        /// Otherwise, the table_ID_extension field is ignored. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyTableIdExtension;

        /// <summary>
        /// Specifies a value for the table_ID_extension field.
        /// </summary>
        public short TableIdExtension;

        /// <summary>
        /// If TRUE, the version_number field in the header must match the value of the <see cref="MPEG2Filter.Version"/> structure member. 
        /// Otherwise, the version_number field is ignored. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyVersion;

        /// <summary>
        /// Specifies a value for the version_number field.
        /// </summary>
        public byte Version;

        /// <summary>
        /// If <b>true</b>, the section_number field in the header must match the value of the <see cref="MPEG2Filter.SectionNumber"/> member. 
        /// Otherwise, the section_number field is ignored. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifySectionNumber;

        /// <summary>
        /// Specifies a value for the section_number field. 
        /// </summary>
        public byte SectionNumber;

        /// <summary>
        /// If TRUE, the current_next_indicator bit in the header must match the value of the <see cref="MPEG2Filter.fNext"/> structure member. 
        /// Otherwise, the current_next_indicator field is ignored. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyCurrentNext;

        /// <summary>
        /// Specifies a value for the current_next_indicator bit. You can use the <see cref="MpegSectionIs"/> enumeration type to specify this value. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fNext;

        /// <summary>
        /// If <b>true</b>, the <see cref="MPEG2Filter.Dsmcc"/> member contains additional filtering criteria for the DSM-CC portions of the section header. 
        /// Otherwise, the <see cref="MPEG2Filter.Dsmcc"/> member is ignored. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyDsmccOptions;

        /// <summary>
        /// Specifies a <see cref="DSMCCFilterOptions"/> structure that contains additional filtering criteria for the DSM-CC portions of the section header. 
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public DSMCCFilterOptions Dsmcc;

        /// <summary>
        /// If <b>true</b>, the <see cref="MPEG2Filter.Atsc"/> member contains additional filtering criteria. Otherwise, the <see cref="MPEG2Filter.Atsc"/> member is ignored. 
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyAtscOptions;

        /// <summary>
        /// Specifies an <see cref="ATSCFilterOptions"/> structure that contains additional filtering criteria. 
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public ATSCFilterOptions Atsc;
    }

    /// <summary>
    /// From DVB_EIT_FILTER_OPTIONS
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class DVB_EIT_FILTER_OPTIONS
    {
        [MarshalAs(UnmanagedType.Bool)]
        bool fSpecifySegment;
        byte bSegment;
    }

    /// <summary>
    /// From MPEG2_FILTER2
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MPEG2Filter2 : MPEG2Filter
    {
        [MarshalAs(UnmanagedType.Bool)]
        bool fSpecifyDvbEitOptions;
        DVB_EIT_FILTER_OPTIONS DvbEit;
    }

    /// <summary>
    /// A union that contains the MPEG-2 source data.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct MPEGContextUnion
    {
        /// <summary>
        /// Specifies an <see cref="BCSDeMux"/> structure that identifies the filter graph instance.
        /// </summary>
        [FieldOffset(0)]
        public BCSDeMux Demux;

        /// <summary>
        /// Currently not supported.
        /// </summary>
        [FieldOffset(0)]
        public MPEGWinSock Winsock;
    }

    /// <summary>
    /// The MPEG_BCS_DEMUX structure identifies the filter graph that is providing the MPEG-2 data stream.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BCSDeMux
    {
        /// <summary>
        /// Specifies the filter graph instance.
        /// </summary>
        public int AVMGraphId;
    }

    /// <summary>
    /// From MPEG_WINSOCK
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MPEGWinSock
    {
        public int AVMGraphId;
    }

    /// <summary>
    /// The MPEG_CONTEXT structure identifies the source of an MPEG-2 data stream.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MPEGContext
    {
        /// <summary>
        /// Specifies the source type, as an <see cref="MPEGContextType"/> value. Currently, the value must be MPEG_CONTEXT_BCS_DEMUX.
        /// </summary>
        public MPEGContextType Type;

        /// <summary>
        /// A union that contains the following members:
        /// <list type="bullet">
        /// <item>
        /// <term>Demux</term>
        /// <description>Specifies an <see cref="BCSDeMux"/> structure that identifies the filter graph instance.</description>
        /// </item>
        /// <item>
        /// <term>Winsock</term>
        /// <description>Currently not supported.</description>
        /// </item>
        /// </list>
        /// </summary>
        public MPEGContextUnion U;
    }

    /// <summary>
    /// From MPEG_STREAM_BUFFER
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MPEGStreamBuffer
    {
        //[MarshalAs(UnmanagedType.Error)]
        public int hr;
        public int dwDataBufferSize;
        public int dwSizeOfDataRead;
        public IntPtr pDataBuffer;
    }

    #endregion

    #region Interfaces

    [ComImport, SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("BDCDD913-9ECD-4fb2-81AE-ADF747EA75A5")]
    public interface IMpeg2TableFilter
    {
        [PreserveSig]
        int AddPID( 
            short pid
            );
        
        [PreserveSig]
        int AddTable(
            short pid,
            byte tid
            );
        
        [PreserveSig]
        int AddExtension(
            short pid,
            byte tid,
            short eid
            );
        
        [PreserveSig]
        int RemovePID(
            short pid
            );
        
        [PreserveSig]
        int RemoveTable(
            short pid,
            byte tid
            );
        
        [PreserveSig]
        int RemoveExtension( 
            short pid,
            byte tid,
            short eid
            );
    }

    /// <summary>
    /// The IMpeg2Data interface is exposed by the MPEG-2 Sections and Tables filter. It enables the client to retrieve unparsed sections or tables from an MPEG-2 transport stream.
    /// </summary>
    [ComImport, SuppressUnmanagedCodeSecurity,
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     Guid("9B396D40-F380-4E3C-A514-1A82BF6EBFE6")]
    public interface IMpeg2Data
    {
        /// <summary>
        /// Retrieves an MPEG-2 table section. This method blocks until the filter receives a matching table section, or until the specified time out elapses.
        /// </summary>
        /// <param name="pid">Specifies the packet identifier (PID) of the transport stream packets to examine.</param>
        /// <param name="tid">Specifies the table identifier (TID) of the section to retrieve.</param>
        /// <param name="pFilter">Optional pointer to an <see cref="MPEG2Filter"/> structure. The caller can use this parameter to exclude packets based on additional 
        /// MPEG-2 header fields. This parameter can be <b>null</b>.</param>
        /// <param name="dwTimeout">Specifies a time-out value, in milliseconds. If the filter does not receive a matching section within the time-out period, the method fails.</param>
        /// <param name="ppSectionList">Pointer to a variable that receives an <see cref="ISectionList"/> interface pointer. Use this interface to retrieve the section data. 
        /// The caller must release the interface.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetSection(
            [In] short pid,
            [In] byte tid,
            [In] MPEG2Filter pFilter,
            [In] int dwTimeout,
            [MarshalAs(UnmanagedType.Interface)] out ISectionList ppSectionList
        );

        [PreserveSig]
        int GetTable(
            [In] short pid,
            [In] byte tid,
            [In] MPEG2Filter pFilter,
            [In] int dwTimeout,
            [MarshalAs(UnmanagedType.Interface)] out ISectionList ppSectionList
        );

        [PreserveSig]
        int GetStreamOfSections(
            [In] short pid,
            [In] byte tid,
            [In] MPEG2Filter pFilter,
            [In] IntPtr hDataReadyEvent,
            [MarshalAs(UnmanagedType.Interface)] out IMpeg2Stream ppMpegStream
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("400CC286-32A0-4CE4-9041-39571125A635"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMpeg2Stream
    {
        [PreserveSig]
        int Initialize(
            [In] MPEGRequestType requestType,
            [In, MarshalAs(UnmanagedType.Interface)] IMpeg2Data pMpeg2Data,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEGContext pContext,
            [In] short pid,
            [In] byte tid,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEG2Filter pFilter,
            [In] IntPtr hDataReadyEvent
        );

        [PreserveSig]
        int SupplyDataBuffer(
            [In] MPEGStreamBuffer pStreamBuffer
        );
    }

    /// <summary>
    /// The interface represents a list of MPEG-2 table sections.
    /// </summary>
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("AFEC1EB5-2A64-46C6-BF4B-AE3CCB6AFDB0"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISectionList
    {
        /// <summary>
        /// Initializes the object. This method should be called once, immediately after creating the object. 
        /// The <see cref="IMpeg2Data.GetSection"/> and <see cref="IMpeg2Data.GetTable"/> methods call this method internally, so typically an application will not call it.
        /// </summary>
        /// <param name="requestType">Specifies the request type, as an <see cref="MPEGRequestType"/> value.</param>
        /// <param name="pMpeg2Data">Reference to the <see cref="IMpeg2Data"/> interface of the MPEG-2 Sections and Tables filter.</param>
        /// <param name="pContext">Reference to an <see cref="MPEGContext"/> structure. This structure indicates the MPEG-2 source.</param>
        /// <param name="pid">Specifies a packet identifier (PID), indicating which packets in the transport stream are requested.</param>
        /// <param name="tid">Specifies a table identifier (TID), indicating which table sections to retrieve.</param>
        /// <param name="pFilter">Optional reference to an <see cref="MPEG2Filter"/> structure. The caller can use this parameter to exclude packets based on 
        /// additional MPEG-2 header fields. This parameter can be <b>null</b>.</param>
        /// <param name="timeout">Specifies the maximum length of time that a synchronous request should wait before it times out.</param>
        /// <param name="hDoneEvent">Specifies a handle to an event. The object signals the event when the request completes. This parameter is optional; 
        /// it should be specified for asynchronous requests.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_INVALIDARG</b> - Invalid argument.</item>
        /// <item><b>E_OUTOFMEMORY</b> - Insufficient memory.</item>
        /// <item><b>MPEG2_E_ALREADY_INITIALIZED</b> - The object has already been initialized.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>This method is either synchronous or asynchronous, depending on the request type defined in the requestType parameter. 
        /// When the method is asynchronous, it returns immediately and signals the event specified in hDoneEvent. When the method is synchronous, 
        /// it blocks until the request completes or until the time out specified in the timeout parameter expires.</remarks>
        [PreserveSig]
        int Initialize(
            [In] MPEGRequestType requestType,
            [In, MarshalAs(UnmanagedType.Interface)] IMpeg2Data pMpeg2Data,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEGContext pContext,
            [In] short pid,
            [In] byte tid,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEG2Filter pFilter,
            [In] int timeout,
            [In] IntPtr hDoneEvent
        );

        /// <summary>
        /// Initializes the object with raw section data. This method allows for custom processing of section data.
        /// </summary>
        /// <param name="pmplSections">Pointer to an <see cref="MPEGPacketList"/> structure that contains a list of MPEG-2 sections.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_INVALIDARG</b> - Invalid argument.</item>
        /// <item><b>MPEG2_E_ALREADY_INITIALIZED</b> - The object has already been initialized.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int InitializeWithRawSections(
            [In] ref MPEGPacketList pmplSections
        );

        /// <summary>
        /// Cancels any pending asynchronous request.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int CancelPendingRequest();

        /// <summary>
        /// Gets the number of MPEG-2 sections that were received.
        /// </summary>
        /// <param name="pCount">Receives the number of sections.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_ACCESSDENIED</b> - The request has not completed yet.</item>
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int GetNumberOfSections(
            out short pCount
        );

        /// <summary>
        /// Retrieves a section.
        /// </summary>
        /// <param name="sectionNumber">Specifies the section number to retrieve, indexed from zero. Call the <see cref="GetNumberOfSections"/> method to get the 
        /// number of sections.</param>
        /// <param name="pdwRawPacketLength">Receives the size of the section data, in bytes.</param>
        /// <param name="ppSection">Address of a variable that receives a pointer to a <see cref="Section"/> structure, containing the section data. 
        /// Do not free the memory for the structure; the object frees the memory when the interface is released.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_ACCESSDENIED</b> - The request has not completed yet.</item>
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>MPEG2_E_OUT_OF_BOUNDS</b> - The section number is out of range.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>The section header is converted from network byte order to native byte order. The number of header bytes that are converted depends on the header type. 
        /// The header types are short header (<see cref="Section"/> structure), long header (<see cref="LongSection"/> structure), or DSM-CC header (<see cref="DsmccSection"/> structure). 
        /// If the section has a short header, the first three bytes are converted; for a long header, the first eight bytes are converted; and for a 
        /// DSM-CC header, the first 20 bytes are converted.
        /// 
        /// The body of the section data, after the header, is left unparsed and unconverted.</remarks>
        [PreserveSig]
        int GetSectionData(
            [In] short sectionNumber,
            [Out] out int pdwRawPacketLength,
            [Out, MarshalAs(UnmanagedType.LPStruct)] out Section ppSection // PSECTION*
        );

        /// <summary>
        /// Retrieves the program identifier (PID) of the packets that this object is receiving.
        /// </summary>
        /// <param name="pPid">Receives the PID.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>The PID value is set when the object is first initialized.</remarks>
        [PreserveSig]
        int GetProgramIdentifier(
            out short pPid
        );

        /// <summary>
        /// Gets the table identifier (TID) of the packets that this object is receiving.
        /// </summary>
        /// <param name="pTableId">Receives the TID.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int GetTableIdentifier(
            out byte pTableId
        );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("D19BDB43-405B-4a7c-A791-C89110C33165"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITSDT
    {
        [PreserveSig]
        int Initialize(
          [In] ISectionList pSectionList,
          [In] IMpeg2Data pMPEGData
          );

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex(
          [In] int dwIndex,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int GetTableDescriptorByTag(
          [In] byte bTag,
          [In, Out] DsInt pdwCookie,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        [PreserveSig]
        int GetNextTable([Out] out ITSDT ppTSDT);

        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        [PreserveSig]
        int ConvertNextToCurrent();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("919F24C5-7B14-42ac-A4B0-2AE08DAF00AC"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPSITables
    {
        [PreserveSig]
        int GetTable(
          [In] int dwTSID,
          [In] int dwTID_PID,
          [In] int dwHashedVer,
          [In] int dwPara4,
          [Out] out object ppIUnknown
          );
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
    Guid("BF02FB7E-9792-4e10-A68D-033A2CC246A5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGenericDescriptor2 : IGenericDescriptor
    {
    #region IGenericDescriptor methods

        [PreserveSig]
        new int Initialize(
          [In] IntPtr pbDesc,
          [In] int bCount
          );

        [PreserveSig]
        new int GetTag(
            [Out] out byte pbVal
            );

        [PreserveSig]
        new int GetLength(
            [Out] out byte pbVal
            );

        [PreserveSig]
        new int GetBody(
            [Out] out IntPtr ppbVal
            );

    #endregion

        [PreserveSig]
        int Initialize( 
            IntPtr pbDesc,
            short wCount
            );

        [PreserveSig]
        int  GetLength( 
            out short pwVal
            );
        
    };
    
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("6A5918F8-A77A-4f61-AED0-5702BDCDA3E6"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGenericDescriptor
    {
        [PreserveSig]
        int Initialize(
            [In] IntPtr pbDesc,
            [In] int bCount
        );

        [PreserveSig]
        int GetTag([Out] out byte pbVal);

        [PreserveSig]
        int GetLength([Out] out byte pbVal);

        [PreserveSig]
        int GetBody([Out] out IntPtr ppbVal);
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("7C6995FB-2A31-4bd7-953E-B1AD7FB7D31C"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICAT
    {
        [PreserveSig]
        int Initialize(
            [In] ISectionList pSectionList,
            [In] IMpeg2Data pMPEGData
        );

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex(
            [In] int dwIndex,
            [Out] out IGenericDescriptor ppDescriptor
        );

        [PreserveSig]
        int GetTableDescriptorByTag(
            [In] byte bTag,
            [In, Out] DsInt pdwCookie,
            [Out] out IGenericDescriptor ppDescriptor
        );

        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        [PreserveSig]
        int GetNextTable(
            [In] int dwTimeout,
            [Out] out ICAT ppCAT);

        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        [PreserveSig]
        int ConvertNextToCurrent();
    }

    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("01F3B398-9527-4736-94DB-5195878E97A8"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPMT
    {
        [PreserveSig]
        int Initialize(
            [In] ISectionList pSectionList,
            [In] IMpeg2Data pMPEGData
        );

        [PreserveSig]
        int GetProgramNumber([Out] out short pwVal);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetPcrPid([Out] out short pPidVal);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex(
            [In] int dwIndex,
            [Out] out IGenericDescriptor ppDescriptor
        );

        [PreserveSig]
        int GetTableDescriptorByTag(
            [In] Byte bTag,
            [In, Out] DsInt pdwCookie,
            [Out] out IGenericDescriptor ppDescriptor
        );

        [PreserveSig]
        int GetCountOfRecords([Out] out short pwVal);

        [PreserveSig]
        int GetRecordStreamType(
            [In] int dwRecordIndex,
            [Out] out byte pbVal
        );

        [PreserveSig]
        int GetRecordElementaryPid(
            [In] int dwRecordIndex,
            [Out] out short pPidVal
        );

        [PreserveSig]
        int GetRecordCountOfDescriptors(
            [In] int dwRecordIndex,
            [Out] out int pdwVal
        );

        [PreserveSig]
        int GetRecordDescriptorByIndex(
            [In] int dwRecordIndex,
            [In] int dwDescIndex,
            [Out] out IGenericDescriptor ppDescriptor
        );

        [PreserveSig]
        int GetRecordDescriptorByTag(
            [In] int dwRecordIndex,
            [In] Byte bTag,
            [In, Out] DsInt pdwCookie,
            [Out] out IGenericDescriptor ppDescriptor
        );

        [PreserveSig]
        int QueryServiceGatewayInfo(
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] out DsmccElement[] ppDSMCCList,
            [Out] out int puiCount
        );

        [PreserveSig]
        int QueryMPEInfo(
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] out MpeElement[] ppMPEList,
            [Out] out int puiCount
        );

        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        [PreserveSig]
        int GetNextTable([Out] out IPMT ppPMT);

        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        [PreserveSig]
        int ConvertNextToCurrent();
    }

    /// <summary>
    /// The IPAT interface enables the client to get information from a Program Association Table (PAT). The <see cref="IAtscPsipParser.GetPAT"/> method returns a 
    /// pointer to this interface.
    /// </summary>
    [ComImport, SuppressUnmanagedCodeSecurity,
     Guid("6623B511-4B5F-43c3-9A01-E8FF84188060"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPAT
    {
        /// <summary>
        /// Initializes the object using captured table section data. This method is called internally by the <see cref="IAtscPsipParser.GetPAT"/> method, 
        /// so applications typically should not call it.
        /// </summary>
        /// <param name="pSectionList">Pointer to the <see cref="ISectionList"/> interface of the SectionList object that contains the section data.</param>
        /// <param name="pMpegData">Pointer to the <see cref="IMpeg2Data"/> interface of the MPEG-2 Sections and Tables filter.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_INVALIDARG</b> - Invalid argument.</item>
        /// <item><b>MPEG2_E_ALREADY_INITIALIZED</b> - The object is already initialized.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int Initialize(
            [In] ISectionList pSectionList,
            [In] IMpeg2Data pMpegData
        );

        /// <summary>
        /// Returns the transport stream identifier (TSID) for the PAT.
        /// </summary>
        /// <param name="pwVal">Receives the transport_stream_id field.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int GetTransportStreamId([Out] out short pwVal);

        /// <summary>
        /// Returns the version number for the PAT.
        /// </summary>
        /// <param name="pbVal">Receives the version_number field.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        /// <summary>
        /// Returns the number of records in the PAT. Each record corresponds to one program.
        /// </summary>
        /// <param name="pwVal">Receives the number of records.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int GetCountOfRecords([Out] out int pwVal);

        /// <summary>
        /// Retrieves a program number from the PAT.
        /// </summary>
        /// <param name="dwIndex">Specifies the record to retrieve, indexed from zero. Call the <see cref="IPAT.GetCountOfRecords"/> method to get the number of records in the PAT.</param>
        /// <param name="pwVal">Receives the program number.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>MPEG2_E_OUT_OF_BOUNDS</b> - Index out of bounds.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int GetRecordProgramNumber(
            [In] int dwIndex,
            [Out] out short pwVal
        );

        /// <summary>
        /// Returns the packet identifier (PID) for a given record in the PAT.
        /// </summary>
        /// <param name="dwIndex">Specifies the record to retrieve, indexed from zero. Call the <see cref="IPAT.GetCountOfRecords"/> method to get the number of records in the PAT.</param>
        /// <param name="pwVal">Receives the PID. This value identifies the PID for the packets that contain the program map table (PMT) of the associated program.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>MPEG2_E_OUT_OF_BOUNDS</b> - Index out of bounds.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int GetRecordProgramMapPid(
            [In] int dwIndex,
            [Out] out short pwVal
        );

        /// <summary>
        /// Returns the packet identifier (PID) for the program map table (PMT) associated with a given program number.
        /// </summary>
        /// <param name="wProgramNumber">Specifies the program number.</param>
        /// <param name="pwVal">Receives the PID.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_POINTER</b> - <b>null</b> pointer argument.</item>
        /// <item><b>MPEG2_E_NOT_PRESENT</b> - The table does not contain the specified program number.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        [PreserveSig]
        int FindRecordProgramMapPid(
            [In] short wProgramNumber,
            [Out] out short pwVal
        );

        /// <summary>
        /// Registers the client to be notified when a next table arrives that will replace the current table.
        /// </summary>
        /// <param name="hNextTableAvailable">Handle to an event created by the caller. The object signals the event when the next table arrives. When the event is signaled, 
        /// call the <see cref="IPAT.GetNextTable"/> method to retrieve the table.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_ACCESSDENIED</b> - This table is already a next table.</item>
        /// <item><b>E_INVALIDARG</b> - Invalid argument <paramref name="hNextTableAvailable"/> cannot be <b>null</b>.</item>
        /// <item><b>MPEG2_E_ALREADY_INITIALIZED</b> - The method has already been called.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>This method applies only to current tables. Otherwise, the method returns <b>E_ACCESSDENIED</b>.</remarks>
        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        /// <summary>
        ///  Retrieves the <i>next</i> table that follows the current table.
        /// </summary>
        /// <param name="ppPat">Address of a variable that receives an <see cref="IPAT"/> interface pointer. The caller must release the interface.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_ACCESSDENIED</b> - This table is already a next table.</item>
        /// <item><b>E_FAIL</b> - Failure.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>This method applies only to current tables. Otherwise, the method returns <b>E_ACCESSDENIED</b>.</remarks>
        [PreserveSig]
        int GetNextTable([Out] out IPAT ppPat);

        /// <summary>
        /// Registers the client to be notified when the table becomes current.
        /// </summary>
        /// <param name="hNextTableIsCurrent">Handle to an event created by the caller. The object signals the event when the table becomes current.</param>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_ACCESSDENIED</b> - This table is already a next table.</item>
        /// <item><b>E_INVALIDARG</b> - Invalid argument <paramref name="hNextTableIsCurrent"/> cannot be <b>null</b>.</item>
        /// <item><b>MPEG2_E_ALREADY_INITIALIZED</b> - The method has already been called.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>This method applies only to <i>next</i> tables. Otherwise, the method returns <b>E_ACCESSDENIED</b>.</remarks>
        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        /// <summary>
        /// Converts a next table to a current table.
        /// </summary>
        /// <returns>The method returns an HRESULT. Possible values include those in the following table.
        /// <list type="bullet">
        /// <item><b>E_ACCESSDENIED</b> - This table is already current.</item>
        /// <item><b>E_INVALIDARG</b> - The <see cref="RegisterForWhenCurrent"/> method was not called.</item>
        /// <item><b>MPEG2_E_MALFORMED_TABLE</b> - The new current table is malformed.</item>
        /// <item><b>S_OK</b> - The method succeeded.</item>
        /// </list>
        /// </returns>
        /// <remarks>This method applies only to next tables that have become current. Before calling this method, call <see cref="IPAT.RegisterForWhenCurrent"/> and 
        /// wait for the event to be signaled.</remarks>
        [PreserveSig]
        int ConvertNextToCurrent();
    }

    #endregion
}