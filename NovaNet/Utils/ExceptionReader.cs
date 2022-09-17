/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 6/3/2009
 * Time: 12:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace NovaNet
{
namespace Utils
{
	public struct KeyValueStruct
	{
		public string Section;
		public string Key;
	}
		
	/// <summary>
	/// Description of ExceptionReader.
	/// </summary>
	public abstract class ExceptionReader
	{
		protected string fileName=null;
		public ExceptionReader(string prmFileName){fileName=prmFileName;}
		public abstract string Read(KeyValueStruct udtKeyValue);
		
	}
}
}