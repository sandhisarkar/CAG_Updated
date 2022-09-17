using System;
using System.Drawing;
using System.Windows.Forms;
using LItems;
using NovaNet.Utils;
using NovaNet.wfe;
using System.Data.Odbc;
using System.Data;

namespace ImageHeaven
{
    public partial class aeCustomExp : Form
    {
        private wfeImage wImage = null;
        private wfeProject pProject = null;
        private wfeBatch pBatch = null;
        private OdbcConnection sqlCon;
        private Credentials crd = new Credentials();
        OdbcDataAdapter sqlAdap;

        public aeCustomExp()
        {
            InitializeComponent();
        }

        public aeCustomExp(wfeImage prmImage, OdbcConnection prmCon, Credentials prmCrd)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            wImage = prmImage;
            sqlCon = prmCon;
            crd = prmCrd;
            InitializeComponent();
            this.Text = "Custom exception details";
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void aeCustomExp_Load(object sender, EventArgs e)
        {
            pProject = new wfeProject(sqlCon);
            pBatch = new wfeBatch(sqlCon);
            lblProject.Text = pProject.GetProjectName(wImage.ctrlImage.ProjectKey);
            lblBatch.Text = pBatch.GetBundleName(wImage.ctrlImage.ProjectKey, wImage.ctrlImage.BatchKey);
            //lblBox.Text = wImage.ctrlImage.BoxNumber.ToString();
            lblPolicy.Text = wImage.ctrlImage.PolicyNumber.ToString();
            lblImageName.Text = wImage.ctrlImage.ImageName.ToString();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool AddCustomException(int prmStatus, string prmProblemType, string prmRemarks, Credentials prmCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"insert into custom_exception(Proj_key,batch_key,box_number,policy_number,problem_type,Image_name,Remarks,status,created_by,created_dttm)" +
                " values(" + wImage.ctrlImage.ProjectKey + " ," + wImage.ctrlImage.BatchKey + ", '" + wImage.ctrlImage.BoxNumber + "','" + wImage.ctrlImage.PolicyNumber + "','" + prmProblemType + "','" + wImage.ctrlImage.ImageName + "','" + prmRemarks + "'," + prmStatus + ",'" + prmCrd.created_by + "','" + prmCrd.created_dttm + "' )";
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
                //commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                MessageBox.Show(ex.ToString());
                //stateLog = new MemoryStream();
                //tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                //stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                //exMailLog.Log(ex);
            }
            return commitBol;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string prblem = string.Empty;
            if (rdoDarkImage.Checked == true)
            {
                prblem = rdoDarkImage.Text;
            }
            if (rdoDifferentPolicyNo.Checked == true)
            {
                prblem = rdoDifferentPolicyNo.Text;
            }
            if (rdoDocumentMixed.Checked == true)
            {
                prblem = rdoDocumentMixed.Text;
            }
            if (rdoImagesMissing.Checked == true)
            {
                prblem = rdoImagesMissing.Text;
            }
            if (rdoOther.Checked == true)
            {
                prblem = rdoOther.Text;
            }
            if (rdoPhotoScan.Checked == true)
            {
                prblem = rdoPhotoScan.Text;
            }
            if (rdoPolicyNumberMismatch.Checked == true)
            {
                prblem = rdoPolicyNumberMismatch.Text;
            }
            if (rdoSkewedImage.Checked == true)
            {
                prblem = rdoSkewedImage.Text;
            }
            if (AddCustomException(ihConstants._NOT_RESOLVED, prblem, txtRemarks.Text, crd) == true)
            {
                MessageBox.Show("Exception added successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error while adding exception", "Error");
            }
        }
    }
}
