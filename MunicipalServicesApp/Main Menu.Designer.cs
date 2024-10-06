namespace MunicipalServicesApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReportIssues
            // 
            this.btnReportIssues.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReportIssues.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportIssues.FlatAppearance.BorderSize = 0;
            this.btnReportIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportIssues.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnReportIssues.ForeColor = System.Drawing.Color.White;
            this.btnReportIssues.Location = new System.Drawing.Point(50, 250);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(250, 60);
            this.btnReportIssues.TabIndex = 2;
            this.btnReportIssues.Text = "Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = false;
            this.btnReportIssues.Click += new System.EventHandler(this.btnReportIssues_Click);
            // 
            // btnServiceStatus
            // 
            this.btnServiceStatus.BackColor = System.Drawing.Color.ForestGreen;
            this.btnServiceStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnServiceStatus.Enabled = false;
            this.btnServiceStatus.FlatAppearance.BorderSize = 0;
            this.btnServiceStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServiceStatus.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnServiceStatus.ForeColor = System.Drawing.Color.White;
            this.btnServiceStatus.Location = new System.Drawing.Point(275, 350);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(250, 60);
            this.btnServiceStatus.TabIndex = 1;
            this.btnServiceStatus.Text = "Service Request Status";
            this.btnServiceStatus.UseVisualStyleBackColor = false;
            // 
            // btnLocalEvents
            // 
            this.btnLocalEvents.BackColor = System.Drawing.Color.Orange;
            this.btnLocalEvents.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocalEvents.Enabled = false;
            this.btnLocalEvents.FlatAppearance.BorderSize = 0;
            this.btnLocalEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalEvents.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLocalEvents.ForeColor = System.Drawing.Color.White;
            this.btnLocalEvents.Location = new System.Drawing.Point(500, 250);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(250, 60);
            this.btnLocalEvents.TabIndex = 0;
            this.btnLocalEvents.Text = "Local Events";
            this.btnLocalEvents.UseVisualStyleBackColor = false;
            // 
            // Title
            // 
            this.Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.Title.ForeColor = System.Drawing.Color.DarkBlue;
            this.Title.Location = new System.Drawing.Point(20, 20);
            this.Title.Name = "Title";
            this.Title.Padding = new System.Windows.Forms.Padding(0, 40, 0, 40);
            this.Title.Size = new System.Drawing.Size(760, 23);
            this.Title.TabIndex = 3;
            this.Title.Text = "Welcome to City Services";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(275, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(266, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 50);
            this.label1.TabIndex = 5;
            this.label1.Text = "Municipal services";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLocalEvents);
            this.Controls.Add(this.btnServiceStatus);
            this.Controls.Add(this.btnReportIssues);
            this.Controls.Add(this.Title);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "City Services Portal";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}
    

    #endregion

//    private System.Windows.Forms.Button btnReportIssues;
//        private System.Windows.Forms.Button btnServiceStatus;
//        private System.Windows.Forms.Button btnLocalEvents;
//    }
//}

