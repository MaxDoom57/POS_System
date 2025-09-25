using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Custom_Components
{
    public class MyCustomDGV : DataGridView
    {
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Check if current column is "name"
                if (this.CurrentCell.OwningColumn.Name == "name")
                {
                    int row = this.CurrentCell.RowIndex;
                    int qtyCol = this.Columns["qty"].Index;

                    this.EndEdit(); // Commit current cell
                    this.CurrentCell = this.Rows[row].Cells[qtyCol];
                    this.BeginEdit(true); // Start editing "qty"

                    return true; // suppress default Enter behavior
                }
            }

            return base.ProcessDataGridViewKey(e);
        }
    }

}
