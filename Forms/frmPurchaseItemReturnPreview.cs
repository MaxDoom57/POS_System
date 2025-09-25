using CrystalDecisions.CrystalReports.Engine;
using POS_System.Classes;
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

namespace POS_System.Forms
{
    public partial class frmPurchaseItemReturnPreview : Form
    {
        public frmPurchaseItemReturnPreview()
        {
            InitializeComponent();
        }

        public void ShowGreReport(int greNo)
        {
            string query = "SELECT * FROM PurRtnRptQry WHERE TrnNo = @tranNo  AND OurCd = 'PURRTN'";
            DataTable dt = new DataTable();
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tranNo", greNo);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            ReportDocument reportGrn = new ReportDocument();
            reportGrn.Load(@"E:\POS System\Reports\rptGre.rpt");

            reportGrn.SetDataSource(dt);
            reportGrn.SetParameterValue("reportTitle", "PURCHASE RETURN NOTE");
            reportGrn.SetParameterValue("currentUserId", UserSession.Instance.UserId);

            frmReportViewer frmReportViewer = new frmReportViewer();
            frmReportViewer.LoadGrnReport(reportGrn);
            frmReportViewer.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.Focus();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtGrnNo.Text == "" || string.IsNullOrEmpty(txtGrnNo.Text))
            {
                MessageBox.Show("GRE No is must!");
            }
            else if (txtGrnNo.Text != "0")
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select TrnNo from PurRtnRptQry Where TrnNo = @trnNo And CKy = @companyKey And OurCd ='PURRTN' ", conn);
                    cmd.Parameters.AddWithValue("@trnNo", txtGrnNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ShowGreReport(Convert.ToInt32(txtGrnNo.Text));
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid GRE No!");
                        }
                    }
                }

            }
        }
    }
}
