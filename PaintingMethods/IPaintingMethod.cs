using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.PaintingMethods
{
    public interface IPaintingMethod
    {
        public void DrawLine(Graphics g, Pen pen, SolidBrush brush, float x1, float y1, float x2, float y2);
    }
}
