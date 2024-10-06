using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public class Issue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaPath { get; set; }
        public DateTime SubmittedAt { get; set; }
    }

    public partial class ReportIssuesForm : Form
    {
        List<Issue> issues = new List<Issue>();
    }
}
