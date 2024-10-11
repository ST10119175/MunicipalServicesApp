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
    public partial class Local_Events_and_Announcements : Form
    {
        public Local_Events_and_Announcements()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Example of adding columns to the ListView
            listView1.View = View.Details;
            listView1.Columns.Add("Event Name", 150);
            listView1.Columns.Add("Date", 100);
            listView1.Columns.Add("Category", 100);

        }
    }
}
