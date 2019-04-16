using System;
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

        public SortableSearchableList<LogEntry> DataSource
        {
            set
            {
                bindingSource1.DataSource = value;
            }
        }

        


        public bool IsMainLog {
            get { return _isMainLog; }
            set {
                if (_isMainLog != value) {
                    _isMainLog = value;
                    IsMainLogChanged();
                }
            }
        }

        private void IsMainLogChanged() {
            if (IsMainLog)
            {
                liveToolstripButton.Checked = Settings.Instance.Live;
                liveToolstripButton.Visible = true;
                EventHub.FindEntry += EventHub_FindEntry;
            } else {
                liveToolstripButton.Visible = false;
                EventHub.FindEntry -= EventHub_FindEntry;
            }
        }


        private LogEntry _currentContextItem;
        private bool _isMainLog;
        /* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
        #region public LogEntryView()
        /// <summary>
        /// Initializes a new instance of the <b>LogEntryView</b> class.
        /// </summary>
        public LogEntryView() {
            InitializeComponent();
            liveToolstripButton.Visible = false;
            bindingSource1.RaiseListChangedEvents = true;
            EventHub.HideLogger += EventHub_HideLogger;
            EventHub.FilterThread += EventHub_FilterThread;
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
                    richTextBox1.Text = logEntry.Message.Message;
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
            EventHub.OnHideLogger(new LogEntryEventArgs(_currentContextItem));
        }
        #endregion
        #region void EventHub_HideLogger(object source, HideLoggerEventArgs args)
        /// <summary>
        /// This method is called when the EventHub's HideLogger event has been fired.
        /// </summary>
        /// <param name="source">The <see cref="object"/> that fired the event.</param>
        /// <param name="args">The <see cref="LogEntryEventArgs"/> of the event.</param>
        void EventHub_HideLogger(object source, LogEntryEventArgs args) {
            if (args?.Entry == null) return;
            var bindingSource = dataGridView1.DataSource as BindingSource;
            var list = bindingSource?.DataSource as SortableSearchableList<LogEntry>;
            if (list == null) return;
            var noIfItems = list.Hide(x => string.Equals(x.Logger, args.Entry.Logger, StringComparison.CurrentCultureIgnoreCase));
            Debug.WriteLine("Hide " + noIfItems + " items.");
        }
        #endregion
        private void EventHub_FilterThread(object source, LogEntryEventArgs args) {
            if (args?.Entry == null) return;
            var bindingSource = dataGridView1.DataSource as BindingSource;
            var list = bindingSource?.DataSource as SortableSearchableList<LogEntry>;
            if (list == null) return;
            var noIfItems = list.Hide(x => x.Thread != args.Entry.Thread);
            Debug.WriteLine("Hide " + noIfItems + " items.");
        }

        private void EventHub_FindEntry(object source, LogEntryEventArgs args) {
            if (args?.Entry == null) return;
            var bindingSource = dataGridView1.DataSource as BindingSource;
            var list = bindingSource?.DataSource as SortableSearchableList<LogEntry>;
            if (list == null) return;
            var indexOf = list.IndexOf(args.Entry);
            if (indexOf >= 0) {
                var firstRow = Math.Max(0, indexOf - dataGridView1.DisplayedRowCount(false) / 2);
                dataGridView1.FirstDisplayedScrollingRowIndex = firstRow;
                dataGridView1.Rows[indexOf].Selected = true;
            }
        }
        private void findEntryToolStripMenuItem_Click(object sender, EventArgs e) {
            if (IsMainLog) return;
            if (_currentContextItem == null) return;
            EventHub.OnFindEntry(new LogEntryEventArgs(_currentContextItem));

        }

        private void filterOnThreadToolStripMenuItem_Click(object sender, EventArgs e) {
            EventHub.OnFilterThread(new LogEntryEventArgs(_currentContextItem));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            liveToolstripButton.Checked = !liveToolstripButton.Checked;
            //liveToolstripButton.Text = liveToolstripButton.Checked ? "Pause Updates" : "Continue";

                
            if (IsMainLog)
            {
                Settings.Instance.Live = liveToolstripButton.Checked;
                Settings.Instance.Save();
            }
        }

        private void bindingSource1_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            Debug.WriteLine("LogEntryView data list changed");
            //bindingSource1.ResetBindings(false);
        }
    }

}
