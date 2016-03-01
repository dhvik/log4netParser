using System;
using System.Linq;

namespace log4netParser {
	public class LogEntry {
		/* *******************************************************************
		 *  Properties
		 * *******************************************************************/
		#region public string Message
		/// <summary>
		/// Get/Sets the Message of the LogEntry
		/// </summary>
		/// <value></value>
        public LogMessage Message { get; set; }
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
		public int? Thread { get; set; }
		#endregion
		#region public string Logger
		/// <summary>
		/// Get/Sets the Logger of the LogEntry
		/// </summary>
		/// <value></value>
		public string Logger { get; set; }
        #endregion
        public string Process { get; set; }
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
    public class LogMessage:IComparable{
        public string Message { get; set; }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: 
        /// Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.
        /// Zero This object is equal to <paramref name="other"/>. 
        /// Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(object other) {
            var logMessage = other as LogMessage;
            if (logMessage == null) return 1;
            if (logMessage.Message == null && Message == null) return 0;
            if (logMessage.Message == null) return 1;
            if (Message == null) return 0;
            //if (DuoVia.FuzzyStrings.StringExtensions.FuzzyEquals(Message, logMessage.Message)) return 0;
            return String.Compare(Message, logMessage.Message, StringComparison.Ordinal);
        }

        #region public LogMessage(string message)
        /// <summary>
        /// Initializes a new instance of the <b>LogMessage</b> class.
        /// </summary>
        /// <param name="message"></param>
        public LogMessage(string message) {
            Message = message;
        }
        #endregion
        public override string ToString() {
            return Message;
        }

    }
}