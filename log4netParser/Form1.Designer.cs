using log4netParser.Controls;

namespace log4netParser {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.closeTabButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.loggerView1 = new log4netParser.Controls.LoggerView();
            this.mainLogTabPage = new System.Windows.Forms.TabPage();
            this.mainLogEntryView = new log4netParser.Controls.LogEntryView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.mainLogTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchButton);
            this.panel1.Controls.Add(this.searchTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.closeTabButton);
            this.panel1.Controls.Add(this.browseButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(948, 42);
            this.panel1.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(651, 11);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 6;
            this.searchButton.Text = "Search";
            this.toolTip1.SetToolTip(this.searchButton, "Click to perform filtering on message.");
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(457, 13);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(188, 20);
            this.searchTextBox.TabIndex = 5;
            this.toolTip1.SetToolTip(this.searchTextBox, "Enter a search term to filter on the message column. Clear to match all.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search";
            // 
            // closeTabButton
            // 
            this.closeTabButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeTabButton.Location = new System.Drawing.Point(930, 22);
            this.closeTabButton.Name = "closeTabButton";
            this.closeTabButton.Size = new System.Drawing.Size(18, 20);
            this.closeTabButton.TabIndex = 3;
            this.closeTabButton.Text = "x";
            this.toolTip1.SetToolTip(this.closeTabButton, "Click to close the tab (cannot close Group by logger and All entries tabs)");
            this.closeTabButton.UseVisualStyleBackColor = true;
            this.closeTabButton.Click += new System.EventHandler(this.closeTabButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(328, 11);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse...";
            this.toolTip1.SetToolTip(this.browseButton, "Click to parse the selected logfile");
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Logfile";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(268, 20);
            this.textBox1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBox1, "Current logfile");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(948, 553);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.mainLogTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(948, 553);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.loggerView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(940, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Group by logger";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // loggerView1
            // 
            this.loggerView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loggerView1.Location = new System.Drawing.Point(3, 3);
            this.loggerView1.Name = "loggerView1";
            this.loggerView1.Size = new System.Drawing.Size(934, 521);
            this.loggerView1.TabIndex = 2;
            // 
            // mainLogTabPage
            // 
            this.mainLogTabPage.Controls.Add(this.mainLogEntryView);
            this.mainLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainLogTabPage.Name = "mainLogTabPage";
            this.mainLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainLogTabPage.Size = new System.Drawing.Size(940, 527);
            this.mainLogTabPage.TabIndex = 0;
            this.mainLogTabPage.Text = "All entries";
            this.mainLogTabPage.UseVisualStyleBackColor = true;
            // 
            // mainLogEntryView
            // 
            this.mainLogEntryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLogEntryView.IsMainLog = true;
            this.mainLogEntryView.Location = new System.Drawing.Point(3, 3);
            this.mainLogEntryView.Name = "mainLogEntryView";
            this.mainLogEntryView.Size = new System.Drawing.Size(934, 521);
            this.mainLogEntryView.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 42);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(948, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(log4netParser.Logger);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "error.log";
            this.openFileDialog1.Title = "Select logfile to open";
            // 
            // Form1
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 598);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "log4net log parser";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.mainLogTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.BindingSource bindingSource2;
		private System.Windows.Forms.Button closeTabButton;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage mainLogTabPage;
        private LogEntryView mainLogEntryView;
		private System.Windows.Forms.ToolTip toolTip1;
        private Controls.LoggerView loggerView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

