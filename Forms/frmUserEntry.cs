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
    public partial class frmUserEntry : Form
    {
        private int childFormNumber = 0;

        public frmUserEntry()
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
        int userKey;

        private void LoadUserId ()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT UsrKy,UsrID FROM UsrMas  Where fInAct = 0 Order By UsrID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbUserId.DataSource = dt;
                cmbUserId.DisplayMember = "UsrID";
                cmbUserId.ValueMember = "UsrKy";

                conn.Close();
            }
        }

        //--------------------------------------------------------------------------------------------------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var frmChangePassword = new frmChangePassword();
            frmChangePassword.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmAddNewUser = new frmAddNewUser();
            DialogResult result =  frmAddNewUser.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadUserId();
            }
        }

        private void frmUserEntry_Load(object sender, EventArgs e)
        {
            LoadUserId();
        }

        private void cmbUserId_Leave(object sender, EventArgs e)
        {
            userKey = (int)cmbUserId.SelectedValue;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want delete this user", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update vewUsrQry Set finact = 1, Status = 'D' Where UsrKy =@userKey", conn);
                    cmd.Parameters.AddWithValue("@userKey", userKey);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    LoadUserId();
                }
            }
        }


        //--------------------------------------------------------------------------------------------------------------------
        private void cmbUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate.Focus();
            }
            
        }

        private void cmbUserId_Click(object sender, EventArgs e)
        {
            
        }
    }
}
