/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 19/3/2009
 * Time: 7:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NovaNet.wfe;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using LItems;
using NovaNet.Utils;

namespace LItems
{
	/// <summary>
	/// Description of ihwQuery.
	/// </summary>
	public class ihwQuery: wQuery
	{
		private OdbcConnection sqlCon=null;
        private int photo=-1;
        private Credentials crd;
        private int stage;
		public ihwQuery(OdbcConnection prmCon)
		{
			sqlCon=prmCon;
		}
        public ihwQuery(OdbcConnection prmCon,int prmPhoto)
        {
            sqlCon = prmCon;
            photo = prmPhoto;
        }
        public ihwQuery(OdbcConnection prmCon, int prmPhoto,int pStage)
        {
            sqlCon = prmCon;
            photo = prmPhoto;
            stage = pStage;
        }
        public ihwQuery(OdbcConnection prmCon,Credentials pCrd)
        {
            sqlCon = prmCon;
            crd = pCrd;
        }
        public string GetSysConfigValue(string pKey)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select sysvalues from sysconfig " +
                    " where syskeys='" + pKey + "'";
                

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                sqlAdap.Dispose();
            }
            return dsImage.Tables[0].Rows[0]["sysvalues"].ToString();
        }
        public override DataSet GetDeedVolume(string proj_key,string batch_key,string box_key,string pPolicyNo)
        {
            string sqlStr = null;
            DataSet dsVol = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select deed_vol,page_from,page_to from policy_master where proj_key = '" + proj_key + "' and batch_key = '"+batch_key+"' and box_number = '"+box_key+"' and policy_number='"+ pPolicyNo +"'";


            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsVol);
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                sqlAdap.Dispose();
            }
            return dsVol;
        }
        public override DataSet GetDeedVolume(string deed_no)
        {
            string sqlStr = null;
            DataSet dsVol = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select deed_vol,page_from,page_to from policy_master where policy_no = '"+deed_no+"'";


            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsVol);
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                sqlAdap.Dispose();
            }
            return dsVol;
        }
        public override DataSet GetIndexDetails(string deed_no,string deed_year)
        {
            string sqlStr = null;
            DataSet dsVol = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select letter_no,a.deed_no,b.ec_name,CONCAT(a.First_name , ' '  ,a.LAST_name) as name from index_of_name a,party_code b where a.party_code = b.ec_code and a.deed_no = '" + deed_no + "' ";


            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsVol);
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                sqlAdap.Dispose();
            }
            return dsVol;
        }
		public override ArrayList GetItems(eITEMS item, eSTATES[] state, wItem wi)
		{
			OdbcDataAdapter wAdap;
            OdbcTransaction trns = null;
            OdbcCommand oCom = new OdbcCommand();
			string strQuery=null;
			wItemControl wic=null;
			DataSet ds=new DataSet();
			ArrayList arrItem=new ArrayList();
			//string docType=null;
			
			switch (item)
			{
				case eITEMS.PROJECT:
						strQuery = "select proj_key, proj_code from Project_master where" ;
						for(int j=0;j<state.Length;j++)
						{
							strQuery=strQuery + state[j] + " or ";
						}
						strQuery=System.Text.RegularExpressions.Regex.Replace(strQuery, " or $", "");
						wAdap=new OdbcDataAdapter(strQuery,sqlCon);
						wAdap.Fill(ds);
						for(int i=0;i<ds.Tables[0].Rows.Count;i++)
						{
							wic = new CtrlProject((int)ds.Tables[0].Rows[i]["proj_key"], ds.Tables[0].Rows[i]["proj_code"].ToString());
							arrItem.Add (wic);
						}
						break;
				case eITEMS.BATCH:
						strQuery = "select batch_key, proj_code,batch_code,batch_name from batch_master where" ;
						for(int j=0;j<state.Length;j++)
						{
							strQuery=strQuery + state[j] + " or ";
						}
						strQuery=System.Text.RegularExpressions.Regex.Replace(strQuery, " or $", "");
						wAdap=new OdbcDataAdapter(strQuery,sqlCon);
						wAdap.Fill(ds);
						for(int i=0;i<ds.Tables[0].Rows.Count;i++)
						{
							wic = new CtrlBatch((int)ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["batch_code"].ToString(), (int)ds.Tables[0].Rows[i]["proj_code"], ds.Tables[0].Rows[i]["batch_name"].ToString());
							arrItem.Add (wic);
						}
						break;
				case eITEMS.BOX:
						strQuery = "select proj_key,batch_key,box_number from box_master where" ;
						for(int j=0;j<state.Length;j++)
						{
							strQuery=strQuery + state[j] + " or ";
						}
						strQuery=System.Text.RegularExpressions.Regex.Replace(strQuery, " or $", "");
						wAdap=new OdbcDataAdapter(strQuery,sqlCon);
						wAdap.Fill(ds);
						for(int i=0;i<ds.Tables[0].Rows.Count;i++)
						{
							wic = new CtrlBox((int)ds.Tables[0].Rows[i]["proj_key"],(int) ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["box_number"].ToString());
							arrItem.Add (wic);
						}
						break;
				case eITEMS.POLICY:
                    wfeBox queryBox = (wfeBox)wi;
                    if (state.Length == 0)
                    {
                        if (photo == -1)
                        {
                            strQuery = "select proj_key,batch_key,box_number,policy_number from policy_master where count_of_pages is not null  and proj_key=" + queryBox.ctrlBox.ProjectCode + " and batch_key=" + queryBox.ctrlBox.BatchKey + " and box_number='" + queryBox.ctrlBox.BoxNumber+"'";
                            if (state.Length != 0)
                            {
                                for (int j = 0; j < state.Length; j++)
                                {
                                    if ((int)state[j] != 0)
                                    {
                                        if (j == 0)
                                        {
                                            strQuery = strQuery + " and (status=" + (int)state[j];
                                        }
                                        else
                                            strQuery = strQuery + " or status=" + (int)state[j];
                                    }
                                }
                                strQuery = strQuery + ") order by Convert(page_from,signed integer)";
                            }
                            else
                            {
                                strQuery = strQuery + " and 1=1 order by Convert(page_from,signed integer)";
                            }
                        }
                        else
                        {
                            strQuery = "select proj_key,batch_key,box_number,policy_number from policy_master where count_of_pages is not null and proj_key=" + queryBox.ctrlBox.ProjectCode + " and batch_key=" + queryBox.ctrlBox.BatchKey + " and box_number='" + queryBox.ctrlBox.BoxNumber+"'";
                            for (int j = 0; j < state.Length; j++)
                            {
                                if ((int)state[j] != 0)
                                {
                                    if (j == 0)
                                    {
                                        strQuery = strQuery + " and (status=" + (int)state[j];
                                    }
                                    else
                                        strQuery = strQuery + " or status=" + (int)state[j];
                                }
                            }
                            if (photo == 1)
                            {
                                strQuery = strQuery + ") and photo=0 ";
                            }
                            else
                            {
                                strQuery = strQuery + ") and photo=1 ";
                            }
                            strQuery = strQuery + "order by Convert(page_from,signed integer)";
                        }
                        wAdap = new OdbcDataAdapter(strQuery, sqlCon);
                        wAdap.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            wic = new CtrlPolicy((int)ds.Tables[0].Rows[i]["proj_key"], (int)ds.Tables[0].Rows[i]["batch_key"], ds.Tables[0].Rows[i]["box_number"].ToString(), ds.Tables[0].Rows[i]["policy_number"].ToString());
                            arrItem.Add(wic);
                        }
                    }
                    else
                    {
                        if ((state[0] == eSTATES.POLICY_FQC) || (state[0] == eSTATES.POLICY_EXCEPTION))
                        {
                            if (photo == -1)
                            {
                                strQuery = "select proj_key,batch_key,box_number,policy_number from policy_master where count_of_pages is not null  and proj_key=" + queryBox.ctrlBox.ProjectCode + " and batch_key=" + queryBox.ctrlBox.BatchKey + " and box_number='" + queryBox.ctrlBox.BoxNumber+"'";
                                if (state.Length != 0)
                                {
                                    for (int j = 0; j < state.Length; j++)
                                    {
                                        if ((int)state[j] != 0)
                                        {
                                            if (j == 0)
                                            {
                                                strQuery = strQuery + " and (status=" + (int)state[j];
                                            }
                                            else
                                                strQuery = strQuery + " or status=" + (int)state[j];
                                        }
                                    }
                                    strQuery = strQuery + ") order by Convert(page_from,signed integer)";
                                }
                                else
                                {
                                    strQuery = strQuery + " and 1=1 order by Convert(page_from,signed integer)";
                                }
                            }
                            else
                            {
                                strQuery = "select proj_key,batch_key,box_number,policy_number from policy_master where count_of_pages is not null and proj_key=" + queryBox.ctrlBox.ProjectCode + " and batch_key=" + queryBox.ctrlBox.BatchKey + " and box_number='" + queryBox.ctrlBox.BoxNumber+"'";
                                for (int j = 0; j < state.Length; j++)
                                {
                                    if ((int)state[j] != 0)
                                    {
                                        if (j == 0)
                                        {
                                            strQuery = strQuery + " and (status=" + (int)state[j];
                                        }
                                        else
                                            strQuery = strQuery + " or status=" + (int)state[j];
                                    }
                                }
                                if (photo == 1)
                                {
                                    strQuery = strQuery + ") and photo=0 ";
                                }
                                else
                                {
                                    strQuery = strQuery + ") and photo=1 ";
                                }
                                strQuery = strQuery + "order by Convert(page_from,signed integer)";
                            }
                            wAdap = new OdbcDataAdapter(strQuery, sqlCon);
                            wAdap.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                wic = new CtrlPolicy((int)ds.Tables[0].Rows[i]["proj_key"], (int)ds.Tables[0].Rows[i]["batch_key"], (string)ds.Tables[0].Rows[i]["box_number"].ToString(), (string)ds.Tables[0].Rows[i]["policy_number"].ToString());
                                arrItem.Add(wic);
                            }
                        }
                        else
                        {
                            if (photo == -1)
                            {
                                strQuery = "select proj_key,batch_key,box_number,policy_number from policy_master where (count_of_pages is not null  and proj_key=" + queryBox.ctrlBox.ProjectCode + " and batch_key=" + queryBox.ctrlBox.BatchKey + " and box_number='" + queryBox.ctrlBox.BoxNumber + "')";
                                if (state.Length != 0)
                                {
                                    for (int j = 0; j < state.Length; j++)
                                    {
                                        if ((int)state[j] != 0)
                                        {
                                            if (j == 0)
                                            {
                                                strQuery = strQuery + " and (status=" + (int)state[j];
                                            }
                                            else
                                                strQuery = strQuery + " or status=" + (int)state[j];
                                        }
                                    }
                                    if ((state[0] == eSTATES.POLICY_QC) || (state[0] == eSTATES.POLICY_SCANNED))
                                    {
                                        if (stage != 1)
                                        {
                                            strQuery = strQuery + ") and (locked_uid='" + crd.created_by + "' or expires_dttm <= NOW() or invalid = 0) and 1=1 order by Convert(page_from,signed integer) LIMIT 1 for update";
                                        }
                                        else
                                        {
                                            strQuery = strQuery + " order by Convert(page_from,signed integer)";
                                        }
                                    }
                                    else
                                    {
                                        strQuery = strQuery + " order by Convert(page_from,signed integer)";
                                    }
                                }
                                else
                                {
                                    if ((state[0] == eSTATES.POLICY_QC) || (state[0] == eSTATES.POLICY_SCANNED))
                                    {
                                        if (stage != 1)
                                        {
                                            strQuery = strQuery + " and (locked_uid='" + crd.created_by + "' or expires_dttm <= NOW() or invalid = 0) and 1=1 order by Convert(page_from,signed integer) LIMIT 1 for update";
                                        }
                                        else
                                        {
                                            strQuery = strQuery + " order by Convert(page_from,signed integer)";
                                        }
                                    }
                                    else
                                    {
                                        strQuery = strQuery + " order by Convert(page_from,signed integer)";
                                    }
                                }
                            }
                            else
                            {
                                strQuery = "select proj_key,batch_key,box_number,policy_number from policy_master where (proj_key=" + queryBox.ctrlBox.ProjectCode + " and batch_key=" + queryBox.ctrlBox.BatchKey + " and box_number='" + queryBox.ctrlBox.BoxNumber + "')";
                                for (int j = 0; j < state.Length; j++)
                                {
                                    if ((int)state[j] != 0)
                                    {
                                        if (j == 0)
                                        {
                                            strQuery = strQuery + " and (status=" + (int)state[j];
                                        }
                                        else
                                            strQuery = strQuery + " or status=" + (int)state[j];
                                    }
                                }
                                if (photo == 1)
                                {
                                    strQuery = strQuery + ") and photo=0 ";
                                }
                                else
                                {
                                    strQuery = strQuery + ") and photo=1 ";
                                }
                                if ((state[0] == eSTATES.POLICY_QC) || (state[0] == eSTATES.POLICY_SCANNED))
                                {
                                    if (stage != 1)
                                    {
                                        strQuery = strQuery + ") and (locked_uid='" + crd.created_by + "' or expires_dttm <= NOW() or invalid = 0) and 1=1 order by Convert(page_from,signed integer) LIMIT 1 for update";
                                    }
                                    else
                                    {
                                        strQuery = strQuery + " order by Convert(page_from,signed integer)";
                                    }
                                }
                                else
                                {
                                    strQuery = strQuery + " order by Convert(page_from,signed integer)";
                                }
                            }
                            oCom.Connection = sqlCon;
                            if ((state[0] == eSTATES.POLICY_QC) || (state[0] == eSTATES.POLICY_SCANNED))
                            {
                                if (stage != 1)
                                {
                                    trns = sqlCon.BeginTransaction();
                                    oCom.Transaction = trns;
                                }
                            }

                            oCom.CommandText = strQuery;
                            wAdap = new OdbcDataAdapter(oCom);
                               wAdap.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    string p = ds.Tables[0].Rows[i]["proj_key"].ToString();
                                    string b = ds.Tables[0].Rows[i]["batch_key"].ToString();
                                    string bb = ds.Tables[0].Rows[i]["box_number"].ToString();
                                    string pp = ds.Tables[0].Rows[i]["policy_number"].ToString();
                                    wic = new CtrlPolicy(Convert.ToInt32(ds.Tables[0].Rows[i]["proj_key"]), Convert.ToInt32(ds.Tables[0].Rows[i]["batch_key"]), ds.Tables[0].Rows[i]["box_number"].ToString(), ds.Tables[0].Rows[i]["policy_number"].ToString());
                                    arrItem.Add(wic);
                                    if ((state[0] == eSTATES.POLICY_QC) || (state[0] == eSTATES.POLICY_SCANNED))
                                    {
                                        if (stage != 1)
                                        {
                                            CtrlPolicy ctrlPolicy = new CtrlPolicy(Convert.ToInt32(ds.Tables[0].Rows[i]["proj_key"]), Convert.ToInt32(ds.Tables[0].Rows[i]["batch_key"]), ds.Tables[0].Rows[i]["box_number"].ToString(), ds.Tables[0].Rows[i]["policy_number"].ToString());
                                            wfePolicy policy = new wfePolicy(sqlCon, ctrlPolicy);
                                            if (policy.LockPolicy(crd, trns))
                                            {
                                                trns.Commit();
                                            }
                                            else
                                            {
                                                trns.Rollback();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (stage != 1)
                                    trns.Rollback();
                            }
                        }
                    }
					break;
				case eITEMS.PAGE:
						string strQr=string.Empty; 
						wfePolicy queryPolicy = (wfePolicy) wi;
						if((int)state[0] == (int)NovaNet.wfe.eSTATES.POLICY_SCANNED)
						{
                            strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.page_index_name,A.doc_type from image_master A,policy_master B where A.proj_key=B.proj_key and A.batch_key = B.batch_key and A.box_number=B.box_number and A.policy_number = B.policy_number and A.photo <> 1 and A.proj_key=" + queryPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + queryPolicy.ctrlPolicy.BatchKey + " and A.box_number='" + queryPolicy.ctrlPolicy.BoxNumber + "' and A.policy_number='" + queryPolicy.ctrlPolicy.PolicyNumber+"'";
							
						}
                        else
                        {
                            strQr = "select * from (select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.page_index_name,A.doc_type from image_master A inner join policy_master B on A.policy_number = B.policy_number left join doc_type_master c on a.doc_type = c.doc_type where A.proj_key=B.proj_key and A.batch_key = B.batch_key and A.box_number=B.box_number and A.policy_number = B.policy_number and A.proj_key=B.proj_key and A.proj_key=" + queryPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + queryPolicy.ctrlPolicy.BatchKey + " and A.box_number='" + queryPolicy.ctrlPolicy.BoxNumber + "' and A.policy_number='" + queryPolicy.ctrlPolicy.PolicyNumber+"'";
                        }
						for(int j=0;j<state.Length;j++)
						{
							if((int)state[j]!= 0)
							{
								if(j==0)
								{
									strQuery=strQuery + " and (B.status=" + (int)state[j] ;
								}
								else
									strQuery=strQuery + " or B.status=" + (int)state[j] ;
							}
						}
                        if (((int)state[0] == (int)NovaNet.wfe.eSTATES.POLICY_SCANNED)) // || ((int)state[0] == (int)NovaNet.wfe.eSTATES.POLICY_QC))
                        {
                            strQuery = strQuery + ") and A.status<>" + (int)eSTATES.PAGE_DELETED + " order by A.serial_no";
                        }
                        else
                        {
                       		strQuery = strQuery + ") and A.status<>" + (int)eSTATES.PAGE_DELETED + " order by c.srlno,A.serial_no)";
                       		string qr1=strQr + strQuery + " as aa where aa.doc_type is not null union all ";
                       		string qr2=strQr + strQuery + " as aa where aa.doc_type is null";
                       		strQuery = qr1+qr2;
                        }
                        
						//strQuery=System.Text.RegularExpressions.Regex.Replace(strQuery, " or $", "");
						wAdap=new OdbcDataAdapter(strQuery,sqlCon);
						wAdap.Fill(ds);
						for(int i=0;i<ds.Tables[0].Rows.Count;i++)
						{

							wic = new CtrlImage(Convert.ToInt32(ds.Tables[0].Rows[i]["proj_key"].ToString()),Convert.ToInt32(ds.Tables[0].Rows[i]["batch_key"].ToString()), ds.Tables[0].Rows[i]["box_number"].ToString(),ds.Tables[0].Rows[i]["policy_number"].ToString(),ds.Tables[0].Rows[i]["page_name"].ToString(),ds.Tables[0].Rows[i]["doc_type"].ToString());
							arrItem.Add (wic);
						}
						break;
                    case eITEMS.LIC_QA_PAGE:
                        wfePolicy licPolicy = (wfePolicy)wi;
                        strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.page_index_name,A.doc_type from image_master A,policy_master B,doc_type_master c where A.proj_key=B.proj_key and A.batch_key=B.batch_key and A.box_number=B.box_number and A.policy_number = B.policy_number and A.doc_type = c.doc_type and  A.proj_key=" + licPolicy.ctrlPolicy.ProjectKey + " and A.batch_key=" + licPolicy.ctrlPolicy.BatchKey + " and A.box_number=" + licPolicy.ctrlPolicy.BoxNumber + " and A.policy_number='" + licPolicy.ctrlPolicy.PolicyNumber+"'";
                        for (int j = 0; j < state.Length; j++)
                        {
                            if ((int)state[j] != 0)
                            {
                                if (j == 0)
                                {
                                    strQuery = strQuery + " and (B.status=" + (int)state[j];
                                }
                                else
                                    strQuery = strQuery + " or B.status=" + (int)state[j];
                            }
                        }
                        strQuery = strQuery + ") and A.status<>" + (int)eSTATES.PAGE_DELETED + " order by c.srlno,A.serial_no";
                        //strQuery=System.Text.RegularExpressions.Regex.Replace(strQuery, " or $", "");
                        wAdap = new OdbcDataAdapter(strQuery, sqlCon);
                        wAdap.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            wic = new CtrlImage((int)ds.Tables[0].Rows[i]["proj_key"], (int)ds.Tables[0].Rows[i]["batch_key"], (string)ds.Tables[0].Rows[i]["box_number"].ToString(), ds.Tables[0].Rows[i]["policy_number"].ToString(), ds.Tables[0].Rows[i]["page_name"].ToString(), ds.Tables[0].Rows[i]["doc_type"].ToString());
                            arrItem.Add(wic);
                        }
                        break;
			}
			return arrItem;
		}
        
	}
}
