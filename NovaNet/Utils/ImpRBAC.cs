/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/21/2009
 * Time: 3:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.Odbc;
using NovaNet.Utils;

namespace NovaNet
{
	namespace Utils
	{

	/// <summary>
	/// Description of ImpAbstRBAC.
	/// </summary>
	public class ImpRBAC : RBAC
	{
		static OdbcConnection sqlCon;
		static NovaNet.Utils.GetProfile pData = null;
		static NovaNet.Utils.ChangePassword pCpwd = null;
		public ImpRBAC(OdbcConnection prmcon): base(prmcon, pData,pCpwd)
		{
			sqlCon = prmcon;
		}
		public static void main(String[] args)
		{
			NovaNet.Utils.dbCon dbcon;
			dbcon =new NovaNet.Utils.dbCon();
			sqlCon=dbcon.Connect();
			ImpRBAC objImpRBAC = new ImpRBAC(sqlCon);

			
			
		}
	}
}
}
