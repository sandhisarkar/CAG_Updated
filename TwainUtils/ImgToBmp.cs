using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Drawing.Imaging;
using NovaNet;

//
// Created by SharpDevelop.
// User: SubhajitB
// Date: 6/5/2009
// Time: 10:30 AM
// 
// To change this template use Tools | Options | Coding | Edit Standard Headers.
//
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel;

namespace ToBitmap
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)] 
  public struct BITMAPINFO
  { public uint biSize;
    public int biWidth, biHeight;
    public short biPlanes, biBitCount;
    public uint biCompression, biSizeImage;
    public int biXPelsPerMeter, biYPelsPerMeter;
    public uint biClrUsed, biClrImportant;
    [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=256)]
    public uint[] cols;
  }
	public class scanToImage
	{

		[DllImport("gdi32.dll", ExactSpelling = true)]
		static internal extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst, int width, int height, int xsrc, int ysrc, int start, int lines, IntPtr bitsptr, 
		IntPtr bmiptr, int color);
		[DllImport("kernel32.dll", ExactSpelling = true)]
		static internal extern IntPtr GlobalLock(IntPtr handle);
		[DllImport("kernel32.dll", ExactSpelling = true)]
		static internal extern IntPtr GlobalFree(IntPtr handle);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern void OutputDebugString(string outstr);
		[DllImport("dibp.dll", ExactSpelling = true)]
		public static extern int WriteDIB(string flName, IntPtr imgData);
		[DllImport("dibp.dll", ExactSpelling = true)]
		public static extern int SaveBitmap(IntPtr imgData);
  [System.Runtime.InteropServices.DllImport("gdi32.dll")]
  public static extern bool DeleteObject(IntPtr hObject);

  [System.Runtime.InteropServices.DllImport("user32.dll")]
  public static extern int InvalidateRect(IntPtr hwnd, IntPtr rect, int bErase);

  [System.Runtime.InteropServices.DllImport("user32.dll")]
  public static extern IntPtr GetDC(IntPtr hwnd);

  [System.Runtime.InteropServices.DllImport("gdi32.dll")]
  public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

  [System.Runtime.InteropServices.DllImport("user32.dll")]
  public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

  [System.Runtime.InteropServices.DllImport("gdi32.dll")]
  public static extern int DeleteDC(IntPtr hdc);

  [System.Runtime.InteropServices.DllImport("gdi32.dll")]
  public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

  [System.Runtime.InteropServices.DllImport("gdi32.dll")]
  public static extern int BitBlt(IntPtr hdcDst, int xDst, int yDst, int w, int h, IntPtr hdcSrc, int xSrc, int ySrc, int rop);
  static int SRCCOPY = 0x00CC0020;

  [System.Runtime.InteropServices.DllImport("gdi32.dll")]
  static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO bmi, uint Usage, out IntPtr bits, IntPtr hSection, uint dwOffset); 
  static uint BI_RGB = 0;
  static uint DIB_RGB_COLORS=0;

  static uint MAKERGB(int r,int g,int b)
  { return ((uint)(b&255)) | ((uint)((r&255)<<8)) | ((uint)((g&255)<<16));
  }
		
		//[DllImport("gdiplus.dll", ExactSpelling = true)]
        //private static extern int GdipCreateBitmapFromGdiDib(IntPtr bminfo, IntPtr pixdat, ref IntPtr image);
        [DllImport("GdiPlus.dll", 
   CharSet=CharSet.Unicode, ExactSpelling=true)] 
