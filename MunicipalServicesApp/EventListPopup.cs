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
    public partial class EventListPopup : Form
    {
        public EventListPopup(HashSet<string> uniqueCategories, HashSet<DateTime> uniqueDates)
        {
            InitializeComponent();
            PopulateLists(uniqueCategories, uniqueDates);
        }

        private void PopulateLists(HashSet<string> uniqueCategories, HashSet<DateTime> uniqueDates)
        {
            listBoxCategories.Items.Clear();
            listBoxDates.Items.Clear();

            foreach (var category in uniqueCategories)
            {
                listBoxCategories.Items.Add(category);
            }

            foreach (var date in uniqueDates)
            {
                listBoxDates.Items.Add(date.ToString("dd MMM yyyy"));
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the popup form
        }
    }
}
