using System;
using System.IO;
using log4net;
using Meridium.Lib.Xml.Serialization;

namespace log4netParser {
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	/// <remarks>
	/// 2013-04-10 dan: Created
	/// </remarks>
	public class Settings {
		public static ILog Log = LogManager.GetLogger(typeof(Settings));

		/* *******************************************************************
         *  Properties 
         * *******************************************************************/
		#region public static Settings Instance
		/// <summary>
		/// Returns the singleton instance of the <see cref="Settings"/> class.
		/// </summary>
		public static Settings Instance {
			get {
				if (_instance == null) {
					lock (InstanceLock) {
						if (_instance == null) {
							var file = new FileInfo("log4netParserSettings.xml");
							Settings tmp = null;
							try {
								if (file.Exists)
									tmp = XmlSerializerUtil.DeserializeFromXmlFile<Settings>(file.FullName);
							} catch (Exception e) {
								Log.Error(string.Format("Deserialize xml file '{0}' failed. {1}", file.FullName, e.Message), e);
							}
							if (tmp == null)
								tmp = new Settings();
							tmp._filename = file.FullName;

							_instance = tmp;
						}
					}
				}
				return _instance;
			}
		}
		private static volatile Settings _instance;
		private static readonly object InstanceLock = new object();
		#endregion

		private string _filename;
		#region public string LastLoadedFile
		/// <summary>
		/// Get/Sets the LastLoadedFile of the Settings
		/// </summary>
		/// <value></value>
		public string LastLoadedFile { get; set; }
		#endregion
		/* *******************************************************************
         *  Methods 
         * *******************************************************************/
		#region public void Save()
		/// <summary>
		/// Saves the current settings
		/// </summary>
		public void Save() {
			XmlSerializerUtil.SerializeToXmlFile(this, _filename);
		}
		#endregion
	}
}