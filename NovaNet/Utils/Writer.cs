/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 23/2/2009
 * Time: 6:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Data.Odbc;

namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// Description of Writer.
	/// </summary>
	public abstract class Writer
	{

		public Writer()
		{
		}
        public abstract bool SaveData(DataSet prmCSVData, Credentials prmCrd, ControlInfo prmInfo, NotifyProgress nt, int prmBoxStatus, int prmPolicyStatus, OdbcTransaction sqlTrans);
	}
	public class MySqlWriter: Writer
	{
		OdbcConnection sqlCon=null;
		OdbcCommand sqlCmd=null;
		OdbcTransaction sqlTrans=null;
        DataSet ds = null;
		private string err=null;
		
		public MySqlWriter(OdbcConnection prmCon)
		{
			sqlCon=prmCon;
		}
        public override bool SaveData(DataSet prmCSVData, Credentials prmCrd, ControlInfo prmInfo, NotifyProgress nt, int prmBoxStatus, int prmPolicyStatus, OdbcTransaction sqlTrans)
		{
			try
			{
				//sqlTrans=sqlCon.BeginTransaction();
                ds = prmCSVData;
				SaveRawData(sqlTrans,prmCSVData,prmCrd,prmInfo);
				if (nt != null)
					nt(33);
				SaveBoxData(sqlTrans,prmCrd,prmInfo,prmBoxStatus);
				if (nt != null)
					nt(66);
				SavePolicyMasterData(sqlTrans,prmCSVData,prmCrd,prmInfo,prmPolicyStatus);
				if (nt != null)
					nt(100);
				//sqlTrans.Commit();
				return true;
			}
			catch(Exception ex)
			{
				sqlTrans.Rollback();
				err=ex.Message;
				return false;
			}
			
		}
		private void SaveRawData(OdbcTransaction prmTransaction,DataSet prmCSVData,Credentials prmCrd,ControlInfo prmInfo)
		{
			string sqlStr=null;
			sqlCmd=new OdbcCommand();
            
            for (int i = 0; i < prmCSVData.Tables[0].Rows.Count; i++)
            {
                string deedNo = prmCSVData.Tables[0].Rows[i][0].ToString() + prmCSVData.Tables[0].Rows[i][1].ToString() + prmCSVData.Tables[0].Rows[i][2].ToString() + prmCSVData.Tables[0].Rows[i][3].ToString() + "[" + prmCSVData.Tables[0].Rows[i][4].ToString() + "]";
                sqlStr = "INSERT INTO rawdata (proj_key, batch_key, serial_number, division_code, branch_code, batch_serial, policy_no,created_by,created_dttm)" +
                    " VALUES (" + prmInfo.proj_Key + "," + prmInfo.batch_Key + "," +
                    "'" + prmCSVData.Tables[0].Rows[i][0] + "', '" + prmCSVData.Tables[0].Rows[i][1] + "'," +
                    "'" + prmCSVData.Tables[0].Rows[i][2] + "', '" + prmCSVData.Tables[0].Rows[i][3] + "'," +
                    "'" + deedNo + "'," +
                    "'" + prmCrd.created_by + "', '" + prmCrd.created_dttm + "')";

                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = prmTransaction;
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();
            }
		}
		
		private void SaveBoxData(OdbcTransaction prmTransaction,Credentials prmCrd,ControlInfo prmInfo,int prmBoxStatus)
		{
			string sqlStr=null;
			sqlCmd=new OdbcCommand();
			string batchPath=null;
			string boxPath=null;
            
            DataTable distinctDT = ds.Tables[0].DefaultView.ToTable(true, new string[] { "Book" });
            //DataTable distinctDT = ds.Tables[0].DefaultView.ToTable();
			batchPath=GetBatchPath(prmInfo.proj_Key,prmInfo.batch_Key,prmTransaction);
            for (int i = 0; i < distinctDT.Rows.Count; i++)
			{
                
                boxPath = batchPath + "\\" + "1";
                boxPath = boxPath.Replace("\\", "\\\\");
                sqlStr = @"INSERT INTO box_master (proj_key, batch_key, box_number, box_path, created_by, created_dttm,status)" +
                    "VALUES (" + prmInfo.proj_Key + "," + prmInfo.batch_Key + ",'1', '" + boxPath + "','" + prmCrd.created_by + "', '" + prmCrd.created_dttm + "'," + prmBoxStatus + ")";
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = prmTransaction;
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();
                
			}
		}
		
		private string GetBatchPath(int prmProjKey,int prmBatchKey,OdbcTransaction prmTrans)
		{
			string sqlStr=null;
			DataSet dsPath=new DataSet();
			string batchPath=null;
			OdbcDataAdapter sqlAdap;
			sqlCmd=new OdbcCommand();
			
			sqlCmd.Connection=sqlCon;
			sqlStr=@"select batch_path from batch_master where proj_code=" + prmProjKey + " and batch_key=" + prmBatchKey ;
			sqlCmd.Transaction=prmTrans;
			sqlCmd.CommandText=sqlStr;
			sqlAdap=new OdbcDataAdapter(sqlCmd);
			sqlAdap.Fill(dsPath);
			if (dsPath.Tables[0].Rows.Count>0)
	        {
				batchPath=dsPath.Tables[0].Rows[0]["batch_Path"].ToString();
			}
			return batchPath;
		}
		
		private void SavePolicyMasterData(OdbcTransaction prmTransaction,DataSet prmCSVData,Credentials prmCrd,ControlInfo prmInfo,int prmPolicyStatus)
		{
			string sqlStr=null;
			sqlCmd=new OdbcCommand();
			string boxPath=null;
			string policyPath=null;
			

			for (int i=0;i<prmCSVData.Tables[0].Rows.Count;i++)
			{
                boxPath = GetBoxPath(prmInfo.proj_Key, prmInfo.batch_Key, "1", prmTransaction);
                string deedNo = prmCSVData.Tables[0].Rows[i][0].ToString() + prmCSVData.Tables[0].Rows[i][1].ToString() + prmCSVData.Tables[0].Rows[i][2].ToString() + prmCSVData.Tables[0].Rows[i][3].ToString() + "[" + prmCSVData.Tables[0].Rows[i][4].ToString() + "]";
				policyPath=boxPath + "\\" + deedNo;
				policyPath=policyPath.Replace("\\","\\\\");
                
				sqlStr=@"INSERT INTO policy_master (proj_key, batch_key, box_number, policy_number, policy_path, created_by, created_dttm,status,scan_upload_flag,DO_CODE,BR_CODE,YEAR,deed_year,deed_no,deed_vol,page_from,page_to)" +
                    "VALUES (" + prmInfo.proj_Key + "," + prmInfo.batch_Key + ",'1','" + deedNo + "'," +
                    "'" + policyPath + "','" + prmCrd.created_by + "', '" + prmCrd.created_dttm + "'," + prmPolicyStatus + ",'" + Constants._SCAN_PENDING + "','" + prmCSVData.Tables[0].Rows[i][0].ToString() + "','" + prmCSVData.Tables[0].Rows[i][1].ToString() + "','" + prmCSVData.Tables[0].Rows[i][2].ToString() + "','" + prmCSVData.Tables[0].Rows[i][3].ToString() + "','" + prmCSVData.Tables[0].Rows[i][4].ToString() + "','" + prmCSVData.Tables[0].Rows[i][9].ToString() + "','" + prmCSVData.Tables[0].Rows[i][10].ToString() + "','" + prmCSVData.Tables[0].Rows[i][11].ToString() + "')";
	
					sqlCmd.Connection = sqlCon;
					sqlCmd.Transaction=prmTransaction;
		            sqlCmd.CommandText = sqlStr;
		            sqlCmd.ExecuteNonQuery();
			}
		}
		private string GetBoxPath(int prmProjKey,int prmBatchKey,string prmBoxNo,OdbcTransaction prmTrans)
		{
			string sqlStr=null;
			DataSet dsPath=new DataSet();
			string boxPath=null;
			OdbcDataAdapter sqlAdap;
			sqlCmd=new OdbcCommand();
			
			sqlStr=@"select box_path from box_master where proj_key=" + prmProjKey + " and batch_key=" + prmBatchKey + " and box_number='1'";
			sqlCmd.Connection=sqlCon;
			sqlCmd.Transaction=prmTrans;
			sqlCmd.CommandText=sqlStr;
			
			sqlAdap=new OdbcDataAdapter(sqlCmd);
			sqlAdap.Fill(dsPath);
			if (dsPath.Tables[0].Rows.Count>0)
	        {
				boxPath=dsPath.Tables[0].Rows[0]["box_path"].ToString();
			}
			return boxPath;
		}
	}
}
}