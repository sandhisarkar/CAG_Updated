using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Timers;

namespace TwainLib
{

	public enum TwainCommand
	{
	Not				= -1,
	Null			= 0,
	TransferReady	= 1,
	CloseRequest	= 2,
	CloseOk			= 3,
	DeviceEvent		= 4
	}
public class Twain
	{
	public delegate void ImageNotification(System.IntPtr prmHbmp);
    public delegate void TwainEventNotification(string pNotification);
	private const short CountryUSA		= 1;
	private const short LanguageUSA		= 13;
	public const short __BLACKWHITE	= 1;
	public const short __COLOUR		= 2;
    //static System.Timers.Timer timer;
    bool CancelScanning = false;
    public delegate void NotifyCancellation();
    TwainEventNotification TEnv = null;
    public void SetNotification(TwainEventNotification pTEvn)
    {
        TEnv = pTEvn;
    }
	public Twain()
		{
		appid = new TwIdentity();
		appid.Id				= IntPtr.Zero;
		appid.Version.MajorNum	= 1;
		appid.Version.MinorNum	= 1;
		appid.Version.Language	= LanguageUSA;
		appid.Version.Country	= CountryUSA;
		appid.Version.Info		= "Hack 1";
		appid.ProtocolMajor		= TwProtocol.Major;
		appid.ProtocolMinor		= TwProtocol.Minor;
		appid.SupportedGroups	= (int)(TwDG.Image | TwDG.Control);
		appid.Manufacturer		= "NETMaster";
		appid.ProductFamily		= "Freeware";
		appid.ProductName		= "Hack";

		srcds = new TwIdentity();
		srcds.Id = IntPtr.Zero;
        

		evtmsg.EventPtr = Marshal.AllocHGlobal( Marshal.SizeOf( winmsg ) );
		}

	~Twain()
		{
		Marshal.FreeHGlobal( evtmsg.EventPtr );
		}
	public void Init( IntPtr hwndp )
		{
		Finish();
		TwRC rc = DSMparent( appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwndp );
		if( rc == TwRC.Success )
			{
			rc = DSMident( appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, srcds );
			if( rc == TwRC.Success )
				hwnd = hwndp;
			else
				rc = DSMparent( appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp );
			}
		}
    public bool Destruct()
    {
        Marshal.FreeHGlobal(evtmsg.EventPtr);
        return true;
    }
	public bool Select()
		{
		TwRC rc;
		//CloseSrc();
			if( appid.Id == IntPtr.Zero )
			{
				Init( hwnd );
				if( appid.Id == IntPtr.Zero )
					return false;
			}
		rc = DSMident( appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, srcds );
			if(rc != TwRC.Success)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

    public bool Acquire(bool prmShowUI, int prmDuplex)
    {
        TwRC rc;
        AcquireMsg = 0;
        CloseSrc();
        if (appid.Id == IntPtr.Zero)
        {
            Init(hwnd);
            if (appid.Id == IntPtr.Zero)
                return false;
        }
        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
        if (rc != TwRC.Success)
        {
            AcquireMsg = 0;
            return false;
        }

        TwCapability cap = new TwCapability(TwCap.XferCount, 1);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            AcquireMsg = 0;
            CloseSrc();
            return false;
        }


        cap = new TwCapability(TwCap.XferCount, -1, TwType.Int16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            //AcquireMsg++;
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.DuplexEnabled, (short)1, TwType.UInt16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            //AcquireMsg++;
            CloseSrc();
            return false;
        }

        ushort isOk = 0;
        IntPtr p = Twain.GlobalLock(cap.Handle);
        isOk = (ushort)Marshal.ReadInt16(p, 2);
        //isOk = (ushort)Marshal.PtrToStructure(p, typeof(ushort));
        Twain.GlobalUnlock(p);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        if (isOk != 0)
        {
            //Duplex negotiation
            cap = new TwCapability(TwCap.DuplexEnabled, (short)prmDuplex, TwType.UInt16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return false;
            }
        }

        TwUserInterface guif = new TwUserInterface();
        if (prmShowUI == true)
        {
            guif.ShowUI = 0;
        }
        else
        {
            guif.ShowUI = 0;
        }
        guif.ModalUI = 0;
        guif.ParentHand = hwnd;
        //Enable data source
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
        if (rc != TwRC.Success)
        {
            //AcquireMsg++;
            CloseSrc();
            return false;
        }
        

        return true;

    }


