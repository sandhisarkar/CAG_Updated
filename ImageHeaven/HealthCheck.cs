/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 17/09/2009
 * Time: 1:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VersionCheck
{
	public struct AssemblyDetails
	{
		public string FullName;
		public string vMajor;
		public string vMinor;
		public string vRevision;
		public string CultureInfo;
		public string CodeBase;
	}
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class HealthInfo
	{
		public static List<AssemblyDetails> GetAssemblyDetails(List<string> prmAsmName)
		{
			List<AssemblyDetails> _ad = new List<AssemblyDetails>();
			AssemblyDetails ad;
			foreach(string str in prmAsmName)
			{
				Assembly a = GetAssembly(str);
				ad = new AssemblyDetails();
				if (a != null)
				{
					ad.FullName = a.GetName().Name;
					ad.vMajor = a.GetName().Version.ToString();
                    //ad.vMinor = a.GetName().Version.Minor.ToString();
                    //ad.vRevision = a.GetName().Version.MajorRevision.ToString();
					ad.CultureInfo = a.GetName().CultureInfo.ToString();
					ad.CodeBase = a.GetName().CodeBase;
				}
				//else
				//{
				//	ad.FullName = "Not found";
				//}
				_ad.Add(ad);
			}
			return _ad;
		}
		private static Assembly GetAssembly(string prmStr)
		{
			Assembly a = null;
			try
			{
				a = Assembly.Load(prmStr);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Print(ex.Message);
			}
			return a;
		}
	}
}