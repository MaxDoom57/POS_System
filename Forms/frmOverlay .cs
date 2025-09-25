using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS_System.Forms
{
    public partial class frmOverlay : Form
    {
        public frmOverlay(Form owner)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.BackColor = Color.Black;
            this.Opacity = 0.6;
            this.Owner = owner;
            this.Size = owner.Size;
            this.Location = owner.Location;
            this.TopMost = true;
        }

        // Optional: prevent interacting with overlay
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80; // WS_EX_TOOLWINDOW - prevents showing in Alt+Tab
                return cp;
            }
        }
    }
}