    public bool Acquire(bool prmShowUI)
    {
        TwRC rc;
        AcquireMsg = 0;
        CloseSrc();
        if (appid.Id == IntPtr.Zero)
        {
            Init(hwnd);
            if (appid.Id == IntPtr.Zero)
                return false;
        }
        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
        if (rc != TwRC.Success)
        {
            AcquireMsg = 0;
            return false;
        }

        TwCapability cap = new TwCapability(TwCap.XferCount, 1);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            AcquireMsg = 0;
            CloseSrc();
            return false;
        }

       
        cap = new TwCapability(TwCap.XferCount, -1, TwType.Int16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.DuplexEnabled, (short)1, TwType.UInt16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }


        TwUserInterface guif = new TwUserInterface();
        if (prmShowUI == true)
        {
            guif.ShowUI = 0;
        }
        else
        {
            guif.ShowUI = 0;
        }
        guif.ModalUI = 0;
        guif.ParentHand = hwnd;
        //Enable data source
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }


        return true;

    }

    public bool Acquire(bool prmShowUI, bool prmColorMode, short scandpi,short duplex)
    {
        TwRC rc;
        TwCapability cap;
        CloseSrc();
        ScanInColor = prmColorMode;
        if (ScanInColor)
        {
            currColMode = __COLOUR;
        }
        if (appid.Id == IntPtr.Zero)
        {
            Init(hwnd);
            if (appid.Id == IntPtr.Zero)
                return false;
        }
        //Open Data Source
        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
        if (rc != TwRC.Success)
            return false;


        TwCapability capUnit = new TwCapability(TwCap.FeederEnabled, 1, TwType.Int16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capUnit);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        //
        //       TwCapability capUnit1 = new TwCapability(TwCap.CAP_AUTOFEED, 0, TwType.Int16);
        //        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capUnit1);
        //        if (rc != TwRC.Success)
        //        {
        //            CloseSrc();
        //            return false;
        //        }

        //        TwCapability capUnit2 = new TwCapability(TwCap.CAP_AUTOSCAN, 0, TwType.Int16);
        //        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capUnit2);
        //        if (rc != TwRC.Success)
        //        {
        //            CloseSrc();
        //            return false;
        //        }

        cap = new TwCapability(TwCap.XResolution, scandpi, TwType.Fix32);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.Indicators, 0, TwType.Fix32);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        //f32.FromFloat(200);

        cap = new TwCapability(TwCap.YResolution, scandpi, TwType.Fix32);
        //cap.Cap = TwCap.CAP_FEEDERLOADED;
        //cap.ConType = TwOn.One;
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);

        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        if (prmColorMode == true)
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_GRAY, TwType.Int16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return false;
            }
        }
        else
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_BW, TwType.Int16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return false;
            }
        }

        cap = new TwCapability(TwCap.XferCount, -1, TwType.Int16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        ////Duplex
        cap = new TwCapability(TwCap.DuplexEnabled, duplex, TwType.Int16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        TwUserInterface guif = new TwUserInterface();
        if (prmShowUI == true)
        {
            guif.ShowUI = 0;
        }
        else
        {
            guif.ShowUI = 0;
        }
        guif.ModalUI = 0;
        guif.ParentHand = hwnd;
        //Enable data source
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        return true;
    }
    public int AcquireMsg { get; set; }
    public bool Acquire(bool prmShowUI, bool prmColorMode,short scandpi)
    {
        TwRC rc;
        AcquireMsg = 0;
        TwCapability cap;
        //CloseSrc();
        ScanInColor = prmColorMode;
        if (ScanInColor)
        {
        	currColMode = __COLOUR;
        }
        if (appid.Id == IntPtr.Zero)
        {
            Init(hwnd);
            if (appid.Id == IntPtr.Zero)
                return false;
        }
        //Open Data Source
        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            return false;
        }
       	TwCapability capUnit = new TwCapability(TwCap.FeederEnabled, 1, TwType.UInt16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, capUnit);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }
        
        //cap = new TwCapability(TwCap.Automaticdeskew,1,TwType.Bool);
        //rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
                
        cap = new TwCapability(TwCap.XResolution, scandpi, TwType.Fix32);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }


        //f32.FromFloat(200);

        cap = new TwCapability(TwCap.YResolution, scandpi, TwType.Fix32);
        //cap.Cap = TwCap.CAP_FEEDERLOADED;
        //cap.ConType = TwOn.One;
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);

        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }
        if (prmColorMode == true)
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_GRAY, TwType.Int16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                AcquireMsg++;
                CloseSrc();
                return false;
            }
        }
        else
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_BW, TwType.UInt16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                AcquireMsg++;
                CloseSrc();
                return false;
            }
        }

        cap = new TwCapability(TwCap.XferCount, -1, TwType.Int16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.DuplexEnabled, (short)1, TwType.UInt16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }

        TwUserInterface guif = new TwUserInterface();
        if (prmShowUI == true)
        {
            guif.ShowUI = 0;
        }
        else
        {
            guif.ShowUI = 0;
        }
        guif.ModalUI = 0;
        guif.ParentHand = hwnd;
        //Enable data source
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
        if (rc != TwRC.Success)
        {
            AcquireMsg++;
            CloseSrc();
            return false;
        }
        return true;
    }
    //Scan with fixed DPI
    public bool AcquireFixed(bool prmShowUI, bool prmColorMode, int prmPages, int prmDuplex)
    {
        TwRC rc;
        TwCapability cap;
        CloseSrc();
        ScanInColor = prmColorMode;
        if (ScanInColor)
        {
        	currColMode = __COLOUR;
        }
        if (appid.Id == IntPtr.Zero)
        {
            Init(hwnd);
            if (appid.Id == IntPtr.Zero)
                return false;
        }
        //Open Data Source
        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
        if (rc != TwRC.Success)
            return false;

        
        if (prmColorMode == true)
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_GRAY, TwType.UInt16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return false;
            }
        }
        else
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_BW, TwType.UInt16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return false;
            }
        }

        //cap = new TwCapability(TwCap.Automaticdeskew, 1, TwType.Bool);
        //rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        //if (rc != TwRC.Success)
        //{
        //    CloseSrc();
        //    return false;
        //}

        cap = new TwCapability(TwCap.XResolution, 150, TwType.Fix32);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.YResolution, 150, TwType.Fix32);

        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);

        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.XferCount, (short)prmPages, TwType.UInt16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        ////Duplex
        cap = new TwCapability(TwCap.DuplexEnabled, (short)1, TwType.UInt16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        ushort isOk = 0;
        IntPtr p = Twain.GlobalLock(cap.Handle);
        isOk = (ushort)Marshal.ReadInt16(p, 2);
        //isOk = (ushort)Marshal.PtrToStructure(p, typeof(ushort));
        Twain.GlobalUnlock(p);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        if (prmDuplex == ScanMode.SCAN_IN_DUPLEX_MODE)
        {
            
            if (isOk != 0)
            {
                //Duplex negotiation
                cap = new TwCapability(TwCap.DuplexEnabled, (short)prmDuplex, TwType.UInt16);
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return false;
                }
            }
            else
            {
                ///Cancel user request
                MessageBox.Show("Requested feature dose not supported by this scanner, aborting.....");
                CloseSrc();
                return false;
            }
        }
        else
        {
            if (isOk != 0)
            {
                //Simplex negotiation
                cap = new TwCapability(TwCap.DuplexEnabled, (short)ScanMode.SCAN_IN_SIMPLEX_MODE, TwType.UInt16);
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return false;
                }
            }
        }
        
        TwUserInterface guif = new TwUserInterface();
        if (prmShowUI == true)
        {
            guif.ShowUI = 0;
        }
        else
        {
            guif.ShowUI = 0;
        }
        guif.ModalUI = 1;
        guif.ParentHand = hwnd;
        //Enable data source
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        return true;
    }

    //Scan with variable DPI
    //sumit exp
