using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace log4netParser {
    public class LogData {
		/* *******************************************************************
		 *  Properties
		 * *******************************************************************/
        private readonly Dictionary<string,Logger> _loggers = new Dictionary<string, Logger>();
        private readonly ISynchronizeInvoke _invoke;
        private bool _logEntryDataSourceModified;
        private bool _loggerDataSourceModified;
        public SortableSearchableList<Logger> LoggerDataSource { get; }
        public SortableSearchableList<LogEntry> LogEntryDataSource { get; }

        public LogData(ISynchronizeInvoke invoke)
        {
            _invoke = invoke;
            LoggerDataSource = new SortableSearchableList<Logger>(invoke);
            LogEntryDataSource = new SortableSearchableList<LogEntry>(invoke);
        }

        public void Clear()
        {
            _loggers.Clear();
            LogEntryDataSource.Clear();
            LogEntryDataSource.Clear();
        }

        public void SuspendUpdates()
        {
            _logEntryDataSourceModified = false;
            _loggerDataSourceModified = false;
            LoggerDataSource.RaiseListChangedEvents = false;
            LogEntryDataSource.RaiseListChangedEvents = false;
            foreach (var loggersValue in _loggers.Values)
            {
                loggersValue.DataSource.RaiseListChangedEvents = false;
            }
        }
        public void ResumeUpdates()
        {
            LoggerDataSource.RaiseListChangedEvents = true;
            if (_loggerDataSourceModified)
            {
                LoggerDataSource.ResetBindings();
                LoggerDataSource.Sort();
            }

            LogEntryDataSource.RaiseListChangedEvents = true;
            foreach (var loggersValue in _loggers.Values)
            {
                loggersValue.DataSource.RaiseListChangedEvents = true;
            }
            if (_logEntryDataSourceModified)
            {
                Debug.WriteLine("Before reset bindings");
                LogEntryDataSource.ResetBindings();
                Debug.WriteLine("After reset bindings");

                LogEntryDataSource.Sort();
                Debug.WriteLine("After Sort");

                foreach (var loggersValue in _loggers.Values)
                {
                    loggersValue.DataSource.ResetBindings();
                    loggersValue.DataSource.Sort();
                }

            }
        }
        public void Add(LogEntry entry)
        {
            LogEntryDataSource.Add(entry);
            _logEntryDataSourceModified = true;
            var key = $"{entry.Level}|{entry.Logger}";
            if (!_loggers.ContainsKey(key))
            {
                var logger = new Logger(_invoke,entry);
                _loggers.Add(key,logger);
                LoggerDataSource.Add(logger);
            }
            else
            {
                _loggers[key].DataSource.Add(entry);
            }
            _loggerDataSourceModified = true;
        }
    }
}