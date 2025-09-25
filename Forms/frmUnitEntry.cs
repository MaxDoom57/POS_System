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
    public partial class frmUnitEntry : Form
    {
        private int childFormNumber = 0;

        //Define Variables
        private SqlConnection sqlConnection;

        public frmUnitEntry()
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
        string query;
        string keyPress =""; //what key press the user (Add or Edit)
        int unitKey;
        int unitKeyBeforeEdit;
        private bool _unitTextChanged = true;

        private void LoadUnits()
        {
            try
            {
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select UNIT from UnitCnv where fInAct = '0' Order By Unit";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbUnit.Items.Add(reader["UNIT"].ToString());
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadUnitTypes()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select distinct UnitTyp from UnitCnv Order By UnitTyp ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbUnitType.Items.Add(reader["UnitTyp"].ToString());
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDescription()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select Des from UnitCnv WHERE unitky = @unitKey ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@unitKy", unitKey.ToString());
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        txtDescription.Text = reader["Des"].ToString();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool isUnitAlready (string unit)
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select UNIT from UnitCnv where fInAct = '0' Order By Unit";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["UNIT"].ToString().Equals(unit, StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }


        //--------------------------------------------------------------------------------------------------------------------------------------

        private void frmUnitEntry_Load(object sender, EventArgs e)
        {
            LoadUnits();
            LoadUnitTypes();

            btnDelete.Enabled = false;
            btnSave.Enabled = false;
        }

        private void cmbUnit_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (UserSession.Instance.GetPermission("mnuunitentry")!= null && UserSession.Instance.GetPermission("mnuunitentry").CanCreateNew)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                keyPress = "Add";
                _unitTextChanged = false;
            }
            else
            {
                MessageBox.Show("Access is denied");
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (UserSession.Instance.GetPermission("mnuunitentry") != null && UserSession.Instance.GetPermission("mnuunitentry").CanUpdate)
            {
                _unitTextChanged = false;
                try
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        query = "Select unitky,UNIT,UnitTyp,Des from UnitCnv where unit =@unit";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@unit", cmbUnit.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                unitKeyBeforeEdit = Convert.ToInt32(reader["unitky"]);
                            }
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                keyPress = "Edit";
            }
            else
            {
                MessageBox.Show("Access is denied");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;

            cmbUnit.Text = "";
            cmbUnitType.Text = "";
            txtDescription.Text = "";
            keyPress = "";
            cmbUnit.Items.Clear();
            cmbUnitType.Items.Clear();
            _unitTextChanged = true;

            LoadUnits();
            LoadUnitTypes();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbUnit.Text =="")
            {
                MessageBox.Show("Unit is Must");
                cmbUnit.Focus();
            }
            else if (cmbUnitType.Text =="")
            {
                MessageBox.Show("Unit Type is Must");
                cmbUnitType.Focus();
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Description is Must");
                txtDescription.Focus();
            }
            else if (keyPress == "Add")
            {
                if (isUnitAlready(cmbUnit.Text))
                {
                    MessageBox.Show("Unit is already exist");
                }
                else
                {
                    try
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            query = "insert into UnitCnv( Unit, UnitTyp, Des) values(@unit, @unitType, @description)";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@unit",cmbUnit.Text);
                                cmd.Parameters.AddWithValue("@unitType", cmbUnitType.Text);
                                cmd.Parameters.AddWithValue("@description", txtDescription.Text);

                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                            btnCancel_Click(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            else if (keyPress == "Edit")
            {
                if (cmbUnit.Text == "")
                {
                    MessageBox.Show("Select unit code");
                }
                else if (isUnitAlready(cmbUnit.Text))
                {
                    MessageBox.Show("Unit is already exist");
                }
                else
                {
                    try
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            query = "update Unitcnv Set UNIT = @unit, UnitTyp = @unitType, des = @description WHERE unitky = @unitKey";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@unit", cmbUnit.Text);
                                cmd.Parameters.AddWithValue("@unitType", cmbUnitType.Text);
                                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@unitKey", unitKeyBeforeEdit);

                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                            btnCancel_Click(sender, e);
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
            if (UserSession.Instance.GetPermission("mnuunitentry") != null && UserSession.Instance.GetPermission("mnuunitentry").CanDelete)
            {
                DialogResult result = MessageBox.Show($"Do you want delete this unit {cmbUnit.Text}", this.Text, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (cmbUnit.Text == "")
                    {
                        MessageBox.Show("Unit is required");
                        cmbUnit.Focus();
                    }
                    else if (!isUnitAlready(cmbUnit.Text))
                    {
                        MessageBox.Show("Invalid unit");
                        cmbUnit.Focus();
                    }
                    else
                    {
                        try
                        {
                            using (SqlConnection conn = DBConnectionManager.GetConnection())
                            {
                                conn.Open();
                                query = "Update Unitcnv Set fInAct=1,Status='D'  Where Unit = @unit And fInAct=0";
                                SqlCommand cmd = new SqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@unit", cmbUnit.Text);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                btnCancel_Click(sender, e);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Access is denied");
            }
        }


        //Keys are handle here ---------------------------------------------------------------------------------------------------------------------------
        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbUnitType.Focus();
            }
        }

        private void cmbUnitType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescription.Focus();
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            if (_unitTextChanged)
                if (cmbUnit.Text != "")
                {
                    try
                    {
                        using (SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            query = "Select unitky,UNIT,UnitTyp,Des from UnitCnv where unit =@unit";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@unit", cmbUnit.Text);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    unitKey = Convert.ToInt32(reader["unitky"]);
                                    cmbUnitType.Text = reader["UnitTyp"].ToString();
                                    txtDescription.Text = reader["Des"].ToString();
                                }
                                else
                                {
                                    cmbUnitType.Text = "";
                                    txtDescription.Text = "";
                                }
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
                    cmbUnitType.Text = "";
                    txtDescription.Text = "";
                }
        }
    }
}
