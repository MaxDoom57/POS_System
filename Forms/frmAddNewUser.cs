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
using POS_System.Modules;

namespace POS_System.Forms
{
    public partial class frmAddNewUser : Form
    {
        private int childFormNumber = 0;

        public frmAddNewUser()
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

        //--------------------------------------------------------------------------------------------------------------------
        string userId, userName, password, confirmPassword, passwordTip;

        private bool CheckValidUserId() 
        {
            bool idAlreadyExist = false;

            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT UsrID FROM UsrMas  Where fInAct = 0", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (string.Equals(reader["UsrID"].ToString(), txtUserId.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        idAlreadyExist = true;
                    }
                }
            }
            return txtUserId.Text.Length >= 2 && !idAlreadyExist;
        }

        private bool CheckValidPassword() 
        {
            return txtNewPassword.Text.Length >= 5 || txtNewPassword.Text.Length ==0;
        }

        private void ClearFields()
        {
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtPasswordTip.Text = "";
        }

        //--------------------------------------------------------------------------------------------------------------------

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void txtUserId_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = CheckValidUserId() && CheckValidPassword();
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = CheckValidUserId() && CheckValidPassword();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            userId = txtUserId.Text;
            userName = txtUserName.Text;
            password = txtNewPassword.Text;
            confirmPassword = txtConfirmPassword.Text;
            passwordTip = txtPasswordTip.Text;

            if (password == confirmPassword)
            {
                try
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        string hashedPassword = "";

                        if (!string.IsNullOrEmpty(password))
                        {
                            hashedPassword = SecurityModules.HashPassword(password);
                        }
                        conn.Open();
                        string query = "INSERT INTO VewUsrQry( UsrNm,UsrId, Pwd, PwdTip) values(@userName, @userId, @hashedPassword, @passwordTip)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@userName", userName);
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                            cmd.Parameters.AddWithValue("@passwordTip", passwordTip);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show($"{userId} is successfully added");
                            ClearFields();
                            this.Close();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Confirmation of password is incorrect.");
            }
        }


        //--------------------------------------------------------------------------------------------------------------------

        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtUserName.Focus(); }
            if (e.KeyCode == Keys.Down) { txtUserName.Focus(); }
        }


        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtNewPassword.Focus(); }
            if (e.KeyCode == Keys.Down) { txtNewPassword.Focus(); }
            if (e.KeyCode == Keys.Up) { txtUserId.Focus(); }
        }

        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtConfirmPassword.Focus(); }
            if (e.KeyCode == Keys.Down) { txtConfirmPassword.Focus(); }
            if (e.KeyCode == Keys.Up) { txtUserName.Focus(); }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtPasswordTip.Focus(); }
            if (e.KeyCode == Keys.Down) { txtPasswordTip.Focus(); }
            if (e.KeyCode == Keys.Up) { txtNewPassword.Focus(); }
        }

        private void txtPasswordTip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnSave.Focus(); }
            if (e.KeyCode == Keys.Down) { btnSave.Focus(); }
            if (e.KeyCode == Keys.Up) { txtConfirmPassword.Focus(); }
        }
    }
}
