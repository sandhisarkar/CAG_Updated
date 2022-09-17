using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using DockSample.Customization;
using System.IO;
using DockSample;
using NovaNet.Utils;
using NovaNet.wfe;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using LItems;
//using AForge.Imaging;
//using AForge;
//using AForge.Imaging.Filters;
using TwainLib;
using Inlite.ClearImageNet;
//using System.Drawing.Bitmap;
//using System.Drawing.Graphics;
//using Graphics.DrawImage;


namespace ImageHeaven
{
    public partial class aeImageQc : Form
    {
        public delegate void NextClickedHandler(object sender, EventArgs e);
        public delegate void PreviousClickedHandler(object sender, EventArgs e);
        public delegate void PolicyChangeHandler(object sender, EventArgs e);
        public delegate void ImageChangeHandler(object sender, EventArgs e);
        public delegate void BoxDetailsLoaded(object sender, EventArgs e);
        public delegate void LstDelImageKeyPress(object sender, KeyEventArgs e);
        public delegate void LstImageIndexKeyPress(object sender, KeyPressEventArgs e);
        public delegate void BoxDetailsMouseClick(object sender, MouseEventArgs e);
        public delegate void LstImageClick(object sender, EventArgs e);
        public delegate void LstDelImageClick(object sender, EventArgs e);
        public delegate void LstNextKeyPress(object sender, KeyEventArgs e);
        public delegate void ComboValueChanged(object sender, EventArgs e);

        //System.Windows.Forms.Button prmButtonCrop = new Button();
        //System.Windows.Forms.Button prmButtonAutoCrp = new Button();
        //System.Windows.Forms.Button prmButtonRotateRight = new Button();
        //System.Windows.Forms.Button prmButtonRotateLeft = new Button();
        //System.Windows.Forms.Button prmButtonZoomIn = new Button();
        //System.Windows.Forms.Button prmButtonZoomOut = new Button();
        //System.Windows.Forms.Button prmButtonSkewRight = new Button();
        System.Windows.Forms.Button prmButtonSkewLeft = new Button();
        //System.Windows.Forms.Button prmButtonNoiseRemove = new Button();
        //System.Windows.Forms.Button prmButtonCleanImg = new Button();
        //System.Windows.Forms.Button prmButtonCopyImage = new Button();
        //System.Windows.Forms.Button prmButtonDelImage = new Button();
        System.Windows.Forms.Button prmButtonRescan = new Button();
        //System.Windows.Forms.Button prmPhotoCrop = new Button();
        //System.Windows.Forms.Button prmEndPhotoCrop = new Button();
        //System.Windows.Forms.Button prmGetPhoto = new Button();
        System.Windows.Forms.Button prmNext = null;
        System.Windows.Forms.Button prmPrevious = null;
        //            System.Windows.Forms.Label prmProject = null;
        //            System.Windows.Forms.Label prmBatch = null;
        //            System.Windows.Forms.Label prmBox = null;
        //		
        //private DummyToolbox m_toolbox = new DummyToolbox();
        private OdbcConnection sqlCon = null;
        private bool m_bSaveLayout = true;
        //private DeserializeDockContent m_deserializeDockContent;
        //private FloatToolbox m_toolbox = new FloatToolbox();
        //private MagickNet.Image imgQc;
        private string imgFileName = null;
        NovaNet.Utils.ImageManupulation delImage;
        private wfeBox wBox = null;
        private CtrlPolicy pPolicy = null;
        private CtrlImage pImage = null;
        private CtrlBox pBox = null;
        NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();
        private bool photoCropOperation = false;
        private bool getPhotoOperation = false;
        private string error = null;
        private Credentials crd = new Credentials();
        MemoryStream stateLog;
        byte[] tmpWrite;
        ///For key config
        string cropKey;
        string zoomInKey;
        string zoomOutKey;
        string autoCropKey;
        string rotateRKey;
        string rotateLKey;
        string skewRKey;
        string skewLKey;
        string noiseRemovalLKey;
        string cleanKey;
        string deleteKey;
        //ImageCodecInfo info = null;
        //EncoderParameters ep =null;
        /// <summary>
        /// For drawing rectangle
        /// </summary>
        private int cropX;
        private int cropY;
        private int cropWidth;
        private int cropHeight;
        private double constRotateAngle;
        private int OperationInProgress;
        private bool hasPhotoBol;
        private bool delinsrtBol = false;
        //private Bitmap cropBitmap;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);
        private Pen cropPen;
        private int cropPenSize = 1;
        private System.Drawing.Color cropPenColor = System.Drawing.Color.Blue;

        public static string projKey = null;
        public static string bundleKey = null;
        public static string boxNumber = null;
        OdbcDataAdapter sqlAdap;

        frmdialog dg;
        private int zoomWidth;
        private int zoomHeight;
        private Size zoomSize = new Size();
        private int keyPressed = 1;
        private ImageConfig config = null;

        private ListBox policyLst = null;
        private ListBox delImgList = null;
        private Label lblImageSize = null;
        private Label lblinfo = null;
        private ListBox imageLst = null;
        private ListBox imageDelLst = null;
        private CtrlPolicy ctrlPolicy = null;
        private wfePolicy policy = null;
        private udtPolicy policyData = null;
        private FileorFolder fileMove = null;
        private string sourceFilePath = null;
        private string qcFolderName = null;
        private ToolTip tp = new ToolTip();
        private Imagery img;
        private string policy_Path = string.Empty;
        private int policyLen = 0;


        wfeBatch pBatch;
        wfeProject pProject;

        private int indexCount = 0;
        private eSTATES[] currState;
        private eSTATES[] imageCurrState;
        private int pLeft = 0;
        private int pTop = 0;

        public static string dept;
        public static string category;

        string iniPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\" + "IhConfiguration.ini";
        INIFile ini = new INIFile();

        public aeImageQc()
        {
            InitializeComponent();
            //m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            this.Text = "Image Quality Control";

            exMailLog.SetNextLogger(exTxtLog);
        }

        //public aeImageQC()
        //{
        //    InitializeComponent();
        //    m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        //    this.Text = "Image Quality Control";

        //    exMailLog.SetNextLogger(exTxtLog);

        //}

        public aeImageQc(OdbcConnection prmCon, Credentials prmCrd)
        {
            sqlCon = prmCon;
            //wBox = prmBox;
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            crd = prmCrd;
            InitializeComponent();
            img = IgrFactory.GetImagery(Constants.IGR_CLEARIMAGE);
            currState = new eSTATES[1];

            currState[0] = eSTATES.POLICY_SCANNED;
           
            //img = IgrFactory.GetImagery(Constants.IGR_GDPICTURE);
            //m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            this.Text = "Image Quality Control" + "Connection: " + sqlCon.ConnectionString.ToString();
            exMailLog.SetNextLogger(exTxtLog);
        }

