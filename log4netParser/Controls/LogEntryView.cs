using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace log4netParser.Controls {
    /// <summary>
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    public partial class LogEntryView : UserControl {
        /* *******************************************************************
         *  Properties
         * *******************************************************************/
        #region public List<LogEntry> LogEntires
        /// <summary>
        /// Get/Sets the LogEntires of the LogEntryView
        /// </summary>
        /// <value></value>
        public List<LogEntry> LogEntires {
            get { return _logEntires; }
            set {
                if (_logEntires != value) {
                    _logEntires = value;
                    bindingSource1.DataSource = new SortableSearchableList<LogEntry>(_logEntires);
                }
            }
        }
        private List<LogEntry> _logEntires;
        #endregion
        #region public LogData LogData
        /// <summary>
        /// Sets the LogData of the LogEntryView
        /// </summary>
        /// <value></value>
        public LogData LogData {
            set {
                LogEntires = value == null ? null : value.Entries;
            }
        }
        #endregion
        private LogEntry _currentContextItem;
        /* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
        #region public LogEntryView()
        /// <summary>
        /// Initializes a new instance of the <b>LogEntryView</b> class.
        /// </summary>
        public LogEntryView() {
            InitializeComponent();
            EventHub.HideLogger += EventHub_HideLogger;
        }
        #endregion
        /* *******************************************************************
		 *  Methods
		 * *******************************************************************/
        #region private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        /// <summary>
        /// This method is called when the dataGridView1's SelectionChanged event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> of the event.</param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e) {
            richTextBox1.SuspendLayout();
            richTextBox1.Text = "";
            if (dataGridView1.SelectedRows.Count > 0) {
                var logEntry = dataGridView1.SelectedRows[0].DataBoundItem as LogEntry;
                if (logEntry != null) {
                    richTextBox1.Text = logEntry.Message;
                }

            }
            richTextBox1.ResumeLayout();
        }
        #endregion
        #region private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        /// <summary>
        /// This method is called when the dataGridView1's CellMouseClick event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> of the event.</param>
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            //must rightclick
            if (e.Button != MouseButtons.Right) return;
            //must not click header row
            if (e.RowIndex < 0) return;
            _currentContextItem = null;
            var bindingSource = dataGridView1.DataSource as BindingSource;
            if (bindingSource == null || bindingSource.Count <= e.RowIndex) return;
            var item = bindingSource[e.RowIndex] as LogEntry;
            if (item == null) return;
            _currentContextItem = item;

            hideLoggerXToolStripMenuItem.Text = "Hide logger '" + item.Logger + "'";
            var cellDisplayRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var x = e.X + cellDisplayRectangle.X;
            var y = e.Y + cellDisplayRectangle.Y;
            contextMenuStrip1.Show(dataGridView1, x, y);
        }
        #endregion
        #region private void hideLoggerXToolStripMenuItem_Click(object sender, EventArgs e)
        /// <summary>
        /// This method is called when the hideLoggerXToolStripMenuItem's Click event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> of the event.</param>
        private void hideLoggerXToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_currentContextItem == null) return;
            EventHub.OnHideLogger(new HideLoggerEventArgs(_currentContextItem));
        }
        #endregion
        #region void EventHub_HideLogger(object source, HideLoggerEventArgs args)
        /// <summary>
        /// This method is called when the EventHub's HideLogger event has been fired.
        /// </summary>
        /// <param name="source">The <see cref="object"/> that fired the event.</param>
        /// <param name="args">The <see cref="HideLoggerEventArgs"/> of the event.</param>
        void EventHub_HideLogger(object source, HideLoggerEventArgs args) {
            if (args == null || args.Entry == null) return;
            var bindingSource = dataGridView1.DataSource as BindingSource;
            if (bindingSource == null) return;
            var list = bindingSource.DataSource as SortableSearchableList<LogEntry>;
            if (list == null) return;
            int noIfItems = list.Hide(x => string.Equals(x.Logger, args.Entry.Logger, StringComparison.CurrentCultureIgnoreCase));
            Debug.WriteLine("Hide " + noIfItems + " items.");
        }
        #endregion

    }
}
