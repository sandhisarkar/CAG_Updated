/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 18/2/2009
 * Time: 11:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//using NovaNet.
using System.Collections;
using System.Data.Odbc;
using System.Data;
using System.IO;

namespace NovaNet
{
namespace wfe
{
    
    

	/// <summary>
	/// Description of wfe.
	/// </summary>
	/// 
	public enum eSTATES{
				PROJECT_INITIALIZED = 1,
				BATCH_CREATED=2,
				BATCH_SCANNED=3,
                BATCH_SUBMITTED = 66,
                //BATCH_SUBMITTED = 66,
				BATCH_QC=4,
				BATCH_INDEXED=5,
				BATCH_FQC=6,
				BATCH_READY_FOR_UAT=7,
				BATCH_EXPORTED=8,
				BOX_CREATED=9,
				BOX_SCANNED=10,
				BOX_QC=11,
				BOX_INDEXED=12,
				BOX_FQC=13,
				BOX_CONFLICT=14,
				BOX_EXPORTED=15,
				POLICY_CREATED=16,
				POLICY_SCANNED=17,
				POLICY_QC=18,
				POLICY_INDEXED=19,
				//POLICY_FQC=20,
                POLICY_FQC = 5,
				POLICY_CONFLICT=21,
                POLICY_NOT_INDEXED = 40,
				POLICY_EXPORTED=22,
				POLICY_EXCEPTION=30,
				POLICY_CHECKED=31,
				PAGE_CREATED=23,
				PAGE_SCANNED=24,
				PAGE_QC=25,
				PAGE_INDEXED=26,
				PAGE_FQC=27,
				PAGE_EXPORTED=28,
				PAGE_DELETED=29,
				PAGE_EXCEPTION=32,	
				PAGE_CHECKED=33,
                PAGE_NOT_INDEXED = 41,
				BOX_INITIALIZED=42,
				POLICY_INITIALIZED=34,	
				BOX_CHECKED=35,
                POLICY_MISSING=36,
                POLICY_ON_HOLD = 37,
                PAGE_ON_HOLD=38,
                PAGE_RESCANNED_NOT_INDEXED=39,
                POLICY_SUBMITTED = 77,
                METADATA_ENTRY =100,
                BARCODE_ENTRY =101
	};
	public enum eITEMS
	{
		PROJECT = 1,
		BATCH=2,
		BOX=3,
		POLICY=4,
		PAGE=5,
        LIC_QA_PAGE = 6
	};
	//Delegates to return workflow items
	public delegate void SendValue(wItem pItem);
    public delegate void ScanNotify(wItem pItem,int prmMode); // For photo scanning
	public abstract class wItem
	{
		//System constants
		public const int _LENGTH_OF_PAGE_NAME = 19;
		
		protected int mode;
		protected OdbcConnection dbcon;
		public wItem(OdbcConnection prmCon, int prmMode){dbcon=prmCon; mode=prmMode;}
		
		//public abstract bool Display();
		public abstract bool TransferValues(udtCmd cmd);
		public abstract udtCmd LoadValuesFromDB();
		public abstract bool Commit();
		public abstract bool KeyCheck(string prmValue);
		public int GetMode()
		{
			return mode;
		}
	}
	public interface udtCmd
	{
	}
    
	public interface ErrReporter
	{
		Hashtable GetErrors();
	}
	public abstract class wItemFactory
	{
		public abstract wItem GetWItem(String prmItemType);
	}
	public interface wItemControl
	{
		
	}
	public abstract class wQuery
	{
		public abstract ArrayList GetItems(eITEMS item, eSTATES[] state,wItem wi);
        public abstract DataSet GetDeedVolume(string proj_key,string batch_key,string box_key,string pPolicyNo);
        public abstract DataSet GetDeedVolume(string deed_no);
        public abstract DataSet GetIndexDetails(string deed_no,string deed_year);
	}
}
}