using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_System.Classes;

namespace POS_System.Forms
{
    public partial class frmBranchEntry : Form
    {
        private int childFormNumber = 0;

        public frmBranchEntry()
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
        string branchKey;
        string branchName;
        bool isCodeChange;
        bool isNameChange;
        bool isCmbCodeLock;


        private void LoadBankName () 
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

        private void LoadBankKey (string bankName) 
        {
            try
            {
                SqlConnection conn = DBConnectionManager.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BnkKy, BnkCd FROM BnkMas WHERE BnkNm = @bankName AND fInAct='0'", conn);
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
                MessageBox.Show (ex.Message);
            }
        }

        private void LoadBranchCode (string bankKey) 
        {
            try
            {
                SqlConnection conn = DBConnectionManager.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BrnCd FROM BrnMas WHERE BnkKy = @bankKey AND fInAct='0'", conn);
                cmd.Parameters.AddWithValue("@bankKey", bankKey);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbBranchCode.Items.Add(reader["BrnCd"]);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
            }
        }

        private void LoadBranchName (string bankKey) 
        {
            try
            {
                SqlConnection conn = DBConnectionManager.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BrnNm FROM BrnMas WHERE BnkKy = @bankKey AND fInAct='0'", conn);
                cmd.Parameters.AddWithValue("@bankKey", bankKey);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbBranchName.Items.Add(reader["BrnNm"]);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckCodeChanges(string branchCodeText)
        {
            try
            {
                isCodeChange = true;

                SqlConnection conn = DBConnectionManager.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BrnCd FROM BrnMas WHERE BnkKy = @bankKey", conn);
                cmd.Parameters.AddWithValue("@bankKey", bankKey);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["BrnCd"].ToString() == branchCodeText)
                        {
                            isCodeChange = false;
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckNameChanges (string branchNameText )
        {
            try
            {
                isNameChange = true;

                SqlConnection conn = DBConnectionManager.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT BrnNm FROM BrnMas WHERE BnkKy = @bankKey AND fInAct='0'", conn);
                cmd.Parameters.AddWithValue("@bankKey", bankKey);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["BrnNm"].ToString() == branchNameText)
                        {
                            isNameChange = false;
                        }
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
        private void frmBranchEntry_Load(object sender, EventArgs e)
        {
            LoadBankName();
            cmbBankName.Focus();
        }

        private void cmbBankName_Leave(object sender, EventArgs e)
        {
            cmbBranchCode.Enabled = true;
        }

        private void cmbBranchCode_Click(object sender, EventArgs e)
        {
            
            
        }

        private void cmbBranchName_Click(object sender, EventArgs e)
        {
           
        }

        private void cmbBranchName_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbBankName.Text))
            {
                cmbBranchName.Items.Clear();
                LoadBankKey(cmbBankName.Text.Trim());
                LoadBranchName(bankKey);
            }

            else
            {
                MessageBox.Show("Please select Bank name first");
            }

            if (!string.IsNullOrEmpty(cmbBranchCode.Text))
            {
                branchCode = cmbBranchCode.Text;
                try
                {
                    SqlConnection conn = DBConnectionManager.GetConnection();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT BrnNm,BrnKy FROM BrnMas WHERE BrnCd = @branchCode AND fInAct='0'", conn);
                    cmd.Parameters.AddWithValue("@branchCode", branchCode);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbBranchName.Text = (reader["BrnNm"].ToString());
                            branchKey = reader["BrnKy"].ToString();
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //isCmbCodeLock = true;
            }
        }

        private void cmbBranchName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbBranchCode.Text))
            {
                branchName = cmbBranchName.Text;
                try
                {
                    SqlConnection conn = DBConnectionManager.GetConnection();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT BrnCd,BrnKy FROM BrnMas WHERE BrnNm = @branchName AND fInAct='0'", conn);
                    cmd.Parameters.AddWithValue("@branchName", branchName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbBranchCode.Text = (reader["BrnCd"].ToString());
                            branchKey = reader["BrnKy"].ToString();
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNewBranchCode frmAddNewBranchCode = new frmAddNewBranchCode();
            var result = frmAddNewBranchCode.ShowDialog();

            if(result == DialogResult.OK)
            {
                cmbBankName.Text = "";
                cmbBranchCode.Text = "";
                cmbBranchName.Text = "";
                cmbBankName.Focus();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbBranchCode.Text) || string.IsNullOrEmpty(cmbBranchName.Text))
            {
                MessageBox.Show("Branch code and name can't be empty");
            }
            else if (cmbBranchCode.Text.Length <3)
            {
                MessageBox.Show("Branch code must have 3 numbers");
            }
            else
            {
                CheckNameChanges(cmbBranchName.Text);
                CheckCodeChanges(cmbBranchCode.Text);

                if (isNameChange || isCodeChange)
                {
                    try
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            branchCode = cmbBranchCode.Text;
                            branchName = cmbBranchName.Text;

                            conn.Open();
                            string query = "Update BrnMas Set BnkCd=@bankCode, BrnCd=@branchCode, BrnNm=@branchName, BnkKy=@bankKey WHERE brnKy=@branchKey";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@bankCode", bankCode);
                            cmd.Parameters.AddWithValue("@branchCode", branchCode);
                            cmd.Parameters.AddWithValue("@branchName", branchName);
                            cmd.Parameters.AddWithValue("@bankKey", bankKey);
                            cmd.Parameters.AddWithValue("@branchKey", branchKey);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show($"{branchName} update successfully");
                            cmbBankName.Text = "";
                            cmbBranchCode.Text = "";
                            cmbBranchName.Text = "";

                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbBranchCode.Text))
            {
                MessageBox.Show("Select branch code");
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want DELETE this branch", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection conn = DBConnectionManager.GetConnection();
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("Update BrnMas Set fInAct='1',Status='D' Where BrnKy=@branchKey", conn);
                        cmd.Parameters.AddWithValue("@branchKey", branchKey);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show($"{branchCode} successfully Deleted");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private void cmbBranchCode_DropDown(object sender, EventArgs e)
        {
            //if (isCmbCodeLock)
            //{
            //    cmbBranchCode.DroppedDown = false;
            //    cmbBranchCode.Enabled = false;
            //}
        }

        private void cmbBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (isCmbCodeLock)
            //{
            //    e.Handled = true;
            //}
        }

        private void cmbBankName_TextChanged(object sender, EventArgs e)
        {
            cmbBranchCode.Enabled = true;
        }

        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBranchCode.Focus();
                cmbBranchCode.Enabled = true ;
            }
        }

        private void cmbBranchCode_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbBankName.Text))
            {
                cmbBranchCode.Items.Clear();
                LoadBankKey(cmbBankName.Text.Trim());
                LoadBranchCode(bankKey);
            }
            else
            {
                MessageBox.Show("Please select Bank name first");
            }
        }

        private void cmbBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBranchName.Focus();
                LoadBranchName(bankKey);
            }
        }

        private void cmbBranchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate.Focus();
            }
        }

        private void cmbBranchCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbBranchCode.Text))
            {
                //cmbBranchCode.Enabled = false;
            }
        }
    }
}
