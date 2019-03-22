using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace log4netParser.Controls {
    public partial class LoggerView : UserControl {
        /* *******************************************************************
         *  Properties
         * *******************************************************************/
        private Logger _currentContextItem;

        public SortableSearchableList<Logger> DataSource
        {
            set
            {
                bindingSource2.DataSource = value;
            }
        }

        /* *******************************************************************
         *  Constructors
         * *******************************************************************/
        public LoggerView() {
            InitializeComponent();
            EventHub.HideLogger += EventHub_HideLogger;
        }

        /* *******************************************************************
         *  Methods
         * *******************************************************************/
        #region private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        /// <summary>
        /// This method is called when the dataGridView2's CellContentDoubleClick event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> of the event.</param>
        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
                var logger = dataGridView2.Rows[e.RowIndex].DataBoundItem as Logger;
                OnAddLogger(new AddLoggerEventArgs{Logger=logger});
        }
        #endregion

        #region public event AddLoggerEventHandler AddLogger
        /// <summary>
        /// This event is fired when 
        /// </summary>
        public event AddLoggerEventHandler AddLogger;
        #endregion
        #region protected virtual void OnAddLogger(AddLoggerEventArgs e)
        /// <summary>
        /// Notifies the listeners of the AddLogger event
        /// </summary>
        /// <param name="e">The argument to send to the listeners</param>
        protected virtual void OnAddLogger(AddLoggerEventArgs e) {
        	if(AddLogger != null) {
        		AddLogger(this,e);
        	}
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
            var bindingSource = dataGridView2.DataSource as BindingSource;
            if (bindingSource == null || bindingSource.Count <= e.RowIndex) return;
            var item = bindingSource[e.RowIndex] as Logger;
            if (item == null) return;
            _currentContextItem = item;

            hideLoggerXToolStripMenuItem.Text = "Hide logger '" + item.Name + "'";
            var cellDisplayRectangle = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var x = e.X + cellDisplayRectangle.X;
            var y = e.Y + cellDisplayRectangle.Y;
            contextMenuStrip1.Show(dataGridView2, x, y);
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
            EventHub.OnHideLogger(new LogEntryEventArgs(new LogEntry{Logger=_currentContextItem.Name}));
        }
        #endregion

        void EventHub_HideLogger(object source, LogEntryEventArgs args) {
            if (args?.Entry == null) return;
            var bindingSource = dataGridView2.DataSource as BindingSource;
            var list = bindingSource?.DataSource as SortableSearchableList<Logger>;
            if (list == null) return;
            int noIfItems = list.Hide(x => string.Equals(x.Name, args.Entry.Logger, StringComparison.CurrentCultureIgnoreCase));
            Debug.WriteLine("Hide " + noIfItems + " items.");
        }


    }
    public delegate void AddLoggerEventHandler(object sender, AddLoggerEventArgs args);

    public class AddLoggerEventArgs : EventArgs {
        public Logger Logger { get; set; }
    }
}
