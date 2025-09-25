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
using POS_System.Modules;

namespace POS_System.Forms
{
    public partial class frmStockDeductions : Form
    {
        private int childFormNumber = 0;

        public frmStockDeductions()
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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        private bool formLoaded;
        private int formMode;
        private int invoiceNo;
        DataTable itemNamesTable;
        long transactionKey;
        string locationKey;
        decimal inHand;

        public static void EnsureOpen(SqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        private void ProcUpdate()
        {
            if (!UserSession.Instance.GetPermission("mnuStkDed").CanUpdate)
            {
                MessageBox.Show("Access is denied");
            }
            else
            {
                formMode = (int)FormMode.UPDATE;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void ProcNew()
        {
            formMode = (int)FormMode.NEW;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;

            invoiceNo = DatabaseFunctions.GetTrnNoList(1, 0, dtpDate.Value, "STKDED") + 1;
            cmbTrnNo.Text = invoiceNo.ToString();
            cmbLocation.Text = "";
            txtDescription.Text = "";
            dgvStockDeduction.Rows.Clear();
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

        private void GetQty(long itemKey)
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmdInhand = new SqlCommand("SELECT Qty FROM vewStkBalLocWise WHERE ItmKy = @itemKey", conn))
                {
                    cmdInhand.Parameters.AddWithValue("@itemKey", itemKey);
                    SqlDataReader readerInHand = cmdInhand.ExecuteReader();
                    if (readerInHand.Read())
                    {
                        inHand = Convert.ToDecimal(readerInHand["Qty"]);
                    }
                    readerInHand.Close();
                }
            }
        }

        private void GetData()
        {
            if (!string.IsNullOrWhiteSpace(cmbTrnNo.Text))
            {
                invoiceNo = Convert.ToInt32(cmbTrnNo.Text);
                if (formMode == (int)FormMode.UPDATE)
                {
                    try
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();

                            // 1. Load Header
                            string query = "SELECT LocKy, TrnDt, Des, TrnKy FROM vewStkDedHdr WHERE TrnNo = @transactionNo AND CKy = @companyKey";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@transactionNo", cmbTrnNo.Text);
                            cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    locationKey = reader["LocKy"].ToString();
                                    dtpDate.Text = Convert.ToDateTime(reader["TrnDt"]).ToString("yyyy-MM-dd");
                                    txtDescription.Text = reader["Des"] != DBNull.Value ? reader["Des"].ToString() : "";
                                    transactionKey = Convert.ToInt64(reader["TrnKy"]);
                                }
                            }
                            // 2. Load Location Name
                            string queryLocNm = "SELECT LocCd FROM vewLocCd WHERE LocKy = @locationKey";
                            SqlCommand cmdLocNm = new SqlCommand(queryLocNm, conn);
                            cmdLocNm.Parameters.AddWithValue("@locationKey", locationKey);

                            using (SqlDataReader locReader = cmdLocNm.ExecuteReader())
                            {
                                if (locReader.Read())
                                {
                                    cmbLocation.Text = locReader["LocCd"].ToString();
                                }
                            }

                            // 3. Load Grid Details + InHand
                            string queryDgvLoad = @"
                                                SELECT ItmKy, ItmCd, ItmNm, Unit, CosPri, SlsPri, TrnPri, Qty, ItmTrnKy, 0 AS updt, 0 AS Del 
                                                FROM vewStkDedDtls 
                                                WHERE TrnKy = @transactionKey";
                            SqlCommand cmdDgvLoad = new SqlCommand(queryDgvLoad, conn);
                            cmdDgvLoad.Parameters.AddWithValue("@transactionKey", transactionKey);

                            using (SqlDataReader readerDgvLoad = cmdDgvLoad.ExecuteReader())
                            {
                                dgvStockDeduction.Rows.Clear();

                                while (readerDgvLoad.Read())
                                {
                                    long itemKey = Convert.ToInt64(readerDgvLoad["ItmKy"]);

                                    string itemName = readerDgvLoad["ItmNm"] == DBNull.Value ? null : readerDgvLoad["ItmNm"].ToString();
                                    object itemNameKey = DBNull.Value;

                                    if (!string.IsNullOrEmpty(itemName))
                                    {
                                        DataRow[] matchingItemRow = itemNamesTable.Select($"ItmNm = '{itemName.Replace("'", "''")}'");
                                        if (matchingItemRow.Length > 0)
                                        {
                                            itemNameKey = matchingItemRow[0]["ItmKy"];
                                        }
                                    }

                                    GetQty(itemKey);
                                    dgvStockDeduction.Rows.Add(
                                        itemKey,
                                        readerDgvLoad["ItmCd"].ToString(),
                                        itemNameKey,
                                        readerDgvLoad["Unit"].ToString(),
                                        Convert.ToDecimal(readerDgvLoad["CosPri"]),
                                        Convert.ToDecimal(readerDgvLoad["SlsPri"]),
                                        Convert.ToDecimal(readerDgvLoad["TrnPri"]),
                                        Convert.ToDecimal(readerDgvLoad["Qty"]) * -1,
                                        readerDgvLoad["ItmTrnKy"].ToString(),
                                        0, // updt
                                        0, // Del
                                        inHand
                                    );
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            reportStockAddition.SetParameterValue("caption", "Stock Deduction");

            frmReportViewer frmReportViewer = new frmReportViewer();
            frmReportViewer.LoadStockAdditionReport(reportStockAddition);
            frmReportViewer.Show();
        }

        private void LoadTrnNo()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT TrnNo From vewTrnNo Where OurCd='STKDED' And CKy = 1 Order By TrnNo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbTrnNo.Items.Add(reader["TrnNo"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadLocation()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT LocKy,LocCd From vewLocCd Order By LocCd";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbLocation.Items.Add(reader["LocCd"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmStockDeduction_Load(object sender, EventArgs e)
        {
            if (!UserSession.Instance.GetPermission("mnuStkDed").CanAccess)
            {
                MessageBox.Show("Access is denied");
                this.Close();
            }
            else
            {
                this.KeyPreview = true;

                LoadItemNameComboBox();
                SetupDgvStockAddition();
                LoadTrnNo();
                LoadLocation();
                formLoaded = false;
            }
        }

        private void SetupDgvStockAddition()
        {
            dgvStockDeduction.Columns.Clear();

            DataGridViewTextBoxColumn colItemKey = new DataGridViewTextBoxColumn();
            colItemKey.Name = "itemKey";
            colItemKey.HeaderText = "Item Key";
            colItemKey.Visible = false;
            dgvStockDeduction.Columns.Add(colItemKey);

            DataGridViewTextBoxColumn colItemCode = new DataGridViewTextBoxColumn();
            colItemCode.Name = "itemCode";
            colItemCode.HeaderText = "Item Code";
            colItemCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colItemCode.Width = 120;
            dgvStockDeduction.Columns.Add(colItemCode);

            DataGridViewComboBoxColumn colItemName = new DataGridViewComboBoxColumn();
            colItemName.Name = "itemName";
            colItemName.HeaderText = "Item Name";
            colItemName.DataSource = itemNamesTable;
            colItemName.DisplayMember = "ItmNm";
            colItemName.ValueMember = "ItmKy";
            colItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colItemName.Width = 340;
            dgvStockDeduction.Columns.Add(colItemName);

            DataGridViewTextBoxColumn colUnit = new DataGridViewTextBoxColumn();
            colUnit.Name = "unit";
            colUnit.HeaderText = "Unit";
            colUnit.ReadOnly = true;
            colUnit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colUnit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colUnit.Width = 40;
            dgvStockDeduction.Columns.Add(colUnit);

            DataGridViewTextBoxColumn colCostPrice = new DataGridViewTextBoxColumn();
            colCostPrice.Name = "costPrice";
            colCostPrice.HeaderText = "Cost Price";
            colCostPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCostPrice.DefaultCellStyle.Format = "n2";
            colCostPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCostPrice.Width = 90;
            dgvStockDeduction.Columns.Add(colCostPrice);

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Name = "col6";
            col6.HeaderText = "col6";
            col6.Visible = false;
            dgvStockDeduction.Columns.Add(col6);

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.Name = "col7";
            col7.HeaderText = "col7";
            col7.Visible = false;
            dgvStockDeduction.Columns.Add(col7);

            DataGridViewTextBoxColumn colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.Name = "quantity";
            colQuantity.HeaderText = "Quantity";
            colQuantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colQuantity.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colQuantity.Width = 50;
            dgvStockDeduction.Columns.Add(colQuantity);

            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
            col9.Name = "col9";
            col9.HeaderText = "col9";
            col9.Visible = false;
            dgvStockDeduction.Columns.Add(col9);

            DataGridViewTextBoxColumn col10 = new DataGridViewTextBoxColumn();
            col10.Name = "col10";
            col10.HeaderText = "col10";
            col10.Visible = false;
            dgvStockDeduction.Columns.Add(col10);

            DataGridViewTextBoxColumn colUpdate = new DataGridViewTextBoxColumn();
            colUpdate.Name = "update";
            colUpdate.HeaderText = "Update";
            colUpdate.Visible = false;
            dgvStockDeduction.Columns.Add(colUpdate);

            DataGridViewTextBoxColumn colDelete = new DataGridViewTextBoxColumn();
            colDelete.Name = "delete";
            colDelete.HeaderText = "Delete";
            colDelete.Visible = false;
            dgvStockDeduction.Columns.Add(colDelete);

            DataGridViewTextBoxColumn colInHand = new DataGridViewTextBoxColumn();
            colInHand.Name = "inHand";
            colInHand.HeaderText = "InHand";
            dgvStockDeduction.Columns.Add(colInHand);

            foreach (DataGridViewColumn col in dgvStockDeduction.Columns)
            {
                if (col is DataGridViewComboBoxColumn)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                else
                {
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }

            dgvStockDeduction.ColumnHeadersDefaultCellStyle.Font = new Font(dgvStockDeduction.Font, FontStyle.Bold);
        }

        private void cmbTrnNo_Enter(object sender, EventArgs e)
        {
            cmbTrnNo.Tag = cmbTrnNo.Text;
        }

        private void frmStockDeductioion_Activated(object sender, EventArgs e)
        {
            if (!formLoaded)
            {
                if (UserSession.Instance.GetPermission("mnuStkDed").CanCreateNew)
                {
                    formMode = (int)FormMode.NEW;
                    ProcNew();
                }
                else if (UserSession.Instance.GetPermission("mnuStkDed").CanUpdate)
                {
                    formMode = (int)FormMode.UPDATE;
                    ProcUpdate();
                }
                else
                {
                    formMode = (int)FormMode.VIEW;
                }

                formLoaded = true;
            }
        }

        private void cmbTrnNo_Leave(object sender, EventArgs e)
        {
            if (cmbTrnNo.Text != "" && cmbTrnNo.Tag.ToString() != cmbTrnNo.Text)
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    string query = "Select TrnKy from VewTrnNo Where TrnNo = @transactionNo And OurCd = 'STKDED' And CKy = @companyKey";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@transactionNo", cmbTrnNo.Text);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (UserSession.Instance.GetPermission("mnuStkDed").CanUpdate)
                    {
                        if (reader.Read())
                        {
                            ProcUpdate();
                            GetData();
                            btnAdd.Enabled = true;
                        }
                        //else
                        //{
                        //    if (UserSession.Instance.GetPermission("mnuStkDed").CanCreateNew)
                        //    {
                        //        DialogResult result = MessageBox.Show("Transaction No. not exist" + "\r\n" + "Would you like to enter new Stock Addition ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //        if (result == DialogResult.Yes)
                        //        {
                        //            ProcNew();

                        //            dgvStockDeduction.Rows.Clear();
                        //            cmbLocation.Text = "";
                        //            txtDescription.Text = "";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Sorry, You don't have enough Privileges to enter new transactions");
                        //    }
                        //}
                    }
                    else
                    {
                        //MessageBox.Show("Sorry, You don't have enough Privileges to update transactions");
                    }
                    reader.Close();
                }
            }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbLocation.Text != "")
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT LocKy from vewLocCd WHERE LocCd = @locationCode", conn);
                        cmd.Parameters.AddWithValue("@locationCode", cmbLocation.Text.Trim());
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                locationKey = reader["LocKy"].ToString();
                            }
                        }
                    }
                }
                dtpDate.Focus();
            }
        }

