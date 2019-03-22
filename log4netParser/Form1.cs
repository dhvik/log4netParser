using log4netParser.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace log4netParser
{
    public partial class Form1 : Form
    {
        /* *******************************************************************
         *  Properties
         * *******************************************************************/
        /* *******************************************************************
         *  Constructors
         * *******************************************************************/
        #region public Form1()
        /// <summary>
        /// Initializes a new instance of the <b>Form1</b> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Settings.Instance.LastLoadedFile;

            LogDataSource.Instance = new LogDataSource(this);
            loggerView1.DataSource = LogDataSource.Instance.LogData.LoggerDataSource;
            mainLogEntryView.DataSource = LogDataSource.Instance.LogData.LogEntryDataSource;

            loggerView1.AddLogger += (sender, args) => AddLoggerTab(args.Logger);
            EventHub.FindEntry += EventHub_FindEntry;
        }

        private void EventHub_FindEntry(object source, LogEntryEventArgs e)
        {
            SelectMainLogTab();
        }


        #endregion
        /* *******************************************************************
		 *  Methods
		 * *******************************************************************/
        /// <summary>
        /// This method is called when the button1's Click event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> of the event.</param>
        private void BrowseButtonClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
                openFileDialog1.FileName = textBox1.Text;

            if (openFileDialog1.ShowDialog(FindForm()) == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;
                if (!LoadFromFile(filename))
                {
                    MessageBox.Show(this, $"Cannot find file '{filename}'", @"File not found");
                }

            }
        }
       
        #region private void LoadDataFromClipboard()
        /// <summary>
        /// Loads the clipboard as data
        /// </summary>
        private void LoadDataFromClipboard()
        {
            if (!Clipboard.ContainsText()) return;
            var text = Clipboard.GetText();
            LogDataSource.Instance.SetData(text);
        }
        #endregion
        #region private void PerformSearch(string searchString)
        /// <summary>
        /// Performs a search in the current data
        /// </summary>
        /// <param name="searchString"></param>
        private void PerformSearch(string searchString)
        {
            //if (CurrentData == null)
            //{
            //    return;
            //}
            //var dataToDisplay = CurrentData;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    var matchingEntries = CurrentData.Entries.Where(item => IsMatch(searchString, item.Message.Message)).ToList();
            //    var currentSearchData = new LogData();
            //    currentSearchData.Entries.AddRange(matchingEntries);
            //    currentSearchData.Analyze();
            //    dataToDisplay = currentSearchData;
            //}
            //SetViewModel(dataToDisplay);
        }
        #endregion
        #region private bool IsMatch(string searchString, string message)
        /// <summary>
        /// Checks if the supplied searchstring is a match for the supplied text
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="message"></param>
        /// <returns>True if it is match, otherwise false.</returns>
        private bool IsMatch(string searchString, string message)
        {
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
        private void AddLoggerTab(Logger logger)
        {
            var tabPage = new TabPage(logger.Name);
            var view = new LogEntryView
            {
                DataSource =  logger.DataSource,
                Dock = DockStyle.Fill
            };
            tabPage.Controls.Add(view);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectTab(tabPage);
        }
        #endregion
        private void SelectMainLogTab()
        {
            tabControl1.SelectTab(mainLogTabPage);
        }
        #region private void CloseCurrentTab()
        /// <summary>
        /// Closes the current tab
        /// </summary>
        private void CloseCurrentTab()
        {
            //cannot close main tabs
            var tabToClose = tabControl1.SelectedTab;
            if (tabToClose == mainLogTabPage || tabToClose == tabPage2)
                return;
            tabControl1.SuspendLayout();
            var selectedIndex = tabControl1.SelectedIndex;
            if (selectedIndex == (tabControl1.TabCount - 1))
            {
                selectedIndex--;
            }
            else
            {
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
        private void closeTabButton_Click(object sender, EventArgs e)
        {
            CloseCurrentTab();
        }



        #endregion
        #region private void searchButton_Click(object sender, EventArgs e)
        /// <summary>
        /// This method is called when the searchButton's Click event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> of the event.</param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            PerformSearch(searchTextBox.Text);
        }
        #endregion
        #region private void Form1_KeyDown(object sender, KeyEventArgs e)
        /// <summary>
        /// This method is called when the Form1's KeyDown event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> of the event.</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                LoadDataFromClipboard();
            }
        }
        #endregion

        public bool LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                textBox1.Text = null;
                return false;
            }

            textBox1.Text = filename;
            LogDataSource.Instance.MonitorFile(filename);
            return true;
        }
    }
}
