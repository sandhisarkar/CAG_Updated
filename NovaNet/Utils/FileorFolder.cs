/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 20/2/2009
 * Time: 9:36 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ClearImage;

namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// Description of FileorFolder.
	/// </summary>
	public class FileorFolder
	{
		
		
		public FileorFolder()
		{
		}
		public static bool CreateFolder(string prmPath)
		{
			string dirPath=@prmPath;
			bool crtFolderBol=true;
			string err=null;
			
			try 
			{
				if (Directory.Exists(dirPath)==false)
				{
					Directory.CreateDirectory(dirPath);	
				}
				crtFolderBol=true;
			} 
			catch (IOException ioe)
			{
				crtFolderBol=false;
				err=ioe.Message;
			}
			return crtFolderBol;
		}

        public static bool RenameFolder(string pOldFolder,string pNewPath)
        {
            bool crtFolderBol = true;
            string err = string.Empty;

            try
            {
                if (Directory.Exists(pOldFolder) == true)
                {
                    Directory.Move(pOldFolder,pNewPath);
                }
                crtFolderBol = true;
            }
            catch (IOException ioe)
            {
                crtFolderBol = false;
                err = ioe.Message;
            }
            return crtFolderBol;
        }
		public static bool MoveFiles(string prmSourceFolder,string prmDestFolder)
		{
			string fileName=null;
			string destFile=null;
			INIReader rd=null;
		    KeyValueStruct udtKeyValue;
		    
			if (System.IO.Directory.Exists(prmSourceFolder))
	        {
	            string[] files = System.IO.Directory.GetFiles(prmSourceFolder);
	
	            // Copy the files and overwrite destination files if they already exist.
	            foreach (string s in files)
	            {
	                // Use static Path methods to extract only the file name from the path.
	                fileName = System.IO.Path.GetFileName(s);
	                destFile = System.IO.Path.Combine(prmDestFolder, fileName);
	                System.IO.File.Copy(s, destFile, true);
	            }
	        }
			else
			{
				rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
				udtKeyValue.Key=Constants.FOLDER_NOT_FOUND_ERROR.ToString();
				udtKeyValue.Section=Constants.COMMON_EXCEPTION_SECTION.ToString();
				string ErrMsg=rd.Read(udtKeyValue);
				throw new FolderNotFoundException();
			}
			return true;	
		}
		public void MoveFile(string sourceFolder,string destFolder,string fileName,bool prmOverWright)
		{
			int pos = fileName.IndexOf("-");
			string originalImage;
			string sourceFilePath;
			string destFilePath;
            

			if(pos > 0)
			{
				originalImage = fileName.Substring(0,pos) + "*" + ".TIF";
                string[] searchFileName = Directory.GetFiles(sourceFolder, originalImage);
                originalImage = searchFileName[0];
				sourceFilePath=originalImage;
				destFilePath=destFolder + "\\" + fileName;
			}
			else
			{
				sourceFilePath=sourceFolder + "\\" + fileName;
				destFilePath=destFolder + "\\" + fileName;
			}
			if((File.Exists(destFilePath)==false) && (prmOverWright==false))
			{
				File.Copy(sourceFilePath,destFilePath , prmOverWright);
			}
			else if(prmOverWright==true)
				File.Copy(sourceFilePath,destFilePath , prmOverWright);
		}
		public static void CombineTif_Back(string[] InputFilenames, string OutputFilename)
		{
			//get ImageCodecInfo, generate tif format
			
			ImageCodecInfo info = null;
			foreach (ImageCodecInfo ice in ImageCodecInfo.GetImageEncoders())
			{
				if (ice.MimeType == "image/tiff")
				{
				info = ice;
				break;
				}
			}
		
			/*
			 * define the encoderparameter, 
			 * when the 1st page, will be EncoderValue.MultiFrame.
			 * when the other pages, will be EncoderValue.FrameDimensionPage.
			 * when all pages saved, will be the EncoderValue.Flush.
			 */
			
			EncoderParameters ep = new EncoderParameters(2);
		
			/*
			 * when the 1st file, 1st frame, will be true.
			 * from the 1st file, 2nd frame, will be false.
			 */
			bool b11 = true;
		
			Image img =null;
		
			//create a image instance from the 1st image
			for (int nLoopfile = 0; nLoopfile < InputFilenames.Length; nLoopfile ++)
			{

				
				//get image from src file
				Image img_src = Image.FromFile(InputFilenames[nLoopfile]);
		
				Guid guid = img_src.FrameDimensionsList[0];
				System.Drawing.Imaging.FrameDimension dimension = new
					System.Drawing.Imaging.FrameDimension(guid);
		
				//get the frames from src file
				for (int nLoopFrame =0; nLoopFrame < img_src.GetFrameCount(dimension); nLoopFrame ++)
				{
					img_src.SelectActiveFrame(dimension, nLoopFrame);
					
					/*
					 * if black and write image, we use CCITT4 compression
					 * else we use the LZW compression
					 */
					if (img_src.PixelFormat == PixelFormat.Format1bppIndexed)
					{
						ep.Param[0] = new EncoderParameter(Encoder.Compression, Convert.ToInt32(EncoderValue.CompressionCCITT4));
					}
					else
					{
						ep.Param[0] = new EncoderParameter(Encoder.Compression, Convert.ToInt32(EncoderValue.CompressionLZW));
					}
		
					if (b11)
					{
						//1st file, 1st frame, create the master image
						img = img_src;
		
						ep.Param[1] = new EncoderParameter(Encoder.SaveFlag, Convert.ToInt32(EncoderValue.MultiFrame));
						img.Save(OutputFilename, info, ep);
		
						b11 = false;
						continue;
					}
		
					ep.Param[1] = new EncoderParameter(Encoder.SaveFlag, Convert.ToInt32(EncoderValue.FrameDimensionPage));
					img.SaveAdd(img_src, ep);
				}
			}
			ep.Param[1] = new EncoderParameter(Encoder.SaveFlag, Convert.ToInt32(EncoderValue.Flush));
			img.SaveAdd(ep);
		}
        /*
		public static bool GDCombineTif(string[] InputFilenames, string OutputFilename)
		{
			int ino=0;
			int nno=0;
			bool retval = false;
			try
			{
				GdPicture.GdPictureImaging imgQc = new GdPicture.GdPictureImaging();
                imgQc.SetLicenseNumber(Utils.Constants.LIC);
				if (InputFilenames.Length > 0)
				{
					ino = imgQc.CreateGdPictureImageFromFile(InputFilenames[0]);
					
					if (imgQc.GetBitDepth(ino) == 24)
					{
						
                        imgQc.SaveAsTIFF(ino,InputFilenames[0],GdPicture.TiffCompression.TiffCompressionJPEG);
                        ino = imgQc.CreateGdPictureImageFromFile(InputFilenames[0]);
						imgQc.TiffSaveAsMultiPageFile(ino,OutputFilename, TiffCompression.TiffCompressionLZW);

                        //ino = imgQc.TiffCreateMultiPageFromFile(InputFilenames[0]);
                        //imgQc.TiffSaveAsMultiPageFile(ino, OutputFilename, TiffCompression.TiffCompressionJPEG);

					}
					else
					{
						imgQc.TiffSaveAsMultiPageFile(ino,OutputFilename, TiffCompression.TiffCompressionCCITT4);
					}				
				}
				else
				{
					return false;
				}
				//create a image instance from the 1st image
				for (int nLoopfile = 1; nLoopfile < InputFilenames.Length; nLoopfile ++)
				{
                        nno = imgQc.CreateGdPictureImageFromFile(InputFilenames[nLoopfile]);
                        imgQc.TiffAddToMultiPageFile(ino, nno);
				}
                //if (imgQc.TiffIsEditableMultiPage(ino))
                    //simgQc.TiffSaveAsMultiPageFile(ino, OutputFilename, TiffCompression.TiffCompressionCCITT4);
                //imgQc.TiffCloseMultiPageFile(ino);
                nno = imgQc.CreateGdPictureImageFromFile(InputFilenames[0]);
                imgQc.TiffAddToMultiPageFile(ino, nno);

                imgQc.TiffCloseMultiPageFile(ino);
				retval = true;
			}
			catch(Exception ex)
			{
				//MessageBox.show
                string err = ex.Message;
			}
			return retval;
		}
         */
		public static bool CombineTif(string[] InputFilenames, string OutputFilename)
		{
			//Return value
			bool retval = false;
			try
			{
				//Clear Image init
				ClearImage.CiServer cis;
				cis = new ClearImage.CiServerClass();
				ClearImage.CiRepair cir = cis.CreateRepair();
				ClearImage.CiImage cii = cis.CreateImage();
				cii.Open(InputFilenames[0],1);
				if (cii.BitsPerPixel > 1)
				{
					cii.pComprColor = EComprColor.citcJPEG;
					cii.JpegQuality=75;
					cii.Append(OutputFilename, EFileFormat.ciTIFF_JPEG);				
				}			
				else
				{
					cii.Append(OutputFilename, EFileFormat.ciTIFF_G4);
				}
				for (int i = 1; i < InputFilenames.Length; i++)
				{
					cii.Open(InputFilenames[i],1);
					if (cii.BitsPerPixel > 1)
					{
						cii.pComprColor = EComprColor.citcJPEG;
						cii.JpegQuality=75;
						cii.Append(OutputFilename, EFileFormat.ciTIFF_JPEG);				
					}
					else
					{
						cii.Append(OutputFilename, EFileFormat.ciTIFF_G4);
					}
				}
				retval = true;
				cii.Close();
			}
			catch(Exception ex)
			{
				retval = false;
                string err = ex.Message;
			}
			return retval;
		}
	}
}
}