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
    public partial class frmBankEntry : Form
    {
        private int childFormNumber = 0;

        public frmBankEntry()
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

        //Define Variables
        string query;
        int bankKey;
        string bankCode;
        string bankName;
        string selectedCode, selectedName;
        long frmMode;


        //Define Functions
        private void SetNewMode() { }
        private void SetUpdateMode() { }
        private void LoadComboItems() 
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                //Fill Bank code Combolist
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select BnkCd From BnkMas Where fInAct='0' Order by BnkCd", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmdBankCode.Items.Add(reader["BnkCd"].ToString());
                    }
                }

                //Fill Bank Name Combolist
                using (SqlCommand cmd = new SqlCommand("Select BnkNm From BnkMas Where fInAct='0' Order By BnkNm", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbBankName.Items.Add(reader["BnkNm"].ToString());
                    }
                }
                conn.Close();
            }
        }
        private void RefreshComboItems() 
        {
            LoadComboItems();
        }

        //-----------------------------------------------------------------------------------------------------------------------
        private void frmBankEntry_Load(object sender, EventArgs e)
        {
            LoadComboItems();
            cmbBankName.Focus();
        }

        private void cmdBankCode_Leave(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(cmdBankCode.Text))
            //{
            //    try
            //    {
            //        bankCode = cmdBankCode.Text;

            //        using (SqlConnection conn = DBConnectionManager.GetConnection())
            //        {
            //            conn.Open();
            //            SqlCommand cmd = new SqlCommand("SELECT BnkKy, BnkNm, BnkCd FROM BnkMas WHERE BnkCd = @bankCode AND fInAct = 0", conn);
            //            cmd.Parameters.AddWithValue("@bankCode", bankCode);

            //            using (SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                if (reader.Read())
            //                {
            //                    SetUpdateMode();
            //                    bankKey = Convert.ToInt32(reader["BnkKy"]);
            //                    cmbBankName.Text = reader["BnkNm"].ToString();
            //                    selectedCode = cmdBankCode.Text;
            //                }
            //                else
            //                {
            //                    SetNewMode();
            //                    cmbBankName.Text = "";
            //                }
            //            }
            //            conn.Close ();
            //        }                    
            //    }

            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error: " + ex.Message);
            //    }
            //}
        }

        private void cmbBankName_Leave(object sender, EventArgs e)
        {
            //if(!string.IsNullOrEmpty(cmbBankName.Text))
            //{
            //    try
            //    {
            //        bankName = cmbBankName.Text;
            //        using (SqlConnection conn = DBConnectionManager.GetConnection())
            //        {
            //            conn.Open();
            //            SqlCommand cmd = new SqlCommand("SELECT BnkKy, BnkNm, BnkCd FROM BnkMas WHERE BnkNm = @bankName AND fInAct = 0", conn);
            //            cmd.Parameters.AddWithValue("@bankName",bankName);

            //            using(SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                if (reader.Read())
            //                {
            //                    SetUpdateMode();
            //                    bankCode = reader["BnkCd"].ToString();
            //                    bankKey = Convert.ToInt32(reader["BnkKy"]);
            //                    cmdBankCode.Text = reader["BnkCd"].ToString();
            //                    selectedName = cmbBankName.Text;
            //                }
            //                else
            //                {
            //                    SetNewMode();
            //                    cmdBankCode.Text = "1122";
            //                }
            //            }
            //            conn.Close();
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error: " + ex.Message);
            //    }
            //}
        }

        private void cmdBankCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmdBankCode.Text))
            {
                try
                {
                    bankCode = cmdBankCode.Text;

                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT BnkKy, BnkNm, BnkCd FROM BnkMas WHERE BnkCd = @bankCode AND fInAct = 0", conn);
                        cmd.Parameters.AddWithValue("@bankCode", bankCode);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                SetUpdateMode();
                                bankKey = Convert.ToInt32(reader["BnkKy"]);
                                cmbBankName.Text = reader["BnkNm"].ToString();
                                selectedCode = cmdBankCode.Text;
                            }
                            else
                            {
                                SetNewMode();
                                cmbBankName.Text = "";
                            }
                        }
                        conn.Close();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void cmbBankName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbBankName.Text))
            {
                try
                {
                    bankName = cmbBankName.Text;
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT BnkKy, BnkNm, BnkCd FROM BnkMas WHERE BnkNm = @bankName AND fInAct = 0", conn);
                        cmd.Parameters.AddWithValue("@bankName", bankName);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                SetUpdateMode();
                                bankCode = reader["BnkCd"].ToString();
                                bankKey = Convert.ToInt32(reader["BnkKy"]);
                                cmdBankCode.Text = reader["BnkCd"].ToString();
                                selectedName = cmbBankName.Text;
                            }
                            else
                            {
                                SetNewMode();
                                cmdBankCode.Text = "1122";
                            }
                        }
                        conn.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmAddNewBankCode = new frmAddNewBankCode();
            var result = frmAddNewBankCode.ShowDialog();

            if (result == DialogResult.OK)
            {
                RefreshComboItems();
                cmdBankCode.Text = "";
                cmbBankName.Text = "";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if ((selectedCode != cmdBankCode.Text) || (selectedName != cmbBankName.Text))
            {
                try
                {
                    SqlConnection conn = DBConnectionManager.GetConnection();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update BnkMas Set BnkCd = @bankCode, BnkNm = @bankName, fInAct = 0 Where BnkKy = @bankKey", conn);
                    cmd.Parameters.AddWithValue("@bankCode", cmdBankCode.Text);
                    cmd.Parameters.AddWithValue("@bankName", cmbBankName.Text);
                    cmd.Parameters.AddWithValue("@bankKey", bankKey);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Update Successfully");
                    RefreshComboItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                cmdBankCode.Text = "";
                cmbBankName.Text = "";
                RefreshComboItems();
            }
            else
            {
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmdBankCode.Text))
            {
                MessageBox.Show("Select bank code");
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want DELETE this bank", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection conn = DBConnectionManager.GetConnection();
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("Update BnkMas Set fInAct='1',Status='D' Where BnkKy = @bankKey", conn);
                        cmd.Parameters.AddWithValue("@bankKey", bankKey);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show($"{bankCode} successfully Deleted");
                        cmdBankCode.Text = "";
                        cmbBankName.Text = "";
                        RefreshComboItems();
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

        //---------------------------------------------------------------------------------------------------
        //Key Handle

        private void cmdBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBankName.Focus();
            }
        }

        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate.Focus();
            }
        }

    }
}
