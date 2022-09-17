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
using ClearImage;
using System.IO;
using System.Runtime.InteropServices;

namespace NovaNet
{
	namespace Utils
	{
		/// <summary>
		/// Implementation of imagery of ClearImage
		/// </summary>
		public class ci: Imagery
		{
			StreamWriter appndWr;
            [StructLayout(LayoutKind.Sequential)]
            public struct SYSTEMTIME
            {
                public short wYear;
                public short wMonth;
                public short wDayOfWeek;
                public short wDay;
                public short wHour;
                public short wMinute;
                public short wSecond;
                public short wMilliseconds;
            }
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool SetSystemTime([In] ref SYSTEMTIME st);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool GetSystemTime(ref SYSTEMTIME st);

            public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev,Constants._MAIL_TO,Constants._MAIL_FROM,Constants._SMTP);
            public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);	
            
			//Image Server
			ClearImage.CiServer cis;
			//Image Repair
			ClearImage.CiRepair cir;
			//Image
			ClearImage.CiImage cii;
            //Temporary image object
            ClearImage.CiImage ciiTemp;
            //Tools
            ClearImage.CiTools cit;
            //Barcode Pro
            ClearImage.CiBarcodePro cib;
            //JPEG quality constant
			private const int _JPEGQUALITY = 50;
            //The temporary image object
            ClearImage.CiImage ciTemp;
            //The Error, if happened - added on 27/01/16, Rahul
            string err = "";
            //Constructor and initialization

            
            public int selfcheck;
			public ci()
			{
                //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
                SYSTEMTIME st = new SYSTEMTIME();
                SYSTEMTIME stCur = new SYSTEMTIME();
                GetSystemTime(ref st);
                stCur = st;
                st.wYear = Constants._SET_YEAR; // must be short
                st.wMonth = Constants._SET_MONTH;
                st.wDay = Constants._SET_DATE;

                SetSystemTime(ref st); // invoke this method.
				//Clear Image init
				cis = new ClearImage.CiServerClass();
				cir = cis.CreateRepair();
				cii = cis.CreateImage();
                cit = cis.CreateTools();
                cib = cis.CreateBarcodePro();
                ciTemp = cis.CreateImage();
                exMailLog.SetNextLogger(exTxtLog);

                st.wYear = (short)stCur.wYear; // must be short
                st.wMonth = (short)stCur.wMonth;
                st.wDay = (short)stCur.wDay;
                //st.wHour = (short)st.wHour;
                //st.wMinute = (short)st.wMinute;
                //st.wSecond = (short)st.wSecond;
                
                SetSystemTime(ref st); // invoke this method.
			}
			//Returns the currently selected bitmap, if any
			public Bitmap GetBitmap()
			{
				Bitmap bmp = null;
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						string tmpFile = Path.GetTempFileName();
						if (cii.BitsPerPixel > 1)
						{
							
							cii.pComprColor = EComprColor.citcLZW;
							cii.SaveAs(tmpFile, EFileFormat.ciTIFF_LZW);
						}
						else
						{
							cii.SaveAs(tmpFile, EFileFormat.ciTIFF_G4);
						}
						FileStream sr = new FileStream(tmpFile,FileMode.Open);
						bmp = new Bitmap(sr);
						sr.Close();
						File.Delete(tmpFile);
					}
				}
				
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				return bmp;
			}
            
            public void GetLZW(string prmfName)
            {
                try
                {
                    if (cii.IsValid == EBoolean.ciTrue)
                    {
                        //string tmpFile = Path.GetTempFileName();
                        if (cii.BitsPerPixel > 1)
                        {

                            cii.pComprColor = EComprColor.citcLZW;
                            cii.SaveAs(prmfName, EFileFormat.ciTIFF_LZW);
                        }
                        else
                        {
                            cii.SaveAs(prmfName, EFileFormat.ciTIFF_G4);
                        }
                        //FileStream sr = new FileStream(prmfName, FileMode.Open);
                        //bmp = new Bitmap(sr);
                        //sr.Close();
                        //File.Delete(tmpFile);
                    }
                }

                catch (Exception ex)
                {
                    err = ex.Message;
                    MessageBox.Show(ex.Message, "Error - Imagery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //public bool TifToPdf(string[] InputFilenames, int pJpegQaulityValue, string pPdfFileName)
            //{
            //    //Return value
            //    bool retval = false;
            //    try
            //    {
            //        //Clear Image init
            //        if (InputFilenames.Length > 0)
            //        {
            //            cii.Open(InputFilenames[0], 1);
            //            if (cii.BitsPerPixel > 1)
            //            {
            //                cii.pComprColor = EComprColor.citcJPEG;
            //                cii.JpegQuality = pJpegQaulityValue;
            //                //cii.Append(OutputFilename, EFileFormat.ciTIFF_JPEG);
            //                cii.Append(pPdfFileName, EFileFormat.cifPDF);
            //            }
            //            else
            //            {
            //                //cii.JpegQuality = pJpegQaulityValue;
            //                cii.Append(pPdfFileName, EFileFormat.cifPDF);
            //            }
            //            for (int i = 1; i < InputFilenames.Length; i++)
            //            {
            //                cii.Open(InputFilenames[i], 1);
            //                if (cii.BitsPerPixel > 1)
            //                {
            //                    cii.pComprColor = EComprColor.citcJPEG;
            //                    cii.JpegQuality = pJpegQaulityValue;
            //                    //cii.Append(OutputFilename, EFileFormat.ciTIFF_JPEG);
            //                    cii.Append(pPdfFileName, EFileFormat.cifPDF);
            //                }
            //                else
            //                {
            //                    cii.Append(pPdfFileName, EFileFormat.cifPDF);
            //                }
            //            }
            //            retval = true;
            //            cii.Close();
            //        }
            //        else
            //        {
            //            retval = true;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        retval = false;
            //        string err = ex.Message;
            //    }
            //    return retval;
            //}
            public bool TifToPdf(string[] InputFilenames, int pJpegQaulityValue, string pPdfFileName)
            {
                //Return value
                bool retval = false;
                int idxPass = 0;
                try
                {
                    //Clear Image init
                    if (InputFilenames.Length > 0)
                    {
                        cii.Open(InputFilenames[0], 1);
                        if (cii.BitsPerPixel > 1)
                        {
                            cii.pComprColor = EComprColor.citcJPEG;
                            cii.JpegQuality = pJpegQaulityValue;
                            //cii.Append(OutputFilename, EFileFormat.ciTIFF_JPEG);
                            cii.Append(pPdfFileName, EFileFormat.cifPDF);
                        }
                        else
                        {
                            //cii.JpegQuality = pJpegQaulityValue;
                            cii.Append(pPdfFileName, EFileFormat.cifPDF);
                        }
                        for (int i = 1; i < InputFilenames.Length; i++)
                        {
                            idxPass = i;
                            cii.Open(InputFilenames[i], 1);
                            if (cii.BitsPerPixel > 1)
                            {
                                cii.pComprColor = EComprColor.citcJPEG;
                                cii.JpegQuality = pJpegQaulityValue;
                                //cii.Append(OutputFilename, EFileFormat.ciTIFF_JPEG);
                                cii.Append(pPdfFileName, EFileFormat.cifPDF);
                            }
                            else
                            {
                                cii.Append(pPdfFileName, EFileFormat.cifPDF);
                            }
                        }
                        retval = true;
                        cii.Close();
                    }
                    else
                    {
                        retval = true;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    retval = false;
                    err = ex.Message + InputFilenames[idxPass];
                }
                return retval;
            }


			//Loads Bitmap from the file specified in the 1st param
			public IGRStatus LoadBitmapFromFile(string flName)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
						cii.Close();
					cii.Open(flName,1);
					retVal = IGRStatus.Success;
				}
				catch(Exception ex)
				{
                    err = ex.Message;
                    MessageBox.Show("Error while loading the image......");  
				}
				return retVal;
			}
			//Loads Bitmap from HBITMAP
			public IGRStatus LoadBitmapFromDIB(System.IntPtr prmHBmp)
			{
				IGRStatus retVal = IGRStatus.Failure;
                
				try
                {
                    selfcheck = 1;
					if (cii.IsValid == EBoolean.ciTrue)
					cii.Close();
					cii.OpenFromBitmap((int)prmHBmp);
					retVal = IGRStatus.Success;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
                selfcheck=0;
				return retVal;
			}
			//To save the current image in TIFF with the given compression
			public IGRStatus SaveAsTiff(string flName, IGRComressionTIFF compr)
			{
				IGRStatus retVal = IGRStatus.Failure;
				//try
				{
					//Only act if something is in store
					if (cii.IsValid == EBoolean.ciTrue)
					{
						//Check the compression and save
						switch(compr)
						{
							case IGRComressionTIFF.JPEG:
								//oRepair.Image.SaveAs(flName, EFileFormat.ciTIFF_G4);
								cii.JpegQuality = _JPEGQUALITY;
								cii.SaveAs(flName, EFileFormat.ciTIFF_LZW);
								break;
							case IGRComressionTIFF.CCIT4:
								//oRepair.Image.SaveAs(flName, EFileFormat.ciTIFF_G4);
								cii.SaveAs(flName, EFileFormat.ciTIFF_G4);
								break;								
						}
					}
					DoGC();
					retVal = IGRStatus.Success;
				}
				/*
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				*/
				return retVal;
			}
			//Crops the image with the specified area
			public IGRStatus Crop(Rectangle rect)
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						cii.Crop(rect.X,rect.Y,(rect.X + rect.Width), (rect.Y + rect.Height));
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Cleans the image with the specified area
			public IGRStatus Clean(Rectangle rect)
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
                        ClearImage.CiRect ciRect;
                        //To bypass date during ClearImage object creation :-)
                        SYSTEMTIME st = new SYSTEMTIME();
                        SYSTEMTIME stCur = new SYSTEMTIME();
                        GetSystemTime(ref st);
                        stCur = st;
                        st.wYear = Constants._SET_YEAR; // must be short
                        st.wMonth = Constants._SET_MONTH;
                        st.wDay = Constants._SET_DATE;
                        //st.wHour = (short)st.wHour;
                        //st.wMinute = (short)st.wMinute;
                        //st.wSecond = (short)st.wSecond;

                        SetSystemTime(ref st); // invoke this method.
                        //Clear Image init
                        ciRect = cis.CreateRect(rect.X, rect.Y, (rect.X + rect.Width), (rect.Y + rect.Height));
                        st.wYear = (short)stCur.wYear; // must be short
                        st.wMonth = (short)stCur.wMonth;
                        st.wDay = (short)stCur.wDay;

                        SetSystemTime(ref st); // invoke this method.

                        ciiTemp = cii.CreateZoneRect(ciRect);
						ciiTemp.Clear();
						ciiTemp.Close();
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
            //Cleans the image with the specified area
            public IGRStatus ScaleImage(double x,double y)
            {
                IGRStatus retVal = IGRStatus.Failure;
                try
                {
                    if (cii.IsValid == EBoolean.ciTrue)
                    {
                        cii.ScaleImage(x, y);
                        retVal = IGRStatus.Success;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    MessageBox.Show(ex.Message, "Error - Imagery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exMailLog.Log(ex);
                }
                return retVal;
            }
			//Auto Crops the image
			public IGRStatus AutoCrop()
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						cir.Image = cii;
						cir.AutoCrop(5,5,5,5);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}

            public IGRStatus BackGround()
            {
                IGRStatus retVal = IGRStatus.Failure;
                try
                {
                    if (cii.IsValid == EBoolean.ciTrue)
                    {
                        cir.Image = cii;
                        cir.SmoothCharacters(ESmoothType.ciSmoothLightenEdges);
                        retVal = IGRStatus.Success;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    MessageBox.Show(ex.Message, "Error - Imagery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exMailLog.Log(ex);
                }
                return retVal;
            }
			//Auto Deskews the image
			public IGRStatus AutoDeSkew()
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
                        cit.Image = cii;
						cir.Image = cii;
                        double i = cit.MeasureSkew();
                        //MessageBox.Show(i.ToString());
                        if ((i >= 1.2d) || (i <= -1.2d))
                        {
                            cir.AutoDeskew();
                            retVal = IGRStatus.Success;
                        }
                        else
                        {
                            retVal = IGRStatus.Failure;
                        }
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
            /*
            public IGRStatus Paste(Bitmap pBmp)
            {
                IGRStatus retVal = IGRStatus.Failure;
                try
                {
                    if (cii.IsValid == EBoolean.ciTrue)
                    {
                        ciTemp;
                        cit.Image = cii;
                        cit.PasteImage(ciTemp, 0, 0);
                        cii = ciTemp;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error - Imagery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exMailLog.Log(ex);
                }
                return retVal;
            }
             */
            //Auto Deskews the image
            public IGRStatus AutoDeSkew(bool pForced)
            {
                IGRStatus retVal = IGRStatus.Failure;
                try
                {
                    if (cii.IsValid == EBoolean.ciTrue)
                    {
                        cir.Image = cii;
                        cir.AutoDeskew();
                        retVal = IGRStatus.Success;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    MessageBox.Show(ex.Message, "Error - Imagery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exMailLog.Log(ex);
                }
                return retVal;
            }
			//Auto Despeckles the image
			public IGRStatus Despeckle()
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						cir.Image = cii;
						cir.CleanNoise(5);
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
            //Despeckles with user given value
            public IGRStatus Despeckle(int pAngle)
            {
                IGRStatus retVal = IGRStatus.Failure;
                try
                {
                    if (cii.IsValid == EBoolean.ciTrue)
                    {
                        cir.Image = cii;
                        cir.CleanNoise(pAngle);
                        retVal = IGRStatus.Success;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    MessageBox.Show(ex.Message, "Error - Imagery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exMailLog.Log(ex);
                }
                return retVal;
            }
			//Rotates image +90
			public IGRStatus RotateRight()
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						cii.RotateRight();
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Rotates image -90
			public IGRStatus RotateLeft()
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						cii.RotateLeft();
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
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
					if (cii.IsValid == EBoolean.ciTrue)
					{
						//Check the compression and save
						switch(cii.BitsPerPixel)
						{
							case 8:
								cii.pComprColor = EComprColor.citcLZW;
								cii.JpegQuality = _JPEGQUALITY;
								cii.SaveAs(flName, EFileFormat.ciTIFF_LZW);
								break;								
							case 24:
								cii.pComprColor = EComprColor.citcJPEG;
								cii.JpegQuality = _JPEGQUALITY;
								cii.SaveAs(flName, EFileFormat.ciTIFF_JPEG);
								break;
							default:
								cii.SaveAs(flName, EFileFormat.ciTIFF_G4);
								break;								
						}
					}
					retVal = IGRStatus.Success;				
				}
				
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
					retVal = IGRStatus.Failure;
				}
				
				return retVal;
			}			
			//Converts p1 to 1bpp in p2
			public IGRStatus ConvertTo1Bpp(string source, string dest)
			{
				IGRStatus retVal = IGRStatus.Failure;
				try
				{
					ciTemp.Open(source, 1);
					//Only act if something is in store
					if (ciTemp.IsValid == EBoolean.ciTrue)
					{
						ciTemp.ToBitonal();
						ciTemp.pComprBitonal = EComprBitonal.citbCCITTFAX4;
						ciTemp.SaveAs(dest, EFileFormat.ciTIFF_G4);
						ciTemp.Close();
					}
					retVal = IGRStatus.Success;				
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;
			}
			//Returns state of the image, whether operations are possible or not
			public bool IsValid()
			{
				bool retVal = false;
				if (cii.IsValid > 0)
				{
					retVal = true;
				}
				return retVal;
			}
			//Internal function to garbage collect
			private void DoGC ()
			{
				System.GC.Collect();
				System.GC.WaitForPendingFinalizers();
			}
			public IGRStatus ToBitonal()
			{
				IGRStatus retVal = IGRStatus.Failure;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						cii.ToBitonal();
						retVal = IGRStatus.Success;
					}
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;				
			}
			public IGRStatus IsBlankPage()
			{
				IGRStatus retVal = IGRStatus.Success;				
				try
				{
					if (cii.IsValid == EBoolean.ciTrue)
					{
						cir.Image = cii;
						if (cir.IsBlankImage(0,0,0) == EBoolean.ciTrue)
							retVal = IGRStatus.Failure;
					}
                    //cii.Close();
				}
				catch(Exception ex)
				{
                    err = ex.Message;
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    exMailLog.Log(ex);
				}
				return retVal;				
			}
            //For changing the size of the image
            public Bitmap Resize(double width, double height)
            {
                Bitmap bmp = null;
                try
                {
                    if (cii.IsValid == EBoolean.ciTrue)
                    {
//                        ClearImage.CiRect ciRect;
//                        ClearImage.CiImage ciiTemp;
                        cit.Image = cii.Duplicate();
                        cit.pScaleType = EScaleType.ciScaleThreshold;
                        cit.pScaleThreshold = 30;
                        cit.ScaleImage(width/cit.Image.Width, height/cit.Image.Height);
                        if (cit.Image.IsValid == EBoolean.ciTrue)
                        {
                            string tmpFile = Path.GetTempFileName();
                            if (cit.Image.BitsPerPixel > 1)
                            {

                                cit.Image.pComprColor = EComprColor.citcLZW;
                                cit.Image.SaveAs(tmpFile, EFileFormat.ciTIFF_LZW);
                            }
                            else
                            {
                                cit.Image.SaveAs(tmpFile, EFileFormat.ciTIFF_G4);
                            }
                            FileStream sr = new FileStream(tmpFile, FileMode.Open);
                            bmp = new Bitmap(sr);
                            sr.Close();
                            File.Delete(tmpFile);
                        }
                        //ciRect = cis.CreateRect(0,0,cii.Width,cii.Height);
                        //ciiTemp = cis.CreateImage();
                        //ciiTemp = cii.CreateZoneRect(ciRect);

                        //ciiTemp.Clear();
                        //ciiTemp.Close();
                        //retVal = IGRStatus.Success;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    MessageBox.Show(ex.Message, "Error - Imagery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exMailLog.Log(ex);
                }
                return bmp;
            }
            public bool isSeparator(string pStrComp)
            {
                DateTime startTime = DateTime.Now;

                bool retVal = false;
                
                if (cii.IsValid == EBoolean.ciTrue)
                {
                    //BarCode
                    cib.Image = cii;
                    cib.Type = FBarcodeType.cibfCode128;
                    CiBarcode ciReader = cib.FirstBarcode();
                    if (ciReader != null)
                    {
                        if (ciReader.Text.ToUpper() == pStrComp.ToUpper())
                            retVal = true;
                        else
                            retVal = false;
                    }
                    else
                    {
                        retVal = false;
                    }
                    //cii.Close();
                }
                //TimeSpan duration = DateTime.Now - startTime;
                //MessageBox.Show(duration.ToString());
                return retVal;
            }
            public bool CombineTif(string[] InputFilenames, string OutputFilename)
            {
                //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
                //Return value
                bool retval = false;
                #if DEBUG
                DateTime openStTime;
                DateTime appendStTime;
                DateTime openEndTime;
                DateTime appendEndTime;
                TimeSpan otsp = new TimeSpan();
                TimeSpan atsp = new TimeSpan();
                #endif
                try
                {
                	#if DEBUG
                	appndWr = new StreamWriter("C:\\AppendLog.txt",true);
                	
                	#endif
                    if (InputFilenames.Length > 0)
                    {
                        //Clear Image init
                        #if DEBUG
                        openStTime = DateTime.Now;
                        #endif
                        cii.Open(InputFilenames[0], 1);
                        #if DEBUG
                        otsp = DateTime.Now - openStTime ;
                        
                        
                        appendStTime = DateTime.Now;
                        #endif
                        if (cii.BitsPerPixel > 1)
                        {
                            cii.pComprColor = EComprColor.citcLZW;
                            cii.JpegQuality = 75;
                            cii.Append(OutputFilename, EFileFormat.ciTIFF_LZW);
                        }
                        else
                        {
                            cii.Append(OutputFilename, EFileFormat.ciTIFF_G4);
                        }
                        #if DEBUG
                        atsp = DateTime.Now - appendStTime;
                        #endif
                        //cii.Clear();
                        cii.Close();
                        
                        for (int i = 1; i < InputFilenames.Length; i++)
                        {
                        	#if DEBUG
                        	openStTime = DateTime.Now;
                        	#endif
                            cii.Open(InputFilenames[i], 1);
                            #if DEBUG
                            otsp += DateTime.Now - openStTime;
                            
                            
                            appendStTime = DateTime.Now;
                            #endif
                            if (cii.BitsPerPixel > 1)
                            {
                                cii.pComprColor = EComprColor.citcLZW;
                                cii.JpegQuality = 75;
                                cii.Append(OutputFilename, EFileFormat.ciTIFF_LZW);
                            }
                            else
                            {
                                cii.Append(OutputFilename, EFileFormat.ciTIFF_G4);
                            }
                            #if DEBUG
                            atsp +=  DateTime.Now - appendStTime;
                            #endif
                            //cii.Clear();
                            cii.Close();
                        }
                        #if DEBUG
                        appndWr.WriteLine("Pages: " + InputFilenames.Length + " Open time: " + otsp.Milliseconds + " Append time: " + atsp.Milliseconds);
                        appndWr.Close();
                        #endif
                        retval = true;
                        
                        //cii.Close();
                    }
                    else
                    {
                        retval = true;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    retval = false;
                    
                }
                finally
                {
                	#if DEBUG
                	appndWr.Close();
                	#endif
                }
                return retval;
            }
            public bool Close()
            {
                cii.Close();
                return true;
            }

            public bool CombinePDF(string[] InputFilenames, string OutputFilename)
            {
                //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
                //Return value
                bool retval = false;
#if DEBUG
                DateTime openStTime;
                DateTime appendStTime;
                DateTime openEndTime;
                DateTime appendEndTime;
                TimeSpan otsp = new TimeSpan();
                TimeSpan atsp = new TimeSpan();
#endif
                try
                {
#if DEBUG
                    appndWr = new StreamWriter("C:\\AppendLog.txt", true);

#endif
                    if (InputFilenames.Length > 0)
                    {
                        //Clear Image init
#if DEBUG
                        openStTime = DateTime.Now;
#endif
                        cii.Open(InputFilenames[0], 1);
#if DEBUG
                        otsp = DateTime.Now - openStTime;


                        appendStTime = DateTime.Now;
#endif
                        if (cii.BitsPerPixel > 1)
                        {
                            cii.pComprColor = EComprColor.citcJPEG;
                            cii.JpegQuality = 75;
                            cii.Append(OutputFilename, EFileFormat.cifPDF);
                        }
                        else
                        {
                            cii.Append(OutputFilename, EFileFormat.cifPDF);
                        }
#if DEBUG
                        atsp = DateTime.Now - appendStTime;
#endif
                        //cii.Clear();
                        cii.Close();

                        for (int i = 1; i < InputFilenames.Length; i++)
                        {
#if DEBUG
                            openStTime = DateTime.Now;
#endif
                            cii.Open(InputFilenames[i], 1);
#if DEBUG
                            otsp += DateTime.Now - openStTime;


                            appendStTime = DateTime.Now;
#endif
                            if (cii.BitsPerPixel > 1)
                            {
                                cii.pComprColor = EComprColor.citcJPEG;
                                cii.JpegQuality = 75;
                                cii.Append(OutputFilename, EFileFormat.cifPDF);
                            }
                            else
                            {
                                cii.Append(OutputFilename, EFileFormat.cifPDF);
                            }
#if DEBUG
                            atsp += DateTime.Now - appendStTime;
#endif
                            //cii.Clear();
                            cii.Close();
                        }
#if DEBUG
                        appndWr.WriteLine("Pages: " + InputFilenames.Length + " Open time: " + otsp.Milliseconds + " Append time: " + atsp.Milliseconds);
                        appndWr.Close();
#endif
                        retval = true;

                        //cii.Close();
                    }
                    else
                    {
                        retval = true;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    retval = false;
                    
                }
                finally
                {
#if DEBUG
                    appndWr.Close();
#endif
                }
                return retval;
            }

            public string GetError()
            {
                return err;
            }
		}
	}
}
