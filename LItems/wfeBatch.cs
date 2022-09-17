/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 19/2/2008
 * Time: 4:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;
using NovaNet.Utils;
using NovaNet.wfe;

namespace LItems
{
	public class udtBatch: NovaNet.wfe.udtCmd
	{
		public int batch_key; 
		public int proj_code; 
		public string batch_code;
		public string batch_name;
		public string Created_By;
		public string Created_DTTM;
		public string Modified_By;
		public string Modified_DTTM;
	}
	public class CtrlBatch: NovaNet.wfe.wItemControl
	{
		private int batch_key; 
		private int proj_code; 
		private string Code;
		private string batch_name;
		
		public CtrlBatch(int key, string code,int projKey,string batchName)
		{
			batch_key = key;
			Code = code;
			proj_code=projKey;
			batch_name=batchName;
		}
        public CtrlBatch(int projKey, int key)
        {
            batch_key = key;
            proj_code = projKey;
        }
		public int BatchKey
		{
			get
			{
				return batch_key;
			}
		}
		public string BatchCode
		{
			get
			{
				return Code;
			}
		}
		public int ProjectCode
		{
			get
			{
				return proj_code;
			}
		}
		public string BatchName
		{
			get
			{
				return batch_name;
			}
		}
	}
	/// <summary>
	/// Description of wfeBatch.
	/// </summary>
	public class wfeBatch:wItem,ErrReporter,StateData
	{
		private udtBatch objBatch;
		private Hashtable errList;
		private OdbcConnection sqlCon=null;
		private OdbcDataAdapter sqlAdap=null;
		private DataSet dsPath=null;
		private wfeProject objProj=null;
		private INIReader rd=null;
		private KeyValueStruct udtKeyValue;
		public string err=null;
		private int projCode;
        MemoryStream stateLog;
        byte[] tmpWrite;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);	
		public wfeBatch(OdbcConnection prmCon): base(prmCon, NovaNet.Utils.Constants._ADDING)
		{
			sqlCon=prmCon;
            exMailLog.SetNextLogger(exTxtLog);
            
		}
		public wfeBatch(String prmCtrl, OdbcConnection prmCon): base(prmCon, NovaNet.Utils.Constants._EDITING)
		{
			sqlCon=prmCon;
            exMailLog.SetNextLogger(exTxtLog);
            
		}
		
        public int ProjectCode
		{
        	set
        	{
        		projCode=value;
        	}
			get
			{
				return projCode;
			}
		}
        
//		public override bool Display()
//		{
//			frmAddEdit frmDisp;
//			frmMain frmMan=new frmMain();
//			if (mode==1)
//				frmDisp = new aeBatch(this);
//			else
//				frmDisp = new aeBatch();
//			frmDisp.ShowDialog(frmMan);
//			return true;
//		}
		public override bool TransferValues(udtCmd cmd)
		{
			
			objBatch = (udtBatch) (cmd);
			if (KeyCheck(objBatch.batch_code)==false)
			{
				if (Validate(objBatch)==true)
				{
					if( Commit()==true)
					{
						return true;
					}
					else
					{
						rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
						udtKeyValue.Key=Constants.SAVE_ERROR.ToString();
						udtKeyValue.Section=Constants.COMMON_EXCEPTION_SECTION;
						string ErrMsg=rd.Read(udtKeyValue);
						throw new DbCommitException(ErrMsg);
					}
				}
				else
				{
					//throw new ValidationException(Constants.ValidationException) ;
					return false;
				}
			}
			else
			{
				rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
				udtKeyValue.Key=Constants.DUPLICATE_KEY_CHECK.ToString();
				udtKeyValue.Section=Constants.COMMON_EXCEPTION_SECTION;
				string ErrMsg=rd.Read(udtKeyValue);
				throw new KeyCheckException(ErrMsg);
			}
		}

       

		public override udtCmd LoadValuesFromDB()
		{
			throw new NotImplementedException();
		}


        public bool Commit_Bundle(string establishment,string bundle_no,string createDt,string handoverDt)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            string scanbatchPath = null;

            errList = new Hashtable();
            objProj = new wfeProject(dbcon);

            dsPath = objProj.GetPath(objBatch.proj_code);