private static extern int GdipCreateBitmapFromGdiDib(IntPtr pBIH, 
                          IntPtr pPix, out IntPtr pBitmap); 


		BITMAPINFOHEADER bmi;
		Rectangle bmprect;
		IntPtr dibhand;
		IntPtr bmpptr;
		IntPtr pixptr;
        string fileName;
        static System.Threading.Mutex mut = new System.Threading.Mutex();

		public scanToImage(IntPtr dibhandp,string prmFileName)
		{
			bmprect = new Rectangle(0, 0, 0, 0);
			dibhand = dibhandp;
			bmpptr = GlobalLock(dibhand);
			pixptr = GetPixelInfo(bmpptr);
            fileName = prmFileName;
		}

		protected IntPtr GetPixelInfo(IntPtr bmpptr)
		{
            bmi = new BITMAPINFOHEADER();

            Marshal.PtrToStructure(bmpptr, bmi);
            
            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)bmpptr;
            return (IntPtr)p;
		}

		public Bitmap ImgToBitmap()
		{
            bmprect = new Rectangle(0, 0, 0, 0);
            //dibhand = dibhandp;
            bmpptr = GlobalLock(dibhand);
            pixptr = GetPixelInfo(bmpptr);

            GdiPlusLib.Gdip.SaveDIBAs(fileName, bmpptr, pixptr);
            Bitmap bmp = new Bitmap(bmprect.Width, bmprect.Height);
            bmp.SetResolution(200f, 200f);
            Graphics tempGrap = Graphics.FromImage(bmp);
            IntPtr intpt = tempGrap.GetHdc();

            SetDIBitsToDevice(intpt, 0, 0, bmprect.Width, bmprect.Height, 0, 0, 0, bmprect.Height, pixptr, bmpptr, 0);
            tempGrap.ReleaseHdc(intpt);
            tempGrap.Dispose();
            GlobalFree(dibhand);
            dibhand = IntPtr.Zero;

            return (bmp);
		}
        /*
		//With gdPicture
		//Saves the bitmap in the given file after converting to 1 BPP from HBITMAP
		public static bool gdConvertFile1BPP(IntPtr b, string flName)
		{
			int imageNo;
			try
			{
				GdPicture.GdPictureImaging gdi = new GdPicture.GdPictureImaging();
				gdi.SetLicenseNumber(NovaNet.Utils.Constants.LIC);
				imageNo=gdi.CreateGdPictureImageFromDIB(b);
				gdi.ConvertTo1Bpp(imageNo);
				gdi.SaveAsTIFF(imageNo,flName,GdPicture.TiffCompression.TiffCompressionCCITT4);
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
        /*
		//With gdPicture
		//Saves the bitmap in the given file after converting to 1 BPP from Source File
		public static bool gdConvertFile1BPP(string b, string flName)
		{
			int imageNo;
			try
			{
				GdPicture.GdPictureImaging gdi = new GdPicture.GdPictureImaging();
				gdi.SetLicenseNumber(NovaNet.Utils.Constants.LIC);
				imageNo=gdi.CreateGdPictureImageFromFile(b) ;
				gdi.ConvertTo1Bpp(imageNo);
				gdi.SaveAsTIFF(imageNo,flName,GdPicture.TiffCompression.TiffCompressionCCITT4);
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
        /*
		//With gdPicture
		//Saves the bitmap in the given file after converting to 1 BPP from Source File
		public static bool gdConvertFile1BPP(GdPicture.GdPictureImaging gdi, int imageNo, string flName)
		{
			try
			{
				gdi.ConvertTo1Bpp(imageNo);
				gdi.SaveAsTIFF(imageNo,flName,GdPicture.TiffCompression.TiffCompressionCCITT4);
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
         */
        /*
		//With gdPicture
		//Saves the bitmap in the given file after converting to 1 BPP
		public static bool gdSaveFile(GdPicture.GdPictureImaging gdi, int imageNo, string flName)
		{
			try
			{
				switch (gdi.GetBitDepth(imageNo))
				{
				case 24:
						gdi.SaveAsTIFF(imageNo,flName,GdPicture.TiffCompression.TiffCompressionLZW);
						break;
				default:
						gdi.SaveAsTIFF(imageNo,flName,GdPicture.TiffCompression.TiffCompressionCCITT4);
						break;
				}
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
         */
		/// <summary>
		/// Copies a bitmap into a 1bpp/8bpp bitmap of the same dimensions, fast
		/// </summary>
		/// <param name="b">original bitmap</param>
		/// <param name="bpp">1 or 8, target bpp</param>
		/// <returns>a 1bpp copy of the bitmap</returns>
		public static System.Drawing.Bitmap CopyToBpp(System.Drawing.Bitmap b, int bpp)
		{ if (bpp!=1 && bpp!=8) throw new System.ArgumentException("1 or 8","bpp");
		
		  // Plan: built into Windows GDI is the ability to convert
		  // bitmaps from one format to another. Most of the time, this
		  // job is actually done by the graphics hardware accelerator card
		  // and so is extremely fast. The rest of the time, the job is done by
		  // very fast native code.
		  // We will call into this GDI functionality from C#. Our plan:
		  // (1) Convert our Bitmap into a GDI hbitmap (ie. copy unmanaged->managed)
		  // (2) Create a GDI monochrome hbitmap
		  // (3) Use GDI "BitBlt" function to copy from hbitmap into monochrome (as above)
		  // (4) Convert the monochrone hbitmap into a Bitmap (ie. copy unmanaged->managed)
		  
		  int w=b.Width, h=b.Height;
		  IntPtr hbm = b.GetHbitmap(); // this is step (1)
		  //
		  // Step (2): create the monochrome bitmap.
		  // "BITMAPINFO" is an interop-struct which we define below.
		  // In GDI terms, it's a BITMAPHEADERINFO followed by an array of two RGBQUADs
		  BITMAPINFO bmi = new BITMAPINFO();
		  bmi.biSize=40;  // the size of the BITMAPHEADERINFO struct
		  bmi.biWidth=w;
		  bmi.biHeight=h;
		  bmi.biPlanes=1; // "planes" are confusing. We always use just 1. Read MSDN for more info.
		  bmi.biBitCount=(short)bpp; // ie. 1bpp or 8bpp
		  bmi.biCompression=BI_RGB; // ie. the pixels in our RGBQUAD table are stored as RGBs, not palette indexes
		  bmi.biSizeImage = (uint)(((w+7)&0xFFFFFFF8)*h/8);
		  bmi.biXPelsPerMeter=1000000; // not really important
		  bmi.biYPelsPerMeter=1000000; // not really important
		  // Now for the colour table.
		  uint ncols = (uint)1<<bpp; // 2 colours for 1bpp; 256 colours for 8bpp
		  bmi.biClrUsed=ncols;
		  bmi.biClrImportant=ncols;
		  bmi.cols=new uint[256]; // The structure always has fixed size 256, even if we end up using fewer colours
		  if (bpp==1) {bmi.cols[0]=MAKERGB(0,0,0); bmi.cols[1]=MAKERGB(255,255,255);}
		  else {for (int i=0; i<ncols; i++) bmi.cols[i]=MAKERGB(i,i,i);}
		  // For 8bpp we've created an palette with just greyscale colours.
		  // You can set up any palette you want here. Here are some possibilities:
		  // greyscale: for (int i=0; i<256; i++) bmi.cols[i]=MAKERGB(i,i,i);
		  // rainbow: bmi.biClrUsed=216; bmi.biClrImportant=216; int[] colv=new int[6]{0,51,102,153,204,255};
		  //          for (int i=0; i<216; i++) bmi.cols[i]=MAKERGB(colv[i/36],colv[(i/6)%6],colv[i%6]);
		  // optimal: a difficult topic: http://en.wikipedia.org/wiki/Color_quantization
		  // 
		  // Now create the indexed bitmap "hbm0"
		  IntPtr bits0; // not used for our purposes. It returns a pointer to the raw bits that make up the bitmap.
		  IntPtr hbm0 = CreateDIBSection(IntPtr.Zero,ref bmi,DIB_RGB_COLORS,out bits0,IntPtr.Zero,0);
		  //
		  // Step (3): use GDI's BitBlt function to copy from original hbitmap into monocrhome bitmap
		  // GDI programming is kind of confusing... nb. The GDI equivalent of "Graphics" is called a "DC".
		  IntPtr sdc = GetDC(IntPtr.Zero);       // First we obtain the DC for the screen
		   // Next, create a DC for the original hbitmap
		  IntPtr hdc = CreateCompatibleDC(sdc); SelectObject(hdc,hbm); 
		  // and create a DC for the monochrome hbitmap
		  IntPtr hdc0 = CreateCompatibleDC(sdc); SelectObject(hdc0,hbm0);
		  // Now we can do the BitBlt:
		  BitBlt(hdc0,0,0,w,h,hdc,0,0,SRCCOPY);
		  // Step (4): convert this monochrome hbitmap back into a Bitmap:
		  System.Drawing.Bitmap b0 = System.Drawing.Bitmap.FromHbitmap(hbm0);
		  b0.Save("./Test1.TIF",ImageFormat.Tiff);
		  //
		  // Finally some cleanup.
		  DeleteDC(hdc);
		  DeleteDC(hdc0);
		  ReleaseDC(IntPtr.Zero,sdc);
		  DeleteObject(hbm);
		  DeleteObject(hbm0);
		  //
		  return b0;
		}
	}
	
}
