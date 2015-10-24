using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace log4netParser {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
		        
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
		    var mainForm = new Form1();
		    if (args != null && args.Length > 0) {
		        mainForm.LoadFromFile(args[0]);
		    }
            Application.Run(mainForm);
		}
	}
}
