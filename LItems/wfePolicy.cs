/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 20/3/2009
 * Time: 11:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NovaNet.wfe;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using NovaNet.Utils;
using System.IO;

namespace LItems
{
	/// <summary>
	/// Structure for policy table
	/// </summary>
	public class udtPolicy: NovaNet.wfe.udtCmd
	{
		public int proj_key;
  		public int batch_key;
  		public int box_number;
  		public int policy_number;
  		public string policy_path;
  		public string created_by;
  		public string created_dttm;
  		public string modified_by;
  		public string modified_dttm;
  		public int count_of_pages;
  		public int status;
	}
	
	public struct policyException
	{
  		public int missing_img_exp;
  		public int crop_clean_exp;
  		public int poor_scan_exp;
  		public int wrong_indexing_exp;
  		public int linked_policy_exp;
  		public int decision_misd_exp;
  		public int extra_page_exp;
  		public int rearrange_exp;
  		public int other_exp;
  		public int move_to_respective_policy_exp;
  		public int solved;
  		public string comments;
  		public int status;
	}
	
	public class CtrlPolicy: NovaNet.wfe.wItemControl
	{
		private int proj_Key;
  		private int batch_key;
        private string box_number;
        private string policy_number;
        //private string _item;

        public CtrlPolicy(int projKey, int batchKey, string boxNumber)
        {
            proj_Key = projKey;
            batch_key = batchKey;
            box_number = boxNumber;
        }

        //public CtrlPolicy(int projKey, int batchKey, string boxNumber, string policyNumber,string item_no)
        //{
        //    proj_Key = projKey;
        //    batch_key = batchKey;
        //    box_number = boxNumber;
        //    policy_number = policyNumber;
        //    _item = item_no;
        //}

        public CtrlPolicy(int projKey, int batchKey,string boxNumber,string policyNumber)
		{
			proj_Key=projKey;
			batch_key=batchKey;
			box_number=boxNumber;
			policy_number=policyNumber;
		}
		public int BatchKey
		{
			get
			{
				return batch_key;
			}
		}
		
		public int ProjectKey
		{
			get
			{
				return proj_Key;
			}
		}
        public string BoxNumber
		{
			get
			{
				return box_number;
			}
		}
        public string PolicyNumber
		{
			get
			{
				return policy_number;
			}
		}

        //public string ItemNo
        //{
        //    get
        //    {
        //        return _item;
        //    }
        //}
    }

    public class PolicyDetails
    {
        public string proj_key { get; set; }
        public string batch_key { get; set; }
        public string box_number { get; set; }
        public string policy_number { get; set; }
        public string policy_path { get; set; }
        public string created_by { get; set; }
        public string created_dttm { get; set; }
        public string modified_by { get; set; }
        public string modified_dttm { get; set; }
        public string count_of_pages { get; set; }
        public string status { get; set; }
        public string Scan_upload_flag { get; set; }
        public string scanned_date { get; set; }
        public string Incremented_Scan { get; set; }
        public string Lic_checked { get; set; }
        public string photo { get; set; }
        public string custom_exception { get; set; }
        public string Locked_uid { get; set; }
        public string expires_dttm { get; set; }
        public string invalid { get; set; }
        public string validation_status { get; set; }
        public string Do_code { get; set; }
        public string Bo_Code { get; set; }
        public string Book { get; set; }
        public string Year { get; set; }
        public string deed_year { get; set; }
        public string Deed_no { get; set; }
        public string Deed_vol { get; set; }
        public string Page_from { get; set; }
        public string Page_to { get; set; }
        public string vol { get; set; }
        public string runno { get; set; }
    }

	/// <summary>
	/// Description of wfePolicy.
	/// </summary>
	public class wfePolicy: wItem, StateData
	{
        
        MemoryStream stateLog;
        byte[] tmpWrite;
		OdbcConnection sqlCon;
		public CtrlPolicy ctrlPolicy=null;
		udtPolicy Data=null;
		OdbcDataAdapter sqlAdap;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);	
		
		public wfePolicy(OdbcConnection prmCon): base(prmCon, NovaNet.Utils.Constants._ADDING)
		{
			sqlCon=prmCon;
            exMailLog.SetNextLogger(exTxtLog);
		}
		
		public wfePolicy(OdbcConnection prmCon, CtrlPolicy prmCtrl): base(prmCon, NovaNet.Utils.Constants._EDITING)
		{
            //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
			sqlCon=prmCon;
			ctrlPolicy = prmCtrl;
            //exMailLog.SetNextLogger(exTxtLog);
            //LoadValuesFromDB();
		}

        //added by arpan

        //public wfePolicy(OdbcConnection prmCon, CtrlPolicy prmCtrl, OdbcTransaction oTrans): base(prmCon, NovaNet.Utils.Constants._EDITING)
        //{
        //    //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
        //    sqlCon = prmCon;
        //    ctrlPolicy = prmCtrl;
        //    sqlTrans = oTrans;
        //    //exMailLog.SetNextLogger(exTxtLog);
        //    //LoadValuesFromDB();
        //}



