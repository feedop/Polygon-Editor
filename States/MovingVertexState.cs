using GK_CAD.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GK_CAD.States
{
    public class MovingVertexState : State
    {
        public MovingVertexState(Form1 form, RelationManager relationManager) : base(form, relationManager) { }
        public override void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int i = form.MovingVertex.Item1;
            int j = form.MovingVertex.Item2;
            Polygon polygon = form.Polygons[i];
            Vertex v0 = polygon[j];
            int jPrev = j == 0 ? polygon.Count - 1 : j - 1;
            Vertex vPrev = polygon[jPrev];
            Vertex vPrev2 = polygon[jPrev == 0 ? polygon.Count - 1 : jPrev - 1];
            Vertex vNext = polygon[(j + 1) % polygon.Count];
            Vertex vNext2 = polygon[(j + 2) % polygon.Count];

            // special cases for better user experience

            // moved the first vertex of a right angle
            if (relationManager.Relations.ContainsKey((v0, vNext)) && relationManager.Relations.ContainsKey((vNext, vNext2)))
            {
                if (relationManager.Relations[(v0, vNext)].First != null && relationManager.Relations[(v0, vNext)].First.ValueRef.Exclusive())
                {
                    for (var node = relationManager.Relations[(v0, vNext)].First.Next; node != null; node = node.Next)
                    {
                        var relation = node.ValueRef as PerpendicularityRelation;
                        if (relation.V10 == vNext && relation.V11 == vNext2)
                        {
                            PointF initial = new PointF(v0.X, v0.Y);
                            relationManager.Changed.Add(v0);
                            v0.MoveVertex(e.X, e.Y);
                            polygon.MoveVertex((j + 1) % polygon.Count, vNext.X + (e.X-initial.X) , vNext.Y + (e.Y - initial.Y));
                            break;
                        }
                    }
                }
            }

            // moved the last vertex of a right angle
            if (relationManager.Relations.ContainsKey((vPrev, v0)) && relationManager.Relations.ContainsKey((vPrev, v0)))
            {
                if (relationManager.Relations[(vPrev, v0)].First != null && relationManager.Relations[(vPrev, v0)].First.ValueRef.Exclusive())
                {
                    for (var node = relationManager.Relations[(vPrev, v0)].First.Next; node != null; node = node.Next)
                    {
                        var relation = node.ValueRef as PerpendicularityRelation;
                        if (relation.V10 == vPrev2 && relation.V11 == vPrev)
                        {
                            PointF initial = new PointF(v0.X, v0.Y);
                            relationManager.Changed.Add(v0);
                            v0.MoveVertex(e.X, e.Y);
                            polygon.MoveVertex(jPrev, vPrev.X + (e.X - initial.X), vPrev.Y + (e.Y - initial.Y));
                            break;
                        }
                    }
                }
            }

            // standard case and propagating relations
            polygon.MoveVertex(j, e.X, e.Y);
            base.pictureBox1_MouseMove(sender, e);
        }
        public override void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            form.State = new ManipulationState(form, relationManager);
        }
    }
}
