/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 23/2/2009
 * Time: 6:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.Odbc;
using System.Data;
namespace NovaNet
{
namespace Utils
{
	
	public struct ControlInfo
	{
		public int proj_Key;
  		public int batch_Key;				
  		public string csvPath;
	}
    //public struct Credentials
    //{
    //    public string created_by;
    //    public string created_dttm;
    //    public string modified_by;
    //    public string modified_dttm;
    //}
	/// <summary>
	/// Description of wItemCreator.
	/// </summary>
	public abstract class wItemCreator: UtilsDeletgates
	{
		protected NotifyProgress nt=null;
		public wItemCreator()
		{
		}
		//public abstract bool CreateBox(Credentials prmCrd,ControlInfo prmInfo,int prmBoxStatus,int prmPolicyStatus);
        public abstract bool CreateBox(Credentials prmCrd, ControlInfo prmInfo, int prmBoxStatus, int prmPolicyStatus,DataSet ds,OdbcTransaction trans);
		public bool RegisterNotification(NotifyProgress prmNt)
		{
			nt=prmNt;
			return true;
		}
	}
	public class BoxCreator: wItemCreator
	{
		OdbcConnection sqlCon=null;
		Writer writeDb=null;
		Reader readCsv=null;
		DataSet ds=null;
		private INIReader rd=null;
		private KeyValueStruct udtKeyValue;
		
		public BoxCreator(OdbcConnection dbcon)
		{
			sqlCon=dbcon;
		}
		public override bool CreateBox(Credentials prmCrd,ControlInfo prmInfo,int prmBoxStatus,int prmPolicyStatus,DataSet ds,OdbcTransaction trans)
		{
            //ds=new DataSet();
            //readCsv=new csvReader(prmInfo.csvPath);
            //ds=readCsv.ReadData();
			writeDb=new MySqlWriter(sqlCon);
			
            //if(DuplicatePolicyCheck(ds,prmInfo)==false)
            //{
            //    if (KeyCheck(prmInfo)==false)
            //    {
            if (writeDb.SaveData(ds, prmCrd, prmInfo, nt, prmBoxStatus, prmPolicyStatus,trans) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
                    //else
                    //{
                    //    rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                    //    udtKeyValue.Key=Constants.SAVE_ERROR.ToString();
                    //    udtKeyValue.Section=Constants.COMMON_EXCEPTION_SECTION;
                    //    string ErrMsg=rd.Read(udtKeyValue);
                    //    throw new DbCommitException(ErrMsg);
                    //}
                //}
                //else
                //{
                //    rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                //    udtKeyValue.Key=Constants.DUPLICATE_KEY_CHECK.ToString();
                //    udtKeyValue.Section=Constants.COMMON_EXCEPTION_SECTION;
                //    string ErrMsg=rd.Read(udtKeyValue);
                //    throw new KeyCheckException(ErrMsg);
                //}
            //}
            //else
            //{
            //    rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
            //    udtKeyValue.Key=Constants.DUPLICATE_POLICY_ERROR.ToString();
            //    udtKeyValue.Section=Constants.CSV_READ_EXCEPTION_SECTION;
            //    string ErrMsg=rd.Read(udtKeyValue);
            //    throw new DuplicateCsvException(ErrMsg);
            //}
		}
		private bool KeyCheck(ControlInfo prmInfo)
		{
			string sqlStr=null;
			OdbcDataReader reader=null;
			OdbcCommand cmd=null;
			bool existsBol=true;
			
			
				sqlStr="select proj_key,batch_key from box_master where proj_key=" + prmInfo.proj_Key + " and batch_key=" + prmInfo.batch_Key;
				cmd=new OdbcCommand(sqlStr,sqlCon);
				reader=cmd.ExecuteReader();
				//Check primary key in boxmaster
				//if none found then show msg that policy no. should be primary key
				//if it is - go ahead
				if (reader.Read())
				{
					existsBol=true;
				}
				else
				{
					existsBol=false;			
				}
					return existsBol;
		}
		private bool DuplicatePolicyCheck(DataSet prmDs,ControlInfo pControl)
		{
			string sqlStr=null;
			OdbcDataReader reader=null;
			OdbcCommand cmd=null;
			
			for (int i=0;i<prmDs.Tables[0].Rows.Count;i++)
			{
                sqlStr = "select policy_number from policy_master where proj_key=" + pControl.proj_Key + " and batch_key=" + pControl.batch_Key + " and box_number='" + prmDs.Tables[0].Rows[i]["Book"].ToString() + "' and policy_number=" + prmDs.Tables[0].Rows[i]["DeedNo"];
				cmd=new OdbcCommand(sqlStr,sqlCon);
				reader=cmd.ExecuteReader();
				
				if(reader.HasRows==true)
				{
                    System.Windows.Forms.MessageBox.Show("Duplicate policy found - " + prmDs.Tables[0].Rows[i]["DeedNo"].ToString());
					return true;
				}
			}
			return false;
		}
	}
}
}