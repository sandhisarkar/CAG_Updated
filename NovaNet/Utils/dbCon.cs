/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 17/2/2009
 * Time: 12:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.Odbc;
using System.Data;
using System.IO;
using System.Windows.Forms;

//using MySql.Data.MySqlClient;
namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// This class is used for connect with database
	/// </summary>
	public class dbCon
	{
		private OdbcConnection dbcon;
		public string err=null;
		private INIReader rd=null;
		private KeyValueStruct udtKeyValue;
		private FileorFolder fileExs=null;
        private IWin32Window parentWindow;
        private bool cancelNot = false;

		public dbCon()
		{
            parentWindow = null;
		}
        public dbCon(IWin32Window prmParentWindow)
        {
            parentWindow = prmParentWindow;
        }
		//[STAThread]
		/// <summary>
		/// This method is to connect with the database
		/// </summary>
		/// <returns>Open Connection objcet</returns>
		public OdbcConnection Connect()
		{
			string conString=null;
			string iniPath=System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase ).Remove(0,6)+ "\\" + Constants.INI_FILE_NAME;
			string err=null;
			fileExs=new FileorFolder();
            DialogResult result;
			INIFile ini=new INIFile();
			dbcon = new OdbcConnection(conString);
			
			try
			{
				if(File.Exists(iniPath)==true)
				{
					conString=ini.ReadINI(Constants.INI_SECTION,Constants.INI_KEY,string.Empty,iniPath);
					dbcon.ConnectionString=conString;
					dbcon.Open();
				}
				else
				{
						rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
						udtKeyValue.Key=Constants.INI_FILE_EROR.ToString();
						udtKeyValue.Section=Constants.COMMON_EXCEPTION_SECTION;
						string ErrMsg=rd.Read(udtKeyValue);
                        //result = MessageBox.Show("Error while connect to the database...You want to create it?", "Connection error", MessageBoxButtons.YesNo);
                        //if (result == DialogResult.Yes)
                        //{
                        //    frmConnection frmConn = new frmConnection(iniPath);
                        //}
                        //else
                        //{
                        //    Application.Exit();
                        //}
				}
			}
			catch(Exception ex)
			{
				err=ex.Message;
                //result = MessageBox.Show("Error while connect to the database...You want to create it?","Connection error",MessageBoxButtons.YesNo);
                //if (result == DialogResult.Yes)
                //{
                //    frmConnection frmConn = new frmConnection(iniPath);
                //}
                //else
                //{
                //    Application.Exit();
                //}
			}
			if(dbcon.State== ConnectionState.Closed)
			{
				rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
				udtKeyValue.Key=Constants.DB_CONNECTION_ERROR.ToString();
				udtKeyValue.Section=Constants.DB_CONNECTION_EXCEPTION_SECTION;
				string ErrMsg=rd.Read(udtKeyValue);
                result = MessageBox.Show("Error while connect to the database...You want to create it?", "Connection error", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    frmConnection frmConn = new frmConnection(iniPath);
                    if (parentWindow != null)
                    {
                        frmConn.ShowDialog(parentWindow);
                    }
                    else
                    {
                        frmConn.ShowDialog();
                    }
                    if ((File.Exists(iniPath) == true))
                    {
                        conString = ini.ReadINI(Constants.INI_SECTION, Constants.INI_KEY, string.Empty, iniPath);
                        dbcon.ConnectionString = conString;
                        dbcon.Open();
                    }
                }
                else
                {
                    Application.Exit();
                }
			}
			return dbcon;
		}
        public void CancelNot(bool prmNot)
        {
            cancelNot = prmNot;
        }
		/// <summary>
		/// This method is used for get the current date with time
		/// </summary>
		/// <param name="prmFormat">1 for date with time and other for only date</param>
		/// <param name="prmCon">open connection object</param>
		/// <returns>date/time</returns>
		public string GetCurrenctDTTM(int prmType,OdbcConnection prmCon)
		{
			string sqlStr=null;
			OdbcDataReader reader=null;
			OdbcCommand cmd=null;
			string currDTTM=null;
            OdbcDataAdapter oAdap;
            DataSet ds = new DataSet();
			try 
			{
                if (prmType == 1)
                {
                    sqlStr = "select date_format(sysdate(),'%y-%m-%d %H.%i.%s')";
                    cmd = new OdbcCommand(sqlStr, prmCon);
                    oAdap = new OdbcDataAdapter(cmd);
                    oAdap.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currDTTM = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    //sqlStr = "select sysdate() as dt";
                    sqlStr = "select date_format(sysdate(),'%d/%m/%Y') as dt";
                    cmd = new OdbcCommand(sqlStr, prmCon);
                    oAdap = new OdbcDataAdapter(cmd);
                    oAdap.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currDTTM = ds.Tables[0].Rows[0]["dt"].ToString();
                    }
                }
				
			} catch (Exception ex) 
			{
				reader.Close();
				err=ex.Message;
			}
			return currDTTM;
		}
	}
}
}