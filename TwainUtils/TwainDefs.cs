using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TwainLib
{

public class TwProtocol
	{									// TWON_PROTOCOL...
	public const short Major	= 1;
	public const short Minor	= 9;
	}

    public class ScanMode
    {
        public ScanMode()
        {
        }
        public static int SCAN_IN_DUPLEX_MODE = 1;
        public static int SCAN_IN_SIMPLEX_MODE = 0;
    }
	[Flags]
internal enum TwDG : short
	{									// DG_.....
	Control			= 0x0001,
	Image			= 0x0002,
	Audio			= 0x0004
	}

internal enum TwDAT : short
	{									// DAT_....
	Null			= 0x0000,
	Capability		= 0x0001,
	Event			= 0x0002,
	Identity		= 0x0003,
	Parent			= 0x0004,
	PendingXfers	= 0x0005,
	SetupMemXfer	= 0x0006,
	SetupFileXfer	= 0x0007,
	Status			= 0x0008,
	UserInterface	= 0x0009,
	XferGroup		= 0x000a,
	TwunkIdentity	= 0x000b,
	CustomDSData	= 0x000c,
	DeviceEvent		= 0x000d,
	FileSystem		= 0x000e,
	PassThru		= 0x000f,

	ImageInfo		= 0x0101,
	ImageLayout		= 0x0102,
	ImageMemXfer	= 0x0103,
	ImageNativeXfer	= 0x0104,
	ImageFileXfer	= 0x0105,
	CieColor		= 0x0106,
	GrayResponse	= 0x0107,
	RGBResponse		= 0x0108,
	JpegCompression	= 0x0109,
	Palette8		= 0x010a,
	ExtImageInfo	= 0x010b,

	SetupFileXfer2	= 0x0301
	}

internal enum TwMSG : short
	{									// MSG_.....
	Null			= 0x0000,
	Get				= 0x0001,
	GetCurrent		= 0x0002,
	GetDefault		= 0x0003,
	GetFirst		= 0x0004,
	GetNext			= 0x0005,
	Set				= 0x0006,
	Reset			= 0x0007,
	QuerySupport	= 0x0008,

	XFerReady		= 0x0101,
	CloseDSReq		= 0x0102,
	CloseDSOK		= 0x0103,
	DeviceEvent		= 0x0104,

	CheckStatus		= 0x0201,

	OpenDSM			= 0x0301,
	CloseDSM		= 0x0302,

	OpenDS			= 0x0401,
	CloseDS			= 0x0402,
	UserSelect		= 0x0403,

	DisableDS		= 0x0501,
	EnableDS		= 0x0502,
	EnableDSUIOnly	= 0x0503,

	ProcessEvent	= 0x0601,

	EndXfer			= 0x0701,
	StopFeeder		= 0x0702,

	ChangeDirectory	= 0x0801,
	CreateDirectory	= 0x0802,
	Delete			= 0x0803,
	FormatMedia		= 0x0804,
	GetClose		= 0x0805,
	GetFirstFile	= 0x0806,
	GetInfo			= 0x0807,
	GetNextFile		= 0x0808,
	Rename			= 0x0809,
	Copy			= 0x080A,
	AutoCaptureDir	= 0x080B,

	PassThru		= 0x0901
	}


internal enum TwRC : short
	{									// TWRC_....
	Success				= 0x0000,
	Failure				= 0x0001,
	CheckStatus			= 0x0002,
	Cancel				= 0x0003,
	DSEvent				= 0x0004,
	NotDSEvent			= 0x0005,
	XferDone			= 0x0006,
	EndOfList			= 0x0007,
	InfoNotSupported	= 0x0008,
	DataNotAvailable	= 0x0009
	}

internal enum TwCC : short
	{									// TWCC_....
	Success				= 0x0000,
	Bummer				= 0x0001,
	LowMemory			= 0x0002,
	NoDS				= 0x0003,
	MaxConnections		= 0x0004,
	OperationError		= 0x0005,
	BadCap				= 0x0006,
	BadProtocol			= 0x0009,
	BadValue			= 0x000a,
	SeqError			= 0x000b,
	BadDest				= 0x000c,
	CapUnsupported		= 0x000d,
	CapBadOperation		= 0x000e,
	CapSeqError			= 0x000f,
	Denied				= 0x0010,
	FileExists			= 0x0011,
	FileNotFound		= 0x0012,
	NotEmpty			= 0x0013,
	PaperJam			= 0x0014,
	PaperDoubleFeed		= 0x0015,
	FileWriteError		= 0x0016,
	CheckDeviceOnline	= 0x0017
	}




internal enum TwOn : short
	{									// TWON_....
	Array			= 0x0003,
	Enum			= 0x0004,
	One				= 0x0005,
	Range			= 0x0006,
	DontCare		= -1
	}

internal enum TwType : short
	{									// TWTY_....
	Int8			= 0x0000,
	Int16			= 0x0001,
	Int32			= 0x0002,
	UInt8			= 0x0003,
	UInt16			= 0x0004,
	UInt32			= 0x0005,
	Bool			= 0x0006,
	Fix32			= 0x0007,
	Frame			= 0x0008,
	Str32			= 0x0009,
	Str64			= 0x000a,
	Str128			= 0x000b,
	Str255			= 0x000c,
	Str1024			= 0x000d,
	Str512			= 0x000e
	}


internal enum TwCap : short
	{
        XferCount = 0x0001,

        ICompression = 0x0100,
        IPixelType = 0x0101,
        IUnits = 0x0102,
        IXferMech = 0x0103,

        Author = 0x1000,
        Caption = 0x1001,
        FeederEnabled = 0x1002,
        FeederLoaded = 0x1003,
        Timedate = 0x1004,
        SupportedCapabilities = 0x1005,
        Extendedcaps = 0x1006,
        AutoFeed = 0x1007,
        ClearPage = 0x1008,
        FeedPage = 0x1009,
        RewindPage = 0x100a,

        Indicators = 0x100b,
        SupportedCapsExt = 0x100c,
        PaperDetectable = 0x100d,
        UIControllable = 0x100e,
        DeviceOnline = 0x100f,
        AutoScan = 0x1010,
        ThumbnailsEnabled = 0x1011,
        Duplex = 0x1012,
        DuplexEnabled = 0x1013,
        Enabledsuionly = 0x1014,
        CustomdsData = 0x1015,
        Endorser = 0x1016,
        JobControl = 0x1017,
        Alarms = 0x1018,
        AlarmVolume = 0x1019,
        AutomaticCapture = 0x101a,
        TimeBeforeFirstCapture = 0x101b,
        TimeBetweenCaptures = 0x101c,
        ClearBuffers = 0x101d,
        MaxBatchBuffers = 0x101e,
        DeviceTimeDate = 0x101f,
        PowerSupply = 0x1020,
        CameraPreviewUI = 0x1021,
        DeviceEvent = 0x1022,
        SerialNumber = 0x1024,
        Printer = 0x1026,
        PrinterEnabled = 0x1027,
        PrinterIndex = 0x1028,
        PrinterMode = 0x1029,
        PrinterString = 0x102a,
        PrinterSuffix = 0x102b,
        Language = 0x102c,
        FeederAlignment = 0x102d,
        FeederOrder = 0x102e,
        ReAcquireAllowed = 0x1030,
        BatteryMinutes = 0x1032,
        BatteryPercentage = 0x1033,
        CameraSide = 0x1034,
        Segmented = 0x1035,
        CameraEnabled = 0x1036,
        CameraOrder = 0x1037,
        MicrEnabled = 0x1038,
        FeederPrep = 0x1039,
        Feederpocket = 0x103a,

        Autobright = 0x1100,
        Brightness = 0x1101,
        Contrast = 0x1103,
        CustHalftone = 0x1104,
        ExposureTime = 0x1105,
        Filter = 0x1106,
        Flashused = 0x1107,
        Gamma = 0x1108,
        Halftones = 0x1109,
        Highlight = 0x110a,
        ImageFileFormat = 0x110c,
        LampState = 0x110d,
        LightSource = 0x110e,
        Orientation = 0x1110,
        PhysicalWidth = 0x1111,
        PhysicalHeight = 0x1112,
        Shadow = 0x1113,
        Frames = 0x1114,
        XNativeResolution = 0x1116,
        YNativeResolution = 0x1117,
        XResolution = 0x1118,
        YResolution = 0x1119,
        MaxFrames = 0x111a,

        Tiles = 0x111b,
        Bitorder = 0x111c,
        Ccittkfactor = 0x111d,
        Lightpath = 0x111e,
        Pixelflavor = 0x111f,
        Planarchunky = 0x1120,
        Rotation = 0x1121,
        Supportedsizes = 0x1122,
        Threshold = 0x1123,
        Xscaling = 0x1124,
        Yscaling = 0x1125,
        Bitordercodes = 0x1126,
        Pixelflavorcodes = 0x1127,
        Jpegpixeltype = 0x1128,
        Timefill = 0x112a,
        BitDepth = 0x112b,
        Bitdepthreduction = 0x112c,
        Undefinedimagesize = 0x112d,
        Imagedataset = 0x112e,
        Extimageinfo = 0x112f,
        Minimumheight = 0x1130,
        Minimumwidth = 0x1131,
        Fliprotation = 0x1136,
        Barcodedetectionenabled = 0x1137,
        Supportedbarcodetypes = 0x1138,
        Barcodemaxsearchpriorities = 0x1139,
        Barcodesearchpriorities = 0x113a,
        Barcodesearchmode = 0x113b,
        Barcodemaxretries = 0x113c,
        Barcodetimeout = 0x113d,
        Zoomfactor = 0x113e,
        Patchcodedetectionenabled = 0x113f,
        Supportedpatchcodetypes = 0x1140,
        Patchcodemaxsearchpriorities = 0x1141,
        Patchcodesearchpriorities = 0x1142,
        Patchcodesearchmode = 0x1143,
        Patchcodemaxretries = 0x1144,
        Patchcodetimeout = 0x1145,
        Flashused2 = 0x1146,
        Imagefilter = 0x1147,
        Noisefilter = 0x1148,
        Overscan = 0x1149,
        Automaticborderdetection = 0x1150,
        Automaticdeskew = 0x1151,
        Automaticrotate = 0x1152,
        Jpegquality = 0x1153,
        Feedertype = 0x1154,
        Iccprofile = 0x1155,
        Autosize = 0x1156,
        AutomaticCropUsesFrame = 0x1157,
        AutomaticLengthDetection = 0x1158,
        AutomaticColorEnabled = 0x1159,
        AutomaticColorNonColorPixelType = 0x115a,
        ColorManagementEnabled = 0x115b,
        ImageMerge = 0x115c,
        ImageMergeHeightThreshold = 0x115d,
        SupoortedExtImageInfo = 0x115e,
        Audiofileformat = 0x1201,
        Xfermech = 0x1202,
    ICAP_PIXELTYPE = 0x0101,			//Set B/W=0, Gray=1, RGB=2
	CAP_DUPLEX     = 0x1012,   /* Added 1.7 */    	
	CAP_DUPLEXENABLED = 0x1013   /* Added 1.7 */
	}

    internal enum TwICap : short
    {
        TWPT_BW = 0x0000,
        TWPT_GRAY = 0x0001,
        TWPT_RGB = 0x0002
    }
    internal enum TwICapDuplex : short
    {
    	TWDX_NONE	     =0,
		TWDX_1PASSDUPLEX =1,
		TWDX_2PASSDUPLEX =2
    }







// ------------------- STRUCTS --------------------------------------------

	[StructLayout(LayoutKind.Sequential, Pack=2, CharSet=CharSet.Ansi)]
internal class TwIdentity
	{									// TW_IDENTITY
	public IntPtr		Id;
	public TwVersion	Version;
	public short		ProtocolMajor;
	public short		ProtocolMinor;
	public int			SupportedGroups;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=34)]
	public string		Manufacturer;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=34)]
	public string		ProductFamily;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=34)]
	public string		ProductName;
	}

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwainFrame
    {
        public TwFix32 Left;
        public TwFix32 Top;
        public TwFix32 Right;
        public TwFix32 Bottom;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwainImageLayout
    {
        public TwainFrame Frame;
        public int DocumentNumber;
        public int PageNumber;
        public int FrameNumber;
    }

	[StructLayout(LayoutKind.Sequential, Pack=2, CharSet=CharSet.Ansi)]
internal struct TwVersion
	{									// TW_VERSION
	public short		MajorNum;
	public short		MinorNum;
	public short		Language;
	public short		Country;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=34)]
	public string		Info;
	}

	[StructLayout(LayoutKind.Sequential, Pack=2)]
