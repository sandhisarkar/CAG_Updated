/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 03/06/2009
 * Time: 12:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
namespace NovaNet
{
	namespace Utils
	{
		//Enumeration to identify type of compression
		public enum IGRComressionTIFF : short
		{									// DG_.....
		JPEG			= 0x0001,
		CCIT4			= 0x0002,
		LZW				= 0x0004
		}
		//Enumeration to identify state of the operation
		public enum IGRStatus : short
		{									// DG_.....
		Success			= 0x0001,
		Failure			= 0x0002
		}		
		/// <summary>
		/// Interface for imaging toolkit.
		/// </summary>
		public interface Imagery
		{
			bool IsValid();	//Returns state of the image, whether operations are possible or not
			Bitmap GetBitmap();	//Returns the currently selected bitmap, if any
			IGRStatus LoadBitmapFromFile(string flName);	//Loads Bitmap from the file specified in the 1st param
			IGRStatus LoadBitmapFromDIB(System.IntPtr prmHBmp); //Loads Bitmap from HBITMAP
			IGRStatus SaveAsTiff(string flName, IGRComressionTIFF compr); //To save the current image in TIFF with the given compression
			IGRStatus SaveFile(string flName);	//Saves the image in the format already existing within the image object
			IGRStatus Crop(Rectangle rect);	//Crops the image with the specified area
			IGRStatus Clean(Rectangle rect);	//Cleans the image with the specified area
			IGRStatus AutoCrop();	//Runs Auto Crop
			IGRStatus AutoDeSkew();	//Runs Auto Deskew
            IGRStatus AutoDeSkew(bool pForced);	//Runs Auto Deskew
			IGRStatus Despeckle();	//Runs Auto Despeckle]
            IGRStatus Despeckle(int pAngle); //Auto despeckle with given angle
			IGRStatus RotateRight(); //Rotates image +90
			IGRStatus RotateLeft(); //Rotates image -90
			IGRStatus ConvertTo1Bpp(string source, string dest);	//Converts p1 to 1bpp in p2
			IGRStatus ToBitonal(); //Converts the source image format to bitonal
			IGRStatus IsBlankPage(); //Detects a blank page
            IGRStatus ScaleImage(double x, double y);//For scaling the image
            Bitmap Resize(double width, double height); //For changing the size of the image
            void GetLZW(string fName); // to save image in lzw format
            bool isSeparator(string pStrComp);  //To check whether this image is a separator or not
            IGRStatus BackGround();
            bool CombineTif(string[] InputFilenames, string OutputFilename);
            bool CombinePDF(string[] InputFilenames, string OutputFilename);//Combine single tif file to multi tif
            bool Close();   //Dispose the object
            bool TifToPdf(string[] InputFilenames, int pJpegQualityValue, string pPdfFileName);
            string GetError(); //Returns the error, if something happened - Rahul, 27/01/16
		}
	}
}
