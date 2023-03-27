using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_app_tutorial.Class;

namespace windows_form_app_tutorial.Views
{
    public partial class frmButton : Form
    {

        private bool paintEnabled = true;

        public frmButton()
        {
            InitializeComponent();
        }

        private void btnClickAndEnter_Click(object sender, EventArgs e)
        {

            // Try casting event argument to MouseEventArgs (Subclass)
            // If null, casting is failed which means its not coming from Mouse click
            MouseEventArgs mouseArgs = e as MouseEventArgs;

            if (mouseArgs != null)
            {
                // Button was clicked using mouse
                textBox1.Text = "Button nomor 1 ditekan menggunakan mouse ! ";
            }
            else
            {
                // Check if modifier button is pressed
                if(ModifierKeys == Keys.Shift)
                {
                    // Button was clicked using Enter + Shift key
                    textBox1.Text = "Button nomor 1 aktif karena tombol enter dan shift ditekan! ";
                }
                else
                {
                    textBox1.Text = "Button nomor 1 aktif karena tombol enter ditekan! ";

                }

            }

        }

        private void btnPaint_Paint(object sender, PaintEventArgs e)
        {
            Button button = (Button)sender;

            // Draw the button surface with a gradient
            ColorGradient colorGradient;
            if (button.ClientRectangle.Contains(button.PointToClient(Cursor.Position)))
            {
                colorGradient = new ColorGradient(Color.Black, Color.FromArgb(200, 0, 255), button.Height);
            }
            else
            {
                colorGradient = new ColorGradient(Color.Black, Color.Gray, button.Height);
            }
            e.Graphics.FillRectangle(colorGradient.Brush, button.ClientRectangle);

            // Draw the button text
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // if overflows
            format.Trimming = StringTrimming.EllipsisCharacter;

            //Write the text
            e.Graphics.DrawString(button.Text, button.Font, Brushes.White, button.ClientRectangle, format);

        }

        private void btnPaint_Click(object sender, EventArgs e)
        {
            if (paintEnabled)
            {
                btnPaint.Paint -= btnPaint_Paint;
                paintEnabled = false;
                btnPaint.Text = "Click to modify me!";

            }
            else
            {
                btnPaint.Text = "I've been modified! click me to unmodified me!";
                btnPaint.Paint += btnPaint_Paint;
                paintEnabled = true;

            }
        }

        private void btnClickAndEnter_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                btnClickAndEnter.PerformClick();
            }
        }
    }
}
