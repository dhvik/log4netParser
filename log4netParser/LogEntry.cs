using System;

namespace log4netParser {
	internal class LogEntry {
		/* *******************************************************************
		 *  Properties
		 * *******************************************************************/
		#region public string Message
		/// <summary>
		/// Get/Sets the Message of the LogEntry
		/// </summary>
		/// <value></value>
		public string Message { get; set; }
		#endregion
		#region public DateTime Time
		/// <summary>
		/// Get/Sets the Time of the LogEntry
		/// </summary>
		/// <value></value>
		public DateTime Time { get; set; }
		#endregion
		#region public string Level
		/// <summary>
		/// Get/Sets the Level of the LogEntry
		/// </summary>
		/// <value></value>
		public string Level { get; set; }
		#endregion
		#region public int Thread
		/// <summary>
		/// Get/Sets the Thread of the LogEntry
		/// </summary>
		/// <value></value>
		public int Thread { get; set; }
		#endregion
		#region public string Logger
		/// <summary>
		/// Get/Sets the Logger of the LogEntry
		/// </summary>
		/// <value></value>
		public string Logger { get; set; }
		#endregion
		/* *******************************************************************
		 *  Methods
		 * *******************************************************************/
		#region public override string ToString()
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="log4netParser.LogEntry"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="log4netParser.LogEntry"/>.</returns>
		public override string ToString() {
			return Time.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + Level + "\t" + Logger + "\t" + Message;
		}
		#endregion
	}
}