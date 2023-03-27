using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_form_app_tutorial.Class
{
    public class ColorGradient
    {
        public Brush Brush { get; private set; }

        public ColorGradient(Color startColor, Color endColor, int height)
        {
            LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(0, height), startColor, endColor);
            Brush = brush;
        }
    }
}
