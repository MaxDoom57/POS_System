using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using POS_System.Classes;
using POS_System.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace POS_System.Forms
{
    public partial class frmPurchaseReturn : Form
    {
        private int childFormNumber = 0;

        public frmPurchaseReturn()
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

        //Handle key navigation ------------------------------------
        private void cmbReturnNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpReturnDate.Focus();
            }
        }

        private void dtpReturnDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbYourReference.Focus();
            }
        }

        private void cmbYourReference_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmbLocation.Focus();
            }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.KeyCode == Keys.Enter)
            {
                cmbAccountCode.Focus();
            }
        }

        private void cmbAccountCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                cmbAccountName.Focus();
            }
        }

        private void cmbAccountName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmbAddress.Focus();
            }
        }

        private void cmbAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtDocumentNo.Focus();
            }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmbPurchaseAccount.Focus();
            }
        }

        private void cmbPurchaseAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmbPaymentTerm.Focus();
            }
        }

        private void cmbPaymentTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvGre.Focus();
            }
        }



        //Define all functions here -----------------------------------------

        DataTable accountDetails;
        DataTable itemNamesTable;
        int formMode;
        int greNo;
        int tranKey;
        bool formLoaded;
        bool inTransaction;


        private void LoadReturnNumbers()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    cmbReturnNo.Items.Clear();
                    string query = "SELECT TrnNo,TrnKy From vewTrnNo where OurCd = 'PurRtn' And Cky = @companyKey Order By TrnNo";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbReturnNo.Items.Add(reader["TrnNo"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadReferences()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    cmbYourReference.Items.Clear();
                    string query = "SELECT YurRef,TrnKy From vewTrnNo where OurCd='PurRtn' And Cky = @companyKey Order by YurRef";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbYourReference.Items.Add(reader["YurRef"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadLocations()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    cmbLocation.Items.Clear();
                    string query = "SELECT LocKy,LocCd From vewLocCd Order By LocCd";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbLocation.Items.Add(reader["LocCd"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadAccountDetails()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    accountDetails = new DataTable();

                    string query = "SELECT SupAccKy, SupAccCd, SupAccNm from vewSupAccCd Order By SupAccCd";
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.Fill(accountDetails);
                    }
                }

                cmbAccountCode.DataSource = accountDetails.Copy();
                cmbAccountCode.DisplayMember = "SupAccCd";
                cmbAccountCode.ValueMember = "SupAccKy";

                cmbAccountName.DataSource = accountDetails;
                cmbAccountName.DisplayMember = "SupAccNm";
                cmbAccountName.ValueMember = "SupAccKy";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPurchaseAccounts()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    cmbPurchaseAccount.Items.Clear();
                    string query = "SELECT AccKy,AccNm From vewAccAccCd where AccTyp = 'PUR' Order By AccNm";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbPurchaseAccount.Items.Add(reader["AccNm"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPaymentTerm()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    cmbPaymentTerm.Items.Clear();
                    string query = "SELECT PmtTrmKy,PmtTrmCd From vewPmtTrmCd Order By PmtTrmCd";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbPaymentTerm.Items.Add(reader["PmtTrmCd"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadItemNameComboBox()
        {
            itemNamesTable = new DataTable();
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    string query = "Select Itmky,ItmCd,ItmNm from vewItmNm Order By ItmNm";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(itemNamesTable);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setupDgvGre()
        {
            dgvGre.Columns.Clear();

            DataGridViewTextBoxColumn colItemCode = new DataGridViewTextBoxColumn();
            colItemCode.Name = "itemCode";
            colItemCode.HeaderText = "Item Code";
            dgvGre.Columns.Add(colItemCode);

            DataGridViewComboBoxColumn colItemName = new DataGridViewComboBoxColumn();
            colItemName.Name = "itemName";
            colItemName.HeaderText = "Item Name";
            colItemName.DataSource = itemNamesTable;
            colItemName.DisplayMember = "ItmNm";
            colItemName.ValueMember = "Itmky";
            dgvGre.Columns.Add(colItemName);

            DataGridViewTextBoxColumn colUnit = new DataGridViewTextBoxColumn();
            colUnit.Name = "unit";
            colUnit.HeaderText = "Unit";
            dgvGre.Columns.Add(colUnit);

            DataGridViewTextBoxColumn colRate = new DataGridViewTextBoxColumn();
            colRate.Name = "rate";
            colRate.HeaderText = "Rate";
            dgvGre.Columns.Add(colRate);

            DataGridViewTextBoxColumn colQty = new DataGridViewTextBoxColumn();
            colQty.Name = "quantity";
            colQty.HeaderText = "Quantity";
            dgvGre.Columns.Add(colQty);

            DataGridViewTextBoxColumn colTotal = new DataGridViewTextBoxColumn();
            colTotal.Name = "total";
            colTotal.HeaderText = "Total";
            dgvGre.Columns.Add(colTotal);

            //DataGridViewTextBoxColumn colNsl = new DataGridViewTextBoxColumn();
            //colNsl.Name = "nsl";
            //colNsl.HeaderText = "NSL";
            //dgvGre.Columns.Add(colNsl);

            //DataGridViewTextBoxColumn colNslAmount = new DataGridViewTextBoxColumn();
            //colNslAmount.Name = "nslAmount";
            //colNslAmount.HeaderText = "NSL Amount";
            //dgvGre.Columns.Add(colNslAmount);

            DataGridViewTextBoxColumn colItemTranKey = new DataGridViewTextBoxColumn();
            colItemTranKey.Name = "itemTranKey";
            colItemTranKey.HeaderText = "Item Tran Key";
            dgvGre.Columns.Add(colItemTranKey);

            DataGridViewCheckBoxColumn colUpdate = new DataGridViewCheckBoxColumn();
            colUpdate.Name = "update";
            colUpdate.HeaderText = "Update";
            dgvGre.Columns.Add(colUpdate);

            DataGridViewCheckBoxColumn colDelete = new DataGridViewCheckBoxColumn();
            colDelete.Name = "delete";
            colDelete.HeaderText = "Delete";
            dgvGre.Columns.Add(colDelete);

            //Alignment
            dgvGre.Columns["itemCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvGre.Columns["unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGre.Columns["rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvGre.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGre.Columns["total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //Format
            dgvGre.Columns["rate"].DefaultCellStyle.Format = "N2";
            dgvGre.Columns["quantity"].DefaultCellStyle.Format = "N1";
            dgvGre.Columns["total"].DefaultCellStyle.Format = "N2";

            //Value Type
            dgvGre.Columns["rate"].ValueType = typeof(decimal);
            dgvGre.Columns["quantity"].ValueType = typeof(decimal);
            dgvGre.Columns["total"].ValueType = typeof(decimal);

            //Column Width
            dgvGre.Columns["itemCode"].Width = 100;

            //Bold Column Headers
            dgvGre.ColumnHeadersDefaultCellStyle.Font = new Font(dgvGre.Font, FontStyle.Bold);
            dgvGre.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void FormClear()
        {
            cmbReturnNo.Text = "";
            cmbYourReference.Text = "";
            cmbLocation.Text = "";
            cmbAccountCode.Text = "";
            cmbAccountName.Text = "";
            cmbAddress.Text = "";
            cmbPurchaseAccount.Text = "";
            cmbPaymentTerm.Text = "";
            txtDocumentNo.Text = "";
            txtTotalAmount.Text = "";
            dtpReturnDate.Value = DateTime.Now;
            dgvGre.Rows.Clear();
        }

        private void ProcNewMode()
        {
            if (UserSession.Instance.GetPermission("mnuPurRtn").CanCreateNew)
            {
                formMode = (int)FormMode.NEW;

                btnDelete.Enabled = false;
                btnSave.Enabled = true;

                FormClear();
                greNo = DatabaseFunctions.GetTrnNoList(UserSession.Instance.CompanyKey, 0, dtpReturnDate.Value, "PURRTN") + 1;
                cmbReturnNo.Text = greNo.ToString();
                setupDgvGre();
            }
            else
            {
                MessageBox.Show("Access is denied");
            }
        }

        private void ProcUpdate()
        {
            if (UserSession.Instance.GetPermission("mnuPurRtn").CanCreateNew)
            {
                formMode = (int)FormMode.UPDATE;

                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("Access is denied");
            }
        }

        private string GetPaymentTermNm(string paymentTermKey)
        {
            string paymentTermNm = "";

            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                string query = "SELECT PmtTrmCd From vewPmtTrmCd Where PmtTrmKy = @paymentTermKy";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@paymentTermKy", paymentTermKey);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        paymentTermNm = (reader["PmtTrmCd"].ToString());
                    }
                }
            }
            return paymentTermNm;
        }

        private string GetAddressNm(string addressKey)
        {
            string addressNm = "";

            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                string query = "Select AdrNm from vewAdrNmPAccKy Where AdrKy = @addressKey And CKy = @companyKey";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        addressNm = (reader["AdrNm"].ToString());
                    }
                }
            }
            return addressNm;
        }

        private string GetPurchaseAccNm(string purchaseAccKey)
        {
            string purchaseAccNm = "";

            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                string query = "SELECT AccNm From vewAccAccCd where AccTyp = 'PUR' AND AccKy = @purchaseAccKey";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@purchaseAccKey", purchaseAccKey);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        purchaseAccNm = (reader["AccNm"].ToString());
                    }
                }
            }
            return purchaseAccNm;
        }

        private void GetData()
        {
            try
            {
                if (formMode == (int)FormMode.UPDATE && cmbReturnNo.Text != "")
                {
                    //Get tranKey
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "Select TrnKy from vewTrnNo Where TrnNo = @greNo And OurCd = 'PURRTN' And CKy = @companyKey";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@greNo", cmbReturnNo.Text);
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                tranKey = Convert.ToInt32(reader["TrnKy"]);
                            }
                            else
                            {
                                MessageBox.Show("No Transaction found for this number!");
                                cmbReturnNo.Text = "";
                                cmbReturnNo.Focus();
                                return;
                            }
                        }
                    }

                    //Load existing transaction details
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "SELECT PurAccKy, TrnDt, Code, YurRef, DocNo, AdrKy, AccKy, PmtTrmKy " +
                                        "FROM vewPURRTNHdr WHERE TrnKy = @tranKey";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@tranKey", tranKey);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dtpReturnDate.Value = Convert.ToDateTime(reader["TrnDt"]);
                                cmbYourReference.Text = reader["YurRef"] != DBNull.Value ? reader["YurRef"].ToString() : "";
                                cmbLocation.Text = reader["Code"].ToString();
                                cmbAccountCode.SelectedValue = reader["AccKy"];
                                cmbAddress.Text = reader["AdrKy"] != DBNull.Value ? GetAddressNm(reader["AdrKy"].ToString()) : "";
                                txtDocumentNo.Text = reader["DocNo"] != DBNull.Value ? reader["DocNo"].ToString() : "";
                                cmbPurchaseAccount.Text = GetPurchaseAccNm(reader["PurAccKy"].ToString());
                                cmbPaymentTerm.Text = GetPaymentTermNm(reader["PmtTrmKy"].ToString());
                            }
                        }
                    }

                    //Fill datagridview from data
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        dgvGre.Rows.Clear();
                        string query = "SELECT ItmKy, ItmCd, ItmNm, Unit, CosPri, TrnPri, SlsPri, -1 * Qty as Qty, -1 * Qty*TrnPri As total, 0 As Expr1, 0 As Expr2, Amt1, Amt2, ItmTrnKy, 0 as Updt,0 as Del " +
                            "From vewPURRTNDtls WHERE TrnKy = @tranKey";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@tranKey", tranKey);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //int itemKey = reader["ItmKy"] != DBNull.Value ? Convert.ToInt32(reader["ItmKy"]) : -1;
                                string itemCode = reader["ItmCd"] != DBNull.Value ? reader["ItmCd"].ToString() : "";

                                string itemName = reader["ItmNm"] == DBNull.Value ? null : reader["ItmNm"].ToString();
                                object itemNameKey = DBNull.Value;

                                if (!string.IsNullOrEmpty(itemName))
                                {
                                    DataRow[] matchingItemRow = itemNamesTable.Select($"ItmNm = '{itemName.Replace("'", "''")}'");
                                    if (matchingItemRow.Length > 0)
                                    {
                                        itemNameKey = matchingItemRow[0]["ItmKy"];
                                    }
                                }

                                string unit = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "";
                                decimal costPrice = reader["CosPri"] != DBNull.Value ? Convert.ToDecimal(reader["CosPri"]) : 0;
                                decimal quantity = reader["Qty"] != DBNull.Value ? Convert.ToDecimal(reader["Qty"]) : 0;
                                decimal total = reader["total"] != DBNull.Value ? Convert.ToDecimal(reader["total"]) : 0;
                                string itemTranKey = reader["ItmTrnKy"] != DBNull.Value ? reader["ItmTrnKy"].ToString() : "";
                                int update = Convert.ToInt32(reader["Updt"]);
                                int delete = Convert.ToInt32(reader["Del"]);

                                dgvGre.Rows.Add(
                                    itemCode,
                                    itemNameKey,
                                    unit,
                                    costPrice,
                                    quantity,
                                    total,
                                    itemTranKey,
                                    update,
                                    delete
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetInvoiceTotal()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dgvGre.Rows)
            {
                decimal amount = 0;

                if (row.IsNewRow) continue;

                if (row.Cells["total"].Value != DBNull.Value)
                {
                    amount = Convert.ToDecimal(row.Cells["total"].Value);
                    totalAmount += amount;
                }
            }

            txtTotalAmount.Text = totalAmount.ToString("#,0.00");
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


        //Handle all events here --------------------------------------------
        private void frmPurchaseReturn_Load(object sender, EventArgs e)
        {
            LoadReturnNumbers();
            LoadReferences();
            LoadLocations();
            LoadAccountDetails();
            LoadPurchaseAccounts();
            LoadPaymentTerm();
            LoadItemNameComboBox();

            setupDgvGre();

            cmbAccountCode.SelectedIndex = -1;
            cmbAccountName.SelectedIndex = -1;

            btnDelete.Enabled = false;
            btnSave.Enabled = false;

            formLoaded = false;
        }

        private void frmPurchaseReturn_Activated(object sender, EventArgs e)
        {
            if (!formLoaded)
            {
                if (UserSession.Instance.GetPermission("mnuPurRtn").CanCreateNew)
                {
                    formMode = (int)FormMode.NEW;
                    ProcNewMode();
                }
                else if (UserSession.Instance.GetPermission("mnuPurRtn").CanUpdate)
                {
                    formMode = (int)FormMode.UPDATE;
                    ProcUpdate();
                }
                else
                {
                    formMode = (int)FormMode.VIEW;
                    //enable previewbtn click
                }
                formLoaded = true;
            }
        }

        private void cmbAccountCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccountCode.SelectedItem is DataRowView selectedRow)
            {
                object key = selectedRow["SupAccKy"];

                if (key != DBNull.Value)
                {
                    cmbAccountName.SelectedValue = key;
                }
            }
        }

        private void cmbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccountName.SelectedItem is DataRowView selectedRow)
            {
                object key = selectedRow["SupAccKy"];

                if (key != DBNull.Value)
                {
                    cmbAccountCode.SelectedValue = key;
                }
            }
        }

        private void cmbAccountCode_Leave(object sender, EventArgs e)
        {
            if (cmbAccountCode.Text != "")
            {
                cmbAddress.Text = "";
                cmbAddress.Items.Clear();

                try
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "Select AdrKy,AdrNm from vewAdrNmPAccKy Where AccKy = @accoountKey And CKy = @companyKey";
                        int count = 0; //count variable get count of the rows get from database
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@accoountKey", cmbAccountCode.SelectedValue);
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbAddress.Items.Add(reader["AdrNm"].ToString());
                                count++;
                            }
                        }

                        //if have one row only, display it automatically
                        if (count == 1)
                        {
                            cmbAddress.SelectedIndex = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmbReturnNo_Enter(object sender, EventArgs e)
        {
            cmbReturnNo.Tag = cmbReturnNo.Text;
        }

        private void cmbReturnNo_Leave(object sender, EventArgs e)
        {
            if (cmbReturnNo.Text != "" && cmbReturnNo.Tag.ToString().Trim() != cmbReturnNo.Text.Trim())
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    string query = "Select TrnKy from VewTrnNo Where TrnNo = @greNo And OurCd = 'PURRTN' And CKy = @companyKey";

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@greNo", cmbReturnNo.Text);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            if (UserSession.Instance.GetPermission("mnuPurRtn").CanUpdate)
                            {
                                ProcUpdate();
                                GetData();
                                GetInvoiceTotal();

                                btnAdd.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show("Sorry, You don't have enough Privileges to update transactions");
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                if (UserSession.Instance.GetPermission("mnuPurRtn").CanCreateNew)
                {
                    MessageBox.Show("Transaction No. not exist \n Would you like to enter new purchase return ?");
                    ProcNewMode();
                    FormClear();
                    return;
                }
                else
                {
                    MessageBox.Show("Sorry, You don't have enough Privileges to enter new transactions");
                    return;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (UserSession.Instance.GetPermission("mnuPurRtn").CanCreateNew)
            {
                if (dgvGre.Rows.Count > 1)
                {
                    DialogResult result = (MessageBox.Show("Do you really want to enter new transaction ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                    if (result == DialogResult.Yes)
                    {
                        ProcNewMode();
                    }
                }
                else
                {
                    ProcNewMode();
                }
            }
            else
            {
                MessageBox.Show("Access is denied!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();

                if (!UserSession.Instance.GetPermission("mnuPurRtn").CanDelete)
                {
                    MessageBox.Show("Access is denied");
                    return;
                }

                if (!DatabaseFunctions.isValidTrnDate(UserSession.Instance.CompanyKey, "PURRTN", dtpReturnDate.Value, conn))
                {
                    MessageBox.Show("You cannot delete transactions for this date!");
                    return;
                }

                formMode = (int)FormMode.DELETE;

                DialogResult result = MessageBox.Show("This Record will be Permanently deleted from the files. Are you sure ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        cmbReturnNo_Leave(cmbReturnNo, EventArgs.Empty);

                        //Get tranKey
                        string query = "Select TrnKy from vewTrnNo Where TrnNo = @grnNo And OurCd = 'PURRTN' And CKy = @companyKey";

                        SqlCommand cmdGetTranKey = new SqlCommand(query, conn);
                        cmdGetTranKey.Parameters.AddWithValue("@grnNo", cmbReturnNo.Text);
                        cmdGetTranKey.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using (SqlDataReader TranKeyReader = cmdGetTranKey.ExecuteReader())
                        {
                            if (TranKeyReader.HasRows)
                            {
                                TranKeyReader.Read();
                                tranKey = Convert.ToInt32(TranKeyReader["TrnKy"]);
                            }
                            else
                            {
                                MessageBox.Show("No Transaction found for this number!");
                                cmbReturnNo.Text = "";
                                cmbReturnNo.Focus();
                                return;
                            }
                        }

                        SqlTransaction tran = conn.BeginTransaction();
                        inTransaction = true;

                        btnDelete.Enabled = false;

                        try
                        {
                            //Update TranKey as inAct
                            string tranUpdateQuery = "UPDATE TrnMas SET fInAct = 1 WHERE TrnKy = @tranKey";

                            using (SqlCommand cmd = new SqlCommand(tranUpdateQuery, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranKey", tranKey);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from ItmTrn
                            string deleteItemQuery = "DELETE FROM ItmTrn WHERE TrnKy = @tranKey";
                            using (SqlCommand cmd = new SqlCommand(deleteItemQuery, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranKey", tranKey);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from AccTrn
                            string deleteAccTrnQuery = "DELETE FROM AccTrn WHERE TrnKy = @tranKey";
                            using (SqlCommand cmd = new SqlCommand(deleteAccTrnQuery, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranKey", tranKey);
                                cmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                            inTransaction = false;

                            MessageBox.Show("Transaction deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            btnDelete.Enabled = false;
                            btnSave.Enabled = true;
                            FormClear();
                            ProcNewMode();
                            LoadReturnNumbers();
                        }
                        catch (Exception ex)
                        {
                            if (inTransaction)
                            {
                                tran.Rollback();
                            }
                            MessageBox.Show(ex.Message);
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int tranTypeKey;
            int addressKey = 0;
            int locationKey = 0;
            int paymentTermKey = 0;

            inTransaction = false;

            //Check valid user inputs
            if (string.IsNullOrEmpty(cmbLocation.Text))
            {
                MessageBox.Show("Select the location");
                cmbLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cmbReturnNo.Text))
            {
                MessageBox.Show("Return No. is a must");
                cmbReturnNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cmbAccountCode.Text))
            {
                MessageBox.Show("Select the Supplier Account.");
                cmbAccountCode.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cmbAddress.Text))
            {
                MessageBox.Show("Select the Supplier Address.");
                cmbAddress.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cmbPurchaseAccount.Text))
            {
                MessageBox.Show("Select the Purchase Account.");
                cmbPurchaseAccount.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cmbPaymentTerm.Text))
            {
                MessageBox.Show("Select the payment term.");
                cmbPaymentTerm.Focus();
                return;
            }
            Console.WriteLine("Run 1");
            Console.WriteLine(formMode);

            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();

                //Get tranTypeKey
                string query = "Select TrnTypKy from vewTrnTypCd where TrnTyp ='PURRTN' And Cky = @companyKey";

                using (SqlCommand cmdGetTranTypeKey = new SqlCommand(query, conn))
                {
                    cmdGetTranTypeKey.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    using (SqlDataReader TranTypeKeyReader = cmdGetTranTypeKey.ExecuteReader())
                    {
                        if (TranTypeKeyReader.HasRows)
                        {
                            TranTypeKeyReader.Read();
                            tranTypeKey = Convert.ToInt32(TranTypeKeyReader["TrnTypKy"]);
                        }
                        else
                        {
                            tranTypeKey = 0;
                            MessageBox.Show("Transaction type not found, Call InterWave...!");
                            return;
                        }
                    }
                }

                //Get AddressKey
                using (SqlCommand cmd = new SqlCommand("Select AdrKy from vewAdrNmPAccKy Where AdrNm = @addressName", conn))
                {
                    cmd.Parameters.AddWithValue("@addressName", cmbAddress.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            addressKey = Convert.ToInt32(reader["AdrKy"]);
                        }
                    }
                }

                //Get LocationKey
                using (SqlCommand cmd = new SqlCommand("SELECT LocKy From vewLocCd WHERE LocCd = @locationCode", conn))
                {
                    cmd.Parameters.AddWithValue("@locationCode", cmbLocation.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            locationKey = Convert.ToInt32(reader["LocKy"]);
                        }
                    }
                }

                //Get PaymentTermKey
                using (SqlCommand cmd = new SqlCommand("SELECT PmtTrmKy From vewPmtTrmCd WHERE PmtTrmCd = @paymentTermCode", conn))
                {
                    cmd.Parameters.AddWithValue("@paymentTermCode", cmbPaymentTerm.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            paymentTermKey = Convert.ToInt32(reader["PmtTrmKy"]);
                        }
                    }
                }

                //Check tran date valid or not
                if (!DatabaseFunctions.isValidTrnDate(UserSession.Instance.CompanyKey, "PURRTN", dtpReturnDate.Value, conn))
                {
                    MessageBox.Show("You cannot enter/alter transactions for this date");
                    return;
                }
                Console.WriteLine("Run 2");

                SqlTransaction tran = conn.BeginTransaction();
                inTransaction = true;

                btnSave.Enabled = false;

                try
                {
                    Console.WriteLine("Run 3");

                    int tranNo = 0;
                    int maxTranKey = 0;
                    decimal amount = 0;


                    //Save input field details
                    if (formMode == (int)FormMode.NEW)
                    {
                        if (string.IsNullOrWhiteSpace(cmbYourReference.Text))
                        {

                            tranNo = DatabaseFunctions.GetTrnNoLstSave(UserSession.Instance.CompanyKey, 0, dtpReturnDate.Value, "PURRTN", conn, tran);

                            query = "Insert into TrnMas(TrnDt, TrnNo, TrnTypKy, DocNo, ourcd, AdrKy, fApr, LocKy, Des, AccKy, PmtTrmKy, EntUsrKy, EntDtm) " +
                                    "values(@tranDate, @tranNo, @tranTypeKey, @docNo, @ourCode, @addressKey, @fApr, @locationKey, @description, @accKey, @paymentTermKey, @userKey, @enterDateTime)";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpReturnDate.Value);
                                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                                cmd.Parameters.AddWithValue("@tranTypeKey", tranTypeKey);
                                cmd.Parameters.AddWithValue("@ourCode", "GRN");
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@fApr", 1);
                                cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                cmd.Parameters.AddWithValue("@docNo", txtDocumentNo.Text);
                                cmd.Parameters.AddWithValue("@accKey", cmbAccountCode.SelectedValue);
                                cmd.Parameters.AddWithValue("@paymentTermKey", paymentTermKey);
                                cmd.Parameters.AddWithValue("@userKey", UserSession.Instance.UserKey);
                                cmd.Parameters.AddWithValue("@enterDateTime", DateTime.Now);

                                cmd.ExecuteNonQuery();
                                Console.WriteLine("Run 4");
                            }

                        }
                        else
                        {
                            tranNo = DatabaseFunctions.GetTrnNoLstSave(UserSession.Instance.CompanyKey, 0, dtpReturnDate.Value, "GRN", conn, tran);

                            query = "Insert into TrnMas(TrnDt, TrnNo, TrnTypKy, ourcd, YurRef, AdrKy, fApr, LocKy, Des, AccKy, PmtTrmKy, EntUsrKy, EntDtm) " +
                                    "values(@tranDate, @tranNo, @tranTypeKey, @ourCode, @reference, @addressKey, @fApr, @locationKey, @description, @accKey, @paymentTermKey, @userKey, @enterDateTime)";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpReturnDate.Value);
                                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                                cmd.Parameters.AddWithValue("@tranTypeKey", tranTypeKey);
                                cmd.Parameters.AddWithValue("@ourCode", "GRN");
                                cmd.Parameters.AddWithValue("@reference", cmbYourReference.Text);
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@fApr", 1);
                                cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                cmd.Parameters.AddWithValue("@docNo", txtDocumentNo.Text);
                                cmd.Parameters.AddWithValue("@accKey", cmbAccountCode.SelectedValue);
                                cmd.Parameters.AddWithValue("@paymentTermKey", paymentTermKey);
                                cmd.Parameters.AddWithValue("@userKey", UserSession.Instance.UserKey);
                                cmd.Parameters.AddWithValue("@enterDateTime", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }

                        }
                    }

                    else if (formMode == (int)FormMode.UPDATE)
                    {
                        Console.WriteLine("Run 4.1 update mode");
                        //Get maxTranKey
                        query = "Select TrnKy from vewTrnNo Where TrnNo = @grnNo And OurCd = 'PURRTN' And CKy = @companyKey";

                        using (SqlCommand cmdGetTranKey = new SqlCommand(query, conn, tran))
                        {
                            cmdGetTranKey.Parameters.AddWithValue("@grnNo", cmbReturnNo.Text);
                            cmdGetTranKey.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                            using (SqlDataReader TranKeyReader = cmdGetTranKey.ExecuteReader())
                            {
                                if (TranKeyReader.HasRows)
                                {
                                    TranKeyReader.Read();
                                    maxTranKey = Convert.ToInt32(TranKeyReader["TrnKy"]);
                                }
                                else
                                {
                                    maxTranKey = 0;
                                }
                            }

                        }
                        if (string.IsNullOrWhiteSpace(cmbYourReference.Text))
                        {
                            Console.WriteLine("Run 4.2.0 update mode");

                            query = "UPDATE TrnMas " +
                                "SET TrnDt = @tranDate, Status = 'U', AdrKy = @addressKey, PmtModeKy = @paymentModeKey, DocNo = @docNo, PmtTrmKy = @paymentTermKey " +
                                "WHERE TrnKy = @TrnKy";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpReturnDate.Value);
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@docNo", txtDocumentNo.Text);
                                cmd.Parameters.AddWithValue("@paymentTermKey", paymentTermKey);
                                cmd.Parameters.AddWithValue("@paymentModeKey", 0);
                                cmd.Parameters.AddWithValue("@TranKy", maxTranKey);

                                cmd.ExecuteNonQuery();
                            }

                        }
                        else
                        {
                            Console.WriteLine("Run 4.2.1 update mode");

                            query = "UPDATE TrnMas " +
                                "SET TrnDt = @tranDate, YurRef = @yourReference, Status = 'U', AdrKy = @addressKey, PmtModeKy = @paymentModeKey, DocNo = @docNo, PmtTrmKy = @paymentTermKey " +
                                "WHERE TrnKy = @TrnKy";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@yourReference", cmbYourReference.Text);
                                cmd.Parameters.AddWithValue("@tranDate", dtpReturnDate.Value);
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@docNo", txtDocumentNo.Text);
                                cmd.Parameters.AddWithValue("@paymentTermKey", paymentTermKey);
                                cmd.Parameters.AddWithValue("@paymentModeKey", 0);
                                cmd.Parameters.AddWithValue("@TranKy", maxTranKey);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    //**********************
                    if (formMode == (int)FormMode.NEW || formMode == (int)FormMode.UPDATE)
                    {
                        //Get maxTranKey
                        query = "Select TrnKy from vewTrnNo Where TrnNo = @grnNo And OurCd = 'GRN' And CKy = @companyKey";

                        using (SqlCommand cmdGetTranKey = new SqlCommand(query, conn, tran))
                        {
                            cmdGetTranKey.Parameters.AddWithValue("@grnNo", cmbReturnNo.Text);
                            cmdGetTranKey.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                            using (SqlDataReader TranKeyReader = cmdGetTranKey.ExecuteReader())
                            {
                                if (TranKeyReader.HasRows)
                                {
                                    TranKeyReader.Read();
                                    maxTranKey = Convert.ToInt32(TranKeyReader["TrnKy"]);
                                }
                                else
                                {
                                    maxTranKey = 0;
                                }
                            }
                            Console.WriteLine("Run 5");

                        }

                        //_| ItemCode & "|" & ItemName & "|" & Unit & "||" & Rate & "|| Quantity|Total|VAT|NSL|VATAmount|NSLAmount|ItmTrnKy|Updt|Del"
                        //saving the itemtrnky,trnky itemky and qty into the ItemTrn Table
                        foreach (DataGridViewRow row in dgvGre.Rows)
                        {
                            decimal salePrice;
                            decimal costPrice;

                            if (!string.IsNullOrWhiteSpace(row.Cells["itemName"].Value?.ToString()))
                            {
                                if (Convert.ToInt32(row.Cells["update"].Value) == 1)
                                {
                                    if (Convert.ToInt32(row.Cells["delete"].Value) == 1)
                                    {
                                        query = "Delete From ItmTrn Where ItmTrnKy = @itemTranKey";
                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@itemTranKey", Convert.ToInt32(row.Cells["itemTranKey"].Value));
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    int itemKey = Convert.ToInt32(row.Cells["itemName"].Value);

                                    query = "Select SlsPri,CosPri,UnitKy from vewItmMas where ItmKy = @itemKey";
                                    using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                    {
                                        cmd.Parameters.AddWithValue("@itemKey", itemKey);
                                        using (SqlDataReader reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                salePrice = Convert.ToDecimal(reader["SlsPri"]?.ToString());
                                                costPrice = Convert.ToDecimal(reader["CosPri"]?.ToString());
                                            }
                                            else
                                            {
                                                salePrice = 0;
                                                costPrice = 0;
                                            }
                                        }
                                    }

                                    if (string.IsNullOrEmpty(row.Cells["itemTranKey"].Value?.ToString()))
                                    {
                                        //if trankey enpty, insert into db
                                        query = "Insert into ItmTrn(TrnKy,LocKy,ItmKy,Qty,SlsPri,CosPri,TrnPri,Amt1,Amt2,LiNo) " +
                                            "values(@maxTranKey, @locationKey, @itemKey, @quantity, @salePrice, @costPrice, @tranPrice, 0, 0, @rowNo)";
                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@maxTranKey", maxTranKey);
                                            cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                            cmd.Parameters.AddWithValue("@itemKey", itemKey);
                                            cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["quantity"].Value) * -1);
                                            cmd.Parameters.AddWithValue("@salePrice", salePrice);
                                            cmd.Parameters.AddWithValue("@costPrice", costPrice);
                                            cmd.Parameters.AddWithValue("@tranPrice", Convert.ToDecimal(row.Cells["rate"].Value));
                                            cmd.Parameters.AddWithValue("@rowNo", row.Index);

                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        query = "Update ItmTrn SET ItmKy = @itemKey, Qty = @quantity, CosPri = @costPrice, SlsPri = @salePrice, " +
                                            "Amt1 = 0, Amt2 = 0, TrnPri = @tranPrice WHERE ItmTrnKy = @itemTranKey";
                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@itemKey", itemKey);
                                            cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["quantity"].Value) * -1);
                                            cmd.Parameters.AddWithValue("@salePrice", salePrice);
                                            cmd.Parameters.AddWithValue("@costPrice", costPrice);
                                            cmd.Parameters.AddWithValue("@tranPrice", Convert.ToDecimal(row.Cells["rate"].Value));
                                            cmd.Parameters.AddWithValue("@itemTranKey", Convert.ToInt32(row.Cells["itemTranKey"].Value));

                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }

                        query = "SELECT TrnKy,Qty,TrnPri,Amt1,Amt2,Amt3 From vewGRNDtls Where TrnKy = @maxTranKey";
                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@maxTranKey", maxTranKey);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        amount += Convert.ToDecimal(reader["Qty"]) * Convert.ToDecimal(reader["TrnPri"]);
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Run 8");
                    }

                    if (formMode == (int)FormMode.NEW || formMode == (int)FormMode.UPDATE)
                    {
                        query = "Select AccTrnKy from vewAccTrnKy Where TrnKy = @tranKey And LiNo=1";
                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@tranKey", maxTranKey);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                bool hasRows = reader.HasRows;
                                reader.Close();

                                if (hasRows)
                                {
                                    query = "Insert into AccTrn(TrnKy,AccKy,LiNo,Amt,PmtModeKy) " +
                                            "Values(@maxTranKey, @accKey, 1 , @amount, @paymentModeKey)";
                                }
                                else
                                {
                                    query = "Update AccTrn set Amt = @amount, status = 'U', AccKy = @accKey, PmtModeKy = @paymentModeKey Where TrnKy = @maxTranKey and LiNo = 1";
                                }
                                using (SqlCommand cmdInsert = new SqlCommand(query, conn, tran))
                                {
                                    cmdInsert.Parameters.AddWithValue("@maxTranKey", maxTranKey);
                                    cmdInsert.Parameters.AddWithValue("@accKey", cmbAccountCode.SelectedValue);
                                    cmdInsert.Parameters.AddWithValue("@amount", amount);
                                    cmdInsert.Parameters.AddWithValue("@paymentModeKey", 0);
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                        }

                        query = "Select AccTrnKy from vewAccTrnKy Where TrnKy = @maxTranKey And LiNo=2";
                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@maxTranKey", maxTranKey);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                bool hasRows = reader.HasRows;
                                reader.Close();

                                if (hasRows)
                                {
                                    query = "Insert into AccTrn(TrnKy,AccKy,LiNo,amt,PmtModeKy) " +
                                            "Values(@maxTranKey, @accKey, 2 , @amount, @paymentModeKey)";
                                }
                                else
                                {
                                    query = "Update AccTrn set Amt = @amount, status = 'U', AccKy = @accKey, PmtModeKy = @paymentModeKey Where TrnKy = @maxTranKey and LiNo = 1";
                                }
                                using (SqlCommand cmdInsert = new SqlCommand(query, conn, tran))
                                {
                                    cmdInsert.Parameters.AddWithValue("@maxTranKey", maxTranKey);
                                    cmdInsert.Parameters.AddWithValue("@accKey", cmbAccountCode.SelectedValue);
                                    cmdInsert.Parameters.AddWithValue("@amount", amount * -1);
                                    cmdInsert.Parameters.AddWithValue("@paymentModeKey", 0);
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    tran.Commit();
                    inTransaction = false;

                    MessageBox.Show("Saved");
                    ShowGreReport(Convert.ToInt32(cmbReturnNo.Text));


                    //Open Report here

                    ProcNewMode();
                    Console.WriteLine("Run 10");

                }

                catch (Exception ex)
                {
                    try
                    {
                        if (tran != null && tran.Connection != null)
                        {
                            tran.Rollback();
                        }
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine("Rollback failed: " + rollbackEx.Message);
                    }

                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var frmPurchaseItemReturnPreview = new frmPurchaseItemReturnPreview();
            frmPurchaseItemReturnPreview.Show();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvGre_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvGre.Columns[e.ColumnIndex].Name == "itemCode")
                {
                    var cellValue = dgvGre.Rows[e.RowIndex].Cells["itemCode"].Value;

                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        dgvGre.CurrentCell = dgvGre.Rows[e.RowIndex].Cells["itemName"];
                        return;
                    }
                    else
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            string query = "SELECT ItmCd,ItmNm,Unit,CosPri,ItmKy From vewItmMas where ItmCd = @itemCode";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@itemCode", dgvGre.Rows[e.RowIndex].Cells["colItemCode"].Value.ToString());
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int rowIndex = dgvGre.CurrentRow.Index;

                                        string itemName = reader["ItmNm"] == DBNull.Value ? null : reader["ItmNm"].ToString();
                                        object itemNm = DBNull.Value;

                                        if (!string.IsNullOrEmpty(itemName))
                                        {
                                            DataRow[] matchingItemRow = itemNamesTable.Select($"ItmNm = '{itemName.Replace("'", "''")}'");
                                            if (matchingItemRow.Length > 0)
                                            {
                                                itemNm = matchingItemRow[0]["ItmKy"];
                                            }
                                        }
                                        dgvGre.Rows[rowIndex].Cells["itemCode"].Value = reader["ItmCd"].ToString();
                                        dgvGre.Rows[rowIndex].Cells["itemName"].Value = itemNm;
                                        dgvGre.Rows[rowIndex].Cells["unit"].Value = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "";
                                        dgvGre.Rows[rowIndex].Cells["rate"].Value = reader["CosPri"] != DBNull.Value ? Convert.ToDecimal(reader["CosPri"]) : 0;
                                    }
                                }
                            }
                        }
                    }
                }

                if (dgvGre.Columns[e.ColumnIndex].Name == "itemName")//strt here
                {
                    var cellValue = dgvGre.Rows[e.RowIndex].Cells["itemName"].Value;

                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        dgvGre.CurrentCell = dgvGre.Rows[e.RowIndex].Cells["itemCode"];
                        return;
                    }
                    else
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            string query = "SELECT ItmCd,ItmNm,Unit,CosPri,ItmKy From vewItmMas where ItmKy = @itemKey";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                object val = dgvGre.Rows[e.RowIndex].Cells["colItemName"].Value;
                                if (val == null || !int.TryParse(val.ToString(), out int itmKy))
                                    return;

                                cmd.Parameters.AddWithValue("@itemKey", itmKy);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int rowIndex = dgvGre.CurrentRow.Index;

                                        dgvGre.Rows[rowIndex].Cells["itemCode"].Value = reader["ItmCd"].ToString();
                                        dgvGre.Rows[rowIndex].Cells["unit"].Value = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "";
                                        dgvGre.Rows[rowIndex].Cells["rate"].Value = reader["CosPri"] != DBNull.Value ? Convert.ToDecimal(reader["CosPri"]) : 0;
                                    }
                                }
                            }
                        }
                    }
                }
                
                if (dgvGre.Columns[e.ColumnIndex].Name == "quantity")
                {
                    if (!string.IsNullOrWhiteSpace(dgvGre.Rows[e.RowIndex].Cells["quantity"].Value.ToString()))
                    {
                        int rowIndex = dgvGre.CurrentRow.Index;

                        dgvGre.Rows[rowIndex].Cells["total"].Value = Convert.ToDecimal(dgvGre.Rows[rowIndex].Cells["total"].Value) * Convert.ToDecimal(dgvGre.Rows[rowIndex].Cells["total"].Value);
                    }
                }

                dgvGre.Rows[e.RowIndex].Cells["colUpdate"].Value = 1;

                GetInvoiceTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Satrt from here
        private void dgvGre_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbLocation.Text))
            {
                MessageBox.Show("Select location first.");
                cmbLocation.Focus();
            }
        }

        private void dgvGre_KeyDown(object sender, KeyEventArgs e)
        {
            int rowIndex = dgvGre.CurrentCell.RowIndex;
            int columnIndex = dgvGre.CurrentCell.ColumnIndex;

            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Do you really want to DELETE the current row ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dgvGre.Rows[rowIndex].Cells["update"].Value = 1;
                    dgvGre.Rows[rowIndex].Cells["delete"].Value = 1;
                    dgvGre.Rows[rowIndex].Visible = false;
                }
            }
        }
    }
}
