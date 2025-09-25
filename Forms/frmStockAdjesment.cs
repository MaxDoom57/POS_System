using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using POS_System.Classes;

namespace POS_System.Forms
{
    public partial class frmStockAdjesment : Form
    {
        private int childFormNumber = 0;

        public frmStockAdjesment()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }


        public void ShowReport(int trnKey)
        {
            string query = "SELECT * FROM vewStkAddDedRpt WHERE TrnKy = @trnKey";

            DataTable dt = new DataTable();
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@trnKey", trnKey);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            ReportDocument reportStockAddition = new ReportDocument();
            reportStockAddition.Load(@"E:\POS System\Reports\rptStockAddition.rpt");

            reportStockAddition.SetDataSource(dt);
            reportStockAddition.SetParameterValue("CNm", UserSession.Instance.CompanyName);   //UserSession.Instance.CompanyName
            reportStockAddition.SetParameterValue("userNm", UserSession.Instance.UserId);   //UserSession.Instance.UserId
            reportStockAddition.SetParameterValue("caption", "Stock Addition");

            frmReportViewer frmReportViewer = new frmReportViewer();
            frmReportViewer.LoadStockAdditionReport(reportStockAddition);
            frmReportViewer.Show();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int trnKey = -1;
            if (txtTrnNo.Text == "" || string.IsNullOrEmpty(txtTrnNo.Text))
            {
                MessageBox.Show("Transfer Note No. is must!");
            }
            else if(txtTrnNo.Text != "0")
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd  = new SqlCommand("Select TrnKy from vewTrnNo Where TrnNo = @trnNo And CKy = @companyKey And OurCd ='STKADD' ", conn);
                    cmd.Parameters.AddWithValue("@trnNo", txtTrnNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            trnKey = Convert.ToInt32(reader["TrnKy"]);
                        }
                    }
                }

                ShowReport(trnKey);
                this.Close();
            }
        }

        private void txtTrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnOk.Focus();
            }
        }
    }
}
