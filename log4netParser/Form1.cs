using log4netParser.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace log4netParser {
    public partial class Form1 : Form {
        /* *******************************************************************
         *  Properties
         * *******************************************************************/
        #region public LogData CurrentData
        /// <summary>
        /// Get/Sets the CurrentData of the Form1
        /// </summary>
        /// <value></value>
        public LogData CurrentData {
            get { return _currentData; }
            set {
                if (_currentData != value) {
                    SetViewModel(_currentData);
                    _currentData = value;

                }
            }
        }
        private LogData _currentData;
        #endregion
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
            loggerView1.AddLogger += (sender, args) => AddLoggerTab(args.Logger);
            EventHub.FindEntry += EventHub_FindEntry;
        }

        private void EventHub_FindEntry(object source, LogEntryEventArgs e) {
            SelectMainLogTab();
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
            CurrentData = ParseLogfile(textBox1.Text);
        }
        #endregion
        #region private void SetViewModel(LogData logData)
        /// <summary>
        /// Sets the supplied logData as the current view model
        /// </summary>
        /// <param name="logData"></param>
        private void SetViewModel(LogData logData) {
            mainLogEntryView.LogData = logData;
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
            using (var stream = file.OpenRead()) {
                return ParseStream(stream);
            }
        }
        #endregion
        #region private static LogData ParseStream(Stream stream)
        /// <summary>
        /// Parses the supplied stream and returns the logdata
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static LogData ParseStream(Stream stream) {
            var parser = new Parser();

            using (var reader = new StreamReader(stream)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    parser.ParseLine(line);
                }
            }
            parser.LogData.Analyze();
            return parser.LogData;
        }
        #endregion
        #region private void LoadDataFromClipboard()
        /// <summary>
        /// Loads the clipboard as data
        /// </summary>
        private void LoadDataFromClipboard() {
            if (!Clipboard.ContainsText()) return;
            var text = Clipboard.GetText();
            using (var stream = new MemoryStream()) {
                var writer = new StreamWriter(stream);
                writer.Write(text);
                writer.Flush();
                stream.Position = 0;
                CurrentData = ParseStream(stream);
            }
        }
        #endregion
        #region private void PerformSearch(string searchString)
        /// <summary>
        /// Performs a search in the current data
        /// </summary>
        /// <param name="searchString"></param>
        private void PerformSearch(string searchString) {
            if (CurrentData == null) {
                return;
            }
            var dataToDisplay = CurrentData;
            if (!string.IsNullOrEmpty(searchString)) {
                var matchingEntries = CurrentData.Entries.Where(item => IsMatch(searchString, item.Message.Message)).ToList();
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
        private void SelectMainLogTab() {
            tabControl1.SelectTab(mainLogTabPage);
        }
        #region private void CloseCurrentTab()
        /// <summary>
        /// Closes the current tab
        /// </summary>
        private void CloseCurrentTab() {
            //cannot close main tabs
            var tabToClose = tabControl1.SelectedTab;
            if (tabToClose == mainLogTabPage || tabToClose == tabPage2)
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

        /* *******************************************************************
         *  Events
         * *******************************************************************/
        #region private void closeTabButton_Click(object sender, EventArgs e)
        /// <summary>
        /// This method is called when the closeTabButton's Click event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> of the event.</param>
        private void closeTabButton_Click(object sender, EventArgs e) {
            CloseCurrentTab();
        }



        #endregion
        #region private void searchButton_Click(object sender, EventArgs e)
        /// <summary>
        /// This method is called when the searchButton's Click event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> of the event.</param>
        private void searchButton_Click(object sender, EventArgs e) {
            PerformSearch(searchTextBox.Text);
        }
        #endregion
        #region private void Form1_KeyDown(object sender, KeyEventArgs e)
        /// <summary>
        /// This method is called when the Form1's KeyDown event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> of the event.</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.V) {
                LoadDataFromClipboard();
            }
        }
        #endregion

    }
}
