/*
 * Created by SharpDevelop.
 * User: SubhajitB
 * Date: 13/3/2009
 * Time: 4:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ImageHeaven
{
    /// <summary>
    /// Description of Constants.
    /// </summary>
    public class ihConstants
    {
        public ihConstants()
        {
        }
        public static string CONFIG_FILE_PATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\" + "IhConfiguration.ini";
        public static string IMAGE_SHORTCUT_KEY_SECTION = "IMAGESHORTCUTKEY";
        public static string CROP_KEY = "CROP";
        public static string AUTO_CROP_KEY = "AUTOCROP";
        public static string ROTATE_LEFT_KEY = "ROTATELEFT";
        public static string ROTATE_RIGHT_KEY = "ROTATERIGHT";
        public static string SKEW_LEFT_KEY = "SKEWLEFT";
        public static string SKEW_RIGHT_KEY = "SKEWRIGHT";
        public static string ZOOM_IN_KEY = "ZOOMIN";
        public static string ZOOM_OUT_KEY = "ZOOMOUT";
        public static string NOISE_REMOVE_KEY = "NOISEREMOVAL";
        public static string CLEAN_KEY = "CLEAN";
        public static string EXPORT_FOLDER_SECTION = "EXPORTFOLDER";
        public static string EXPORT_FOLDER_KEY = "EXFOLDERLOCATION";
        public static string REEXPORT_FOLDER_SECTION = "REEXPORTFOLDER";
        public static string REEXPORT_FOLDER_KEY = "REEXFOLDERLOCATION";
        public static string _EXPORT_DRIVE = "C:";
        /// <summary>
        /// Indexing constants
        /// </summary>
        public static string INDEX_SHORTCUT_KEY_SECTION = "INDEXSHORTCUTKEY";
        public static string PROPOSALFORM_KEY = "PROPOSALFORM";
        public static string PHOTOADDENDUM_KEY = "PHOTOADDENDUM";
        public static string PROPOSALENCLOSERS_KEY = "PROPOSALENCLOSERS";
        public static string SIGNATUREPAGE_KEY = "SIGNATUREPAGE";
        public static string MEDICALREPORT_KEY = "MEDICALREPORT";
        public static string PROPOSALREVIEWSLIP_KEY = "PROPOSALREVIEWSLIP";
        public static string POLICYBOND_KEY = "POLICYBOND";
        public static string NOMINATION_KEY = "NOMINATION";
        public static string ASSIGNMENT_KEY = "ASSIGNMENT";
        public static string ALTERATION_KEY = "ALTERATION";
        public static string REVIVALS_KEY = "REVIVALS";
        public static string POLICYLOANS_KEY = "POLICYLOANS";
        public static string SURRENDER_KEY = "SURRENDER";
        public static string CLAIMS_KEY = "CLAIMS";
        public static string CORRESPONDENCE_KEY = "CORRESPONDENCE";
        public static string OTHERS_KEY = "OTHERS";
        public static string KYCDOCUMENT_KEY = "KYCDOCUMENT";
        public static string DELETE_KEY = "DELETE";
        public static string MAINPETITION_KEY = "MAINPETITION";
        public static string MAINPETITIONANNEXTURES_KEY = "MAINPETITIONANNEXTURES";
        public static string AFFIDAVITSWITHANNEXTURES_KEY = "AFFIDAVITSWITHANNEXTURES";
        public static string WRITTENSTATEMENTOBJECTION_KEY = "WRITTENSTATEMENTOBJECTION";
        public static string CONNECTEDAPPLICATIONS_KEY = "CONNECTEDAPPLICATIONS";
        public static string ANALOGOUSANDCONNECTEDCASE_KEY = "ANALOGOUSANDCONNECTEDCASE";
        public static string VAKALATNAMAANDWARRENT_KEY = "VAKALATNAMAANDWARRENT";
        public static string SUMMONS_KEY = "SUMMONS";
        //public static string NOTICE_KEY = "NOTICE";
        public static string REPORT_KEY = "REPORT";
        public static string BRIEF_KEY = "BRIEF";
        public static string SETTLEMENT_KEY = "SETTLEMENT";
        public static string RULE_KEY = "RULE";
        public static string BOND_KEY = "BOND";
        public static string CAVEAT_KEY = "CAVEAT";
        public static string WITNESSACTIONDEPOSITION_KEY = "WITNESSACTIONDEPOSITION";
        public static string ISSUES_KEY = "ISSUES";
        public static string EXHIBITS_KEY = "EXHIBITS";
        //public static string DRAFTPAPERS_KEY = "DRAFTPAPERS";
        public static string NOTICEOFARGUMENT_KEY = "NOTICEOFARGUMENT";
        public static string ENGROSSEDPRELIMINARY_KEY = "ENGROSSEDPRELIMINARY";
        public static string ORDERSMAINCASE_KEY = "ORDERSMAINCASE";
        public static string ORDERSAPPLICATIONS_KEY = "ORDERSAPPLICATIONS";
        public static string FINALJUDGEMENTORDER_KEY = "FINALJUDGEMENTORDER";
        public static string LOWERCOURTRECORDS_KEY = "LOWERCOURTRECORDS";
        public static string IMPUGNEDORDER_KEY = "IMPUGNEDORDER";
        //public static string PAPERBOOK_KEY = "PAPERBOOK";
        /// proposal file name when signature file created
        /// 
        public static string _PROPOSALFORM_FILE_NO = "040";

        /// <summary>
        /// Index file name constants
        /// </summary>
        public static string PROPOSALFORM_FILE = "Proposal_form";
        public static string PHOTOADDENDUM_FILE = "Photo_addendum";
        public static string PROPOSALENCLOSERS_FILE = "Proposal_enclosures";
        public static string SIGNATUREPAGE_FILE = "Signature_page";
        public static string MEDICALREPORT_FILE = "Medical_report";
        public static string PROPOSALREVIEWSLIP_FILE = "Proposal_review_slip";
        public static string POLICYBOND_FILE = "Policy_bond";
        public static string NOMINATION_FILE = "Nomination";
        public static string ASSIGNMENT_FILE = "Assignment";
        public static string ALTERATION_FILE = "Alteration";
        public static string REVIVALS_FILE = "Revivals";
        public static string POLICYLOANS_FILE = "Policy_loans";
        public static string SURRENDER_FILE = "Surrender";
        public static string CLAIMS_FILE = "Claims";
        public static string CORRESPONDENCE_FILE = "Correspondence";
        public static string OTHERS_FILE = "Others";
        public static string KYCDOCUMENT_FILE = "KYC_documents";
        public static string MAINPETITION_FILE = "Main_Petition";
        public static string MAINPETITIONANNEXTURES_FILE = "Main_Petition_Annexures";
        public static string AFFIDAVITSWITHANNEXTURES_FILE = "Affidavits_with_Annexures";
        public static string WRITTENSTATEMENTOBJECTION_FILE = "Written_Statement_Objection";
        public static string CONNECTEDAPPLICATIONS_FILE = "Connected_Applications_with_Annexures";
        public static string ANALOGOUSANDCONNECTEDCASE_FILE = "Analogous_Connected_Case";
        public static string VAKALATNAMAANDWARRENT_FILE = "Vakalatnama_Affidavit_of";
        public static string SUMMONS_FILE = "Summons_Notice_Warrent";
        //public static string NOTICE_FILE = "Notice";
        public static string REPORT_FILE = "Report";
        public static string BRIEF_FILE = "Brief_of_Documents";
        public static string SETTLEMENT_FILE = "Terms_of_Settlement";
        public static string RULE_FILE = "Rule";
        public static string BOND_FILE = "Bond";
        public static string CAVEAT_FILE = "Caveat";
        public static string WITNESSACTIONDEPOSITION_FILE = "Witness_Action_Deposition";
        public static string ISSUES_FILE = "Issues";
        public static string EXHIBITS_FILE = "Exhibits";
        //public static string DRAFTPAPERS_FILE = "Draft_papers";
        public static string NOTICEOFARGUMENT_FILE = "Notes_of_Argument";
        public static string ENGROSSEDPRELIMINARY_FILE = "Engrossed_Priliminary";
        public static string ORDERSMAINCASE_FILE = "Orders_Main_Case";
        public static string ORDERSAPPLICATIONS_FILE = "Orders_Connected_Application";
        public static string FINALJUDGEMENTORDER_FILE = "Final_Judgement_Order";
        public static string LOWERCOURTRECORDS_FILE = "Lower_Court_Records";
        public static string IMPUGNEDORDER_FILE = "Impugned_Judgement_Decree_Order";
        //public static string PAPERBOOK_FILE = "Paper_book";
        //public static string DELETE_FILE="DELETE";
        /// <summary>
        /// Image value constants
        /// </summary>
        public static string IMAGE_RELATED_VALUE_SECTION = "IMAGERELATEDVALUES";
        public static string ROTATE_ANGLE_KEY = "ROTATEANGLE";
        public static string _SCAN_SECTION = "SCAN";
        public static string _SCAN_KEY = "PAGESPLITTER";
        public static string _ACQUIRE_TIME_MESSAGE_KEY = "SHOWACQUIREMESSAGE";
        public static string _SCAN_IN_GRAYSCALE = "SCANINGRAYSCALE";
        public static string SKEW_X_KEY = "SKEWX";
        public static string SKEW_Y_KEY = "SKEWY";
        public static string _SCAN_DPI = "SCANDPI";
        public static string _SCAN_GREY_DPI = "SCANGREYDPI";
        public static string _IMG_QUALITY = "QUALITY";
        //public static string EXCEPTION_INI_FILE_PATH=System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase ).Remove(0,6)+ "\\" + "IhException.ini";
        /// <summary>
        /// Constants for internal programming
        /// </summary>
        public static int _ADDING = 0;
        public static int _EDITING = 1;
        public const double _DOCKET_MAX_SIZE = 1.5;
        /// <summary>
        /// Constants for Error
        /// </summary>
        public static string DBERRORTYPE = "DBERROR";

        /// <summary>
        /// Populate Error List
        /// </summary>
        public static string NOT_VALID = "ERROR";

        /// <summary>
        /// For Exception list
        /// </summary>
        public static string ValidationException = "Blank Filed";
        public static string SCHEMA_FILE_NAME = "schema.ini";

        ///For Image related error
        /// 
        public static string IMAGE_ERROR = "Error while image manupulation";
        ///QC operation
        public static int _CROP = 0x01;
        public static int _PHOTO_CROP = 0x03;
        public static int _CLEAN = 0x02;
        public static int _OTHER_OPERATION = 0x00;
        public static int _NO_OPERATION = 0x03;

        public static double _SKEW_X_ANGLE = -10;
        public static double _SKEW_Y_ANGLE = -10;

        ///Box Status
        public static int _BOX_SCANNED = 0;
        public static int _BOX_QC = 1;
        public static int _BOX_INDEXED = 2;

        ///Operation folder name
        public static string _SCAN_FOLDER = "Scan";
        public static string _QC_FOLDER = "QC";
        public static string _INDEXING_FOLDER = "QC";
        public static string _FQC_FOLDER = "QC";
        public static string _DELETE_FOLDER = "Deleted";
        public static string _EXPORT_FOLDER = "Export";
        public static string _RE_EXPORT_FOLDER = "ReExport2";
        public static string _LOCAL_APPEND_IMAGE_FOLDER = "APPEND_IMAGE_TEMP";
        public static string _LOCAL_APPEND_TEXT_FOLDER = "APPEND_TEXT_TEMP";
        ///Form Type 
        public static int _QC_TYPE_FORM = 0x00;

        ///Constants for scan
        public const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        public const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
        public const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
        public const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
        public const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";
        public const int SCAN_COLOR = 1;
        public const int SCAN_BLACK_WHITE = 4;
        public const int SCAN_GRAY_SCALE = 2;
        public const int SCAN_RE_FQC = 0x0100;
        public const int SCAN_NEW_FQC = 0x0101;
        public const int MAX_NO_SCAN_FQC = -1;
        public const int MAX_NO_SCAN_FQC_FLATBED = 1;
        public const int _SCAN_WITH_PHOTO = 0;
        public const int _SCAN_WITHOUT_PHOTO = 1;
        //Scan related constants
        public const string SCAN_SUCCESS_FLAG = "01";
        public const string SCAN_PENDING_FLAG = "02";
        public const string SCAN_UPLOAD_SUCCESS_FLAG = "03";
        public const string UPLOAD_ERROR_FLAG = "04";
        public const string INCREMENTEDSCAN = "N";
        public const string SCAN_SEPARATOR_BLACK = "0";
        public const string SCAN_SEPARATOR_BAR = "1";
        public const string SCAN_SEPARATOR_COMPARE_STRING = "BZerPS";


        //Inventory constants
        public const int _POLICY_CONTAINS_PHOTO = 1;
        public const int _POLICY_DOES_NOT_CONTAINS_PHOTO = 0;
        public const int _HOLD_POLICY_EXCEPTION = 0;
        public const int _MISSING_POLICY_EXCEPTION = 1;
        public const int _PHOTO_PAGE_POSITION = 1;
        public const int _PHOTO_PAGE = 1;
        public const int _NORMAL_PAGE = 0;

        //Qa status
        public const int _LIC_QA_POLICY_CHECKED = 0;
        public const int _LIC_QA_POLICY_VIEWED = 1;
        public const int _LIC_QA_POLICY_EXCEPTION = 2;
        public const int _LIC_QA_POLICY_EXCEPTION_SLOVED = 6;
        public static int _POLICY_EXCEPTION_SOLVED = 3;
        public static int _POLICY_EXCEPTION_NOT_SOLVED = 4;
        public static int _POLICY_EXCEPTION_INITIALIZED = 5;
        public static int _POLICY_EXCEPTION_RECTIFIED = 7;

        //Image Manipulation constants
        public const int _BATCHSPLITTER_STARTX = 400;
        public const int _BATCHSPLITTER_STARTY = 400;
        public const int _BATCHSPLITTER_XLEN = 600;
        public const int _BATCHSPLITTER_YLEN = 600;

        //For custom exceptions
        public const int _RESOLVED = 1;
        public const int _NOT_RESOLVED = 2;

        //For user role
        public const string _ADMINISTRATOR_ROLE = "Admin";
        public const string _LIC_ROLE = "Audit";

        //Max export page count
        public const int _MAX_POLICY_PAGE_COUNT = 0;

        /// <summary>
        /// Key value pair constants for 100% fqc, batch rejection and database vertion
        /// </summary>
        public static string CENT_PERCENT_FQC_KEY = "CENT_FQC";
        public static string ISADMINHOLD_KEY = "ISADMINHOLD";
        public static string BATCH_REJECTION_KEY = "BATCH_REJECTION";
        public static string DB_VERSION_KEY = "DB_VERSION";
        public static string SCAN_TIME_HOLD_KEY = "SCANTIMEHOLD";
        public static string SPECIALBOND_KEY = "SPECIAL_MANDATORY_DOC";
        public static string OTHER_THAN_POLICYBOND_IMPORT_NEEDED_KEY = "OTHER_THAN_POLICYBOND_IMPORT_NEEDED";

        public static string CENT_PERCENT_FQC_VALUE = "0";
        public static string ISADMINHOLD_VALUE = "1";
        public static string SCAN_TIME_HOLD_VALUE = "1";
        public static string BATCH_REJECTION_VALUE = "0";
        public static string DB_VERSION_VALUE = "2";
        public static string SPECIALBOND_VALUE = "1";
        public static string OTHER_THAN_POLICYBOND_IMPORT_NEEDED_VALUE = "1";
        /// <summary>
        /// DIFFERENT STAGES
        /// </summary>
        public static int _RESCAN = 1;
    }
}
