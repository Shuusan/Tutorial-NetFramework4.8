using System;
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
