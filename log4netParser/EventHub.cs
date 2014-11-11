using System;

namespace log4netParser {
    /// <summary>
    /// Summary description for EventHub.
    /// </summary>
    /// <remarks>
    /// 2014-11-11 dan: Created
    /// </remarks>
    public static class EventHub {

        /* *******************************************************************
         *  Events 
         * *******************************************************************/
        #region public event HideLoggerEventHandler HideLogger
        /// <summary>
        /// This event is fired when 
        /// </summary>
        public static event HideLoggerEventHandler HideLogger;
        #endregion
        #region protected virtual void OnHideLogger(HideLoggerEventArgs e)
        /// <summary>
        /// Notifies the listeners of the HideLogger event
        /// </summary>
        /// <param name="e">The argument to send to the listeners</param>
        public static void OnHideLogger(HideLoggerEventArgs e) {
        	if(HideLogger != null) {
        		HideLogger(null,e);
        	}
        }
        #endregion
    }

    public delegate void HideLoggerEventHandler(object source, HideLoggerEventArgs args);
    public class HideLoggerEventArgs : EventArgs {
        #region public LogEntry Entry
        /// <summary>
        /// Get/Sets the Entry of the HideLoggerEventArgs
        /// </summary>
        /// <value></value>
        public LogEntry Entry { get; set; }
        #endregion
        #region public HideLoggerEventArgs()
        /// <summary>
        /// Initializes a new instance of the <b>HideLoggerEventArgs</b> class.
        /// </summary>
        public HideLoggerEventArgs() { }
        #endregion
        #region public HideLoggerEventArgs(LogEntry entry)
        /// <summary>
        /// Initializes a new instance of the <b>HideLoggerEventArgs</b> class.
        /// </summary>
        /// <param name="entry"></param>
        public HideLoggerEventArgs(LogEntry entry) {
            Entry = entry;
        }
        #endregion
    }
}