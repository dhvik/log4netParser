using System;
using System.Windows.Forms;

namespace log4netParser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Form1();
            if (args != null && args.Length > 0)
            {
                mainForm.LoadFromFile(args[0]);
            }
            else if (!string.IsNullOrEmpty(Settings.Instance.LastLoadedFile))
            {
                mainForm.LoadFromFile(Settings.Instance.LastLoadedFile);
            }
            Application.Run(mainForm);
        }
    }
}
