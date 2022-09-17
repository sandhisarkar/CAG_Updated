/*
 * Created by SharpDevelop.
 * User: Subhajit Bhadury
 * Date: 17/2/2009
 * Time: 1:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using System.Collections.Generic;
namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// Description of INIFile.
	/// </summary>
	public class INIFile
	{
		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
			SetLastError=true,
			CharSet=CharSet.Unicode, ExactSpelling=true,
			CallingConvention=CallingConvention.StdCall)]
		private static extern int GetPrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpDefault,
			string lpReturnString,
			int nSize,
			string lpFilename);

		[DllImport("KERNEL32.DLL", EntryPoint="WritePrivateProfileStringW",
			SetLastError=true,
			CharSet=CharSet.Unicode, ExactSpelling=true,
			CallingConvention=CallingConvention.StdCall)]
		private static extern int WritePrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpString,
			string lpFilename);
			string err=null;
		
		public INIFile()
		{
		}
		/// <summary>
		/// This method is used for read INI file against section and key value
		/// </summary>
		/// <param name="section">section name</param>
		/// <param name="key">key value</param>
		/// <param name="sDefault">default value should be empty</param>
		/// <param name="filename">file name with full path of the INI file</param>
		/// <returns></returns>
		public string ReadINI(string section,string key,string sDefault,string filename)
		{
			int chars = 1024;
        	string returnString = new string(' ', 1024);
        	//string sDefault = string.Empty;
        	string conString=string.Empty;
        	sDefault=string.Empty;
			try
			{
				GetPrivateProfileString(section,key,sDefault,returnString,chars,filename);
				conString=returnString;
			}
			catch(Exception ex)
			{
				err=ex.Message;
			}
			return conString;
		}
		
		/// <summary>
		/// This method is used for writing connection string in INI file with section and key
		/// </summary>
		/// <param name="wiSection">section value</param>
		/// <param name="wiKey">key value</param>
		/// <param name="wiValue">connection string</param>
		/// <param name="wiFile">INI file name with full path</param>
		/// <returns></returns>
		public int WriteINI(string wiSection,Dictionary<string, string>  wiKeyValue,string wiFile)
		{
			int writeBol=0;
			
			try 
			{
				foreach(string aKey in wiKeyValue.Keys)
				{
					writeBol=WritePrivateProfileString(wiSection,aKey,wiKeyValue[aKey].ToString(),wiFile);
				}
			} 
			catch (Exception ex) 
			{
				writeBol=0;
				err=ex.Message;
			}
			return writeBol;
		}
        public int WriteINI(string wiSection, string wiKey,string wiKeyValue, string wiFile)
        {
            int writeBol = 0;

            try
            {
                writeBol = WritePrivateProfileString(wiSection, wiKey, wiKeyValue, wiFile);
            }
            catch (Exception ex)
            {
                writeBol = 0;
                err = ex.Message;
            }
            return writeBol;
        }
	}
}
}