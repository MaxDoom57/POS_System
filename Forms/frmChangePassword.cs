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
    public partial class frmChangePassword : Form
    {
        private int childFormNumber = 0;

        public frmChangePassword()
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

        //------------------------------------------------------------------------------------------------------------------------------------
        string userKey = UserSession.Instance.UserKey;
        string userId, userName, storedPassword, passwordTip;

        

        private bool CheckValidPassword()
        {
            return txtNewPassword.Text.Length >= 5 || txtNewPassword.Text.Length == 0;
        }


        //------------------------------------------------------------------------------------------------------------------------------------
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM UsrMas Where UsrKy =@userKey", conn);
                    cmd.Parameters.AddWithValue("@userKey", userKey);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userId = reader["usrID"].ToString();
                            userName = reader["usrNm"].ToString();
                            storedPassword = reader["Pwd"].ToString();
                            passwordTip = reader["PwdTip"].ToString();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtUserId.Text = userId;
            txtUserName.Text = userName;
            txtPasswordTip.Text = passwordTip;

            txtUserId.Enabled = false;
            txtUserName.Enabled = false;
            txtPasswordTip.Enabled = false;

            txtOldPassword.Focus();
            btnOk.Enabled = false;
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = CheckValidPassword();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!SecurityModules.VerifyPassword(txtOldPassword.Text, storedPassword))
            {
                MessageBox.Show("The old password you typed is incorrect");
                return;
            }
            else if (!(txtNewPassword.Text == txtConfirmPassword.Text))
            {
                MessageBox.Show("Confirmation of password is incorrect");
                return;
            }
            else
            {
                try
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE vewUsrQry SET pwd =@updatedPassword WHERE UsrKy= @userKey", conn);
                        cmd.Parameters.AddWithValue("@updatedPassword", SecurityModules.HashPassword(txtNewPassword.Text));
                        cmd.Parameters.AddWithValue("@userKey", userKey);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Password updated successfully");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        private void txtOldPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtNewPassword.Focus(); }
            if (e.KeyCode == Keys.Down) { txtNewPassword.Focus(); }
        }

        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtConfirmPassword.Focus(); }
            if (e.KeyCode == Keys.Down) { txtConfirmPassword.Focus(); }
            if (e.KeyCode == Keys.Up) { txtOldPassword.Focus(); }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnOk.Focus(); }
            if (e.KeyCode == Keys.Up) { txtNewPassword.Focus(); }
        }
    }
}
