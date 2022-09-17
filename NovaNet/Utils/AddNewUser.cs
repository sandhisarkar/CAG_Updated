/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/23/2009
 * Time: 3:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Data;
using System.Text.RegularExpressions;

namespace NovaNet
{
	namespace Utils
	{

	/// <summary>
	/// Description of AddNewUser.
	/// </summary>
	public partial class AddNewUser : Form
	{
		//private Profile p;
		NovaNet.Utils.GetProfile pCallBack;
		OdbcConnection sqlCon;
		//OdbcDataReader oReader = null;
		String roleID=null;
		String usrid;
		Regex regex;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);	
	
		public AddNewUser()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public AddNewUser(NovaNet.Utils.GetProfile prmGetProfile,OdbcConnection prmcon)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//p = prmP;
			pCallBack = prmGetProfile;
			sqlCon = prmcon;
            exMailLog.SetNextLogger(exTxtLog);
            //
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void AddNewUserLoad(object sender, EventArgs e)
		{
			try
			{
				// code for populating combobox for role description
				
				String sSelectSQL = "select role_id,role_description from ac_role where role_id <> '6' and role_id <> '7'";
	        	OdbcCommand oCommand = new OdbcCommand(sSelectSQL,sqlCon);
	        	OdbcDataAdapter objDataAdapter = new OdbcDataAdapter(oCommand);
				DataSet ds = new DataSet();
				objDataAdapter.Fill(ds);
	       		//open the connection, and use the reader to populate the combobox
	       		
					if(sqlCon!=null)
					{
						
						cmboRole.DataSource = ds.Tables[0];
						cmboRole.DisplayMember=ds.Tables[0].Columns[1].ToString();
						cmboRole.ValueMember=ds.Tables[0].Columns[0].ToString();
		        	
					}
					
					
			}
			catch (Exception oE)
    		{
        			MessageBox.Show("Problem Populating Reader Box:" + oE.ToString());
                    exTxtLog.Log(oE);
    		}
 
		}
		
		void BtSaveClick(object sender, EventArgs e)
		{
			//int i;
					
			Profile pf = new Profile();
			//String usrid = txtUserId.Text;
			
			String usrname = txtUserName.Text;
			String usrpwd = txtPassword.Text;
			String confirm = txtConPwd.Text;
			roleID = cmboRole.SelectedValue.ToString();
			
			regex = new Regex("^[a-z0-9]*$");
			    if (regex.IsMatch(txtUserId.Text))
			    {
			    	
			    	usrid = txtUserId.Text;
			    	if((usrid.Length > 30 || usrid.Length < 1) || (usrname.Length > 100 || usrname.Length < 1) || (usrpwd.Length > 50 || usrpwd.Length < 6))
					{
						if(usrid.Length > 30 || usrid.Length < 1)
						{
							MessageBox.Show("UserID should not left blank and not exceed 30 characters");
						}
						if(usrname.Length > 100 || usrname.Length < 1)
						{
							MessageBox.Show("User Name should not left blank and not exceed 100 characters");
						}
						if(usrpwd.Length > 50 || usrpwd.Length < 6 )
						{
							MessageBox.Show("Password should be minimum 6 and maximum 50 characters");
						}
					}
					else
					{
							pf.UserId=usrid;
							pf.UserName=usrname;
							pf.Password=usrpwd;
							pf.Role_des=roleID;
							if(confirm.Equals(pf.Password))
							{
								this.Hide();
								pCallBack.Invoke(ref pf);
							}
							else 
							{
								MessageBox.Show("Unmatched Password");
							}
					}
			    }
			    else
			    {
			        MessageBox.Show("Special characters and upper case characters are not allowed");
			    }

								
//			if((usrid.Length > 30 || usrid.Length < 1) || (usrname.Length > 100 || usrname.Length < 1) || (usrpwd.Length > 50 || usrpwd.Length < 6))
//			{
//				if(usrid.Length > 30 || usrid.Length < 1)
//				{
//					MessageBox.Show("UserID should not left blank and not exceed 30 characters");
//				}
//				if(usrname.Length > 100 || usrname.Length < 1)
//				{
//					MessageBox.Show("User Name should not left blank and not exceed 100 characters");
//				}
//				if(usrpwd.Length > 50 || usrpwd.Length < 6 )
//				{
//					MessageBox.Show("Password should be minimum 6 and maximum 50 characters");
//				}
//			}
//			else
//			{
//				pf.UserId=usrid;
//				pf.UserName=usrname;
//				pf.Password=usrpwd;
//				pf.Role_des=roleID;
//				if(confirm.Equals(pf.Password))
//				{
//					this.Hide();
//					pCallBack.Invoke(ref pf);
//				}
//				else 
//				{
//					MessageBox.Show("Unmatched Password");
//				}
//			}
			
		}
		
		void BtCancelClick(object sender, EventArgs e)
		{
			this.Close();
			this.Dispose();
		}
	}
}
}
