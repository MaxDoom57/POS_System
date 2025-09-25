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
using POS_System.Classes;

namespace POS_System.Forms
{
    public partial class frmAddNewBranchCode : Form
    {
        private int childFormNumber = 0;

        public frmAddNewBranchCode()
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


        



        //-----------------------------------------------------------------------------------------------------------------------
        string bankKey;
        string bankCode;
        string branchCode;
        string branchName;
        bool isItNewBranchCode;
        bool isItNewBranchName;
        private void LoadBankName()
        {
            try
            {
                SqlConnection conn = DBConnectionManager.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select BnkKy,BnkNm,BnkCd from vewBnkNm Order By BnkNm", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbBankName.Items.Add(reader["BnkNm"].ToString());
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadBankKey(string bankName)
        {
            try
            {
                SqlConnection conn = DBConnectionManager.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BnkKy,BnkCD FROM BnkMas WHERE BnkNm = @bankName", conn);
                cmd.Parameters.AddWithValue("@bankName", bankName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bankKey = reader["BnkKy"].ToString();
                        bankCode = reader["BnkCd"].ToString();
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------

        private void cmbBankName_Click(object sender, EventArgs e)
        {
            LoadBankName();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            branchCode = txtBranchCode.Text;
            branchName = txtBranchName.Text;
            LoadBankKey(cmbBankName.Text);

            if (string.IsNullOrEmpty(cmbBankName.Text))
            {
                MessageBox.Show("Select Bank Name first");
            }
            else if (string.IsNullOrEmpty(txtBranchCode.Text) || string.IsNullOrEmpty(txtBranchName.Text))
            {
                MessageBox.Show("Please enter Branch code and Branch name");
            }
            else if ((txtBranchCode.Text.Length < 3))
            {
                MessageBox.Show("Branch code must have 3 numbers");
            }
            else
            {
                try
                {
                    isItNewBranchName = true;
                    isItNewBranchCode = true;

                    SqlConnection conn = DBConnectionManager.GetConnection();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT BrnCd,BrnNm FROM BrnMas WHERE BnkKy = @bankKey", conn);
                    cmd.Parameters.AddWithValue("@bankKey", bankKey);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["BrnCd"].ToString() == branchCode)
                            {
                                isItNewBranchCode = false;
                            }

                            if (reader["BrnNm"].ToString() == branchName)
                            {
                                isItNewBranchName = false;
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                if (!isItNewBranchCode)
                {
                    MessageBox.Show("Branch code already exist");
                }

                if (!isItNewBranchName)
                {
                    MessageBox.Show("Branch name already exist");
                }

                if (isItNewBranchCode == true && isItNewBranchName == true)
                {
                    try
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            string query = "Insert Into BrnMas(BnkCd,BrnCd,BrnNm,BnkKy) Values (@bankCode, @branchCode, @branchName, @bankKey)";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@bankKey", bankKey);
                            cmd.Parameters.AddWithValue("@bankCode", bankCode);
                            cmd.Parameters.AddWithValue("@branchCode", branchCode);
                            cmd.Parameters.AddWithValue("@branchName", branchName);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show($"{branchName} saved successfully");
                            cmbBankName.Text = "";
                            txtBranchCode.Text = "";
                            txtBranchName.Text = "";

                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void frmAddNewBranchCode_Load(object sender, EventArgs e)
        {
            LoadBankName();
        }


        //------------------------------------------------------------------------------------------------------------------
        // Key handles
        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtBranchCode.Focus(); }
            
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtBranchName.Focus(); }
        }

        private void txtBranchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnSave.Focus(); }
        }
    }
}
