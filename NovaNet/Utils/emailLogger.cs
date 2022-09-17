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
using System.Net;
using System.Net.Mail;

namespace NovaNet
{
	namespace Utils
	{
		namespace exLog
		{
			/// <summary>
			/// Implementation of Logger abstract class in text files
			/// </summary>
			public class emailLogger: Logger
			{
				/// <summary>
				/// Holds server name
				/// </summary>
				//private string serverName;
				/// <summary>
				/// The filestream object to write with
				/// </summary>
				private Stream fs;
				/// <summary>
				/// Whether the output is OK for writing
				/// </summary>
				private int flState;
                
                /// <summary>
                /// Getter setter properties for email
                /// </summary>
                private string sendMailTo = string.Empty;
                private string receiveMailFrom = string.Empty;
                private string SMTPServer = string.Empty;
				public emailLogger(string prmOutputName, LogLevel prmLgLevel,string prmSendTo,string prmReceiveFrom,string prmSmtpServer): base(prmOutputName, prmLgLevel)
				{
                    sendMailTo = prmSendTo;
                    receiveMailFrom = prmReceiveFrom;
                    SMTPServer = prmSmtpServer;
				}
				public override int Log(Exception ex)
				{
					int retval = 0;
                    flState = Logger._SUCCESS;
					if (flState == Logger._SUCCESS)
					{
						try
						{
                            
                            //serverName = prmOutputName;
                            fs = new MemoryStream();
                            //fs = new FileStream(prmOutputName,FileMode.Append);
                            outputType = "eMail";
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
							fs.Flush();
							/// ///
							MailMessage mmsg = new MailMessage(receiveMailFrom,sendMailTo,"Error report","An error occurred");
							SmtpClient smtp = new SmtpClient(SMTPServer);
							smtp.Send(mmsg);
							retval = tmpWrite.Length;
							/// ////
						}
						catch(Exception ex1)
						{
                            flState = Logger._ERROR;
                            //System.Windows.Forms.MessageBox.Show("Error in logging exception - " + ex.Message, "Error - " + Ver, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
							if (nextLogger != null)
							{
								retval = nextLogger.Log(ex1);
							}
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

                            //serverName = prmOutputName;
                            fs = new MemoryStream();
                            //fs = new FileStream(prmOutputName,FileMode.Append);
                            outputType = "eMail";
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
                            fs.Flush();
                            /// ///
                            MailMessage mmsg = new MailMessage(receiveMailFrom, sendMailTo, "Error report", "An error occurred");
                            SmtpClient smtp = new SmtpClient(SMTPServer);
                            smtp.Send(mmsg);

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
                /// <summary>
                /// Overload of Log
                /// </summary>
                /// <param name="ex"></param>
                /// <returns></returns>
                public override int Log(Exception ex,StateData er)
                {
                    int retval = 0;
                    flState = Logger._SUCCESS;
                    if (flState == Logger._SUCCESS)
                    {
                        try
                        {

                            //serverName = prmOutputName;
                            fs = new MemoryStream();
                            //fs = new FileStream(prmOutputName,FileMode.Append);
                            outputType = "eMail";
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
                            
                            tmpWrite = er.StateLog().GetBuffer();
                            fs.Write(tmpWrite, 0, tmpWrite.Length);

                            tmpWrite = new System.Text.ASCIIEncoding().GetBytes("--------End--------\n");
                            fs.Write(tmpWrite, 0, tmpWrite.Length);
                            
                            fs.Flush();
                            /// ///
                            MailMessage mmsg = new MailMessage(receiveMailFrom, sendMailTo, "Error report", "An error occurred");
                            SmtpClient smtp = new SmtpClient(SMTPServer);
                            smtp.Send(mmsg);
                            retval = tmpWrite.Length;
                            /// ////
                        }
                        catch (Exception ex1)
                        {
                            flState = Logger._ERROR;
                            //System.Windows.Forms.MessageBox.Show("Error in logging exception - " + ex.Message, "Error - " + Ver, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (nextLogger != null)
                            {
                                retval = nextLogger.Log(ex1,er);
                            }
                        }
                    }
                    else
                    {
                        retval = 0;
                    }
                    return retval;
                }
                //~emailLogger()
                //{
                //    fs.Close();
                //}
			}
		}
	}
}