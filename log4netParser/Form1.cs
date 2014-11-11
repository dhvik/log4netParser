using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using log4netParser.Controls;

namespace log4netParser {
	public partial class Form1 : Form {
		private LogData _currentData;
		/* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
		#region public Form1()
		/// <summary>
		/// Initializes a new instance of the <b>Form1</b> class.
		/// </summary>
		public Form1() {
			InitializeComponent();
			textBox1.Text = Settings.Instance.LastLoadedFile;
            loggerView1.AddLogger+=(sender, args) => AddLoggerTab(args.Logger);
		}
		#endregion
		/* *******************************************************************
		 *  Methods
		 * *******************************************************************/
		#region private void button1_Click(object sender, EventArgs e)
		/// <summary>
		/// This method is called when the button1's Click event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void button1_Click(object sender, EventArgs e) {
			var logData = ParseLogfile(textBox1.Text);
            SetViewModel(logData);
			_currentData = logData;
		}
		#endregion
        #region private void SetViewModel(LogData logData)
        /// <summary>
        /// Sets the supplied logData as the current view model
        /// </summary>
        /// <param name="logData"></param>
        private void SetViewModel(LogData logData) {
            logEntryView1.LogData = logData;
            loggerView1.LogData = logData;
        }
        #endregion
		#region private LogData ParseLogfile(string filename)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		private LogData ParseLogfile(string filename) {
			Settings.Instance.LastLoadedFile = filename;
			Settings.Instance.Save();
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
		#endregion
		#region private void AddLoggerTab(Logger logger)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		private void AddLoggerTab(Logger logger) {
			var tabPage = new TabPage(logger.Name);
			var view = new LogEntryView {
				LogEntires = logger.Entries,
				Dock = DockStyle.Fill
			};
			tabPage.Controls.Add(view);
			tabControl1.TabPages.Add(tabPage);
			tabControl1.SelectTab(tabPage);
		}
		#endregion
		#region private void closeTabButton_Click(object sender, EventArgs e)
		/// <summary>
		/// This method is called when the closeTabButton's Click event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void closeTabButton_Click(object sender, EventArgs e) {
			//cannot close main tabs
			var tabToClose = tabControl1.SelectedTab;
			if (tabToClose == tabPage1 || tabToClose == tabPage2)
				return;
			tabControl1.SuspendLayout();
			var selectedIndex = tabControl1.SelectedIndex;
			if (selectedIndex == (tabControl1.TabCount - 1)) {
				selectedIndex--;
			} else {
				selectedIndex++;
			}
			tabControl1.SelectedIndex = selectedIndex;
			tabControl1.TabPages.Remove(tabToClose);
			tabControl1.ResumeLayout();
		}
		#endregion
		#region private void searchButton_Click(object sender, EventArgs e)
		/// <summary>
		/// This method is called when the searchButton's Click event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void searchButton_Click(object sender, EventArgs e) {
			if (_currentData == null) {
				return;
			}
			var dataToDisplay = _currentData;
			var searchString = searchTextBox.Text;
			if (!string.IsNullOrEmpty(searchString)) {
				var matchingEntries = _currentData.Entries.Where(item => IsMatch(searchString, item.Message)).ToList();
				var currentSearchData = new LogData();
				currentSearchData.Entries.AddRange(matchingEntries);
				currentSearchData.Analyze();
				dataToDisplay = currentSearchData;
			}
            SetViewModel(dataToDisplay);
		}
		#endregion
		#region private bool IsMatch(string searchString, string message)
		/// <summary>
		/// Checks if the supplied searchstring is a match for the supplied text
		/// </summary>
		/// <param name="searchString"></param>
		/// <param name="message"></param>
		/// <returns>True if it is match, otherwise false.</returns>
		private bool IsMatch(string searchString, string message) {
			if (string.IsNullOrEmpty(message))
				return false;
			if (string.IsNullOrEmpty(searchString))
				return true;

			return message.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0;
		}
		#endregion


	}
}
