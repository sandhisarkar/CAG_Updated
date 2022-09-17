/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 13/3/2009
 * Time: 3:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// This abstract class is used for implement, data exchange from configuration file
	/// </summary>
	public abstract class Configuration
	{
		public abstract string GetValue(string prmSection,string prmKey);
		public abstract int SetValue(string prmSection,string prmKey,string prmValue);
	}
}
}