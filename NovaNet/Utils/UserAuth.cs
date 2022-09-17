/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/29/2009
 * Time: 11:30 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
 
 
/*
 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Odbc;


namespace NovaNet
{
	namespace Utils
	{
	/// <summary>
	/// Description of MainProgram.
	/// </summary>
	public class UserAuth
	{
		private OdbcConnection sqlCon;
		NovaNet.Utils.GetProfile pData;
		NovaNet.Utils.ChangePassword pCPwd=null;
		NovaNet.Utils.Profile p;
		
		NovaNet.Utils.IntrRBAC rbc;
		private bool authStatus;
		public UserAuth(NovaNet.Utils.GetProfile prmCB)
		{
			NovaNet.Utils.dbCon dbcon;
			dbcon =new NovaNet.Utils.dbCon();
			sqlCon=dbcon.Connect();
			pData = prmCB;
			rbc = new NovaNet.Utils.RBAC(sqlCon,pData,pCPwd);
			rbc.GUIgetChallenge();
			
		}
		private void getData(ref NovaNet.Utils.Profile prmp)
		{
			p=prmp;
			MessageBox.Show(p.UserId);
			MessageBox.Show(p.Password);
			
			//rbc.getResource(p.UserId);
			//rbc.addUser(p.UserId,p.UserName,p.Password);
			
			if(rbc.authenticate(p.UserId,p.Password) == true)
			{
				authStatus= true;
			}
			else
			{
				authStatus= false;
			}
			//pData.Invoke(prmp);
		}
		public bool isAuthenticated()
		{
			
			//pCPwd = getCPwd;
			return authStatus;
		}
		
	}
}
}


*/