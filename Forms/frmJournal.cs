using Microsoft.Reporting.WinForms;
using POS_System.Classes;
using POS_System.Modules;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS_System.Forms
{
    public partial class frmJournal : Form
    {
        public frmJournal()
        {
            InitializeComponent();
        }

        //------------------------------------
        DataTable accDetails;
        DataTable buDetails;
        DataTable anlType1Details;
        DataTable anlType2Details;
        private bool frmLoaded;
        private int formMode;
        private int voucherNo;
        int TrnKey;
        string trnType = "JRNL";

        private void LoadVoucherNo()
        {
            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TrnNo FROM vewTrnNo Where Ourcd = @trnType and CKy = @companyKey Order By TrnNo", conn);
                cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                cmd.Parameters.AddWithValue("@trnType", trnType);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            cmbVoucherNo.Items.Add(reader["TrnNo"].ToString());
                        }
                    }
                }
            }
        }

        private void LoadOurRef()
        {
            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DocNo FROM vewTrnNo where Ourcd = @trnType and CKy = @companyKey Order by DocNo", conn);
                cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                cmd.Parameters.AddWithValue("@trnType", trnType);
                using ( SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            cmbOurRef.Items.Add(reader["DocNo"].ToString());
                        }
                    }
                }
            }
        }

        private void frmJournal_Load(object sender, EventArgs e)
        {
            LoadAccDetails();
            LoadBuCode();
            LoadAnlType1();
            LoadAnlType2();

            LoadVoucherNo();
            LoadOurRef();
            SetupDgvJournal();

            btnDelete.Enabled = false;
            frmLoaded = false;
        }
        private void frmJournal_Activated(object sender, EventArgs e)
        {
            if (!frmLoaded)
            {
                if(formMode == (int)FormMode.NEW)
                {
                    formMode = (int)FormMode.NEW;
                    ProcNew();
                }
                else if(formMode == (int)FormMode.UPDATE)
                {
                    formMode = (int)FormMode.UPDATE;
                    ProcUpdate();
                }
                else
                {
                    formMode = (int)FormMode.VIEW;
                }

                frmLoaded = true;
            }
        }

        private void LoadAccDetails()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                try
                {
                    accDetails = new DataTable();

                    conn.Open();
                    string query = "SELECT AccKy, AccCd, AccNm FROM vewAccAccCd WHERE CKy = @companyKey ORDER BY AccCd";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    SqlDataAdapter dt = new SqlDataAdapter(cmd);
                    dt.Fill(accDetails);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void LoadBuCode()
        {
            try
            {
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    buDetails = new DataTable();
                    conn.Open();
                    string query = "Select BuCd,BUNm,BuKy From vewBUCd Where CKy = @companyKey";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(buDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadAnlType1()
        {
            try
            {
                anlType1Details = new DataTable();
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    string query = "Select AnlTyp1Ky,AnlTyp1Cd,AnlTyp1Nm From vewAnlTyp1Cd  Where CKy = @companyKey";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(anlType1Details);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadAnlType2()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    anlType2Details = new DataTable();
                    conn.Open();
                    string query = "Select AnlTyp2Ky,AnlTyp2Cd,AnlTyp2Nm From vewAnlTyp2Cd  Where CKy = @companyKey";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(anlType2Details);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetupDgvJournal()
        {
            dgvJournal.Columns.Clear();

            DataGridViewTextBoxColumn colNo = new DataGridViewTextBoxColumn();
            colNo.Name = "no";
            colNo.HeaderText = "No";
            dgvJournal.Columns.Add(colNo);

            DataGridViewComboBoxColumn colAccCd = new DataGridViewComboBoxColumn();
            colAccCd.Name = "accCd";
            colAccCd.HeaderText = "Acc Cd";
            colAccCd.DataSource = accDetails;
            colAccCd.ValueMember = "AccKy";
            colAccCd.DisplayMember = "AccCd";
            colAccCd.DataPropertyName = "AccCd";
            dgvJournal.Columns.Add(colAccCd);

            DataGridViewComboBoxColumn colAccName = new DataGridViewComboBoxColumn();
            colAccName.Name = "accName";
            colAccName.HeaderText = "Acc Name";
            colAccName.DataSource = accDetails;
            colAccName.ValueMember = "AccKy";
            colAccName.DisplayMember = "AccNm";
            dgvJournal.Columns.Add(colAccName);

            DataGridViewTextBoxColumn colDescription = new DataGridViewTextBoxColumn();
            colDescription.Name = "description";
            colDescription.HeaderText = "Description";
            dgvJournal.Columns.Add(colDescription);

            DataGridViewTextBoxColumn colDrAmt = new DataGridViewTextBoxColumn();//5
            colDrAmt.Name = "drAmt";
            colDrAmt.HeaderText = "Dr.Amount";
            dgvJournal.Columns.Add(colDrAmt);

            DataGridViewTextBoxColumn colCrAmt = new DataGridViewTextBoxColumn();
            colCrAmt.Name = "crAmt";
            colCrAmt.HeaderText = "Cr.Amount";
            dgvJournal.Columns.Add(colCrAmt);

            DataGridViewTextBoxColumn colAccKy = new DataGridViewTextBoxColumn();//7
            colAccKy.Name = "accKey";
            colAccKy.HeaderText = "Acc Key";
            dgvJournal.Columns.Add(colAccKy);

            DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn();
            colDesc.Name = "desc";
            colDesc.HeaderText = "Desc";
            dgvJournal.Columns.Add(colDesc);

            DataGridViewComboBoxColumn colBuCode = new DataGridViewComboBoxColumn(); //9
            colBuCode.Name = "BuCode";
            colBuCode.HeaderText = "BU.Code";
            colBuCode.DataSource = buDetails;
            colBuCode.DisplayMember = "BuCd";
            colBuCode.ValueMember = "BuKy";
            dgvJournal.Columns.Add(colBuCode);

            DataGridViewComboBoxColumn colAnalType1 = new DataGridViewComboBoxColumn(); //10
            colAnalType1.Name = "anlType1";
            colAnalType1.HeaderText = "Anal Type1";
            colAnalType1.DataSource = anlType1Details;
            colAnalType1.DisplayMember = "AnlTyp1Cd";
            colAnalType1.ValueMember = "AnlTyp1Ky";
            dgvJournal.Columns.Add(colAnalType1);

            DataGridViewComboBoxColumn colAnalType2 = new DataGridViewComboBoxColumn(); //11
            colAnalType2.Name = "anlType2";
            colAnalType2.HeaderText = "Anal Type2";
            colAnalType2.DataSource = anlType2Details;
            colAnalType2.DisplayMember = "AnlTyp2Cd";
            colAnalType2.ValueMember = "AnlTyp2Ky";
            dgvJournal.Columns.Add(colAnalType2);

            DataGridViewTextBoxColumn colBuKey = new DataGridViewTextBoxColumn();//12
            colBuKey.Name = "buKey";
            colBuKey.HeaderText = "BuKey";
            dgvJournal.Columns.Add(colBuKey);

            DataGridViewTextBoxColumn colAnalType1Key = new DataGridViewTextBoxColumn();//13
            colAnalType1Key.Name = "anlType1Key";
            colAnalType1Key.HeaderText = "Anal Type1 Key";
            dgvJournal.Columns.Add(colAnalType1Key);

            DataGridViewTextBoxColumn colAnalType2Key = new DataGridViewTextBoxColumn();//14
            colAnalType2Key.Name = "anlType2Key";
            colAnalType2Key.HeaderText = "Anal Type2 Key";
            dgvJournal.Columns.Add(colAnalType2Key);

            DataGridViewTextBoxColumn colUpdate = new DataGridViewTextBoxColumn();//15
            colUpdate.Name = "update";
            colUpdate.HeaderText = "Update";
            dgvJournal.Columns.Add(colUpdate);

            DataGridViewTextBoxColumn colDelete = new DataGridViewTextBoxColumn();
            colDelete.Name = "delete";
            colDelete.HeaderText = "Delete";
            dgvJournal.Columns.Add(colDelete);

            DataGridViewTextBoxColumn colAccTrnKey = new DataGridViewTextBoxColumn();
            colAccTrnKey.Name = "accTrnKey";
            colAccTrnKey.HeaderText = "Acc Trn Key";
            dgvJournal.Columns.Add(colAccTrnKey);

            dgvJournal.ColumnHeadersDefaultCellStyle.Font = new Font(dgvJournal.Font, FontStyle.Bold);

        }

        private void ProcUpdate()
        {
            if (!UserSession.Instance.GetPermission("MnuJrnl").CanUpdate)
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

            voucherNo = DatabaseFunctions.GetTrnNoList(1, 0, dtpVoucherdate.Value, "JRNL") + 1;
            cmbVoucherNo.Text = voucherNo.ToString();
            dtpVoucherdate.Text = DateTime.Now.ToString();
            cmbOurRef.Text = "";
            dgvJournal.Rows.Clear();
        }

        private bool chkForSetoff(string trnNo, string ourCode, SqlConnection conn)
        {
            bool chkForSetoff = false;

            using (SqlCommand cmd = new SqlCommand("Select SetoffNo From vewAccSetOffDet Where DrTrnNo = @trnNo And DrOurCd = @ourCode", conn))
            {
                cmd.Parameters.AddWithValue("@trnNo", trnNo);
                cmd.Parameters.AddWithValue("@ourCode", ourCode);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    if(reader.HasRows)
                    {
                        chkForSetoff = true;
                    }
                    else
                    {
                        chkForSetoff = false;
                    }
                }
            }

            using (SqlCommand cmd = new SqlCommand("Select SetoffNo From vewAccSetOffDet Where CrTrnNo = @trnNo And DrOurCd = @ourCode", conn))
            {
                cmd.Parameters.AddWithValue("@trnNo", trnNo);
                cmd.Parameters.AddWithValue("@ourCode", ourCode);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    if (reader.HasRows)
                    {
                        chkForSetoff = true;
                    }
                    else
                    {
                        chkForSetoff = false;
                    }
                }
            }

            return chkForSetoff;
        }

        private void ClearFields()
        {
            cmbVoucherNo.Text = "";
            dtpVoucherdate.Value = DateTime.Now;
            cmbOurRef.Text = "";
            dgvJournal.Rows.Clear();
            txtRemark.Text = "";
        }

        private void CalDrCrSumAmount()
        {
            decimal crSubAmount = 0;
            decimal drSubAmount = 0;

            foreach(DataGridViewRow row in dgvJournal.Rows)
            {
                if (row.IsNewRow) continue;

                var accountCode = row.Cells["accCd"]?.Value?.ToString()?.Trim();
                if(string.IsNullOrEmpty(accountCode)) continue;

                var isDeletedRow = row.Cells["delete"]?.Value?.ToString();
                if(isDeletedRow == "1") continue;

                decimal crAmount = 0;
                decimal drAmount = 0;

                decimal.TryParse(row.Cells["crAmt"]?.Value?.ToString(), out crAmount);
                decimal.TryParse(row.Cells["drAmt"]?.Value?.ToString(), out drAmount);

                crSubAmount += crAmount;
                drSubAmount += drAmount;
            }

            lblCrSumAmount.Text = crSubAmount.ToString("N2");
            lblDrSumAmount.Text = drSubAmount.ToString("N2");
            lblDifferenceAmount.Text = (drSubAmount - crSubAmount).ToString("N2");
        }

        private void GetData()
        {
            int trnKey = 0;

            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                string query = "Select TrnKy,Des,DocNo,TrnDt from vewJrnlHdr where TrnNo = @voucherNo And OurCd = @trnType";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@voucherNo", cmbVoucherNo.Text);
                cmd.Parameters.AddWithValue("@trnType", trnType);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        reader.Read();
                        trnKey = Convert.ToInt32(reader["TrnKy"]);
                        txtRemark.Text = reader["Des"].ToString();
                        cmbOurRef.Text = reader["DocNo"].ToString();
                        dtpVoucherdate.Value = Convert.ToDateTime(reader["TrnDt"]);
                    }
                    else
                    {
                        MessageBox.Show("No record found!");
                        return;
                    }
                }
            }

            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                string query = "Select LiNo,AccCd,AccNm,Des,DrAmt,CrAmt,AccKy,'',BuCd,AnlTyp1Cd,AnlTyp2Cd,BUKy,AnlTyp1Ky,AnlTyp2Ky,0 as updt,0 as Del,AccTrnKy " +
                                "from vewJrnlVsfQry where TrnKy = @trnKey";
                SqlCommand cmd = new SqlCommand(@query, conn);
                cmd.Parameters.AddWithValue("trnKey", trnKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                //add here manual data binder

            }
        }

        private void CalcDrCrSumAmount()
        {
            decimal crSumAmount = 0;
            decimal drSumAmount = 0;

            foreach(DataGridViewRow row in dgvJournal.Rows)
            {
                if (string.IsNullOrWhiteSpace(row.Cells["accCd"].Value?.ToString()) && row.Cells["accCd"].Value?.ToString() != "0")
                {
                    if (Convert.ToInt32(row.Cells["delete"].Value) != 1)
                    {
                        if (string.IsNullOrWhiteSpace(row.Cells["drAmt"].Value?.ToString()))
                        {
                            row.Cells["drAmt"].Value = 0;
                        }
                        if (string.IsNullOrWhiteSpace(row.Cells["crAmt"].Value?.ToString()))
                        {
                            row.Cells["crAmt"].Value = 0;
                        }

                        crSumAmount += Convert.ToDecimal(row.Cells["crAmt"].Value);
                        drSumAmount += Convert.ToDecimal(row.Cells["drAmt"].Value);
                    }
                }
            }

            lblCrSumAmount.Text = crSumAmount.ToString("#, 0.00");
            lblDrSumAmount.Text = drSumAmount.ToString("#, 0.00");
            lblDifferenceAmount.Text = (drSumAmount - crSumAmount).ToString("#, 0.00");
        }

        private bool CheckForSetOff(int tranNo, string ourCode, SqlConnection conn)
        {
            bool checkForSetOff = false;

            string query = "Select SetoffNo From vewAccSetOffDet Where (DrTrnNo = @tranNo And DrOurCd = @ourCode) OR (CrTrnNo = @tranNo And DrOurCd = @ourCode)";
            using(SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                cmd.Parameters.AddWithValue("@ourCode", ourCode);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        checkForSetOff = true;
                    }
                    else
                    {
                        checkForSetOff = false;
                    }
                }
            }

            return checkForSetOff;
        }

        //-----------------------------------------------------------

        private void cmbVoucherNo_Enter(object sender, EventArgs e)
        {
            cmbVoucherNo.Tag = cmbVoucherNo.Text;
        }

        private void cmbVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dtpVoucherdate.Focus();
            }
        }

        private void dtpVoucherdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOurRef.Focus();
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void cmbOurRef_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dgvJournal.Focus();
            }
        }

        private void cmbVoucherNo_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(cmbVoucherNo.Text) && cmbVoucherNo.Tag.ToString() != cmbVoucherNo.Text)
            {
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    string query = "Select TrnKy from VewTrnNo Where TrnNo = @voucherNo And OurCd = @trnType And CKy = @companyKey";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@voucherNo", cmbVoucherNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                    cmd.Parameters.AddWithValue("@trnType", trnType);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            reader.Read();
                            TrnKey = Convert.ToInt32(reader["TrnKy"]);

                            if(UserSession.Instance.GetPermission("MnuJrnl").CanUpdate)
                            {
                                ProcUpdate();
                                GetData();
                                CalDrCrSumAmount();

                                btnAdd.Enabled = true;
                            }
                        }
                        else
                        {
                            if(UserSession.Instance.GetPermission("MnuJrnl").CanCreateNew)
                            {
                                DialogResult result = MessageBox.Show("Transaction No. not exist! \n Would you like to enter new Transaction ?", this.Text, MessageBoxButtons.YesNo);
                                if(result == DialogResult.Yes)
                                {
                                    ProcNew();
                                }
                                else
                                {
                                    this.Close();
                                    ClearFields();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sorry, You don't have enough Privileges to enter new transactions");
                            }
                        }
                    }
                }

            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!UserSession.Instance.GetPermission("MnuJrnl").CanCreateNew)
            {
                MessageBox.Show("Access is denied");
                return;
            }
            else
            {
                if(dgvJournal.Rows.Count > 2)
                {
                    DialogResult result = MessageBox.Show("Do you really want to enter new transaction ?", this.Text, MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        ProcNew();
                    }
                }
                else
                {
                    ProcNew();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool inTransaction;
            try
            {
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();

                    if (!UserSession.Instance.GetPermission("MnuJrnl").CanDelete)
                    {
                        MessageBox.Show("Access is denied!");
                        return;
                    }

                    if (!DatabaseFunctions.isValidTrnDate(1, "strTrnTyp", dtpVoucherdate.Value, conn))
                    {
                        MessageBox.Show("You cannot delete transactions for this date");
                        return;
                    }

                    using (SqlCommand cmdGetTrnKy = new SqlCommand("Select TrnKy from vewTrnNo Where OurCd = @trnType And TrnNo = @trnNo And CKy = @companyKey", conn))
                    {
                        cmdGetTrnKy.Parameters.AddWithValue("@trnNo", cmbVoucherNo.Text);
                        cmdGetTrnKy.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        cmdGetTrnKy.Parameters.AddWithValue("@trnType", trnType);
                        using (SqlDataReader reader = cmdGetTrnKy.ExecuteReader())
                        {
                            reader.Read();
                            if(reader.HasRows)
                            {
                                TrnKey = Convert.ToInt32(reader["TrnKy"]);
                            }
                            else
                            {
                                MessageBox.Show("Invalid transaction No!");
                                return;
                            }
                        }
                    }

                    if(chkForSetoff(cmbVoucherNo.Text, "strTrnTyp", conn))
                    {
                        MessageBox.Show("Setoff details exist, Delete the setoff & try again.");
                        return;
                    }

                    DialogResult result = MessageBox.Show("These transaction will be Permanently deleted. \n Do you want to continue?", this.Text, MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        inTransaction = true;
                        SqlTransaction tran = conn.BeginTransaction();

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("Update TrnMas Set fInAct = 1, Status ='D' Where TrnKy = @trnKey", conn))
                            {
                                cmd.Parameters.AddWithValue("@trnKey", TrnKey);
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmdDelete = new SqlCommand("Delete From AccTrn Where TrnKy = @trnKey", conn))
                            {
                                cmdDelete.Parameters.AddWithValue("@trnKey", TrnKey);
                                cmdDelete.ExecuteNonQuery();
                            }

                            tran.Commit();
                            inTransaction = false;
                            ProcNew();
                        }
                        catch (Exception ex)
                        {
                            if(inTransaction)
                            {
                                tran.Rollback();
                            }
                            MessageBox.Show(ex.Message);
                            ClearFields();
                            return;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                int tranTypeKey;
                int tranKey;
                decimal difference = 0;
                decimal amount;

                bool inTransaction = false;

                string description = string.IsNullOrEmpty(txtRemark.Text) ? "" : txtRemark.Text.Trim();
                string refNo = string.IsNullOrWhiteSpace(cmbOurRef.Text) ? "" : cmbOurRef.Text.Trim();

                try
                {
                    string query = "Select TrnTypKy from vewTrnTypCd Where TrnTyp ='JRNL' and CKy = @companyKey";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            tranTypeKey = Convert.ToInt32(reader["TrnTypKy"]);
                        }
                        else
                        {
                            MessageBox.Show("Transaction type not found");
                            return;
                        }
                    }


                    if (!DatabaseFunctions.isValidTrnDate(UserSession.Instance.CompanyKey, trnType, dtpVoucherdate.Value, conn))
                    {
                        MessageBox.Show("You cannot enter/alter transactions for this date");
                        return;
                    }

                    query = "Select TrnKy from vewTrnNo Where TrnNo = @tranNo And OurCd = 'JRNL' And CKy = @companyKey";
                    using(SqlCommand cmd = new SqlCommand(query,conn))
                    {
                        cmd.Parameters.AddWithValue("@tranNo", cmbVoucherNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                tranKey = Convert.ToInt32(reader["TrnKy"]);
                            }
                            else
                            {
                                tranKey = 0;
                            }
                        }
                        
                    }

                    CalcDrCrSumAmount();

                    difference = Convert.ToDecimal(lblDifferenceAmount.Text);
                    if(difference != 0)
                    {
                        MessageBox.Show("Check the entry");
                        return;
                    }

                    if(formMode == (int)FormMode.UPDATE)
                    {
                        if(CheckForSetOff(Convert.ToInt32(cmbVoucherNo.Text), "JRNL", conn))
                        {
                            MessageBox.Show("Setoff details exist, Delete the setoff & try again.");
                            return;
                        }

                        SqlTransaction tran = conn.BeginTransaction();
                        inTransaction = true;

                        try
                        {
                            query = "Update TrnMas Set TrnDt = @tranDate, Des = @description, DocNo = @refNo Where TrnKy = @tranKey";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpVoucherdate.Value);
                                cmd.Parameters.AddWithValue("@refNo", cmbOurRef.Text);
                                cmd.Parameters.AddWithValue("@tranKey", tranKey);

                                cmd.ExecuteNonQuery();
                            }

                            query = "Delete From AccTrn Where TrnKy = @tranKey";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranKey", tranKey);

                                cmd.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dgvJournal.Rows)
                            {
                                if (string.IsNullOrWhiteSpace(row.Cells["accCd"].Value?.ToString()) && row.Cells["accCd"].Value?.ToString() != "0")
                                {
                                    if (Convert.ToInt32(row.Cells["delete"].Value) != 1)
                                    {
                                        if (Convert.ToDecimal(row.Cells["drAmt"].Value) == 0)
                                        {
                                            if (Convert.ToDecimal(row.Cells["crAmt"].Value) == 0)
                                            {
                                                amount = 0;
                                            }
                                            else
                                            {
                                                amount = -1;
                                            }
                                        }
                                        else
                                        {
                                            amount = Convert.ToDecimal(row.Cells["drAmt"].Value);
                                        }

                                        //insert function
                                        query = "Insert into AccTrn(TrnKy,AccKy,LiNo,Des,Amt,Fchqdet,AnlTyp1Ky,AnlTyp2Ky,PmtModeKy,BUKy) " +
                                            "values(@tranKey, @accKey, @liNo, @description, @amount, @fchq, @anlTyp1Key, @anlTyp2Ky, @mtModeKey, @buKey)";
                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@tranKey", tranKey);
                                            cmd.Parameters.AddWithValue("@accKey", row.Cells["accKey"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@liNo", row.Cells["no"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@description", row.Cells["description"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@amount", amount);
                                            cmd.Parameters.AddWithValue("@fchq", 0);
                                            cmd.Parameters.AddWithValue("@anlTyp1Key", tranKey);
                                            cmd.Parameters.AddWithValue("@anlTyp2Ky", row.Cells["anlType1Key"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@mtModeKey", row.Cells["anlType2Key"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@buKey", 0);

                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            tran.Commit();
                            inTransaction = false;
                        }
                        catch (Exception ex)
                        {
                            if (!inTransaction)
                            {
                                tran.Rollback();
                            }
                            MessageBox.Show(ex.Message);
                        }
                    }

                    else if(formMode == (int)FormMode.NEW)
                    {
                        SqlTransaction tran = conn.BeginTransaction();
                        inTransaction = true;

                        try
                        {
                            int tranNo = DatabaseFunctions.GetTrnNoLstSave(UserSession.Instance.CompanyKey, 0, dtpVoucherdate.Value, "JRNL", conn, tran);

                            query = "Insert Into TrnMas(TrnDt,TrnNo,TrnTypKy,OurCd,Des,DocNo,fApr,EntUsrKy,EntDtm) " +
                                "values(@tranDate, @tranNo, @tranTypeKey, @ourCode, @description, @refNo, 1, @userKey, @enterDateTime)";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranDate", dtpVoucherdate.Value);
                                cmd.Parameters.AddWithValue("@tranNo", tranNo);
                                cmd.Parameters.AddWithValue("@tranTypeKey", tranTypeKey);
                                cmd.Parameters.AddWithValue("@ourCode", "JRNL");
                                cmd.Parameters.AddWithValue("@description", txtRemark.Text?? "");
                                cmd.Parameters.AddWithValue("@refNo", cmbOurRef.Text ?? "");
                                cmd.Parameters.AddWithValue("@userKey", UserSession.Instance.UserKey);
                                cmd.Parameters.AddWithValue("@enterDateTime", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }

                            query = "Select TrnKy from vewTrnNo Where TrnNo = @tranNo And OurCd = 'JRNL' And CKy = @companyKey";
                            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@tranNo", cmbVoucherNo.Text.Trim());
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
                                        tranKey = 0;
                                    }
                                }

                            }

                            foreach (DataGridViewRow row in dgvJournal.Rows)
                            {
                                if (string.IsNullOrWhiteSpace(row.Cells["accCd"].Value?.ToString()) && row.Cells["accCd"].Value?.ToString() != "0")
                                {
                                    if (Convert.ToInt32(row.Cells["delete"].Value) != 1)
                                    {
                                        if (Convert.ToDecimal(row.Cells["drAmt"].Value) == 0)
                                        {
                                            if (Convert.ToDecimal(row.Cells["crAmt"].Value) == 0)
                                            {
                                                amount = 0;
                                            }
                                            else
                                            {
                                                amount = -1;
                                            }
                                        }
                                        else
                                        {
                                            amount = Convert.ToDecimal(row.Cells["drAmt"].Value);
                                        }

                                        //insert function
                                        query = "Insert into AccTrn(TrnKy,AccKy,LiNo,Des,Amt,Fchqdet,AnlTyp1Ky,AnlTyp2Ky,PmtModeKy,BUKy) " +
                                            "values(@tranKey, @accKey, @liNo, @description, @amount, @fchq, @anlTyp1Key, @anlTyp2Ky, @mtModeKey, @buKey)";
                                        using (SqlCommand cmd = new SqlCommand(query, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@tranKey", tranKey);
                                            cmd.Parameters.AddWithValue("@accKey", row.Cells["accKey"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@liNo", row.Cells["no"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@description", row.Cells["description"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@amount", amount);
                                            cmd.Parameters.AddWithValue("@fchq", 0);
                                            cmd.Parameters.AddWithValue("@anlTyp1Key", tranKey);
                                            cmd.Parameters.AddWithValue("@anlTyp2Ky", row.Cells["anlType1Key"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@mtModeKey", row.Cells["anlType2Key"].Value.ToString().Trim());
                                            cmd.Parameters.AddWithValue("@buKey", 0);

                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            tran.Commit();
                            inTransaction = false;
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

                    btnAdd_Click(this, EventArgs.Empty);

                    //report view
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvJournal_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                int rowIndex = dgvJournal.CurrentCell.RowIndex;
                DialogResult result = MessageBox.Show("Do you really want to DELETE the current row ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    dgvJournal.Rows[rowIndex].Cells["update"].Value = 1;
                    dgvJournal.Rows[rowIndex].Cells["delete"].Value = 1;
                    dgvJournal.CurrentRow.Visible = false;

                    CalcDrCrSumAmount();
                }
            }
        }


        private void dgvJournal_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = dgvJournal.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = dgvJournal.Rows[e.RowIndex];

            if (!decimal.TryParse(e.FormattedValue?.ToString() ?? "", out decimal editedValue))
                editedValue = 0;

            decimal otherValue = 0;

            if (columnName == "drAmt" && editedValue > 0)
            {
                decimal.TryParse(row.Cells["crAmt"].Value?.ToString() ?? "", out otherValue);
            }
            else if (columnName == "crAmt" && editedValue > 0)
            {
                decimal.TryParse(row.Cells["drAmt"].Value?.ToString() ?? "", out otherValue);
            }

            if (editedValue > 0 && otherValue > 0)
            {
                dgvJournal.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                MessageBox.Show("Cr & Dr both can't contain values", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvJournal_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvJournal_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvJournal.CurrentRow;
            try
            {
                if (dgvJournal.Columns[e.ColumnIndex].Name == "accCd" && !string.IsNullOrWhiteSpace(row.Cells["accCd"].Value?.ToString())) //2
                {
                    using(SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();

                        string query = "SELECT AccCd,AccNm,AccKy FROM vewAccAccCd where AccKy = @accKey And CKy = @companyKey";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@accKey", row.Cells["accCd"].Value.ToString());
                            cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if(reader.Read())
                                {
                                    row.Cells["accName"].Value = reader["AccKy"];
                                    row.Cells["accKey"].Value = reader["AccKy"];
                                }
                                else
                                {
                                    //stay current cell
                                }
                            }
                        }
                    }
                }

                else if (dgvJournal.Columns[e.ColumnIndex].Name == "accName" && !string.IsNullOrWhiteSpace(row.Cells["accName"].Value?.ToString())) //3
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();

                        string query = "SELECT AccCd,AccNm,AccKy FROM vewAccAccCd where AccKy = @accKey And CKy = @companyKey";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@accKey", row.Cells["accName"].Value);
                            cmd.Parameters.AddWithValue("@companyKey", UserSession.Instance.CompanyKey);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row.Cells["accCd"].Value = reader["AccKy"];
                                    row.Cells["accKey"].Value = reader["AccKy"];
                                }
                                else
                                {
                                    //stay current cell
                                }
                            }
                        }
                    }
                }

                else if (dgvJournal.Columns[e.ColumnIndex].Name == "drAmt" && row.Cells["drAmt"].Value?.ToString() != "") //10
                {

                }
                else if (dgvJournal.Columns[e.ColumnIndex].Name == "crAmt" && row.Cells["crAmt"].Value?.ToString() != "") //10
                {

                }

                else if (dgvJournal.Columns[e.ColumnIndex].Name == "BuCode") //9
                {
                    if(row.Cells["buKey"].Value?.ToString() != "0")
                    {
                        row.Cells["buKey"].Value = row.Cells["BuCode"].Value;
                    }
                }

                else if (dgvJournal.Columns[e.ColumnIndex].Name == "anlType1" && row.Cells["anlType1"].Value?.ToString() != "0") //10
                {
                    row.Cells["anlType1Key"].Value = row.Cells["anlType1"].Value;
                }

                else if (dgvJournal.Columns[e.ColumnIndex].Name == "anlType2" && row.Cells["anlType2"].Value?.ToString() != "0") //11
                {
                    row.Cells["anlType2Key"].Value = row.Cells["anlType2"].Value;
                }

                else
                {
                    if (string.IsNullOrWhiteSpace(row.Cells["drAmt"].Value?.ToString()))
                    {
                        row.Cells["drAmt"].Value = 0;
                    }

                    if (string.IsNullOrWhiteSpace(row.Cells["crAmt"].Value?.ToString()))
                    {
                        row.Cells["crAmt"].Value = 0;
                    }
                }

                row.Cells["no"].Value = Convert.ToInt32(dgvJournal.Rows[e.RowIndex].Index + 1);
                row.Cells["update"].Value = 1;

                CalcDrCrSumAmount();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
