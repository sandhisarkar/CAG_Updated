/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 5/3/2009
 * Time: 11:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace NovaNet
{
namespace Utils
{
	public delegate void NotifyProgress(int prmNt);
	public delegate int ImageManupulation();
	public delegate void FormType(int prmFormType);
	public delegate void NotifyImageDetails(string prmImageName,long prmImageSize);	
	public delegate void NotifyPageCountMismatch(int prmPageCountToBeScanned,int prmPageScanned);	
	/// <summary>
	/// Description of UtilsDeletgates.
	/// </summary>
	public interface UtilsDeletgates
	{
		bool RegisterNotification(NotifyProgress prmNt);
	}
    public interface StateData
    {
        MemoryStream StateLog();
    }
}
}