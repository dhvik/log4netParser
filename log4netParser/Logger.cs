using System.Collections.Generic;
using System.ComponentModel;

namespace log4netParser {
    public class Logger {
		/* *******************************************************************
		 *  Properties
		 * *******************************************************************/
		#region public string Name
		/// <summary>
		/// Get/Sets the Name of the Logger
		/// </summary>
		/// <value></value>
		public string Name { get; set; }
		#endregion
		#region public string Level
		/// <summary>
		/// Get/Sets the Level of the Logger
		/// </summary>
		/// <value></value>
		public string Level { get; set; }
		#endregion
		#region public int NoOfEntires
		/// <summary>
		/// Gets the NoOfEntires of the Logger
		/// </summary>
		/// <value></value>
		public int NoOfEntires { get { return DataSource.Count; } }
		#endregion
        public SortableSearchableList<LogEntry> DataSource { get; }
		/* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
		#region public Logger(LogEntry logEntry)

        /// <summary>
        /// Initializes a new instance of the <b>Logger</b> class.
        /// </summary>
        /// <param name="invoke"></param>
        /// <param name="logEntry"></param>
        public Logger(ISynchronizeInvoke invoke, LogEntry logEntry) {
			Name = logEntry.Logger;
			Level = logEntry.Level;
		    DataSource = new SortableSearchableList<LogEntry>(invoke) {logEntry};
		}
		#endregion
	}
}