		public override bool Commit()
		{
			throw new NotImplementedException();
		}
		public override bool KeyCheck(string prmValue)
		{
			throw new NotImplementedException();
		}
        MemoryStream StateData.StateLog()
        {
            return stateLog;
        }
		public override udtCmd LoadValuesFromDB()
		{
			string sqlStr=null;
			
			DataSet policyDs=new DataSet();
			Data=new udtPolicy();
			try 
			{
				sqlStr="select proj_key,batch_key,box_number,policy_number,policy_path,created_by,created_dttm,modified_by,modified_dttm,count_of_pages,status from policy_master where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'" ;
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(policyDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			if(policyDs.Tables[0].Rows.Count>0)
			{
				Data.policy_path=policyDs.Tables[0].Rows[0]["policy_path"].ToString();
			}
			return Data;
		}
        public DataSet GetVol(string dis, string ro, string book, string deedyear)
        {
            string sql = "select distinct Volume_No from deed_details where District_Code = '" + dis + "' and RO_Code = '" + ro + "' and Book = '" + book + "' and Deed_year = '" + deedyear + "' order by convert(Volume_No,UNSIGNED INTEGER)";
            DataSet ds = new DataSet();
            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(ds);
            return ds;
        }
        public DataSet getMovePolicyPageDetails(CtrlImage pImage)
        {
            string sql = "select page_name from image_master where proj_key = '2' and batch_key = '23' and box_number = '1' and policy_number = '19031222[00099]' order by serial_no";
            DataSet ds = new DataSet();
            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(ds);
            return ds;

        }
        public DataSet GetProject(string dis, string ro, string book, string deedyear,string vol)
        {
            string sql = "select distinct proj_key,batch_key from deed_details where District_Code = '" + dis + "' and ro_code = '" + ro + "' and book = '" + book + "' and deed_year = '" + deedyear + "' and Volume_No = '" + vol + "'";
            DataSet ds = new DataSet();
            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(ds);
            return ds;
        }
        public DataSet GetDeedStatus(string proj_key, string batch_key)
        {
            string sql = "select status from box_master where proj_key = '" + proj_key + "' and batch_key = '" + batch_key + "' and box_number = '1'";
            DataSet ds = new DataSet();
            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(ds);
            return ds;
        }
        public DataSet GetPolicyListImport(string proj_key, string batch_key)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {

               // sqlStr = "select policy_number from policy_master where proj_key='" + proj_key + "'and batch_key= '" + batch_key + "' and status = '16' order by CONVERT(page_from,UNSIGNED INTEGER)";
                sqlStr = "select filename from metadata_entry where proj_code='" + proj_key + "'and batch_key= '" + batch_key + "' ";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public bool SaveUatInfo(string runno, string percent, string created_by, string created_dttm)
        {
            OdbcCommand Cmd = new OdbcCommand();
            try
            {
                string sql = "insert into tbl_uat_info(percent_checked,run_no,cretaed_by,created_dttm) values ('"+percent+"','"+runno+"','"+created_by+"','"+created_dttm+"')";
                Cmd.Connection = sqlCon;
                Cmd.CommandText = sql;
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool SaveBlankDeeds(PolicyDetails pFile,OdbcTransaction txn)
        {
            try
            {
                OdbcTransaction sqlTrans = null;
                OdbcCommand sqlCmdPolicy = new OdbcCommand();
                OdbcCommand sqlRawdata = new OdbcCommand();
                string sqlStr = @"INSERT INTO policy_master (proj_key, batch_key, box_number, policy_number, policy_path, created_by, created_dttm,status,scan_upload_flag,count_of_pages,photo,do_code,br_code,deed_year,deed_no,year,deed_vol,page_from,page_to,run_no)" +
                        "VALUES (" + pFile.proj_key + "," + pFile.batch_key + ",'" + pFile.box_number + "','" + pFile.policy_number + "'," +
                        "'" + pFile.policy_path + "','" + pFile.created_by + "', '" + pFile.created_dttm + "',20,'01','15','0','" + pFile.Do_code + "','" + pFile.Bo_Code + "','" + pFile.deed_year + "','" + pFile.Deed_no + "','"+ pFile.Book +"','"+ pFile.vol +"','"+ pFile.Page_from +"','"+ pFile.Page_to +"','"+pFile.runno+"')";
                sqlTrans = txn;
                sqlCmdPolicy.Connection = sqlCon;
                sqlCmdPolicy.Transaction = sqlTrans;
                sqlCmdPolicy.CommandText = sqlStr;
                if (sqlCmdPolicy.ExecuteNonQuery() > 0)
                {
                    sqlStr = @"INSERT INTO rawdata (proj_key, batch_key, serial_number, division_code, branch_code, batch_serial,policy_no)" +
                        "VALUES (" + pFile.proj_key + "," + pFile.batch_key + ",'" + pFile.Do_code + "','" + pFile.Bo_Code + "'," +
                        "'" + pFile.Book + "','" + pFile.Year + "', '" + pFile.policy_number + "')";
                    sqlRawdata.Connection = sqlCon;
                    sqlRawdata.Transaction = sqlTrans;
                    sqlRawdata.CommandText = sqlStr;
                    sqlRawdata.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool UpdateDeeds(OdbcTransaction pTxn,string pDoCode,string pRoCode,string pBook,string pDeedYear,string pDeedNumber,PolicyDetails pDetails)
        {
            try
            {
                OdbcTransaction sqlTrans = null;
                OdbcCommand sqlCmdPolicy = new OdbcCommand();
                OdbcCommand sqlRawdata = new OdbcCommand();
                string sqlStr = @"UPDATE deed_details set proj_key = '"+ pDetails.proj_key +"',batch_key='"+ pDetails.batch_key +"',box_key='" + pDetails.box_number + "'" +
                                 " where district_code='"+ pDoCode + "' and ro_code="+ pRoCode +" and book='"+ pBook + "' and deed_year='"+ pDeedYear +"' and deed_no='"+ pDeedNumber +"'";
                sqlTrans = pTxn;
                sqlCmdPolicy.Connection = sqlCon;
                sqlCmdPolicy.Transaction = sqlTrans;
                sqlCmdPolicy.CommandText = sqlStr;
                sqlCmdPolicy.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool UpdateBlankDeeds(OdbcTransaction pTxn,string pPageFrom,string pPageTo)
        {
            try
            {
                OdbcTransaction sqlTrans = null;
                OdbcCommand sqlCmdPolicy = new OdbcCommand();
                OdbcCommand sqlRawdata = new OdbcCommand();
                string sqlStr = @"UPDATE policy_master set page_from = '" + pPageFrom + "',page_to='" + pPageTo + "'" +
                                 " where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber + "'";
                sqlTrans = pTxn;
                sqlCmdPolicy.Connection = sqlCon;
                sqlCmdPolicy.Transaction = sqlTrans;
                sqlCmdPolicy.CommandText = sqlStr;
                sqlCmdPolicy.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }
		public int  GetPolicyCount(eSTATES[] state)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			sqlStr="select count(*) from metadata_entry " + 
					" where proj_code=" + ctrlPolicy.ProjectKey + 
				" and batch_key=" + ctrlPolicy.BatchKey + " ";
			
			
			try 
			{
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(dsImage);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
            if (dsImage.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsImage.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
		}
        public string GetPolicyNumberFromTransaction()
        {
            string sqlStr = null;

            DataSet expDs = new DataSet();

            try
            {
                sqlStr = "select transaction_number from rawdata where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key = " + ctrlPolicy.BatchKey + " and policy_no='" + ctrlPolicy.PolicyNumber + "'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(expDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            if (expDs.Tables[0].Rows.Count > 0)
            {
                return expDs.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public string GetHoldDeedCount()
        {
            string sqlStr = null;

            DataSet expDs = new DataSet();

            try
            {
                sqlStr = "select count(*) from deed_details where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key = " + ctrlPolicy.BatchKey + " and box_key='" + ctrlPolicy.BoxNumber + "' and hold='Y'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(expDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            if (expDs.Tables[0].Rows.Count > 0)
            {
                return expDs.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public int GetTransactionLogCount(string prmBatchId,string prmDate,string prmUser,eSTATES state)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            switch (state)
            {
                case eSTATES.POLICY_QC:
                    {
                        sqlStr = "SELECT count(*) FROM transaction_log where qc_user='" + prmUser + "' and date_format(qc_dttm,'%d/%m/%Y')='" + prmDate + "'";
                        break;
                    }
                case eSTATES.POLICY_INDEXED:
                    {
                        sqlStr = "SELECT count(*) FROM transaction_log where index_user='" + prmUser + "' and date_format(index_dttm,'%d/%m/%Y')='" + prmDate + "'";
                        break;
                    }
                case eSTATES.POLICY_FQC:
                    {
                        sqlStr = "SELECT count(*) FROM transaction_log where fqc_user='" + prmUser + "' and date_format(fqc_dttm,'%d/%m/%Y')='" + prmDate + "'";
                        break;
                    }
            }
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            if (dsImage.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsImage.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }



        public int GetPolicyStatus()
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select status from policy_master " +
                    " where proj_key=" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return Convert.ToInt32(dsImage.Tables[0].Rows[0]["status"]);
        }

        public int GetPolicyStatus(string policyNo)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select status from policy_master " +
                    " where policy_number='" + policyNo + "'";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return Convert.ToInt32(dsImage.Tables[0].Rows[0]["status"]);
        }

		public DataSet GetPolicyDetails()
		{
			string sqlStr=null;
			
			DataSet policyDs=new DataSet();
			
			try 
			{
                sqlStr = "select A.name_of_policyholder,A.date_of_birth,A.date_of_commencement,A.customer_id,A.policy_no,B.Scan_upload_flag,date_format(B.scanned_date,'%Y%m%d') as scanned_date,B.Incremented_Scan,B.status,A.serial_number from rawdata A,policy_master B where (A.proj_key=B.proj_key and A.batch_key=b.batch_key and A.policy_no = B.policy_number) and (B.proj_key=" + ctrlPolicy.ProjectKey + " and B.batch_key=" + ctrlPolicy.BatchKey + " and B.box_number=" + ctrlPolicy.BoxNumber + " and B.policy_number='" + ctrlPolicy.PolicyNumber + "')";
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(policyDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			
			return policyDs;
		}
        public DataSet GetPolicyLog()
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select Scanned_user,qc_user,index_user,fqc_user from transaction_log where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }

        public DataSet GetPolicyLog1()
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select created_by from metadata_entry where proj_code =" + ctrlPolicy.ProjectKey + " and batch_key =" + ctrlPolicy.BatchKey + " and filename ='" + ctrlPolicy.PolicyNumber + "'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }

        public DataSet GetAllPolicyDetailsRaw(wfeBox wBox)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select A.name_of_policyholder,A.date_of_birth,A.date_of_commencement,A.customer_id,trim(A.policy_no) as policy_no,A.serial_number,A.batch_serial from rawdata A where A.proj_key=" + wBox.ctrlBox.ProjectCode + " and A.batch_key=" + wBox.ctrlBox.BatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            
            return policyDs;
        }
        /*
        public int GetMaxSerial(wfeBox wBox)
        {
            string sqlStr = null;
            string maxSerial = string.Empty;
            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select max(A.serial_number) from rawdata A,policy_master B where A.proj_key=B.proj_key and A.batch_key=B.batch_key and A.proj_key=" + ctrlPolicy.ProjectKey + " and A.batch_key=" + ctrlPolicy.BatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            if (policyDs.Tables[0].Rows.Count > 0)
            {
                maxSerial = policyDs.Tables[0].Rows[0][0].ToString();
                return Convert.ToInt32(maxSerial);
            }
            else
            {
                maxSerial = 0;
            }
        }
         */ 
        public DataSet GetAllPolicyDetails(wfeBox wBox,out OdbcDataAdapter pAdp)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select Scan_upload_flag,date_format(scanned_date,'%Y%m%d') as scanned_date,Incremented_Scan,status,policy_number,box_number,proj_key,batch_key from policy_master where proj_key=" + wBox.ctrlBox.ProjectCode + " and batch_key=" + wBox.ctrlBox.BatchKey + " order by box_number,policy_number";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
                
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            finally
            {
                pAdp = sqlAdap;
            }
            return policyDs;
        }
        public DataSet GetCustExcpList()
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select policy_number as 'File Number',problem_type as 'Problem Type',image_name as 'Image Name',remarks as 'Remarks',created_by as User_Name,if(status=2,'Unresolved','Resolved') as ImageStatus from custom_exception where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }

        public DataSet GetMaxScannedDate()
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select min(date_format(B.scanned_date,'%Y%m%d')) as scanned_date from policy_master B where b.proj_key=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey + " and b.box_number=" + ctrlPolicy.BoxNumber;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
		public DataSet GetAllException()
		{
			string sqlStr=null;
			
			DataSet expDs=new DataSet();
			
			try 
			{
				sqlStr="select missing_img_exp,crop_clean_exp,poor_scan_exp,wrong_indexing_exp,linked_policy_exp,decision_misd_exp,extra_page_exp,rearrange_exp,other_exp,move_to_respective_policy_exp,comments from lic_qa_log where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and policy_number='" + ctrlPolicy.PolicyNumber + "' and qa_status<>0";
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(expDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			
			return expDs;
		}
        public int GetLICLogStatus()
        {
            string sqlStr = null;

            DataSet expDs = new DataSet();

            try
            {
                sqlStr = "select qa_status from lic_qa_log where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(expDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            if (expDs.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(expDs.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }
        public DataSet GetMissingDocumentPolicyLst()
        {
            string sqlStr = null;

            DataSet expDs = new DataSet();

            try
            {
                sqlStr = "select doc_type,count(*),policy_number,box_number from image_master where batch_key = " + ctrlPolicy.BatchKey + " and policy_number='" + ctrlPolicy.PolicyNumber + "' and doc_type in ('Main_Petition','Main_Petition_Annextures','Vakalatnama_Affidavit_of','Orders_Main_Case','Final_Judgement_Order') group by policy_number, doc_type";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(expDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return expDs;
        }
		public DataSet GetPolicyList(eSTATES[] prmState)
		{
			string sqlStr=null;
			
			DataSet policyDs=new DataSet();
			
			try 
			{
				if (prmState.Length == 0)
				{
                    sqlStr = "select A.policy_no,A.name_of_policyholder,A.date_of_birth,A.date_of_commencement,B.status,B.count_of_pages,B.photo from rawdata A, policy_master B where A.proj_key=B.proj_key and A.batch_key=B.batch_key and A.policy_no = B.policy_number and B.proj_key=" + ctrlPolicy.ProjectKey + " and B.batch_key=" + ctrlPolicy.BatchKey + " and B.box_number=" + ctrlPolicy.BoxNumber;
				}
				else
				{
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select A.policy_no,A.name_of_policyholder,A.date_of_birth,A.date_of_commencement,B.status,B.count_of_pages,B.photo,B.status from rawdata A, policy_master B where A.proj_key=B.proj_key and A.batch_key=B.batch_key and A.policy_no = B.policy_number and B.proj_key=" + ctrlPolicy.ProjectKey + " and B.batch_key=" + ctrlPolicy.BatchKey + " and B.box_number=" + ctrlPolicy.BoxNumber;
                    }
                    else
                    {
                        sqlStr = "select A.policy_no,A.name_of_policyholder,A.date_of_birth,A.date_of_commencement,B.status,B.count_of_pages,B.photo,B.status,B.box_number from rawdata A, policy_master B where A.proj_key=B.proj_key and A.batch_key=B.batch_key and A.policy_no = B.policy_number and B.proj_key=" + ctrlPolicy.ProjectKey + " and B.batch_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (B.status=" + (int)prmState[j];
                            }
                            else
                                sqlStr = sqlStr + " or B.status=" + (int)prmState[j];
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(policyDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			
			return policyDs;
		}
        public DataSet GetPolicyList1(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            
            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey ;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                    }
                   
                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (status=" + 0;
                            }
                            else
                                sqlStr = sqlStr + " or status=" + 0;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList2(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (status=" + 1;
                            }
                            else
                                sqlStr = sqlStr + " or status=" + 1;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList3(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (status=" + 2;
                            }
                            else
                                sqlStr = sqlStr + " or status=" + 2;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList4(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (status=" + 4;
                            }
                            else
                                sqlStr = sqlStr + " or status=" + 4;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList5(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 6 or b.status = 8) and b.phase1 ='F' and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                    //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 6 or b.status = 8) and b.phase1 ='F' and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                        //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + 4 + " or a.status = " + 5 + " or a.status = " + 31 + " or a.status = " + 40;
                            }
                            else
                                sqlStr = sqlStr + " or a.status=" + 4 + " or a.status=" + 5 + " or a.status = " + 31 + " or a.status =" + 40 ;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList55(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 6 or b.status = 7) and b.phase2 ='F' and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                    //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 6 or b.status = 7) and b.phase2 ='F' and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                        //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + 4 + " or a.status = " + 5 + " or a.status = "+ 31 + " or a.status = " + 40;
                            }
                            else
                                sqlStr = sqlStr + " or a.status=" + 4 + " or a.status=" + 5 + " or a.status = " + 31 + " or a.status =" + 40;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList6(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 6 or b.status = 7) and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                    //sqlStr = "select case_file_no from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 6 or b.status = 7) and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                        //sqlStr = "select case_file_no from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + 30;
                            }
                            else
                                sqlStr = sqlStr + " or a.status=" + 30;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList11(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 4 or b.status = 5) and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                    //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and (b.status = 2 or b.status = 3 or b.status = 4 or b.status = 5) and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                        //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + 4 + " or a.status=" + 5 + " or a.status =" + 31 + " or a.status=" + 40;
                            }
                            else
                                sqlStr = sqlStr + "  or a.status=" + 4 + " or a.status=" + 5 + " or a.status =" + 31 + " or a.status=" + 40;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList12(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey +" and entry_exception <> '' ";
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select filename from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and entry_exception <> '' ";
                    }

                    //for (int j = 0; j < prmState.Length; j++)
                    //{
                    //    if ((int)prmState[j] != 0)
                    //    {
                    //        if (j == 0)
                    //        {
                    //            sqlStr = sqlStr + " and (remarks=" + remarks;
                    //        }
                    //        else
                    //            sqlStr = sqlStr + " or remarks=" + remarks;
                    //    }
                    //}
                    //sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }

        public DataSet GetPolicyList13(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey + " and image_exception <> '' ";
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey + " and image_exception <> '' ";
                    }

                    //for (int j = 0; j < prmState.Length; j++)
                    //{
                    //    if ((int)prmState[j] != 0)
                    //    {
                    //        if (j == 0)
                    //        {
                    //            sqlStr = sqlStr + " and (remarks=" + remarks;
                    //        }
                    //        else
                    //            sqlStr = sqlStr + " or remarks=" + remarks;
                    //    }
                    //}
                    //sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyListCombo(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select filename,status from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select filename,status from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (status=" + 4;
                            }
                            else
                                sqlStr = sqlStr + " or status=" + 5 + " or status=" + 6 + " or status=" + 7 + " or status=" + 8 + " or status=" + 9 + " or status=" + 30 + " or status=" + 31 + " or status=" + 40;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList7(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select case_file_no from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select case_file_no from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (status=" + 37;
                            }
                            else
                                sqlStr = sqlStr + " or status=" + 37;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList8(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select case_file_no from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select case_file_no from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (status=" + 36;
                            }
                            else
                                sqlStr = sqlStr + " or status=" + 36;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList9(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and b.status = 8 and phase1 = 'T' and phase2 = 'T' and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                    //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and b.status = 8 and phase1 = 'T' and phase2 = 'T' and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                        //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + 4 + " or a.status=" + 5 + " or a.status =" + 31 + " or a.status =" + 40;
                            }
                            else
                                sqlStr = sqlStr + " or a.status=" + 4 + " or a.status=" + 5 + " or a.status =" + 31 + " or a.status =" + 40;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyList10(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();


            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and b.status = 9 and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                    //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                }
                else
                {
                    if (ctrlPolicy.BoxNumber != "0")
                    {
                        sqlStr = "select a.filename from metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and b.status = 9 and b.proj_code=" + ctrlPolicy.ProjectKey + " and b.batch_key=" + ctrlPolicy.BatchKey;
                        //sqlStr = "select filename from case_file_master where proj_code=" + ctrlPolicy.ProjectKey + " and bundle_key=" + ctrlPolicy.BatchKey;
                    }

                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + 4 + " or a.status = " + 5 + " or a.status = " + 9 + " or a.status = " + 31 + " or a.status = " + 40;
                            }
                            else
                                sqlStr = sqlStr + " or a.status=" + 4 + " or a.status = " + 5 + " or a.status = " + 9 + " or a.status = " + 31 + " or a.status = " + 40;
                        }
                    }
                    sqlStr = sqlStr + ")";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetImportablePolicyList(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                
                    sqlStr = "select A.policy_number,A.STATUS from policy_master A,image_master B where A.proj_key=B.proj_key and A.batch_key=B.batch_key and A.box_number=B.box_number and A.policy_number=B.policy_number and B.proj_key=" + ctrlPolicy.ProjectKey + "  and B.batch_key=" + ctrlPolicy.BatchKey + " and B.box_number=" + ctrlPolicy.BoxNumber;
                
                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + (int)prmState[j];
                            }
                            else
                                sqlStr = sqlStr + " or a.status=" + (int)prmState[j];
                        }
                    }
                    sqlStr = sqlStr + ") and (B.policy_number in (select policy_number from image_master where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and doc_type='Policy_bond' group by policy_number having count(policy_number) = 1) or B.policy_number not in " +
                             "(select policy_number from image_master where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and doc_type='Policy_bond' group by policy_number)) group by policy_number";

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return policyDs;
        }
        public DataSet GetPolicyCount()
        {
            string sqlStr = null;
            string path = string.Empty;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select B.filename from metadata_entry B  where B.proj_code=" + ctrlPolicy.ProjectKey + " and B.batch_key=" + ctrlPolicy.BatchKey + " ";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            //if (policyDs.Tables[0].Rows.Count > 0)
            //{
            //    path = policyDs.Tables[0].Rows[0]["policy_path"].ToString();
            //}
            return policyDs;
        }
        public string GetPolicyPath()
        {
            string sqlStr = null;
            string path = string.Empty;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select B.policy_path from policy_master B  where B.proj_key=" + ctrlPolicy.ProjectKey + " and B.batch_key=" + ctrlPolicy.BatchKey + " and B.box_number=" + ctrlPolicy.BoxNumber + " and B.policy_number='" + ctrlPolicy.PolicyNumber+"'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            if (policyDs.Tables[0].Rows.Count > 0)
            {
                path = policyDs.Tables[0].Rows[0]["policy_path"].ToString();
            }
            return path;
        }
        public string GetPolicyPath(OdbcTransaction txn)
        {
            string sqlStr = null;
            string path = string.Empty;
            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select B.policy_path from policy_master B  where B.proj_key=" + ctrlPolicy.ProjectKey + " and B.batch_key=" + ctrlPolicy.BatchKey + " and B.box_number=" + ctrlPolicy.BoxNumber + " and B.policy_number='" + ctrlPolicy.PolicyNumber + "'";
                OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                txn.Rollback();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            if (policyDs.Tables[0].Rows.Count > 0)
            {
                path = policyDs.Tables[0].Rows[0]["policy_path"].ToString();
                path.Replace(@"\",@"\\");
            }
            return path;
        }
        public string GetPolicyPath(string policy_no)
        {
            string sqlStr = null;
            string path = string.Empty;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select B.policy_path from policy_master B  where B.policy_number = '"+policy_no +"'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            if (policyDs.Tables[0].Rows.Count > 0)
            {
                path = policyDs.Tables[0].Rows[0]["policy_path"].ToString();
            }
            return path;
        }
        public DataSet GetPolicyPath(string policy_no, int temp)
        {
            string sqlStr = null;
            string path = string.Empty;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select B.proj_key,B.batch_key from policy_master B  where B.policy_number = '" + policy_no + "'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            if (policyDs.Tables[0].Rows.Count > 0)
            {
                // path = policyDs.Tables[0].Rows[0]["policy_path"].ToString();
            }
            return policyDs;
        }
        public DataSet GetDistrict_Active()
        {
            string sql = "Select distinct district_code,trim(district_name) as district_name from district where active = 'Y'";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sql,sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public DataSet GetDistrict()
        {
            string sql = "Select distinct district_code,trim(district_name) as district_name from district";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public bool set_Active(string dis_code,string ro_code)
        {
            bool result = false;
            try
            {
                string qry = "update district set active = 'Y' where district_code = '" + dis_code + "'";
                OdbcCommand cmd = new OdbcCommand(qry, sqlCon);
                cmd.ExecuteNonQuery();

                string qry1 = "update ro_master set active = 'Y' where district_code = '" + dis_code + "' and ro_code = '" + ro_code + "'";
                OdbcCommand cmd1 = new OdbcCommand(qry1, sqlCon);
                cmd1.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public DataSet GetUAT_Result(string do_code,string ro_code,string book,string deed_year, string vol)
        {
            string sqlstr = "select x.district_code, x.ro_code, x.deed_year, x.Book, x.Volume_no, count(*) as Count_of_Deeds, sum(x.count_of_persons) as count_of_persons, sum(x.count_of_properties) as count_of_properties from ( select a.District_Code, a.RO_Code, a.Deed_year, a.Book, a.Volume_No, a.deed_no, count(distinct b.Item_no) as count_of_persons, count(distinct c.Item_no) as count_of_properties from deed_details a, index_of_name b, index_of_property c where a.District_Code = b.District_Code  and a.RO_Code = b.RO_Code and a.Deed_year = b.Deed_year and a.Book = b.Book and a.Deed_no = b.Deed_no and a.District_Code = c.District_Code and a.RO_Code = c.RO_Code and a.Deed_year = c.Deed_year and a.Book = c.Book and a.Deed_no = c.Deed_no and a.district_code = '"+do_code+"' and a.ro_code = '"+ro_code+"' and a.deed_year = '"+deed_year+"' and a.book = '"+book+"' and a.volume_no ='"+vol+"' group by b.District_Code, b.RO_Code, b.Deed_year, b.Book, b.Deed_no, c.Deed_no order by a.deed_year, a.Volume_No, a.Deed_no, count_of_properties desc ) x group by x.district_code, x.ro_code, x.deed_year, x.Book, x.Volume_no order by x.district_code, x.ro_code, x.deed_year, x.Book, convert(x.volume_no, unsigned integer)";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sqlstr, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public DataSet GetUAT_Resultproject(string do_code, string ro_code, string book, string deed_year)
        {
            string sqlstr = "select x.district_code, x.ro_code, x.deed_year, x.Book, x.Volume_no, count(*) as Count_of_Deeds, sum(x.count_of_persons) as count_of_persons, sum(x.count_of_properties) as count_of_properties from ( select a.District_Code, a.RO_Code, a.Deed_year, a.Book, a.Volume_No, a.deed_no, count(distinct b.Item_no) as count_of_persons, count(distinct c.Item_no) as count_of_properties from deed_details a, index_of_name b, index_of_property c where a.District_Code = b.District_Code  and a.RO_Code = b.RO_Code and a.Deed_year = b.Deed_year and a.Book = b.Book and a.Deed_no = b.Deed_no and a.District_Code = c.District_Code and a.RO_Code = c.RO_Code and a.Deed_year = c.Deed_year and a.Book = c.Book and a.Deed_no = c.Deed_no and a.district_code = '" + do_code + "' and a.ro_code = '" + ro_code + "' and a.deed_year = '" + deed_year + "' and a.book = '" + book + "' group by b.District_Code, b.RO_Code, b.Deed_year, b.Book, b.Deed_no, c.Deed_no order by a.deed_year, a.Volume_No, a.Deed_no, count_of_properties desc ) x group by x.district_code, x.ro_code, x.deed_year, x.Book, x.Volume_no order by x.district_code, x.ro_code, x.deed_year, x.Book, convert(x.volume_no, unsigned integer)";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sqlstr, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public DataSet GetProj_Batch(string do_code, string ro_code, string book, string deed_year, string vol)
        {
            string sqlstr = "select distinct proj_key,batch_key from policy_master where do_code = '"+do_code +"' and br_code = '"+ro_code +"' and year = '"+book+"' and deed_year = '"+deed_year+"' and deed_vol = '"+vol+"' and status<>16";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sqlstr, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }

        public DataSet getTotalScanImageCount(string proj_key, string Batch_key)
        {
            string sqlstr = "select count(*) from deed_details a,image_master b where a.proj_key = b.proj_key and a.batch_key=b.batch_key and concat(a.district_code,a.ro_code,a.book,a.deed_year,'[',a.deed_no,']') = b.policy_number and a.proj_key = '" + proj_key + "' and a.batch_key = '" + Batch_key + "' and b.status <> '29'";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sqlstr, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public DataSet getTotalScanImageCountonHold(string proj_key, string Batch_key)
        {
            string sqlstr = "select count(*) from deed_details a,image_master b where a.proj_key = b.proj_key and a.batch_key=b.batch_key and concat(a.district_code,a.ro_code,a.book,a.deed_year,'[',a.deed_no,']') = b.policy_number and a.proj_key = '" + proj_key + "' and a.batch_key = '" + Batch_key + "' and a.hold = 'Y'and b.status <> '29'";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sqlstr, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public DataSet getTotalDeedCountonHold(string proj_key, string Batch_key)
        {
            string sqlstr = "select distinct concat(a.district_code,a.ro_code,a.book,a.deed_year,'[',a.deed_no,']') as deed_no from deed_details a,image_master b where a.proj_key = b.proj_key and a.batch_key=b.batch_key and concat(a.district_code,a.ro_code,a.book,a.deed_year,'[',a.deed_no,']') = b.policy_number and a.proj_key = '"+proj_key +"' and a.batch_key = '"+Batch_key+"' and a.hold = 'Y'";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sqlstr, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public DataSet GetROffice(string districtCode)
        {
            string sql = "Select RO_code,trim(RO_name) as RO_name from RO_MASTER where district_code='" + districtCode + "' and active = 'Y'";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
        public DataSet GetRO(string districtCode)
        {
            string sql = "Select RO_code,trim(RO_name) as RO_name from RO_MASTER where district_code='" + districtCode + "'";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            return ds;
        }
		public int GetPolicyPhotoStatus()
		{
			string sqlStr=null;
			
			DataSet policyDs=new DataSet();
			
			try 
			{
				sqlStr="select photo from policy_master where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(policyDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			
			return Convert.ToInt32(policyDs.Tables[0].Rows[0]["photo"].ToString());
		}
		
		public int GetPolicyPageCount()
		{
			string sqlStr=null;
			int pageCount = 0;
			DataSet policyDs=new DataSet();
			
			try 
			{
				sqlStr="select policy_number,count_of_pages from policy_master  where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'" ;
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(policyDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			
			if(policyDs.Tables[0].Rows.Count>0)
			{
				pageCount =Convert.ToInt32(policyDs.Tables[0].Rows[0]["count_of_pages"].ToString());
			}
			return pageCount;
		}
		
//		public bool GetPolicyPhotoStatus()
//		{
//			string sqlStr=null;
//			string err=null;
//			int pageCount = 0;
//			DataSet policyDs=new DataSet();
//			
//			try 
//			{
//				sqlStr="select policy_number,photo from ih_db.policy_master  where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number=" + ctrlPolicy.PolicyNumber ;
//				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
//				sqlAdap.Fill(policyDs);
//			}
//			catch (Exception ex) 
//			{
//				sqlAdap.Dispose();
//				err=ex.Message;
//			}
//			
//			if(policyDs.Tables[0].Rows.Count>0)
//			{
//				return true;
//			}
//			else
//				return false;
//		}
//		
		public int GetInventoryInExcp()
		{
			string sqlStr=null;
			int excpType = 2;
			DataSet policyDs=new DataSet();
			
			try 
			{
				sqlStr="select exception_type from inventory_in_exception  where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'" ;
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(policyDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			
			if(policyDs.Tables[0].Rows.Count>0)
			{
				excpType =Convert.ToInt32(policyDs.Tables[0].Rows[0]["exception_type"].ToString());
			}
			return excpType;
		}
		
		public int GetLicExpCount()
		{
			string sqlStr=null;
			DataSet policyDs=new DataSet();
			
			try 
			{
				sqlStr="select policy_number from lic_qa_log where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(policyDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			
			return policyDs.Tables[0].Rows.Count;
		}
		
		public override bool TransferValues(udtCmd cmd)
		{
			throw new NotImplementedException();
		}
        public bool RenameDeed(string deed_no,string newdeed_no,OdbcTransaction pTxn,string newPath)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = pTxn;
            bool commitBol = false;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                string deed = newdeed_no.Split(new char[] { '[', ']' })[1];
                sqlStr = "update policy_master set policy_number = '"+newdeed_no +"',deed_no = '"+deed+"',policy_path = '"+newPath.Replace("\\","\\\\") +"' where proj_key="+ ctrlPolicy.ProjectKey +" and batch_key="+ ctrlPolicy.BatchKey +" and "+ ctrlPolicy.BoxNumber +" and policy_number = '"+deed_no+"'";
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }

                sqlStr = "update transaction_log set policy_number = '" + newdeed_no + "' where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and " + ctrlPolicy.BoxNumber + " and policy_number = '" + deed_no + "'";
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int x = sqlCmd.ExecuteNonQuery();

                if (x >= 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }

                sqlStr = "update rawdata set policy_no = '" + newdeed_no + "' where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and " + ctrlPolicy.BoxNumber + " and policy_no = '" + deed_no + "'";
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int y = sqlCmd.ExecuteNonQuery();

                if (y >= 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }

                sqlStr = "UPDATE image_master SET page_index_name = REPLACE(page_index_name, policy_number, '" + newdeed_no + "'),page_name = REPLACE(page_name, policy_number, '" + newdeed_no + "') where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and " + ctrlPolicy.BoxNumber + " and policy_number = '" + deed_no + "'";
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int z = sqlCmd.ExecuteNonQuery();

                if (z >= 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
                sqlStr = "UPDATE image_master SET policy_number = '" + newdeed_no + "' where proj_key="+ ctrlPolicy.ProjectKey +" and batch_key="+ ctrlPolicy.BatchKey +" and "+ ctrlPolicy.BoxNumber +" and policy_number = '"+deed_no+"'";
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int r = sqlCmd.ExecuteNonQuery();

                if (r >= 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
                    //sqlTrans.Commit();
            }
            catch (Exception ex)
            {
                commitBol = false;
                //sqlTrans.Rollback();
                sqlCmd.Dispose();
            }
            return commitBol;

        }


        public bool UpdateFilePrevStatus(eSTATES state, Credentials prmCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update case_file_master" +
                " set status=" + 1 + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_code=" + ctrlPolicy.ProjectKey +
                " and bundle_key=" + ctrlPolicy.BatchKey + " " +
                " and filename='" + ctrlPolicy.PolicyNumber + "' ";

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }

        public bool UpdateFileStatus(eSTATES state, Credentials prmCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update case_file_master" +
                " set status=" + 2 + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_code=" + ctrlPolicy.ProjectKey +
                " and bundle_key=" + ctrlPolicy.BatchKey + " " +
                " and filename='" + ctrlPolicy.PolicyNumber + "' and status<>" + 2;

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }

		public bool UpdateStatus(eSTATES state,Credentials prmCrd)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			
			sqlStr=@"update policy_master" +
                " set status=" + (int)state + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
				" and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
				" and policy_number='" + ctrlPolicy.PolicyNumber + "' and status<>" + (int)eSTATES.POLICY_EXPORTED;
				
			try
			{
				
				sqlTrans=sqlCon.BeginTransaction();
				sqlCmd.Connection = sqlCon;
				sqlCmd.Transaction=sqlTrans;
	            sqlCmd.CommandText = sqlStr;
	            int i = sqlCmd.ExecuteNonQuery();
	            sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}

        public bool UpdateBatchStatus(string Proj_Key, string Batch_Key)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update batch_master" +
                " set status=" + (int)eSTATES.BATCH_EXPORTED + " where " +
                " batch_key=" + Batch_Key + " and proj_code = '" + Proj_Key + "'";

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public int getMaxExportCount(string proj_key, string batch_key)
        {
            string sql = null;
            OdbcTransaction sqlTrans = null;
            OdbcCommand sqlCmd = new OdbcCommand();
            DataSet ds = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            int maxVal = 0;

            sql = "select ifnull(max(runnum),0) from export_log where proj_key = '"+proj_key+ "' and batch_key = '"+batch_key+ "'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sql, sqlCon);
                sqlAdap.Fill(ds);
                if (ds.Tables[0].Rows[0][0] != null || ds.Tables[0].Rows[0][0].ToString() != "")
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        maxVal = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) + 1;
                    }
                    else
                    {
                        maxVal = maxVal + 1;
                    }
                }
                return maxVal;
            }
            catch (Exception ex)
            {
                maxVal = 0;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sql + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return maxVal;
        }
        public bool UpdateExportLog(string proj_key,string batch_key,Credentials crd, int count)
        {
            string sql = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            //sql = "select ifnull(max(runnum),0) from export_log where proj_key = '"+proj_key+ "' and batch_key = '"+batch_key+ "'";
            try
            {
            //    sqlAdap = new OdbcDataAdapter(sql, sqlCon);
            //    sqlAdap.Fill(ds);
            //    if (ds.Tables[0].Rows[0][0] != null || ds.Tables[0].Rows[0][0].ToString() != "")
            //    {
            //        if (Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) > 0)
            //        {
            //            maxVal = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) + 1;
            //        }
            //        else
            //        {
            //            maxVal = maxVal + 1;
            //        }
            //    }
                sql = "insert into export_log values('"+proj_key+"','"+batch_key+"','"+crd.created_dttm +"','"+crd.created_by +"','"+count+"')";
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sql;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sql + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public bool UpdateStatus(eSTATES state, Credentials prmCrd,bool pLock)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update policy_master" +
                " set Locked_uid = null,expires_dttm=null,invalid=0,status=" + (int)state + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber +
                " and policy_number='" + ctrlPolicy.PolicyNumber + "' and status<>" + (int)eSTATES.POLICY_EXPORTED;

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public bool UpdateAllPolicyStatus(eSTATES state,Credentials pCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update policy_master" +
                " set status=" + (int)state + ",modified_by='" + pCrd.created_by + "',modified_dttm='" + pCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber +
                " and (status<>" + (int)eSTATES.POLICY_ON_HOLD + " and status<>" + (int)eSTATES.POLICY_MISSING + ")";

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
public bool UpdateStatus(int pStatus,Credentials prmCrd)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			
			sqlStr=@"update policy_master" +
                " set status=" + pStatus + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
				" and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber +
				" and policy_number='" + ctrlPolicy.PolicyNumber + "' and status<>" + (int)eSTATES.POLICY_EXPORTED;
				
			try
			{
				
				sqlTrans=sqlCon.BeginTransaction();
				sqlCmd.Connection = sqlCon;
				sqlCmd.Transaction=sqlTrans;
	            sqlCmd.CommandText = sqlStr;
	            int i = sqlCmd.ExecuteNonQuery();
	            sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
        public bool UpdateStatus(eSTATES state, Credentials prmCrd,OdbcTransaction prmTrans)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update policy_master" +
                " set status=" + (int)state + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber +
                " and policy_number='" + ctrlPolicy.PolicyNumber+"'";

            try
            {

                sqlTrans = prmTrans;
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }

        public bool DeleteTransLogEntry()
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"delete from transaction_log" +
                 " where proj_key=" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey + " and box_number=" + ctrlPolicy.BoxNumber;

            try
            {
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
       
        public bool ReadyForUAT(string proj_key,string batch_key)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                sqlStr = "update box_master set status = '7' where proj_key ='" + proj_key + "'and batch_key = '" + batch_key + "' and box_number = '1'";
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                commitBol = true;
                
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                            }
            return commitBol;
        }
        public bool ReadyForUAT(string runnum)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                sqlStr = "update batch_master set status = '7' where run_no = '"+runnum+"'";
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                commitBol = true;

            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
            }
            return commitBol;
        }

        public bool UpdateTransactionLog(eSTATES state, Credentials prmCrd, OdbcTransaction sqlTrans=null)
        {
            string sqlStr = null;
           // OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            bool flagTrans = false;
            switch (state)
            {
                case eSTATES.POLICY_SCANNED:
                    {
                        sqlStr = @"insert into transaction_log (proj_key,Batch_Key,Box_number,Policy_number,Scanned_user,scanned_dttm,fqc_user)" +
                        " values(" + ctrlPolicy.ProjectKey + "," + ctrlPolicy.BatchKey + ",'" + ctrlPolicy.BoxNumber + "','" + ctrlPolicy.PolicyNumber + "','" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','')";
                        break;
                    }
                case eSTATES.POLICY_QC:
                    {
                        sqlStr = @"update transaction_log" +
                        " set QC_User='" + prmCrd.created_by + "',Qc_DTTM='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
                        " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
                        " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
                        break;
                    }
                case eSTATES.POLICY_INDEXED:
                    {
                        sqlStr = @"update transaction_log" +
                        " set Index_User='" + prmCrd.created_by + "',Index_DTTM='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
                        " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
                        " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
                        break;
                    }
                case eSTATES.POLICY_FQC:
                    {
                        sqlStr = @"update transaction_log" +
                        " set Fqc_User='" + prmCrd.created_by + "',fqc_DTTM='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
                        " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
                        " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
                        break;
                    }
            }
            try
            {
                if (sqlTrans == null)
                {
                    sqlTrans = sqlCon.BeginTransaction();
                    flagTrans = true;
                }
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                if (flagTrans)
                {
                    sqlTrans.Commit();
                }
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n" + "Wfe State--" + Convert.ToString(Convert.ToInt32(state)) + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }













		public bool SavePolicyPageCount(int prmPageCount,int prmPhoto,eSTATES prmstatus,Credentials prmCrd)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			
			sqlStr=@"update policy_master" +
                " set count_of_pages=" + prmPageCount + ",photo=" + prmPhoto + ",status=" + (int)prmstatus + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlPolicy.ProjectKey +
				" and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
				" and policy_number='" + ctrlPolicy.PolicyNumber+"'" ;
				
			try
			{
				
				sqlTrans=sqlCon.BeginTransaction();
				sqlCmd.Connection = sqlCon;
				sqlCmd.Transaction=sqlTrans;
	            sqlCmd.CommandText = sqlStr;
	            sqlCmd.ExecuteNonQuery();
	            sqlTrans.Commit();
	            commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}

        public bool DeleteAllPage()
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"delete from image_master where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber + "' and policy_number='" + ctrlPolicy.PolicyNumber+"'";

            try
            {
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i= sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
		
		public bool UpdateScanDetails(string prmScanDate,string prmScanUploadFlag)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			
			sqlStr=@"update policy_master" +
				" set Scan_upload_flag='" + prmScanUploadFlag + "',scanned_date='" + prmScanDate + "' where proj_key=" + ctrlPolicy.ProjectKey +
				" and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
				" and policy_number='" + ctrlPolicy.PolicyNumber+"'";
				
			try
			{
				
				sqlTrans=sqlCon.BeginTransaction();
				sqlCmd.Connection = sqlCon;
				sqlCmd.Transaction=sqlTrans;
	            sqlCmd.CommandText = sqlStr;
	            sqlCmd.ExecuteNonQuery();
	            sqlTrans.Commit();
	            commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
		public bool UpdateQaPolicyException1(NovaNet.Utils.Credentials prmCrd,policyException udtException)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			try
			{
				sqlTrans = sqlCon.BeginTransaction();
					sqlCmd.Connection = sqlCon;
					sqlCmd.Transaction=sqlTrans;

                    sqlStr = @"update lic_qa_log set missing_img_exp=" + udtException.missing_img_exp + ",crop_clean_exp=" + udtException.crop_clean_exp + ",poor_scan_exp=" + udtException.poor_scan_exp + ",wrong_indexing_exp=" + udtException.wrong_indexing_exp + ",linked_policy_exp=" + udtException.linked_policy_exp + ",decision_misd_exp=" + udtException.decision_misd_exp + ",extra_page_exp=" + udtException.extra_page_exp + ",rearrange_exp=" + udtException.rearrange_exp + ",other_exp=" + udtException.other_exp + ",move_to_respective_policy_exp=" + udtException.move_to_respective_policy_exp + ",modified_by_phase1='" + prmCrd.created_by + "',modified_dttm_phase1='" + prmCrd.created_dttm + "',SOLVED=" + udtException.solved + ",comments='" + udtException.comments + "' where proj_key= " + ctrlPolicy.ProjectKey + " and box_number= " + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber + "' and batch_key=" + ctrlPolicy.BatchKey ;
	                sqlCmd.CommandText = sqlStr;
	                sqlCmd.ExecuteNonQuery();
	                
	                sqlTrans.Commit();
	                commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
        public bool UpdateQaPolicyException2(NovaNet.Utils.Credentials prmCrd, policyException udtException)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;

                sqlStr = @"update lic_qa_log set missing_img_exp=" + udtException.missing_img_exp + ",crop_clean_exp=" + udtException.crop_clean_exp + ",poor_scan_exp=" + udtException.poor_scan_exp + ",wrong_indexing_exp=" + udtException.wrong_indexing_exp + ",linked_policy_exp=" + udtException.linked_policy_exp + ",decision_misd_exp=" + udtException.decision_misd_exp + ",extra_page_exp=" + udtException.extra_page_exp + ",rearrange_exp=" + udtException.rearrange_exp + ",other_exp=" + udtException.other_exp + ",move_to_respective_policy_exp=" + udtException.move_to_respective_policy_exp + ",modified_by_phase2='" + prmCrd.created_by + "',modified_dttm_phase2='" + prmCrd.created_dttm + "',SOLVED=" + udtException.solved + ",comments='" + udtException.comments + "' where proj_key= " + ctrlPolicy.ProjectKey + " and box_number= " + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber + "' and batch_key=" + ctrlPolicy.BatchKey;
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public bool UnLockPolicy()
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;

                sqlStr = @"update policy_master set Locked_uid = null,expires_dttm=null,invalid=0 where proj_key= " + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number= '" + ctrlPolicy.BoxNumber + "' and policy_number='" + ctrlPolicy.PolicyNumber+"'";
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public string deedCheck(string deedNo)
        {
            string sql = null;
            string deedStatus = null;
            OdbcCommand sqlCmd = new OdbcCommand();
            DataSet ds = new DataSet();
            try
            {
                sqlCmd.Connection = sqlCon;
                sql = "select status from policy_master where policy_number ='"+deedNo+"'";
                sqlCmd.CommandText = sql;
                sqlAdap = new OdbcDataAdapter(sqlCmd);
                sqlAdap.Fill(ds);
                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                deedStatus = ds.Tables[0].Rows[0]["status"].ToString();
                return deedStatus;
            }
            else
            { return string.Empty; }
            
        }
        public bool LockPolicy(Credentials pCrd,OdbcTransaction pTrns)
        {
            string sqlStr = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            DataSet ds = new DataSet();
            long time=0;
            try
            {
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = pTrns;
                sqlStr = "select SYSVALUES from SYSCONFIG  where SYSKEYS='LOCK_EXPIRES_TIME'";
                sqlCmd.CommandText = sqlStr;
                sqlAdap = new OdbcDataAdapter(sqlCmd);
                sqlAdap.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    time = Convert.ToInt32(ds.Tables[0].Rows[0]["SYSVALUES"].ToString());
                }

                sqlStr = @"update policy_master set expires_dttm = Now()+interval " + time + " SECOND,Locked_uid ='" + pCrd.created_by + "',invalid=1 where proj_key= " + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number= " + ctrlPolicy.BoxNumber + " and policy_number='" + ctrlPolicy.PolicyNumber+"'";
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
		public bool InitiateQaPolicyException1(NovaNet.Utils.Credentials prmCrd)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			try
			{
					sqlTrans = sqlCon.BeginTransaction();
					sqlCmd.Connection = sqlCon;
					sqlCmd.Transaction=sqlTrans;
	                
					sqlStr= @"insert into lic_qa_log (proj_key,box_number,policy_number,batch_key,created_by_phase1,created_dttm_phase1) values(" + ctrlPolicy.ProjectKey + ",'" + ctrlPolicy.BoxNumber + "','" + ctrlPolicy.PolicyNumber + "'," + ctrlPolicy.BatchKey + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm +"')";
	                sqlCmd.CommandText = sqlStr;
	                sqlCmd.ExecuteNonQuery();
	                
	                sqlTrans.Commit();
	                commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
        public bool InitiateQaPolicyException1(NovaNet.Utils.Credentials prmCrd, string policy)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;

                sqlStr = @"insert into lic_qa_log (proj_key,box_number,policy_number,batch_key,created_by_phase1,created_dttm_phase1) values(" + ctrlPolicy.ProjectKey + ",'" + ctrlPolicy.BoxNumber + "','" + policy + "'," + ctrlPolicy.BatchKey + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "')";
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public bool InitiateQaPolicyException2(NovaNet.Utils.Credentials prmCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;

                sqlStr = @"insert into lic_qa_log (proj_key,box_number,policy_number,batch_key,created_by_phase2,created_dttm_phase2) values(" + ctrlPolicy.ProjectKey + ",'" + ctrlPolicy.BoxNumber + "','" + ctrlPolicy.PolicyNumber + "'," + ctrlPolicy.BatchKey + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "')";
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public bool InitiateQaPolicyException2(NovaNet.Utils.Credentials prmCrd,string policy)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            try
            {
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;

                sqlStr = @"insert into lic_qa_log (proj_key,box_number,policy_number,batch_key,created_by_phase2,created_dttm_phase2) values(" + ctrlPolicy.ProjectKey + ",'" + ctrlPolicy.BoxNumber + "','" + policy + "'," + ctrlPolicy.BatchKey + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "')";
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
        public bool SaveInventoryInException(NovaNet.Utils.Credentials prmCrd,int prmInvtInExcpType)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			try
			{
				sqlStr=@"delete from inventory_in_exception where proj_key=" +
					ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber + "' and policy_number='" + ctrlPolicy.PolicyNumber+"'";
					sqlTrans=sqlCon.BeginTransaction();
					sqlCmd.Connection = sqlCon;
					sqlCmd.Transaction=sqlTrans;
	                sqlCmd.CommandText = sqlStr;
	                sqlCmd.ExecuteNonQuery();
	                
	                sqlStr=@"insert into inventory_in_exception(proj_key,batch_key,box_number, policy_number,exception_type,created_by,created_dttm) values(" +
					ctrlPolicy.ProjectKey + "," + ctrlPolicy.BatchKey + ",'" + ctrlPolicy.BoxNumber + "','" + ctrlPolicy.PolicyNumber + "'," + prmInvtInExcpType + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "')";
	                sqlCmd.CommandText = sqlStr;
	                sqlCmd.ExecuteNonQuery();
	                
	                sqlTrans.Commit();
	                commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
		
		public bool SavePolicyPageCountException(NovaNet.Utils.Credentials prmCrd,int prmPageToBeScanned,int prmPageScanned)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			try
			{
	                sqlStr=@"insert into policy_page_count_exception_log(Proj_key,Btach_key,box_number,policy_number,tot_page_to_be_scanned,page_scanned,created_by,created_dttm) values(" +
					ctrlPolicy.ProjectKey + "," + ctrlPolicy.BatchKey + "," + ctrlPolicy.BatchKey + ",'" + ctrlPolicy.PolicyNumber + "'," + prmPageToBeScanned +
					"," + prmPageScanned + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "')";
	                sqlTrans=sqlCon.BeginTransaction();
					sqlCmd.Connection = sqlCon;
					sqlCmd.Transaction=sqlTrans;
	                sqlCmd.CommandText = sqlStr;
	                sqlCmd.ExecuteNonQuery();
	                
	                sqlTrans.Commit();
	                commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
		
		public bool QaExceptionStatus(int prmStatus,int prmExpStatus)
		{
			string sqlStr=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			OdbcTransaction prmTrans;
				
			try
			{
                prmTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = prmTrans;
                
                sqlStr = @"update lic_qa_log" +
                " set solved=" + prmStatus + " where proj_key=" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
                " and policy_number='" + ctrlPolicy.PolicyNumber + "' and solved <>" + 7;

				
	            sqlCmd.CommandText = sqlStr;
	            sqlCmd.ExecuteNonQuery();

                sqlStr = @"update lic_qa_log" +
                " set qa_status=" + prmExpStatus + " where proj_key=" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber +"'"+
                " and policy_number='" + ctrlPolicy.PolicyNumber+"'";


                sqlCmd.CommandText = sqlStr;
                int i=sqlCmd.ExecuteNonQuery();

	            prmTrans.Commit();
	            commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
		public bool UpdatePolicyDetails(string prmName,string prmDob,string prmCommDt)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			
			sqlStr=@"update rawdata" +
				" set name_of_policyholder='" + prmName + "',date_of_birth='" + prmDob + "',date_of_commencement='" + prmCommDt + "' where proj_key=" + ctrlPolicy.ProjectKey +
				" and batch_key=" + ctrlPolicy.BatchKey +
				" and policy_no='" + ctrlPolicy.PolicyNumber+"'" ;
				
			try
			{
				
				sqlTrans=sqlCon.BeginTransaction();
				sqlCmd.Connection = sqlCon;
				sqlCmd.Transaction=sqlTrans;
	            sqlCmd.CommandText = sqlStr;
	            sqlCmd.ExecuteNonQuery();
	            sqlTrans.Commit();
	            commitBol=true;
			}
			catch(Exception ex)
			{
				commitBol=false;
				sqlTrans.Rollback();
				sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
        public DataSet GetFilteredPolicyonHold(string pDocType)
        {
            string sqlstr = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sqlstr = "select distinct a.policy_number " +
                         "from policy_master a,deed_details b " +
                         "where a.proj_key = b.proj_key and a.batch_key = b.batch_key " +
                         "and a.deed_no=b.deed_no " +
                         "and a.do_code = b.district_code " +
                         "and a.br_code= b.ro_code " +
                         "and a.year = b.book " +
                         "and a.deed_year = b.deed_year " +
                         "and b.proj_key = '" + ctrlPolicy.ProjectKey + "' " +
                         "and b.batch_key = '" + ctrlPolicy.BatchKey + "' " +
                         "and b.hold='Y' order by Convert(a.page_from,signed integer)";
                sqlAdap = new OdbcDataAdapter(sqlstr, sqlCon);
                sqlAdap.Fill(ds); 
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
            }
            return ds;
        }
        public DataSet GetFilteredPolicyAllFile(string pDocType, eSTATES[] state)
        {
            string sqlstr = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sqlstr = "select distinct a.case_file_no " +
                         "from case_file_master" +
                         "where proj_code = '" + ctrlPolicy.ProjectKey + "' " +
                         "and bundle_key = '" + ctrlPolicy.BatchKey + "' and status = '2' order by item_no";
                string strQuery = sqlstr;
                
                sqlAdap = new OdbcDataAdapter(strQuery, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
            }
            return ds;
        }
        public DataSet GetFilteredPolicyAll(string pDocType,eSTATES[] state)
        {
            string sqlstr = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sqlstr = "select distinct a.policy_number " +
                         "from policy_master a,deed_details b " +
                         "where a.proj_key = b.proj_key and a.batch_key = b.batch_key " +
                         "and a.deed_no=b.deed_no " +
                         "and a.do_code = b.district_code " +
                         "and a.br_code= b.ro_code " +
                         "and a.year = b.book " +
                         "and a.deed_year = b.deed_year " +
                         "and b.proj_key = '" + ctrlPolicy.ProjectKey + "' " +
                         "and b.batch_key = '" + ctrlPolicy.BatchKey + "'";
                string strQuery = sqlstr;
                if (state.Length != 0)
                {
                    for (int j = 0; j < state.Length; j++)
                    {
                        if ((int)state[j] != 0)
                        {
                            if (j == 0)
                            {
                                strQuery = strQuery + " and (a.status=" + (int)state[j];
                            }
                            else
                                strQuery = strQuery + " or a.status=" + (int)state[j];
                        }
                    }
                    strQuery = strQuery + ") order by Convert(a.page_from,signed integer)";
                }
                sqlAdap = new OdbcDataAdapter(strQuery, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
            }
            return ds;
        }
        public DataSet GetFilteredPolicyTrans(string pDocType,eSTATES[] state)
        {
            string sqlstr = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sqlstr = "select distinct a.policy_number from policy_master a,deed_details c where " +
                         "a.do_code = c.district_code " +
                         "and a.br_code = c.ro_code " +
                         "and a.year = c.book " +
                         "and a.deed_year = c.deed_year " +
                         "and a.deed_no = c.deed_no " +
                         "and c.proj_key = '"+ctrlPolicy.ProjectKey+"' " +
                         "and c.batch_key = '"+ctrlPolicy.BatchKey+"' " +
                         "and c.tran_maj_code ='" + pDocType + "'";
                string strQuery = sqlstr;
                         if (state.Length != 0)
                            {
                                for (int j = 0; j < state.Length; j++)
                                {
                                    if ((int)state[j] != 0)
                                    {
                                        if (j == 0)
                                        {
                                            strQuery = strQuery + " and (a.status=" + (int)state[j];
                                        }
                                        else
                                            strQuery = strQuery + " or a.status=" + (int)state[j];
                                    }
                                }
                                strQuery = strQuery + ") order by Convert(a.page_from,signed integer)";
                            }
                         sqlAdap = new OdbcDataAdapter(strQuery, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
            }
            return ds;
        }
        public DataSet GetFilteredPolicyProperty(string pDocType,eSTATES[] state)
        {
            string sqlstr = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sqlstr = "select distinct a.policy_number from policy_master a,deed_details c,index_of_property b where " +
                         "a.do_code = c.district_code " +
                         "and a.br_code = c.ro_code " +
                         "and a.year = c.book " +
                         "and a.deed_year = c.deed_year " +
                         "and a.deed_no = c.deed_no " +
                         "and c.proj_key = '"+ctrlPolicy.ProjectKey+"' " +
                         "and c.batch_key = '"+ctrlPolicy.BatchKey+"' " +
                         "and c.district_code = b.district_code " +
                         "and c.ro_code = b.ro_code " +
                         "and c.book = b.book " +
                         "and c.deed_year = b.deed_year " +
                         "and c.deed_no = b.deed_no " +
                         "and b.property_type ='" + pDocType + "'";
                string strQuery = sqlstr;
                         if (state.Length != 0)
                            {
                                for (int j = 0; j < state.Length; j++)
                                {
                                    if ((int)state[j] != 0)
                                    {
                                        if (j == 0)
                                        {
                                            strQuery = strQuery + " and (a.status=" + (int)state[j];
                                        }
                                        else
                                            strQuery = strQuery + " or a.status=" + (int)state[j];
                                    }
                                }
                                strQuery = strQuery + ") order by Convert(a.page_from,signed integer)";
                            }

                         sqlAdap = new OdbcDataAdapter(strQuery, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
            }
            return ds;
        }
        public DataSet GetFilteredPolicyIndex_of_name(string pOpartor,int pCount,eSTATES[] state)
        {
            string sqlstr = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sqlstr = "select distinct policy_number,count(*) from policy_master a,index_of_name b where " +
                         "a.do_code = b.district_code " +
                         "and a.br_code = b.ro_code " +
                         "and a.year = b.book " +
                         "and a.deed_year = b.deed_year " +
                         "and a.deed_no = b.deed_no " +
                         "and a.proj_key = '" + ctrlPolicy.ProjectKey + "' " +
                         "and a.batch_key = '" + ctrlPolicy.BatchKey + "' ";
                         
                string strQuery = sqlstr;
                if (state.Length != 0)
                {
                    for (int j = 0; j < state.Length; j++)
                    {
                        if ((int)state[j] != 0)
                        {
                            if (j == 0)
                            {
                                strQuery = strQuery + " and (a.status=" + (int)state[j];
                            }
                            else
                                strQuery = strQuery + " or a.status=" + (int)state[j];
                        }
                    }
                    strQuery = strQuery + ") group by policy_number having count(*) " + pOpartor + " '" + pCount + "' order by Convert(a.page_from,signed integer)";
                }
                sqlAdap = new OdbcDataAdapter(strQuery, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
            }
            return ds;
        }
        public DataSet GetFilteredPolicyIndex_of_property(string pOpartor, int pCount,eSTATES[] state)
        {
            string sqlstr = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sqlstr = "select policy_number,count(*) from policy_master a,index_of_property b where " +
                         "a.do_code = b.district_code " +
                         "and a.br_code = b.ro_code " +
                         "and a.year = b.book " +
                         "and a.deed_year = b.deed_year " +
                         "and a.deed_no = b.deed_no " +
                         "and a.proj_key = '" + ctrlPolicy.ProjectKey + "' " +
                         "and a.batch_key = '" + ctrlPolicy.BatchKey + "' ";
                         
                string strQuery = sqlstr;
                if (state.Length != 0)
                {
                    for (int j = 0; j < state.Length; j++)
                    {
                        if ((int)state[j] != 0)
                        {
                            if (j == 0)
                            {
                                strQuery = strQuery + " and (a.status=" + (int)state[j];
                            }
                            else
                                strQuery = strQuery + " or a.status=" + (int)state[j];
                        }
                    }
                    strQuery = strQuery + ") group by policy_number having count(*) " + pOpartor + " '" + pCount + "' order by Convert(a.page_from,signed integer)";
                }
                sqlAdap = new OdbcDataAdapter(strQuery, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
            }
            return ds;
        }
        public DataSet getPropertyType()
        {
            DataSet ds = new DataSet();
            string sql = "select apartment_type_code,trim(description) as description from property_type";
            sqlAdap = new OdbcDataAdapter(sql, sqlCon);
            sqlAdap.Fill(ds);
            return ds;
        }
        public DataSet getTransactionTypeMajor()
        {
            DataSet ds = new DataSet();
            string sql = "select tran_maj_code,trim(tran_maj_name) as tran_maj_name from party ";
            sqlAdap = new OdbcDataAdapter(sql, sqlCon);
            sqlAdap.Fill(ds);
            return ds;
        }
        public DataSet GetFilteredPolicy(string pDocType, string pFilterSign, int pCount,eSTATES[] pState)
        {
            string sqlStr = null;
            string path = string.Empty;
            string stateSql = string.Empty;
            DataSet policyDs = new DataSet();

            try
            {
                for (int j = 0; j < pState.Length; j++)
                {
                    if ((int)pState[j] != 0)
                    {
                        if (j == 0)
                        {
                            stateSql = stateSql + "(A.status=" + (int)pState[j];
                        }
                        else
                            stateSql = stateSql + " or A.status=" + (int)pState[j];
                    }
                }
                stateSql = stateSql + ")";
                if ((pCount == 0) && (pFilterSign == "="))
                {
                    sqlStr = "select distinct A.policy_number from policy_master A where " + stateSql + " and A.proj_key=" + ctrlPolicy.ProjectKey +
                                " and A.batch_key=" + ctrlPolicy.BatchKey + " and A.box_number='" + ctrlPolicy.BoxNumber + "' and A.policy_number not in " +
                               "(" +
                                "select policy_number from image_master where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber + "' and doc_type = '" + pDocType + "' and status<>29 group by policy_number, doc_type )";
                }
                else
                {
                    //sqlStr = "select A.policy_number from image_master A,policy_master B " +
                    //        "where A.proj_key = B.proj_key and A.batch_key = B.batch_key and A.box_number = B.box_number and A.policy_number = B.policy_number and " + stateSql +
                    //        " and A.proj_key=" + ctrlPolicy.ProjectKey + " and A.batch_key=" + ctrlPolicy.BatchKey + " and A.box_number=" + ctrlPolicy.BoxNumber + " and A.status <> 29 and A.doc_type='" + pDocType + "' group by A.policy_number, A.doc_type having count(A.policy_number) " + pFilterSign + pCount;    
                    //if (pFilterSign == "<")
                    //{
                    //    pFilterSign = ">";
                    //}
                    //else
                    //{
                    //    pFilterSign = "<";
                    //}
                        sqlStr = "select policy_number from policy_master where policy_number in( " +
                            "select A.policy_number from image_master A " +
                            "where  A.proj_key=" + ctrlPolicy.ProjectKey + " and A.batch_key=" + ctrlPolicy.BatchKey + " and A.box_number=" + ctrlPolicy.BoxNumber + " and A.status <> 29 " +
                            " and A.doc_type='" + pDocType + "' group by A.policy_number, A.doc_type having count(A.policy_number) " + pFilterSign + pCount + ") and " +
                            stateLog + " proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number='" + ctrlPolicy.BoxNumber+"'";
                    
                }
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return policyDs;
        }
        public DataSet GetRectifiedPolicyCount()
        {
            string sqlStr = null;

            DataSet expDs = new DataSet();

            try
            {
                sqlStr = "select policy_number from lic_qa_log where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and solved=7";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(expDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }

            return expDs;
        }
        public bool DeleteFromInventory()
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = "delete FROM inventory_in_exception where policy_number not in(select policy_number from policy_master where (status=36 or status=37) and batch_key='" + ctrlPolicy.BatchKey + "' and box_number='" + ctrlPolicy.BoxNumber + "')";

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return commitBol;
        }
		public string GetBatchSerial()
		{
			string sqlStr=null;
			string batchName=null;
			
			DataSet batchDs=new DataSet();
			
			try 
			{
				sqlStr="select distinct batch_serial from rawdata where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey;
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(batchDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
                exMailLog.Log(ex);
			}
			if(batchDs.Tables[0].Rows.Count>0)
			{
				batchName=batchDs.Tables[0].Rows[0]["batch_serial"].ToString();
			}
			else
				batchName=string.Empty;
			return batchName;
		}
        public string GetBatchSerial(wfeBox pBox)
        {
            string sqlStr = null;
            string batchName = null;

            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select distinct batch_serial from rawdata where proj_key=" + pBox.ctrlBox.ProjectCode + " and batch_key=" + pBox.ctrlBox.BatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                exMailLog.Log(ex);
            }
            if (batchDs.Tables[0].Rows.Count > 0)
            {
                batchName = batchDs.Tables[0].Rows[0]["batch_serial"].ToString();
            }
            else
                batchName = string.Empty;
            return batchName;
        }
        public bool UpdateImageSrl(ArrayList arr)
        {

                String sqlStr = string.Empty;
                int tmp;
                OdbcTransaction sqlTrans = null;
                OdbcCommand sqlCmd = new OdbcCommand();
                try
                {
                    sqlTrans = sqlCon.BeginTransaction();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.Transaction = sqlTrans;
                    for (int i = 0; i < arr.Count; i++)
                    {
                        tmp = i + 1;
                        sqlStr = "Update image_master set page_index_name = '" + arr[i].ToString() + "', serial_no = " + tmp + " where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number = '" + ctrlPolicy.BoxNumber + "' and policy_number = '" + ctrlPolicy.PolicyNumber + "' and page_name = '" + arr[i].ToString() + "'";

                        sqlCmd.CommandText = sqlStr;
                        sqlCmd.ExecuteNonQuery();
                    }
                    sqlTrans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    return false;
                }
            }
        
//		public bool UpdatePolicy(string prmPolicyNo)
//		{
//			
//		}
//		private bool QueryPolicyStatus(string prmPolicyNo)
//		{
//			
//		}
		public bool UpdateSrl(ArrayList arr)
        {
            String sqlStr=string.Empty;
            int tmp;
            OdbcTransaction sqlTrans=null;
            OdbcCommand sqlCmd=new OdbcCommand();
            int lengthOfPageName = 0;
            try
            {
            sqlTrans=sqlCon.BeginTransaction();
            sqlCmd.Connection = sqlCon;
            sqlCmd.Transaction=sqlTrans;
            lengthOfPageName = Convert.ToInt32(ctrlPolicy.PolicyNumber.ToString().Length) + 12;    
                for (int i=0; i < arr.Count; i++)
                {
                    tmp = i+1;
                    if (arr[i].ToString().Trim().Length > lengthOfPageName)
                    {
                        sqlStr = "Update image_master set serial_no = " + tmp + " where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number = '" + ctrlPolicy.BoxNumber + "' and policy_number = '" + ctrlPolicy.PolicyNumber + "' and page_name = '" + arr[i].ToString().Substring(0, arr[i].ToString().IndexOf('-')) + "'";
                    }
                    else
                    {
                        sqlStr = "Update image_master set serial_no = " + tmp + " where proj_key=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and box_number = '" + ctrlPolicy.BoxNumber + "' and policy_number = '" + ctrlPolicy.PolicyNumber + "' and page_name = '" + arr[i].ToString() + "'";
                    }
                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();
                }
            sqlTrans.Commit();
            }
            catch(Exception ex)
            {
                sqlTrans.Rollback();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
                return false;
            }
            return true;
        }

        

        public bool UpdateRunNoBatchStatus(OdbcTransaction pTxn, string proj_key,string batch_key,string Run_No)
        {
            try
            {
                OdbcTransaction sqlTrans = null;
                OdbcCommand sqlCmdPolicy = new OdbcCommand();
                OdbcCommand sqlRawdata = new OdbcCommand();
                string sqlStr = @"update batch_master set status='"+(int)eSTATES.BATCH_SUBMITTED+"',run_no='"+Run_No+"' where " 
                                 + "proj_code='"+proj_key+"' and batch_key='"+batch_key+"'";
                sqlTrans = pTxn;
                sqlCmdPolicy.Connection = sqlCon;
                sqlCmdPolicy.Transaction = sqlTrans;
                sqlCmdPolicy.CommandText = sqlStr;
                sqlCmdPolicy.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateRunPolicyStatus(OdbcTransaction pTxn, string proj_key, string batch_key, string Run_No)
        {
            try
            {
                OdbcTransaction sqlTrans = null;
                OdbcCommand sqlCmdPolicy = new OdbcCommand();
                OdbcCommand sqlRawdata = new OdbcCommand();
                string sqlStr = @"update policy_master set status='" + (int)eSTATES.POLICY_SUBMITTED + "',run_no='" + Run_No + "' where "
                                 + "proj_key='" + proj_key + "' and batch_key='" + batch_key + "'";
                sqlTrans = pTxn;
                sqlCmdPolicy.Connection = sqlCon;
                sqlCmdPolicy.Transaction = sqlTrans;
                sqlCmdPolicy.CommandText = sqlStr;
                sqlCmdPolicy.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string GetDtRo()
        {
            string sqlStr = null;
            string DtRo = null;

            DataSet DtRoDs = new DataSet();

            try
            {
                sqlStr = "select District_Code,RO_Code from ro_master where Active='Y'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(DtRoDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                exMailLog.Log(ex);
            }
            if (DtRoDs.Tables[0].Rows.Count > 0)
            {
                DtRo = DtRoDs.Tables[0].Rows[0]["District_Code"].ToString() + DtRoDs.Tables[0].Rows[0]["RO_Code"].ToString();
            }
            else
                DtRo = string.Empty;
            return DtRo;
        }
        public string GetCdKey()
        {
            string sqlStr = null;
            string CdKey = null;

            DataSet CDkeyDs = new DataSet();

            try
            {
                sqlStr = "select sysValues from sysconfig where sysKeys='CD_NO'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(CDkeyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                exMailLog.Log(ex);
            }
            if (CDkeyDs.Tables[0].Rows.Count > 0)
            {
                CdKey = CDkeyDs.Tables[0].Rows[0]["sysValues"].ToString();
            }
            else
                CdKey = string.Empty;
            return CdKey;
        }

		
	}

}

		
	

