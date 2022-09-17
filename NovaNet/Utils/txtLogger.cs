/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 23/06/2009
 * Time: 5:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace NovaNet
{
	namespace Utils
	{
		namespace exLog
		{
			/// <summary>
			/// Implementation of Logger abstract class in text files
			/// </summary>
			public class txtLogger: Logger
			{
				/// <summary>
				/// Holds filename of the output device
				/// </summary>
				private string flName;
				/// <summary>
				/// The filestream object to write with
				/// </summary>
				private FileStream fs;
				/// <summary>
				/// Whether the output is OK for writing
				/// </summary>
				private int flState;
				public txtLogger(string prmOutputName, LogLevel prmLgLevel): base(prmOutputName, prmLgLevel)
				{
                    flName = prmOutputName;
				}
				public override int Log(Exception ex)
				{
					int retval = 0;
                    flState = Logger._SUCCESS;
					if (flState == Logger._SUCCESS)
					{
                        try
                        {
                           
                            fs = new FileStream(flName, FileMode.Append);
                            outputType = "Text";
                            flState = Logger._SUCCESS;
                            //Date and time of occurrence
						    byte[] tmpWrite = new System.Text.ASCIIEncoding().GetBytes(System.DateTime.Now + "\n");
						    fs.Write(tmpWrite,0,tmpWrite.Length);
						    //Message as seen by the user
						    tmpWrite = new System.Text.ASCIIEncoding().GetBytes(ex.Message + "\n");
						    fs.Write(tmpWrite,0,tmpWrite.Length);
						    //Stack trace
						    tmpWrite = new System.Text.ASCIIEncoding().GetBytes(ex.StackTrace + "\n");
						    fs.Write(tmpWrite,0,tmpWrite.Length);
						    //Source
						    tmpWrite = new System.Text.ASCIIEncoding().GetBytes(ex.Source + "\n");
						    fs.Write(tmpWrite,0,tmpWrite.Length);
						    //End
						    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("--------End--------\n");
						    fs.Write(tmpWrite,0,tmpWrite.Length);						
						    fs.Close();

						    retval = tmpWrite.Length;
                        }
                        catch (Exception exL)
                        {
                            flState = Logger._ERROR;
                            System.Windows.Forms.MessageBox.Show("Error in logging exception - " + exL.Message, "Error - " + Ver, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
					}
					else
					{
						retval = 0;
					}
					return retval;
				}
                public override int Log(string pLogMsg)
                {
                    int retval = 0;
                    flState = Logger._SUCCESS;
                    if (flState == Logger._SUCCESS)
                    {
                        try
                        {

                            fs = new FileStream(flName, FileMode.Append);
                            outputType = "Text";
                            flState = Logger._SUCCESS;
                            //Date and time of occurrence
                            byte[] tmpWrite = new System.Text.ASCIIEncoding().GetBytes(System.DateTime.Now + "\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            //Message as seen by the user
                            tmpWrite = new System.Text.ASCIIEncoding().GetBytes(pLogMsg + "\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            //End
                            tmpWrite = new System.Text.ASCIIEncoding().GetBytes("--------End--------\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            fs.Close();

                            retval = tmpWrite.Length;
                        }
                        catch (Exception exL)
                        {
                            flState = Logger._ERROR;
                            System.Windows.Forms.MessageBox.Show("Error in logging exception - " + exL.Message, "Error - " + Ver, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        retval = 0;
                    }
                    return retval;
                }
                public override int Log(Exception ex,StateData er)
                {
                    int retval = 0;
                    flState = Logger._SUCCESS;
                    if (flState == Logger._SUCCESS)
                    {
                        try
                        {

                            fs = new FileStream(flName, FileMode.Append);
                            outputType = "Text";
                            flState = Logger._SUCCESS;
                            //Date and time of occurrence
                            byte[] tmpWrite = new System.Text.ASCIIEncoding().GetBytes(System.DateTime.Now + "\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            //Message as seen by the user
                            tmpWrite = new System.Text.ASCIIEncoding().GetBytes(ex.Message + "\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            //Stack trace
                            tmpWrite = new System.Text.ASCIIEncoding().GetBytes(ex.StackTrace + "\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            //Source
                            tmpWrite = new System.Text.ASCIIEncoding().GetBytes(ex.Source + "\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);

                            tmpWrite = er.StateLog().GetBuffer() ;
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            //End
                            tmpWrite = new System.Text.ASCIIEncoding().GetBytes("--------End--------\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            fs.Close();

                            retval = tmpWrite.Length;
                        }
                        catch (Exception exL)
                        {
                            flState = Logger._ERROR;
                            System.Windows.Forms.MessageBox.Show("Error in logging exception - " + exL.Message, "Error - " + Ver, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        retval = 0;
                    }
                    return retval;
                }
                //~txtLogger()
                //{
                //    if(File.
                //    fs.Close();
                //}
			}
		}
	}
}