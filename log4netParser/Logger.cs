using System.Collections.Generic;

namespace log4netParser {
	internal class Logger {
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
		public int NoOfEntires { get { return Entries.Count; } }
		#endregion
		#region public List<LogEntry> Entries
		/// <summary>
		/// Get/Sets the Entries of the Logger
		/// </summary>
		/// <value></value>
		public List<LogEntry> Entries { get; private set; }
		#endregion
		/* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
		#region public Logger(LogEntry logEntry)
		/// <summary>
		/// Initializes a new instance of the <b>Logger</b> class.
		/// </summary>
		/// <param name="logEntry"></param>
		public Logger(LogEntry logEntry) {
			Name = logEntry.Logger;
			Level = logEntry.Level;
			Entries = new List<LogEntry> { logEntry };
		}
		#endregion
	}
}