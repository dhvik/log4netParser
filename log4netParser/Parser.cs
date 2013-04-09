using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace log4netParser {
	internal class Parser {
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
				_current.Message += line;
			}
			//2013-04-09 00:57:29,648 ERROR [57] EPiServer.Global.Global_Error - 1.2.5 Unhandled exception in ASP.NET
			//System.Web.HttpException (0x80004005): A potentially dangerous Request.Path value was detected from the client (?).
			//   at System.Web.HttpRequest.ValidateInputIfRequiredByConfig()
			//   at System.Web.HttpApplication.PipelineStepManager.ValidateHelper(HttpContext context)
		}
		#endregion
		#region public LogEntry ParseEntry(string line)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		private LogEntry ParseEntry(string line) {
			const string logPattern = @"
^
(?<date>\d{4}-\d{2}-\d{2}
\s
\d{2}:\d{2}:\d{2},\d{3})\s+
(?<level>\w+)\s+
\[(?<thread>\d+)\]\s+
(?<logger>\S+)\s+
(?<whut>\S+)\s+
(?<message>.*)$
";

			var match = Regex.Match(line, logPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			if (match.Success) {
				var entry = new LogEntry {
					Time = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture),
					Level = match.Groups["level"].Value,
					Thread = int.Parse(match.Groups["thread"].Value),
					Logger = match.Groups["logger"].Value,
					Message = match.Groups["message"].Value
				};
				return entry;
			}
			return null;
		}
		#endregion

	}
}