        void ReadConfigKey()
        {
            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            cropKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.CROP_KEY).Remove(1, 1).Trim();
            zoomInKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ZOOM_IN_KEY).Remove(1, 1).Trim();
            zoomOutKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ZOOM_OUT_KEY).Remove(1, 1).Trim();
            autoCropKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.AUTO_CROP_KEY).Remove(1, 1).Trim();
            rotateRKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ROTATE_RIGHT_KEY).Remove(1, 1).Trim();
            rotateLKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ROTATE_LEFT_KEY).Remove(1, 1).Trim();
            skewRKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.SKEW_RIGHT_KEY).Remove(1, 1).Trim();
            skewLKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.SKEW_LEFT_KEY).Remove(1, 1).Trim();
            noiseRemovalLKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.NOISE_REMOVE_KEY).Remove(1, 1).Trim();
            cleanKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.CLEAN_KEY).Remove(1, 1).Trim();
            deleteKey = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.DELETE_KEY).Remove(1, 1).Trim();
        }
        public string GetProjectName(int prmProjectKey)
        {
            string sqlStr = null;
            string projName = null;

            DataSet projDs = new DataSet();

            try
            {
                sqlStr = "select proj_key,proj_code from project_master where proj_key=" + prmProjectKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                projName = projDs.Tables[0].Rows[0]["proj_code"].ToString();
            }
            else
                projName = string.Empty;
            return projName;
        }
        public DataTable _GetBundleStatus(string proj, string bundle)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct status,dept_name,category from batch_master where proj_code = '" + proj + "' and batch_key = '" + bundle + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        void DisplayValues()
        {
            projKey = frmMain.projKey;
            bundleKey = frmMain.bundleKey;
            boxNumber = "1";
            lblProject.Text = "Project: " + GetProjectName(Convert.ToInt32(projKey));
            lblBatch.Text = "Batch: " + GetBundleName(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey));
            lblBox.Text = "Box: " + boxNumber;
            lblProject.ForeColor = Color.RoyalBlue;
            lblBatch.ForeColor = Color.RoyalBlue;
            lblBox.ForeColor = Color.RoyalBlue;
            lblCount.ForeColor = Color.RoyalBlue;

            dept = _GetBundleStatus(projKey, bundleKey).Rows[0][1].ToString();
            category = _GetBundleStatus(projKey, bundleKey).Rows[0][2].ToString();

        }
        public string GetBundleName(int prmProjectKey, int prmBundleKey)
        {
            string sqlStr = null;
            string projName = null;

            DataSet projDs = new DataSet();

            try
            {
                sqlStr = "select batch_code,batch_name from batch_master where proj_code= '" + prmProjectKey + "' and batch_key = '" + prmBundleKey + "' ";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                projName = projDs.Tables[0].Rows[0]["batch_code"].ToString();
            }
            else
                projName = string.Empty;
            return projName;
        }

        int CropRegister()
        {
            OperationInProgress = ihConstants._CROP;
            delinsrtBol = false;
            return 0;
        }

        int CleanImageRegister()
        {
            OperationInProgress = ihConstants._CLEAN;
            delinsrtBol = false;
            //ChangeSize();
            return 0;
        }
        int PhotoCropRegister()
        {
            delinsrtBol = false;
            imageLst = lstImage;
            if ((hasPhotoBol == true) && ((imageLst.SelectedIndex + 1) == ihConstants._PHOTO_PAGE_POSITION))
            {
                if (File.Exists(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString()) == false)
                {
                    File.Copy(sourceFilePath + "\\" + imageLst.SelectedItem.ToString(), qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString());
                }
                imgFileName = qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString();

                img.LoadBitmapFromFile(imgFileName);
                //pictureControl.Image = img.GetBitmap();

                ChangeSize();
                OperationInProgress = ihConstants._PHOTO_CROP;
                prmButtonCrop.Enabled = false;
                prmButtonAutoCrp.Enabled = false;
                prmButtonRotateRight.Enabled = false;
                prmButtonRotateLeft.Enabled = false;
                prmButtonZoomIn.Enabled = false;
                prmButtonZoomOut.Enabled = false;
                prmButtonSkewRight.Enabled = false;
                prmButtonSkewLeft.Enabled = false;
                prmButtonNoiseRemove.Enabled = false;
                prmButtonCleanImg.Enabled = false;
                prmButtonCopyImage.Enabled = false;
                prmButtonDelImage.Enabled = false;
                prmButtonRescan.Enabled = false;
                prmGetPhoto.Enabled = false;
                //prmButtonSkewRight.Enabled = true;
            }
            //ChangeSize();
            return 0;
        }
        int EndPhotoCrop()
        {
            string photoName;
            wfeImage wImg;
            long fileSize;

            try
            {
                System.IO.FileInfo info = null;
                imgFileName = qcFolderName + "\\" + imageLst.SelectedItem.ToString().Substring(0, policyLen) + "_000_A.TIF";
                if (File.Exists(imgFileName))
                {
                    if (File.Exists(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString()))
                    {
                        File.Delete(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString());
                    }
                    OperationInProgress = ihConstants._NO_OPERATION;
                    if (photoCropOperation == true)
                    {
                        info = new System.IO.FileInfo(imgFileName);
                        fileSize = info.Length;
                        fileSize = fileSize / 1024;

                        policyLst = lstPolicy;
                        imageLst = lstImage;
                        photoName = imageLst.SelectedItem.ToString().Substring(0, policyLen) + "_000_A.TIF";

                        pImage = new CtrlImage(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), boxNumber, policyLst.SelectedItem.ToString(), photoName, string.Empty);

                        wImg = new wfeImage(sqlCon, pImage);
                        if (wImg.DeletePage() == true)
                        {
                            wImg.Save(crd, eSTATES.PAGE_QC, fileSize, ihConstants._PHOTO_PAGE, 0, photoName);
                        }
                        //Save the colour photograph into 000
                        //img.SaveAsTiff(imgFileName,IGRComressionTIFF.JPEG);
                    }
                    //Now load the black and white image	
                }
                imgFileName = qcFolderName + "\\" + imageLst.SelectedItem.ToString();

                ChangeSize(imgFileName);
                if (File.Exists(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString()))
                {
                    File.Delete(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString());
                }
                if (File.Exists(qcFolderName + "\\" + "0_" + imageLst.SelectedItem.ToString()))
                {
                    File.Delete(qcFolderName + "\\" + "0_" + imageLst.SelectedItem.ToString());
                }
                prmButtonCrop.Enabled = true;
                prmButtonAutoCrp.Enabled = true;
                prmButtonRotateRight.Enabled = true;
                prmButtonRotateLeft.Enabled = true;
                prmButtonZoomIn.Enabled = true;
                prmButtonZoomOut.Enabled = true;
                prmButtonSkewRight.Enabled = true;
                prmButtonSkewLeft.Enabled = true;
                prmButtonNoiseRemove.Enabled = true;
                prmButtonCleanImg.Enabled = true;
                prmButtonCopyImage.Enabled = true;
                prmButtonDelImage.Enabled = true;
                prmButtonRescan.Enabled = true;
                photoCropOperation = false;
                getPhotoOperation = false;
                prmPhotoCrop.Enabled = true;
                prmGetPhoto.Enabled = true;
                prmButtonSkewRight.Enabled = true;
                prmNext = cmdNext;
                prmPrevious = cmdPrevious;
                prmNext.Enabled = true;
                prmPrevious.Enabled = true;
                delinsrtBol = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return 0;
        }
        int GetPhoto()
        {

            try
            {
                prmButtonRescan.Enabled = false;
                prmButtonCopyImage.Enabled = false;
                prmPhotoCrop.Enabled = false;
                //imageLst = (ListBox)BoxDtls.Controls["lstImage"];

                //img.UpdateImageSize(crd, eSTATES.PAGE_QC,fileSize);
                imgFileName = qcFolderName + "\\" + imageLst.SelectedItem.ToString().Substring(0, policyLen) + "_000_A.TIF";
                if (File.Exists(imgFileName))
                {
                    /*
                    if (ino > 0)
                        imgQc.ReleaseGdPictureImage(ino);
	    		
                    ino=imgQc.CreateGdPictureImageFromFile(imgFileName);
                    imgQc.SaveAsTIFF(ino,imgFileName,GdPicture.TiffCompression.TiffCompressionJPEG);
                    Bitmap newBmp = new Bitmap(pictureControl.Width,pictureControl.Height);
                    newBmp = imgQc.GetBitmapFromGdPictureImage(ino);                           
                    pictureControl.Image = newBmp;//imgQc.GetBitmapFromGdPictureImage(ino);
                    */
                    //img.LoadBitmapFromFile(imgFileName);
                    //img.SaveAsTiff(imgFileName, IGRComressionTIFF.JPEG);
                    ChangeSize(imgFileName);
                    //pictureControl.Image=img.GetBitmap();
                    getPhotoOperation = true;
                    prmNext = cmdNext;
                    prmPrevious = cmdPrevious;
                    prmNext.Enabled = false;
                    prmPrevious.Enabled = false;
                    //prmButtonSkewRight.Enabled = false;
                    prmButtonDelImage.Enabled = false;
                    delinsrtBol = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return 0;
        }
        void ShowImage(bool prmOverWrite)
        {
            string policyPath;
            string policyName;
            string changedImageName = string.Empty;
            DataSet ds = new DataSet();
            int photoImageName;
            try
            {
                policyLst = lstPolicy;
                imageLst = lstImage;
                if (policyLst.SelectedItem != null)
                {
                    //Check for policy has photo or not
                    ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString());
                    policy = new wfePolicy(sqlCon, ctrlPolicy);

                    //policyData = (udtPolicy)policy.LoadValuesFromDB();

                    policyPath = policy_Path;
                    //ds	= policy.GetPolicyList();
                    //pageCount = wPolicy.GetPolicyPageCount();
                    //prmPhotoCrop = new System.Windows.Forms.Button();
                    //prmEndPhotoCrop = new System.Windows.Forms.Button();
                    //tssBatch.Text = string.Empty;
                    if (GetPolicyPhotoStatus() == ihConstants._POLICY_CONTAINS_PHOTO)
                    {
                        if ((imageLst.SelectedIndex + 1) == ihConstants._PHOTO_PAGE_POSITION)
                        {
                            prmPhotoCrop.Enabled = true;
                            prmEndPhotoCrop.Enabled = true;
                            hasPhotoBol = true;
                            if (File.Exists(policyPath + "\\" + ihConstants._QC_FOLDER + "\\" + imageLst.SelectedItem.ToString().Substring(0, policyLen) + "_000_A.TIF") == false)
                            {
                                lblNote.Text = "Warning: This policy contains photo, crop it with photo crop button.";
                                lblNote.Visible = true;
                                lblNote.ForeColor = Color.Red;
                            }
                            else
                            {
                                lblNote.Visible = true;
                                lblNote.Text = "Photo crop has been done, select GetPhoto to check it";
                                lblNote.ForeColor = Color.Green;
                            }
                        }
                        else
                        {
                            prmPhotoCrop.Enabled = false;
                            prmEndPhotoCrop.Enabled = false;
                            hasPhotoBol = false;
                            lblNote.Visible = false;
                        }
                    }
                    else
                    {
                        prmPhotoCrop.Enabled = false;
                        prmEndPhotoCrop.Enabled = false;
                        hasPhotoBol = false;
                        lblNote.Visible = false;
                    }
                    policyName = policyLst.SelectedItem.ToString();
                    if (lstImage.SelectedItems.Count > 0)
                    { 
                        changedImageName = imageLst.SelectedItem.ToString();

                        //			ctrlPolicy = new CtrlPolicy(Convert.ToInt32(wBox.ctrlBox.ProjectCode),Convert.ToInt32(wBox.ctrlBox.BatchKey),wBox.ctrlBox.BoxNumber.ToString(), policyLst.SelectedItem.ToString());
                        //		    policy = new wfePolicy(sqlCon, ctrlPolicy);

                        fileMove = new FileorFolder();
                        string sourcePath = policyPath + "\\" + ihConstants._SCAN_FOLDER;
                        string destPath = policyPath + "\\" + ihConstants._QC_FOLDER;
                        sourceFilePath = sourcePath;
                        qcFolderName = destPath;
                        if (policyPath == null || policyPath == "")
                        {

                        }
                        else
                        {
                            if (FileorFolder.CreateFolder(destPath) == true)
                            {
                                if (File.Exists(destPath + "\\" + changedImageName) == false)
                                {
                                    File.Delete(destPath + "\\" + changedImageName);
                                    File.Copy(sourcePath + "\\" + changedImageName, destPath + "\\" + changedImageName, true);
                                    //fileMove.MoveFile(sourcePath, destPath, changedImageName, true);
                                }
                                imgFileName = destPath + "\\" + changedImageName;


                                if (File.Exists(destPath + "\\" + "0_" + changedImageName))
                                {
                                    File.Delete(destPath + "\\" + "0_" + changedImageName);
                                }
                                photoImageName = 100; //Convert.ToInt32(changedImageName.Substring(10, 3));
                                if (((imageLst.SelectedIndex + 1) == ihConstants._PHOTO_PAGE_POSITION) && (hasPhotoBol == true) && ihConstants._PHOTO_PAGE_POSITION == photoImageName)
                                {
                                    if ((File.Exists(destPath + "\\" + "1_" + changedImageName) == false) && (File.Exists(destPath + "\\" + imageLst.SelectedItem.ToString().Substring(0, policyLen) + "_000_A.TIF") == false))
                                    {
                                        File.Copy(imgFileName, destPath + "\\" + "0_" + changedImageName);
                                        img.ConvertTo1Bpp(destPath + "\\" + "0_" + changedImageName, destPath + "\\" + "1_" + changedImageName);
                                        File.Move(imgFileName, destPath + "\\" + "2_" + changedImageName);
                                        File.Move(destPath + "\\" + "1_" + changedImageName, imgFileName);
                                        File.Move(destPath + "\\" + "2_" + changedImageName, destPath + "\\" + "1_" + changedImageName);
                                        prmGetPhoto.Enabled = true;
                                        prmPhotoCrop.Enabled = true;
                                        prmEndPhotoCrop.Enabled = true;
                                    }
                                    if (File.Exists(destPath + "\\" + imageLst.SelectedItem.ToString().Substring(0, policyLen) + "_000_A.TIF") == true)
                                    {
                                        prmGetPhoto.Enabled = true;
                                    }
                                }
                                else
                                {
                                    prmGetPhoto.Enabled = false;
                                    prmPhotoCrop.Enabled = false;
                                    prmEndPhotoCrop.Enabled = false;
                                }
                                img.LoadBitmapFromFile(imgFileName);
                                //AutoSkew();
                                ChangeSize();
                                //pictureControl.Image = img.GetBitmap();
                                System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                                long fileSize = info.Length;
                                fileSize = fileSize / 1024;
                                // lblImageSize = lblImageSize;
                                lblImgSize.Text = fileSize.ToString() + " KB";
                                //lblImageSize.Text = fileSize.ToString() + " KB";
                                //lblinfo = (Label)BoxDtls.Controls["lblinfo"];
                                //lblinfo.Text = "Press F5 to move to the next File";
                                lblinformation.Text = "Press F5 to move to the next File";

                            }
                            else
                                MessageBox.Show("Error while creaing QC folder");
                        }
                    }

                    

                }
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error while showing the image " + ex.Message ,"Image error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + Convert.ToInt32(projKey) + " ,Batch-" + Convert.ToInt32(bundleKey) + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
        }
        public int GetPolicyPhotoStatus()
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                sqlStr = "select photo from metadata_entry where proj_code=" + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + "  and filename ='" + ctrlPolicy.PolicyNumber + "'";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }

            return Convert.ToInt32(policyDs.Tables[0].Rows[0]["photo"].ToString());
        }
        private void ChangeSize()
        {
            try
            {
                if (img.IsValid() == true)
                {
                    if (pictureControl.Dock != DockStyle.Fill)
                    {
                        //pictureControl.Dock = DockStyle.Fill;
                    }
                    if (!System.IO.File.Exists(imgFileName)) return;
                    Image newImage = img.GetBitmap();
                    if (newImage.PixelFormat == PixelFormat.Format1bppIndexed)
                    {
                        pictureControl.Image = null;
                        pictureControl.Width = 559;
                        pictureControl.Height = 680;
                        if (!System.IO.File.Exists(imgFileName)) return;
                        double scaleX = (double)pictureControl.Width / (double)newImage.Width;
                        double scaleY = (double)pictureControl.Height / (double)newImage.Height;
                        double Scale = Math.Min(scaleX, scaleY);
                        int w = (int)(newImage.Width * Scale);
                        int h = (int)(newImage.Height * Scale);
                        pictureControl.Width = 559;
                        pictureControl.Height = 680;
                        pictureControl.Image = CreateThumbnail(newImage, w, h); //newImage.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
                        newImage.Dispose();
                    }
                    else
                    {
                        img.LoadBitmapFromFile(imgFileName);
                        pictureControl.Image = img.GetBitmap();
                        pictureControl.SizeMode = PictureBoxSizeMode.StretchImage;
                        //pictureControl.Dock= DockStyle.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                exMailLog.Log(ex);
                MessageBox.Show("Error while" + ex.Message, "Error");
            }
        }
        private void ChangeSize(string fName)
        {
            Image imgTot = null;
            try
            {
                if (img.IsValid() == true)
                {
                    if (pictureControl.Dock != DockStyle.Fill)
                    {
                        //pictureControl.Dock = DockStyle.Fill;
                    }
                    pictureControl.Width = 559;
                    pictureControl.Height = 680;
                    if (!System.IO.File.Exists(fName)) return;
                    Image newImage;
                    img.LoadBitmapFromFile(fName);
                    if (img.GetBitmap().PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        img.GetLZW("tmp.TIF");
                        imgTot = Image.FromFile("tmp.TIF");
                        newImage = imgTot;
                        //File.Delete("tmp1.TIF");
                    }
                    else
                    {
                        newImage = System.Drawing.Image.FromFile(fName);
                    }
                    double scaleX = (double)pictureControl.Width / (double)newImage.Width;
                    double scaleY = (double)pictureControl.Height / (double)newImage.Height;
                    double Scale = Math.Min(scaleX, scaleY);
                    int w = (int)(newImage.Width * Scale);
                    int h = (int)(newImage.Height * Scale);
                    pictureControl.Width = 559;
                    pictureControl.Height = 680;
                    pictureControl.Image = CreateThumbnail(newImage, w, h); //newImage.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
                    if (imgTot != null)
                    {
                        imgTot.Dispose();
                        imgTot = null;
                        if (File.Exists("tmp.tif"))
                            File.Delete("tmp.TIF");
                    }
                    if (newImage != null)
                    {
                        newImage.Dispose();
                        newImage = null;
                    }
                }
            }
            catch (Exception ex)
            {
                exMailLog.Log(ex);
                MessageBox.Show("Error while cropping the image" + ex.Message, "Crop error");
            }
        }
        public Image CreateThumbnail(Image pImage, int lnWidth, int lnHeight)
        {

            Bitmap bmp = new Bitmap(lnWidth, lnHeight);
            try
            {

                DateTime stdt = DateTime.Now;

                //create a new Bitmap the size of the new image

                //create a new graphic from the Bitmap
                Graphics graphic = Graphics.FromImage((Image)bmp);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //draw the newly resized image
                graphic.DrawImage(pImage, 0, 0, lnWidth, lnHeight);
                //dispose and free up the resources
                graphic.Dispose();
                DateTime dt = DateTime.Now;
                TimeSpan tp = dt - stdt;
                //MessageBox.Show(tp.Milliseconds.ToString());
                //return the image

            }
            catch
            {
                return null;
            }
            return (Image)bmp;
        }

        private string GetPolicyPath()
        {
            policyLst = lstPolicy;
            wfeBatch wBatch = new wfeBatch(sqlCon);
            string batchPath = GetPath(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey));
            return batchPath + "\\" + policyLst.SelectedItem.ToString();
        }
        public string GetPath(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();
            string Path;

            try
            {
                sqlStr = @"select batch_path from batch_master where proj_code=" + prmProjKey + " and batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                //err = ex.Message;
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                Path = projDs.Tables[0].Rows[0]["batch_path"].ToString();
            }
            else
                Path = string.Empty;

            return Path;
        }


        int ZoomIn()
        {
            try
            {
                if (img.IsValid() == true)
                {
                    pictureControl.Dock = DockStyle.None;
                    //OperationInProgress = ihConstants._OTHER_OPERATION;
                    keyPressed = keyPressed + 1;
                    zoomHeight = Convert.ToInt32(img.GetBitmap().Height * (1.2));
                    zoomWidth = Convert.ToInt32(img.GetBitmap().Width * (1.2));
                    zoomSize.Height = zoomHeight;
                    zoomSize.Width = zoomWidth;

                    pictureControl.Width = Convert.ToInt32(Convert.ToDouble(pictureControl.Width) * 1.2);
                    pictureControl.Height = Convert.ToInt32(Convert.ToDouble(pictureControl.Height) * 1.2);
                    pictureControl.Refresh();
                    ChangeZoomSize();
                    delinsrtBol = false;
                    cmdNext.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while zooming the image " + ex.Message, "Zoom Error");
                exMailLog.Log(ex);
            }
            return 0;
        }

        private void ChangeZoomSize()
        {
            if (!System.IO.File.Exists(imgFileName)) return;
            Image newImage = Image.FromFile(imgFileName);
            double scaleX = (double)pictureControl.Width / (double)newImage.Width;
            double scaleY = (double)pictureControl.Height / (double)newImage.Height;
            double Scale = Math.Min(scaleX, scaleY);
            int w = (int)(newImage.Width * Scale);
            int h = (int)(newImage.Height * Scale);
            //pictureControl.Width = w;
            //pictureControl.Height = h;
            pictureControl.Image = newImage.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
            newImage.Dispose();
        }
        private bool GetThumbnailImageAbort()
        {
            return false;
        }
        int ZoomOut()
        {
            try
            {
                if (keyPressed > 0)
                {
                    pictureControl.Dock = DockStyle.None;
                    //OperationInProgress = ihConstants._OTHER_OPERATION;
                    keyPressed = keyPressed + 1;
                    zoomHeight = Convert.ToInt32(img.GetBitmap().Height / (1.2));
                    zoomWidth = Convert.ToInt32(img.GetBitmap().Width / (1.2));
                    zoomSize.Height = zoomHeight;
                    zoomSize.Width = zoomWidth;

                    pictureControl.Width = Convert.ToInt32(Convert.ToDouble(pictureControl.Width) / 1.2);
                    pictureControl.Height = Convert.ToInt32(Convert.ToDouble(pictureControl.Height) / 1.2);
                    pictureControl.Refresh();
                    ChangeZoomSize();
                    delinsrtBol = false;
                    cmdNext.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while zooming the image " + ex.Message, "Zoom Error");
                error = ex.Message;
            }
            return 0;
        }
        int RotateRight()
        {
            long fileSize;

            //OperationInProgress = ihConstants._OTHER_OPERATION;

            try
            {
                //Rotate right +90
                img.RotateRight();
                //Call the save routine
                img.SaveFile(imgFileName);
                img.LoadBitmapFromFile(imgFileName);
                //Show the image back in picture box
                //pictureControl.Image = img.GetBitmap();
                ChangeSize();
                System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                fileSize = info.Length;
                fileSize = fileSize / 1024;
                lblImgSize.Text = fileSize.ToString() + " KB";
                delinsrtBol = false;
                UpdateImageSize(fileSize);
                cmdNext.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while rotate the image" + ex.Message, "Rotation Error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Bundle-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }

            return 0;
        }

        int RotateLeft()
        {
            long fileSize;

            //OperationInProgress = ihConstants._OTHER_OPERATION;

            try
            {
                if (img.IsValid() == true)
                {
                    //Rotate right -90
                    img.RotateLeft();
                    //Call the save routine
                    img.SaveFile(imgFileName);
                    img.LoadBitmapFromFile(imgFileName);
                    //Show the image back in picture box
                    //pictureControl.Image = img.GetBitmap();
                    ChangeSize();
                    delinsrtBol = false;
                    System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                    fileSize = info.Length;
                    fileSize = fileSize / 1024;
                    lblImgSize.Text = fileSize.ToString() + " KB";
                    UpdateImageSize(fileSize);
                    cmdNext.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while rotate the image" + ex.Message, "Rotation Error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Bundle-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            //imgQc.Write(imgFileName);

            return 0;
        }
        int AutoCrop()
        {
            long fileSize;
            try
            {
                //Auto Crop
                img.AutoCrop();
                //Call the save routine
                img.SaveFile(imgFileName);
                //Show the image back in picture box
                img.LoadBitmapFromFile(imgFileName);

                ChangeSize();

                System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                fileSize = info.Length;
                fileSize = fileSize / 1024;
                // lblImageSize = lblImageSize;
                lblImgSize.Text = fileSize.ToString() + " KB";
                delinsrtBol = false;

                UpdateImageSize(fileSize);
                cmdNext.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while auto cropping " + ex.Message, "Auto Crop Error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Bundle-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return 0;
        }

        int SkewRight()
        {
            long fileSize;
            //OperationInProgress = ihConstants._OTHER_OPERATION;
            try
            {
                if (img.IsValid() == true)
                {
                    //Auto Deskew
                    img.AutoDeSkew(true);
                    //Call the save routine
                    img.SaveFile(imgFileName);
                    img.LoadBitmapFromFile(imgFileName);
                    //Show the image back in picture box
                    //pictureControl.Image = img.GetBitmap();
                    ChangeSize();
                    System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                    fileSize = info.Length;
                    fileSize = fileSize / 1024;
                    //lblImageSize = (Label)BoxDtls.Controls["lblImageSize"];
                    lblImgSize.Text = fileSize.ToString() + " KB";
                    UpdateImageSize(fileSize);
                    cmdNext.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while rotate the image" + ex.Message, "Rotation Error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Bundle-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return 0;
        }

        void UpdateImageSize(long prmSize)
        {
            //string photoName;
            wfeImage img;
            //long fileSize;			
            policyLst = lstPolicy;
            imageLst = lstImage;

            pImage = new CtrlImage(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString(), imageLst.SelectedItem.ToString(), string.Empty);
            img = new wfeImage(sqlCon, pImage);
            img.UpdateImageSize(crd, eSTATES.PAGE_QC, prmSize);
        }
        int SkewLeft()
        {
            //            OperationInProgress = ihConstants._OTHER_OPERATION;
            //
            //            //rotateAngle = rotateAngle + constRotateAngle;
            //            imgQc = objQc.Skew(imgQc,(-skewXAngle),(-skewYAngle));
            //            pictureControl.Image = MagickNet.Image.ToBitmap(imgQc);
            //            pictureControl.Refresh();
            //            //imgQc.Write(imgFileName);

            return 0;
        }
        int NoiseRemove()
        {
            long fileSize;
            ComboBox noiseVal = cmbDesValue;
            try
            {
                if (img.IsValid() == true)
                {
                    //Auto Deskew
                    //img.Despeckle();
                    img.Despeckle(Convert.ToInt32(Convert.ToInt32(noiseVal.Text)));
                    //Call the save routine
                    img.SaveFile(imgFileName);
                    img.LoadBitmapFromFile(imgFileName);
                    //Show the image back in picture box
                    //pictureControl.Image = img.GetBitmap();
                    ChangeSize();
                    System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                    fileSize = info.Length;
                    fileSize = fileSize / 1024;
                    //lblImageSize = (Label)BoxDtls.Controls["lblImageSize"];
                    lblImgSize.Text = fileSize.ToString() + " KB";
                    UpdateImageSize(fileSize);
                    cmdNext.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while rotate the image" + ex.Message, "Rotation Error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Bundle-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return 0;
        }

        void EnableDisbleControls(bool prmControl)
        {
            prmButtonCrop.Enabled = prmControl;
            prmButtonAutoCrp.Enabled = prmControl;
            prmButtonRotateRight.Enabled = prmControl;
            prmButtonRotateLeft.Enabled = prmControl;
            prmButtonZoomIn.Enabled = prmControl;
            prmButtonZoomOut.Enabled = prmControl;
            prmButtonSkewRight.Enabled = prmControl;
            prmButtonSkewLeft.Enabled = prmControl;
            prmButtonNoiseRemove.Enabled = prmControl;
            prmButtonCleanImg.Enabled = prmControl;
            prmButtonCopyImage.Enabled = prmControl;
            prmButtonDelImage.Enabled = prmControl;
            prmButtonRescan.Enabled = prmControl;
            //deButton1.Enabled = prmControl;
        }

        int ImageCopy()
        {
            string policyPath;
            string policyName;
            string changedImageName = string.Empty;
            DataSet ds = new DataSet();

            try
            {
                delinsrtBol = false;
                policyLst = lstPolicy;
                imageLst = lstImage;

                //Check for policy has photo or not
                ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), boxNumber, policyLst.SelectedItem.ToString());
                policy = new wfePolicy(sqlCon, ctrlPolicy);
                if (GetPolicyPhotoStatus() == ihConstants._POLICY_CONTAINS_PHOTO)
                {
                    if ((imageLst.SelectedIndex + 1) == ihConstants._PHOTO_PAGE_POSITION)
                    {
                        prmPhotoCrop.Enabled = true;
                        prmEndPhotoCrop.Enabled = true;
                        hasPhotoBol = true;
                    }
                    else
                    {
                        prmPhotoCrop.Enabled = false;
                        prmEndPhotoCrop.Enabled = false;
                        hasPhotoBol = false;
                    }
                }
                else
                {
                    prmPhotoCrop.Enabled = false;
                    prmEndPhotoCrop.Enabled = false;
                    hasPhotoBol = false;
                }

                policyName = policyLst.SelectedItem.ToString();
                changedImageName = imageLst.SelectedItem.ToString();
                //policyData=(udtPolicy)policy.LoadValuesFromDB();
                policyPath = GetPolicyPath(); //policyData.policy_path;
                fileMove = new FileorFolder();
                string sourcePath = policyPath + "\\" + ihConstants._SCAN_FOLDER;
                string destPath = policyPath + "\\" + ihConstants._QC_FOLDER;
                sourceFilePath = sourcePath;
                qcFolderName = destPath;


                if (((imageLst.SelectedIndex + 1) == ihConstants._PHOTO_PAGE_POSITION) && (hasPhotoBol == true))
                {
                    img.LoadBitmapFromFile(sourcePath + "\\" + changedImageName);

                    if (img.GetBitmap().PixelFormat != PixelFormat.Format1bppIndexed)
                    {
                        img.ToBitonal();
                    }
                    pictureControl.Image = img.GetBitmap();
                    img.SaveFile(destPath + "\\" + changedImageName);
                }
                else
                {
                    //ShowImage();
                    img.LoadBitmapFromFile(sourcePath + "\\" + changedImageName);
                    img.SaveFile(destPath + "\\" + changedImageName);
                    img.LoadBitmapFromFile(imgFileName);
                }
                ChangeSize();
                ds.Dispose();
                pictureControl.Refresh();
                cmdNext.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while copying the original image", "Copy Error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Batch-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return 0;
        }
        int ImageDelete()
        {
            string qcDelPath = null;
            string sourceFileName = null;
            string destFileName = null;
            string qcFilePath = null;

            delinsrtBol = true;
            pImage = new CtrlImage(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), boxNumber, policyLst.SelectedItem.ToString(), imageLst.SelectedItem.ToString(), string.Empty);
            if (UpdateState(eSTATES.PAGE_DELETED, imageLst.SelectedItem.ToString()) == true)
            {
                qcDelPath = sourceFilePath + "\\" + ihConstants._DELETE_FOLDER;
                imageLst = lstImage;
                sourceFileName = sourceFilePath + "\\" + imageLst.SelectedItem.ToString();
                destFileName = qcDelPath + "\\" + imageLst.SelectedItem.ToString();
                qcFilePath = qcFolderName + "\\" + imageLst.SelectedItem.ToString();
                if (FileorFolder.CreateFolder(qcDelPath) == true)
                {
                    if (File.Exists(destFileName) == false)
                    {
                        File.Move(sourceFileName, destFileName);
                        File.Delete(qcFilePath);
                    }
                }
                DeleteNotify(imageLst.SelectedIndex);
                ShowImage(true);
                cmdNext.Focus();
                deLabel2.Text = "Total Scanned Image : " + lstImage.Items.Count.ToString();
            }
            return 0;
        }
        bool UpdateState(eSTATES prmPageSate, string prmPageName)
        {
            double fileSize;
            bool delBol = false;
            NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();

            pImage = new CtrlImage(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), boxNumber, policyLst.SelectedItem.ToString(), prmPageName, string.Empty);
            wfeImage wImage = new wfeImage(sqlCon, pImage);
            if (wImage.UpdateStatus(prmPageSate, crd) == true)
            {
                delBol = true;
            }
            if (delinsrtBol == false)
            {
                if (imgFileName == null || imgFileName == "")
                { }
                else
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);

                    fileSize = info.Length;
                    fileSize = fileSize / 1024;

                    wImage.UpdateImageSize(crd, eSTATES.PAGE_QC, fileSize);
                }
            }
            return delBol;
        }
        public bool DeleteNotify(int currIndex)
        {
            lstImage.Items.RemoveAt(currIndex);
            //PopulateImageList((int)lstPolicy.SelectedItem);
            if (lstImage.Items.Count > 0)
            {
                if (currIndex != lstImage.Items.Count)
                {
                    lstImage.SelectedIndex = currIndex;
                    indexCount = lstImage.SelectedIndex;
                }
                else
                {
                    if ((lstPolicy.SelectedIndex) != (lstPolicy.Items.Count - 1))
                    {
                        lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
                    }
                    else
                    {
                        lstImage.SelectedIndex = currIndex - 1;
                    }
                }
            }
            else
            {
                if ((lstPolicy.SelectedIndex) != (lstPolicy.Items.Count - 1))
                {
                    lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
                }
            }
            PopulateDelList(lstPolicy.SelectedItem.ToString());

            return true;
        }
        public void PopulateDelList(string prmPolicyNo)
        {
            lstImageDel.Items.Clear();
            CtrlPolicy ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", prmPolicyNo);
            wItem policy = new wfePolicy(sqlCon, ctrlPolicy);
            //ListViewItem lstView;
            ArrayList arrImage = new ArrayList();
            wfeImage wImage = new wfeImage(sqlCon);
            //eSTATES[] policyState=new eSTATES[1];
            eSTATES[] imageState = new eSTATES[1];
            //            policyState[0]=currState;
            imageState[0] = eSTATES.PAGE_DELETED;
            //state[1]=eSTATES.POLICY_SCANNED;
            CtrlImage ctrlImage;
            arrImage = wImage.GetDeletedPageList1(currState, imageState, policy);
            for (int i = 0; i < arrImage.Count; i++)
            {
                ctrlImage = (CtrlImage)arrImage[i];
                //lstView=lstImage.Items.Add(ctrlImage.ImageName);
                lstImageDel.Items.Add(ctrlImage.ImageName);
            }
        }
        void BoxDtlsImageChanged(object sender, EventArgs e)
        {
            imageLst = imageLst = lstImage;
            //if (File.Exists(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString()))
            //{
            //    File.Delete(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString());
            //}
            //if (File.Exists(qcFolderName + "\\" + "0_" + imageLst.SelectedItem.ToString()))
            //{
            //    File.Delete(qcFolderName + "\\" + "0_" + imageLst.SelectedItem.ToString());
            //}
            if ((imageLst.SelectedIndex + 1) == imageLst.Items.Count)
            {
                if (imageLst.Items.Count > 0)
                {
                    UpdateState(eSTATES.PAGE_QC, imageLst.SelectedItem.ToString());
                }
            }
        }
        private void PopulatePolicyList()
        {
            lstPolicy.Items.Clear();
            ArrayList arrPolicy = new ArrayList();
            wQuery pQuery = new ihwQuery(sqlCon, crd);
            CtrlPolicy ctrPol;

            arrPolicy = GetItems(eITEMS.POLICY, currState);
            for (int i = 0; i < arrPolicy.Count; i++)
            {
                ctrPol = (CtrlPolicy)arrPolicy[i];
                lstPolicy.Items.Add(ctrPol.PolicyNumber);
                if (lstPolicy.SelectedIndex == 0)
                {
                    //lstPolicy.Text = ctrPol.PolicyNumber;
                }
            }
            if (arrPolicy.Count > 0)
            {
                lblCount.Text = "Pending: " + GetItems1(eITEMS.POLICY, currState).Count;
            }
            else
            {
                lblCount.Text = "Pending : 0";
                lstImageDel.Items.Clear();
            }
            //CtrlPolicy ctrPolCurrent = null;
            //lblCurrentPolicy.Text = ctrPol.PolicyNumber.ToString();
            //if(lstPolicy.Items.Count >0)
            //{
            //    GetFileDetails(lstPolicy.SelectedIndices[0].ToString());
            //}
        }
        public ArrayList GetItems(eITEMS item, eSTATES[] state)
        {
            OdbcDataAdapter wAdap;
            OdbcTransaction trns = null;
            OdbcCommand oCom = new OdbcCommand();
            string strQuery = null;
            wItemControl wic = null;
            DataSet ds = new DataSet();
            ArrayList arrItem = new ArrayList();
            if (item == eITEMS.POLICY)
            {
                strQuery = "select proj_code,batch_key,filename from metadata_entry where proj_code= '" + projKey + "' and batch_key='" + bundleKey + "' and status ='2' and (locked_uid='" + crd.created_by + "' or expires_dttm <= NOW() or invalid = 0) and 1=1 LIMIT 1 for update";

                oCom.Connection = sqlCon;
                oCom.CommandText = strQuery;
                wAdap = new OdbcDataAdapter(oCom);
                wAdap.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string p = ds.Tables[0].Rows[i]["proj_code"].ToString();
                        string b = ds.Tables[0].Rows[i]["batch_key"].ToString();

                        string pp = ds.Tables[0].Rows[i]["filename"].ToString();
                        //string ii = ds.Tables[0].Rows[i]["filename"].ToString();
                        wic = new CtrlPolicy(Convert.ToInt32(ds.Tables[0].Rows[i]["proj_code"]), Convert.ToInt32(ds.Tables[0].Rows[i]["batch_key"]), "1", ds.Tables[0].Rows[i]["filename"].ToString());
                        arrItem.Add(wic);

                    }
                }
            }
            return arrItem;
        }
        public ArrayList GetItems1(eITEMS item, eSTATES[] state)
        {
            OdbcDataAdapter wAdap;
            OdbcTransaction trns = null;
            OdbcCommand oCom = new OdbcCommand();
            string strQuery = null;
            wItemControl wic = null;
            DataSet ds = new DataSet();
            ArrayList arrItem = new ArrayList();
            if (item == eITEMS.POLICY)
            {
                strQuery = "select proj_code,batch_key,filename from metadata_entry where proj_code= '" + projKey + "' and batch_key='" + bundleKey + "' and status ='2'";

                oCom.Connection = sqlCon;
                oCom.CommandText = strQuery;
                wAdap = new OdbcDataAdapter(oCom);
                wAdap.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string p = ds.Tables[0].Rows[i]["proj_code"].ToString();
                        string b = ds.Tables[0].Rows[i]["batch_key"].ToString();

                        string pp = ds.Tables[0].Rows[i]["filename"].ToString();
                        //string ii = ds.Tables[0].Rows[i]["filename"].ToString();
                        wic = new CtrlPolicy(Convert.ToInt32(ds.Tables[0].Rows[i]["proj_code"]), Convert.ToInt32(ds.Tables[0].Rows[i]["batch_key"]), "1", ds.Tables[0].Rows[i]["filename"].ToString());
                        arrItem.Add(wic);

                    }
                }
            }
            return arrItem;
        }

        private void PopulateImageList(string prmPolicyNo)
        {
            lstImage.Items.Clear();
            CtrlPolicy ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", prmPolicyNo);
            wItem policy = new wfePolicy(sqlCon, ctrlPolicy);
            //ListViewItem lstView;
            ArrayList arrImage = new ArrayList();
            wQuery pQuery = new ihwQuery(sqlCon);
            CtrlImage ctrlImage;
            arrImage = GetImagesItems(eITEMS.PAGE, currState, prmPolicyNo);
            for (int i = 0; i < arrImage.Count; i++)
            {
                ctrlImage = (CtrlImage)arrImage[i];
                //lstView=lstImage.Items.Add(ctrlImage.ImageName);

                if (ctrlImage.DocType != string.Empty)
                {
                    lstImage.Items.Add(ctrlImage.ImageName + "-" + ctrlImage.DocType);
                }
                else
                    lstImage.Items.Add(ctrlImage.ImageName);
            }
        }

        public ArrayList GetImagesItems(eITEMS item, eSTATES[] state, string fileName)
        {
            OdbcDataAdapter wAdap;
            OdbcTransaction trns = null;
            OdbcCommand oCom = new OdbcCommand();
            string strQuery = null;
            wItemControl wic = null;
            DataSet ds = new DataSet();
            string strQr = string.Empty;
            //wfePolicy queryPolicy = (wfePolicy)wi;
            ArrayList arrItem = new ArrayList();

            if (item == eITEMS.PAGE)
            {
                strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.page_index_name,A.doc_type from image_master A,metadata_entry B where A.proj_key=B.proj_code and A.batch_key = B.batch_key  and A.policy_number = B.filename and A.photo <> 1 and A.proj_key=" + projKey + " and A.batch_key=" + bundleKey + " and  A.policy_number='" + fileName + "' and a.status <> 29 and b.status = 2 order by a.serial_no";

                oCom.Connection = sqlCon;
                oCom.CommandText = strQuery;
                wAdap = new OdbcDataAdapter(oCom);
                wAdap.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        wic = new CtrlImage(Convert.ToInt32(ds.Tables[0].Rows[i]["proj_key"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i]["batch_key"].ToString()), "1", ds.Tables[0].Rows[i]["policy_number"].ToString(), ds.Tables[0].Rows[i]["page_name"].ToString(), ds.Tables[0].Rows[i]["doc_type"].ToString());
                        arrItem.Add(wic);
                    }
                }
            }

            return arrItem;
        }

        private void aeImageQc_Load(object sender, EventArgs e)
        {
            ReadConfigKey();
            cmbDesValue.SelectedIndex = 0;
            DisplayValues();

            if (lblImageSize != null)
                lblImageSize.ForeColor = Color.Black;
            System.Windows.Forms.ToolTip bttnToolTip = new System.Windows.Forms.ToolTip();

            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            ArrayList arrPolicy = new ArrayList();

            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(aeImageQC));


            delImage = new NovaNet.Utils.ImageManupulation(CropRegister);
            tp.SetToolTip(prmButtonCrop, "Crop");
            prmButtonCrop.Text = "Crop";
            bttnToolTip.SetToolTip(prmButtonCrop, "Shortcut Key-" + cropKey);
            //Bitmap bmp = new Bitmap("D:\\subhajit\\Projects\\EDMS\\Working_Code\\M31\\ImageHeaven\\Resources\\crop.png");
            //prmButtonCrop.Image = bmp; //((System.Drawing.Image)(resources.GetObject("crop.png")));
            // m_toolbox.AddButton(prmButtonCrop, delImage);
            prmButtonCrop.ForeColor = Color.Black;

            delImage = new NovaNet.Utils.ImageManupulation(AutoCrop);
            //prmButtonAutoCrp = new System.Windows.Forms.Button();
            bttnToolTip.SetToolTip(prmButtonAutoCrp, "Shortcut Key-" + autoCropKey);
            prmButtonAutoCrp.Text = "Auto-Crop";
            //  m_toolbox.AddButton(prmButtonAutoCrp, delImage);
            prmButtonAutoCrp.ForeColor = Color.Black;

            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            //constRotateAngle = Convert.ToDouble(config.GetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION, ihConstants.ROTATE_ANGLE_KEY).Replace("\0", ""));
            delImage = new NovaNet.Utils.ImageManupulation(RotateRight);
            bttnToolTip.SetToolTip(prmButtonRotateRight, "Shortcut Key-" + rotateRKey);
            prmButtonRotateRight.Text = "Rotate Right";
            // m_toolbox.AddButton(prmButtonRotateRight, delImage);
            prmButtonRotateRight.ForeColor = Color.Black;

            //delImage = ZoomOut;
            delImage = new NovaNet.Utils.ImageManupulation(RotateLeft);
            bttnToolTip.SetToolTip(prmButtonRotateLeft, "Shortcut Key-" + rotateLKey);
            prmButtonRotateLeft.Text = "Rotate Left";
            // m_toolbox.AddButton(prmButtonRotateLeft, delImage);
            prmButtonRotateLeft.ForeColor = Color.Black;

            delImage = new NovaNet.Utils.ImageManupulation(ZoomIn);
            bttnToolTip.SetToolTip(prmButtonZoomIn, "Shortcut Key-" + zoomInKey);
            prmButtonZoomIn.Text = "Zoom In";
            // m_toolbox.AddButton(prmButtonZoomIn, delImage);
            prmButtonZoomIn.ForeColor = Color.Black;

            //delImage = ZoomOut;
            delImage = new NovaNet.Utils.ImageManupulation(ZoomOut);
            bttnToolTip.SetToolTip(prmButtonZoomOut, "Shortcut Key-" + zoomOutKey);
            prmButtonZoomOut.Text = "Zoom Out";
            //m_toolbox.AddButton(prmButtonZoomOut, delImage);
            prmButtonZoomOut.ForeColor = Color.Black;

            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            //skewXAngle =Convert.ToDouble(config.GetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION, ihConstants.SKEW_X_KEY).Replace("\0", ""));
            //skewYAngle=Convert.ToDouble(config.GetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION, ihConstants.SKEW_Y_KEY).Replace("\0", ""));
            delImage = new NovaNet.Utils.ImageManupulation(SkewRight);
            bttnToolTip.SetToolTip(prmButtonSkewRight, "Shortcut Key-" + skewRKey);
            prmButtonSkewRight.Text = "Deskew";
            //m_toolbox.AddButton(prmButtonSkewRight, delImage);
            prmButtonSkewRight.ForeColor = Color.Black;

            //            delImage = new NovaNet.Utils.ImageManupulation(SkewLeft);
            //			//prmButtonSkewLeft = new System.Windows.Forms.Button();
            //			prmButtonSkewLeft.Text="Skew Left";
            //            m_toolbox.AddButton(prmButtonSkewLeft,delImage);

            delImage = new NovaNet.Utils.ImageManupulation(NoiseRemove);
            bttnToolTip.SetToolTip(prmButtonNoiseRemove, "Shortcut Key-" + noiseRemovalLKey);
            prmButtonNoiseRemove.Text = "Despacle";
            //prmButtonNoiseRemove.AutoSize=true;
            //m_toolbox.AddButton(prmButtonNoiseRemove, delImage);
            prmButtonNoiseRemove.ForeColor = Color.Black;

            delImage = new NovaNet.Utils.ImageManupulation(CleanImageRegister);
            bttnToolTip.SetToolTip(prmButtonCleanImg, "Shortcut Key-" + cleanKey);
            prmButtonCleanImg.Text = "Clean";
            //prmButtonCleanImg.AutoSize=true;
            //m_toolbox.AddButton(prmButtonCleanImg, delImage);
            prmButtonCleanImg.ForeColor = Color.Black;

            delImage = new NovaNet.Utils.ImageManupulation(ImageCopy);
            bttnToolTip.SetToolTip(prmButtonCopyImage, "Shortcut Key-(Control+z)");
            prmButtonCopyImage.Text = "Copy Original";
            //prmButtonCopyImage.AutoSize=true;
            //m_toolbox.AddButton(prmButtonCopyImage, delImage);
            prmButtonCopyImage.ForeColor = Color.Black;

            delImage = new NovaNet.Utils.ImageManupulation(ImageDelete);
            bttnToolTip.SetToolTip(prmButtonDelImage, "Shortcut Key-" + deleteKey);
            prmButtonDelImage.Text = "Delete";
            //prmButtonDelImage.AutoSize=true;
            //m_toolbox.AddButton(prmButtonDelImage, delImage);
            prmButtonDelImage.ForeColor = Color.Black;
            delImage = new NovaNet.Utils.ImageManupulation(PhotoCropRegister);
            //prmPhotoCrop = new System.Windows.Forms.Button();
            prmPhotoCrop.Text = "Photo Crop";
            //prmPhotoCrop.AutoSize=true;
            //m_toolbox.AddButton(prmPhotoCrop, delImage);
            prmPhotoCrop.ForeColor = Color.Black;

            delImage = new NovaNet.Utils.ImageManupulation(EndPhotoCrop);
            //prmEndPhotoCrop = new System.Windows.Forms.Button();
            prmEndPhotoCrop.Text = "End Edit(Photo)";
            //prmEndPhotoCrop.AutoSize=true;
            //m_toolbox.AddButton(prmEndPhotoCrop, delImage);
            prmEndPhotoCrop.ForeColor = Color.Black;

            delImage = new NovaNet.Utils.ImageManupulation(GetPhoto);
            //prmEndPhotoCrop = new System.Windows.Forms.Button();
            prmGetPhoto.Text = "Get Photo";
            //prmGetPhoto.AutoSize=true;
            //m_toolbox.AddButton(prmGetPhoto, delImage);
            prmGetPhoto.ForeColor = Color.Black;

            //deButton1 = new NovaNet.Utils.ImageManupulation(AutoCrop);
            //prmButtonAutoCrp = new System.Windows.Forms.Button();
            //bttnToolTip.SetToolTip(deButton1, "Shortcut Key-" + "F4");
            //deButton1.Text = "Auto-QC";
            ////  m_toolbox.AddButton(prmButtonAutoCrp, delImage);
            //deButton1.ForeColor = Color.Black;

            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(lstImageDel, "Press (Insert) key to insert this deleted image");

            if ((currState[0] == eSTATES.POLICY_FQC) || (currState[0] == eSTATES.POLICY_EXCEPTION))
            {
                label5.Visible = true;
                cmbBox.Visible = true;
            }
            else
            {
                label5.Visible = false;
                cmbBox.Visible = false;
            }
            PopulatePolicyList();

            if (lstPolicy.Items.Count > 0)
            {
                lstPolicy.SelectedIndex = 0;
            }
            policyLst = lstPolicy;
            if (policyLst.SelectedItem != null)
            {
                ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString());
                policy = new wfePolicy(sqlCon, ctrlPolicy);
                policy_Path = GetPolicyPath();
                if (lstImage.Items.Count == 0)
                {
                    MessageBox.Show(this, "No image found for this file", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pictureControl.Image = null;
                }
                else { ShowImage(false); }
                ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString());
                policy = new wfePolicy(sqlCon, ctrlPolicy);
                //policyData=(udtPolicy)policy.LoadValuesFromDB();
                //policyPath = GetPolicyPath();
                policyLen = policyLst.SelectedItem.ToString().Length;
                //DataSet ds = policy.GetPolicyDetails();
                //Label lblName = (Label)BoxDtls.Controls["lblName"];
                //if (ds.Tables[0].Rows.Count > 0)
                //    lblName.Text = "Name: " + ds.Tables[0].Rows[0]["name_of_policyholder"].ToString();
            }
            deLabel2.Text = "Total Scanned Image : " + lstImage.Items.Count.ToString();
            lstImage.Focus();
            lstImage.Select();
        }

        public DataTable getFileDetails(string projKey, string bundleKey, string file_no)
        {
            DataTable dt = new DataTable();

            string sql = "select filename, state_name, pension_file_no,rule_file,vol,series,acc,emp_name,ge_no,id_no from metadata_entry where proj_code = '" + projKey + "' and batch_key = '" + bundleKey + "' and filename = '" + file_no + "'";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);

            return dt;
        }

        private void GetFileDetails(string file_no)
        {
            DataTable dsVol = new DataTable();

            dsVol = getFileDetails(projKey, bundleKey, file_no);
            if (dsVol.Rows.Count > 0)
            {
                if(category.ToString() == "Pension Case File")
                {
                    deLabel3.Text = "File No : " + dsVol.Rows[0][2].ToString();
                    deLabel4.Text = "Name : "+ dsVol.Rows[0][7].ToString(); 
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
                else if(category.ToString() == "Pension Case Registers")
                {
                    deLabel3.Text = "File No : " + dsVol.Rows[0][2].ToString();
                    deLabel4.Text = "Name : " + dsVol.Rows[0][7].ToString();
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
                else if (category.ToString() == "Pension Rule Files")
                {
                    deLabel3.Text = "File No : " + dsVol.Rows[0][3].ToString();
                    deLabel4.Text = "Volume : "+ dsVol.Rows[0][4].ToString();
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
                else if (category.ToString() == "Ledger Cards")
                {
                    deLabel3.Text = "Series : " + dsVol.Rows[0][5].ToString();
                    deLabel4.Text = "Account No : " + dsVol.Rows[0][6].ToString();
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
                else if (category.ToString() == "Nomination")
                {
                    deLabel3.Text = "Series : " + dsVol.Rows[0][5].ToString();
                    deLabel4.Text = "Account No : " + dsVol.Rows[0][6].ToString();
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
                else if (category.ToString() == "Final Payment Case File")
                {
                    deLabel3.Text = "Series : " + dsVol.Rows[0][5].ToString();
                    deLabel4.Text = "Account No : " + dsVol.Rows[0][6].ToString();
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
                else if (dept.ToLower() == "ge")
                {
                    deLabel3.Text = "GE No : " + dsVol.Rows[0][8].ToString();
                    deLabel4.Text = "Name : " + dsVol.Rows[0][7].ToString();
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
                else if (dept.ToLower() == "admin")
                {
                    deLabel3.Text = "ID No : " + dsVol.Rows[0][9].ToString();
                    deLabel4.Text = "Name : " + dsVol.Rows[0][7].ToString();
                    Txtfirstparty.Text = dsVol.Rows[0][1].ToString();
                }
            }
        }



        [Category("Action")]
        [Description("Fires when the Policy is changed.")]
        public event PolicyChangeHandler PolicyChanged;
        private void lstPolicy_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateImageList(lstPolicy.SelectedItem.ToString());
            wQuery pQuery = new ihwQuery(sqlCon);
            DataSet dsVol = new DataSet();
            CtrlPolicy ctrPol;
            policyLst = lstPolicy;
            imageLst = lstImage;
            ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString());
            policy = new wfePolicy(sqlCon, ctrlPolicy);
            if (lstImage.Items.Count > 0)
            {
                lstImage.SelectedIndex = 0;
            }

            if (PolicyChanged != null)
            {
                PolicyChanged(this, e);
                OdbcTransaction sqlTrans = null;
                GetFileDetails(lstPolicy.SelectedItem.ToString());
                LockPolicy(crd, sqlTrans);
                //sqlTrans.Commit();
                //GetIndexDetails(lstPolicy.SelectedItem.ToString());
            }
            else
            {
                GetFileDetails(lstPolicy.SelectedItem.ToString());
                OdbcTransaction sqlTrans = null;
                GetFileDetails(lstPolicy.SelectedItem.ToString());
                LockPolicy(crd, sqlTrans);
                //sqlTrans.Commit();
            }


            string policyPath;

            if (policyLst.SelectedItem != null)
            {
                EnableDisbleControls(true);
                delImgList = lstImageDel;
                if ((delImgList.SelectedIndex >= 0) && (delImgList.Items.Count > 0))
                {
                    delImgList.SetSelected(delImgList.SelectedIndex, false);
                }
                policyLst = lstPolicy;
                imageLst = lstImage;
                ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString());
                policy = new wfePolicy(sqlCon, ctrlPolicy);
                policy_Path = GetPolicyPath();
                PopulateDelList(policyLst.SelectedItem.ToString());
                if (lstImage.Items.Count == 0)
                {
                    MessageBox.Show(this, "No image found for this file", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pictureControl.Image = null;
                }
                else { ShowImage(false); }

                ctrlPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString());
                policy = new wfePolicy(sqlCon, ctrlPolicy);
                //policyData=(udtPolicy)policy.LoadValuesFromDB();
                //policyPath = GetPolicyPath();
                policyLen = policyLst.SelectedItem.ToString().Length;
                //DataSet ds = policy.GetPolicyDetails();
                //Label lblName = (Label)BoxDtls.Controls["lblName"];
                //if (ds.Tables[0].Rows.Count > 0)
                //    lblName.Text = "Name: " + ds.Tables[0].Rows[0]["name_of_policyholder"].ToString();
            }



        }

        public bool LockPolicy(Credentials pCrd, OdbcTransaction pTrns)
        {
            string sqlStr = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            DataSet ds = new DataSet();
            long time = 0;
            try
            {
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = pTrns;
                sqlStr = "select SYSVALUES from SYSCONFIG  where SYSKEYS='LOCK_EXPIRES_TIME'";
                sqlCmd.CommandText = sqlStr;
                sqlAdap = new OdbcDataAdapter(sqlCmd);
                sqlAdap.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    time = Convert.ToInt32(ds.Tables[0].Rows[0]["SYSVALUES"].ToString());
                }

                sqlStr = @"update metadata_entry set expires_dttm = Now()+interval " + time + " SECOND,Locked_uid ='" + pCrd.created_by + "',invalid=1 where proj_code= " + ctrlPolicy.ProjectKey + " and batch_key=" + ctrlPolicy.BatchKey + " and filename='" + ctrlPolicy.PolicyNumber + "'";
                sqlCmd.CommandText = sqlStr;
                sqlCmd.ExecuteNonQuery();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return commitBol;
        }

        public void RefreshNotify()
        {
            PopulatePolicyList();
            if (lstPolicy.Items.Count > 0)
            {
                lstPolicy.SelectedIndex = 0;
            }
            //PopulateImageList((int)lstPolicy.SelectedItem);
        }

        public int GetFileCount(string projkey, string bundleKey)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select filename from metadata_entry " +
                    " where proj_code=" + projkey +
                " and batch_key=" + bundleKey + " and status = '2'";


            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

            }
            return dsImage.Tables[0].Rows.Count;
        }

        public bool UpdateBundleStatus(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update batch_master" +
                " set status=" + 4 + " where " +
                " batch_key=" + prmBatchKey + " and proj_code = '" + prmProjKey + "' and status = '2' and status<>" + 4;

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();

            }
            return commitBol;
        }

        void UpdateAllPolicyStatus()
        {
            //string policyPath;
            //string photoPath = null;
            wfeImage wImage;
            //string changedImageName;
            System.IO.FileInfo info;
            //long fileSize;
            wfePolicy wPolicy;
            wfeBox box;

            policyLst = lstPolicy;


            if (policyLst.Items.Count > 0)
            {
                for (int i = 0; i < policyLst.Items.Count; i++)
                {
                    pImage = new CtrlImage(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.Items[i].ToString(), string.Empty, string.Empty);
                    wImage = new wfeImage(sqlCon, pImage);
                    pPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.Items[i].ToString());
                    wPolicy = new wfePolicy(sqlCon, pPolicy);
                    if (wImage.GetImageCount(eSTATES.PAGE_SCANNED) == false)
                    {
                        crd.created_dttm = dbcon.GetCurrenctDTTM(1, sqlCon);
                        //policy update status
                        // case_file_master status update
                        UpdateStatus(eSTATES.POLICY_QC, crd, true);
                        // wPolicy.UnLockPolicy();
                        ///update into transaction log
                        wPolicy.UpdateTransactionLog(eSTATES.POLICY_QC, crd);
                    }

                    pBox = new CtrlBox(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), boxNumber);
                    box = new wfeBox(sqlCon, pBox);
                    NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[1];
                    state[0] = NovaNet.wfe.eSTATES.POLICY_SCANNED;

                    if (wPolicy.GetPolicyCount(state) == 0)
                    {
                        //  box.UpdateStatus(eSTATES.BOX_QC);
                    }
                    if (GetFileCount(projKey, bundleKey) == 0)
                    {
                        ///Update the batch status
                        //wBatch.UpdateStatus(eSTATES.BATCH_SCANNED, wBox.ctrlBox.BatchKey);
                        UpdateBundleStatus(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey));
                    }
                }

            }
        }

        public bool UpdateStatus(eSTATES state, Credentials prmCrd, bool pLock)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update metadata_entry" +
                " set Locked_uid = null,expires_dttm=null,invalid=0,status=" + 4 + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_code =" + ctrlPolicy.ProjectKey +
                " and batch_key=" + ctrlPolicy.BatchKey +
                " and filename ='" + ctrlPolicy.PolicyNumber + "' and status<>" + (int)eSTATES.POLICY_EXPORTED;

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return commitBol;
        }

        public bool CheckQCStatus()
        {
            wfeImage wImage;
            bool flag = false;
            DataSet ds = new DataSet();
            System.IO.FileInfo info;
            //wfePolicy wPolicy;
            wfeBox box;
            policyLst = lstPolicy;
            if (policyLst.Items.Count > 0)
            {
                //pImage = new CtrlImage(Convert.ToInt32(wBox.ctrlBox.ProjectCode), Convert.ToInt32(wBox.ctrlBox.BatchKey.ToString()), wBox.ctrlBox.BoxNumber.ToString(), policyLst.Items[i].ToString(), string.Empty, string.Empty);
                string sql = "select * from image_master where proj_key = '" + Convert.ToInt32(projKey) + "' and batch_key = '" + Convert.ToInt32(bundleKey) + "' and box_number ='" + boxNumber + "' and policy_number = '" + policyLst.SelectedItem.ToString() + "' and status = '24'";
                OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
                odap.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                }
                else
                {
                    flag = true;
                }


            }
            return flag;
        }
        public bool InsertNotify(int currIndex)
        {
            //lstImage.Items.Remove(delValue);
            PopulateImageList(lstPolicy.SelectedItem.ToString());
            if (lstImage.Items.Count > 0)
            {
                if (currIndex != lstImage.Items.Count)
                {
                    lstImage.SelectedIndex = currIndex;
                    indexCount = lstImage.SelectedIndex;
                }
                else
                    lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
            }
            else
            {
                if ((lstPolicy.SelectedIndex) != (lstPolicy.Items.Count - 1))
                {
                    lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
                }
            }

            PopulateDelList(lstPolicy.SelectedItem.ToString());
            //			//MoveNext();
            //			lstImage.Refresh();
            //			lstImageDel.Refresh();
            return true;
        }

        public int GetKeyVal(string key)
        {
            int cropKeyVal = 0;

            foreach (char c in key)
            {
                cropKeyVal = (int)c;
            }
            return cropKeyVal;
        }


        public bool MovePrevious()
        {
            if ((indexCount > 0) && (lstImage.Items.Count > 0))
            {
                //ClearSelection();
                indexCount = indexCount - 1;
                lstImage.SelectedIndex = indexCount;
            }
            if ((indexCount == 0) && (lstImage.Items.Count > 0) && (lstPolicy.SelectedIndex != 0))
            {
                //ClearSelection();
                indexCount = 0;
                lstPolicy.SelectedIndex = lstPolicy.SelectedIndex - 1;
            }
            return true;
        }
        public bool MoveUp()
        {
            ArrayList arr = new ArrayList();
            CtrlPolicy ctPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", lstPolicy.SelectedItem.ToString());
            wfePolicy wp = new wfePolicy(sqlCon, ctPolicy);
            //Can only move up when there are records in the list box of images
            //and the currently selected element is greater than 0
            if (lstImage.Items.Count > 0 && lstImage.SelectedIndex > 0)
            {
                indexCount = indexCount - 1;
                object swp = lstImage.Items[lstImage.SelectedIndex];
                lstImage.Items[lstImage.SelectedIndex] = lstImage.Items[lstImage.SelectedIndex - 1]; ;
                lstImage.Items[lstImage.SelectedIndex - 1] = swp;
                lstImage.SelectedIndex = lstImage.SelectedIndex - 1;
                for (int i = 0; i < lstImage.Items.Count; i++)
                {
                    arr.Add(lstImage.Items[i]);
                }
                wp.UpdateSrl(arr);
            }

            return true;
        }
        public bool MoveDown()
        {
            ArrayList arr = new ArrayList();
            CtrlPolicy ctPolicy = new CtrlPolicy(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", lstPolicy.SelectedItem.ToString());
            wfePolicy wp = new wfePolicy(sqlCon, ctPolicy);
            //Can only move up when there are records in the list box of images
            //and the currently selected element is less than count of items-1
            if (lstImage.Items.Count > 0 && lstImage.SelectedIndex < (lstImage.Items.Count - 1))
            {
                string swp = lstImage.SelectedItem.ToString();
                lstImage.Items[lstImage.SelectedIndex] = lstImage.Items[lstImage.SelectedIndex + 1];
                lstImage.Items[lstImage.SelectedIndex + 1] = swp;
                lstImage.SelectedIndex = lstImage.SelectedIndex + 1;
                indexCount = indexCount + 1;
                for (int i = 0; i < lstImage.Items.Count; i++)
                {
                    arr.Add(lstImage.Items[i]);
                }
                wp.UpdateSrl(arr);

            }
            return true;
        }
        public bool MoveNext()
        {
            if (lstImage.Items.Count > 0)
            {
                indexCount = indexCount + 1;
                if ((lstImage.Items.Count - 1) < indexCount)
                {
                    indexCount = 0;
                    if ((lstPolicy.Items.Count - 1) > (lstPolicy.SelectedIndex))
                    {
                        lstPolicy.SelectedIndex = lstPolicy.SelectedIndex;
                    }
                    if (lstImage.Items.Count > 0)
                    {
                        //ClearSelection();
                        lstImage.SelectedIndex = indexCount;
                    }
                    //lstImage.EnsureVisible(indexCount);
                }
                else
                {
                    //					lstImage.Items[indexCount].Selected=true;
                    //					lstImage.EnsureVisible(indexCount);
                    //ClearSelection();
                    lstImage.SelectedIndex = indexCount;
                }
            }
            else
            {
                if ((lstPolicy.Items.Count - 1) > (lstPolicy.SelectedIndex))
                {
                    lstPolicy.SelectedIndex = lstPolicy.SelectedIndex;
                }
            }
            return true;
        }

        private void aeImageQc_KeyUp(object sender, KeyEventArgs e)
        {
            int cropKeyVal = GetKeyVal(cropKey.ToUpper());
            int zoomInKeyVal = GetKeyVal(zoomInKey.ToUpper());
            int zoomOutKeyVal = GetKeyVal(zoomOutKey.ToUpper());
            int autoCropKeyVal = GetKeyVal(autoCropKey.ToUpper());
            int rotateRKeyVal = GetKeyVal(rotateRKey.ToUpper());
            int rotateLKeyVal = GetKeyVal(rotateLKey.ToUpper());
            int skewRKeyVal = GetKeyVal(skewRKey.ToUpper());
            int skewLKeyVal = GetKeyVal(skewLKey.ToUpper());
            int noiseRemovalLKeyVal = GetKeyVal(noiseRemovalLKey.ToUpper());
            int cleanKeyVal = GetKeyVal(cleanKey.ToUpper());

            if ((int)e.KeyData == cropKeyVal)
            {
                OperationInProgress = ihConstants._CROP;
            }
            if ((int)e.KeyData == (zoomInKeyVal + 64))
            {
                ZoomIn();
            }
            if ((int)e.KeyData == (zoomOutKeyVal))
            {
                ZoomOut();
            }

            if ((int)e.KeyData == (autoCropKeyVal))
            {
                AutoCrop();
            }

            if ((int)e.KeyData == (rotateRKeyVal))
            {
                RotateRight();
            }
            if ((int)e.KeyData == (rotateLKeyVal))
            {
                RotateLeft();
            }

            if ((int)e.KeyData == (skewRKeyVal))
            {
                SkewRight();
            }
            if ((int)e.KeyData == (skewLKeyVal))
            {
                SkewLeft();
            }

            if ((int)e.KeyData == (noiseRemovalLKeyVal))
            {
                NoiseRemove();
            }
            if ((int)e.KeyData == (cleanKeyVal))
            {
                OperationInProgress = ihConstants._CLEAN;
            }


            if (e.KeyCode == Keys.Escape)
            {
                OperationInProgress = ihConstants._NO_OPERATION;
                Cursor = Cursors.No;
                Cursor = Cursors.Default;
                pictureControl.Cursor = Cursors.Default;
            }
            if (e.KeyCode == Keys.F11)
            {
                MoveUp();
            }
            if (e.KeyCode == Keys.F12)
            {
                MoveDown();
            }
            if (e.KeyCode == Keys.Right)
            {
                imageLst = imageLst = lstImage;
                policyLst = lstPolicy;
                if (imageLst.Items.Count > 0)
                {
                    UpdateState(eSTATES.PAGE_QC, imageLst.SelectedItem.ToString());
                }
                MoveNext();
                //ChangeSize();
                ShowImage(false);
            }
            if (e.KeyCode == Keys.Left)
            {
                imageLst = imageLst = lstImage;
                policyLst = lstPolicy;
                if (imageLst.Items.Count > 0)
                {
                    UpdateState(eSTATES.PAGE_QC, imageLst.SelectedItem.ToString());
                }
                MovePrevious();
                ShowImage(false);
            }
            if ((e.KeyCode == Keys.Z) && (e.Control == true))
            {
                ImageCopy();
            }
            if (e.KeyCode == Keys.Subtract)
            {
                ZoomOut();
            }
        }

        [Category("Action")]
        [Description("Fires when the key Pressed for indexing")]
        public event LstImageIndexKeyPress LstImageIndex;
        private void lstImage_KeyPress(object sender, KeyPressEventArgs e)
        {
            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            if (LstImageIndex != null)
            {
                LstImageIndex(this, e);
            }
            char DELETE = Convert.ToChar(config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.DELETE_KEY).Remove(1, 1).Trim());
            if (Convert.ToChar(e.KeyChar.ToString().ToUpper()) == DELETE)
            {
                ImageDelete();
            }
            if ((policyLst.SelectedIndex + 1) != (policyLst.Items.Count))
            {
                policyLst.SelectedIndex = policyLst.SelectedIndex + 1;
            }

        }

        public event LstImageClick LstImgClick;
        private void lstImage_Click(object sender, EventArgs e)
        {
            ShowImage(false);
            if (LstImgClick != null)
            {
                LstImgClick(this, e);
            }
        }


        [Category("Action")]
        [Description("Fires when the Image is changed.")]
        public event ImageChangeHandler ImageChanged;
        private void lstImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PopulateDelList((int)lstPolicy.SelectedItem);
            imageLst = imageLst = lstImage;
            DataSet dsVol = new DataSet();
            DateTime st = DateTime.Now;
            indexCount = lstImage.SelectedIndex;
            ShowImage(false);
            if ((ImageChanged != null) && (indexCount >= 0))
            {
                ImageChanged(this, e);
            }
            DateTime end = DateTime.Now;
            TimeSpan duration = end - st;
            //MessageBox.Show(duration.Milliseconds.ToString());
            if ((imageLst.SelectedIndex + 1) == imageLst.Items.Count)
            {
                if (imageLst.Items.Count > 0)
                {
                    UpdateState(eSTATES.PAGE_QC, imageLst.SelectedItem.ToString());
                }
            }
        }

        private void lstImage_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        [Category("Action")]
        [Description("Fires when the Next button is clicked.")]
        public event NextClickedHandler NextClicked;
        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (NextClicked != null)
            {
                NextClicked(this, e);
            }
            imageLst = imageLst = lstImage;
            policyLst = lstPolicy;


            if (imageLst.Items.Count > 0)
            {
                EnableDisbleControls(true);
            }
            delImgList = lstImageDel;
            if ((delImgList.SelectedIndex >= 0) && (delImgList.Items.Count > 0))
            {
                delImgList.SetSelected(delImgList.SelectedIndex, false);
            }

            if (imageLst.Items.Count > 0)
            {
                UpdateState(eSTATES.PAGE_QC, imageLst.SelectedItem.ToString());
            }
            MoveNext();
            ShowImage(false);//added in version 1.0.0.1
            //ChangeSize();//added in version 1.0.0.1
        }

        [Category("Action")]
        [Description("Fires when the Previous button is clicked.")]
        public event PreviousClickedHandler PreviousClicked;
        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            MovePrevious();

            if (PreviousClicked != null)
            {
                PreviousClicked(this, e);
            }

            imageLst = imageLst = lstImage;
            policyLst = lstPolicy;


            if (imageLst.Items.Count > 0)
            {
                EnableDisbleControls(true);
                ShowImage(false);

                delImgList = lstImageDel;
                if ((delImgList.SelectedIndex >= 0) && (delImgList.Items.Count > 0))
                {
                    delImgList.SetSelected(delImgList.SelectedIndex, false);
                }

                if (File.Exists(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString()))
                {
                    File.Delete(qcFolderName + "\\" + "1_" + imageLst.SelectedItem.ToString());
                }
                if (File.Exists(qcFolderName + "\\" + "0_" + imageLst.SelectedItem.ToString()))
                {
                    File.Delete(qcFolderName + "\\" + "0_" + imageLst.SelectedItem.ToString());
                }
            }
        }

        public event LstDelImageClick LstDelImgClick;
        private void lstImageDel_Click(object sender, EventArgs e)
        {
            string delFileName;

            delImgList = lstImageDel;
            if (lstImageDel.Items.Count > 0)
            {
                //statusStrip1.Visible = true;
                toolStripStatusLabel2.Text = "";
                toolStripStatusLabel2.Text = "Press 'Insert' to remove image from deleted list";
            }
            else
            {
                statusStrip1.Visible = false;
                toolStripStatusLabel2.Text = "";
            }
            if (delImgList.Items.Count > 0)
            {
                if (delImgList.SelectedIndex >= 0)
                {
                    EnableDisbleControls(false);
                    delFileName = sourceFilePath + "\\" + ihConstants._DELETE_FOLDER;
                    string[] searchFileName = Directory.GetFiles(delFileName, delImgList.SelectedItem.ToString());
                    //For searching deleted file in deleted folder.
                    if (searchFileName.Length >= 0)
                    {
                        delFileName = searchFileName[0];

                        img.LoadBitmapFromFile(delFileName);
                        //Show the image back in picture box
                        //pictureControl.Image = img.GetBitmap();
                        ChangeSize(delFileName);
                    }
                }
            }
            if (LstDelImgClick != null)
            {
                LstDelImgClick(this, e);
            }
        }

        private void lstImageDel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string delFileName;

            delImgList = lstImageDel;
            if (lstImageDel.Items.Count > 0)
            {
                //statusStrip1.Visible = true;
                toolStripStatusLabel2.Text = "";
                toolStripStatusLabel2.Text = "Press 'Insert' to remove image from deleted list";
            }
            else
            {
                statusStrip1.Visible = false;
                toolStripStatusLabel2.Text = "";
            }
            if (delImgList.Items.Count > 0)
            {
                if (delImgList.SelectedIndex >= 0)
                {
                    EnableDisbleControls(false);
                    delFileName = sourceFilePath + "\\" + ihConstants._DELETE_FOLDER;
                    string[] searchFileName = Directory.GetFiles(delFileName, delImgList.SelectedItem.ToString());
                    //For searching deleted file in deleted folder.
                    if (searchFileName.Length >= 0)
                    {
                        delFileName = searchFileName[0];

                        img.LoadBitmapFromFile(delFileName);
                        //Show the image back in picture box
                        //pictureControl.Image = img.GetBitmap();
                        ChangeSize(delFileName);
                    }
                }
            }
        }

        [Category("Action")]
        [Description("Fires when the image from deleted list inserted.")]
        public event LstDelImageKeyPress LstDelIamgeInsert;
        private void lstImageDel_KeyDown(object sender, KeyEventArgs e)
        {
            string delPath = null;
            string sourceFileName = null;
            string delFileName = null;
            string qcFilePath = null;
            int photoPageCount;

            if (e.KeyCode == Keys.Insert)
            {
                imageDelLst = lstImageDel;
                policyLst = lstPolicy;
                imageLst = lstImage;
                delinsrtBol = true;
                if (imageDelLst.Items.Count > 0)
                {
                    pImage = new CtrlImage(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey), "1", policyLst.SelectedItem.ToString(), imageDelLst.SelectedItem.ToString(), string.Empty);
                    if (UpdateState(eSTATES.PAGE_QC, imageDelLst.SelectedItem.ToString()) == true)
                    {
                        delPath = sourceFilePath + "\\" + ihConstants._DELETE_FOLDER;
                        imageDelLst = lstImageDel;
                        sourceFileName = sourceFilePath + "\\" + imageDelLst.SelectedItem.ToString();
                        delFileName = delPath + "\\" + imageDelLst.SelectedItem.ToString();
                        qcFilePath = qcFolderName + "\\" + imageDelLst.SelectedItem.ToString();
                        photoPageCount = 100; //Convert.ToInt32(imageDelLst.SelectedItem.ToString().Substring(10, 3));
                        if (File.Exists(delFileName) == true)
                        {
                            File.Move(delFileName, sourceFileName);
                            if (ihConstants._PHOTO_PAGE_POSITION == photoPageCount)
                            {
                                img.ConvertTo1Bpp(sourceFileName, qcFilePath);
                            }
                            else
                            {
                                File.Copy(sourceFileName, qcFilePath);
                            }
                        }

                        InsertNotify(imageLst.SelectedIndex);
                        EnableDisbleControls(true);

                        if ((delImgList.SelectedIndex >= 0) && (delImgList.Items.Count > 0))
                        {
                            delImgList.SetSelected(delImgList.SelectedIndex, false);
                        }
                    }
                }
                deLabel2.Text = "Total Scanned Image : " + lstImage.Items.Count.ToString();
            }

            if (LstDelIamgeInsert != null)
            {
                LstDelIamgeInsert(this, e);
            }
        }

        private void aeImageQc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void pictureControl_Click(object sender, EventArgs e)
        {

        }

        private void pictureControl_MouseDown(object sender, MouseEventArgs e)
        {
            if ((OperationInProgress == ihConstants._CROP) || (OperationInProgress == ihConstants._PHOTO_CROP))
            {
                if (e.Button == MouseButtons.Left)
                {

                    cropX = e.X;
                    cropY = e.Y;

                    //MessageBox.Show("X-" + cropX + "Y-" + cropY);
                    cropPen = new Pen(cropPenColor, cropPenSize);
                    cropPen.DashStyle = DashStyle.Solid;
                    Cursor = Cursors.Cross;
                    pictureControl.Refresh();
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }
            if (OperationInProgress == ihConstants._CLEAN)
            {
                if (e.Button == MouseButtons.Left)
                {
                    cropX = e.X;
                    cropY = e.Y;

                    //MessageBox.Show("X-" + cropX + "Y-" + cropY);
                    cropPen = new Pen(cropPenColor, cropPenSize);
                    cropPen.DashStyle = DashStyle.Solid;
                    Cursor = Cursors.Cross;
                    pictureControl.Refresh();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                OperationInProgress = ihConstants._NO_OPERATION;
                Cursor = Cursors.No;
                Cursor = Cursors.Default;
                pictureControl.Cursor = Cursors.Default;
            }
        }

        private void pictureControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ((OperationInProgress == ihConstants._CROP) || (OperationInProgress == ihConstants._PHOTO_CROP))
            {

                if ((pictureControl.Image != null) && (cropPen != null))
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Cursor = Cursors.Cross;
                        pictureControl.Refresh();
                        cropWidth = Math.Abs(e.X - cropX);
                        cropHeight = Math.Abs(e.Y - cropY);
                        pictureControl.CreateGraphics().DrawRectangle(cropPen, ((cropX > e.X) ? e.X : cropX), ((cropY > e.Y) ? e.Y : cropY), cropWidth, cropHeight);
                    }
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }
            if (OperationInProgress == ihConstants._CLEAN)
            {
                if ((pictureControl.Image != null))
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Cursor = Cursors.Cross;
                        pictureControl.Refresh();
                        cropWidth = Math.Abs(e.X - cropX);
                        cropHeight = Math.Abs(e.Y - cropY);
                        pictureControl.CreateGraphics().DrawRectangle(cropPen, ((cropX > e.X) ? e.X : cropX), ((cropY > e.Y) ? e.Y : cropY), cropWidth, cropHeight);
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                Cursor = Cursors.Default;
                OperationInProgress = ihConstants._NO_OPERATION;
            }
        }

        int Crop(Rectangle udtRect)
        {
            double htRatio = 0;
            double wdRatio = 0;
            long fileSize;
            Bitmap bmpImage = null;
            try
            {
                Rectangle rect = udtRect;
                if (img.IsValid() == true)
                {
                    bmpImage = img.GetBitmap();

                    htRatio = Convert.ToDouble(bmpImage.Size.Height) / Convert.ToDouble(pictureControl.Height);
                    rect.Height = Convert.ToInt32(Convert.ToDouble(rect.Height) * htRatio);
                    wdRatio = (Convert.ToDouble(bmpImage.Size.Width) / Convert.ToDouble(pictureControl.Width));
                    rect.Width = Convert.ToInt32(Convert.ToDouble(rect.Width) * wdRatio);

                    rect.X = Convert.ToInt32(rect.X * wdRatio);
                    rect.Y = Convert.ToInt32(rect.Y * htRatio);

                    img.Crop(rect);
                    img.SaveFile(imgFileName);
                    img.LoadBitmapFromFile(imgFileName);
                    //pictureControl.Image = img.GetBitmap();
                    //bool ret = ToBitmap.scanToImage.gdSaveFile(imgQc,ino,imgFileName);
                    //pictureControl.Image = imgQc.GetBitmapFromGdPictureImage(ino);
                }
                //Change the size of the image in relation to the canvas
                ChangeSize();

                System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                fileSize = info.Length;
                fileSize = fileSize / 1024;
                lblImageSize = lblImgSize;

                lblImageSize.Text = fileSize.ToString() + " KB";
                UpdateImageSize(fileSize);
                cmdNext.Focus();
                //bmpCrop.Dispose();
                delinsrtBol = false;
            }
            catch (Exception ex)
            {
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Batch-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
                MessageBox.Show("Error while cropping the image" + ex.Message, "Crop error");
            }
            return 0;
        }
        int Clean(Rectangle rect)
        {
            //Rectangle rect  = new Rectangle(e.X,e.Y, 6,6);

            double htRatio = 0;
            double wdRatio = 0;
            long fileSize;
            try
            {
                Bitmap bmpImage = img.GetBitmap();
                htRatio = Convert.ToDouble(bmpImage.Size.Height) / Convert.ToDouble(pictureControl.Height);
                rect.Height = Convert.ToInt32(Convert.ToDouble(rect.Height) * htRatio);
                wdRatio = (Convert.ToDouble(bmpImage.Size.Width) / Convert.ToDouble(pictureControl.Width));
                rect.Width = Convert.ToInt32(Convert.ToDouble(rect.Width) * wdRatio);

                rect.X = Convert.ToInt32(rect.X * wdRatio);
                rect.Y = Convert.ToInt32(rect.Y * htRatio);

                img.Clean(rect);
                img.SaveFile(imgFileName);
                img.LoadBitmapFromFile(imgFileName);
                //pictureControl.Image = img.GetBitmap();
                //Change the size of the image in relation to the canvas
                ChangeSize();

                System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                fileSize = info.Length;
                fileSize = fileSize / 1024;
                lblImageSize = lblImgSize;
                lblImageSize.Text = fileSize.ToString() + " KB";
                UpdateImageSize(fileSize);
                //bmpCrop.Dispose();
                delinsrtBol = false;
                cmdNext.Focus();
            }
            catch (Exception ex)
            {
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Batch-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
                MessageBox.Show("Error while cleaning the image" + ex.Message, "Crop error");
                return 0;
            }
            return 1;
        }

        int CropPhoto(Rectangle udtRect)
        {
            Bitmap bmpImage;
            double htRatio = 0;
            double wdRatio = 0;
            imageLst = lstImage;
            long fileSize;

            foreach (ImageCodecInfo ice in ImageCodecInfo.GetImageEncoders())
            {
                if (ice.MimeType == "image/tiff")
                {
                    //info = ice;
                    break;
                }
            }
            try
            {
                if (OperationInProgress == ihConstants._PHOTO_CROP)
                {
                    Rectangle rect = udtRect;
                    bmpImage = img.GetBitmap();
                    //Calculate the ratio for Picturebox to actual image
                    htRatio = Convert.ToDouble(bmpImage.Size.Height) / Convert.ToDouble(pictureControl.Height);
                    rect.Height = Convert.ToInt32(Convert.ToDouble(rect.Height) * htRatio);
                    wdRatio = (Convert.ToDouble(bmpImage.Size.Width) / Convert.ToDouble(pictureControl.Width));
                    rect.Width = Convert.ToInt32(Convert.ToDouble(rect.Width) * wdRatio);
                    rect.X = Convert.ToInt32(rect.X * wdRatio);
                    rect.Y = Convert.ToInt32(rect.Y * htRatio);

                    //Crop
                    img.Crop(rect);

                    //Set the new image file name
                    imgFileName = qcFolderName + "\\" + imageLst.SelectedItem.ToString().Substring(0, policyLen) + "_000_A.TIF";

                    //Call the save routine
                    img.SaveFile(imgFileName);
                    img.LoadBitmapFromFile(imgFileName);
                    pictureControl.Image = img.GetBitmap();
                    //Show the image back in picture box
                    //					pictureControl.Width = panel1.Width - 2;
                    //	                pictureControl.Height = panel1.Height - 2;
                    //	                if (!System.IO.File.Exists(imgFileName)) return 0;
                    //	                Image newImage = Image.FromFile(imgFileName);
                    //	                
                    //	                pictureControl.Image = newImage.GetThumbnailImage(pictureControl.Width, pictureControl.Height, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
                    //	                newImage.Dispose();

                    //Change the size of the image in relation to the canvas
                    ChangeSize();
                    //panel1.Left = BoxDtls.Right + 4;
                    //panel1.Width = (dockPanel.Width - BoxDtls.Right) - 6;
                    //panel1.Top = panel2.Top;
                    // panel1.Height = this.ClientSize.Height - 20;
                    //Calculate and show file info
                    System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);
                    fileSize = info.Length;
                    fileSize = fileSize / 1024;
                    lblImageSize = lblImgSize;
                    lblImageSize.Text = fileSize.ToString() + " KB";
                    //Update file info in db
                    UpdateImageSize(fileSize);
                    delinsrtBol = false;
                    cmdNext.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while cropping the photo", "Crop Error");
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Project-" + projKey + " ,Batch-" + bundleKey + " ,Box-" + boxNumber + "Image name-" + imgFileName + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            photoCropOperation = true;
            //ChangeSize();
            //imgQc.Write(imgFileName);
            //pictureControl.Image = MagickNet.Image.ToBitmap(imgQc);
            return 0;
        }

        private void pictureControl_MouseUp(object sender, MouseEventArgs e)
        {
            cropWidth = Math.Abs(e.X - cropX);
            cropHeight = Math.Abs(e.Y - cropY);


            Cursor = Cursors.Default;

            if ((OperationInProgress == ihConstants._CROP) || (OperationInProgress == ihConstants._PHOTO_CROP) || (OperationInProgress == ihConstants._CLEAN))
            {
                //Create the rectangle on which to operate
                if ((cropWidth > 1) && (e.Button == MouseButtons.Left))
                {
                    //Works both ways
                    Rectangle rect = new Rectangle(((cropX > e.X) ? e.X : cropX), ((cropY > e.Y) ? e.Y : cropY), cropWidth, cropHeight);
                    if (OperationInProgress == ihConstants._CROP)
                    {
                        Crop(rect);
                    }
                    if (OperationInProgress == ihConstants._PHOTO_CROP)
                    {
                        CropPhoto(rect);
                    }
                    if (OperationInProgress == ihConstants._CLEAN)
                    {
                        Clean(rect);
                    }
                }
                else
                    Cursor = Cursors.Default;
            }

            if (e.Button == MouseButtons.Right)
            {
                Cursor = Cursors.Default;
                OperationInProgress = ihConstants._NO_OPERATION;
            }
            pictureControl.Refresh();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSearch_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSearch.Text.ToUpper().Trim()))
                {
                    for (int i = 0; i < lstPolicy.Items.Count; i++)
                    {
                        if (lstPolicy.Items[i].ToString().Contains(txtSearch.Text.ToUpper().Trim()))
                        {
                            //lstPolicy.SelectedIndex = i;
                            lstPolicy.SetSelected(i, true);
                        }
                    }
                }
            }
        }

        private void aeImageQc_Resize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void aeImageQc_AutoSizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void prmGetPhoto_Click(object sender, EventArgs e)
        {
            GetPhoto();
        }

        private void prmEndPhotoCrop_Click(object sender, EventArgs e)
        {
            EndPhotoCrop();
        }

        private void prmPhotoCrop_Click(object sender, EventArgs e)
        {
            PhotoCropRegister();
        }

        private void prmButtonDelImage_Click(object sender, EventArgs e)
        {
            ImageDelete();
        }

        private void prmButtonCopyImage_Click(object sender, EventArgs e)
        {
            ImageCopy();
        }

        private void prmButtonCleanImg_Click(object sender, EventArgs e)
        {
            CleanImageRegister();
        }

        private void prmButtonNoiseRemove_Click(object sender, EventArgs e)
        {
            NoiseRemove();
        }

        private void prmButtonSkewRight_Click(object sender, EventArgs e)
        {
            SkewRight();
        }

        private void prmButtonZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void prmButtonZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void prmButtonRotateLeft_Click(object sender, EventArgs e)
        {
            RotateLeft();
        }

        private void prmButtonRotateRight_Click(object sender, EventArgs e)
        {
            RotateRight();
        }

        private void prmButtonAutoCrp_Click(object sender, EventArgs e)
        {
            AutoCrop();
        }

        private void prmButtonCrop_Click(object sender, EventArgs e)
        {
            CropRegister();
        }

        private void aeImageQc_KeyDown(object sender, KeyEventArgs e)
        {
            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            int cropKeyVal = GetKeyVal(cropKey.ToUpper());
            int zoomInKeyVal = GetKeyVal(zoomInKey.ToUpper());
            int zoomOutKeyVal = GetKeyVal(zoomOutKey.ToUpper());
            int autoCropKeyVal = GetKeyVal(autoCropKey.ToUpper());
            int rotateRKeyVal = GetKeyVal(rotateRKey.ToUpper());
            int rotateLKeyVal = GetKeyVal(rotateLKey.ToUpper());
            int skewRKeyVal = GetKeyVal(skewRKey.ToUpper());
            int skewLKeyVal = GetKeyVal(skewLKey.ToUpper());
            int noiseRemovalLKeyVal = GetKeyVal(noiseRemovalLKey.ToUpper());
            int cleanKeyVal = GetKeyVal(cleanKey.ToUpper());
            if (e.KeyCode == Keys.Escape)
            {
                //dg.Close();
            }
            //if (e.KeyCode == Keys.F4)
            //{
            //    if (deButton1.Enabled == true)
            //    {
            //        deButton1_Click(sender, e);
            //    }

            //}
            if (e.KeyCode == Keys.F1)
            {
                frmHelp frm = new frmHelp("QC");
                frm.ShowDialog(this);
            }
            if (e.KeyCode == Keys.F5)
            {
                if (CheckQCStatus() == true)
                {
                    UpdateAllPolicyStatus();
                    //wfePolicy wPolicy = new wfePolicy(sqlCon);
                    //int count = wPolicy.GetTransactionLogCount(wBox.ctrlBox.BatchKey.ToString(), dbcon.GetCurrenctDTTM(2, sqlCon), crd.created_by, eSTATES.POLICY_QC);
                    //this.Text = "Image Quality Control";
                    //this.Text = this.Text + "                                       Today you have done " + count + " ";
                    RefreshNotify();

                    if (policyLst.Items.Count > 0)
                    {
                        //ShowImage(false);
                        // GetIndexDetails(policyLst.SelectedItem.ToString());
                    }
                    else
                    {
                        imageLst.Items.Clear();
                        if (policyLst.Items.Count == 0)
                        {
                            EnableDisbleControls(false);
                            prmEndPhotoCrop.Enabled = false;
                            prmGetPhoto.Enabled = false;
                            prmPhotoCrop.Enabled = false;
                            pictureControl.Image = null;
                            deLabel2.Visible = false;
                            //dg.Close();
                            MessageBox.Show(this, "QC Completed for this Batch ....", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Please visit all images", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                deLabel2.Text = "Total Scanned Image : " + lstImage.Items.Count.ToString();
            }
            ///For checking todays production count
            if ((e.KeyCode == Keys.F9))
            {
                wfePolicy wPolicy = new wfePolicy(sqlCon);
                int count = wPolicy.GetTransactionLogCount(bundleKey, dbcon.GetCurrenctDTTM(2, sqlCon), crd.created_by, eSTATES.POLICY_QC);
                frmProductionCount frmProd = new frmProductionCount(count);
                frmProd.ShowDialog(this);
            }
            if (e.KeyCode == Keys.Space)
            {
                if ((this.ActiveControl == null) || (this.ActiveControl.Name != "cmdNext" && this.ActiveControl.Name != "cmdPrevious"))
                {
                    object o = new object();
                    EventArgs a = new KeyEventArgs(Keys.None);
                    cmdNext_Click(o, a);
                }
            }

            char DELETE = Convert.ToChar(config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.DELETE_KEY).Remove(1, 1).Trim());
            char rslt;
            if (char.TryParse(e.KeyCode.ToString(), out rslt))
            {
                if (Convert.ToChar(e.KeyCode.ToString().ToUpper()) == DELETE)
                {
                    ImageDelete();
                    return;
                }
            }
        }
    }
}
