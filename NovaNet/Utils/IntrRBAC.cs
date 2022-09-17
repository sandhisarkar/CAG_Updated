/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/21/2009
 * Time: 7:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;


namespace NovaNet
{
	namespace Utils
	{

	
	public struct Profile
	{
		public String UserId;
		public String UserName;
		public String Password;
		public String Role_des;
	}
	public struct Credentials
	{
  		public string created_by;
  		public string created_dttm;
        /// <summary>
        /// changed in version 1.0.2
        /// </summary>
        public string role;
        public string userName;
	}
	
	public delegate void GetProfile(ref Profile p);
	public delegate void ChangePassword(ref Profile cpwd); 
	
	
	/// <summary>
	/// Description of Interface1.
	/// </summary>
	public interface IntrRBAC
	{
		
		bool isAuthenticated();
		
		bool authenticate(String usrID,String usrPWD);
		DataSet getResource(String usrID);		
		void GUIgetChallenge();				// used for Login Form
		void GUIpwdChange(Profile prf);		// used for PwdChange Form
		void GUInewUser();					// used for AddNewUser Form
		void changePassword(String usrID,String oldPWD,String newPWD);
		//void addUser(String usrID,String usrNAME,String usrPWD);
		bool addUser(String usrID,String usrNAME,String usrRole,String usrPWD);
		Credentials getCredentials(Profile prf);
		Profile getProfile();
        bool CheckUserIsLogged(string pUId);  //used for checking a user is already logged or not
        bool LockedUser(string pUserId, string pDateTime);
        bool UnLockedUser(string pUserId);
        DataSet GetOnlineUsersList(Credentials pCrd);  //Get online users list
	}
}
}
