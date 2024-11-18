using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Button btnServiceStatus;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Label Title; 



        public Form1()
        {
            InitializeComponent();
            //ApplyCustomStyling();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            // Open the Report Issues Form
            ReportIssuesForm reportIssuesForm = new ReportIssuesForm();
            reportIssuesForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {

            // Open the Report Issues Form
            ReportIssuesForm reportIssuesForm = new ReportIssuesForm();
            reportIssuesForm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            //open frmLocalEvents form
            frmLocalEvents frmLocalEvents = new frmLocalEvents();
            frmLocalEvents.ShowDialog();

        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            // Open the Service Status Form
            ServiceRequestStatusForm serviceRequestStatusForm = new ServiceRequestStatusForm();
            serviceRequestStatusForm.ShowDialog();

        }
    }
}