            if (dsPath.Tables[0].Rows.Count > 0)
            {
                scanbatchPath = dsPath.Tables[0].Rows[0]["project_Path"] + "\\" + objBatch.batch_code;
            }

            sqlStr = @"insert into bundle_master(proj_code,bundle_code,bundle_name,created_by" +
                ",Created_DTTM,establishment,bundle_no,creation_date,handover_date,bundle_path) values(" +
                objBatch.proj_code + ",'" + objBatch.batch_code.ToUpper() + "','" + objBatch.batch_name + "'," +
                "'" + objBatch.Created_By + "','" + objBatch.Created_DTTM + "','"+establishment+"','"+bundle_no+"','"+createDt+"','"+handoverDt+"','" +
                scanbatchPath.Replace("\\", "\\\\") + "')";
            try
            {
                if (KeyCheck(objBatch.batch_code) == false)
                {
                    sqlTrans = sqlCon.BeginTransaction();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.Transaction = sqlTrans;
                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();

                    if (mode == Constants._ADDING)
                    {
                        if (FileorFolder.CreateFolder(scanbatchPath) == true)
                        {
                            commitBol = true;
                            sqlTrans.Commit();
                        }
                        else
                        {
                            commitBol = false;
                            sqlTrans.Rollback();
                            rd = new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                            udtKeyValue.Key = Constants.BATCH_FOLDER_CREATE_ERROR.ToString();
                            udtKeyValue.Section = Constants.BATCH_EXCEPTION_SECTION;
                            string ErrMsg = rd.Read(udtKeyValue);
                            throw new CreateFolderException(ErrMsg);
                        }
                    }
                    else
                    {
                        commitBol = true;
                        sqlTrans.Commit();
                    }
                }
                else
                    commitBol = false;
            }
            catch (Exception ex)
            {
                errList.Add(Constants.DBERRORTYPE, ex.Message);
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

		public override bool Commit()
		{
			string sqlStr=null;
			OdbcTransaction sqlTrans=null;
			bool commitBol=true;
			OdbcCommand sqlCmd=new OdbcCommand();
			string scanbatchPath=null;

			errList=new Hashtable();
			objProj=new wfeProject(dbcon);

			dsPath=objProj.GetPath(objBatch.proj_code);

			if (dsPath.Tables[0].Rows.Count>0)
	        {
				scanbatchPath=dsPath.Tables[0].Rows[0]["project_Path"] + "\\" + objBatch.batch_code;
			}
			
			sqlStr=@"insert into batch_master(proj_code,batch_code,batch_name,created_by" +
				",Created_DTTM,batch_path) values(" +
				objBatch.proj_code + ",'" + objBatch.batch_code.ToUpper() + "','" + objBatch.batch_name + "'," +
				"'" + objBatch.Created_By + "','" + objBatch.Created_DTTM + "','" +
				scanbatchPath.Replace("\\","\\\\") + "')";
			try
			{
				if (KeyCheck(objBatch.batch_code)==false)
				{
					sqlTrans=sqlCon.BeginTransaction();
					sqlCmd.Connection = sqlCon;
					sqlCmd.Transaction=sqlTrans;
	                sqlCmd.CommandText = sqlStr;
	                sqlCmd.ExecuteNonQuery();
	            	
	                if (mode==Constants._ADDING)
	                {
						if (FileorFolder.CreateFolder(scanbatchPath)==true)
						{
		            		commitBol=true;    
		            		sqlTrans.Commit();
						}
						else
	                	{
		                	commitBol=false;    
		                	sqlTrans.Rollback();
		                	rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
							udtKeyValue.Key=Constants.BATCH_FOLDER_CREATE_ERROR.ToString();
							udtKeyValue.Section=Constants.BATCH_EXCEPTION_SECTION;
							string ErrMsg=rd.Read(udtKeyValue);
							throw new CreateFolderException(ErrMsg);
	                	}
	                }
	                else
	                {
	                	commitBol=true;    
						sqlTrans.Commit();
	                }
				}
				else
					commitBol=false;
			}
			catch(Exception ex)
			{
				errList.Add(Constants.DBERRORTYPE,ex.Message);
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
		
		public override bool KeyCheck(string prmValue)
		{
			string sqlStr=null;
			OdbcCommand cmd=null;
			bool existsBol=true;

			sqlStr="select batch_code from batch_master where batch_code='" + prmValue.ToUpper() + "'";
			cmd=new OdbcCommand(sqlStr,sqlCon);
			existsBol=cmd.ExecuteReader().HasRows;
				
			return existsBol;
		}
		
		public Hashtable GetErrors()
		{
			return errList;
		}
        MemoryStream StateData.StateLog()
        {
            return stateLog;
        }
		private bool Validate(udtBatch cmd)
			
		{
			bool validateBol=true;
			errList = new Hashtable();
			if (cmd.batch_code==string.Empty || KeyCheck(cmd.batch_code)==true)
			{
				validateBol=false;
				errList.Add("Code",Constants.NOT_VALID);
			}
						
			if (cmd.batch_name==string.Empty)
			{
				validateBol=false;
				errList.Add("Name",Constants.NOT_VALID);
			}
											
			if (cmd.Created_By==string.Empty && mode==Constants._ADDING)
			{
				validateBol=false;
				errList.Add("Created_By",Constants.NOT_VALID);
			}
			
			if (cmd.Created_DTTM==string.Empty && mode==Constants._ADDING)
			{
				validateBol=false;
				errList.Add("Created_DTTM",Constants.NOT_VALID);
			}
						
			///Required at the time of editing
			if (cmd.Modified_By==string.Empty && mode==Constants._EDITING)
			{
				validateBol=false;
				errList.Add("Modified_By",Constants.NOT_VALID);
			}
						
			if (cmd.Modified_DTTM==string.Empty && mode==Constants._EDITING)
			{
				validateBol=false;
				errList.Add("Modified_DTTM",Constants.NOT_VALID);
			}
						
			return validateBol;
		}
        public System.Data.DataSet GetAllValuesBundle(int prmProjectKey)
        {
            string sqlStr = null;

            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select bundle_key,bundle_name from bundle_master where proj_code=" + prmProjectKey + " order by bundle_name";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return batchDs;
        }
        public System.Data.DataSet GetAllValuesFile(int prmProjectKey, int bundleKey)
        {
            string sqlStr = null;

            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select case_file_no from case_file_master where proj_code=" + prmProjectKey + " and bundle_key = "+bundleKey+" order by item_no";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return batchDs;
        }
		public System.Data.DataSet GetAllValues(int prmProjectKey)
		{
			string sqlStr=null;
			
			DataSet batchDs=new DataSet();
			
			try 
			{
				sqlStr="select batch_key,batch_name from batch_master where proj_code=" + prmProjectKey + " order by batch_name";
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(batchDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
				err=ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			return batchDs;
		}
        public System.Data.DataSet GetAllValuesExported(int prmProjectKey,bool pReExport)
        {
            string sqlStr = null;

            DataSet batchDs = new DataSet();

            try
            {
                //changed for UAT
                if (pReExport == false)
                {
                    sqlStr = "select a.batch_key,a.batch_name from batch_master a,box_master b where a.proj_code =" + prmProjectKey + " and a.proj_code = b.proj_key and a.batch_key = b.batch_key and a.status = '7' and b.exported = 'N'";
                }
                else
                {
                    sqlStr = "select a.batch_key,a.batch_name from batch_master a,box_master b where a.proj_code =" + prmProjectKey + " and a.proj_code = b.proj_key and a.batch_key = b.batch_key and (a.status = '8' or a.status = '15') and b.exported = 'N'";
                }
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return batchDs;
        }
        public System.Data.DataSet GetAllValues(int prmProjectKey,eSTATES[] bState)
        {
            string sqlStr = null;

            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select batch_key,batch_name from batch_master where proj_code='" + prmProjectKey+"' order by created_dttm";
                for (int j = 0; j < bState.Length; j++)
                {
                    if ((int)bState[j] != 0)
                    {
                        if (j == 0)
                        {
                            sqlStr = sqlStr + " and (status=" + (int)bState[j];
                        }
                        else
                            sqlStr = sqlStr + " or status=" + (int)bState[j];
                    }
                }        
                sqlStr = sqlStr + ") order by batch_name";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            return batchDs;
        }
        public string GetPath(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();
            string Path;

            try
            {
                sqlStr = @"select batch_path from batch_master where proj_code=" + prmProjKey + " and batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                Path = projDs.Tables[0].Rows[0]["batch_path"].ToString();
            }
            else
                Path = string.Empty;

            return Path;
        }
        public bool PolicyWithLICException(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();

            try
            {
                sqlStr = @"select filename from metadata_entry where proj_code=" + prmProjKey + " and batch_key=" + prmBatchKey + " and status="+ (int) eSTATES.POLICY_EXCEPTION;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool PolicyNotIndexed(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();

            try
            {
                sqlStr = @"select policy_number from policy_master where proj_key=" + prmProjKey + " and batch_key=" + prmBatchKey + " and status=" + (int)eSTATES.POLICY_NOT_INDEXED;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public int GetBatchStatus(int prmBatchKey)
        {
            string sqlStr = null;
            int status = 0;
            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select status from batch_master where batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
            }
            if (batchDs.Tables[0].Rows.Count > 0)
            {
                status =Convert.ToInt32(batchDs.Tables[0].Rows[0]["status"].ToString());
            }
            
            return status;
        }

        public bool UpdateBundleStatus(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update bundle_master" +
                " set status=" + 2 + " where " +
                " bundle_key=" + prmBatchKey + " and proj_code = '"+prmProjKey+"' and status = '1' and status<>" + 2;

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

        public bool UpdateStatus(eSTATES state,int prmBatchKey)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update batch_master" +
                " set status=" + (int)state + " where " +
            	" batch_key=" + prmBatchKey + " and status<>" + (int)eSTATES.BATCH_READY_FOR_UAT;

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

        public bool RejectBatch(int prmBatchKey,eSTATES pBatchStatus,eSTATES pBoxStatus,eSTATES pPolicyStatus,eSTATES pImageStatus)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update batch_master" +
                " set status=" + (int)pBatchStatus + " where " +
                " batch_key=" + prmBatchKey;

            try
            {
                ///For initialize batch
                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                ///For initialize box muster against batch
                sqlStr = @"update box_master" +
                " set status=" + (int)pBoxStatus + " where " +
                " batch_key=" + prmBatchKey;

                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                ///For initialize policy muster against batch
                sqlStr = @"update policy_master" +
                " set status=" + (int)pPolicyStatus + " where " +
                " batch_key=" + prmBatchKey + " and (status=" + (int)eSTATES.POLICY_FQC + " or status = " + (int)eSTATES.POLICY_EXCEPTION + " or status=" + (int)eSTATES.POLICY_CHECKED + ")";

                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();

                ///For initialize image muster against batch
                sqlStr = @"update image_master" +
                " set status=" + (int)pImageStatus + " where " +
                " batch_key=" + prmBatchKey + " and (status<>29)";

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

		public string GetBatchName(int prmProjectKey,int prmBatchKey)
		{
			string sqlStr=null;
			string batchName=null;
			
			DataSet batchDs=new DataSet();
			
			try 
			{
				sqlStr="select batch_name from batch_master where proj_code=" + prmProjectKey + " and batch_key=" + prmBatchKey;
				sqlAdap=new OdbcDataAdapter(sqlStr,sqlCon);
				sqlAdap.Fill(batchDs);
			}
			catch (Exception ex) 
			{
				sqlAdap.Dispose();
				err=ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex, this);
			}
			if(batchDs.Tables[0].Rows.Count>0)
			{
				batchName=batchDs.Tables[0].Rows[0]["batch_name"].ToString();
			}
			else
				batchName=string.Empty;
			return batchName;
		}
        public string GetBundleName(int prmProjectKey, int prmBundleKey)
        {
            string sqlStr = null;
            string projName = null;

            DataSet projDs = new DataSet();

            try
            {
                sqlStr = "select batch_code,batch_name from batch_master where proj_code= '" + prmProjectKey + "' and batch_key = '" + prmBundleKey + "' ";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                projName = projDs.Tables[0].Rows[0]["batch_code"].ToString();
            }
            else
                projName = string.Empty;
            return projName;
        }
		
	}
}
