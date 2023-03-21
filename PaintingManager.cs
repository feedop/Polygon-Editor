using GK_CAD.PaintingMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_CAD
{
    internal class PaintingManager
    {
        public IPaintingMethod PaintingMethod { get; set; }
        private Form1 form;

        public PaintingManager(IPaintingMethod paintingMethod, Form1 form)
        {
            PaintingMethod = paintingMethod;
            this.form = form;
        }

        public void Paint()
        {
            using (Graphics g = Graphics.FromImage(form.DrawArea))
            {
                // CLear
                g.Clear(SystemColors.Control);

                // Draw vertices that do not form a full polygon yet
                for (int i = 0; i < form.Vertices.Count - 1; i++)
                {
                    g.FillCircle(form.DrawBrush, form.Vertices[i].X, form.Vertices[i].Y, Form1.brushWidth);
                    PaintingMethod.DrawLine(g, form.BlackPen, form.DrawBrush, form.Vertices[i].X, form.Vertices[i].Y, form.Vertices[i + 1].X, form.Vertices[i + 1].Y);
                }
                if (form.Vertices.Count > 0)
                {
                    g.FillCircle(form.DrawBrush, form.Vertices[form.Vertices.Count - 1].X, form.Vertices[form.Vertices.Count - 1].Y, Form1.brushWidth);
                    var position = form.pictureBox1.PointToClient(Cursor.Position);
                    PaintingMethod.DrawLine(g, form.GrayPen, form.DrawBrush, form.Vertices[form.Vertices.Count - 1].X, form.Vertices[form.Vertices.Count - 1].Y, position.X, position.Y);
                }

                // Draw polygons
                foreach (Polygon polygon in form.Polygons)
                {
                    polygon.Draw(PaintingMethod, g, form.BlackPen, form.DrawBrush, form.RelationBrush, form.LengthBrush, form.TextFont, Form1.brushWidth);
                }
            }
        }
    }
}
