using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;


using MaterialSkin;
using System.IO;
using System.Diagnostics;

namespace MunicipalServicesApp
{
    public partial class ReportIssuesForm : Form
    {
        string attachedFilePath;
        int progress = 0;



        public ReportIssuesForm()
        {
            InitializeComponent();
            ApplyCustomStyling();
            InitializeCategories();
            InitializeProgressBar();


        }

        private void InitializeProgressBar()
        {
            // Initialize progress bar settings (Microsoft, 2023)
            pbProgress.Minimum = 0;
            pbProgress.Maximum = 100;
            pbProgress.Value = 0;
        }


        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }

        private void UpdateProgressBar()
        {


            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) 
            { 
                progress += 25;
                Debug.WriteLine("Location text changed. Progress: " + progress);
            }
            if (cbCategory.SelectedIndex != -1) 
            { 
                progress += 25;
                Debug.WriteLine("Location category changed. Progress: " + progress);
            }
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text)) 
            { 
                progress += 25;
                Debug.WriteLine("Location Description changed. Progress: " + progress);
            }
            if (!string.IsNullOrWhiteSpace(attachedFilePath)) 
            { 
                progress += 25;
                Debug.WriteLine("Location text changed. Progress: " + progress);
            }

            pbProgress.Value = progress;
        }

        // Apply custom styling to the form and controls (Microsoft, 2023)
        private void ApplyCustomStyling()
        {
            // Form
            this.ClientSize = new Size(600, 650);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Padding = new Padding(20);
            this.BackColor = Color.WhiteSmoke;

            // Title
            Title.Text = "Report an Issue";
            Title.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            Title.ForeColor = Color.DarkBlue;
            Title.Dock = DockStyle.Top;
            Title.TextAlign = ContentAlignment.MiddleCenter;
            Title.Padding = new Padding(0, 20, 0, 20);

            // Location
            label1.Text = "Address";
            label1.Font = new Font("Segoe UI", 12F);
            label1.Dock = DockStyle.Top;
            label1.Padding = new Padding(0, 10, 0, 5);

            txtLocation.Multiline = true;
            txtLocation.Dock = DockStyle.Top;
            txtLocation.Height = 60;

            // Category
            label2.Text = "Category";
            label2.Font = new Font("Segoe UI", 12F);
            label2.Dock = DockStyle.Top;
            label2.Padding = new Padding(0, 10, 0, 5);

            cbCategory.Dock = DockStyle.Top;
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // Description
            label3.Text = "Description";
            label3.Font = new Font("Segoe UI", 12F);
            label3.Dock = DockStyle.Top;
            label3.Padding = new Padding(0, 10, 0, 5);

            rtbDescription.Dock = DockStyle.Top;
            rtbDescription.Height = 100;

            // Attach File Button
            btnAttachFile.Text = "Attach Files";
            btnAttachFile.Dock = DockStyle.Top;
            btnAttachFile.Padding = new Padding(0, 10, 0, 0);
            btnAttachFile.Height = 40;
            btnAttachFile.BackColor = Color.LightGray;
            btnAttachFile.FlatStyle = FlatStyle.Flat;
            btnAttachFile.FlatAppearance.BorderSize = 0;

            // Progress Bar
            pbProgress.Dock = DockStyle.Top;
            pbProgress.Height = 20;
            pbProgress.Margin = new Padding(0, 10, 0, 10);

            // Submit Button
            btnSubmit.Text = "Submit";
            btnSubmit.Dock = DockStyle.Top;
            btnSubmit.Height = 50;
            btnSubmit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSubmit.BackColor = Color.DodgerBlue;
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;

            // Back to Main Menu Button
            btnBackToMainMenu.Text = "Back to Main Menu";
            btnBackToMainMenu.Dock = DockStyle.Bottom;
            btnBackToMainMenu.Height = 40;
            btnBackToMainMenu.BackColor = Color.LightGray;
            btnBackToMainMenu.FlatStyle = FlatStyle.Flat;
            btnBackToMainMenu.FlatAppearance.BorderSize = 0;

            // Rearrange controls
            this.Controls.Clear();
            this.Controls.Add(btnBackToMainMenu);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(pbProgress);
            this.Controls.Add(btnAttachFile);
            this.Controls.Add(rtbDescription);
            this.Controls.Add(label3);
            this.Controls.Add(cbCategory);
            this.Controls.Add(label2);
            this.Controls.Add(txtLocation);
            this.Controls.Add(label1);
            this.Controls.Add(Title);
        }

        private void InitializeCategories()
        {
            // Initialize categories(Microsoft, 2023)
            cbCategory.Items.AddRange(new object[] { "Electric", "Refuse", "pothole" });
            cbCategory.SelectedIndex = -1; // No category selected by default
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SubmitIssue();
            }
        }


        private bool ValidateForm()
        {
            // Validate Location input: Ensure the user provides a location (IAmTimCorey,2022)
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                ShowValidationError("Please enter the location of the issue.");
                return false;
            }

            // Validate Category selection: Ensure the user selects a category 
            if (cbCategory.SelectedItem == null)
            {
                ShowValidationError("Please select a category for the issue.");
                return false;
            }

            // Validate Description input: Ensure the user provides a description 
            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                ShowValidationError("Please provide a description of the issue.");
                return false;
            }

            // Validate Media attachment: Ensure the user attaches a file (IAmTimCorey,2022)
            if (string.IsNullOrWhiteSpace(attachedFilePath))
            {
                ShowValidationError("Please attach a file or image related to the issue.");
                return false;
            }

            return true;
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void SubmitIssue()
        {
            try
            {
                // If all validations pass, create a new issue and add it to the list
                Issue newIssue = new Issue
                {
                    Location = txtLocation.Text,
                    Category = cbCategory.SelectedItem.ToString(),
                    Description = rtbDescription.Text,
                    MediaPath = attachedFilePath,
                    SubmittedAt = DateTime.Now
                };

                // Add the issue to the list (winforms,2021)
                issues.Add(newIssue);

                // Display success message (IAmTimCorey,2022)
                MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while submitting the issue: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            // Reset the form for the next entry
            txtLocation.Clear();
            cbCategory.SelectedIndex = -1; // Deselect any selected category
            rtbDescription.Clear();
            attachedFilePath = string.Empty; // Reset the file path

            // Reset the progress bar 
            pbProgress.Value = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize file dialog for selecting images or other files (winforms,2021)
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*"; // Filter for image files (Moyo, 2023)

                    // Check if user selected a file (winforms,2021)
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Store the selected file path 
                        attachedFilePath = openFileDialog.FileName;

                        // Display success message to user 
                        MessageBox.Show("File attached successfully: " + Path.GetFileName(attachedFilePath));

                        // Update progress bar to indicate file attachment 
                        pbProgress.Value = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while attaching the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

//    private void button1_Click(object sender, EventArgs e)
//        {
//            // Validate Location input: Ensure the user provides a location (Bhengu, 2023)
//            if (string.IsNullOrWhiteSpace(txtLocation.Text))
//            {
//                MessageBox.Show("Please enter the location of the issue.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return; // Prevent submission if location is empty
//            }

//            // Validate Category selection: Ensure the user selects a category (Moyo, 2023)
//            if (cbCategory.SelectedItem == null)
//            {
//                MessageBox.Show("Please select a category for the issue.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return; // Prevent submission if no category is selected
//            }

//            // Validate Description input: Ensure the user provides a description (Nkosi, 2024)
//            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
//            {
//                MessageBox.Show("Please provide a description of the issue.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return; // Prevent submission if description is empty
//            }

//            // Validate Media attachment: Ensure the user attaches a file (Williams, 2023)
//            if (string.IsNullOrWhiteSpace(attachedFilePath))
//            {
//                MessageBox.Show("Please attach a file or image related to the issue.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return; // Prevent submission if no file is attached
//            }

//            // If all validations pass, create a new issue and add it to the list
//            Issue newIssue = new Issue
//            {
//                Location = txtLocation.Text,
//                Category = cbCategory.SelectedItem.ToString(),
//                Description = rtbDescription.Text,
//                MediaPath = attachedFilePath,
//                SubmittedAt = DateTime.Now
//            };

//            // Add the issue to the list (Smith, 2023)
//            issues.Add(newIssue);

//            // Display success message (Bhengu, 2023)
//            MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

//            // Reset the form for the next entry
//            txtLocation.Clear();
//            cbCategory.SelectedIndex = -1; // Deselect any selected category
//            rtbDescription.Clear();
//            attachedFilePath = string.Empty; // Reset the file path

//            // Reset the progress bar (Nkosi, 2024)
//            pbProgress.Value = 100;

//        }

//        private void button1_Click_1(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void btnAttachFile_Click(object sender, EventArgs e)
//        {
//            // Initialize file dialog for selecting images or other files (Williams, 2023)
//            OpenFileDialog openFileDialog = new OpenFileDialog();
//            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*"; // Filter for image files (Moyo, 2023)

//            // Check if user selected a file (Smith, 2023)
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                // Store the selected file path (Bhengu, 2023)
//                attachedFilePath = openFileDialog.FileName;

//                // Display success message to user (Nkosi, 2024)
//                MessageBox.Show("File attached successfully.");

//            }


//        }
//    }


//}
