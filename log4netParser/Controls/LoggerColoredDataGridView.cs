using System.Drawing;
using System.Windows.Forms;

namespace log4netParser.Controls {
    /// <summary>
    /// Summary description for LoggerColoredDataGridView.
    /// </summary>
    /// <remarks>
    /// 2014-11-11 dan: Created
    /// </remarks>
    public class LoggerColoredDataGridView : DataGridView {
        /* *******************************************************************
         *  Properties 
         * *******************************************************************/

        /* *******************************************************************
         *  Constructors 
         * *******************************************************************/

        #region public LoggerColoredDataGridView()
        /// <summary>
        /// Initializes a new instance of the <b>LoggerColoredDataGridView</b> class.
        /// </summary>
        public LoggerColoredDataGridView() {
            CellFormatting += LoggerColoredDataGridView_CellFormatting;
        }
        #endregion


        /* *******************************************************************
         *  Methods 
         * *******************************************************************/
        #region void LoggerColoredDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        /// <summary>
        /// This method is called when the LoggerColoredDataGridView's CellFormatting event has been fired.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that fired the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellFormattingEventArgs"/> of the event.</param>
        void LoggerColoredDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            var row = Rows[e.RowIndex];
            var level = GetLevel(row.DataBoundItem) ?? string.Empty;
            var color = SystemColors.WindowText;
            var backColor = Color.White;
            switch (level.ToUpper()) {
                case "ERROR":
                    backColor= Color.LightPink;
                    break;
                case "FATAL":
                    color = Color.Red;
                    backColor = Color.LightGray;
                    break;
                case "DEBUG":
                    color = Color.Green;
                    break;
                case "WARN":
                    //color = Color.Yellow;
                    backColor = Color.PaleGoldenrod;
                    break;
            }
            row.DefaultCellStyle.ForeColor = color;
            row.DefaultCellStyle.BackColor = backColor;

        }
        #endregion

        private string GetLevel(object o) {
            var logger = o as Logger;
            if(logger !=null) {
                return logger.Level;
            }
            var logEntry = o as LogEntry;
            if (logEntry != null)
                return logEntry.Level;
            return null;
        }

    }
}