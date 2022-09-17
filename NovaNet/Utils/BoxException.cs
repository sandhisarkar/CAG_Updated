/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 9/3/2009
 * Time: 1:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace NovaNet
{
	namespace Utils
	{
		/// <summary>
		/// Description of BoxException.
		/// </summary>
		public class BoxException
		{
			public BoxException()
			{
			}
		}
		
		/// <summary>
		/// Custom error class for handling csv file read error
		/// </summary>
		public class CSVReadException: Exception
		{
			public CSVReadException(String msg): base(msg)
			{}
		}
		
		/// <summary>
		/// Custom error class for duplicate csv file upload error
		/// </summary>
		public class DuplicateCsvException: Exception
		{
			public DuplicateCsvException(String msg): base(msg)
			{}
		}
	}
}