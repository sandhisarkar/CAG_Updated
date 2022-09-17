/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/20/2009
 * Time: 7:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.Odbc;
using NovaNet.Utils;
using System.Data;
using System.Windows.Forms;



namespace NovaNet
{
	namespace Utils
	{

	/// <summary>
	/// Description of AbstRBAC.
	/// </summary>
	public class RBAC: IntrRBAC
	{
		OdbcConnection sqlCon;
		GetProfile CallBack;
		ChangePassword CallCPwd ; 
		dbCon dbn;
		String temp;
		private bool auth;
		Profile prf;
		public RBAC(OdbcConnection prmcon, dbCon db, GetProfile prmGetProfile,ChangePassword prmCpwd)
		{
			auth = false;
			CallBack = prmGetProfile;
			CallCPwd = prmCpwd;
			sqlCon = prmcon;
			dbn = db;
			prf = new Profile();
		}

        public RBAC(OdbcConnection prmcon, GetProfile prmGetProfile, ChangePassword prmCpwd)
        {
            auth = false;
            CallBack = prmGetProfile;
            CallCPwd = prmCpwd;
            sqlCon = prmcon;
            prf = new Profile();
        }
		
		public Credentials getCredentials(Profile prf)
		{
			Credentials crd = new Credentials();
			if (prf.UserId != null)
			{
				crd.created_by = prf.UserId;
				crd.created_dttm = dbn.GetCurrenctDTTM(1,sqlCon);
                ///changed in version 1.0.2
                crd.userName = prf.UserName;
                crd.role = prf.Role_des;
			}
			return (crd);
			
		}
		// Method for User Authentication
		public bool authenticate(String usrID,String usrPWD)
		{
			bool retval = false;
			try
			{
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();
                String sqlStatement = "select A.user_pwd,A.user_name,C.role_description from ac_user A,ac_user_role_map B,ac_role C where A.user_id = B.user_id and B.role_id = C.role_id and A.user_id = '" + usrID + "'";
				OdbcCommand cmd = new OdbcCommand(sqlStatement,sqlCon);
				OdbcDataReader objDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				if(objDataReader.Read())
				{
					if(objDataReader["user_pwd"].ToString() == usrPWD)
					{
						prf.UserId=usrID.Trim();
						prf.UserName=objDataReader["user_name"].ToString().Trim();
                        prf.Password = usrPWD.Trim();
                        prf.Role_des = objDataReader["role_description"].ToString().Trim();
						auth=true;
						retval = true;
					}
					else
					{
						auth=false;
						MessageBox.Show("Invalid Password");
						retval = false;						
					}
				}
				else
				{
					auth=false;
					MessageBox.Show("User ID not found");
					retval = false;
					
				}
				//objDataReader.Close();
                //sqlCon.Close();
			}
			catch(Exception ex)
			{
				auth=false;
                string err = ex.Message;
				retval = false;
			}
			if (auth==false)
			{
				prf.UserId=null;
				prf.UserName=null;
				prf.Password=null;
			}
			return retval;

		}
        public bool CheckUserIsLogged(String usrID)
        {
            bool retval = false;
            try
            {
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();
                String sqlStatement = "select A.user_id from ac_user A where A.user_id = '" + usrID + "' and Logged=0";
                OdbcCommand cmd = new OdbcCommand(sqlStatement, sqlCon);
                OdbcDataReader objDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (objDataReader.Read())
                {
                    if (objDataReader["user_id"].ToString() == usrID)
                    {
                        retval = true;
                    }
                    else
                    {
                        MessageBox.Show("This user already logged in.......");
                        retval = false;
                    }
                }
                else
                {
                    MessageBox.Show("This user already logged in.......");
                    retval = false;
                }
            }
            catch (Exception ex)
            {
                auth = false;
                string err = ex.Message;
                retval = false;
            }
            if (retval == false)
            {
                prf.UserId = null;
                prf.UserName = null;
                prf.Password = null;
            }
            return retval;

        }
		// Method for fetching Resource ID for a particular User
		public DataSet getResource(String usrID)
		{
			DataSet objDataSet = new DataSet();
			try{
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();	
				String sqlStatement = "select resource_id from ac_role_resource_map where role_id =(select role_id from ac_user_role_map where user_id = '"+usrID+"')";
				OdbcCommand cmd = new OdbcCommand(sqlStatement,sqlCon);
				OdbcDataAdapter objDataAdapter = new OdbcDataAdapter(cmd);
				
				objDataAdapter.Fill(objDataSet);
				objDataAdapter.Dispose();
                //sqlCon.Close();
			}
			catch(Exception ex){
				if(sqlCon != null)
					//sqlCon.Close();
				System.Console.WriteLine("ERROR = ",ex);
			}
            return objDataSet;
		}

        public DataSet GetOnlineUsersList(Credentials pCrd)
        {
            DataSet objDataSet = new DataSet();
            try
            {
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();	
                String sqlStatement = "select distinct a.user_name,a.user_id,c.role_description ,date_format(a.logged_dttm,'%d-%m-%Y %H.%i.%s') from ac_user a,ac_user_role_map b,ac_role c where a.user_id = b.user_id and b.role_id = c.role_id and a.logged = 1 and a.user_id<>'" + pCrd.created_by + "'";
                OdbcCommand cmd = new OdbcCommand(sqlStatement, sqlCon);
                OdbcDataAdapter objDataAdapter = new OdbcDataAdapter(cmd);

                objDataAdapter.Fill(objDataSet);
                objDataAdapter.Dispose();
                //sqlCon.Close();
            }
            catch (Exception ex)
            {
                if (sqlCon != null)
                    //sqlCon.Close();
                    System.Console.WriteLine("ERROR = ", ex);
            }
            return objDataSet;
        }

