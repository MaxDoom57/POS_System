using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace POS_System.Forms
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            InitializeComponent();
        }

        public void LoadCodeEntryReport(ReportDocument report)
        {
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();
        }

        public void LoadStockAdditionReport(ReportDocument reportStockAddition)
        {
            crystalReportViewer1.ReportSource = reportStockAddition;
            crystalReportViewer1.Refresh();
        }

        public void LoadGrnReport(ReportDocument reportGrn)
        {
            crystalReportViewer1.ReportSource = reportGrn;
            crystalReportViewer1.Refresh();
        }
    }
}
