/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 17/2/2009
 * Time: 6:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// Description of Constants.
	/// </summary>
	public class Constants
	{
		public Constants()
		{
		}
		/// <summary>
		/// Constants for INI file
		/// </summary>
		public static string INI_FILE_NAME = "EDMS.INI";
		public static string INI_SECTION = "DBCon";
		public static string INI_KEY = "EDMS";
		public static string EXCEPTION_INI_FILE_PATH=System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase ).Remove(0,6)+ "\\" + "IhException.ini";
		/// <summary>
		/// Constants for internal programming
		/// </summary>
		public static int _ADDING = 0;
		public static int _EDITING = 1;
		
		/// <summary>
		/// Constants for Error
		/// </summary>
		public static string DBERRORTYPE = "DBERROR";
        public static string _SCAN_DPI = "SCANDPI";
		/// <summary>
		/// Populate Error List
		/// </summary>
		public static string NOT_VALID = "ERROR";
		
		/// <summary>
		/// For Exception list
		/// </summary>
		public static string ValidationException="Blank Filed";
		public static string SCHEMA_FILE_NAME="schema.ini";
		
		/// <summary>
		/// For Exception constants
		/// </summary>
		public static int DUPLICATE_KEY_CHECK=1001;
		public static int SAVE_ERROR=1002;
		public static string COMMON_EXCEPTION_SECTION="COMMONEXCEPTION";
		public static int PROJECT_FOLDER_CREATE_ERROR=2002;
		public static string PROJECT_EXCEPTION_SECTION="PROJECTEXCEPTION";
		public static string BATCH_EXCEPTION_SECTION="BATCHEXCEPTION";
		public static int BATCH_FOLDER_CREATE_ERROR=3002;
		public static string DB_CONNECTION_EXCEPTION_SECTION="DBCONNECTIONEXCEPTION";
		public static int DB_CONNECTION_ERROR=4001;
		public static int INI_FILE_EROR=1003;
		public static int CSV_READ_ERROR=5001;
		public static string CSV_READ_EXCEPTION_SECTION="CSVEXCEPTION";	
		public static int DUPLICATE_POLICY_ERROR=5002;
		public static int FOLDER_NOT_FOUND_ERROR=1004;
		
		///For Image related error
		/// 
		public static string IMAGE_ERROR="Error while image manupulation";
		
		//License constants
		public const string LIC = "0369983979922518273271316";

        //Scan upload flag
        public const string _SCAN_PENDING = "02";
        
        //Imaging constants
        public const short IGR_GDPICTURE = 1;
        public const short IGR_CLEARIMAGE = 2;

        ///MySql driver
        public const string  _MYSQL_DRIVER = "{MySQL ODBC 3.51 Driver}";
        public const string _MAIL_FROM = "rahuln@nevaehtech.com";
        public const string _MAIL_TO = "devsupport@nevaehtech.com";
        public const string _SMTP = "mail.nevaehtech.com";

        //Date time
        public const short _SET_YEAR = 1975;
        public const short _SET_MONTH = 01;
        public const short _SET_DATE = 01;
	}
}
}