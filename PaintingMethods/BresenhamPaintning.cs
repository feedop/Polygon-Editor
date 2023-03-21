using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.PaintingMethods
{
    public class BresenhamPaintning : IPaintingMethod
    {
        // https://stackoverflow.com/questions/11678693/all-cases-covered-bresenhams-line-algorithm
        public void DrawLine(Graphics g, Pen pen, SolidBrush brush, float x1, float y1, float x2, float y2)
        {
            int x = (int)x1;
            int y = (int)y1;
            int w = (int)x2 - x;
            int h = (int)y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                g.FillCircle(brush, x, y, Form1.penWidth);

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}