public bool AcquireFixed(bool prmShowUI, bool prmColorMode, int prmPages, int prmDuplex, short pDPI)
    {
        TwRC rc;
        TwCapability cap;
        CloseSrc();
        ScanInColor = prmColorMode;
        if (ScanInColor)
        {
            currColMode = __COLOUR;
        }
        if (appid.Id == IntPtr.Zero)
        {
            Init(hwnd);
            if (appid.Id == IntPtr.Zero)
                return false;
        }
        //Open Data Source
        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
        if (rc != TwRC.Success)
            return false;


        if (prmColorMode == true)
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_GRAY, TwType.UInt16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return false;
            }
        }
        else
        {
            cap = new TwCapability(TwCap.IPixelType, (short)TwICap.TWPT_BW, TwType.UInt16);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return false;
            }
        }

        //cap = new TwCapability(TwCap.Automaticdeskew, 1, TwType.Bool);
        //rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        //if (rc != TwRC.Success)
        //{
        //    CloseSrc();
        //    return false;
        //}

        cap = new TwCapability(TwCap.XResolution, pDPI, TwType.Fix32);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.YResolution, pDPI, TwType.Fix32);

        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);

        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        cap = new TwCapability(TwCap.XferCount, (short)prmPages, TwType.Int16);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        ///Duplex checking
        cap = new TwCapability(TwCap.Duplex);
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Get, cap);
       
        ushort isOk = 0;
        IntPtr p = Twain.GlobalLock(cap.Handle);
        isOk = (ushort)Marshal.ReadInt16(p, 2);
        //isOk = (ushort)Marshal.PtrToStructure(p, typeof(ushort));
        Twain.GlobalUnlock(p);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }

        if (prmDuplex == ScanMode.SCAN_IN_DUPLEX_MODE)
        {

            if (isOk != 0)
            {
                //Duplex negotiation
                cap = new TwCapability(TwCap.DuplexEnabled, (short)prmDuplex, TwType.UInt16);
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return false;
                }
            }
            else
            {
                ///Cancel user request
                MessageBox.Show("Requested feature dose not supported by this scanner, aborting.....");
                CloseSrc();
                return false;
            }
        }
        else
        {
            if (isOk != 0)
            {
                //Simplex negotiation
                cap = new TwCapability(TwCap.DuplexEnabled, (short)ScanMode.SCAN_IN_SIMPLEX_MODE, TwType.UInt16);
                rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return false;
                }
            }
        }

        TwUserInterface guif = new TwUserInterface();
        if (prmShowUI == true)
        {
            guif.ShowUI = 0;
        }
        else
        {
            guif.ShowUI = 0;
        }
        guif.ModalUI = 1;
        guif.ParentHand = hwnd;
        //Enable data source
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return false;
        }
        return true;
    }
    
    
    public void GetCancelNote()
    {
        CancelScanning = true;
    }
    public bool ReqChangeMode(short prmColour)
    {
    	if (prmColour==__COLOUR || prmColour==__BLACKWHITE)
    	{
    		reqColMode = prmColour;
    		ScanInColor = true;
    		return true;
    	}
    	else
    	{
    		ScanInColor = false;
    		return false;
    	}
    }
    public int GetScanMode()
    {
    	return currColMode;
    }
    
    public int TransferPictures(ImageNotification imgNot, IWin32Window prmWin)
		{
        int retVal = 0;
		ArrayList pics = new ArrayList();
        //DialogResult result;
		if( srcds.Id == IntPtr.Zero )
			return 0;

		TwRC rc;
		IntPtr hbitmap = IntPtr.Zero;
		TwPendingXfers pxfr = new TwPendingXfers();
        TwPendingXfers pxstatus = new TwPendingXfers();
        TwCapability cap;
        long PageLocator=0;					//To find out whether it's scanned or returned from already scanned
        //bool isShown = false;
        int i = 0;
        CancelScanning = false;
        try
        {
            do
            {
                pxfr.Count = 0;
                hbitmap = IntPtr.Zero;

                TwImageInfo iinf = new TwImageInfo();
                
                rc = DSiinf(appid, srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf);
                if (rc != TwRC.Success)
                {
                    if (rc == TwRC.Cancel)
                    {
                        rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);
                        rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);
                        //CloseSrc();
                    }
                    //continue;
                    CloseSrc();
                    return 0;
                }
                else
                {
                    rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);
                    if (rc == TwRC.Failure || rc == TwRC.Cancel)
                    {
                        retVal = reqColMode;
                        currColMode = reqColMode;
                        rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
                        //CloseSrc();
                        return -1;
                    }
                    if (rc != TwRC.XferDone)
                    {
                        //MessageBox.Show("Hi");
                    }
                    else
                    {
                        PageLocator++;
                    }
                    i++;
                    
                    imgNot.Invoke(hbitmap);
                    rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);

                    //Block to swap color/black & white mode
                    /*
                    if ((PageLocator % 2) == 0)
                    {
                        if (ScanInColor==true)
                        {
                            if (currColMode != reqColMode)
                            {
                                retVal = reqColMode;
                                currColMode = reqColMode;
                                rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
                                CloseSrc();
                                return currColMode;
                            }
                        }
                    }
                    //End: Block to swap color/black & white mode
                    */
                    if (pxfr.Count == 0)
                    {
                        break;
                    }
                    /*
                    if (pxfr.Count == 0)
                    {
                        do
                        {
                            cap = new TwCapability(TwCap.FeederLoaded);
                            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Get, cap);
                            TwPendingXfers tmpStore = new TwPendingXfers();
                            tmpStore = (TwPendingXfers)Marshal.PtrToStructure(GlobalLock(cap.Handle), typeof(TwPendingXfers));
                            Application.DoEvents();
                            if (CancelScanning)
                            {
                                //CloseSrc();
                                retVal = 0;
                                //not.Close();
                                break;
                            }
                            if (tmpStore.EOJ == 1)
                            {
                                rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
                                retVal = 1;
                                //CloseSrc();
                                break;
                            }
                        }
                        while (1 == 1);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                    //pics.Add(hbitmap);
                    */
                }
            }
            while (1 == 1);
            
            //rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, srcds);
            TwUserInterface guif = new TwUserInterface();
            guif.ShowUI = 0;
            guif.ModalUI = 0;
            guif.ParentHand = hwnd;
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif);
            
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return 0;
            }

            if (rc != TwRC.Success)
            {
            	retVal = 0;
            }
            else
            {
            	retVal = 1;
            }
        }
        catch (Exception ex)
        {
            CloseSrc();
            string err = ex.Message;
        }

		return retVal;
		}
	public int TransferPicturesFixed(ImageNotification imgNot, IWin32Window prmWin)
		{
		int retVal = 0;
		ArrayList pics = new ArrayList();
		if( srcds.Id == IntPtr.Zero )
			return retVal;

		TwRC rc;
		IntPtr hbitmap = IntPtr.Zero;
		TwPendingXfers pxfr = new TwPendingXfers();
        //int i = 0;
		do
		{
			pxfr.Count = 0;
			hbitmap = IntPtr.Zero;

            TwImageInfo iinf = new TwImageInfo();
            rc = DSiinf(appid, srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf);
            if (rc != TwRC.Success)
            {
                if (rc == TwRC.Cancel)
                {
                    rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);
                    rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);
                    CloseSrc();
                }
                //continue;
                CloseSrc();
                return 0;
            }
            else
            {
                rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);

                if (rc != TwRC.XferDone)
                {
                    CloseSrc();
                    return retVal;
                }
                imgNot.Invoke(hbitmap);
                rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return retVal;
                }
            }
		}
		while( pxfr.Count != 0 );
		retVal=1;
		//rc = DSpxfer( appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr );
        TwUserInterface guif = new TwUserInterface();
        guif.ShowUI = 0;
        guif.ModalUI = 0;
        guif.ParentHand = hwnd;
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif);
        if (rc != TwRC.Success)
        {
            CloseSrc();
            return 0;
        }
		return retVal;
		}
	
    private void SetAcquire(Object obj, ElapsedEventArgs e)
    {
        SendKeys.SendWait("{Enter}");
        //timer.Close();
    }
	public TwainCommand PassMessage( ref Message m )
		{
		if( srcds.Id == IntPtr.Zero )
			return TwainCommand.Not;

		int pos = GetMessagePos();

		winmsg.hwnd		= m.HWnd;
		winmsg.message	= m.Msg;
		winmsg.wParam	= m.WParam;
		winmsg.lParam	= m.LParam;
		winmsg.time		= GetMessageTime();
		//winmsg.x		= (short) pos;
		//winmsg.y		= (short) (pos >> 16);
        winmsg.x = pos;
        winmsg.y = (pos >> 16);
		
		Marshal.StructureToPtr( winmsg, evtmsg.EventPtr, false );
		evtmsg.Message = 0; 
		TwRC rc = DSevent( appid, srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref evtmsg );
        string nMessage = string.Empty;
        switch (evtmsg.Message)
        {
            case (short)TwMSG.Null: { nMessage = "No message…"; break; }
            case (short)TwMSG.Get: { nMessage = "Get…"; break; }
            case (short)TwMSG.GetCurrent: { nMessage = "Get Current…"; break; }
            case (short)TwMSG.GetDefault: { nMessage = "Get Default…"; break; }
            case (short)TwMSG.GetFirst: { nMessage = "Get First…"; break; }
            case (short)TwMSG.GetNext: { nMessage = "Get Next…"; break; }
            case (short)TwMSG.Set: { nMessage = "Set…"; break; }
            case (short)TwMSG.Reset: { nMessage = "Reset…"; break; }
            case (short)TwMSG.QuerySupport: { nMessage = "Query Support…"; break; }

            case (short)TwMSG.XFerReady: { nMessage = "Transfer Ready..."; break; }
            case (short)TwMSG.CloseDSReq: { nMessage = "Close DS Requested…"; break; }
            case (short)TwMSG.CloseDSOK: { nMessage = "DS Closed OK!..."; break; }
            case (short)TwMSG.DeviceEvent: { nMessage = "Something Happened in the device…"; break; }

            case (short)TwMSG.CheckStatus: { nMessage = "Checking Status…"; break; }

            case (short)TwMSG.OpenDSM: { nMessage = "Open DSM Requested…"; break; }
            case (short)TwMSG.CloseDSM: { nMessage = "Close DSM Requested…"; break; }

            case (short)TwMSG.OpenDS: { nMessage = "Opening DS…"; break; }
            case (short)TwMSG.CloseDS: { nMessage = "Closing DS…"; break; }
            case (short)TwMSG.UserSelect: { nMessage = "Selection Requested…"; break; }

            case (short)TwMSG.DisableDS: { nMessage = "Disabling DS…"; break; }
            case (short)TwMSG.EnableDS: { nMessage = "Enabling DS…"; break; }
            case (short)TwMSG.EnableDSUIOnly: { nMessage = "Enabling DS UI Only…"; break; }

            case (short)TwMSG.ProcessEvent: { nMessage = "Processing Event…"; break; }

            case (short)TwMSG.EndXfer: { nMessage = "Ending Transfer…"; break; }
            case (short)TwMSG.StopFeeder: { nMessage = "Stopping Feeder…"; break; }

            case (short)TwMSG.ChangeDirectory: { nMessage = "Changing Directory…"; break; }
            case (short)TwMSG.CreateDirectory: { nMessage = "Creating Directory…"; break; }
            case (short)TwMSG.Delete: { nMessage = "Deleting…"; break; }
            case (short)TwMSG.FormatMedia: { nMessage = "Formating Media…"; break; }
            case (short)TwMSG.GetClose: { nMessage = "Get Close…"; break; }
            case (short)TwMSG.GetFirstFile: { nMessage = "Get First File…"; break; }
            case (short)TwMSG.GetInfo: { nMessage = "Get Info…"; break; }
            case (short)TwMSG.GetNextFile: { nMessage = "Get Next File…"; break; }
            case (short)TwMSG.Rename: { nMessage = "Rename…"; break; }
            case (short)TwMSG.Copy: { nMessage = "Copy…"; break; }
            case (short)TwMSG.AutoCaptureDir: { nMessage = "Auto Capture Dir…"; break; }

            case (short)TwMSG.PassThru: { nMessage = "Pass Through…"; break; }
            default: { nMessage = "Unknown Message..."; break; }
        }
        if (TEnv != null)
        {
            TEnv.Invoke(nMessage + "Time happened: " + winmsg.time.ToString());
        }
        
        if (rc == TwRC.NotDSEvent)
        {
            return TwainCommand.Not;
        }
		if( evtmsg.Message == (short) TwMSG.XFerReady )
			return TwainCommand.TransferReady;
		if( evtmsg.Message == (short) TwMSG.CloseDSReq )
			return TwainCommand.CloseRequest;
		if( evtmsg.Message == (short) TwMSG.CloseDSOK )
			return TwainCommand.CloseOk;
		if( evtmsg.Message == (short) TwMSG.DeviceEvent )
			return TwainCommand.DeviceEvent;

		return TwainCommand.Null;
		}
    
    public void CloseSrc()
    {
        
        TwRC rc;
        string CloseSuccessfully = string.Empty;
        TwPendingXfers pxfr = new TwPendingXfers();
        pxfr.Count = 0;
        if (srcds.Id != IntPtr.Zero)
        {
            TwUserInterface guif = new TwUserInterface();
            rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);
            CloseSuccessfully = "End Transfer: " + rc.ToString() + "\r\n";
            rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
            CloseSuccessfully += "Transfer reset: " + rc.ToString() + "\r\n";
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif);
            CloseSuccessfully += "Disable Ds: " + rc.ToString() + "\r\n";
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, srcds);
            CloseSuccessfully += "Close Ds: " + rc.ToString() + "\r\n";
            if (TEnv != null)
            {
                TEnv.Invoke(CloseSuccessfully);
            }
        }
        
    }

	public void Finish()
		{
		TwRC rc;
		//CloseSrc();
		if( appid.Id != IntPtr.Zero )
			rc = DSMparent( appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwnd );
		appid.Id = IntPtr.Zero;
		}

	private IntPtr		hwnd;
	private TwIdentity	appid;
	private TwIdentity	srcds;
	private TwEvent		evtmsg;
	private WINMSG		winmsg;
	private bool 		ScanInColor;	//Whether colour scanning is required
	private int			currColMode;	//Current colour mode for scanning
	private int			reqColMode;		//Requested colour mode for scanning

	// ------ DSM entry point DAT_ variants:
		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSMparent( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr refptr );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSMident( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity idds );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSMstatus( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat );


	// ------ DSM entry point DAT_ variants to DS:
		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSuserif( [In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface guif );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSevent( [In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent evt );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSstatus( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DScap( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability capa );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSiinf( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imginf );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSixfer( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSpxfer( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pxfr );

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        internal static extern TwRC DSilayout([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwainImageLayout layout);

        private delegate TwRC _DSimagelayuot([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwainImageLayout imageLayuot);

		[DllImport("kernel32.dll", ExactSpelling=true)]
	internal static extern IntPtr GlobalAlloc( int flags, int size );
		[DllImport("kernel32.dll", ExactSpelling=true)]
	public static extern IntPtr GlobalLock( IntPtr handle );
		[DllImport("kernel32.dll", ExactSpelling=true)]
	public static extern bool GlobalUnlock( IntPtr handle );
		[DllImport("kernel32.dll", ExactSpelling=true)]
	internal static extern IntPtr GlobalFree( IntPtr handle );

		[DllImport("user32.dll", ExactSpelling=true)]
	private static extern int GetMessagePos();
		[DllImport("user32.dll", ExactSpelling=true)]
	private static extern int GetMessageTime();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr LoadLibrary(string fileName);
    
		[DllImport("gdi32.dll", ExactSpelling=true)]
	private static extern int GetDeviceCaps( IntPtr hDC, int nIndex );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
	private static extern IntPtr CreateDC( string szdriver, string szdevice, string szoutput, IntPtr devmode );

		[DllImport("gdi32.dll", ExactSpelling=true)]
	private static extern bool DeleteDC( IntPtr hdc );

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, int procId);



	public static int ScreenBitDepth {
		get {
			IntPtr screenDC = CreateDC( "DISPLAY", null, null, IntPtr.Zero );
			int bitDepth = GetDeviceCaps( screenDC, 12 );
			bitDepth *= GetDeviceCaps( screenDC, 14 );
			DeleteDC( screenDC );
			return bitDepth;
			}
		}


		[StructLayout(LayoutKind.Sequential, Pack=4)]
	internal struct WINMSG
		{
		public IntPtr		hwnd;
		public int			message;
		public IntPtr		wParam;
		public IntPtr		lParam;
		public int			time;
		public int			x;
		public int			y;
		}
	} // class Twain
}
