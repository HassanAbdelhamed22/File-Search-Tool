namespace FileSearch
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread guiThread = new Thread(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            });
            guiThread.SetApartmentState(ApartmentState.STA);
            guiThread.Start();
            // Optionally wait for the GUI thread to finish
            guiThread.Join();
        }
    }
}