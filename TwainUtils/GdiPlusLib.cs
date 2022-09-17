using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ToBitmap;
using System.Reflection;

namespace GdiPlusLib
{


public class Gdip
	{
	private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

	private static bool GetCodecClsid( string filename, out Guid clsid )
		{
		clsid = Guid.Empty;
		string ext = Path.GetExtension( filename );
		if( ext == null )
			return false;
		ext = "*" + ext.ToUpper();
		foreach( ImageCodecInfo codec in codecs )
			{
			if( codec.FilenameExtension.IndexOf( ext ) >= 0 )
				{
				clsid = codec.Clsid;
				return true;
				}
			}
		return false;
		}

		public static Bitmap BitmapFromDIB(IntPtr pDIB,IntPtr pPix) 
		{ 
		
		
		    MethodInfo mi = typeof(Bitmap).GetMethod("FromGDIplus", 
		                    BindingFlags.Static | BindingFlags.NonPublic); 
		
		    if (mi == null) 
		        return null; // (permission problem) 
		
		
		    IntPtr pBmp = IntPtr.Zero; 
		    int status = GdipCreateBitmapFromGdiDib(pDIB, pPix, out pBmp); 
		
		    if ((status == 0) && (pBmp != IntPtr.Zero)) // success 
		    {
		    	//ToBitmap.scanToImage.SaveBitmap(pBmp);
				Bitmap x = (Bitmap)mi.Invoke(null, new object[] {pBmp});
				x.Save("D:/Test.bmp");
		        return (Bitmap)mi.Invoke(null, new object[] {pBmp}); 
		    }
		
		    else 
		        return null; // failure 
		
		}
	public static bool SaveDIBAs( string picname, IntPtr bminfo, IntPtr pixdat )
		{
            //SaveFileDialog sd = new SaveFileDialog();

            Guid clsid;
            if (!GetCodecClsid(picname, out clsid))
            {
                MessageBox.Show("Unknown picture format for extension " + Path.GetExtension(picname),
                                "Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            IntPtr img = IntPtr.Zero;
            BitmapFromDIB(bminfo, pixdat);
            /*
            int st = GdipCreateBitmapFromGdiDib(bminfo, pixdat, ref img);
            if ((st != 0) || (img == IntPtr.Zero))
                return false;

            //Resolution stuff
            int DpiX = 300;//default X resolution
            int DpiY = 300;//default Y resolution
            BITMAPINFOHEADER bmi;
            bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(bminfo, bmi);
            if (bmi.biXPelsPerMeter > 0)
                DpiX = (int)((float)bmi.biXPelsPerMeter / 39.37008f + 0.5f);
            if (bmi.biYPelsPerMeter > 0)
                DpiY = (int)((float)bmi.biYPelsPerMeter / 39.37008f + 0.5f);
            st = GdipBitmapSetResolution(img, DpiX, DpiY);
            if (st != 0)
                return false;

            st = GdipSaveImageToFile(img, picname, ref clsid, IntPtr.Zero);
            GdipDisposeImage(img);
            
            return st == 0;
            */
           return true;

		}




	/*	[DllImport("gdiplus.dll", ExactSpelling=true)]
	internal static extern int GdipCreateBitmapFromGdiDib( IntPtr bminfo, IntPtr pixdat, ref IntPtr image );
	*/
		[DllImport("gdiplus.dll", ExactSpelling=true, CharSet=CharSet.Unicode)]
	internal static extern int GdipSaveImageToFile( IntPtr image, string filename, [In] ref Guid clsid, IntPtr encparams );

		[DllImport("gdiplus.dll", ExactSpelling=true)]
	internal static extern int GdipDisposeImage( IntPtr image );
    [DllImport("gdiplus.dll", ExactSpelling = true)]
    internal static extern int GdipBitmapSetResolution(IntPtr image, float dpiX, float dpiY);

    [DllImport("GdiPlus.dll",
   	CharSet=CharSet.Unicode, ExactSpelling=true)] 
	private static extern int GdipCreateBitmapFromGdiDib(IntPtr pBIH, 
                          IntPtr pPix, out IntPtr pBitmap); 

	}
	
} // namespace GdiPlusLib
