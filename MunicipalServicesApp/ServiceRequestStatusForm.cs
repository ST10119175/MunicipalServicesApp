using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using MaterialSkin.Controls;


//https://www.w3schools.com/dsa/index.php
//Klein, Prof.  Richard (2014). 8 Binary Search Trees | Introduction to Data Structures & Algorithms. [online] Wits.ac.za. Available at: https://courses.ms.wits.ac.za/~richard/IDSA-Notes-YouTube/bst.html [Accessed 14 Oct. 2024].
//Marcin Jamro (2018). C# data structures and algorithms : explore the possibilities of C# for developing a variety of efficient applications. Birmingham Packt.


namespace MunicipalServicesApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        // Data structures for managing service requests
        private AVLTree requestTree; // For sorted requests
        private MinHeap requestHeap;// For priority management
        private Dictionary<string, ServiceRequeststatus> requestLookup;// For quick ID lookups

       

        //public ServiceRequestStatusForm()
        //{
        //    InitializeComponent();

        //    // Initialize data structures
        //    requestTree = new AVLTree();
        //    requestHeap = new MinHeap();
        //    requestLookup = new Dictionary<string, ServiceRequeststatus>();

        //    // Initialize and populate the ListView
        //    InitializeListView();
        //    PopulateListView();
        //}


        public ServiceRequestStatusForm()
        {
            // Initialize all components first
            InitializeComponent();

            // Initialize requestLookup
            requestLookup = new Dictionary<string, ServiceRequeststatus>();

            // Set up the ListView columns explicitly
            SetupListViewColumns();

            // Populate the filter combo box
            SetupFilterComboBox();

            // Load data
            PopulateListView();

            ApplyCustomStyling();

            // Additional event handlers
            searchButton.Click += SearchButton_Click;
            filterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;
            refreshButton.Click += RefreshButton_Click;
        }

        private void SetupListViewColumns()
        {
            // Clear any existing columns
            requestListView.Columns.Clear();

            // Add columns with appropriate widths
            requestListView.Columns.Add("Request ID", 120, HorizontalAlignment.Left);
            requestListView.Columns.Add("Description", 300, HorizontalAlignment.Left);
            requestListView.Columns.Add("Status", 100, HorizontalAlignment.Left);
            requestListView.Columns.Add("Submission Date", 150, HorizontalAlignment.Left);
            requestListView.Columns.Add("Priority", 80, HorizontalAlignment.Right);
        }

        private void SetupFilterComboBox()
        {
            // Ensure filter combo box has default selection
            filterComboBox.Items.Clear();
            filterComboBox.Items.AddRange(new string[]
            {
        "All Requests",
        "Pending",
        "In Progress",
        "Completed",
        "Cancelled"
            });
            filterComboBox.SelectedIndex = 0; // Select "All Requests" by default
        }



        private void InitializeListView()
        {
            // Create and configure the ListView
            requestListView = new MaterialListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false
            };

            // Add columns to the ListView
            requestListView.Columns.Add("Request ID", 120);
            requestListView.Columns.Add("Description", 300);
            requestListView.Columns.Add("Status", 100);
            requestListView.Columns.Add("Submission Date", 150);
            requestListView.Columns.Add("Priority", 80);
           

            // Add event handler for item double-click
            requestListView.DoubleClick += RequestListView_DoubleClick;

            // Add the ListView to the form
            this.Controls.Add(requestListView);
        }

        private void PopulateListView()
        {
            try
            {
                // Clear existing items
                requestListView.Items.Clear();

                // Prepare file path
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceRequests.csv");

                // Log the exact file path being used
                Console.WriteLine($"Attempting to load CSV from: {filePath}");

                // Check if file exists
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"CSV file not found at: {filePath}",
                        "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Read all lines from the CSV
                var lines = File.ReadAllLines(filePath);

                // Skip header line
                var dataLines = lines.Skip(1).ToList();

                // Log number of data lines
                Console.WriteLine($"Found {dataLines.Count} data lines in CSV");

                // Clear existing data structures
                requestTree = new AVLTree();
                requestHeap = new MinHeap();
                requestLookup.Clear();

                // Populate ListView and data structures
                foreach (var line in dataLines)
                {
                    var parts = line.Split(',');

                    // Validate line has correct number of parts
                    if (parts.Length != 5)
                    {
                        Console.WriteLine($"Skipping invalid line: {line}");
                        continue;
                    }

                    try
                    {
                        var request = new ServiceRequeststatus
                        {
                            Id = parts[0].Trim(),
                            Description = parts[1].Trim(),
                            Status = parts[2].Trim(),
                            SubmissionDate = DateTime.Parse(parts[3].Trim()),
                            Priority = int.Parse(parts[4].Trim())
                        };

                        // Add to ListView
                        ListViewItem item = new ListViewItem(new string[]
                        {
                    request.Id,
                    request.Description,
                    request.Status,
                    request.SubmissionDate.ToString("yyyy-MM-dd"),
                    request.Priority.ToString()
                        });
                        requestListView.Items.Add(item);

                        // Populate data structures
                        requestTree.Insert(request);
                        requestHeap.Insert(request);
                        requestLookup[request.Id] = request;
                    }
                    catch (Exception lineEx)
                    {
                        Console.WriteLine($"Error processing line: {line}");
                        Console.WriteLine($"Exception: {lineEx.Message}");
                    }
                }

                // Update status
                statusLabel.Text = $"Loaded {requestListView.Items.Count} service requests.";

                // Auto-resize columns
                requestListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                // Detailed error logging
                MessageBox.Show($"Error loading service requests:\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Log to console for debugging
                Console.WriteLine($"Full Exception: {ex}");
            }
        }

        private void PopulatelistView()
        {
            try
            {
                // Ensure the ListView is clear
                if (requestListView != null)
                {
                    requestListView.Items.Clear();
                    requestListView.Columns.Clear();
                }
                else
                {
                    // If ListView doesn't exist, initialize it
                    requestListView = new MaterialListView
                    {
                        Dock = DockStyle.Fill,
                        View = View.Details,
                        FullRowSelect = true,
                        GridLines = true,
                        MultiSelect = false
                    };
                    this.Controls.Add(requestListView);
                }

                // Add columns with appropriate widths
                requestListView.Columns.Add("Request ID", 120, HorizontalAlignment.Left);
                requestListView.Columns.Add("Description", 300, HorizontalAlignment.Left);
                requestListView.Columns.Add("Status", 100, HorizontalAlignment.Left);
                requestListView.Columns.Add("Submission Date", 150, HorizontalAlignment.Left);
                requestListView.Columns.Add("Priority", 80, HorizontalAlignment.Right);

                // Load CSV Data
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceRequests.csv");
                var serviceRequests = LoadServiceRequests(filePath);

                // Debug: Check if any requests were loaded
                if (serviceRequests == null || serviceRequests.Count == 0)
                {
                    MessageBox.Show("No service requests found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Populate ListView with service requests
                foreach (var request in serviceRequests)
                {
                    // Create a new ListViewItem for each request
                    ListViewItem item = new ListViewItem(new string[]
                    {
                request.Id,
                request.Description,
                request.Status,
                request.SubmissionDate.ToString("yyyy-MM-dd"),
                request.Priority.ToString()
                    });

                    // Add the item to the ListView
                    requestListView.Items.Add(item);

                    // Populate data structures
                    requestTree.Insert(request);
                    requestHeap.Insert(request);
                    requestLookup[request.Id] = request;
                }

                // Auto-resize columns to fit content
                requestListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                // Update status label
                statusLabel.Text = $"Loaded {serviceRequests.Count} service requests.";
            }
            catch (Exception ex)
            {
                // More detailed error logging
                MessageBox.Show($"Error populating ListView: {ex.Message}\nStack Trace: {ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //private void InitializeUI()
        //{
        //    this.components = new System.ComponentModel.Container();

        //    // Form properties
        //    this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(1200, 800);
        //    this.MinimumSize = new System.Drawing.Size(1000, 700);
        //    this.Text = "Service Request Status";
        //    this.StartPosition = FormStartPosition.CenterScreen;

        //    // Initialize top panel
        //    this.topPanel = new Panel
        //    {
        //        Dock = DockStyle.Top,
        //        Height = 100,
        //        BackColor = Color.FromArgb(240, 240, 240),
        //        Padding = new Padding(20)
        //    };

        //    // Title Label
        //    this.titleLabel = new Label
        //    {
        //        Text = "Service Request Status",
        //        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
        //        AutoSize = true,
        //        Location = new Point(30, 20)
        //    };

        //    // Search TextBox
        //    this.searchTextBox = new TextBox
        //    {
        //        Size = new Size(250, 25),
        //        Location = new Point(30, 60)
        //    };

        //    // Placeholder Label
        //    this.placeholderLabel = new Label
        //    {
        //        Text = "Enter request ID or keywords...",
        //        ForeColor = Color.Gray,
        //        Location = new Point(35, 63),
        //        AutoSize = true,
        //        BackColor = Color.White
        //    };
        //    this.placeholderLabel.Click += PlaceholderLabel_Click;

        //    // Add event handlers for searchTextBox to manage placeholder visibility
        //    this.searchTextBox.GotFocus += SearchTextBox_GotFocus;
        //    this.searchTextBox.LostFocus += SearchTextBox_LostFocus;

        //    // Search Button
        //    this.searchButton = new Button
        //    {
        //        Text = "Search",
        //        Size = new Size(80, 25),
        //        Location = new Point(290, 60),
        //        BackColor = Color.FromArgb(0, 120, 215),
        //        ForeColor = Color.White,
        //        FlatStyle = FlatStyle.Flat
        //    };
        //    this.searchButton.Click += SearchButton_Click;

        //    // Filter ComboBox
        //    this.filterComboBox = new ComboBox
        //    {
        //        Size = new Size(150, 25),
        //        Location = new Point(380, 60)
        //    };
        //    this.filterComboBox.Items.AddRange(new object[]
        //    {
        //        "All Requests",
        //        "Pending",
        //        "In Progress",
        //        "Completed",
        //        "Cancelled"
        //    });
        //    this.filterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;

        //    // Refresh Button
        //    this.refreshButton = new Button
        //    {
        //        Text = "↻ Refresh",
        //        Size = new Size(100, 25),
        //        Location = new Point(540, 60),
        //        BackColor = Color.FromArgb(240, 240, 240),
        //        FlatStyle = FlatStyle.Flat
        //    };
        //    this.refreshButton.Click += RefreshButton_Click;

        //    // Initialize main panel
        //    this.mainPanel = new Panel
        //    {
        //        Dock = DockStyle.Fill,
        //        Padding = new Padding(20)
        //    };

        //    // Initialize ListView
        //    this.requestListView = new ListView
        //    {
        //        Dock = DockStyle.Fill,
        //        View = View.Details,
        //        FullRowSelect = true,
        //        GridLines = true,
        //        MultiSelect = false
        //    };

        //    // Add columns to ListView
        //    this.requestListView.Columns.Add("Request ID", 120);
        //    this.requestListView.Columns.Add("Description", 300);
        //    this.requestListView.Columns.Add("Status", 100);
        //    this.requestListView.Columns.Add("Priority", 80);
        //    this.requestListView.Columns.Add("Submission Date", 150);
        //    this.requestListView.Columns.Add("Last Updated", 150);
        //    this.requestListView.Columns.Add("Assigned To", 150);

        //    // Status Strip
        //    this.statusStrip = new StatusStrip();
        //    this.statusLabel = new ToolStripStatusLabel
        //    {
        //        Text = "Ready"
        //    };
        //    this.statusStrip.Items.Add(this.statusLabel);

        //    // Add controls to panels
        //    this.topPanel.Controls.Add(this.titleLabel);
        //    this.topPanel.Controls.Add(this.searchTextBox);
        //    this.topPanel.Controls.Add(this.placeholderLabel);
        //    this.topPanel.Controls.Add(this.searchButton);
        //    this.topPanel.Controls.Add(this.filterComboBox);
        //    this.topPanel.Controls.Add(this.refreshButton);
        //    this.mainPanel.Controls.Add(this.requestListView);

        //    // Add panels to form
        //    this.Controls.Add(this.mainPanel);
        //    this.Controls.Add(this.topPanel);
        //    this.Controls.Add(this.statusStrip);

        //    // Form Icon and Window State Settings
        //    this.Icon = SystemIcons.Application;
        //    this.MinimizeBox = true;
        //    this.MaximizeBox = true;
        //}

        //private void ShowServiceRequestStatus()
        //{
        //    Form statusForm = new Form
        //    {
        //        Size = new Size(1000, 700),
        //        Text = "Service Request Status"
        //    };

        //    TableLayoutPanel mainPanel = new TableLayoutPanel
        //    {
        //        Dock = DockStyle.Fill,
        //        RowCount = 2,
        //        ColumnCount = 2
        //    };

        //    // Request List
        //    ListView requestList = new ListView
        //    {
        //        Dock = DockStyle.Fill,
        //        View = View.Details
        //    };
        //    requestList.Columns.Add("ID", 100);
        //    requestList.Columns.Add("Description", 300);
        //    requestList.Columns.Add("Status", 100);
        //    requestList.Columns.Add("Submission Date", 150);
        //    requestList.Columns.Add("Priority", 100);

        //    // Search Panel
        //    Panel searchPanel = new Panel { Dock = DockStyle.Top };
        //    TextBox searchBox = new TextBox { Width = 200 };
        //    Button searchBtn = new Button { Text = "Search" };
        //    searchPanel.Controls.AddRange(new Control[] { searchBox, searchBtn });

        //    mainPanel.Controls.Add(searchPanel, 0, 0);
        //    mainPanel.Controls.Add(requestList, 0, 1);

        //    statusForm.Controls.Add(mainPanel);
        //    statusForm.ShowDialog();
        //}

        private void ShowServiceRequestStatus()
        {
            Form statusForm = new Form
            {
                Size = new Size(2000, 1200),
                Text = "Service Request Status"
            };

            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2
            };

            // Request List
            ListView requestList = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details
            };
            requestList.Columns.Add("ID", 100);
            requestList.Columns.Add("Description", 300);
            requestList.Columns.Add("Status", 100);
            requestList.Columns.Add("Submission Date", 150);
            requestList.Columns.Add("Priority", 100);

            // Load CSV Data
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceRequests.csv");
            var serviceRequests = LoadServiceRequests(filePath);

            // Populate ListView
            foreach (var request in serviceRequests)
            {
                var listItem = new ListViewItem(request.Id);
                listItem.SubItems.Add(request.Description);
                listItem.SubItems.Add(request.Status);
                listItem.SubItems.Add(request.SubmissionDate.ToString("yyyy-MM-dd"));
                listItem.SubItems.Add(request.Priority.ToString());
                requestList.Items.Add(listItem);

                // Insert into data structures for advanced usage
                requestTree.Insert(request);
                requestHeap.Insert(request);
                requestLookup[request.Id] = request;
            }

            // Search Panel
            Panel searchPanel = new Panel { Dock = DockStyle.Top };
            TextBox searchBox = new TextBox { Width = 200 };
            Button searchBtn = new Button { Text = "Search" };
            searchPanel.Controls.AddRange(new Control[] { searchBox, searchBtn });

            searchBtn.Click += (s, e) =>
            {
                string searchId = searchBox.Text;
                if (requestLookup.TryGetValue(searchId, out var result))
                {
                    MessageBox.Show($"Found: {result.Description}, Status: {result.Status}", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Request not found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            mainPanel.Controls.Add(searchPanel, 0, 0);
            mainPanel.Controls.Add(requestList, 0, 1);

            statusForm.Controls.Add(mainPanel);
            statusForm.ShowDialog();
        }

        private List<ServiceRequeststatus> LoadServiceRequests(string filePath)
        {
            var serviceRequests = new List<ServiceRequeststatus>();

            try
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines.Skip(1)) // Skip header line
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 5)
                        {
                            var request = new ServiceRequeststatus
                            {
                                Id = parts[0],
                                Description = parts[1],
                                Status = parts[2],
                                SubmissionDate = DateTime.Parse(parts[3]),
                                Priority = int.Parse(parts[4])
                            };
                            serviceRequests.Add(request);


                        }
                        else
                        {
                            // Log or handle the case where the line does not have exactly 5 parts

                            MessageBox.Show($"Invalid line format: {line}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("Request loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Log or handle the case where the file does not exist


                    MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message

                MessageBox.Show($"Error loading service requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return serviceRequests;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextBox.Text.Trim().ToLower();

            requestListView.Items.Clear();
            foreach (var request in requestLookup.Values)
            {
                if (string.IsNullOrEmpty(searchTerm) ||
                    request.Id.ToLower().Contains(searchTerm) ||
                    request.Description.ToLower().Contains(searchTerm))
                {
                    var item = new ListViewItem(request.Id);
                    item.SubItems.Add(request.Description);
                    item.SubItems.Add(request.Status);
                    item.SubItems.Add(request.SubmissionDate.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(request.Priority.ToString());
                    requestListView.Items.Add(item);
                }
            }

            statusLabel.Text = $"Search completed. {requestListView.Items.Count} results found.";
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = filterComboBox.SelectedItem?.ToString() ?? "All Requests";

            requestListView.Items.Clear();
            if (requestLookup != null)
            {
                foreach (var request in requestLookup.Values)
                {
                    if (selectedStatus == "All Requests" || request.Status == selectedStatus)
                    {
                        var item = new ListViewItem(request.Id);
                        item.SubItems.Add(request.Description);
                        item.SubItems.Add(request.Status);
                        item.SubItems.Add(request.SubmissionDate.ToString("yyyy-MM-dd"));
                        item.SubItems.Add(request.Priority.ToString());
                        requestListView.Items.Add(item);
                    }
                }
            }

            statusLabel.Text = $"Filter applied. {requestListView.Items.Count} results found.";
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            requestListView.Items.Clear();  
            statusLabel.Text = "Refreshing...";
            PopulateListView();
            statusLabel.Text = "Data refreshed";
        }

        private void RequestListView_DoubleClick(object sender, EventArgs e)
        {
            if (requestListView.SelectedItems.Count > 0)
            {
                // Show detailed view of selected request
                string requestId = requestListView.SelectedItems[0].Text;
                ShowRequestDetails(requestId);
            }
        }

        private void ShowRequestDetails(string requestId)
        {
            if (requestLookup.TryGetValue(requestId, out var request))
            {
                Form detailsForm = new Form
                {
                    Text = $"Request {requestId} Details",
                    Size = new Size(400, 300),
                    StartPosition = FormStartPosition.CenterScreen
                };

                TableLayoutPanel detailsPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 6
                };

                string[] labels = { "Request ID", "Description", "Status", "Submission Date", "Priority", "Last Updated" };
                string[] values = {
            request.Id,
            request.Description,
            request.Status,
            request.SubmissionDate.ToString("yyyy-MM-dd"),
            request.Priority.ToString(),
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

                for (int i = 0; i < labels.Length; i++)
                {
                    detailsPanel.Controls.Add(new Label { Text = labels[i], Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) }, 0, i);
                    detailsPanel.Controls.Add(new Label { Text = values[i] }, 1, i);
                }

                detailsForm.Controls.Add(detailsPanel);
                detailsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler methods
        private void PlaceholderLabel_Click(object sender, EventArgs e)
        {
            this.searchTextBox.Focus();
        }

        private void SearchTextBox_GotFocus(object sender, EventArgs e)
        {
            this.placeholderLabel.Visible = false;
        }

        private void SearchTextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.searchTextBox.Text))
            {
                this.placeholderLabel.Visible = true;
            }
        }


        private void ApplyCustomStyling()
        {
            // Apply custom styling to the form
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Font = new Font("Segoe UI", 10F);
            // Apply custom styling to the ListView
            requestListView.BackColor = Color.White;
            requestListView.ForeColor = Color.Black;
            requestListView.Font = new Font("Segoe UI", 9F);
            // Apply custom styling to the status label
            statusLabel.BackColor = Color.FromArgb(240, 240, 240);
            statusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }



        private void button1_Click(object sender, EventArgs e)
        {

            Button advancedFilterButton = new Button
            {
                Text = "Advanced Filters",
                Size = new Size(120, 25),
                Location = new Point(660, 60) // Adjust based on your form layout
            };

            advancedFilterButton.Click += (s, x) => ApplyAdvancedFilter();

            this.Controls.Add(advancedFilterButton);
        }

        private void ApplyAdvancedFilter()
        {
            // Create advanced filter form
            Form filterForm = new Form
            {
                Text = "Advanced Request Filters",
                Size = new Size(400, 500),
                StartPosition = FormStartPosition.CenterScreen
            };

            // Create filter controls
            GroupBox priorityGroup = new GroupBox
            {
                Text = "Priority Range",
                Location = new Point(20, 20),
                Size = new Size(350, 100)
            };

            TrackBar minPriorityTrack = new TrackBar
            {
                Minimum = 1,
                Maximum = 5,
                Location = new Point(20, 30),
                Size = new Size(300, 45),
                TickStyle = TickStyle.Both
            };

            TrackBar maxPriorityTrack = new TrackBar
            {
                Minimum = 1,
                Maximum = 5,
                Location = new Point(20, 60),
                Size = new Size(300, 45),
                TickStyle = TickStyle.Both
            };

            Label minPriorityLabel = new Label
            {
                Text = "Min Priority: 1",
                Location = new Point(20, 100)
            };

            Label maxPriorityLabel = new Label
            {
                Text = "Max Priority: 5",
                Location = new Point(20, 130)
            };

            DateTimePicker startDatePicker = new DateTimePicker
            {
                Location = new Point(20, 200),
                Size = new Size(350, 25),
                Format = DateTimePickerFormat.Short,
                Text = "Start Date"
            };

            DateTimePicker endDatePicker = new DateTimePicker
            {
                Location = new Point(20, 250),
                Size = new Size(350, 25),
                Format = DateTimePickerFormat.Short,
                Text = "End Date"
            };

            CheckedListBox statusListBox = new CheckedListBox
            {
                Location = new Point(20, 300),
                Size = new Size(350, 100)
            };
            statusListBox.Items.AddRange(new string[]
            {
        "Pending",
        "In Progress",
        "Completed",
        "Cancelled"
            });

            Button applyFilterBtn = new Button
            {
                Text = "Apply Filters",
                Location = new Point(150, 420),
                Size = new Size(100, 30)
            };

            // Add controls to form
            priorityGroup.Controls.AddRange(new Control[]
            {
        minPriorityTrack,
        maxPriorityTrack,
        minPriorityLabel,
        maxPriorityLabel
            });

            filterForm.Controls.AddRange(new Control[]
            {
        priorityGroup,
        startDatePicker,
        endDatePicker,
        statusListBox,
        applyFilterBtn
            });

            // Event handlers for dynamic updates
            minPriorityTrack.Scroll += (s, e) =>
            {
                minPriorityLabel.Text = $"Min Priority: {minPriorityTrack.Value}";
                if (minPriorityTrack.Value > maxPriorityTrack.Value)
                    maxPriorityTrack.Value = minPriorityTrack.Value;
            };

            maxPriorityTrack.Scroll += (s, e) =>
            {
                maxPriorityLabel.Text = $"Max Priority: {maxPriorityTrack.Value}";
                if (maxPriorityTrack.Value < minPriorityTrack.Value)
                    minPriorityTrack.Value = maxPriorityTrack.Value;
            };

            // Apply filter logic
            applyFilterBtn.Click += (s, e) =>
            {
                int minPriority = minPriorityTrack.Value;
                int maxPriority = maxPriorityTrack.Value;
                DateTime startDate = startDatePicker.Value;
                DateTime endDate = endDatePicker.Value;

                var selectedStatuses = statusListBox.CheckedItems
                    .Cast<string>()
                    .ToList();

                // Clear existing items
                requestListView.Items.Clear();

                // Filter requests
                var filteredRequests = requestLookup.Values
                    .Where(r =>
                        r.Priority >= minPriority &&
                        r.Priority <= maxPriority &&
                        r.SubmissionDate >= startDate &&
                        r.SubmissionDate <= endDate &&
                        (selectedStatuses.Count == 0 || selectedStatuses.Contains(r.Status)))
                    .ToList();

                // Populate ListView with filtered requests
                foreach (var request in filteredRequests)
                {
                    var item = new ListViewItem(request.Id);
                    item.SubItems.Add(request.Description);
                    item.SubItems.Add(request.Status);
                    item.SubItems.Add(request.SubmissionDate.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(request.Priority.ToString());
                    requestListView.Items.Add(item);
                }

                statusLabel.Text = $"Advanced filter applied. {filteredRequests.Count} results found.";
                filterForm.Close();
            };

            filterForm.ShowDialog();
        }

       
    }

    // MinHeap implementation for priority management
    public class MinHeap
    {
        private List<ServiceRequeststatus> heap;

        public MinHeap()
        {
            heap = new List<ServiceRequeststatus>();
        }

        private int Parent(int i) => (i - 1) / 2;
        private int LeftChild(int i) => 2 * i + 1;
        private int RightChild(int i) => 2 * i + 2;

        private void Swap(int i, int j)
        {
            ServiceRequeststatus temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        public void Insert(ServiceRequeststatus request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        private void HeapifyUp(int i)
        {
            while (i > 0 && heap[i].CompareTo(heap[Parent(i)]) < 0)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
        }

        public ServiceRequeststatus ExtractMin()
        {
            if (heap.Count == 0) throw new InvalidOperationException("Heap is empty");

            ServiceRequeststatus min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
                HeapifyDown(0);

            return min;
        }

        private void HeapifyDown(int i)
        {
            int minIndex = i;
            int left = LeftChild(i);
            int right = RightChild(i);

            if (left < heap.Count && heap[left].CompareTo(heap[minIndex]) < 0)
                minIndex = left;

            if (right < heap.Count && heap[right].CompareTo(heap[minIndex]) < 0)
                minIndex = right;

            if (minIndex != i)
            {
                Swap(i, minIndex);
                HeapifyDown(minIndex);
            }
        }


        // Get top N priority requests
        public List<ServiceRequeststatus> GetTopNPriorityRequests(int n)
        {
            var topRequests = new List<ServiceRequeststatus>();
            var tempHeap = new List<ServiceRequeststatus>(heap);

            for (int i = 0; i < n && tempHeap.Count > 0; i++)
            {
                topRequests.Add(ExtractMin());
            }

            return topRequests;
        }

        // Peek at the minimum element without removing
        public ServiceRequeststatus Peek()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            return heap[0];
        }

        // Check if a specific request exists in the heap
        public bool Contains(ServiceRequeststatus request)
        {
            return heap.Contains(request);
        }

        // Get total number of requests in heap
        public int Count => heap.Count;
    }
}

