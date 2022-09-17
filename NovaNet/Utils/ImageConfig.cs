/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 13/3/2009
 * Time: 3:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// Description of ImageConfig.
	/// </summary>
	public class ImageConfig: Configuration
	{
		private string configFile=null;
		
		public ImageConfig(string prmConfigFileName)
		{
			configFile=prmConfigFileName;
		}
		public override string GetValue(string prmSection, string prmKey)
		{
			string prmValue=null;
			INIFile rdConfig=new INIFile();
			
			prmValue=rdConfig.ReadINI(prmSection,prmKey,string.Empty,configFile);
			return prmValue;
		}
		public override int SetValue(string prmSection, string prmKey, string prmValue)
		{
            int returnValue;
            INIFile rdConfig = new INIFile();

            returnValue = rdConfig.WriteINI(prmSection, prmKey,prmValue, configFile);
            return returnValue;
		}
	}
}
}