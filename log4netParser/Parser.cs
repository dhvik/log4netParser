using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace log4netParser {
    public class Parser {
        /* *******************************************************************
         *  Properties
         * *******************************************************************/
        private LogEntry _current;
        #region public LogData LogData
        /// <summary>
        /// Get/Sets the LogData of the Parser
        /// </summary>
        /// <value></value>
        public LogData LogData { get; private set; }
        #endregion
        /* *******************************************************************
		 *  Constructors
		 * *******************************************************************/
        #region public Parser()
        /// <summary>
        /// Initializes a new instance of the <b>Parser</b> class.
        /// </summary>
        public Parser() {
            LogData = new LogData();
        }
        #endregion
        /* *******************************************************************
		 *  Methods
		 * *******************************************************************/
        #region public void ParseLine(string line)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public void ParseLine(string line) {
            var entry = ParseEntry(line);
            if (entry != null) {
                _current = entry;
                LogData.Entries.Add(entry);
            } else {

                if (_current == null) {
                    _current = new LogEntry {Message = new LogMessage(line)};
                } else {
                    _current.Message.Message += Environment.NewLine + line;
                }
            }
            //2013-04-09 00:57:29,648 ERROR [57] EPiServer.Global.Global_Error - 1.2.5 Unhandled exception in ASP.NET
            //System.Web.HttpException (0x80004005): A potentially dangerous Request.Path value was detected from the client (?).
            //   at System.Web.HttpRequest.ValidateInputIfRequiredByConfig()
            //   at System.Web.HttpApplication.PipelineStepManager.ValidateHelper(HttpContext context)
        }
        #endregion


        readonly List<LogPattern> _logPatterns = new List<LogPattern> {
            new LogPattern( 
//date time level thread logger whut message
            @"^
(?<date>\d{4}-\d{2}-\d{2}
\s
\d{2}:\d{2}:\d{2},\d{3})\s+
(?<level>\w+)\s+
\[(?<thread>\d+)\]\s*
(?<logger>\S+)\s+
(?<whut>\S+)\s+
(?<message>.*)$
","yyyy-MM-dd HH:mm:ss,fff"),
//date time thread level logger whut message
            new LogPattern(@"^
(?<date>\d{4}-\d{2}-\d{2}
\s
\d{2}:\d{2}:\d{2},\d{3})\s+
\[(?<thread>\d+)\]\s*
(?<level>\w+)\s+
(?<logger>\S+)\s+
(?<whut>\S+)\s+
(?<message>.*)$
","yyyy-MM-dd HH:mm:ss,fff"),
            //Azure log format
            new LogPattern(@"^
(?<date>\d{4}-\d{2}-\d{2}
T
\d{2}:\d{2}:\d{2})\s+
(?<process>\S+)\s+
(?<level>\w+)\s+
(?<logger>\S+)\s+
(?<message>.*)$
","yyyy-MM-ddTHH:mm:ss")
        };

        private IEnumerable<LogPattern> LogPatterns {
            get {
                if (_lastUsedPattern!=null)
                    yield return _lastUsedPattern;
                var oldLastUsedPattern = _lastUsedPattern;
                foreach (var logPattern in _logPatterns) {
                    //skip last used pattern (returned first)
                    if (oldLastUsedPattern == logPattern) continue;
                    _lastUsedPattern = logPattern;

                    yield return _lastUsedPattern;
                }
            }
        }

        private LogPattern _lastUsedPattern;
        #region public LogEntry ParseEntry(string line)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private LogEntry ParseEntry(string line) {

            foreach (var logPattern in LogPatterns) {
                var match = Regex.Match(line, logPattern.Pattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
                if (match.Success) {
                    int? thread = null;
                    if (match.Groups["thread"].Success) {
                        thread = int.Parse(match.Groups["thread"].Value);
                    }
                    string process = null;
                    if (match.Groups["process"].Success) {
                        process = match.Groups["process"].Value;
                    }
                    var entry = new LogEntry {
                        Time = DateTime.ParseExact(match.Groups["date"].Value, logPattern.DateTimeFormat, CultureInfo.InvariantCulture),
                        Level = match.Groups["level"].Value,
                        Thread = thread,
                        Process = process,
                        Logger = match.Groups["logger"].Value,
                        Message = new LogMessage(match.Groups["message"].Value)
                    };
                    return entry;
                }
            }
            return null;
        }
        #endregion

    }

    public class LogPattern {
        public string Pattern { get; }
        public string DateTimeFormat { get; }

        public LogPattern(string pattern, string dateTimeFormat) {
            Pattern = pattern;
            DateTimeFormat = dateTimeFormat;
        }
    }
}