		// Method used for Adding User
		public bool addUser(String usrID,String usrNAME,String usrRole,String usrPWD)
		{
			int i;
			//OdbcTransaction tran = null;
			try
            {
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();	
				String sqlState = "select user_id from ac_user";
				//String sqlState = "select user_id from ac_user where user_id = '"+usrID+"'";
				OdbcCommand cmd1 = new OdbcCommand(sqlState,sqlCon);
				OdbcDataAdapter objDataAdapter = new OdbcDataAdapter(cmd1);
				DataSet objDataSet = new DataSet();
				objDataAdapter.Fill(objDataSet);
				
//				if(objDataSet.Tables[0].Rows.Count>0)
//				{
//					temp = objDataSet.Tables[0].Rows[0]["user_id"].ToString();
//				}
//				else
//				{
//					MessageBox.Show("Change the UserID");
//					return false;
//				}
				for(i=0;i<objDataSet.Tables[0].Rows.Count;i++)
				{
					if(usrID == objDataSet.Tables[0].Rows[i]["user_id"].ToString())
					{
						MessageBox.Show("Change the UserID");
						return false;
					}
					else
					{
						temp = usrID;
					}
				}
				
				//tran = sqlCon.BeginTransaction();
				
				String sqlUser = "insert into ac_user(user_id,user_name,user_pwd,logged,last_activity,current_activity) values('"+temp+"','"+usrNAME+"','"+usrPWD+"',0,'','')";
				OdbcCommand cmd2 = new OdbcCommand(sqlUser,sqlCon);
//				cmd2.Connection = sqlCon;
//				cmd2.Transaction = tran;
//				cmd2.CommandText=sqlUser;
				cmd2.ExecuteNonQuery();
				//MessageBox.Show("A New User Added");
								
				String sqlUserRole = "insert into ac_user_role_map(user_id,role_id) values('"+temp+"'," +usrRole+ ")";
				OdbcCommand cmd3 = new OdbcCommand(sqlUserRole,sqlCon);
//				cmd3.Connection = sqlCon;
//				cmd3.Transaction = tran;
//				cmd3.CommandText=sqlUserRole;
				cmd3.ExecuteNonQuery();
				//MessageBox.Show("A New User Role Added");
				//sqlCon.Close();
				
				//tran.Commit();
			}
			catch(Exception ex){
//				if(sqlCon != null)
//					sqlCon.Close();
				//MessageBox.Show("ERROR = ",+ex);
				//tran.Rollback();
                string err = ex.Message;
                MessageBox.Show(err);
				return false;
			}
			return true;
		}
		
		// Method used to change password of the users
		public void changePassword(String usrID,String oldPWD,String newPWD)
		{
													   
			try{
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();	
				String sqlStatement = "update ac_user set user_pwd = '"+newPWD+"' where user_id = '"+usrID+"' and user_pwd = '"+oldPWD+"'";
				OdbcCommand cmd = new OdbcCommand(sqlStatement,sqlCon);
				cmd.ExecuteNonQuery();
				
				//sqlCon.Close();
				MessageBox.Show("Password Updated");			
			}
			catch(Exception ex){
				if(sqlCon != null)
					//sqlCon.Close();
				System.Console.WriteLine("ERROR = ",ex);
			}
		}

        public bool LockedUser(String usrID, string pDateTime)
        {
            int i = 0;
            try
            {
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();	
                String sqlStatement = "update ac_user set logged = 1,logged_dttm='" + pDateTime + "' where user_id = '" + usrID + "'";
                OdbcCommand cmd = new OdbcCommand(sqlStatement, sqlCon);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (sqlCon != null)
                    //sqlCon.Close();
                    System.Console.WriteLine("ERROR = ", ex);
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UnLockedUser(String usrID)
        {
            int i = 0;
            try
            {
                //NovaNet.Utils.dbCon dbcon;
                //dbcon = new NovaNet.Utils.dbCon();
                //sqlCon = dbcon.Connect();	
                String sqlStatement = "update ac_user set logged = 0,logged_dttm=null where user_id = '" + usrID + "'";
                OdbcCommand cmd = new OdbcCommand(sqlStatement, sqlCon);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (sqlCon != null)
                    //sqlCon.Close();
                    System.Console.WriteLine("ERROR = ", ex);
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		public void GUIgetChallenge()
		{
			//NovaNet.Utils.Profile prf = new NovaNet.Utils.Profile();
			
			NovaNet.Utils.GetChallenge wnd = new NovaNet.Utils.GetChallenge(CallBack);
			wnd.Show();
		}
		public void GUIpwdChange(Profile prf)
		{
			

			NovaNet.Utils.PwdChange wnd = new NovaNet.Utils.PwdChange(ref prf,CallCPwd);				
			wnd.Show();	
		}
		
		public void GUInewUser()
		{
			//NovaNet.Utils.Profile prf = new NovaNet.Utils.Profile(); 
			NovaNet.Utils.AddNewUser wnd = new NovaNet.Utils.AddNewUser(CallBack,sqlCon);				
			wnd.Show();	
		}
		public bool isAuthenticated()
		{
			return auth;
		}
		public Profile getProfile()
		{
			return prf;
		}
	}
}
}
