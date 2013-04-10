using System.Collections.Generic;
using System.Windows.Forms;

namespace log4netParser {
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
					bindingSource1.DataSource = _logEntires;
				}
			}
		}
		private List<LogEntry> _logEntires;
		#endregion
		/* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
		#region public LogEntryView()
		/// <summary>
		/// Initializes a new instance of the <b>LogEntryView</b> class.
		/// </summary>
		public LogEntryView() {
			InitializeComponent();
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
		private void dataGridView1_SelectionChanged(object sender, System.EventArgs e) {
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

	}
}
