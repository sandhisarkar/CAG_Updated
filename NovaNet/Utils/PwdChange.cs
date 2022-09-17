/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/23/2009
 * Time: 3:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NovaNet
{
	namespace Utils
	{

	/// <summary>
	/// Description of PwdChange.
	/// </summary>
	public partial class PwdChange : Form
	{
		private Profile p;					
		//ihAccControl.GetProfile pCallBack;
		NovaNet.Utils.ChangePassword pCallBackPwd;
		String pwdold;
		String temppwd;
		public PwdChange() 
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public PwdChange(ref Profile prmP,NovaNet.Utils.ChangePassword prmGetCpwd) 
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			p = prmP;
			pCallBackPwd = prmGetCpwd;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void BtUpdateClick(object sender, EventArgs e)
		{
			
//			String usrID = p.UserId; // user id from Profile
//			pwdold = p.Password;    // user password from Profile
			temppwd = txtOldPwd.Text; // current password from PwdChange form
			String usrpwd = txtNewPwd.Text; // new password from PwdChange form
			String confirmpwd = txtConNewPwd.Text; // confirm new password from PwdChange form
			
			if((!pwdold.Equals(temppwd)) || (pwdold.Length > 50 || pwdold.Length < 6) || (temppwd == null))
			{
				
				
					MessageBox.Show("Current Password is wrong or empty");
				
			}
			else
			{
				if((usrpwd.Length > 50 || usrpwd.Length < 6) || (confirmpwd.Length > 50 || confirmpwd.Length < 6))
				{
					MessageBox.Show("Password should be minimum 6 and maximum 50 characters");
				}
				else
				{
					p.UserName = pwdold;	 // old password
					p.Password = usrpwd; 			// new password
					//String confirm = txtConNewPwd.Text; 	// confirm new password
					if(p.Password.Equals(confirmpwd))
					{
						pCallBackPwd.Invoke(ref p);
						this.Hide();
					}
					else
					{
						MessageBox.Show("Unmatched Password");
					}
				}
			}
		}
		
		void BtCancelClick(object sender, EventArgs e)
		{
			this.Close();	// same as GetChallenge Form
			this.Dispose();
		}
		
		void Panel1Paint(object sender, PaintEventArgs e)
		{
			
		}
		
		void PwdChangeLoad(object sender, EventArgs e)
		{
			//MessageBox.Show("Called form PwdChange :"p.UserId);
			String usrID = p.UserId; // user id from Profile
			pwdold = p.Password;   
		}
	}
}
}
