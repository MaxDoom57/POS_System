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
    public partial class frmAddNewBankCode : Form
    {
        private int childFormNumber = 0;

        public frmAddNewBankCode()
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        bool isCodeInDatabase;
        bool isNameInDatabase;
        private void btnSave_Click(object sender, EventArgs e)
        {
            

            //check bank code already exist or not
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BnkKy, BnkNm, BnkCd FROM BnkMas WHERE BnkCd = @bankCode AND fInAct = 0", conn);
                cmd.Parameters.AddWithValue("@bankCode", txtBankCode.Text);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["BnkCd"].ToString() == txtBankCode.Text)
                        {
                            isCodeInDatabase = true;
                            MessageBox.Show("The bank code exist");
                            txtBankCode.Text = "";
                            return;
                        }
                        else
                        {
                            isCodeInDatabase = false;
                        }
                    }
                }
                conn.Close();
            }

            //check bank name already exist or not
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BnkKy, BnkNm, BnkCd FROM BnkMas WHERE BnkNm = @bankName AND fInAct = 0", conn);
                cmd.Parameters.AddWithValue("@bankName", txtBankName.Text);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["BnkNm"].ToString() == txtBankName.Text)
                        {
                            isNameInDatabase = true;
                            MessageBox.Show("The bank name exist");
                            txtBankName.Text = "";
                            return;
                        }
                        else
                        {
                            isNameInDatabase = false;
                        }
                    }
                }
                conn.Close();
            }

            //check bank code and name is empty or not
            if (string.IsNullOrEmpty(txtBankCode.Text) || string.IsNullOrEmpty(txtBankName.Text))
            {
                MessageBox.Show("Must have bank code & bank name");
            }

            //check bank code have 4 numbers or not
            else if (!(txtBankCode.Text.Length == 4))
            {
                MessageBox.Show("Bank code must have 4 numbers");
            }

            else
            {
                if ((isCodeInDatabase == false) && (isNameInDatabase == false))
                {
                    try
                    {
                        SqlConnection conn = DBConnectionManager.GetConnection();
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("Insert Into BnkMas(BnkCd,BnkNm) Values(@bankCode, @bankName)", conn);
                        cmd.Parameters.AddWithValue("@bankCode",txtBankCode.Text);
                        cmd.Parameters.AddWithValue("@bankName", txtBankName.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Saved Successfully");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------
        private void frmAddNewBankCode_Load(object sender, EventArgs e)
        {
            txtBankCode.Focus();
        }

        private void txtBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBankName.Focus();
            }
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            btnSave.Focus();
        }
    }
}
