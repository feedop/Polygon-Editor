using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.PaintingMethods
{
    // standard painting method
    public class GDIPlusPainting : IPaintingMethod
    {
        public void DrawLine(Graphics g, Pen pen, SolidBrush brush, float x1, float y1, float x2, float y2)
        {
            g.DrawLine(pen, x1, y1, x2, y2);
        }
    }
}

