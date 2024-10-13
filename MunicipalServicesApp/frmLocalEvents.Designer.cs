using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    partial class frmLocalEvents
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel headerPanel;
        private Label lblTitle;
        private Panel mainPanel;
        private Panel searchPanel;
        private TextBox txtSearch;
        private Button btnSearch;
        private Panel filterPanel;
        private Label lblFilter;
        private ComboBox comboBoxCategories;
        private Label lblDate;
        private DateTimePicker dateTimePicker1;
        private Button btnSubmit;
        private ListView listViewEvents;
        private Panel buttonPanel;
        private Button btnBack;
        private Button btnRecommend;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
       // Form Designer code with aesthetic design
       
        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.UniqueCategories_Button = new MaterialSkin.Controls.MaterialButton();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRecommend = new System.Windows.Forms.Button();
            this.listViewEvents = new System.Windows.Forms.ListView();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.comboBoxCategories = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.SteelBlue;
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(750, 39);
            this.headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(750, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Local Events and Announcements";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mainPanel.Controls.Add(this.buttonPanel);
            this.mainPanel.Controls.Add(this.listViewEvents);
            this.mainPanel.Controls.Add(this.filterPanel);
            this.mainPanel.Controls.Add(this.searchPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 39);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(15, 13, 15, 13);
            this.mainPanel.Size = new System.Drawing.Size(750, 416);
            this.mainPanel.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.UniqueCategories_Button);
            this.buttonPanel.Controls.Add(this.btnBack);
            this.buttonPanel.Controls.Add(this.btnRecommend);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(15, 371);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.buttonPanel.Size = new System.Drawing.Size(720, 32);
            this.buttonPanel.TabIndex = 3;
            // 
            // UniqueCategories_Button
            // 
            this.UniqueCategories_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.UniqueCategories_Button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.UniqueCategories_Button.Depth = 0;
            this.UniqueCategories_Button.HighEmphasis = true;
            this.UniqueCategories_Button.Icon = null;
            this.UniqueCategories_Button.Location = new System.Drawing.Point(259, 2);
            this.UniqueCategories_Button.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.UniqueCategories_Button.MouseState = MaterialSkin.MouseState.HOVER;
            this.UniqueCategories_Button.Name = "UniqueCategories_Button";
            this.UniqueCategories_Button.NoAccentTextColor = System.Drawing.Color.Empty;
            this.UniqueCategories_Button.Size = new System.Drawing.Size(165, 36);
            this.UniqueCategories_Button.TabIndex = 2;
            this.UniqueCategories_Button.Text = "Unique Categories";
            this.UniqueCategories_Button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.UniqueCategories_Button.UseAccentColor = false;
            this.UniqueCategories_Button.UseVisualStyleBackColor = true;
            this.UniqueCategories_Button.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(0, 6);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 26);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            // 
            // btnRecommend
            // 
            this.btnRecommend.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRecommend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRecommend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecommend.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRecommend.ForeColor = System.Drawing.Color.White;
            this.btnRecommend.Location = new System.Drawing.Point(534, 6);
            this.btnRecommend.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecommend.Name = "btnRecommend";
            this.btnRecommend.Size = new System.Drawing.Size(186, 26);
            this.btnRecommend.TabIndex = 1;
            this.btnRecommend.Text = "Get Recommendations";
            this.btnRecommend.UseVisualStyleBackColor = false;
            // 
            // listViewEvents
            // 
            this.listViewEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEvents.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listViewEvents.FullRowSelect = true;
            this.listViewEvents.GridLines = true;
            this.listViewEvents.HideSelection = false;
            this.listViewEvents.Location = new System.Drawing.Point(15, 90);
            this.listViewEvents.Margin = new System.Windows.Forms.Padding(2);
            this.listViewEvents.Name = "listViewEvents";
            this.listViewEvents.Size = new System.Drawing.Size(720, 313);
            this.listViewEvents.TabIndex = 2;
            this.listViewEvents.UseCompatibleStateImageBehavior = false;
            this.listViewEvents.View = System.Windows.Forms.View.List;
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.btnSubmit);
            this.filterPanel.Controls.Add(this.dateTimePicker1);
            this.filterPanel.Controls.Add(this.lblDate);
            this.filterPanel.Controls.Add(this.comboBoxCategories);
            this.filterPanel.Controls.Add(this.lblFilter);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(15, 51);
            this.filterPanel.Margin = new System.Windows.Forms.Padding(2);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(720, 39);
            this.filterPanel.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(630, 0);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(90, 39);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Reset Filters";
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(375, 10);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 25);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Location = new System.Drawing.Point(300, 13);
            this.lblDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 19);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date:";
            // 
            // comboBoxCategories
            // 
            this.comboBoxCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategories.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxCategories.FormattingEnabled = true;
            this.comboBoxCategories.Location = new System.Drawing.Point(75, 10);
            this.comboBoxCategories.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCategories.Name = "comboBoxCategories";
            this.comboBoxCategories.Size = new System.Drawing.Size(151, 25);
            this.comboBoxCategories.TabIndex = 1;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFilter.Location = new System.Drawing.Point(0, 13);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(68, 19);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Category:";
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.btnSearch);
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(15, 13);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(2);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(720, 38);
            this.searchPanel.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(645, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 38);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(301, 29);
            this.txtSearch.TabIndex = 0;
            // 
            // frmLocalEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 455);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmLocalEvents";
            this.Text = "Local Events and Announcements";
            this.headerPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private MaterialSkin.Controls.MaterialButton UniqueCategories_Button;
    }

        #endregion
    }
