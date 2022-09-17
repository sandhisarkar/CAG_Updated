
 
/* 
* User: SubhajitB
 * Date: 23/2/2009
 * Time: 6:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.IO;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NovaNet
{
namespace Utils
{
	/// <summary>
	/// Description of Reader.
	/// </summary>
	public abstract class Reader
	{
		protected string csvPath=null;
		public Reader(string prmPath)
		{
			csvPath=prmPath;
		}
		public abstract DataSet ReadData();
	}
	
	public class csvReader: Reader
	{
		private INIReader rd=null;
		private KeyValueStruct udtKeyValue;
		
		public csvReader(string prmPath): base(prmPath)
		{
			if(File.Exists(prmPath)==false)
			{
				rd=new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
				udtKeyValue.Key=Constants.CSV_READ_ERROR.ToString();
				udtKeyValue.Section=Constants.CSV_READ_EXCEPTION_SECTION.ToString();
				string ErrMsg=rd.Read(udtKeyValue);
				throw new CSVReadException(ErrMsg);
			}
			
		}
		public override DataSet ReadData()
		{
			DataSet ds = new DataSet();
            string tempPath =Path.GetDirectoryName(csvPath) + "\\" + Path.GetFileName(csvPath).Substring(5);

            File.Copy(csvPath, tempPath);
            PrepareSchemaFile(tempPath);
            OleDbConnection conn = new OleDbConnection(GetConnectionString(tempPath));
            // Read it
            //test.csv is the file that is being read
            conn.Open();
            
            OleDbDataAdapter da = new OleDbDataAdapter("select * from " + Path.GetFileName(tempPath), conn);
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                //ds.Tables[0].Columns.Remove("F22");
                File.Delete(Path.GetDirectoryName(tempPath) + "\\" + Constants.SCHEMA_FILE_NAME);
            }
            else
            {
                rd = new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                udtKeyValue.Key = Constants.CSV_READ_EXCEPTION_SECTION.ToString();
                udtKeyValue.Section = Constants.CSV_READ_ERROR.ToString();
                string ErrMsg = rd.Read(udtKeyValue);
                throw new CSVReadException(ErrMsg);
            }
            conn.Close();
            conn.Dispose();
            ds.Dispose();
            File.Delete(tempPath);
			return ds;
		}
		private bool PrepareSchemaFile(string prmCsvPath)
		{
			string iniSection=null;
			string schemaFilePath=null;
			Dictionary<string, string> prepSchema = new Dictionary<string, string>(); 
			prepSchema.Add("ColNameHeader","True");
			prepSchema.Add("Format","CSVDelimited");
			prepSchema.Add("Col1","DistrictCode Text");
			prepSchema.Add("Col2","ROCode Text");
            prepSchema.Add("Col3","Book Text");
            prepSchema.Add("Col4","DeedYear Text");
            prepSchema.Add("Col5","DeedNo Text");
            //prepSchema.Add("Col6","PolicyNo Text");
            //prepSchema.Add("Col7","LinkPolicyNo Text");
            //prepSchema.Add("Col8","PolicyHolderName Text");
            //prepSchema.Add("Col9","DateCommencement Text");
            //prepSchema.Add("Col10","DOB Text");
            //prepSchema.Add("Col11","StatusCode Text");
            //prepSchema.Add("Col12","CustomerId Text");
            //prepSchema.Add("Col13","DocketPageNo Text");
            //prepSchema.Add("Col14","PageToBeScanned Text");
            //prepSchema.Add("Col15","HandoverDate Text");
            //prepSchema.Add("Col16","ScannedDate Text");
            //prepSchema.Add("Col17","UploadDate Text");
            //prepSchema.Add("Col18","NoOfImageUploaded Text");
            //prepSchema.Add("Col19","CartonNo Text");
            //prepSchema.Add("Col20","AvaillableDocket Text");
            //prepSchema.Add("Col21","ScanUploadFlagText Text");            	
            //prepSchema.Add("Col22","Unused Text");
			//prepSchema.Add("Col23","F22 Text");
			
			iniSection=Path.GetFileName(prmCsvPath);
			schemaFilePath=Path.GetDirectoryName(prmCsvPath) + "\\" + Constants.SCHEMA_FILE_NAME;
			if (File.Exists(schemaFilePath))
			{
				File.Delete(schemaFilePath);
			}
			INIFile prepINI=new INIFile();
			if (prepINI.WriteINI(iniSection,prepSchema,schemaFilePath)>0)
			    {
			    	return true;
			    }
			else
			    	return false;
		}
		private string GetConnectionString(string prmPath)
		{
			string connStr = @"Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + Path.GetDirectoryName(prmPath) + "; Extended Properties = \"text;HDR=YES;FMT=Delimited\"";
			return connStr;				
		}
	}
}
}