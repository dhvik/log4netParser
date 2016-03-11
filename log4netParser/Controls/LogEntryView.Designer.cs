using System.Windows.Forms;

namespace log4netParser.Controls {
	partial class LogEntryView {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideLoggerXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterOnThreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new log4netParser.Controls.LoggerColoredDataGridView();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.threadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loggerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideLoggerXToolStripMenuItem,
            this.findEntryToolStripMenuItem,
            this.filterOnThreadToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 70);
            // 
            // hideLoggerXToolStripMenuItem
            // 
            this.hideLoggerXToolStripMenuItem.Name = "hideLoggerXToolStripMenuItem";
            this.hideLoggerXToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.hideLoggerXToolStripMenuItem.Text = "Hide Logger x";
            this.hideLoggerXToolStripMenuItem.Click += new System.EventHandler(this.hideLoggerXToolStripMenuItem_Click);
            // 
            // findEntryToolStripMenuItem
            // 
            this.findEntryToolStripMenuItem.Name = "findEntryToolStripMenuItem";
            this.findEntryToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.findEntryToolStripMenuItem.Text = "Find entry";
            this.findEntryToolStripMenuItem.Click += new System.EventHandler(this.findEntryToolStripMenuItem_Click);
            // 
            // filterOnThreadToolStripMenuItem
            // 
            this.filterOnThreadToolStripMenuItem.Name = "filterOnThreadToolStripMenuItem";
            this.filterOnThreadToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.filterOnThreadToolStripMenuItem.Text = "Filter on Thread";
            this.filterOnThreadToolStripMenuItem.Click += new System.EventHandler(this.filterOnThreadToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 351);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(668, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 354);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(668, 135);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timeDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn,
            this.Process,
            this.threadDataGridViewTextBoxColumn,
            this.loggerDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(668, 351);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss,fff";
            this.timeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            this.timeDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeDataGridViewTextBoxColumn.Width = 130;
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            this.levelDataGridViewTextBoxColumn.ReadOnly = true;
            this.levelDataGridViewTextBoxColumn.Width = 50;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.Width = 70;
            // 
            // threadDataGridViewTextBoxColumn
            // 
            this.threadDataGridViewTextBoxColumn.DataPropertyName = "Thread";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.threadDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.threadDataGridViewTextBoxColumn.HeaderText = "Thread";
            this.threadDataGridViewTextBoxColumn.Name = "threadDataGridViewTextBoxColumn";
            this.threadDataGridViewTextBoxColumn.ReadOnly = true;
            this.threadDataGridViewTextBoxColumn.Width = 40;
            // 
            // loggerDataGridViewTextBoxColumn
            // 
            this.loggerDataGridViewTextBoxColumn.DataPropertyName = "Logger";
            this.loggerDataGridViewTextBoxColumn.HeaderText = "Logger";
            this.loggerDataGridViewTextBoxColumn.Name = "loggerDataGridViewTextBoxColumn";
            this.loggerDataGridViewTextBoxColumn.ReadOnly = true;
            this.loggerDataGridViewTextBoxColumn.Width = 250;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(log4netParser.LogEntry);
            // 
            // LogEntryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "LogEntryView";
            this.Size = new System.Drawing.Size(668, 489);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.BindingSource bindingSource1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem hideLoggerXToolStripMenuItem;
        private ToolStripMenuItem findEntryToolStripMenuItem;
        private LoggerColoredDataGridView dataGridView1;
        private ToolStripMenuItem filterOnThreadToolStripMenuItem;
        private DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn loggerDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn threadDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn Process;
        private DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
    }
}
