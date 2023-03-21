using GK_CAD.PaintingMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD
{
    public abstract class Shape
    {
        public abstract void Draw(IPaintingMethod paintingMethod, Graphics g, Pen pen, SolidBrush brush,
            SolidBrush relationBrush, SolidBrush lengthBrush, Font font, int radius);
    }
}
