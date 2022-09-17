/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 03/06/2009
 * Time: 12:22 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace NovaNet
{
	namespace Utils
	{
		/// <summary>
		/// Implementation of imagery of GDPicture
		/// </summary>
		public class gdp: Imagery
		{
            public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
            public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);	

			//The primary object that will hold the GDPicture object
			private GdPicture.GdPictureImaging img;
			//Image index
			private int ino;
			//Holds the license key
			private string lickey;
			//Constructor and initialization
			public gdp(string prmKey)
			{
				//Init gdi
				lickey = prmKey;
				img = new GdPicture.GdPictureImaging();				
				img.SetLicenseNumber(lickey);
                exMailLog.SetNextLogger(exTxtLog);
                
				ino = 0;
			}
			//Returns the currently selected bitmap, if any
			public Bitmap GetBitmap()
			{
				Bitmap bmp=null;
				try
				{
					if (ino > 0)
					{
						if (img.GetBitDepth(ino) > 1)
						{
							string tmpFile = Path.GetTempFileName();
							img.SaveAsTIFF(ino,tmpFile,GdPicture.TiffCompression.TiffCompressionLZW);
							FileStream sr = new FileStream(tmpFile,FileMode.Open);
							bmp = new Bitmap(sr);
							sr.Close();
							File.Delete(tmpFile);
						}
						else
						{
							bmp = img.GetBitmapFromGdPictureImage(ino);
						}
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return bmp;
			}
			//Loads Bitmap from the file specified in the 1st param
			public IGRStatus LoadBitmapFromFile(string flName)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					//Release image, if already loaded
					if (ino > 0)
						img.ReleaseGdPictureImage(ino);
					ino=img.CreateGdPictureImageFromFile(flName);
					retVal = IGRStatus.Success;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Loads Bitmap from HBITMAP
			public IGRStatus LoadBitmapFromDIB(System.IntPtr prmHBmp)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					//Release image, if already loaded
					if (ino > 0)
						img.ReleaseGdPictureImage(ino);
					ino=img.CreateGdPictureImageFromDIB(prmHBmp);
					retVal = IGRStatus.Success;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery - LoadBitmapFromDIB",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//To save the current image in TIFF with the given compression
			public IGRStatus SaveAsTiff(string flName, IGRComressionTIFF compr)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					//Only act if something is in store
					if (ino > 0)
					{
						//Check the compression and save
						switch(compr)
						{
							case IGRComressionTIFF.JPEG:
								img.SaveAsTIFF(ino,flName,GdPicture.TiffCompression.TiffCompressionJPEG);
								break;
							case IGRComressionTIFF.CCIT4:
								img.SaveAsTIFF(ino,flName,GdPicture.TiffCompression.TiffCompressionCCITT4);
								break;								
						}
					}
					retVal = IGRStatus.Success;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Crops the image with the specified area
			public IGRStatus Crop(Rectangle rect)
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (ino > 0)
					{
						img.Crop(ino, rect.X,rect.Y,rect.Width,rect.Height);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Crops the image with the specified area
			public IGRStatus Clean(Rectangle rect)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					if (ino > 0)
					{
						int bitDepth = img.GetBitDepth(ino);
						img.DrawFilledRectangle(ino,rect.X,rect.Y,rect.Width,rect.Height, System.Drawing.Color.White,false);
						if (bitDepth==1)
						{
							img.ConvertTo1Bpp(ino);
						}						
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Runs Auto Crop
			public IGRStatus AutoCrop()
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{				
					if (ino > 0)
					{
	        			//Auto Crop
						img.CropBorders(ino,90);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Runs Auto Deskew
			public IGRStatus AutoDeSkew()
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{				
					if (ino > 0)
					{
	        			//Auto Deskew
	        			img.AutoDeskew(ino);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Runs Auto Despeckle
			public IGRStatus Despeckle()
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{				
					if (ino > 0)
					{
	        			//Despeckle
	        			img.FxDespeckle(ino);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Rotate +90
			public IGRStatus RotateRight()
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{				
					if (ino > 0)
					{
	        			//Rotate +90
	        			img.Rotate(ino, RotateFlipType.Rotate90FlipNone);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Rotate -90
			public IGRStatus RotateLeft()
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{				
					if (ino > 0)
					{
	        			//Rotate -90
	        			img.Rotate(ino, RotateFlipType.Rotate270FlipNone);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Converts p1 to 1bpp in p2
			public IGRStatus ConvertTo1Bpp(string source, string dest)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					GdPicture.GdPictureImaging gdi = new GdPicture.GdPictureImaging();
					gdi.SetLicenseNumber(lickey);
					int imageNo=gdi.CreateGdPictureImageFromFile(source) ;
					gdi.ConvertTo1Bpp(imageNo);
					gdi.SaveAsTIFF(imageNo,dest,GdPicture.TiffCompression.TiffCompressionCCITT4);
					gdi.ReleaseGdPictureImage(imageNo);
					retVal = IGRStatus.Success;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Saves the image in the format already existing within the image object
			public IGRStatus SaveFile(string flName)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					//Only act if something is in store
					if (ino > 0)
					{
						//Check the compression and save
						switch(img.GetBitDepth(ino))
						{
							case 24:
								img.SaveAsTIFF(ino,flName,GdPicture.TiffCompression.TiffCompressionJPEG);
								break;
							default:
								img.SaveAsTIFF(ino,flName,GdPicture.TiffCompression.TiffCompressionCCITT4);
								break;								
						}
					}
					retVal = IGRStatus.Success;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Returns state of the image, whether operations are possible or not
			public bool IsValid()
			{
				bool retVal = false;
				if (ino > 0)
				{
					retVal = true;
				}
				return retVal;
			}
			//Converts the image to bitonal
			public IGRStatus ToBitonal()
			{
				return IGRStatus.Success;
			}
			//Detects a blank page
			public IGRStatus IsBlankPage()
			{
				return IGRStatus.Success;
			}
		}
	}
}
