using System;
using System.Threading;
using System.Windows.Forms;

namespace ScreenCapture
{

    static class Program
    {
        // Mutex can be made static so that GC doesn't recycle
        // same effect with GC.KeepAlive(mutex) at the end of main
        static bool bCreatedNew;
        static Mutex mutex = new Mutex(false, "Unique-Application-Concurrent", out bCreatedNew);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //allow run only once
            if (bCreatedNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
