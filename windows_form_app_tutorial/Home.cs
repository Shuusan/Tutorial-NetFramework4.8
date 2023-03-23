using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_app_tutorial.Views;

namespace windows_form_app_tutorial
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnBackgroundWorker_Click(object sender, EventArgs e)
        {
            BackgroudWorker frmBackgroudWorker = new BackgroudWorker();
            frmBackgroudWorker.ShowDialog();
        }
    }
}
