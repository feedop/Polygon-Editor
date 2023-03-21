using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GK_CAD.Relations
{
    public class FixedLengthRelation : Relation
    {
        public float Length { get; set; }
        private readonly Font font;

        public FixedLengthRelation(Polygon polygon, RelationManager relationManager, int i, int j, float length, Font font) : base(polygon, relationManager, i, j)
        {
            this.Length = length;
            this.font = font;
        }

        public override void Propagate(bool increasing)
        {
            int i = V0.Index;
            int j = V1.Index;

            float oldLength = Geometry.EdgeLength(V0, V1);

            // check if the user moved the former or the latter vertex from the relation
            if (increasing)
            {
                if (V1 == relationManager.First)
                {
                    // impossible operation
                    return;
                }
                relationManager.Changed.Add(V0);    
                V1.MoveToFitLength(V0, oldLength, Length);

                int next = (j + 1) % polygon.Count;
                if (relationManager.Relations.ContainsKey((V1, polygon[next])))
                {
                    foreach (Relation relation in relationManager.Relations[(V1, polygon[next])])
                    {
                        relation.Propagate(true);
                    } 
                }
            }
            else
            {
                if (V0 == relationManager.First)
                {
                    // impossible operation
                    return;
                }
                relationManager.Changed.Add(V1);
                V0.MoveToFitLength(V1, oldLength, Length);

                int previous = i == 0 ? polygon.Count - 1 : i - 1;
                if (relationManager.Relations.ContainsKey((polygon[previous], V0)))
                {
                    foreach (Relation relation in relationManager.Relations[(polygon[previous], V0)])
                    {
                        relation.Propagate(false);
                    }
                }
            }
        }
        // Print length near the dge
        public override void Draw(Graphics g, SolidBrush brush, Font font, PointF middle,int counter, int i)
        {
            string drawString = Length.ToString();
            g.DrawString(drawString, font, brush, new PointF(middle.X - Form1.relationHitboxWidth, middle.Y + Form1.relationHitboxWidth));
        }

        // Hitbox of the printed text
        public override bool Hitbox(PointF middle, int k, MouseEventArgs e)
        {
            Size size = TextRenderer.MeasureText(Length.ToString(), font);
            RectangleF rectangle = new RectangleF(middle.X - Form1.relationHitboxWidth, middle.Y + Form1.relationHitboxWidth,
                size.Width, size.Height);

            return rectangle.Contains(e.Location);
        }

        public override bool Exclusive()
        {
            return true;
        }
    }
}
