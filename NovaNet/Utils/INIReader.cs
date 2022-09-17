/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 6/3/2009
 * Time: 12:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;
namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// Description of INIReader.
	/// </summary>
	public class INIReader: ExceptionReader
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
		public string err=null;
		
		public INIReader(string prmFileName): base(prmFileName)
		{
		}
		public override string Read(KeyValueStruct udtKeyValue)
		{
		int chars = 1024;
        	string returnString = new string(' ', 1024);
        	//string sDefault = string.Empty;
        	string INIValue=string.Empty;
        	string sDefault=string.Empty;
			try
			{
				GetPrivateProfileString(udtKeyValue.Section,udtKeyValue.Key,sDefault,returnString,chars,fileName);
				INIValue=returnString;
			}
			catch(Exception ex)
			{
				err=ex.Message;
			}
			return INIValue;	
		}
	}
}
}