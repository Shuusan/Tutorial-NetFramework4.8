using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windows_form_app_tutorial.HelperViews
{
    /// <summary>
    /// New : Shuusan 2023/03/25
    /// </summary>
    public partial class LoadingPopup : Form
    {
        public LoadingPopup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Showing popup when doing some task that taking a time
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task ShowDialogAsync(Form owner, Func<Task> action)
        {
            using (LoadingPopup loadingDialog = new LoadingPopup())
            {

                loadingDialog.StartPosition = FormStartPosition.Manual;
                loadingDialog.Location = new Point(owner.Location.X + (owner.Width - loadingDialog.Width) / 2,
                                                   owner.Location.Y + (owner.Height - loadingDialog.Height) / 2);
                loadingDialog.ShowInTaskbar = false;
                loadingDialog.Show(owner);
                await action();
                loadingDialog.Close();
            }
        }
    }

    
}
