/*
 * Created by SharpDevelop.
 * User: user
 * Date: 3/21/2009
 * Time: 11:34 AM
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
	/// Description of wfeImage.
	/// </summary>
	public class CtrlImage: NovaNet.wfe.wItemControl
	{
		private int proj_Key;
  		private int batch_key;
  		private string	box_number;
  		private string policy_number;
		private string imageName;
		private string docType;
		
		public CtrlImage(int projKey, int batchKey,string boxNumber,string policyNumber,string prmImageName,string prmDocType)
		{
			proj_Key=projKey;
			batch_key=batchKey;
			box_number=boxNumber;
			policy_number=policyNumber;
			imageName =prmImageName;
			docType=prmDocType;
		}

        public CtrlImage(int projKey, int batchKey, string policyNumber, string prmImageName, string prmDocType)
        {
            proj_Key = projKey;
            batch_key = batchKey;
            policy_number = policyNumber;
            imageName = prmImageName;
            docType = prmDocType;
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
		public string ImageName
		{
			get
			{
				return imageName;
			}
		}
		public string DocType
		{
			get
			{
				return docType;
			}
		}
	}
	/// <summary>
	/// Description of wfePolicy.
	/// </summary>
	public class wfeImage: wItem, StateData
	{
		OdbcConnection sqlCon;
        MemoryStream stateLog;
        byte[] tmpWrite;
		public CtrlImage ctrlImage=null;
		wItemControl wic=null;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);	
		public wfeImage(OdbcConnection prmCon): base(prmCon, NovaNet.Utils.Constants._ADDING)
		{
			sqlCon=prmCon;
            exMailLog.SetNextLogger(exTxtLog);
            
		}
		
		public wfeImage(OdbcConnection prmCon, CtrlImage prmCtrl): base(prmCon, NovaNet.Utils.Constants._EDITING)
		{
			sqlCon=prmCon;
			ctrlImage = prmCtrl;
            exMailLog.SetNextLogger(exTxtLog);
            //LoadValuesFromDB();
		}
		public override bool Commit()
		{
			throw new NotImplementedException();
		}
		public override bool KeyCheck(string prmValue)
		{
			throw new NotImplementedException();
		}
		public override udtCmd LoadValuesFromDB()
		{
			throw new NotImplementedException();
		}
		public override bool TransferValues(udtCmd cmd)
		{
			throw new NotImplementedException();
		}
        public bool UpdateStatusFQC(eSTATES state, Credentials prmCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            int status;
            OdbcCommand sqlCmd = new OdbcCommand();
            //2. Collect the state of the image
            status = GetImageStatus();
            //3. Check whether state (parameter) is to export and current state of the image is rescanned_but_not_indexed
            if (status == (int)eSTATES.PAGE_RESCANNED_NOT_INDEXED && state == eSTATES.PAGE_EXPORTED)
            {
                return false;
            }
            else
            {
                sqlStr = @"update image_master" +
                    " set status=" + (int)state + " ,modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlImage.ProjectKey +
                    " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                    " and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + ctrlImage.ImageName + "' and (status <> " + (int)eSTATES.PAGE_EXPORTED + ")";
            }
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
            int status;
			OdbcCommand sqlCmd=new OdbcCommand();
            //2. Collect the state of the image
            status = GetImageStatus();
            //3. Check whether state (parameter) is to export and current state of the image is rescanned_but_not_indexed
            if (status ==(int) eSTATES.PAGE_RESCANNED_NOT_INDEXED && state == eSTATES.PAGE_EXPORTED)
            {
                return false;
            }
            else
            {
                sqlStr = @"update image_master" +
                    " set status=" + (int)state + " ,page_index_name='" + ctrlImage.ImageName + "',doc_type='',modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlImage.ProjectKey +
                    " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                    " and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + ctrlImage.ImageName + "' and (status <> " + (int)eSTATES.PAGE_EXPORTED + ")";
            }
			try
			{
				
				sqlTrans=sqlCon.BeginTransaction();
				sqlCmd.Connection = sqlCon;
				sqlCmd.Transaction=sqlTrans;
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
        MemoryStream StateData.StateLog()
        {
            return stateLog;
        }
        /// <summary>
        /// Update image status with transaction eanbled
        /// </summary>
        /// <param name="state"></param>
        /// <param name="prmCrd"></param>
        /// <returns></returns>
        public bool UpdateAllImageStatus(eSTATES state, Credentials prmCrd,OdbcTransaction prmTrans)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update image_master" +
                " set status=" + (int)state + " , modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                " and policy_number='" + ctrlImage.PolicyNumber + "' and (status <> " + (int)eSTATES.PAGE_DELETED + " and status <> " + (int)eSTATES.PAGE_ON_HOLD + " and status <> " + (int)eSTATES.PAGE_RESCANNED_NOT_INDEXED + " )";

            try
            {

                sqlTrans = prmTrans;
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
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
        /// <summary>
        /// Update image status with transaction eanbled
        /// </summary>
        /// <param name="state"></param>
        /// <param name="prmCrd"></param>
        /// <returns></returns>
        public bool UpdateAllImageStatus(eSTATES state, Credentials prmCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update image_master" +
                " set status=" + (int)state + " , modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                " and policy_number='" + ctrlImage.PolicyNumber + "' and (status <> " + (int)eSTATES.PAGE_DELETED + " and status <> " + (int)eSTATES.PAGE_ON_HOLD + " and status <> " + (int)eSTATES.PAGE_RESCANNED_NOT_INDEXED + " and status<>" + (int) eSTATES.PAGE_EXPORTED + ")";

            try
            {
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
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
		public bool TotalImageUpdateStatus(eSTATES state)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			
			OdbcCommand sqlCmd=new OdbcCommand();
			
			sqlStr=@"update image_master" +
				" set status=" + (int)state + " where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
				" and policy_number='" + ctrlImage.PolicyNumber + "' and status <>29";
				
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
        public DataSet GetAllExportedImage(string proj_key,string batch_key,string box_number,string policy_number)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select page_index_name,status,page_name,doc_type from image_master " +
                    " where proj_key ='" + proj_key +"'"+
                " and batch_key ='" + batch_key + "' and box_number ='" + box_number +"'"+
                " and policy_number ='" + policy_number  + "' and status<>29 order by serial_no";
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
            return dsImage;
        }
        public DataSet GetAllExportedImagewithProj(string policy_number)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select proj_key,batch_key,box_number,page_name,status,page_name,doc_type,policy_number,qc_size from image_master " +
                     " where policy_number ='" + policy_number + "' and status<>29 order by serial_no";

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
            return dsImage;
        }

        
        public DataSet GetAllDeed(string proj_key, string batch_key, string box_number, string policy_number)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select page_index_name,status,page_name,doc_type from image_master " +
                    " where proj_key ='" + proj_key + "'" +
                " and batch_key ='" + batch_key + "' and box_number ='" + box_number + "'" +
                " and policy_number ='" + policy_number + "' and status<>29 order by serial_no";
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
            return dsImage;
        }
        public DataTable GetAllDeedEX(string Do_code, string RO_Code, string year, string deed_year,string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            DataSet ds = new DataSet();
            string exception =  null;
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,a.Serial_No,a.Serial_Year,a.tran_maj_code,a.tran_min_code,a.Volume_No,a.Page_From,a.Page_To,a.Date_of_Completion,a.Date_of_Delivery,replace(replace(replace(a.Deed_Remarks,'\t',''),'\n',''),'\r','') as Deed_Remarks,a.Scan_doc_type,a.hold as Exception from deed_details a,deed_details_exception b where a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "'  and a.deed_no = '" + deed_no + "' and a.district_code = b.district_code and a.Ro_code = b.ro_code and a.book = b.book and a.deed_year =b.deed_year and a.deed_no = b.deed_no";
             sqlStr = "select District_Code,RO_Code,Book,Deed_year,Deed_no,Serial_No,Serial_Year,tran_maj_code,tran_min_code,Volume_No,Page_From,Page_To,Date_of_Completion,Date_of_Delivery,replace(replace(replace(Deed_Remarks,'\t',''),'\n',''),'\r','') as Deed_Remarks,Scan_doc_type,hold from deed_details where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);

                sqlStr = "select exception from deed_details_exception where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(ds);
                if(ds.Tables[0].Rows.Count>0)
                {
                    for(int j=0;j<ds.Tables[0].Rows.Count;j++)
                    {
                    exception = exception + ds.Tables[0].Rows[j][0].ToString()+";";
                    }
                }
                else
                {
                    exception = "";
                }
                dsImage.Tables[0].Columns.Add("Exception_Type");
                for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                {
                    dsImage.Tables[0].Rows[i]["Exception_Type"] = exception.TrimEnd(';');
                }
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            
            return dsImage.Tables[0];
        }
        public DataTable GetAlloutsideWBDeedEX(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select district_code,ro_code,book,deed_year,deed_no,item_no,property_country_code,property_state_code,Property_district_code,thana,moucode,Plot_code_type,Plot_No,Khatian_type,khatian_No,land_use,property_type,area_acre,local_body_type,other_details,area_bigha,area_decimal,area_katha,area_chatak,area_sqf,area_sqfeet,total_area_decimal,struct_sqfeet from index_of_property_out_wb  where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
           // sqlStr = "select * from index_of_property_out_wb a where a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "'";
            // sqlStr = "select District_Code,RO_Code,Book,Deed_year,Deed_no,Serial_No,Serial_Year,tran_maj_code,tran_min_code,Volume_No,Page_From,Page_To,Date_of_Completion,Date_of_Delivery,replace(replace(replace(Deed_Remarks,'\t',''),'\n',''),'\r','') as Deed_Remarks,Scan_doc_type,hold as Exception from deed_details where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
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
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();

            return dsImage.Tables[0];
        }
        public DataTable GetAllDeedEX(string Do_code, string RO_Code, string year, string deed_year)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select a.district_name,b.ro_NAME,c.book,c.deed_year,c.deed_no,c.serial_no,c.serial_year,d.tran_maj_name,e.tran_name,c.volume_no,c.page_from,c.page_to,c.date_of_completion,c.date_of_delivery,c.deed_remarks from district a,ro_master b, deed_details c,party d,tranlist_code e where c.district_code = a.district_code and c.ro_code = b.ro_code and d.tran_maj_code = c.tran_maj_code and e.tran_min_code = c.tran_min_code and c.district_code = '" + Do_code + "' and c.Ro_code = '" + RO_Code + "' and c.book = '" + year + "' and c.deed_year = '" + deed_year + "'";
            sqlStr = "select * from deed_details where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "'";
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
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }
        public DataTable GetAllDeedEXQA(string Do_code, string RO_Code, string year, string deed_year,string vol)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select a.district_name,b.ro_NAME,c.book,c.deed_year,c.deed_no,c.serial_no,c.serial_year,d.tran_maj_name,e.tran_name,c.volume_no,c.page_from,c.page_to,c.date_of_completion,c.date_of_delivery,c.deed_remarks from district a,ro_master b, deed_details c,party d,tranlist_code e where c.district_code = a.district_code and c.ro_code = b.ro_code and d.tran_maj_code = c.tran_maj_code and e.tran_min_code = c.tran_min_code and c.district_code = '" + Do_code + "' and c.Ro_code = '" + RO_Code + "' and c.book = '" + year + "' and c.deed_year = '" + deed_year + "'";
            //sqlStr = "select * from deed_details where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and volume_no = '"+vol+"'";
            sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Serial_No,a.Serial_Year,d.tran_maj_name,e.tran_name,a.Volume_No,a.Page_From,a.Page_To,a.Date_of_Completion,a.Date_of_Delivery,a.Deed_Remarks,a.Created_DTTM,a.Scan_doc_type from deed_details a,district b, ro_master c,party d,tranlist_code e where a.District_Code = b.district_code and a.ro_code = c.ro_code and a.tran_maj_code = d.tran_maj_code and a.tran_min_code = e.tran_min_code and a.tran_maj_code = e.tran_maj_code and a.district_code = '" + Do_code + "' and c.district_code = '" + Do_code + "' and c.ro_code = '" + RO_Code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.volume_no = '" + vol + "' group by a.district_code,a.deed_no order by a.deed_no";

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
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }
        public DataTable GetAllDeedEXQA(string runNum)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select a.district_name,b.ro_NAME,c.book,c.deed_year,c.deed_no,c.serial_no,c.serial_year,d.tran_maj_name,e.tran_name,c.volume_no,c.page_from,c.page_to,c.date_of_completion,c.date_of_delivery,c.deed_remarks from district a,ro_master b, deed_details c,party d,tranlist_code e where c.district_code = a.district_code and c.ro_code = b.ro_code and d.tran_maj_code = c.tran_maj_code and e.tran_min_code = c.tran_min_code and c.district_code = '" + Do_code + "' and c.Ro_code = '" + RO_Code + "' and c.book = '" + year + "' and c.deed_year = '" + deed_year + "'";
            //sqlStr = "select * from deed_details where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and volume_no = '"+vol+"'";
            sqlStr = "select distinct a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Serial_No,a.Serial_Year,d.tran_maj_name as 'Trans Major',e.tran_name as 'Trans Minor',a.Volume_No,a.Page_From,a.Page_To,a.Date_of_Completion,a.Date_of_Delivery,a.Deed_Remarks,a.Created_DTTM,a.Scan_doc_type from deed_details a,district b, ro_master c,party d,tranlist_code e,policy_master f where a.District_Code = b.district_code and a.ro_code = c.ro_code and a.tran_maj_code = d.tran_maj_code and a.tran_min_code = e.tran_min_code and a.tran_maj_code = e.tran_maj_code and f.do_code = a.district_code and f.br_code = a.ro_code and f.year = a. book and f.deed_year = a.deed_year and f.deed_no = a.deed_no and f.run_no = '" + runNum + "' group by a.district_code,a.deed_no order by a.deed_no";

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
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }

        public DataTable GetAllDeedEXQAPercent(string runNum,string limit)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select a.district_name,b.ro_NAME,c.book,c.deed_year,c.deed_no,c.serial_no,c.serial_year,d.tran_maj_name,e.tran_name,c.volume_no,c.page_from,c.page_to,c.date_of_completion,c.date_of_delivery,c.deed_remarks from district a,ro_master b, deed_details c,party d,tranlist_code e where c.district_code = a.district_code and c.ro_code = b.ro_code and d.tran_maj_code = c.tran_maj_code and e.tran_min_code = c.tran_min_code and c.district_code = '" + Do_code + "' and c.Ro_code = '" + RO_Code + "' and c.book = '" + year + "' and c.deed_year = '" + deed_year + "'";
            //sqlStr = "select * from deed_details where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and volume_no = '"+vol+"'";
            sqlStr = "select distinct a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Serial_No,a.Serial_Year,d.tran_maj_name as 'Trans Major',e.tran_name as 'Trans Minor',a.Volume_No,a.Page_From,a.Page_To,a.Date_of_Completion,a.Date_of_Delivery,a.Deed_Remarks,a.Created_DTTM,a.Scan_doc_type,a.hold,a.hold_reason from deed_details a,district b, ro_master c,party d,tranlist_code e,policy_master f where a.District_Code = b.district_code and a.ro_code = c.ro_code and a.tran_maj_code = d.tran_maj_code and a.tran_min_code = e.tran_min_code and a.tran_maj_code = e.tran_maj_code and f.do_code = a.district_code and f.br_code = a.ro_code and f.year = a. book and f.deed_year = a.deed_year and f.deed_no = a.deed_no and f.run_no = '" + runNum + "' group by a.district_code,a.deed_no order by RAND() limit " + limit + "";

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
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }

        //public DataTable GetAllNameEX(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        //{
        //    string sqlStr = null;
        //    DataSet dsImage = new DataSet();
        //    OdbcDataAdapter sqlAdap = null;
        //    string indexPageName = string.Empty;

        //    //sqlStr = "select * from index_of_name where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
        //    sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Item_no,a.initial_name,a.First_name,a.Last_name,D.EC_NAME,a.Admit_code,a.Address,a.Address_district_code,a.Address_district_name,a.Address_ps_code,a.Address_ps_name,a.Father_mother,a.Rel_code,a.Relation,a.occupation_code,a.religion_code from index_of_name a,district b, ro_master c,party_CODE d where a.District_Code = b.district_code  and a.ro_code = c.ro_code  and a.party_code = d.ec_code  and a.district_code = '" + Do_code + "' and c.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and c.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "' order by a.item_no";
        //    try
        //    {
        //        sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
        //        sqlAdap.Fill(dsImage);
        //    }
        //    catch (Exception ex)
        //    {
        //        sqlAdap.Dispose();
        //        stateLog = new MemoryStream();
        //        tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
        //        stateLog.Write(tmpWrite, 0, tmpWrite.Length);
        //        exMailLog.Log(ex, this);
        //    }
        //    //DataRow dr = dsImage.Tables[0].Rows[0];
        //    //dsImage.Dispose();
        //    return dsImage.Tables[0];
        //}
        public DataTable GetAllNameEX(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select * from index_of_name where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Item_no,a.initial_name,a.First_name,a.Last_name,D.EC_NAME,a.Admit_code,a.Address,a.Address_district_code,a.Address_district_name,a.Address_ps_code,a.Address_ps_name,a.Father_mother,a.Rel_code,a.Relation,a.occupation_code,a.religion_code from index_of_name a,district b, ro_master c,party_CODE d where a.District_Code = b.district_code  and a.ro_code = c.ro_code  and a.party_code = d.ec_code  and a.district_code = '" + Do_code + "' and c.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and c.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "' order by a.item_no";
            //sqlStr = "select Concat('Name: ',a.initial_name,' ',a.First_name,' ',a.Last_name,'\r',a.Relation,'\r','\r','Address: ',a.address,'\r','District: ',a.Address_district_name,' Pin: ',a.pin,'\r','State: West Bangal, Country: India') as 'Name & Address',Concat('Status : '),a.item_no,D.EC_NAME,a.Admit_code,a.Address,a.Address_district_code,a.Address_district_name,a.Address_ps_code,a.Address_ps_name,a.Father_mother,a.Rel_code,a.Relation,a.occupation_code,a.religion_code from index_of_name a,district b, ro_master c,party_CODE d where a.District_Code = b.district_code  and a.ro_code = c.ro_code  and a.party_code = d.ec_code  and a.district_code = '" + Do_code + "' and c.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and c.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "' order by a.item_no";
            sqlStr = "select Concat('Name: ',a.initial_name,' ',a.First_name,' ',a.Last_name,'\r',a.rel_code,'/o: ',a.Relation,'\r','\r','Address: ',a.address,'\r','District: ',a.Address_district_name,' Pin: ',a.pin,'\r','PS: ',a.Address_ps_name,' City: ',a.city,'\t','\t','\t','     ') as 'Name and Address',Concat('\r','Transaction : ',trim(f.tran_maj_name),' (',trim(g.tran_name),' )','\r','Deed Registered at: ',c.ro_name,'\r','Status : ',trim(D.EC_NAME),'\r') as 'Status and Transaction',a.other_party_code,a.more from index_of_name a,district b, ro_master c,party_CODE d ,party f,tranlist_code g,deed_details h where a.District_Code = b.district_code and a.ro_code = c.ro_code  and a.party_code = d.ec_code and h.tran_maj_code = f.tran_maj_code and g.tran_maj_code = h.tran_maj_code and g.tran_min_code = h.tran_min_code and  h.District_Code = a.District_Code and h.ro_code = a.ro_code and h.book = a.book and h.deed_year = a.deed_year and h.deed_no = a.deed_no and a.district_code = '" + Do_code + "' and c.district_code = '" + Do_code + "' and h.district_code = '" + Do_code + "'and h.ro_code = '" + RO_Code + "' and h.deed_year = '" + deed_year + "' and h.deed_no = '" + deed_no + "'and a.Ro_code = '" + RO_Code + "' and c.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "' order by a.item_no";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
                for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                {
                    if (dsImage.Tables[0].Rows[i][2].ToString() != "")
                    {
                        dsImage.Tables[0].Rows[i][1] = dsImage.Tables[0].Rows[i][1] + "( " + getParty(dsImage.Tables[0].Rows[i][2].ToString()).Rows[0][0].ToString().Trim() + " )";
                    }
                    if (dsImage.Tables[0].Rows[i][3].ToString() == "Y")
                    {
                        dsImage.Tables[0].Rows[i][0] = dsImage.Tables[0].Rows[i][0] + "And More Persons.";
                    }
                }
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }
        public DataTable getParty(string partyCode)
        {
            DataSet ds = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string sql = "select ec_name from party_code where ec_code = '"+partyCode+"'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sql, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sql + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return ds.Tables[0];

        }
        public DataTable GetAllNameEX1(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            DataSet ds = new DataSet();
            string exception = null;
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;
            //
            //sqlStr = "select a.District_Code,a.ro_code,a.book,a.deed_year,a.deed_no,a.item_no,a.initial_name,a.first_name,a.last_name,a.party_code,a.admit_code,replace(replace(replace(replace(a.Address,'\t',''),'\n',''),'\r',''),'\"','') as Address,a.Address_district_code,a.Address_district_name,a.Address_ps_code,a.Address_ps_name,a.Father_mother,a.Rel_code,a.Relation,a.occupation_code,a.religion_code,a.more,a.pin,a.city,a.other_party_code,a.linked_to,b.exception from index_of_name a,index_of_name_exception b where a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "' and a.district_code = b.district_code and a.Ro_code = b.Ro_code and a.book = b.book and a.deed_year = b.deed_year and a.deed_no = b.deed_no";
            sqlStr = "select District_Code,ro_code,book,deed_year,deed_no,item_no,initial_name,first_name,last_name,party_code,admit_code,replace(replace(replace(replace(Address,'\t',''),'\n',''),'\r',''),'\"','') as Address,Address_district_code,Address_district_name,Address_ps_code,Address_ps_name,Father_mother,Rel_code,Relation,occupation_code,religion_code,more,pin,city,other_party_code,linked_to from index_of_name where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Item_no,a.initial_name,a.First_name,a.Last_name,D.EC_NAME,a.Admit_code,a.Address,a.Address_district_code,a.Address_district_name,a.Address_ps_code,a.Address_ps_name,a.Father_mother,a.Rel_code,a.Relation,e.occupation_name,f.religion_name from index_of_name a,district b, ro_master c,party_CODE d,occupation e,religion f where a.District_Code = b.district_code  and a.ro_code = c.ro_code  and a.party_code = d.ec_code  and a.occupation_code = e.occupation_code and a.religion_code = f.religion_code and a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
                string itm_no = dsImage.Tables[0].Rows[0][5].ToString();
                sqlStr = "select exception from index_of_name_exception where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "' and item_no = '"+itm_no+"'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        exception =  exception+ ds.Tables[0].Rows[j][0].ToString() + ";";
                    }
                }
                else
                {
                    exception = "";
                }
                dsImage.Tables[0].Columns.Add("Exception_Type");
                for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                {
                    dsImage.Tables[0].Rows[i]["Exception_Type"] = exception.TrimEnd(';');
                }
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }

        public DataTable GetAllOtherPlot(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

           // sqlStr = "select * from tblother_plots where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            sqlStr = "select distinct a.* from tblother_plots a,index_of_property b where a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "' and a.district_code = b.district_code and a.ro_code = b.ro_code and a.book = b.book and a.deed_year = b.deed_year and a.deed_no = b.deed_no and b.property_type = 'FL'";
            //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Item_no,a.initial_name,a.First_name,a.Last_name,D.EC_NAME,a.Admit_code,a.Address,a.Address_district_code,a.Address_district_name,a.Address_ps_code,a.Address_ps_name,a.Father_mother,a.Rel_code,a.Relation,e.occupation_name,f.religion_name from index_of_name a,district b, ro_master c,party_CODE d,occupation e,religion f where a.District_Code = b.district_code  and a.ro_code = c.ro_code  and a.party_code = d.ec_code  and a.occupation_code = e.occupation_code and a.religion_code = f.religion_code and a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "'";
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
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }
        public DataTable GetAllOtherKhatian(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select * from tbl_other_khatian where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            sqlStr = "select distinct a.* from tbl_other_khatian a,index_of_property b where a.district_code = '"+Do_code+"' and a.Ro_code = '"+RO_Code+"' and a.book = '"+year+"' and a.deed_year = '"+deed_year+"' and a.deed_no = '"+deed_no+"' and a.district_code = b.district_code and a.ro_code = b.ro_code and a.book = b.book and a.deed_year = b.deed_year and a.deed_no = b.deed_no and b.property_type = 'FL'";
            //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Item_no,a.initial_name,a.First_name,a.Last_name,D.EC_NAME,a.Admit_code,a.Address,a.Address_district_code,a.Address_district_name,a.Address_ps_code,a.Address_ps_name,a.Father_mother,a.Rel_code,a.Relation,e.occupation_name,f.religion_name from index_of_name a,district b, ro_master c,party_CODE d,occupation e,religion f where a.District_Code = b.district_code  and a.ro_code = c.ro_code  and a.party_code = d.ec_code  and a.occupation_code = e.occupation_code and a.religion_code = f.religion_code and a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "'";
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
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }
        public DataTable GetAllPropEX(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            //sqlStr = "select * from index_of_property where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Item_no,b.district_name as Property_district_name,c.ro_name as Property_ro_name,a.ps_code,a.moucode,a.Area_type,a.GP_Muni_Corp_Code,a.Ward,a.Holding,a.Premises,a.road_code,a.Plot_code_type,a.Road,a.Plot_No,a.Bata_No,a.Khatian_type,a.khatian_No,a.bata_khatian_no,a.property_type,a.Land_Area_acre,a.Land_Area_bigha,a.Land_Area_decimal,a.Land_Area_katha,a.Land_Area_chatak,a.Land_Area_sqfeet,a.Structure_area_in_sqFeet,a.ref_ps,a.ref_mouza,a.jl_no from index_of_property a,district b,ro_master c where  a.district_code = b.district_code and c.ro_code = a.ro_code  and a.district_code = '" + Do_code + "' and c.district_code = '" + Do_code + "' and c.ro_code ='" + RO_Code + "' and a.ro_code ='" + RO_Code + "' and a.book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
           
            // change in sequence area_decimal placed after area_chatak and land_area_sqfeet
            sqlStr = " select distinct Concat('District: ',trim(b.district_name),',','\r','PS :') as 'Property Location',concat('Property No: ',a.item_no,'\n','Property Type: ',pt.description) as 'Property Type',Concat('Premises No: ',a.Premises,'\n','\r','Plot: ',a.Plot_code_type,'-',a.Plot_No) as 'Plot and Khatian No',a.Land_Area_acre,a.Land_Area_bigha,a.Land_Area_katha,a.Land_Area_chatak,a.Land_Area_sqfeet,a.Land_Area_decimal,a.Structure_area_in_sqFeet,a.ps_code,a.property_district_code,a.Khatian_type,a.khatian_No,ps_code,moucode,road_code,road,ref_ps,ref_mouza,bata_no,bata_khatian_no from index_of_property a,district b,ro_master c,deed_details h,property_type pt where  a.Property_district_code = b.district_code and h.District_Code = a.district_code and h.ro_code = a.ro_code and h.book = a.book and h.deed_year = a.deed_year and h.deed_no = a.deed_no and pt.apartment_type_code = a.property_type and c.ro_code = a.ro_code  and a.district_code = '" + Do_code + "'  and c.ro_code ='" + RO_Code + "' and a.ro_code ='" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "'";
            //sqlStr = "select Concat('District: ',trim(b.district_name),'\r','PS :') as 'Property Location',concat('Property Type: ', pt.description,'\r','\r','Transaction: ',f.tran_maj_name,'\r',g.tran_name) as 'Property Type and Transaction',Concat('Plot: ',a.Plot_code_type,'-',a.Plot_No,'\r','Khatian: ',a.Khatian_type,'-',a.khatian_No) as 'Plot and Khatian No',a.Land_Area_acre,a.Land_Area_bigha,a.Land_Area_decimal,a.Land_Area_katha,a.Land_Area_chatak,a.Land_Area_sqfeet,a.Structure_area_in_sqFeet,a.ps_code,a.district_code from index_of_property a,district b,ro_master c,party f,tranlist_code g,deed_details h,property_type pt where  a.Property_district_code = b.district_code and h.tran_maj_code = f.tran_maj_code and g.tran_maj_code = h.tran_maj_code and g.tran_min_code = h.tran_min_code and  h.District_Code = a.Property_district_code and h.ro_code = a.ro_code and h.book = a.book and h.deed_year = a.deed_year and h.deed_no = a.deed_no and pt.apartment_type_code = a.property_type and c.ro_code = a.ro_code  and a.district_code = '" + Do_code + "' and c.district_code = '" + Do_code + "' and c.ro_code ='" + RO_Code + "' and a.ro_code ='" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
                dsImage.Tables[0].Columns.Add("Area with Property");
                for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                {
                    string Area = string.Empty;
                    if (dsImage.Tables[0].Rows[i][3].ToString() != "0")
                    {
                        Area = Area + dsImage.Tables[0].Rows[i][3].ToString() + " Acre ";
                    }
                    if (dsImage.Tables[0].Rows[i][4].ToString() != "0")
                    {
                        Area = Area + dsImage.Tables[0].Rows[i][4].ToString() + " Bigha ";
                    }
                    
                    if (dsImage.Tables[0].Rows[i][5].ToString() != "0")
                    {
                        Area = Area +dsImage.Tables[0].Rows[i][5].ToString() + " Katha ";
                    }
                    if (dsImage.Tables[0].Rows[i][6].ToString() != "0")
                    {
                        Area = Area + dsImage.Tables[0].Rows[i][6].ToString() + " Chatak ";
                    }
                    if (dsImage.Tables[0].Rows[i][7].ToString() != "0")
                    {
                        Area = Area + dsImage.Tables[0].Rows[i][7].ToString() +" " + "SqFeet  ";
                    }
                    if (dsImage.Tables[0].Rows[i][8].ToString() != "0")
                    {
                        Area = Area + dsImage.Tables[0].Rows[i][8].ToString() + " Decimal ";
                    }
                    //if (dsImage.Tables[0].Rows[i][8].ToString() != "0")
                    //{
                    //    Area = Area + Math.Round((Convert.ToInt32(dsImage.Tables[0].Rows[i][8].ToString()) / 10.7639), 2) + " SqMeter or " + dsImage.Tables[0].Rows[i][8].ToString() + "SqFeet";
                    //}

                    if (dsImage.Tables[0].Rows[i][9].ToString() != "0")
                    {
                        Area = Area + "Structure Area "+dsImage.Tables[0].Rows[i][9].ToString() + " SqFeet or "+Math.Round((Convert.ToInt32(dsImage.Tables[0].Rows[i][9].ToString()) / 10.7639),2) + " SqMeter";
                    }
                    dsImage.Tables[0].Rows[i][22] = Area;
                    dsImage.Tables[0].Rows[i][0] = dsImage.Tables[0].Rows[i][0] + getPs_name(dsImage.Tables[0].Rows[i][10].ToString(), dsImage.Tables[0].Rows[i][11].ToString()) + '\r' + "Road :" + dsImage.Tables[0].Rows[i][17].ToString();
                    if (dsImage.Tables[0].Rows[i][15].ToString() != "")
                    {
                        dsImage.Tables[0].Rows[i][0] = dsImage.Tables[0].Rows[i][0].ToString() + '\r' + "Mouza: " + getmou_name(dsImage.Tables[0].Rows[i][10].ToString(), dsImage.Tables[0].Rows[i][11].ToString(), dsImage.Tables[0].Rows[i][15].ToString());
                    }
                    if (dsImage.Tables[0].Rows[i][20].ToString() != "")
                    {
                        dsImage.Tables[0].Rows[i][2] = dsImage.Tables[0].Rows[i][2].ToString() + "/"+dsImage.Tables[0].Rows[i][20].ToString();
                    }
                    if (dsImage.Tables[0].Rows[i][19].ToString() != "")
                    {
                        dsImage.Tables[0].Rows[i][0] = dsImage.Tables[0].Rows[i][0].ToString() + " ReF Mouza:" + dsImage.Tables[0].Rows[i][19].ToString();
                    }
                    
                    if (dsImage.Tables[0].Rows[i][13].ToString() != "")
                    {
                        dsImage.Tables[0].Rows[i][2] = dsImage.Tables[0].Rows[i][2].ToString() +'\r' +"Khatian No " + dsImage.Tables[0].Rows[i][12].ToString() + "-" + dsImage.Tables[0].Rows[i][13].ToString();
                    }
                    if (dsImage.Tables[0].Rows[i][21].ToString() != "")
                    {
                        dsImage.Tables[0].Rows[i][2] = dsImage.Tables[0].Rows[i][2].ToString() + "/" + dsImage.Tables[0].Rows[i][21].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return dsImage.Tables[0];
        }
        public string getPs_name(string ps_code,string dist)
        {
            string ps = string.Empty;
            OdbcDataAdapter sqlAdap = null;
            DataSet dsImage = new DataSet();
            string sql = "select trim(ps_name) from ps where ps_code = '"+ps_code+"' and district_code = '"+dist+"'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sql, sqlCon);
                sqlAdap.Fill(dsImage);
                ps = dsImage.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sql + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
                ps = null;
            }
            return ps;
        }

        public string getmou_name(string ps_code, string dist,string mou)
        {
            string ps = string.Empty;
            OdbcDataAdapter sqlAdap = null;
            DataSet dsImage = new DataSet();
            string sql = "select eng_mouname from moucode where moucode = '"+mou+"' and district_code = '"+dist+"' and ps_code = '"+ps_code+"'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sql, sqlCon);
                sqlAdap.Fill(dsImage);
                ps = dsImage.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sql + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
                ps = null;
            }
            return ps;
        }
        public DataTable GetAllPropEX1(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            DataSet ds = new DataSet();
            string exception = null;
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select * from index_of_property where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            //sqlStr = "select a.*,b.exception from index_of_property a, index_of_property_exception b where a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "' and a.deed_no = '" + deed_no + "' and a.district_code = b.district_code and a.Ro_code = b.Ro_code and a.book = b.book and a.deed_year = b.deed_year and a.deed_no = b.deed_no";
           //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,b.district_name,c.ro_name,a.Item_no,b.district_name as Property_district_name,c.ro_name as Property_ro_name,a.ps_code,a.moucode,a.Area_type,a.GP_Muni_Corp_Code,a.Ward,a.Holding,a.Premises,a.road_code,a.Plot_code_type,a.Road,a.Plot_No,a.Bata_No,a.Khatian_type,a.khatian_No,a.bata_khatian_no,a.property_type,a.Land_Area_acre,a.Land_Area_bigha,a.Land_Area_decimal,a.Land_Area_katha,a.Land_Area_chatak,a.Land_Area_sqfeet,a.Structure_area_in_sqFeet,a.ref_ps,a.ref_mouza,a.jl_no from index_of_property a,district b,ro_master c where  a.district_code = b.district_code and c.ro_code = a.ro_code and a.Property_district_code = b.district_code and a.Property_ro_code = c.ro_code and a.district_code = '" + Do_code + "' and a.ro_code ='" + RO_Code + "' and a.book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);

                sqlStr = "select exception from index_of_property_exception where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(ds);
                if(ds.Tables[0].Rows.Count>0)
                {
                    for(int j=0;j<ds.Tables[0].Rows.Count;j++)
                    {
                    exception = exception + ds.Tables[0].Rows[j][0].ToString()+";";
                    }
                }
                else
                {
                    exception = "";
                }
                dsImage.Tables[0].Columns.Add("Exception");
                for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                {
                    dsImage.Tables[0].Rows[i]["Exception"] = exception.TrimEnd(';');
                }
            }
            
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }
        public DataTable GetcsvAllPropEX1(string Do_code, string RO_Code, string year, string deed_year, string deed_no)
        {
            string sqlStr = null;
            string newdbConStr = sqlCon.ConnectionString;
            //newdbConStr = newdbConStr.Replace("3.51", "5.1");
            newdbConStr = newdbConStr.Replace("root;", "root;PASSWORD=root;");
            OdbcConnection newdbCon = new OdbcConnection(newdbConStr);
            newdbCon.Open();
            DataSet dsImage = new DataSet();
            string exception = null;
            DataSet ds = new DataSet();

            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,a.Item_no,a.Property_district_code,trim(b.district_name) as district_name,a.Property_ro_code, "+
                     "a.ps_code,trim(d.ps_name) as ps_name,a.moucode,trim(e.eng_mouname) as mouja,a.Area_type,a.GP_Muni_Corp_Code,a.GP_Muni_Corp_Code as GP_Muni_Name,a.Ward,a.Holding,a.Premises,a.road_code,"+
                     " a.Plot_code_type,a.Road,a.Plot_No,a.Bata_No,a.Khatian_type,a.khatian_No,a.bata_khatian_no,"+
                     " a.property_type,a.Land_Area_acre,a.Land_Area_bigha,a.Land_Area_decimal,"+
                     " a.Land_Area_katha,a.Land_Area_chatak,a.Land_Area_sqfeet,"+
                     " a.Structure_area_in_sqFeet,a.ref_ps,a.ref_mouza,"+
                     " a.jl_no,a.other_plots,a.other_khatian,a.land_type,a.refjl_no from" +
                     " index_of_property a left outer join district b on a.Property_district_code = b.district_code"+
                     " left outer join ro_master c on a.Property_district_code = c.district_code and a.Property_ro_code = c.ro_code"+
                     " left outer join ps d on a.Property_district_code = d.district_code and a.ps_code = d.ps_code"+
                     " left outer join moucode e on a.Property_district_code = e.district_code and a.ps_code = e.ps_code and a.moucode = e.moucode"+
                     " where a.district_code = '"+Do_code+"' and a.ro_code = '"+RO_Code+"' and a.book = '"+year+"' and a.deed_year = '"+deed_year+"' and a.deed_no = '"+deed_no+"' ";
            
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, newdbCon);
                sqlAdap.Fill(dsImage);
                string itm_no = dsImage.Tables[0].Rows[0][5].ToString();
                if (dsImage.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                    {
                        if(dsImage.Tables[0].Rows[i][13].ToString() != "")
                        {
                            dsImage.Tables[0].Rows[i][15] = getmuniCorpValue(dsImage.Tables[0].Rows[i][13].ToString(), dsImage.Tables[0].Rows[i][14].ToString(), dsImage.Tables[0].Rows[i][6].ToString(), dsImage.Tables[0].Rows[i][9].ToString()).Tables[0].Rows[0][0].ToString();
                        }
                    }
                }
                sqlStr = "select exception from index_of_property_exception where district_code = '" + Do_code + "' and Ro_code = '" + RO_Code + "' and book = '" + year + "' and deed_year = '" + deed_year + "' and deed_no = '" + deed_no + "' and item_no = '"+itm_no+"'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(ds);

                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        exception = exception + ds.Tables[0].Rows[j][0].ToString() + ";";
                       
                    }
                }
                else
                {
                    exception = "";
                }
                dsImage.Tables[0].Columns.Add("Exception_Type");
                for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                {
                    dsImage.Tables[0].Rows[i]["Exception_Type"] = exception.TrimEnd(';');
                }
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();
            return dsImage.Tables[0];
        }
        public DataSet getmuniCorpValue(string areaType,string gpCode,string disCode,string psCode)
        {
            DataSet ds = new DataSet();
            string sqlStr = string.Empty;
            OdbcDataAdapter sqlAdap = null;
            try
            {
                if (areaType == "M" || areaType == "C")
                {
                    sqlStr = "select trim(municipality_name) as municipality_name from municipality where district_code = '" + disCode + "' and municipality_code ='" + gpCode + "'";
                }
                else if (areaType == "G")
                {
                    sqlStr = "select trim(gp_desc) as gp_desc from gram_panchayat where district_code = '" + disCode + "' and ps_code = '" + psCode + "' and gp_code ='" + gpCode + "'";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return ds;
        }
        public bool RearrangePoposalDoctype(DataSet pTotImageName,int pMaxSerialNo)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();
            sqlTrans = sqlCon.BeginTransaction();
            sqlCmd.Connection = sqlCon;
            sqlCmd.Transaction = sqlTrans;
            try
            {
                for (int i = 0; i < pTotImageName.Tables[0].Rows.Count; i++)
                {
                sqlStr = @"update image_master" +
                    " set serial_no=" + (pMaxSerialNo+i+2) + " where proj_key=" + ctrlImage.ProjectKey +
                    " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                    " and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + pTotImageName.Tables[0].Rows[i]["page_name"].ToString() + "'";

                
                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();
                    
                    commitBol = true;
                    
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
            if (commitBol == true)
            {
                sqlTrans.Commit();
            }
            return commitBol;
        }
		public bool UpdateStatusAndDockType(eSTATES state,string prmDocType,string prmIndexImageName,Credentials prmCrd)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			
			OdbcCommand sqlCmd=new OdbcCommand();

            if (state != eSTATES.PAGE_DELETED)
            {
                sqlStr = @"update image_master" +
                    " set status=" + (int)state + " , page_index_name='" + prmIndexImageName + "', doc_type='" + prmDocType + "',modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlImage.ProjectKey +
                    " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                    " and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + ctrlImage.ImageName + "'";
            }
            else
            {
                sqlStr = @"update image_master" +
                    " set status=" + (int)state + " ,modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlImage.ProjectKey +
                    " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                    " and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + ctrlImage.ImageName + "'";
            }
			try
			{
				
				sqlTrans=sqlCon.BeginTransaction();
				sqlCmd.Connection = sqlCon;
				sqlCmd.Transaction=sqlTrans;
	            sqlCmd.CommandText = sqlStr;
	            int i = sqlCmd.ExecuteNonQuery();
	            sqlTrans.Commit();
	            if(i>0)
	            {
	            	commitBol=true;
	            }
	            else
	            	commitBol = false;
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
		
		public bool UpdateCustomException(int prmStatus,string prmProblemType,Credentials prmCrd)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			
			OdbcCommand sqlCmd=new OdbcCommand();
			
			sqlStr=@"update custom_exception" +
                " set status=" + (int)prmStatus + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
				" and policy_number='" + ctrlImage.PolicyNumber + "' and status=2 and image_name='" + ctrlImage.ImageName + "' and problem_type='" + prmProblemType.Trim() + "'";
				
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
		
		
		public bool AddCustomException(int prmStatus,string prmProblemType,string prmRemarks,Credentials prmCrd)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();

            sqlStr = @"insert into custom_exception(Proj_key,batch_key,box_number,policy_number,problem_type,Image_name,Remarks,status,created_by,created_dttm)" +
				" values(" + ctrlImage.ProjectKey + " ," + ctrlImage.BatchKey + ", '" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "','" + prmProblemType + "','" + ctrlImage.ImageName + "','" + prmRemarks + "'," + prmStatus + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "' )";	
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
		
		public DataSet GetCustomException(int prmState)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			
			sqlStr="select problem_type,remarks from custom_exception " + 
					" where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                " and policy_number='" + ctrlImage.PolicyNumber + "' and status=" + (int)prmState + " and image_name='" + ctrlImage.ImageName + "'";
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
			return dsImage;
		}

        public int GetImageStatus()
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            int status = 0;

            sqlStr = "select status from image_master " +
                    " where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                " and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + ctrlImage.ImageName + "'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
                if (dsImage.Tables[0].Rows.Count > 0)
                {
                    status = Convert.ToInt32(dsImage.Tables[0].Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            
            
            return status;
        }
		

		public bool GetImageCount(eSTATES state)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			
			sqlStr="select count(*) from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
                " and policy_number='" + ctrlImage.PolicyNumber + "' and status=" + (int)state + " and status<>" + (int)eSTATES.PAGE_DELETED;
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
			int value;
            if (dsImage.Tables[0].Rows.Count > 0)
            {
                value = Convert.ToInt32(dsImage.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                value = 0;
            }
            if (value > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}
		
		public int GetImageCount(eSTATES[] state)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			sqlStr="select page_name from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
				" and policy_number='" + ctrlImage.PolicyNumber+"'";
			for(int j=0;j<state.Length;j++)
			{
				if((int)state[j]!= 0)
				{
					if(j==0)
					{
						sqlStr=sqlStr + " and (status=" + (int)state[j] ;
					}
					else
						sqlStr=sqlStr + " or status=" + (int)state[j] ;
				}
			}
			sqlStr = sqlStr + ")";
			
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
			return dsImage.Tables[0].Rows.Count;
		}
		public int GetImageCount()
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			
			sqlStr="select max(serial_no) from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey + 
				" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
				" and policy_number='" + ctrlImage.PolicyNumber+"'";			
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
            if (dsImage.Tables[0].Rows[0][0].ToString() != string.Empty )
            {
                string i = dsImage.Tables[0].Rows[0][0].ToString();
                return Convert.ToInt32(dsImage.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
		}

        public int GetMaxPageCount()
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            int pagenumberSt = 0;

            pagenumberSt = ctrlImage.PolicyNumber.ToString().Length + 2;

            sqlStr = "select max(substring(page_name," + pagenumberSt + ",5)) from image_master " +
                    " where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
                " and policy_number='" + ctrlImage.PolicyNumber+"'";
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
            if (dsImage.Tables[0].Rows[0][0].ToString() != string.Empty)
            {
                string i = dsImage.Tables[0].Rows[0][0].ToString();
                return Convert.ToInt32(dsImage.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

		public DataSet GetReadyImageCount(eSTATES[] state,eSTATES[] prmPolicyState)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			
			sqlStr="select count(page_name) as page_Count,sum(qc_size) as index_size from image_master A,policy_master B" +
                    " where A.proj_key = B.proj_key and A.batch_key = B.batch_key and A.box_number = B.box_number and A.policy_number = B.policy_number and B.proj_key=" + ctrlImage.ProjectKey + 
				" and B.batch_key=" + ctrlImage.BatchKey + " and B.box_number='" + ctrlImage.BoxNumber + "' and A.status<>29";
            /*
			for(int j=0;j<state.Length;j++)
			{
				if((int)state[j]!= 0)
				{
					if(j==0)
					{
						sqlStr=sqlStr + " and (A.status=" + (int)state[j] ;
					}
					else
						sqlStr=sqlStr + " or A.status=" + (int)state[j] ;
				}
			}
			sqlStr = sqlStr + " and A.status<>" + (int)eSTATES.PAGE_DELETED + " )";
            */
            for (int j = 0; j < state.Length; j++)
            {
                if ((int)state[j] != 0)
                {
                    if (j == 0)
                    {
                        sqlStr = sqlStr + " and (B.status=" + (int)prmPolicyState[j];
                    }
                    else
                        sqlStr = sqlStr + " or B.status=" + (int)prmPolicyState[j];
                }
            }
            sqlStr = sqlStr + " )";
            
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
			return dsImage;
		}
		
		public DataSet GetPolicyWiseImageInfo(eSTATES[] state)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			
			sqlStr="select count(page_name) as page_Count,sum(qc_size) as qc_size from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey + 
				" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
				" and policy_number='" + ctrlImage.PolicyNumber+"' and status <> '29'";
			//for(int j=0;j<state.Length;j++)
			//{
			//	if((int)state[j]!= 0)
			//	{
			//		if(j==0)
			//		{
			//			sqlStr=sqlStr + " and (status=" + (int)state[j] ;
			//		}
			//		else
			//			sqlStr=sqlStr + " or status=" + (int)state[j] ;
			//	}
			//}
			//sqlStr = sqlStr + " )";
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
			return dsImage;
		}
		
		public int GetDocTypeCount(string prmDocType)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			
			sqlStr="select page_name from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey + 
				" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
				" and policy_number='" + ctrlImage.PolicyNumber + "' and doc_type='" + prmDocType + "' and status<>" + (int)eSTATES.PAGE_DELETED;
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
			return dsImage.Tables[0].Rows.Count;
		}
		public int GetDocTypeCount(eSTATES[] state)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			
			
			sqlStr="select distinct doc_type from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey + 
				" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
				" and policy_number='" + ctrlImage.PolicyNumber+"'";
			
			for(int j=0;j<state.Length;j++)
			{
				if((int)state[j]!= 0)
				{
					if(j==0)
					{
						sqlStr=sqlStr + " and (status=" + (int)state[j] ;
					}
					else
						sqlStr=sqlStr + " or status=" + (int)state[j] ;
				}
			}
			sqlStr = sqlStr + " )";
			
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
			return dsImage.Tables[0].Rows.Count;
		}
		public string GetIndexedImageName()
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			string indexPageName=string.Empty;
			
			sqlStr="select page_index_name from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey + 
				" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
				" and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + ctrlImage.ImageName + "'";
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
			if(dsImage.Tables[0].Rows.Count > 0)
			{
				indexPageName= dsImage.Tables[0].Rows[0]["page_index_name"].ToString();
			}
			return indexPageName;
		}
        public DataSet GetAllIndexedImage()
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select page_index_name,status,page_name,doc_type from image_master " +
                    " where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
                " and policy_number='" + ctrlImage.PolicyNumber + "' and status<>29 order by serial_no";
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
            return dsImage;
        }
        public DataSet GetAllImages(out OdbcDataAdapter pAdp)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select page_index_name,status,page_name,doc_type,policy_number,proj_key,batch_key,box_number,serial_no from image_master " +
                    " where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
                " and status<>29 order by policy_number,serial_no";
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
            finally
            {
                pAdp = sqlAdap;
            }
            return dsImage;
        }
        /// <summary>
        /// This method is used for deleting image name from database
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteImage()
        {
            string sqlStr = null;
            OdbcCommand sqlCmd = new OdbcCommand();
            bool commitBol = true;

            sqlStr = "delete from image_master " +
                    " where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
                " and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + ctrlImage.ImageName + "'";
            try
            {
                sqlCmd.Connection = sqlCon;
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
            }
            catch (Exception ex)
            {
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
                commitBol = false;
            }
            return commitBol;
        }
        /// <summary>
        /// Get all indexed page name against one doctype
        /// </summary>
        /// <returns>dataset</returns>
        public DataSet GetAllIndexedImageName()
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select page_index_name from image_master " +
                    " where proj_key=" + ctrlImage.ProjectKey +
                " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
                " and policy_number='" + ctrlImage.PolicyNumber + "' and doc_type='" + ctrlImage.DocType + "' and status<>" + (int)eSTATES.PAGE_DELETED + " order by serial_no";
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
            return dsImage;
        }

        public ArrayList GetDeletedPage_List(eSTATES[] prmPolicyState, eSTATES[] prmImageState, wItem wi)
        {
            ArrayList arrItem = new ArrayList();
            OdbcDataAdapter wAdap = null;
            DataSet ds = new DataSet();
            string strQuery = null;

            try
            {
                wfePolicy queryPolicy = (wfePolicy)wi;
                strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.doc_type from image_master A,metadata_entry B where A.proj_key=B.proj_code and A.batch_key=B.batch_key and A.policy_number=B.filename and A.proj_key=" + queryPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + queryPolicy.ctrlPolicy.BatchKey + " and A.box_number='" + queryPolicy.ctrlPolicy.BoxNumber + "' and A.policy_number='" + queryPolicy.ctrlPolicy.PolicyNumber + "' and A.status= " + (int)prmImageState[0];

               
                wAdap = new OdbcDataAdapter(strQuery, sqlCon);
                wAdap.Fill(ds);
            }
            catch (Exception EX)
            {
                wAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(strQuery + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(EX);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                wic = new CtrlImage((int)ds.Tables[0].Rows[i]["proj_key"], (int)ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["box_number"].ToString(), (string)ds.Tables[0].Rows[i]["policy_number"].ToString(), ds.Tables[0].Rows[i]["page_name"].ToString(), ds.Tables[0].Rows[i]["doc_type"].ToString());
                arrItem.Add(wic);
            }
            return arrItem;
        }


		public string GetPhotoImageName()
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			string indexPageName=string.Empty;
			
			sqlStr="select page_name from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey + 
				" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
				" and policy_number='" + ctrlImage.PolicyNumber + "' and photo=1";
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
			if(dsImage.Tables[0].Rows.Count > 0)
			{
				indexPageName= dsImage.Tables[0].Rows[0]["page_name"].ToString();
			}
			return indexPageName;
		}
		public DataSet GetIndexedImageName(string prmDocType)
		{
			string sqlStr=null;
			DataSet dsImage=new DataSet();
			OdbcDataAdapter sqlAdap=null;
			string indexPageName=string.Empty;
			
			sqlStr="select page_index_name,status,page_name from image_master " + 
					" where proj_key=" + ctrlImage.ProjectKey + 
				" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber +"'"+
				" and policy_number='" + ctrlImage.PolicyNumber + "' and doc_type='" + prmDocType + "' and status<>29 order by serial_no";
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
			return dsImage;
		}
		public ArrayList GetDeletedPageList(eSTATES[] prmPolicyState,eSTATES[] prmImageState, wItem wi)
		{
			ArrayList arrItem=new ArrayList();
			OdbcDataAdapter wAdap=null;
			DataSet ds=new DataSet();
			string strQuery=null;
			
			try 
			{
				wfePolicy queryPolicy = (wfePolicy) wi;
				strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.doc_type from image_master A,policy_master B where A.proj_key=B.proj_key and A.batch_key=B.batch_key and A.box_number=B.box_number and A.policy_number=B.policy_number and A.proj_key=" + queryPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + queryPolicy.ctrlPolicy.BatchKey + " and A.box_number='" + queryPolicy.ctrlPolicy.BoxNumber + "' and A.policy_number='" + queryPolicy.ctrlPolicy.PolicyNumber + "' and A.status= " + (int)prmImageState[0] ;
				for(int j=0;j<prmPolicyState.Length;j++)
				{
					if((int)prmPolicyState[j]!= 0)
					{
						if(j==0)
						{
							strQuery=strQuery + " and (B.status=" + (int)prmPolicyState[j] ;
						}
						else
							strQuery=strQuery + " or B.status=" + (int)prmPolicyState[j] ;
					}
				}
				strQuery = strQuery + " )";
				wAdap=new OdbcDataAdapter(strQuery,sqlCon);
				wAdap.Fill(ds);	
			}
			catch(Exception EX)
			{
				wAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(strQuery + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(EX, this);
			}
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
				wic = new CtrlImage((int)ds.Tables[0].Rows[i]["proj_key"],(int) ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["box_number"].ToString(),(string)ds.Tables[0].Rows[i]["policy_number"].ToString(),ds.Tables[0].Rows[i]["page_name"].ToString(),ds.Tables[0].Rows[i]["doc_type"].ToString());
					arrItem.Add (wic);
				}
			return arrItem;
		}
        public ArrayList GetDeletedPageList1(eSTATES[] prmPolicyState, eSTATES[] prmImageState, wItem wi)
        {
            ArrayList arrItem = new ArrayList();
            OdbcDataAdapter wAdap = null;
            DataSet ds = new DataSet();
            string strQuery = null;

            try
            {
                wfePolicy queryPolicy = (wfePolicy)wi;
                strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.doc_type from image_master A,metadata_entry B where A.proj_key=B.proj_code and A.batch_key=B.batch_key and A.policy_number=B.filename and A.proj_key=" + queryPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + queryPolicy.ctrlPolicy.BatchKey + " and A.box_number='" + queryPolicy.ctrlPolicy.BoxNumber + "' and A.policy_number='" + queryPolicy.ctrlPolicy.PolicyNumber + "' and A.status= " + (int)prmImageState[0];
                for (int j = 0; j < prmPolicyState.Length; j++)
                {
                    if ((int)prmPolicyState[j] != 0)
                    {
                        if (j == 0)
                        {
                            strQuery = strQuery + " and (B.status=" + 2;
                        }
                        else
                            strQuery = strQuery + " or B.status=" + 2;
                    }
                }
                strQuery = strQuery + " )";
                wAdap = new OdbcDataAdapter(strQuery, sqlCon);
                wAdap.Fill(ds);
            }
            catch (Exception EX)
            {
                wAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(strQuery + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(EX, this);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                wic = new CtrlImage((int)ds.Tables[0].Rows[i]["proj_key"], (int)ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["box_number"].ToString(), (string)ds.Tables[0].Rows[i]["policy_number"].ToString(), ds.Tables[0].Rows[i]["page_name"].ToString(), ds.Tables[0].Rows[i]["doc_type"].ToString());
                arrItem.Add(wic);
            }
            return arrItem;
        }
        public ArrayList GetDeletedPageList2(eSTATES[] prmPolicyState, eSTATES[] prmImageState, wItem wi)
        {
            ArrayList arrItem = new ArrayList();
            OdbcDataAdapter wAdap = null;
            DataSet ds = new DataSet();
            string strQuery = null;

            try
            {
                wfePolicy queryPolicy = (wfePolicy)wi;
                strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.doc_type from image_master A,case_file_master B where A.proj_key=B.proj_code and A.batch_key=B.bundle_key and A.policy_number=B.filename and A.proj_key=" + queryPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + queryPolicy.ctrlPolicy.BatchKey + " and A.box_number='" + queryPolicy.ctrlPolicy.BoxNumber + "' and A.policy_number='" + queryPolicy.ctrlPolicy.PolicyNumber + "' and A.status= " + (int)prmImageState[0];
                for (int j = 0; j < prmPolicyState.Length; j++)
                {
                    if ((int)prmPolicyState[j] != 0)
                    {
                        if (j == 0)
                        {
                            strQuery = strQuery + " and (B.status=" + 3;
                        }
                        else
                            strQuery = strQuery + " or B.status=" + 3;
                    }
                }
                strQuery = strQuery + " )";
                wAdap = new OdbcDataAdapter(strQuery, sqlCon);
                wAdap.Fill(ds);
            }
            catch (Exception EX)
            {
                wAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(strQuery + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(EX, this);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                wic = new CtrlImage((int)ds.Tables[0].Rows[i]["proj_key"], (int)ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["box_number"].ToString(), (string)ds.Tables[0].Rows[i]["policy_number"].ToString(), ds.Tables[0].Rows[i]["page_name"].ToString(), ds.Tables[0].Rows[i]["doc_type"].ToString());
                arrItem.Add(wic);
            }
            return arrItem;
        }
        public ArrayList GetDeletedPageList3(eSTATES[] prmImageState, wItem wi)
        {
            ArrayList arrItem = new ArrayList();
            OdbcDataAdapter wAdap = null;
            DataSet ds = new DataSet();
            string strQuery = null;

            try
            {
                wfePolicy queryPolicy = (wfePolicy)wi;
                strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.doc_type from image_master A,metadata_entry B where A.proj_key=B.proj_code and A.batch_key=B.batch_key and A.policy_number=B.filename and A.proj_key=" + queryPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + queryPolicy.ctrlPolicy.BatchKey + " and A.box_number='" + queryPolicy.ctrlPolicy.BoxNumber + "' and A.policy_number='" + queryPolicy.ctrlPolicy.PolicyNumber + "' and A.status= " + (int)prmImageState[0] + " and (B.status=" + 4;
                //for (int j = 0; j < prmPolicyState.Length; j++)
                //{
                //    if ((int)prmPolicyState[j] != 0)
                //    {
                //        if (j == 0)
                //        {
                //            strQuery = strQuery + " and (B.status=" + 4;
                //        }
                //        else
                //            strQuery = strQuery + " or B.status=" + 4;
                //    }
                //}
                strQuery = strQuery + " or B.status = '30' or B.status = '31' or B.status = '7' or B.status = '8' or B.status = '9' or B.status = '40' or B.status = '41' or B.status=" + 5 + " or B.status=" + 6 + ")";
                wAdap = new OdbcDataAdapter(strQuery, sqlCon);
                wAdap.Fill(ds);
            }
            catch (Exception EX)
            {
                wAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(strQuery + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(EX, this);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                wic = new CtrlImage((int)ds.Tables[0].Rows[i]["proj_key"], (int)ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["box_number"].ToString(), (string)ds.Tables[0].Rows[i]["policy_number"].ToString(), ds.Tables[0].Rows[i]["page_name"].ToString(), ds.Tables[0].Rows[i]["doc_type"].ToString());
                arrItem.Add(wic);
            }
            return arrItem;
        }
		public bool Save(Credentials prmCrd,eSTATES prmState,double prmImageSize,int prmPhoto,int prmSrlNo,string prmIndexImageName)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			
			switch (prmState)
			{
				case eSTATES.PAGE_SCANNED:
                    sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,scanned_size,photo,serial_no,page_index_name) values(" +
						ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber +"'"+
						",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + "," + prmImageSize + "," + prmPhoto + "," + prmSrlNo + ",'" + prmIndexImageName + "')";
					break;
				case eSTATES.PAGE_QC:
                    sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,QC_size,photo,serial_no,page_index_name) values(" +
                        ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "'" +
						",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + "," + prmImageSize + "," + prmPhoto + "," + prmSrlNo + ",'" + prmIndexImageName + "')";
					break;
				case eSTATES.PAGE_RESCANNED_NOT_INDEXED:
                    sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,qc_size,photo,serial_no,page_index_name) values(" +
                        ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "'" +
						",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + "," + prmImageSize + "," + prmPhoto + "," + prmSrlNo + ",'" + prmIndexImageName + "')";
					break;
                case eSTATES.PAGE_EXPORTED:
                    sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,qc_size,photo,serial_no,page_index_name) values(" +
                        ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "'" +
                        ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + "," + prmImageSize + "," + prmPhoto + "," + prmSrlNo + ",'" + prmIndexImageName + "')";
                    break;
                case eSTATES.PAGE_FQC:
                    sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,QC_size) values(" +
                        ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "'" +
                        ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + "," + prmImageSize + ")";
                    break;
                case eSTATES.PAGE_NOT_INDEXED:
                    sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,QC_size,serial_no,page_index_name) values(" +
                        ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "'" +
                        ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + "," + prmImageSize + ", " + prmSrlNo + ",'" + ctrlImage.ImageName + "')";
                    break;
			}
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
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n" + "Wfe State--" + Convert.ToString(Convert.ToInt32(prmState)) + "\n");
                
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}

        public bool Save(Credentials prmCrd, eSTATES prmState,string prmDocType,string prmIndexName,int prmSerialNo)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

                
            sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,page_index_name,doc_type,serial_no) values(" +
                ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "'" +
                ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + ",'" + prmIndexName + "','" + prmDocType + "'," + prmSerialNo + ")";
    
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

        public bool Save(Credentials prmCrd, eSTATES prmState, string prmDocType, string prmIndexName,double prmImageSize,int prmSrlNo)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
			
            int imageSize=Convert.ToInt32(prmImageSize);

            sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,page_index_name,doc_type,serial_no,qc_size) values(" +
                ctrlImage.ProjectKey + "," + ctrlImage.BatchKey + ",'" + ctrlImage.BoxNumber + "','" + ctrlImage.PolicyNumber + "'" +
                ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','" + ctrlImage.ImageName + "'," + (int)prmState + ",'" + prmIndexName + "','" + prmDocType + "'," + prmSrlNo + "," + imageSize + ")";

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

		public bool DeletePage()
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();

            sqlStr = @"delete from image_master where proj_key=" + ctrlImage.ProjectKey + " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "' and policy_number='" + ctrlImage.PolicyNumber + "'" +
				" and page_name='" + ctrlImage.ImageName + "'";
				
			try
			{
					sqlTrans=sqlCon.BeginTransaction();
					sqlCmd.Connection = sqlCon;
					sqlCmd.Transaction=sqlTrans;
	                sqlCmd.CommandText = sqlStr;
	                int i= sqlCmd.ExecuteNonQuery();
	                sqlTrans.Commit();
	                if(i>0)
	                {
	                	commitBol=true;
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
		public bool UpdateImageSize(Credentials prmCrd,eSTATES prmState,double prmImageSize)
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			
			OdbcCommand sqlCmd=new OdbcCommand();
			int pos;
			string originalImage;
            ///changed on version 1.0.0.1 for update image size in qc_size field
            int imageSize =Convert.ToInt32(prmImageSize);///changed line
			pos = ctrlImage.ImageName.IndexOf("-");	
			if(pos > 0)
			{
				originalImage = ctrlImage.ImageName.Substring(0,pos);	
			}
			else
			{
				 originalImage= ctrlImage.ImageName;
			}
			switch (prmState)
			{
				
				case eSTATES.PAGE_QC:
					sqlStr=@"update image_master" +
						" set QC_size=" + prmImageSize + " where proj_key=" + ctrlImage.ProjectKey +
						" and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber+"'" +
						" and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + originalImage + "'";
					break;
				case eSTATES.PAGE_INDEXED:
					sqlStr=@"update image_master" +
						" set qc_size=" + imageSize + " where proj_key=" + ctrlImage.ProjectKey +
                        " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
						" and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + originalImage + "'";	
					break;
				case eSTATES.PAGE_FQC:
					sqlStr=@"update image_master" +
						" set qc_size=" + imageSize + " where proj_key=" + ctrlImage.ProjectKey +
                        " and batch_key=" + ctrlImage.BatchKey + " and box_number='" + ctrlImage.BoxNumber + "'" +
						" and policy_number='" + ctrlImage.PolicyNumber + "' and page_name='" + originalImage + "'";	
					break;
			}
			
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
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n" + "Wfe State--" + Convert.ToString(Convert.ToInt32(prmState)) + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return commitBol;
		}
	}
}