internal class TwUserInterface
	{									// TW_USERINTERFACE
	public short		ShowUI;				// bool is strictly 32 bit, so use short
	public short		ModalUI;
	public IntPtr		ParentHand;
	}

	[StructLayout(LayoutKind.Sequential, Pack=2)]
internal class TwStatus
	{									// TW_STATUS
	public short		ConditionCode;		// TwCC
	public short		Reserved;
	}

	[StructLayout(LayoutKind.Sequential, Pack=2)]
internal struct TwEvent
	{									// TW_EVENT
	public IntPtr		EventPtr;
	public short		Message;
	}


	[StructLayout(LayoutKind.Sequential, Pack=2)]
internal class TwImageInfo
	{									// TW_IMAGEINFO
	public int			XResolution;
	public int			YResolution;
	public int			ImageWidth;
	public int			ImageLength;
	public short		SamplesPerPixel;
	[MarshalAs( UnmanagedType.ByValArray, SizeConst=8)] 
	 public short[]		BitsPerSample;
	public short		BitsPerPixel;
	public short		Planar;
	public short		PixelType;
	public short		Compression;
	}

	[StructLayout(LayoutKind.Sequential, Pack=2)]
internal class TwPendingXfers
	{									// TW_PENDINGXFERS
	public short		Count;
	public int			EOJ;
	}






	[StructLayout(LayoutKind.Sequential, Pack=2)]
