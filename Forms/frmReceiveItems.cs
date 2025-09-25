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
using Microsoft.Reporting.Map.WebForms.BingMaps;
using POS_System.Classes;
using POS_System.Modules;

namespace POS_System.Forms
{
    public partial class frmReceiveItems : Form
    {
        private int childFormNumber = 0;

        public frmReceiveItems()
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

        // Handle key navigation ----------------------------------------------------------------------------------------------------------
        private void cmbGrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dtpGrnDate.Focus();
            }
        }

        private void dtpGrnDate_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmbReference.Focus();
            }
        }

        private void cmbReference_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbLocation.Focus();
            }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmbSupplierCode.Focus();
            }
            else if(e.KeyCode == Keys.Back)
            {
                cmbLocation.Text = "";
            }
        }

        private void cmbSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(string.IsNullOrWhiteSpace(cmbSupplierCode.Text))
                {
                    cmbSupplierName.Focus();
                }
                else
                {
                    cmbAddress.Focus();
                }
            }
        }

        private void cmbSupplierName_KeyDown(object sender, KeyEventArgs e)
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
                txtDescription.Focus();
            }
            else if(e.KeyCode == Keys.Back)
            {
                cmbAddress.Text = "";
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
            }
        }

        private void cmbOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
            }
        }

        private void cmbOrderDocNo_KeyDown(object sender, KeyEventArgs e)
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
            if(e.KeyCode == Keys.Enter)
            {
                dgvGrn.Focus();
                dgvGrn.CurrentCell = dgvGrn.Rows[1].Cells[1];
            }
        }

        //-------------------------------------------------------------------------------------------
        int companyKey = UserSession.Instance.CompanyKey;
        int formMode;
        int grnNo;
        int tranKey;
        string tranType;
        string CostPr, SalePr, Unit;
        DataTable SupplierDetails;
        DataTable itemNamesTable;
        bool formLoaded;
        bool inTransaction = false;


        private void LoadGrnNumbers()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    cmbGrnNo.Items.Clear();
                    string query = "SELECT TrnNo,TrnKy From vewTrnNo where OurCd = 'GRN' Order By TrnNo";

                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbGrnNo.Items.Add(reader["TrnNo"].ToString());
                        }
                    }
                }
            }
            catch(Exception ex)
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
                    string query = "SELECT LocKy,LocCd From vewLocCd Order By LocCd";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbLocation.Items.Clear();

                        while (reader.Read())
                        {
                            cmbLocation.Items.Add(reader["LocCd"].ToString());
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPaymentTerms()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    string query = "SELECT PmtTrmKy,PmtTrmCd From vewPmtTrmCd Order By PmtTrmCd";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbPaymentTerm.Items.Clear();

                        while (reader.Read())
                        {
                            cmbPaymentTerm.Items.Add(reader["PmtTrmCd"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPurchaseAccounts()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                string query = "SELECT AccKy,AccNm From vewAccAccCd where AccTyp = 'PUR' Order By AccNm";

                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    cmbPurchaseAccount.Items.Clear();

                    while (reader.Read())
                    {
                        cmbPurchaseAccount.Items.Add(reader["AccNm"].ToString());
                    }
                }
            }
        }

        private int GetPurchaseAccKey()
        {
            int purchaseAccKey = 0;
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                string query = "SELECT AccKy From vewAccAccCd where AccTyp = 'PUR' AND AccNm = @purchaseAccName";

                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@purchaseAccName", cmbPurchaseAccount.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            purchaseAccKey =  Convert.ToInt32(reader["AccKy"]);
                        }
                    }
                }
            }

            return purchaseAccKey;
        }

        private void SetupDgvGrn()
        {
            //Alignments
            dgvGrn.Columns["colUnit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrn.Columns["colCostPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrn.Columns["colTranPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrn.Columns["colSalePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrn.Columns["colQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrn.Columns["colAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrn.Columns["colExpireDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrn.Columns["colBatchNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrn.Columns["colExistQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrn.Columns["colExistExpireDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrn.Columns["colExistCostPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGrn.Columns["colInhand"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrn.Columns["colSave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Format
            dgvGrn.Columns["colCostPrice"].DefaultCellStyle.Format = "N2";
            dgvGrn.Columns["colTranPrice"].DefaultCellStyle.Format = "N2";
            dgvGrn.Columns["colSalePrice"].DefaultCellStyle.Format = "N2";
            dgvGrn.Columns["colQuantity"].DefaultCellStyle.Format = "N2";
            dgvGrn.Columns["colAmount"].DefaultCellStyle.Format = "N2";
            dgvGrn.Columns["colExpireDate"].DefaultCellStyle.Format = "MM-dd-yyyy";
            dgvGrn.Columns["colExistExpireDate"].DefaultCellStyle.Format = "MM-dd-yyyy";
            dgvGrn.Columns["colExistCostPrice"].DefaultCellStyle.Format = "N2";

            //Value type
            dgvGrn.Columns["colCostPrice"].ValueType = typeof(decimal);
            dgvGrn.Columns["colTranPrice"].ValueType = typeof(decimal);
            dgvGrn.Columns["colSalePrice"].ValueType = typeof(decimal);
            dgvGrn.Columns["colExpireDate"].ValueType = typeof(DateTime);
            dgvGrn.Columns["colExistExpireDate"].ValueType = typeof(DateTime);
            dgvGrn.Columns["colExistCostPrice"].ValueType = typeof(decimal);

            //Column width
            dgvGrn.Columns["colItemCode"].Width = 100;
            dgvGrn.Columns["colItemName"].Width = 150;
            dgvGrn.Columns["colUnit"].Width = 30;
            dgvGrn.Columns["colTranPrice"].Width = 65;
            dgvGrn.Columns["colSalePrice"].Width = 65;
            dgvGrn.Columns["colQuantity"].Width = 45;
            dgvGrn.Columns["colAmount"].Width = 50;
            dgvGrn.Columns["colExpireDate"].Width = 70;
            dgvGrn.Columns["colBatchNo"].Width = 45;
            dgvGrn.Columns["colInhand"].Width = 40;
            dgvGrn.Columns["colSave"].Width = 40;

            //bold headers
            dgvGrn.ColumnHeadersDefaultCellStyle.Font = new Font(dgvGrn.Font, FontStyle.Bold);
            dgvGrn.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            DataGridViewComboBoxColumn colItemName = (DataGridViewComboBoxColumn)dgvGrn.Columns["colItemName"];
            colItemName.DataSource = itemNamesTable;
            colItemName.DisplayMember = "ItmNm";
            colItemName.ValueMember = "Itmky";
            colItemName.DataPropertyName = "ItmKy";

        }

        private void LoadSupplierDetails()
        {
            SupplierDetails = new DataTable();

            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                string query = "SELECT SupAccKy, SupAccCd, SupAccNm from vewSupAccCd Order By SupAccCd";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(SupplierDetails);
            }

            cmbSupplierCode.DataSource = SupplierDetails.Copy();
            cmbSupplierCode.DisplayMember = "SupAccCd";
            cmbSupplierCode.ValueMember = "SupAccKy";

            cmbSupplierName.DataSource = SupplierDetails;
            cmbSupplierName.DisplayMember = "SupAccNm";
            cmbSupplierName.ValueMember = "SupAccKy";
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

        private void FormClear()
        {
            cmbGrnNo.Text = "";
            cmbReference.Text = "";
            cmbLocation.Text = "";
            cmbSupplierCode.Text = "";
            cmbSupplierName.Text = "";
            cmbAddress.Text = "";
            cmbPurchaseAccount.Text = "";
            cmbPaymentTerm.Text = "";
            txtDescription.Text = "";
            txtTotalAmount.Text = "";
            dtpGrnDate.Value = DateTime.Now;
            dgvGrn.Rows.Clear();
        }

        private void ProcNewMode()
        {
            if(UserSession.Instance.GetPermission("MnuGRN").CanCreateNew)
            {
                formMode = (int)FormMode.NEW;

                btnDelete.Enabled = false;
                btnSave.Enabled = true;

                FormClear();
                grnNo = DatabaseFunctions.GetTrnNoList(companyKey, 0, dtpGrnDate.Value, tranType) +1;
                cmbGrnNo.Text = grnNo.ToString();
                SetupDgvGrn();
            }
            else
            {
                MessageBox.Show("Access is denied");
            }
        }

        private void ProcUpdate()
        {
            if (UserSession.Instance.GetPermission("MnuGRN").CanCreateNew)
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

        private string GetPaymentTermNm(string paymentTermKey)
        {
            string paymentTermNm = "";

            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                string query = "SELECT PmtTrmCd From vewPmtTrmCd Where PmtTrmKy = @addressKey";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@addressKey", paymentTermKey);
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

        private decimal CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach(DataGridViewRow row in dgvGrn.Rows)
            {
                decimal amount = 0;

                if(row.IsNewRow) continue;

                if (row.Cells["colAmount"].Value != DBNull.Value)
                {
                    amount = Convert.ToDecimal(row.Cells["colAmount"].Value);
                    totalAmount += amount;
                }
            }
            return totalAmount;
        }

        private decimal GetItemQuantity(int itemKey, int locationKey)
        {
            decimal quantity = 0;

            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                string query = "Select Qty from ItmLoc Where ItmKy = @itemKey And LocKy = @locationKey";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@itemKey", itemKey);
                    cmd.Parameters.AddWithValue("@locationKey", locationKey);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            reader.Read();
                            quantity = Convert.ToDecimal(reader["Qty"]);
                        }
                    }
                }
            }
            
            return quantity;
        }

        private void GetData()
        {
            try
            {
                if(formMode == (int)FormMode.UPDATE && cmbGrnNo.Text != "")
                {
                    //Get tranKey
                    using(SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "Select TrnKy from vewTrnNo Where TrnNo = @grnNo And OurCd = 'GRN' And CKy = @companyKey";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@grnNo", cmbGrnNo.Text);
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                reader.Read();
                                tranKey = Convert.ToInt32(reader["TrnKy"]);
                            }
                            else
                            {
                                MessageBox.Show("No Transaction found for this number!");
                                cmbGrnNo.Text = "";
                                cmbGrnNo.Focus();
                                return;
                            }
                        }
                    }

                    //Load existing transaction details
                    using(SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "SELECT TrnDt, PurAccKy, PurAccCd, PurAccNm, AccNm, AccTyp, AccKy, AdrKy, Code, YurRef, Des, TrnKy, PmtTrmKy " +
                            "FROM vewGRNHdr where TrnKy = @tranKey";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@tranKey", tranKey);
                        using( SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                dtpGrnDate.Value = Convert.ToDateTime(reader["TrnDt"]);
                                cmbReference.Text = reader["YurRef"] != DBNull.Value? reader["YurRef"].ToString() : "";
                                cmbLocation.Text = reader["Code"].ToString();
                                cmbSupplierCode.SelectedValue = reader["AccKy"];
                                cmbAddress.Text = reader["AdrKy"] != DBNull.Value? GetAddressNm(reader["AdrKy"].ToString()) : "test";
                                txtDescription.Text = reader["Des"] != DBNull.Value ? reader["Des"].ToString() : "";
                                cmbPurchaseAccount.Text = GetPurchaseAccNm(reader["PurAccKy"].ToString());
                                cmbPaymentTerm.Text = GetPaymentTermNm(reader["PmtTrmKy"].ToString());
                            }
                        }
                    }

                    //Fill datagridview from data
                    using(SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        dgvGrn.Rows.Clear();
                        string query = "SELECT ItmKy,ItmCd,ItmNm,Unit,CosPri,TrnPri,SlsPri,Qty,Qty*TrnPri as Amt," +
                                        "ItmTrnKy,0 as Updt,0 as Del,ExpirDt,BatchNo,Qty as ExistQty,ItmKy as ExistItmKy, ExpirDt as ExistExpirDt," +
                                        "CosPri as ExistCosPri,'' as InHand,1 as fSave " +
                                        "FROM vewGRNDtls Where TrnKy = @tranKey";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@tranKey", tranKey);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int itemKey = reader["ItmKy"] != DBNull.Value ? Convert.ToInt32(reader["ItmKy"]) : -1;
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
                                decimal tranPrice = reader["TrnPri"] != DBNull.Value ? Convert.ToDecimal(reader["TrnPri"]) : 0;
                                decimal salesPrice = reader["SlsPri"] != DBNull.Value ? Convert.ToDecimal(reader["SlsPri"]) : 0;
                                decimal quantity = reader["Qty"] != DBNull.Value ? Convert.ToDecimal(reader["Qty"]) : 0;
                                decimal amount = reader["Amt"] != DBNull.Value ? Convert.ToDecimal(reader["Amt"]) : 0;
                                string itemTranKey = reader["ItmTrnKy"] != DBNull.Value ? reader["ItmTrnKy"].ToString() : "";
                                int update = Convert.ToInt32(reader["Updt"]);
                                int delete = Convert.ToInt32(reader["Del"]);
                                DateTime? expireDate = reader["ExpirDt"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirDt"]) : (DateTime?)null;
                                string batchNo = reader["BatchNo"] != DBNull.Value ? reader["BatchNo"].ToString() : "";
                                decimal existQuantity = reader["ExistQty"] != DBNull.Value ? Convert.ToDecimal(reader["ExistQty"]) : 0;
                                string existItemKey = reader["ExistItmKy"] != DBNull.Value ? reader["ExistItmKy"].ToString() : "";
                                DateTime? existExpireDate = reader["ExistExpirDt"] != DBNull.Value ? Convert.ToDateTime(reader["ExistExpirDt"]) : (DateTime?)null;
                                decimal existCostPrice = reader["ExistCosPri"] != DBNull.Value ? Convert.ToDecimal(reader["ExistCosPri"]) : 0;
                                string inHand = reader["InHand"].ToString();
                                bool save = Convert.ToBoolean(reader["fSave"]);

                                dgvGrn.Rows.Add(
                                    itemKey,
                                    itemCode,
                                    itemNameKey,
                                    unit,
                                    costPrice,
                                    tranPrice,
                                    salesPrice,
                                    quantity,
                                    amount,
                                    itemTranKey,
                                    update,
                                    delete,
                                    expireDate,
                                    batchNo,
                                    existQuantity,
                                    existItemKey,
                                    existExpireDate,
                                    existCostPrice,
                                    inHand,
                                    save
                                );
                            }
                        }
                    }

                    //Fill Total Amount
                    txtTotalAmount.Text = CalculateTotalAmount().ToString("#,0.00");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ShowGrnReport(int grnNo)
        {
            string query = "SELECT * FROM GrnRptQry WHERE TrnNo = @tranNo  AND OurCd = 'GRN'";
            DataTable dt = new DataTable();
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tranNo", grnNo);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            ReportDocument reportGrn = new ReportDocument();
            reportGrn.Load(@"E:\POS System\Reports\rptGrn.rpt");

            reportGrn.SetDataSource(dt);
            reportGrn.SetParameterValue("reportTitle", "GOODS RECEIPT NOTE");
            reportGrn.SetParameterValue("currentUserId", UserSession.Instance.UserId);

            frmReportViewer frmReportViewer = new frmReportViewer();
            frmReportViewer.LoadGrnReport(reportGrn);
            frmReportViewer.Show();
        }

        //----------------
        private void frmReceiveItems_Load(object sender, EventArgs e)
        {
            tranType = "GRN";
            this.KeyPreview = true;

            //Captioning
            //lblInvoiceNo.Text = Captioning.CaptionMe(companyKey, "GRN", "TrnNo");
            //lblCustomer.Text = Captioning.CaptionMe(companyKey, "GRN", "AccKy");
            //lblTranDate.Text = Captioning.CaptionMe(companyKey, "GRN", "TrnDt");
            //lblSalesAccount.Text = Captioning.CaptionMe(companyKey, "GRN", "PurAccKy");
            //lblLocation.Text = Captioning.CaptionMe(companyKey, "GRN", "LocKy");
            //CostPr = Captioning.CaptionMe(companyKey, "GRN", "CosPri");
            //SalePr = Captioning.CaptionMe(companyKey, "GRN", "slspri");
            //Unit = Captioning.CaptionMe(companyKey, "GRN", "UnitKy");

            //Add items to combo boxes
            LoadGrnNumbers();
            LoadLocations();
            LoadSupplierDetails();
            LoadPaymentTerms();
            LoadPurchaseAccounts();
            LoadItemNameComboBox();

            cmbSupplierCode.SelectedIndex = -1;
            cmbSupplierName.SelectedIndex = -1;

            SetupDgvGrn();

            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            formLoaded = false;
            pnlSearch.Visible = false;
        }


        //Search panel
        private void frmReceiveItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (cmbLocation.Text == "")
                {
                    MessageBox.Show("First select the location!");
                    cmbLocation.Focus();
                }
                else
                {
                    pnlSearch.Visible = true;
                    SetupDgvSearch();
                    txtSearchBox.Focus();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtSearchBox.Text = "";
                SetupDgvSearch();
                pnlSearch.Visible = false;
                dgvGrn.Focus();
            }
        }

        private void SetupDgvSearch()
        {
            dgvSearchItems.Columns.Clear();

            DataGridViewTextBoxColumn colItemKey = new DataGridViewTextBoxColumn();
            colItemKey.Name = "itemKey";
            colItemKey.HeaderText = "Item Key";
            colItemKey.Visible = false;
            colItemKey.ReadOnly = true;
            dgvSearchItems.Columns.Add(colItemKey);

            DataGridViewTextBoxColumn colItemCode = new DataGridViewTextBoxColumn();
            colItemCode.Name = "itemCode";
            colItemCode.HeaderText = "Item Code";
            colItemCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            colItemCode.ReadOnly = true;
            dgvSearchItems.Columns.Add(colItemCode);

            DataGridViewTextBoxColumn colItemName = new DataGridViewTextBoxColumn();
            colItemName.Name = "itemName";
            colItemName.HeaderText = "Item Name";
            colItemName.ReadOnly = true;
            dgvSearchItems.Columns.Add(colItemName);

            DataGridViewTextBoxColumn colSalesPrice = new DataGridViewTextBoxColumn();
            colSalesPrice.Name = "salesPrice";
            colSalesPrice.HeaderText = "Sales Pri";
            colSalesPrice.DefaultCellStyle.Format = "N2";
            colSalesPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalesPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            colSalesPrice.ReadOnly = true;
            dgvSearchItems.Columns.Add(colSalesPrice);

            DataGridViewTextBoxColumn colTotQty = new DataGridViewTextBoxColumn();
            colTotQty.Name = "totalQuantity";
            colTotQty.HeaderText = "Total Qty";
            colTotQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTotQty.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            colTotQty.ReadOnly = true;
            dgvSearchItems.Columns.Add(colTotQty);

            DataGridViewTextBoxColumn colUnit = new DataGridViewTextBoxColumn();
            colUnit.Name = "unit";
            colUnit.HeaderText = "Unit";
            colUnit.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            colUnit.ReadOnly = true;
            dgvSearchItems.Columns.Add(colUnit);

            DataGridViewTextBoxColumn colDiscount = new DataGridViewTextBoxColumn();
            colDiscount.Name = "Discount";
            colDiscount.HeaderText = "Discount";
            colDiscount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDiscount.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            colDiscount.ReadOnly = true;
            dgvSearchItems.Columns.Add(colDiscount);

            foreach (DataGridViewColumn col in dgvSearchItems.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchBox.Text.Trim();

            if (txtSearchBox.Text.Length >= 3)
            {
                try
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        string query = "SELECT ItmCd,ItmNm,CosPri,Qty,Unit,ItmKy,DisAmt FROM vewStkBalLocWise WHERE ItmNm LIKE @searchText ORDER BY ItmNm";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dgvSearchItems.Rows.Clear();
                            while (reader.Read())
                            {
                                dgvSearchItems.Rows.Add(
                                    reader["ItmKy"],
                                    reader["ItmCd"],
                                    reader["ItmNm"],
                                    reader["CosPri"],
                                    reader["Qty"],
                                    reader["Unit"],
                                    reader["DisAmt"]
                                );
                            }
                            reader.Close();
                        }
                    }
                    //clear dgv selection after load data
                    dgvSearchItems.ClearSelection();
                    dgvSearchItems.CurrentCell = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                dgvSearchItems.Rows.Clear();
            }
        }

        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvSearchItems.Focus();
            }
        }

        private void dgvSearchItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                int rowIndex = dgvSearchItems.CurrentCell.RowIndex;
                dgvSearchItems.Rows[rowIndex].Selected = true;
            }

            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                DataGridViewRow selectedRow = dgvSearchItems.CurrentRow;

                int newRowIndex = dgvGrn.Rows.Add();
                DataGridViewRow newRow = dgvGrn.Rows[newRowIndex];

                newRow.Cells["colItemKey"].Value = selectedRow.Cells["itemKey"].Value;
                newRow.Cells["colItemCode"].Value = selectedRow.Cells["itemCode"].Value;
                //newRow.Cells["itemName"].Value = selectedRow.Cells["itemName"].Value;
                newRow.Cells["colSalePrice"].Value = selectedRow.Cells["salesPrice"].Value;
                newRow.Cells["colInHand"].Value = selectedRow.Cells["totalQuantity"].Value;
                newRow.Cells["colUnit"].Value = selectedRow.Cells["unit"].Value;

                object itemName = selectedRow.Cells["itemName"].Value;
                selectedRow.Cells["itemName"].Value = itemName;

                txtSearchBox.Text = "";
                pnlSearch.Visible = false;
            }
        }


        //-------------------------------------------------------------------------------------------

        private void cmbSupplierCode_Leave(object sender, EventArgs e)
        {
            if(cmbSupplierCode.Text != "")
            {
                cmbAddress.Text = "";
                cmbAddress.Items.Clear();

                try
                {
                    using(SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "Select AdrKy,AdrNm from vewAdrNmPAccKy Where AccKy = @accoountKey And CKy = @companyKey";
                        int count = 0; //count variable get count of the rows get from database
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@accoountKey", cmbSupplierCode.SelectedValue);
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbAddress.Items.Add(reader["AdrNm"].ToString());
                                count++;
                            }
                        }

                        //if have one row only, display it automatically
                        if(count == 1)
                        {
                            cmbAddress.SelectedIndex = 0;
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmbSupplierName_Leave(object sender, EventArgs e)
        {
            if (cmbSupplierName.Text != "")
            {
                try
                {
                    cmbAddress.Text = "";
                    cmbAddress.Items.Clear();

                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "Select AdrKy,AdrNm from vewAdrNmPAccKy Where AccKy = @accoountKey And CKy = @companyKey";
                        int count = 0; //count variable get count of the rows get from database

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@accoountKey", cmbSupplierName.SelectedValue);
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

        private void cmbSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupplierCode.SelectedItem is DataRowView selectedRow)
            {
                object key = selectedRow["SupAccKy"];
                if (key != DBNull.Value)
                    cmbSupplierName.SelectedValue = key;
            }
        }

        private void cmbSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupplierName.SelectedItem is DataRowView selectedRow)
            {
                object key = selectedRow["SupAccKy"];
                if (key != DBNull.Value)
                    cmbSupplierCode.SelectedValue = key;
            }
        }

        private void frmReceiveItems_Activated(object sender, EventArgs e)
        {
            if(!formLoaded)
            {
                if(UserSession.Instance.GetPermission("MnuGRN").CanCreateNew)
                {
                    formMode = (int)FormMode.NEW;
                    ProcNewMode();
                }
                else if(UserSession.Instance.GetPermission("MnuGRN").CanUpdate)
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

        private void cmbGrnNo_Enter(object sender, EventArgs e)
        {
            cmbGrnNo.Tag = cmbGrnNo.Text;
        }

        private void cmbGrnNo_Leave(object sender, EventArgs e)
        {
            if(cmbGrnNo.Text != "" && cmbGrnNo.Tag.ToString().Trim() != cmbGrnNo.Text.Trim())
            {
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    string query = "Select TrnKy from VewTrnNo Where TrnNo = @grnNo And OurCd ='GRN' And CKy = @companyKey";

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@grnNo", cmbGrnNo.Text);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if(reader.HasRows)
                        {
                            if (UserSession.Instance.GetPermission("MnuGRN").CanUpdate)
                            {
                                ProcUpdate();
                                GetData();

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
                if (UserSession.Instance.GetPermission("MnuGRN").CanCreateNew)
                {
                    MessageBox.Show("Transaction No. not exist \n Would you like to enter new GRN ?");
                    ProcNewMode();
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
            if(UserSession.Instance.GetPermission("MnuGRN").CanCreateNew)
            {
                if(dgvGrn.Rows.Count > 1)
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
            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();

                if (!UserSession.Instance.GetPermission("MnuGRN").CanDelete)
                {
                    MessageBox.Show("Access is denied");
                    return;
                }

                if (!DatabaseFunctions.isValidTrnDate(UserSession.Instance.CompanyKey, "GRN", dtpGrnDate.Value, conn))
                {
                    MessageBox.Show("You cannot delete transactions for this date!");
                    return;
                }

                formMode = (int)FormMode.DELETE;

                DialogResult result = MessageBox.Show("This Record will be Permanently deleted from the files. Are you sure ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    try
                    {
                        cmbGrnNo_Leave(cmbGrnNo, EventArgs.Empty);

                        //Get tranKey
                        string query = "Select TrnKy from vewTrnNo Where TrnNo = @grnNo And OurCd = 'GRN' And CKy = @companyKey";

                        SqlCommand cmdGetTranKey = new SqlCommand(query, conn);
                        cmdGetTranKey.Parameters.AddWithValue("@grnNo", cmbGrnNo.Text);
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
                                cmbGrnNo.Text = "";
                                cmbGrnNo.Focus();
                                return;
                            }
                        }

                        SqlTransaction tran = conn.BeginTransaction();
                        inTransaction = true;

                        btnDelete.Enabled = false;

                        try
                        {
                            //update ItmBatch table
                            foreach (DataGridViewRow row in dgvGrn.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string updateQuery = "UPDATE ItmBatch SET Qty = Qty - @quantity WHERE ItmKy = @itemKey AND ExpirDt = @expirDate AND CosPri = @costPrice";

                                using (SqlCommand cmd = new SqlCommand(updateQuery, conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["colQuantity"].Value));
                                    cmd.Parameters.AddWithValue("@itemKey", row.Cells["colItemKey"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@expirDate", Convert.ToDateTime(row.Cells["colExpireDate"].Value));
                                    cmd.Parameters.AddWithValue("@costPrice", Convert.ToDecimal(row.Cells["colCostPrice"].Value));

                                    cmd.ExecuteNonQuery();
                                }
                            }

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
                            LoadGrnNumbers();
                        }
                        catch (Exception ex)
                        {
                            if(inTransaction)
                            {
                                tran.Rollback();
                            }
                            MessageBox.Show(ex.Message);
                        }
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //bool noValidItems;
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

            if (string.IsNullOrEmpty(cmbSupplierCode.Text))
            {
                MessageBox.Show("Select the Supplier Account.");
                cmbSupplierCode.Focus();
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
                string query = "Select TrnTypKy from vewTrnTypCd where TrnTyp ='GRN' And Cky = @companyKey";

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
                if (!DatabaseFunctions.isValidTrnDate(UserSession.Instance.CompanyKey, "GRN", dtpGrnDate.Value, conn))
                {
                    MessageBox.Show("You cannot enter/alter transactions for this date");
                    return;
                }
                Console.WriteLine("Run 2");

                SqlTransaction tran = conn.BeginTransaction();
                inTransaction = true;
                //noValidItems = true;

                btnSave.Enabled = false;

                try
                {
                    Console.WriteLine("Run 3");

                    int tranNo = 0;
                    int maxTranKey = 0;
                    int itemBatchKey = 0;
                    decimal amount = 0;


                    //Save input field details
                    if (formMode == (int)FormMode.NEW)
                    {
                        if (string.IsNullOrWhiteSpace(cmbReference.Text))
                        {

                            tranNo = DatabaseFunctions.GetTrnNoLstSave(UserSession.Instance.CompanyKey, 0, dtpGrnDate.Value, "GRN", conn, tran);

                            query = "Insert into TrnMas(TrnDt, TrnNo, TrnTypKy, ourcd, AdrKy, fApr, LocKy, Des, AccKy, PmtTrmKy, EntUsrKy, EntDtm) " +
                                    "values(@tranDate, @tranNo, @tranTypeKey, @ourCode, @addressKey, @fApr, @locationKey, @description, @accKey, @paymentTermKey, @userKey, @enterDateTime)";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpGrnDate.Value);
                                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                                cmd.Parameters.AddWithValue("@tranTypeKey", tranTypeKey);
                                cmd.Parameters.AddWithValue("@ourCode", "GRN");
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@fApr", 1);
                                cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@accKey", cmbSupplierCode.SelectedValue);
                                cmd.Parameters.AddWithValue("@paymentTermKey", paymentTermKey);
                                cmd.Parameters.AddWithValue("@userKey", UserSession.Instance.UserKey);
                                cmd.Parameters.AddWithValue("@enterDateTime", DateTime.Now);

                                cmd.ExecuteNonQuery();
                                Console.WriteLine("Run 4");
                            }

                        }
                        else
                        {
                            tranNo = DatabaseFunctions.GetTrnNoLstSave(UserSession.Instance.CompanyKey, 0, dtpGrnDate.Value, "GRN", conn, tran);

                            query = "Insert into TrnMas(TrnDt, TrnNo, TrnTypKy, ourcd, YurRef, AdrKy, fApr, LocKy, Des, AccKy, PmtTrmKy, EntUsrKy, EntDtm) " +
                                    "values(@tranDate, @tranNo, @tranTypeKey, @ourCode, @reference, @addressKey, @fApr, @locationKey, @description, @accKey, @paymentTermKey, @userKey, @enterDateTime)";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpGrnDate.Value);
                                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                                cmd.Parameters.AddWithValue("@tranTypeKey", tranTypeKey);
                                cmd.Parameters.AddWithValue("@ourCode", "GRN");
                                cmd.Parameters.AddWithValue("@reference", cmbReference.Text);
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@fApr", 1);
                                cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@accKey", cmbSupplierCode.SelectedValue);
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

                        if (string.IsNullOrWhiteSpace(cmbReference.Text))
                        {
                            Console.WriteLine("Run 4.2.0 update mode");

                            tranNo = Convert.ToInt32(cmbGrnNo.Text.Trim());

                            query = "Update TrnMas Set " +
                                    "TrnDt = @tranDate, AdrKy = @addressKey, fApr =1, LocKy = @locationKey, Des = @description, PmtTrmKy = @paymentTermKey, AccKy = @accKey " +
                                    "Where OurCd = @ourCode and TrnNo = @tranNo And fInAct = 0";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpGrnDate.Value);
                                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                                cmd.Parameters.AddWithValue("@ourCode", "GRN");
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@accKey", cmbSupplierCode.SelectedValue);
                                cmd.Parameters.AddWithValue("@paymentTermKey", paymentTermKey);

                                cmd.ExecuteNonQuery();
                            }

                        }
                        else
                        {
                            Console.WriteLine("Run 4.2.1 update mode");

                            tranNo = Convert.ToInt32(cmbGrnNo.Text.Trim());

                            query = "Update TrnMas Set " +
                                    "TrnDt = @tranDate, AdrKy = @addressKey, fApr =1, LocKy = @locationKey, Des = @description, PmtTrmKy = @paymentTermKey, AccKy = @accKey, YurRef = @reference " +
                                    "Where OurCd = @ourCode and TrnNo = @tranNo And fInAct = 0";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpGrnDate.Value);
                                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                                cmd.Parameters.AddWithValue("@ourCode", "GRN");
                                cmd.Parameters.AddWithValue("@reference", cmbReference.Text);
                                cmd.Parameters.AddWithValue("@addressKey", addressKey);
                                cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@accKey", cmbSupplierCode.SelectedValue);
                                cmd.Parameters.AddWithValue("@paymentTermKey", paymentTermKey);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    if (formMode == (int)FormMode.NEW || formMode == (int)FormMode.UPDATE)
                    {
                        //Get maxTranKey
                        query = "Select TrnKy from vewTrnNo Where TrnNo = @grnNo And OurCd = 'GRN' And CKy = @companyKey";

                        using (SqlCommand cmdGetTranKey = new SqlCommand(query, conn, tran))
                        {
                            cmdGetTranKey.Parameters.AddWithValue("@grnNo", cmbGrnNo.Text);
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

                        //saving the itemtrnky,trnky itemky and qty into the ItemTrn Table
                        foreach (DataGridViewRow row in dgvGrn.Rows)
                        {
                            decimal profitMargin = 0;
                            decimal quantityDifference = 0;
                            int itemTranKey;

                            if (!string.IsNullOrWhiteSpace(row.Cells["colItemCode"].Value?.ToString()))
                            {
                                if ((row.Cells["colDelete"].Value?.ToString() ?? "") != "1" && Convert.ToDecimal(row.Cells["colAmount"].Value ?? 0) != 0)
                                {
                                    if (Convert.ToInt32(row.Cells["colSave"].Value ?? 0) == 1)
                                    {
                                        //Get profit margin value
                                        query = "Select ItmKy,UnitKy,SlsPri,CosPri,prftMrgn from vewItmMas where ItmKy = @itemKey";
                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@itemKey", row.Cells["colItemKey"].Value);
                                            using (SqlDataReader reader = cmd.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    profitMargin = reader["prftMrgn"] != DBNull.Value ? Convert.ToDecimal(reader["prftMrgn"]) : 0m;
                                                }
                                            }
                                        }

                                        //When edit a transaction Old item quantity delete/reduce from the table
                                        query = "Select ItmBatchKy,Qty from ItmBatch Where ItmKy = @itemKey And ExpirDt = @expireDate And CosPri = @costPrice";
                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@itemKey", row.Cells["colItemKey"].Value);
                                            cmd.Parameters.AddWithValue("@costPrice", row.Cells["colCostPrice"].Value);

                                            object expireDateObj = row.Cells["colExistExpireDate"].Value;
                                            DateTime expireDate;
                                            if (expireDateObj == null || expireDateObj == DBNull.Value || string.IsNullOrWhiteSpace(expireDateObj.ToString()))
                                            {
                                                expireDate = new DateTime(2001, 2, 4);
                                            }
                                            else
                                            {
                                                if (!DateTime.TryParse(expireDateObj.ToString(), out expireDate) || expireDate < new DateTime(1753, 1, 1))
                                                {
                                                    expireDate = new DateTime(2001, 2, 4);
                                                }
                                            }
                                            cmd.Parameters.AddWithValue("@expireDate", expireDate);

                                            using (SqlDataReader reader = cmd.ExecuteReader())
                                            {
                                                if (reader.HasRows)
                                                {
                                                    reader.Read();
                                                    itemBatchKey = reader["ItmBatchKy"] != DBNull.Value ? Convert.ToInt32(reader["ItmBatchKy"]) : 0;

                                                    decimal dbQty = reader["Qty"] != DBNull.Value ? Convert.ToDecimal(reader["Qty"]) : 0;
                                                    decimal uiQty = Convert.ToDecimal(row.Cells["colQuantity"].Value ?? "0");
                                                    quantityDifference = dbQty - uiQty;

                                                    reader.Close();
                                                    //update ItmBatch table

                                                    using (SqlCommand itemBatchUpdateCmd = new SqlCommand("Update ItmBatch Set Qty = @quantityDifference Where ItmBatchKy = @itemBatchKey", conn, tran))
                                                    {
                                                        itemBatchUpdateCmd.Parameters.AddWithValue("@quantityDifference", quantityDifference);
                                                        itemBatchUpdateCmd.Parameters.AddWithValue("@itemBatchKey", itemBatchKey);
                                                        itemBatchUpdateCmd.ExecuteNonQuery();
                                                    }

                                                }
                                                else
                                                {
                                                    itemBatchKey = 0;
                                                }
                                            }
                                        }

                                        //Check wether updated values is exist the database when edit a transaction
                                        query = "Select ItmBatchKy,Qty from ItmBatch Where ItmKy = @itemKey And ExpirDt = @expireDate And CosPri = @costPrice";

                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@itemKey", row.Cells["colItemKey"].Value);
                                            cmd.Parameters.AddWithValue("@costPrice", row.Cells["colCostPrice"].Value);

                                            object expireDateObj = row.Cells["colExpireDate"].Value;
                                            DateTime expireDate;
                                            if (expireDateObj == null || expireDateObj == DBNull.Value || string.IsNullOrWhiteSpace(expireDateObj.ToString()))
                                            {
                                                expireDate = new DateTime(2001, 2, 4);
                                            }
                                            else
                                            {
                                                if (!DateTime.TryParse(expireDateObj.ToString(), out expireDate) || expireDate < new DateTime(1753, 1, 1))
                                                {
                                                    expireDate = new DateTime(2001, 2, 4);
                                                }
                                            }
                                            cmd.Parameters.AddWithValue("@expireDate", expireDate);

                                            using (SqlDataReader reader = cmd.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    itemBatchKey = reader["ItmBatchKy"] != DBNull.Value ? Convert.ToInt32(reader["ItmBatchKy"]) : 0;
                                                    quantityDifference = Convert.ToDecimal(reader["Qty"]) - Convert.ToDecimal(row.Cells["colQuantity"].Value);
                                                }
                                                else
                                                {
                                                    itemBatchKey = 0;
                                                }
                                            }
                                        }
                                        Console.WriteLine("run 5.1");

                                        if (row.Cells["colItemCode"].Value?.ToString().Trim() == "0" || row.Cells["colQuantity"].Value?.ToString().Trim() == "0")
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(row.Cells["colItemTranKey"].Value?.ToString()) == 0)
                                            {
                                                query = "Insert into ItmTrn(TrnKy,ItmKy,Qty,SlsPri,TrnPri,CosPri,LocKy,LiNo) " +
                                                        "values(@maxTranKey, @itemkey, @quantity, @salesPrice, @tranPrice, @costPrice, @locationKey, @rowNo )";

                                                using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                {
                                                    cmd.Parameters.AddWithValue("@maxTranKey", maxTranKey);
                                                    cmd.Parameters.AddWithValue("@itemkey", Convert.ToInt32(row.Cells["colItemKey"].Value));
                                                    cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["colQuantity"].Value));
                                                    cmd.Parameters.AddWithValue("@salesPrice", Convert.ToDecimal(row.Cells["colSalePrice"].Value));
                                                    cmd.Parameters.AddWithValue("@tranPrice", Convert.ToDecimal(row.Cells["colTranPrice"].Value));
                                                    cmd.Parameters.AddWithValue("@costPrice", Convert.ToDecimal(row.Cells["colcostPrice"].Value));
                                                    cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                                    cmd.Parameters.AddWithValue("@rowNo", row.Index);

                                                    cmd.ExecuteNonQuery();
                                                }

                                                query = "Select ItmTrnKy from ItmTrn Where ItmKy = @itemkey And TrnKy = @maxTranKey And LiNo = @rowNo";
                                                using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                {
                                                    cmd.Parameters.AddWithValue("@itemKey", row.Cells["colItemKey"].Value);
                                                    cmd.Parameters.AddWithValue("@maxTranKey", maxTranKey);
                                                    cmd.Parameters.AddWithValue("@rowNo", row.Index);
                                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                                    {
                                                        if (reader.Read())
                                                        {
                                                            itemTranKey = Convert.ToInt32(reader["ItmTrnKy"]);
                                                        }
                                                        else
                                                        {
                                                            itemTranKey = 0;
                                                        }
                                                    }
                                                }
                                                Console.WriteLine("run 5.2");

                                                query = "Insert Into ItmTrnCd(ItmTrnKy,ITCChar,ITCNo,ItcDt) " +
                                                        "Values(@itemTranKey, @batchNo, @quantity, @expireDate)";

                                                using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                {
                                                    cmd.Parameters.AddWithValue("@itemTranKey", itemTranKey);
                                                    var batchNoValue = row.Cells["colBatchNo"].Value?.ToString();
                                                    cmd.Parameters.AddWithValue("@batchNo", string.IsNullOrWhiteSpace(batchNoValue) ? "" : batchNoValue);
                                                    cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["colQuantity"].Value));

                                                    object expireDateObj = row.Cells["colExpireDate"].Value;
                                                    DateTime expireDate;
                                                    if (expireDateObj == null || expireDateObj == DBNull.Value || string.IsNullOrWhiteSpace(expireDateObj.ToString()))
                                                    {
                                                        expireDate = new DateTime(2001, 2, 4);
                                                    }
                                                    else
                                                    {
                                                        if (!DateTime.TryParse(expireDateObj.ToString(), out expireDate) || expireDate < new DateTime(1753, 1, 1))
                                                        {
                                                            expireDate = new DateTime(2001, 2, 4);
                                                        }
                                                    }
                                                    cmd.Parameters.AddWithValue("@expireDate", expireDate);

                                                    cmd.ExecuteNonQuery();
                                                }
                                                Console.WriteLine("run 5.3");

                                                if (row.Cells["colTranPrice"].Value?.ToString() != "0")
                                                {
                                                    if (Convert.ToDecimal(row.Cells["colTranPrice"].Value) > 0)
                                                    {
                                                        query = "Update ItmMas Set CosPri = @costPrice, SlsPri = @salesPrice Where ItmKy = @itemKey";
                                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                        {
                                                            cmd.Parameters.AddWithValue("@costPrice", Convert.ToDecimal(row.Cells["colCostPrice"].Value ?? 0));
                                                            cmd.Parameters.AddWithValue("@salesPrice", Convert.ToDecimal(row.Cells["colSalePrice"].Value ?? 0));
                                                            cmd.Parameters.AddWithValue("@itemKey", Convert.ToInt32(row.Cells["colItemKey"].Value?.ToString()));

                                                            cmd.ExecuteNonQuery();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        query = "Update ItmMas Set CosPri = @costPrice Where ItmKy = @itemKey";
                                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                        {
                                                            cmd.Parameters.AddWithValue("@costPrice", Convert.ToDecimal(row.Cells["colCostPrice"].Value ?? 0));
                                                            cmd.Parameters.AddWithValue("@itemKey", Convert.ToInt32(row.Cells["colItemKey"].Value?.ToString()));

                                                            cmd.ExecuteNonQuery();
                                                        }
                                                    }
                                                    Console.WriteLine("run 5.4");

                                                }
                                            }
                                            else
                                            {
                                                query = "Update ItmTrn Set ItmKy = @itemKey, Qty = @quantity, SlsPri = @salesPrice, TrnPri = @tranPrice, CosPri = @costPrice, LocKy = @locationKey, LiNo = @rowNo Where ItmTrnKy = @itemKey";

                                                using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                {
                                                    cmd.Parameters.AddWithValue("@itemKey", Convert.ToInt32(row.Cells["colItemKey"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["colQuantity"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@salesPrice", Convert.ToDecimal(row.Cells["colSalePrice"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@tranPrice", Convert.ToDecimal(row.Cells["colTranPrice"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@costPrice", Convert.ToDecimal(row.Cells["colCostPrice"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@locationKey", locationKey);
                                                    cmd.Parameters.AddWithValue("@rowNo", row.Index);

                                                    cmd.ExecuteNonQuery();
                                                }
                                                Console.WriteLine("run 5.5");

                                                query = "Update ItmTrnCd Set ItcChar = @batchNo, ItcNo = @quantity, ItcDt = @expireDate Where ItmTrnKy = @itemTranKey";

                                                using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                {
                                                    var batchNoValue = row.Cells["colBatchNo"].Value?.ToString();
                                                    cmd.Parameters.AddWithValue("@batchNo", string.IsNullOrWhiteSpace(batchNoValue) ? "" : batchNoValue);
                                                    cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["colQuantity"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@itemTranKey", Convert.ToInt32(row.Cells["colItemTranKey"].Value ?? 0));

                                                    object expireDateObj = row.Cells["colExpireDate"].Value;
                                                    DateTime expireDate;
                                                    if (expireDateObj == null || expireDateObj == DBNull.Value || string.IsNullOrWhiteSpace(expireDateObj.ToString()))
                                                    {
                                                        expireDate = new DateTime(2001, 2, 4);
                                                    }
                                                    else
                                                    {
                                                        if (!DateTime.TryParse(expireDateObj.ToString(), out expireDate) || expireDate < new DateTime(1753, 1, 1))
                                                        {
                                                            expireDate = new DateTime(2001, 2, 4);
                                                        }
                                                    }
                                                    cmd.Parameters.AddWithValue("@expireDate", expireDate);

                                                    cmd.ExecuteNonQuery();
                                                    Console.WriteLine("run 5.6");

                                                }
                                            }

                                            if (itemBatchKey == 0)
                                            {
                                                query = "Insert Into ItmBatch(ItmKy,BatchNo,ExpirDt,CosPri,Qty,SalePri) " +
                                                        "Values(@itemKey, @batchNo, @expireDate, @costPrice, @quantity, @salePrice) ";

                                                using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                {
                                                    cmd.Parameters.AddWithValue("@itemKey", Convert.ToInt32(row.Cells["colItemKey"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@costPrice", Convert.ToDecimal(row.Cells["colCostPrice"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["colQuantity"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@salePrice", Convert.ToDecimal(row.Cells["colSalePrice"].Value ?? 0));
                                                    var batchNoValue = row.Cells["colBatchNo"].Value?.ToString();
                                                    cmd.Parameters.AddWithValue("@batchNo", string.IsNullOrWhiteSpace(batchNoValue) ? "" : batchNoValue);

                                                    object expireDateObj = row.Cells["colExpireDate"].Value;
                                                    DateTime expireDate;
                                                    if (expireDateObj == null || expireDateObj == DBNull.Value || string.IsNullOrWhiteSpace(expireDateObj.ToString()))
                                                    {
                                                        expireDate = new DateTime(2001, 2, 4);
                                                    }
                                                    else
                                                    {
                                                        if (!DateTime.TryParse(expireDateObj.ToString(), out expireDate) || expireDate < new DateTime(1753, 1, 1))
                                                        {
                                                            expireDate = new DateTime(2001, 2, 4);
                                                        }
                                                    }
                                                    cmd.Parameters.AddWithValue("@expireDate", expireDate);

                                                    cmd.ExecuteNonQuery();
                                                    Console.WriteLine("run 5.7");

                                                }
                                            }
                                            else
                                            {
                                                query = "Update ItmBatch Set BatchNo = @batchNo, Qty = @quantity, CosPri = @costPrice, SalePri = @salePrice Where ItmBatchKy = @itemBatchKey";

                                                using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                                {
                                                    var batchNoValue = row.Cells["colBatchNo"].Value?.ToString();
                                                    cmd.Parameters.AddWithValue("@batchNo", string.IsNullOrWhiteSpace(batchNoValue) ? "" : batchNoValue); 
                                                    cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(row.Cells["colQuantity"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@costPrice", Convert.ToDecimal(row.Cells["colCostPrice"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@salePrice", Convert.ToDecimal(row.Cells["colSalePrice"].Value ?? 0));
                                                    cmd.Parameters.AddWithValue("@itemBatchKey", itemBatchKey);

                                                    cmd.ExecuteNonQuery();
                                                    Console.WriteLine("run 5.8");

                                                }
                                            }
                                        }
                                        Console.WriteLine("Run 6");

                                    }
                                    else
                                    {
                                        query = "Delete from ItmTrn Where ItmTrnKy = @itemTranKey";

                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@itemTranKey", Convert.ToDecimal(row.Cells["colItemTranKey"].Value ?? 0));

                                            cmd.ExecuteNonQuery();
                                        }


                                        query = "Delete from ItmTrnCd Where ItmTrnKy = @itemTranKey";

                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@itemTranKey", Convert.ToDecimal(row.Cells["colItemTranKey"].Value ?? 0));

                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.WriteLine("Run 7");

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


                        DatabaseFunctions.PostAccTrn(maxTranKey, Convert.ToInt32(cmbSupplierCode.SelectedValue), 1, "", -amount, conn: conn, tran:tran);
                        Console.WriteLine("Run 8.1");

                        int purchaseAccKey;
                        purchaseAccKey = GetPurchaseAccKey();
                        Console.WriteLine("Run 8.2");

                        decimal purAmount = amount;
                        DatabaseFunctions.PostAccTrn(maxTranKey, purchaseAccKey, 2, "", purAmount, conn: conn, tran:tran);
                        Console.WriteLine("Run 9");

                    }

                    tran.Commit();
                    inTransaction = false;

                    MessageBox.Show("Saved");
                    ShowGrnReport(Convert.ToInt32(cmbGrnNo.Text));

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

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvGrn_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(cmbLocation.Text))
            {
                MessageBox.Show("Select location first.");
                cmbLocation.Focus();
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var frmRecievItemsPreview = new frmReceieveItemsPreview();
            frmRecievItemsPreview.Show();
        }

        private void dgvGrn_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvGrn.Columns[e.ColumnIndex].Name == "colItemCode")
                {
                    var cellValue = dgvGrn.Rows[e.RowIndex].Cells["colItemCode"].Value;

                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        dgvGrn.CurrentCell = dgvGrn.Rows[e.RowIndex].Cells["colItemName"];
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
                                cmd.Parameters.AddWithValue("@itemCode", dgvGrn.Rows[e.RowIndex].Cells["colItemCode"].Value.ToString());
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int rowIndex = dgvGrn.CurrentRow.Index;

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
                                        dgvGrn.Rows[rowIndex].Cells["colItemKey"].Value = Convert.ToInt32(reader["ItmKy"]);
                                        dgvGrn.Rows[rowIndex].Cells["colItemCode"].Value = reader["ItmCd"].ToString();
                                        dgvGrn.Rows[rowIndex].Cells["colItemName"].Value = itemNm;
                                        dgvGrn.Rows[rowIndex].Cells["colUnit"].Value = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "";
                                        dgvGrn.Rows[rowIndex].Cells["colCostPrice"].Value = reader["CosPri"] != DBNull.Value ? Convert.ToDecimal(reader["CosPri"]) : 0;
                                        dgvGrn.Rows[rowIndex].Cells["colTranPrice"].Value = reader["CosPri"] != DBNull.Value ? Convert.ToDecimal(reader["CosPri"]) : 0;

                                        decimal tranPrice = 0, quantity = 0;
                                        object tranPriceVal = dgvGrn.Rows[rowIndex].Cells["colTranPrice"].Value;
                                        object qtyVal = dgvGrn.Rows[rowIndex].Cells["colQuantity"].Value;

                                        decimal.TryParse(tranPriceVal?.ToString(), out tranPrice);
                                        decimal.TryParse(qtyVal?.ToString(), out quantity);

                                        dgvGrn.Rows[rowIndex].Cells["colAmount"].Value = tranPrice * quantity;
                                    }
                                }
                            }
                        }
                    }

                    //get locationKey
                    int locationKey = 0;
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "SELECT LocKy From vewLocCd WHERE LocCd = @locationCode";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@locationCode", cmbLocation.Text.Trim());
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                locationKey = Convert.ToInt32(reader["LocKy"].ToString());
                            }
                        }
                    }

                    dgvGrn.Rows[e.RowIndex].Cells["colInhand"].Value = GetItemQuantity(Convert.ToInt32(dgvGrn.Rows[e.RowIndex].Cells["colItemKey"].Value), locationKey);
                }

                if (dgvGrn.Columns[e.ColumnIndex].Name == "colItemName")
                {
                    var cellValue = dgvGrn.Rows[e.RowIndex].Cells["colItemName"].Value;

                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        dgvGrn.CurrentCell = dgvGrn.Rows[e.RowIndex].Cells["colItemCode"];
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
                                object val = dgvGrn.Rows[e.RowIndex].Cells["colItemName"].Value;
                                if (val == null || !int.TryParse(val.ToString(), out int itmKy))
                                    return;

                                cmd.Parameters.AddWithValue("@itemKey", itmKy);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int rowIndex = dgvGrn.CurrentRow.Index;

                                        dgvGrn.Rows[rowIndex].Cells["colItemKey"].Value = Convert.ToInt32(reader["ItmKy"]);
                                        dgvGrn.Rows[rowIndex].Cells["colItemCode"].Value = reader["ItmCd"].ToString();
                                        dgvGrn.Rows[rowIndex].Cells["colUnit"].Value = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "";
                                        dgvGrn.Rows[rowIndex].Cells["colCostPrice"].Value = reader["CosPri"] != DBNull.Value ? Convert.ToDecimal(reader["CosPri"]) : 0;
                                        dgvGrn.Rows[rowIndex].Cells["colTranPrice"].Value = reader["CosPri"] != DBNull.Value ? Convert.ToDecimal(reader["CosPri"]) : 0;

                                        decimal tranPrice = 0, quantity = 0;
                                        object tranPriceVal = dgvGrn.Rows[rowIndex].Cells["colTranPrice"].Value;
                                        object qtyVal = dgvGrn.Rows[rowIndex].Cells["colQuantity"].Value;

                                        decimal.TryParse(tranPriceVal?.ToString(), out tranPrice);
                                        decimal.TryParse(qtyVal?.ToString(), out quantity);

                                        dgvGrn.Rows[rowIndex].Cells["colAmount"].Value = tranPrice * quantity;
                                    }
                                }
                            }
                        }
                    }

                    //get locationKey
                    int locationKey = 0;
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string query = "SELECT LocKy From vewLocCd WHERE LocCd = @locationCode";

                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@locationCode", cmbLocation.Text.Trim());
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                locationKey = Convert.ToInt32(reader["LocKy"].ToString());
                            }
                        }
                    }

                    dgvGrn.Rows[e.RowIndex].Cells["colInhand"].Value = GetItemQuantity(Convert.ToInt32(dgvGrn.Rows[e.RowIndex].Cells["colItemKey"].Value), locationKey);
                }

                if (dgvGrn.Columns[e.ColumnIndex].Name == "colTranPrice")
                {
                    if (!string.IsNullOrWhiteSpace(dgvGrn.Rows[e.RowIndex].Cells["colTranPrice"].Value.ToString()))
                    {
                        dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colAmount"].Value = Convert.ToDecimal(dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colTranPrice"].Value) * Convert.ToDecimal(dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colQuantity"].Value);
                    }
                }

                if (dgvGrn.Columns[e.ColumnIndex].Name == "colCostPrice")
                {
                    if (!string.IsNullOrWhiteSpace(dgvGrn.Rows[e.RowIndex].Cells["colCostPrice"].Value.ToString()))
                    {
                        //skip this cell
                    }
                }

                if (dgvGrn.Columns[e.ColumnIndex].Name == "colQuantity")
                {
                    if (!string.IsNullOrWhiteSpace(dgvGrn.Rows[e.RowIndex].Cells["colQuantity"].Value.ToString()))
                    {
                        dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colSave"].Value = 1;
                        dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colAmount"].Value = Convert.ToDecimal(dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colTranPrice"].Value) * Convert.ToDecimal(dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colQuantity"].Value);
                    }
                }

                //if (dgvGrn.Columns[e.ColumnIndex].Name == "colExpireDate")
                //{
                //    if (!string.IsNullOrWhiteSpace(dgvGrn.Rows[e.RowIndex].Cells["colExpireDate"].Value.ToString()))
                //    {
                //        //skip this cell
                //    }
                //}

                if (dgvGrn.Columns[e.ColumnIndex].Name == "colAmount")
                {
                    if (!string.IsNullOrWhiteSpace(dgvGrn.Rows[e.RowIndex].Cells["colAmount"].Value.ToString()))
                    {
                        dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colTranPrice"].Value = Convert.ToDecimal(dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colAmount"].Value) / Convert.ToDecimal(dgvGrn.Rows[dgvGrn.CurrentRow.Index].Cells["colQuantity"].Value);
                    }
                }

                dgvGrn.Rows[e.RowIndex].Cells["colUpdate"].Value = 1;

                txtTotalAmount.Text = CalculateTotalAmount().ToString("#, 0.00");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvGrn_KeyDown(object sender, KeyEventArgs e)
        {
            int rowIndex = dgvGrn.CurrentCell.RowIndex;
            int columnIndex = dgvGrn.CurrentCell.ColumnIndex;

            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Do you really want to DELETE the current row ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    dgvGrn.Rows[rowIndex].Cells["colUpdate"].Value = 1;
                    dgvGrn.Rows[rowIndex].Cells["colDelete"].Value = 1;
                    dgvGrn.Rows[rowIndex].Visible = false;
                }
            }
            else if(e.KeyCode == Keys.Back)
            {
                if (dgvGrn.Columns[columnIndex].Name == "colExpireDate")
                {
                    dgvGrn.CurrentCell.Value = "";
                }
            }
        }

    }
}
