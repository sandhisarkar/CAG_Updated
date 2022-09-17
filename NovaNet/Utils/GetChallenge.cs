/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/21/2009
 * Time: 6:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;							
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;



// used as Login Form
namespace NovaNet
{
	namespace Utils
	{
	/// <summary>
	/// Description of GetChallenge.
	/// </summary>
	public partial class GetChallenge : Form
	{
	
		NovaNet.Utils.GetProfile pCallBack;
		Regex regex;
		String uid;
		public GetChallenge()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public GetChallenge(NovaNet.Utils.GetProfile prmGetProfile)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			pCallBack = prmGetProfile;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
		}

        void GetChallengeLoad(object sender, System.EventArgs e)
        {
            txtUserID.Focus();

        }
		void BtLoginClick(object sender, EventArgs e)
		{
			Profile p = new Profile();
			//String uid = txtUserID.Text;
			String pwd = txtPasswd.Text;
			regex = new Regex("^[a-z0-9]*$");
			if (regex.IsMatch(txtUserID.Text))
			{
					uid = txtUserID.Text;
					//String tempuid = txtUserID.Text.ToUpper();
					if((uid.Length > 30 || uid.Length < 1) || (pwd.Length > 50 || pwd.Length < 6))
					{
						if(uid.Length > 30 || uid.Length < 1)
						{
							MessageBox.Show("User ID should be minimum 1 and maximum 30 characters");
						}
//						if(uid != tempuid)
//						{
//							MessageBox.Show("User ID should be in lower case");
//						}
						if(pwd.Length > 50 || pwd.Length <6)
						{
							MessageBox.Show("Password should be minimum 6 and maximum 50 characters");
						}
					}
					else
					{
							p.UserId=uid;
							p.Password=pwd;
							this.Hide();
							pCallBack.Invoke(ref p);
					}
			}
			else
			{
				MessageBox.Show("Special characters and upper case characters are not allowed");
			}
			
		}
		
		void LbPasswdClick(object sender, EventArgs e)
		{
			
		}
		
		void CmdCancelClick(object sender, EventArgs e)
		{
			this.Close();
			this.Dispose();
			Application.Exit();
		}
		
		void PictureBox1Click(object sender, EventArgs e)
		{
			
		}

        private void GetChallenge_Shown(object sender, EventArgs e)
        {
            txtUserID.Focus();
        }
	}
}
}

