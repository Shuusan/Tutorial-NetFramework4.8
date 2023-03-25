using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using windows_form_app_tutorial.Views;

namespace windows_form_app_tutorial
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = $"Ver {version.Major}.{version.Minor}.{version.Build}";

            // Register a handler for when the program exits
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            // Get the path to the "randomImage" directory on the desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string randomImagePath = Path.Combine(desktopPath, "randomImage");

            //check if directory exist
            if (Directory.Exists(randomImagePath))
            {

                // Delete all files in the directory
                DirectoryInfo directory = new DirectoryInfo(randomImagePath);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
            }

        }

        private void btnBackgroundWorker_Click(object sender, EventArgs e)
        {
            frmBackgrundWorker frmBackgroudWorker = new frmBackgrundWorker();
            frmBackgroudWorker.ShowDialog();
        }

        private void btnBindingNavigator_Click(object sender, EventArgs e)
        {
            frmBindingNavigator frmBindingNavigator = new frmBindingNavigator();
            frmBindingNavigator.ShowDialog();
        }
    }
}