        private void cmbTrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbLocation.Focus();
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescription.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                cmbLocation.Text = "";
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvStockDeduction.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (UserSession.Instance.GetPermission("mnuStkDed").CanCreateNew)
            {
                if (dgvStockDeduction.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Transaction No. not exist" + "\r\n" + "Would you like to enter new Stock Deduction ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        ProcNew();
                    }
                }
                else
                {
                    ProcNew();
                }
            }
            else
            {
                MessageBox.Show("Access is denied");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool inTransaction = false;

            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    if (!UserSession.Instance.GetPermission("mnuStkDed").CanDelete)
                    {
                        MessageBox.Show("Access is denied");
                        return;
                    }

                    if (!DatabaseFunctions.isValidTrnDate(1, "STKDED", dtpDate.Value, conn))
                    {
                        MessageBox.Show("You cannot delete transactions for this date");
                        return;
                    }

                    EnsureOpen(conn);
                    SqlCommand cmd = new SqlCommand("Select TrnKy from vewTrnNo Where OurCd='STKDED' And TrnNo = @trnNo And CKy = @companyKey", conn);
                    cmd.Parameters.AddWithValue("@TrnNo", cmbTrnNo.Text);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            transactionKey = Convert.ToInt32(reader["TrnKy"]);
                        }
                        else
                        {
                            MessageBox.Show("Invalid trnsaction No.");
                            return;
                        }
                    }
                    SqlTransaction tran = conn.BeginTransaction();
                    inTransaction = true;
                    try
                    {
                        using (SqlCommand cmdDelete = new SqlCommand("Delete From ItmTrn Where TrnKy = @transactionKey", conn, tran))
                        {
                            cmdDelete.Parameters.AddWithValue("@transactionKey", transactionKey);
                            cmdDelete.ExecuteNonQuery();
                        }
                        using (SqlCommand cmdUpdate = new SqlCommand("Update TrnMas Set fInAct=1 Where TrnKy = @transactionKey", conn, tran))
                        {
                            cmdUpdate.Parameters.AddWithValue("@transactionKey", transactionKey);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        tran.Commit();
                        inTransaction = false;
                        MessageBox.Show("Deleted");
                        cmbTrnNo.Items.Clear();
                        cmbLocation.Items.Clear();
                        dtpDate.Value = DateTime.Now.Date;
                        txtDescription.Text = "";
                        dgvStockDeduction.Rows.Clear();
                        frmStockDeduction_Load(this, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        if (inTransaction)
                        {
                            tran.Rollback();
                        }
                        MessageBox.Show(ex.Message);
                    }

                    if (!UserSession.Instance.GetPermission("mnuStkDed").CanCreateNew)
                    {
                        cmbLocation.Text = "";
                        txtDescription.Text = "";
                        dgvStockDeduction.Rows.Clear();
                        return;
                    }
                    else
                    {
                        ProcNew();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool inTransaction = false;
            //string locOutKey;
            SqlTransaction tran = null;

            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    Cursor.Current = Cursors.WaitCursor;

                    if (string.IsNullOrWhiteSpace(cmbLocation.Text))
                    {
                        MessageBox.Show("Loacation is a must");
                        cmbLocation.Focus();
                        return;
                    }
                    else
                    {
                        locationKey = cmbLocation.Text;
                    }

                    if (!DatabaseFunctions.isValidTrnDate(1, "STKDED", dtpDate.Value, conn))
                    {
                        MessageBox.Show("You cannot enter/alter transactions for this date");
                        return;
                    }

                    tran = conn.BeginTransaction();
                    inTransaction = true;

                    int trnNo;
                    int trnOutKey = 0;
                    int trnTypeOutKey = DatabaseFunctions.GetTrnTypKy(UserSession.Instance.CompanyKey, "STKDED");
                    //int locKey = 0;
                    //using (conn)
                    //{
                    //    string query = "SELECT LocKy From vewLocCd WHERE LocCd = @LocCd";
                    //    SqlCommand cmd = new SqlCommand(query, conn);
                    //    cmd.Parameters.AddWithValue("@LocCd", cmbLocation.Text);
                    //    using (SqlDataReader reader = cmd.ExecuteReader())
                    //    {
                    //        if(reader.Read())
                    //        {
                    //            locKey = Convert.ToInt32(reader["LocKy"]);
                    //        }
                    //    }
                    //}

                    if (formMode == (int)FormMode.NEW)
                    {
                        trnNo = DatabaseFunctions.GetTrnNoLstSave(1, 0, dtpDate.Value, "STKDED", conn, tran);
                        string insertSql = @"INSERT INTO TrnMas 
                                    (TrnNo, OurCd, TrnTypKy, TrnDt, LocKy, fApr, Des, EntUsrKy, EntDtm)
                                     VALUES (@TrnNo, 'STKDED', @TrnTypKy, @TrnDt, @LocKy, 1, @Des, @EntUsrKy, GETDATE())";

                        using (SqlCommand cmdInsert = new SqlCommand(insertSql, conn, tran))
                        {
                            cmdInsert.Parameters.AddWithValue("@TrnNo", trnNo);
                            cmdInsert.Parameters.AddWithValue("@TrnTypKy", trnTypeOutKey);
                            cmdInsert.Parameters.AddWithValue("@TrnDt", dtpDate.Value.Date);
                            cmdInsert.Parameters.AddWithValue("@LocKy", 276);
                            cmdInsert.Parameters.AddWithValue("@Des", txtDescription.Text);
                            cmdInsert.Parameters.AddWithValue("@EntUsrKy", UserSession.Instance.UserKey);

                            cmdInsert.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("SELECT TrnKy FROM vewTrnNo WHERE OurCd = @OurCd AND TrnNo = @TrnNo AND CKy = @CKy", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@OurCd", "STKDED");
                            cmd.Parameters.AddWithValue("@TrnNo", trnNo);
                            cmd.Parameters.AddWithValue("@CKy", 1);

                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                trnOutKey = Convert.ToInt32(result);
                            }
                        }
                    }

                    else if (formMode == (int)FormMode.UPDATE)
                    {
                        for (int i = dgvStockDeduction.Rows.Count - 1; i >= 0; i--)
                        {
                            DataGridViewRow row = dgvStockDeduction.Rows[i];

                            if (row.Cells["delete"].Value != null && row.Cells["delete"].Value.ToString() == "1")
                            {
                                dgvStockDeduction.Rows.RemoveAt(i);
                            }
                        }


                        using (SqlCommand cmd = new SqlCommand("SELECT TrnKy FROM vewTrnNo WHERE OurCd = @OurCd AND TrnNo = @TrnNo AND CKy = @CKy", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@OurCd", "STKDED");
                            cmd.Parameters.AddWithValue("@TrnNo", cmbTrnNo.Text);
                            cmd.Parameters.AddWithValue("@CKy", 1);
                            object result = cmd.ExecuteScalar();
                            trnOutKey = Convert.ToInt32(result);
                        }
                        string updateSql = @"UPDATE TrnMas SET TrnDt=@TrnDt, Des=@Des, LocKy=@LocKy, EntUsrKy=@EntUsrKy, EntDtm=GETDATE(), Status='U' WHERE TrnKy=@TrnKy";

                        using (SqlCommand cmdUpdate = new SqlCommand(updateSql, conn, tran))
                        {
                            cmdUpdate.Parameters.AddWithValue("@TrnDt", dtpDate.Value.Date);
                            cmdUpdate.Parameters.AddWithValue("@Des", txtDescription.Text);
                            cmdUpdate.Parameters.AddWithValue("@LocKy", 276);
                            cmdUpdate.Parameters.AddWithValue("@EntUsrKy", UserSession.Instance.UserKey);
                            cmdUpdate.Parameters.AddWithValue("@TrnKy", trnOutKey);

                            cmdUpdate.ExecuteNonQuery();
                        }
                    }

                    using (SqlCommand cmdDelete = new SqlCommand("Delete from ItmTrn Where TrnKy = @transactionKey", conn, tran))
                    {
                        cmdDelete.Parameters.AddWithValue("@transactionKey", trnOutKey);
                        cmdDelete.ExecuteNonQuery();
                    }

                    foreach (DataGridViewRow row in dgvStockDeduction.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int itemKey = Convert.ToInt32(row.Cells["itemKey"].Value);
                        decimal qty = Convert.ToDecimal(row.Cells["quantity"].Value);
                        decimal costPri = Convert.ToDecimal(row.Cells["costPrice"].Value);
                        decimal salePri = Convert.ToDecimal(row.Cells["col6"].Value);

                        string insertDetailSql = @"INSERT INTO ItmTrn(TrnKy, ItmKy, Qty, CosPri, SlsPri, TrnPri, LocKy, LiNo)
                                           VALUES (@TrnKy, @ItmKy, @Qty, @CosPri, @SlsPri, @TrnPri, @LocKy, @LiNo)";
                        using (SqlCommand cmdInsertDetail = new SqlCommand(insertDetailSql, conn, tran))
                        {
                            cmdInsertDetail.Parameters.AddWithValue("@TrnKy", trnOutKey);
                            cmdInsertDetail.Parameters.AddWithValue("@ItmKy", itemKey);
                            cmdInsertDetail.Parameters.AddWithValue("@Qty", qty);
                            cmdInsertDetail.Parameters.AddWithValue("@CosPri", costPri);
                            cmdInsertDetail.Parameters.AddWithValue("@SlsPri", salePri);
                            cmdInsertDetail.Parameters.AddWithValue("@TrnPri", salePri);
                            cmdInsertDetail.Parameters.AddWithValue("@LocKy", Convert.ToInt32(cmbLocation.SelectedValue));
                            cmdInsertDetail.Parameters.AddWithValue("@LiNo", row.Index + 1);

                            cmdInsertDetail.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                    MessageBox.Show("saved");
                    ShowReport(trnOutKey);
                    inTransaction = false;
                    cmbTrnNo.Items.Clear();
                    cmbLocation.Items.Clear();
                    dtpDate.Value = DateTime.Now.Date;
                    txtDescription.Text = "";
                    dgvStockDeduction.Rows.Clear();
                    frmStockDeduction_Load(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                if (inTransaction && tran != null)
                {
                    tran.Rollback();
                }
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnSave.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ExistItemCd(string itemCode, int skipIndex)
        {
            foreach (DataGridViewRow row in dgvStockDeduction.Rows)
            {
                if (row.IsNewRow || row.Index == skipIndex) continue;

                if (row.Cells["itemCode"].Value != null && row.Cells["itemCode"].Value.ToString().Equals(itemCode, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ExistItemName(string itemName, int skipIndex)
        {
            foreach (DataGridViewRow row in dgvStockDeduction.Rows)
            {
                if (row.IsNewRow || row.Index == skipIndex) continue;

                if (row.Cells["itemName"].Value != null && row.Cells["itemName"].Value.ToString().Equals(itemName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        bool suppressCellValueChanged = false;

        private void dgvStockDeduction_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (suppressCellValueChanged) return;

                DataGridViewCell cell = dgvStockDeduction.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dgvStockDeduction.Columns[e.ColumnIndex].Name == "itemCode")
                {
                    if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        return;
                    }
                    else
                    {
                        if (ExistItemCd(dgvStockDeduction.Rows[e.RowIndex].Cells["itemcode"].Value.ToString(), e.RowIndex))
                        {
                            MessageBox.Show("This Item is already entered");
                            foreach (DataGridViewCell cellClear in dgvStockDeduction.Rows[e.RowIndex].Cells)
                            {
                                cellClear.Value = null;
                            }
                            dgvStockDeduction.Rows.RemoveAt(e.RowIndex);
                        }
                        else
                        {
                            decimal inHand = 0;
                            using (SqlConnection conn = DBConnectionManager.GetConnection())
                            {
                                conn.Open();
                                using (SqlCommand cmdInhand = new SqlCommand("SELECT Qty FROM vewStkBalLocWise WHERE ItmCd = @itemCode AND LocKy = @locationKey", conn))
                                {
                                    cmdInhand.Parameters.AddWithValue("@itemCode", dgvStockDeduction.Rows[e.RowIndex].Cells["itemCode"].Value);
                                    cmdInhand.Parameters.AddWithValue("@locationKey", locationKey);
                                    SqlDataReader readerInHand = cmdInhand.ExecuteReader();
                                    if (readerInHand.Read())
                                    {
                                        inHand = readerInHand["Qty"] != DBNull.Value ? Convert.ToDecimal(readerInHand["Qty"]) : 0;
                                    }
                                    readerInHand.Close();
                                }
                            }

                            using (SqlConnection conn = DBConnectionManager.GetConnection())
                            {
                                conn.Open();
                                SqlCommand cmd = new SqlCommand("SELECT ItmKy,ItmCd,ItmNm,Unit,CosPri,SlsPri,UnitKy From vewItmMas WHERE ItmCd = @itemCode", conn);
                                cmd.Parameters.AddWithValue("@itemCode", cell.Value.ToString());
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        int rowIndex = dgvStockDeduction.CurrentRow.Index;
                                        if (reader.Read())
                                        {
                                            suppressCellValueChanged = true;
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
                                            dgvStockDeduction.Rows[rowIndex].Cells["itemKey"].Value = reader["ItmKy"] != DBNull.Value ? reader["ItmKy"].ToString() : string.Empty;
                                            dgvStockDeduction.Rows[rowIndex].Cells["itemName"].Value = itemNm;
                                            dgvStockDeduction.Rows[rowIndex].Cells["unit"].Value = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : string.Empty;
                                            dgvStockDeduction.Rows[rowIndex].Cells["costPrice"].Value = reader["CosPri"] != DBNull.Value? string.Format("{0:0.00}", Convert.ToDecimal(reader["CosPri"])): "0.00";
                                            dgvStockDeduction.Rows[rowIndex].Cells["col6"].Value = reader["SlsPri"] != DBNull.Value ? reader["SlsPri"].ToString() : "0.00";
                                            dgvStockDeduction.Rows[rowIndex].Cells["col7"].Value = reader["SlsPri"] != DBNull.Value ? reader["SlsPri"].ToString() : "0.00";
                                            dgvStockDeduction.Rows[rowIndex].Cells["inHand"].Value = inHand;
                                            dgvStockDeduction.Rows[rowIndex].Cells["update"].Value = 1;
                                            suppressCellValueChanged = false;
                                        }

                                        dgvStockDeduction.CurrentCell = dgvStockDeduction.Rows[rowIndex].Cells["quantity"];
                                        dgvStockDeduction.BeginEdit(true);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid Item !");
                                        foreach (DataGridViewCell cellClear in dgvStockDeduction.Rows[e.RowIndex].Cells)
                                        {
                                            cellClear.Value = null;
                                        }
                                        dgvStockDeduction.Rows.RemoveAt(e.RowIndex);

                                        dgvStockDeduction.CurrentCell = dgvStockDeduction.Rows[dgvStockDeduction.CurrentRow.Index].Cells["itemCode"];
                                        dgvStockDeduction.BeginEdit(true);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (dgvStockDeduction.Columns[e.ColumnIndex].Name == "itemName")
                {
                    object val = dgvStockDeduction.Rows[e.RowIndex].Cells["itemName"].Value;
                    if (val == null || val == DBNull.Value)
                        return;

                    int itmKy = Convert.ToInt32(val); // safely convert to int

                    if (ExistItemName(itmKy.ToString(), e.RowIndex))
                    {
                        MessageBox.Show("This Item is already entered");
                        foreach (DataGridViewCell cellClear in dgvStockDeduction.Rows[e.RowIndex].Cells)
                            cellClear.Value = null;
                        dgvStockDeduction.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        decimal inHand = 0;
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            using (SqlCommand cmdInhand = new SqlCommand("SELECT Qty FROM vewStkBalLocWise WHERE ItmKy = @itmKy AND LocKy = @locationKey", conn))
                            {
                                cmdInhand.Parameters.AddWithValue("@itmKy", itmKy);
                                cmdInhand.Parameters.AddWithValue("@locationKey", locationKey);
                                SqlDataReader readerInHand = cmdInhand.ExecuteReader();
                                if (readerInHand.Read())
                                    inHand = readerInHand["Qty"] != DBNull.Value ? Convert.ToDecimal(readerInHand["Qty"]) : 0;
                                readerInHand.Close();
                            }
                        }

                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("SELECT ItmKy,ItmCd,ItmNm,Unit,CosPri,SlsPri,UnitKy FROM vewItmMas WHERE ItmKy = @itmKy", conn);
                            cmd.Parameters.AddWithValue("@itmKy", itmKy);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                int rowIndex = dgvStockDeduction.CurrentRow.Index;
                                if (reader.Read())
                                {
                                    dgvStockDeduction.Rows[rowIndex].Cells["itemKey"].Value = reader["ItmKy"] != DBNull.Value ? reader["ItmKy"].ToString() : string.Empty;
                                    dgvStockDeduction.Rows[rowIndex].Cells["itemCode"].Value = reader["ItmCd"] != DBNull.Value ? reader["ItmCd"].ToString() : string.Empty;
                                    dgvStockDeduction.Rows[rowIndex].Cells["unit"].Value = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : string.Empty;
                                    dgvStockDeduction.Rows[rowIndex].Cells["costPrice"].Value = reader["CosPri"] != DBNull.Value ? string.Format("{0:0.00}", Convert.ToDecimal(reader["CosPri"])) : "0.00";
                                    dgvStockDeduction.Rows[rowIndex].Cells["col6"].Value = reader["SlsPri"] != DBNull.Value ? reader["SlsPri"].ToString() : "0.00";
                                    dgvStockDeduction.Rows[rowIndex].Cells["col7"].Value = reader["SlsPri"] != DBNull.Value ? reader["SlsPri"].ToString() : "0.00";
                                    dgvStockDeduction.Rows[rowIndex].Cells["inHand"].Value = inHand;
                                    dgvStockDeduction.Rows[rowIndex].Cells["update"].Value = 1;
                                    suppressCellValueChanged = false;

                                    dgvStockDeduction.CurrentCell = dgvStockDeduction.Rows[rowIndex].Cells["quantity"];
                                    dgvStockDeduction.BeginEdit(true);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Item !");
                                    foreach (DataGridViewCell cellClear in dgvStockDeduction.Rows[e.RowIndex].Cells)
                                        cellClear.Value = null;
                                    dgvStockDeduction.Rows.RemoveAt(e.RowIndex);
                                    dgvStockDeduction.CurrentCell = dgvStockDeduction.Rows[dgvStockDeduction.CurrentRow.Index].Cells["itemName"];
                                    dgvStockDeduction.BeginEdit(true);
                                }
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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var frmStockAdjesment = new frmStockAdjesment();
            frmStockAdjesment.Show();
        }

        private void dgvStockDeduction_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStockDeduction.Columns[e.ColumnIndex].Name == "quantity")
            {
                string input = dgvStockDeduction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                if (!int.TryParse(input, out _))
                {
                    MessageBox.Show("Only integer values are allowed!");
                    dgvStockDeduction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
        }

        private void dgvStockDeduction_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                int row = dgvStockDeduction.CurrentCell.RowIndex;
                dgvStockDeduction.Rows[row].Visible = false;
                dgvStockDeduction.Rows[row].Cells["delete"].Value = 1;
            }
        }

        private void dgvStockDeduction_Enter(object sender, EventArgs e)
        {
            if(cmbLocation.Text == "")
            {
                MessageBox.Show("First select the location!");
                cmbLocation.Focus();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