internal struct TwFix32
	{												// TW_FIX32
	public float		Whole;
	public ushort		Frac;
	
	public float ToFloat(float prmWhole)
		{
		return prmWhole + ( (float)Frac /65536.0f );
		}
	public void FromFloat( float f )
		{
		int i = (int)((f * 65536.0f) + 0.5f);
		Whole = (short) (i >> 16);
		Frac = (ushort) (i & 0x0000ffff);
		}
	}







	[StructLayout(LayoutKind.Sequential, Pack=2)]
internal class TwCapability
	{									// TW_CAPABILITY
	public TwCapability( TwCap cap )
		{
		Cap = (short) cap;
		ConType = -1;
		}
    
	public TwCapability( TwCap cap, short sval )
		{
		Cap = (short) cap;
		ConType = (short) TwOn.One;
		Handle = Twain.GlobalAlloc( 0x42, 6 );
		IntPtr pv = Twain.GlobalLock( Handle );
		Marshal.WriteInt16( pv, 0, (short) TwType.Int16 );
		Marshal.WriteInt32( pv, 2, (int) sval );
		Twain.GlobalUnlock( Handle );
		}
    public TwCapability(TwCap cap, short sval, TwType type)
    {
        Cap = (short)cap;
        ConType = (short)TwOn.One;
        Handle = Twain.GlobalAlloc(0x42, 6);
        IntPtr pv = Twain.GlobalLock(Handle);
        Marshal.WriteInt16(pv, 0, (short)type); Marshal.WriteInt32(pv, 2, (int)sval);
        Twain.GlobalUnlock(Handle);
    }
    
	~TwCapability()
		{
		if( Handle != IntPtr.Zero )
			Twain.GlobalFree( Handle );
		}
	public short		Cap;
	public short		ConType;
	public IntPtr		Handle;
	}








} // namespace TwainLib
