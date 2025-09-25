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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace POS_System.Forms
{
    public partial class frmUserPermissionEntry : Form
    {
        private int childFormNumber = 0;

        public frmUserPermissionEntry()
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

        //--------------------------------------------------------------------------------------------------------------------------------------
        int userKey;
        string checkName;
        string query;

        private void LoadUserName()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select UsrId From UsrMas Where fInAct=0 Order by UsrId", conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbUserName.Items.Add(reader["UsrId"].ToString());
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetupDgvUserPermission()
        {
            dgvUserPermission.Columns.Clear();

            //Add ObjKy column
            DataGridViewTextBoxColumn colObjectKey = new DataGridViewTextBoxColumn();
            colObjectKey.Name = "colObjectKey";
            colObjectKey.HeaderText = "Object Key";
            colObjectKey.Visible = false;
            colObjectKey.Width = 60;
            dgvUserPermission.Columns.Add(colObjectKey);

            //Add Object Name column
            DataGridViewTextBoxColumn colObjectName = new DataGridViewTextBoxColumn();
            colObjectName.Name = "colObjectName";
            colObjectName.HeaderText = "Object Name";
            colObjectName.Width = 220;
            dgvUserPermission.Columns.Add(colObjectName);

            //Add Access column
            DataGridViewCheckBoxColumn colAccess = new DataGridViewCheckBoxColumn();
            colAccess.Name = "colAccess";
            colAccess.HeaderText = "Access";
            colAccess.Width = 60;
            dgvUserPermission.Columns.Add(colAccess);

            //Add New column
            DataGridViewCheckBoxColumn colNew = new DataGridViewCheckBoxColumn();
            colNew.Name = "colNew";
            colNew.HeaderText = "New";
            colNew.Width = 60;
            dgvUserPermission.Columns.Add(colNew);

            //Add Update column
            DataGridViewCheckBoxColumn colUpdate = new DataGridViewCheckBoxColumn();
            colUpdate.Name = "colUpdate";
            colUpdate.HeaderText = "Update";
            colUpdate.Width = 60;
            dgvUserPermission.Columns.Add(colUpdate);

            //Add Delete column
            DataGridViewCheckBoxColumn colDelete = new DataGridViewCheckBoxColumn();
            colDelete.Name = "colDelete";
            colDelete.HeaderText = "Delete";
            colDelete.Width = 60;
            dgvUserPermission.Columns.Add(colDelete);

            //Add Special column
            DataGridViewCheckBoxColumn colSpecial = new DataGridViewCheckBoxColumn();
            colSpecial.Name = "colSpecial";
            colSpecial.HeaderText = "Special";
            colSpecial.Width = 60;
            dgvUserPermission.Columns.Add(colSpecial);

            dgvUserPermission.AllowUserToAddRows = false;
            dgvUserPermission.AllowUserToResizeColumns = true;
            dgvUserPermission.AllowUserToResizeRows = true;

            foreach (DataGridViewColumn col in dgvUserPermission.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic; //enable sorting
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------
        private void frmUserPermissionEntry_Load(object sender, EventArgs e)
        {
            LoadUserName();
            SetupDgvUserPermission();
        }

        private void cmbUserName_SelectedValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            checkName = cmbUserName.Text;

            if (!string.IsNullOrEmpty(cmbUserName.Text) )
            {
                try
                {
                    SetupDgvUserPermission();
                    query = "Select ObjKy,ObjCap,0,0,0,0,0 From ObjMas Where fInAct=0 And ObjTyp='MNU' And  not ObjCap='-' Order By ObjCap";
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dgvUserPermission.Rows.Add(reader["ObjKy"], reader["ObjCap"], false, false, false, false, false);
                            }
                        }
                        conn.Close() ;

                    }

                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        query = "Select * From UsrObjAccQry Where UsrId=@userId";
                        using (SqlCommand cmd = new SqlCommand (query, conn))
                        {
                            cmd.Parameters.AddWithValue("@userId", cmbUserName.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreach (DataGridViewRow row in dgvUserPermission.Rows)
                                    {
                                        if (row.Cells["colObjectKey"].Value != null && row.Cells["colObjectKey"].Value.ToString() == reader["ObjKy"].ToString())
                                        {
                                            row.Cells["colAccess"].Value = Convert.ToBoolean(reader["fAcs"]);
                                            row.Cells["colNew"].Value = Convert.ToBoolean(reader["fNew"]);
                                            row.Cells["colUpdate"].Value = Convert.ToBoolean(reader["fUpdt"]);
                                            row.Cells["colDelete"].Value = Convert.ToBoolean(reader["fDel"]);
                                            row.Cells["colSpecial"].Value = Convert.ToBoolean(reader["fSp"]);
                                        }
                                    }
                                }
                            }
                        }
                        conn.Close();
                    }

                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        query = "Select UsrKy from UsrMas Where UsrId=@userId And fInAct=0";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@userId", cmbUserName.Text);
                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                userKey = Convert.ToInt32(result);
                            }
                            else
                            {
                                userKey = 0;
                                MessageBox.Show("Error, Invalid User Name");
                                cmbUserName.Text = "";
                                dgvUserPermission.Rows.Clear();
                            }
                        }
                        conn.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                dgvUserPermission.Rows.Clear();
                dgvUserPermission.Rows.Add();
                cmbUserName.Focus();
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbUserName_SelectedValueChanged(null, EventArgs.Empty);
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            List<int> comboColIndexes = new List<int>() { 2, 3, 4, 5, 6 };
            foreach (DataGridViewCell cell in dgvUserPermission.SelectedCells)
            {
                if (comboColIndexes.Contains(cell.ColumnIndex))
                {
                    cell.Value = true;
                }
            }
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            List<int> comboColIndexes = new List<int>() { 2, 3, 4, 5, 6 };
            foreach (DataGridViewCell cell in dgvUserPermission.SelectedCells)
            {
                if (comboColIndexes.Contains(cell.ColumnIndex))
                {
                    cell.Value = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            byte varAccess, varNew, varUpdate, varDelete, varSpecial;
            Cursor.Current = Cursors.WaitCursor;

            if ((cmbUserName.Text != "") && MessageBox.Show("Do you want to save the changes", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvUserPermission.Rows)
                {
                    if (row.Cells["colObjectKey"].Value != null)
                    {
                        var objectKey = row.Cells["colObjectKey"].Value;

                        if (!Convert.ToBoolean(row.Cells["colAccess"].Value) ) {varAccess = 0; } else { varAccess = 1; }
                        if (!Convert.ToBoolean(row.Cells["colNew"].Value) ) {varNew = 0; } else { varNew = 1; }
                        if (!Convert.ToBoolean(row.Cells["colUpdate"].Value) ) {varUpdate = 0; } else { varUpdate = 1; }
                        if (!Convert.ToBoolean(row.Cells["colDelete"].Value) ) {varDelete = 0; } else { varDelete = 1; }
                        if (!Convert.ToBoolean(row.Cells["colSpecial"].Value) ) {varSpecial = 0; } else { varSpecial = 1; }

                        using(SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            query = "SELECT ObjKy FROM UsrObj WHERE ObjKy = @objectKey AND UsrKy = @userKey";
                            using(SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@objectKey", objectKey);
                                cmd.Parameters.AddWithValue("@userKey", userKey);
                                using(SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    bool isRecordExists = reader.HasRows;
                                    reader.Close();
                                    if (!isRecordExists )
                                    {
                                        query = @"INSERT INTO UsrObj (ObjKy, UsrKy, fAcs, fNew, fUpdt, fDel, fSp) VALUES (@ObjKy, @UsrKy, @fAcs, @fNew, @fUpdt, @fDel, @fSp)";
                                        using(SqlCommand insertCmd = new SqlCommand(query, conn))
                                        {
                                            insertCmd.Parameters.AddWithValue("@ObjKy", objectKey);
                                            insertCmd.Parameters.AddWithValue("@UsrKy", userKey);
                                            insertCmd.Parameters.AddWithValue("@fAcs", varAccess);
                                            insertCmd.Parameters.AddWithValue("@fNew", varNew);
                                            insertCmd.Parameters.AddWithValue("@fUpdt", varUpdate);
                                            insertCmd.Parameters.AddWithValue("@fDel", varDelete);
                                            insertCmd.Parameters.AddWithValue("@fSp", varSpecial);

                                            insertCmd.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        query = @"UPDATE UsrObj SET fAcs = @fAcs, fNew = @fNew, fUpdt = @fUpdt, fDel = @fDel, fSp = @fSp WHERE ObjKy = @ObjKy AND UsrKy = @UsrKy";
                                        using (SqlCommand insertCmd = new SqlCommand(query, conn))
                                        {
                                            insertCmd.Parameters.AddWithValue("@ObjKy", objectKey);
                                            insertCmd.Parameters.AddWithValue("@UsrKy", userKey);
                                            insertCmd.Parameters.AddWithValue("@fAcs", varAccess);
                                            insertCmd.Parameters.AddWithValue("@fNew", varNew);
                                            insertCmd.Parameters.AddWithValue("@fUpdt", varUpdate);
                                            insertCmd.Parameters.AddWithValue("@fDel", varDelete);
                                            insertCmd.Parameters.AddWithValue("@fSp", varSpecial);

                                            insertCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            conn.Close();
                        }
                    }
                }
                dgvUserPermission.Rows.Clear();
            }
            else
            {
                dgvUserPermission.Rows.Clear();
                cmbUserName.Text = "";
                cmbUserName.Focus();
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
