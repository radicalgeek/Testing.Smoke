using System;
using System.Windows.Forms;
using TestConfiguration.Forms;

namespace RadicalGeek.Testing.Smoke.TestConfiguration
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDI());
        }
    }
}
