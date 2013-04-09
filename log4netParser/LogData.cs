using System.Collections.Generic;

namespace log4netParser {
	internal class LogData {
		/* *******************************************************************
		 *  Properties
		 * *******************************************************************/
		#region public List<LogEntry> Entries
		/// <summary>
		/// Get/Sets the Entries of the LogData
		/// </summary>
		/// <value></value>
		public List<LogEntry> Entries { get; private set; }
		#endregion
		public List<Logger> Loggers { get; private set; }
		/* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
		#region public LogData()
		/// <summary>
		/// Initializes a new instance of the <b>LogData</b> class.
		/// </summary>
		public LogData() {
			Entries = new List<LogEntry>();
		}
		#endregion

		public void Analyze() {
			var dict = new Dictionary<string, Logger>();
			Loggers = new List<Logger>();
			foreach (var logEntry in Entries) {
				string key = logEntry.Level + "|" + logEntry.Logger;
				if (dict.ContainsKey(key)) {
					var logger = dict[key];
					logger.Entries.Add(logEntry);
				} else {
					var logger = new Logger(logEntry);
					dict.Add(key, logger);
					Loggers.Add(logger);
				}
			}
			
		}
	}
}