using System;
using System.IO;
using System.Windows.Forms;

namespace log4netParser {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			var logData = ParseLogfile(textBox1.Text);
			bindingSource1.DataSource = logData.Entries;
			bindingSource2.DataSource = logData.Loggers;
		}

		private LogData ParseLogfile(string filename) {
			var file = new FileInfo(filename);
			var parser = new Parser();
			using (var reader = new StreamReader(file.OpenRead())) {

				string line;
				while ((line = reader.ReadLine()) != null) {
					parser.ParseLine(line);
				}

			}
			parser.LogData.Analyze();
			return parser.LogData;
		}

		private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			try {
				var logger = dataGridView2.Rows[e.RowIndex].DataBoundItem as Logger;
				AddLoggerTab(logger);
			} catch (Exception exception) {
				MessageBox.Show(exception.Message+Environment.NewLine+exception.StackTrace);
			}
		}

		private void AddLoggerTab(Logger logger) {
			var tabPage = new TabPage(logger.Name);

			var timeColumn = new DataGridViewTextBoxColumn {
				DataPropertyName = "Time",
				HeaderText = "Time",
				Name = "timeDataGridViewTextBoxColumn",
				ReadOnly = true,
				Width = 130,
				DefaultCellStyle = new DataGridViewCellStyle() { Format = "yyyy-MM-dd HH:mm:ss,fff"}
			};
			var levelColumn = new DataGridViewTextBoxColumn {
				DataPropertyName = "Level",
				HeaderText = "Level",
				Name = "levelDataGridViewTextBoxColumn",
				ReadOnly = true,
				Width = 50
			};
			var threadColumn = new DataGridViewTextBoxColumn {
				DataPropertyName = "Thread",
				HeaderText = "Thread",
				Name = "threadDataGridViewTextBoxColumn",
				ReadOnly = true,
				Width = 40
			};
			var loggerColumn = new DataGridViewTextBoxColumn {
				DataPropertyName = "Logger",
				HeaderText = "Logger",
				Name = "loggerDataGridViewTextBoxColumn",
				ReadOnly = true,
				Width = 250
			};
			var messageColumn = new DataGridViewTextBoxColumn {
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				DataPropertyName = "Message",
				HeaderText = "Message",
				Name = "Message",
				ReadOnly = true
			};
			var dataGridView = new DataGridView {
				AllowUserToAddRows = false,
				AllowUserToDeleteRows = false,
				AutoGenerateColumns = false,
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
				Dock = DockStyle.Fill,
				Location = new System.Drawing.Point(3, 3),
				Name = "dataGridView",
				ReadOnly = true,
				RowHeadersVisible = false,
				Size = new System.Drawing.Size(934, 493),
				TabIndex = 0,
				DataSource = logger.Entries
			};

			dataGridView.Columns.AddRange(new DataGridViewColumn[] { timeColumn, levelColumn, threadColumn, loggerColumn, messageColumn });

			tabPage.Controls.Add(dataGridView);
			tabControl1.TabPages.Add(tabPage);
			tabControl1.SelectTab(tabPage);
		}

		private void closeTabButton_Click(object sender, EventArgs e) {
			//cannot close main tabs
			if (tabControl1.SelectedTab == tabPage1 || tabControl1.SelectedTab == tabPage2)
				return;
			tabControl1.TabPages.Remove(tabControl1.SelectedTab);
		}
	}
}
