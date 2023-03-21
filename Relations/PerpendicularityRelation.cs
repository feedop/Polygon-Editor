using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GK_CAD.Relations
{
    public class PerpendicularityRelation : Relation
    {
        private readonly Polygon polygon1;
        public Vertex V10 { get; private set; }
        public Vertex V11 { get; private set; }
        public LinkedList<Relation>? reverseList { get; set; }
        public LinkedListNode<Relation>? reverseNode { get; set; }
        public PerpendicularityRelation(Polygon polygon, Polygon polygon1, RelationManager relationManager, int i0, int j0, int i1, int j1, int counter) : base(polygon, relationManager, i0, j0)
        {
            this.polygon1 = polygon1;
            V10 = polygon1[i1];
            V11 = polygon1[j1];
            this.Counter = counter;
        }

        public override void Delete(LinkedListNode<Relation> node)
        {
            if (relationManager.Relations.ContainsKey((V10, V11)) && reverseList != null && reverseNode != null)
            {
                reverseList.Remove(reverseNode);
                if (reverseList.Count == 0) relationManager.Relations.Remove((V10, V11));
            }
            base.Delete(node);
        }

        public override void Propagate(bool increasing)
        {
            int i = V0.Index;
            int j = V1.Index;
            int i1 = V10.Index;
            int j1 = V11.Index;

            // rotation coefficient for better user experience
            int coefficient = ((polygon == polygon1 && (i > i1 || j == 0)) ||
                polygon != polygon1 && (j == 0 || j > j1)) ? -1 : 1;

            // check if the user moved the former or the latter vertex from the relation
            if (increasing)
            {
                if (relationManager.Changed.Contains(V10))
                {
                    return;
                }
                relationManager.Changed.Add(V10);
                if (Geometry.DotProduct(V0, V1, V10, V11) != 0)
                    V10.MakePerpendicular(V11, V1, V0, coefficient);
                else return;

                // propagate at the destination edge
                if (relationManager.Relations.ContainsKey((V10, V11)))
                {
                    foreach (Relation relation in relationManager.Relations[(V10, V11)])
                    {
                        relation.Propagate(true);
                    }
                }              
            }
            else
            {
                if (relationManager.Changed.Contains(V11))
                {
                    return;
                }
                relationManager.Changed.Add(V11);

                if (Math.Abs(Geometry.DotProduct(V0, V1, V10, V11)) != 0)
                    V11.MakePerpendicular(V10, V0, V1, coefficient);
                else return;

                // propagate at the destination edge
                if (relationManager.Relations.ContainsKey((V10, V11)))
                {
                    foreach (Relation relation in relationManager.Relations[(V10, V11)])
                    {
                        relation.Propagate(false);
                    }
                }
            }
            // Propagate the relation in neighborhood of the destination edge
            int next1 = (j1 + 1) % polygon1.Count;
            if (relationManager.Relations.ContainsKey((V11, polygon1[next1])))
            {
                foreach (Relation relation in relationManager.Relations[(V11, polygon1[next1])])
                {
                    relation.Propagate(true);
                }
            }

            int previous1 = i1 == 0 ? polygon1.Count - 1 : i1 - 1;
            if (relationManager.Relations.ContainsKey((polygon1[previous1], V10)))
            {
                foreach (Relation relation in relationManager.Relations[(polygon1[previous1], V10)])
                {
                    relation.Propagate(false);
                }
            }
        }
        // Print relation marker near the edges
        public override void Draw(Graphics g, SolidBrush brush, Font font, PointF middle, int counter, int i)
        {
            string drawString = counter.ToString();
            g.DrawString(drawString, font, brush, new PointF(middle.X + i * Form1.relationHitboxWidth, middle.Y - Form1.relationHitboxWidth));
        }

        // Hitbox of the relation marker
        public override bool Hitbox(PointF middle, int k, MouseEventArgs e)
        {
            RectangleF rectangle = new RectangleF(middle.X + k * Form1.relationHitboxWidth, middle.Y - Form1.relationHitboxWidth,
                Form1.relationHitboxWidth, Form1.relationHitboxWidth);

            return rectangle.Contains(e.Location);
        }

        public override bool Exclusive()
        {
            return false;
        }
    }
}
