using GK_CAD.PaintingMethods;
using GK_CAD.Relations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD
{
    [Serializable]
    public class Polygon : Shape
    {
        private readonly RelationManager relationManager;
        private readonly List<Vertex> vertices;
        public PerpendicularityRelation? TemporaryRelation { get; set; }
        public RectangleF Hitbox { get; private set; }

        // Square bracket overloading
        public Vertex this[int i]
        {
            get { return vertices[i]; }
            set { vertices[i] = value; }
        }

        public int Count { get { return vertices.Count; } }

        public Vertex[] Vertices { get; }

        public Polygon(List<Vertex> vertices, RelationManager relationManager)
        {
            this.vertices = new List<Vertex>(vertices);
            this.relationManager = relationManager;
            UpdateHitbox();
        }

        public Polygon(Vertex[] vertices, RelationManager relationManager)
        {
            this.vertices = new List<Vertex>(vertices);
            this.relationManager = relationManager;
            UpdateHitbox();
        }

        // Polygon draws itself: vertices, relations and edges
        public override void Draw(IPaintingMethod paintingMethod, Graphics g, Pen pen, SolidBrush brush,
            SolidBrush relationBrush, SolidBrush lengthBrush, Font font, int radius)
        {
            for (int i = 0; i < Count - 1; i++)
            {
                // Bezier
                if (this[i + 1] is BezierVertex && this[(i + 2) % Count] is BezierVertex)
                {
                    Vertex V0 = this[i];
                    Vertex V1 = this[i + 1];
                    Vertex V2 = this[(i + 2) % Count];
                    Vertex V3 = this[(i + 3) % Count];

                    if (i == Count - 1)
                    {
                        V1 = this[0];
                        V2 = this[1];
                        V3 = this[2];
                    }

                    float A0x = V0.X;
                    float A0y = V0.Y;

                    float A1x = 3 * (V1.X - V0.X);
                    float A1y = 3 * (V1.Y - V0.Y);

                    float A2x = 3 * (V2.X - 2* V1.X + V0.X);
                    float A2y = 3 * (V2.Y - 2 * V1.Y + V0.Y);

                    float A3x = (V3.X - 3 * V2.X + 3 * V1.X - V0.X);
                    float A3y = (V3.Y - 3 * V2.Y + 3 * V1.Y - V0.Y);
                    // draw Bezier
                    for (double t = 0.0f; t < 1; t += 0.02f)
                    {

                        double x = A3x * t * t * t + A2x * t * t + A1x * t + A0x;
                        double y = A3y * t * t * t + A2y * t * t + A1y * t + A0y;

                        g.FillCircle(Brushes.OrangeRed, (float)x, (float)y, 1);
                    }
                    Pen GrayPen = new Pen(Brushes.LightBlue, 1);
                    paintingMethod.DrawLine(g, GrayPen, brush, vertices[i].X, this[i].Y, this[(i + 3) % Count].X, this[(i + 3) % Count].Y);
                }


                if (this[i] is BezierVertex || this[i + 1] is BezierVertex)
                {
                    if (this[i] is BezierVertex)
                        g.FillCircle(Brushes.DarkRed, this[i].X, this[i].Y, radius);
                    else
                        g.FillCircle(brush, this[i].X, this[i].Y, radius);
                    Pen GrayPen = new Pen(Brushes.Gray, 1);
                    GrayPen.DashPattern = new float[] { 1.0F, 1.0F, 1.0F, 1.0F };
                    paintingMethod.DrawLine(g, GrayPen, brush, vertices[i].X, this[i].Y, this[i + 1].X, this[i + 1].Y);
                }
                else
                {
                    // draw vertex
                    g.FillCircle(brush, this[i].X, this[i].Y, radius);
                    // draw edge
                    paintingMethod.DrawLine(g, pen, brush, vertices[i].X, this[i].Y, this[i + 1].X, this[i + 1].Y);
                    // draw relation
                    if (relationManager.Relations.ContainsKey((this[i], this[i + 1])))
                    {
                        LinkedList<Relation> list = relationManager.Relations[(this[i], this[i + 1])];
                        PointF middle = Geometry.MiddleOfEdge(this[i], this[i + 1]);
                        int j = 0;
                        foreach (Relation relation in list)
                        {
                            if (relation.Exclusive())
                                relation.Draw(g, lengthBrush, font, middle, relation.Counter, j);
                            else
                                relation.Draw(g, relationBrush, font, middle, relation.Counter, j);
                            j++;
                        }
                    }
                }
                
            }
            // Last edge
            g.FillCircle(brush, this[Count - 1].X, this[Count - 1].Y, radius);
            paintingMethod.DrawLine(g, pen, brush, this[Count - 1].X, this[Count - 1].Y, this[0].X, this[0].Y);
            
            if (relationManager.Relations.ContainsKey((this[Count - 1], this[0])))
            {
                LinkedList<Relation> list = relationManager.Relations[(this[Count - 1], this[0])];
                PointF middle = Geometry.MiddleOfEdge(this[Count - 1], this[0]);
                int j = 0;
                foreach (Relation relation in list)
                {
                    if (relation.Exclusive())
                        relation.Draw(g, lengthBrush, font, middle, relation.Counter, j);
                    else
                        relation.Draw(g, relationBrush, font, middle, relation.Counter, j);
                    j++;
                }
            }
            // Draw temporary relation
            if (TemporaryRelation != null)
            {
                PointF temporaryRelationPoint = Geometry.MiddleOfEdge(TemporaryRelation.V0, TemporaryRelation.V1);
                TemporaryRelation.Draw(g, relationBrush, font, temporaryRelationPoint, relationManager.Counter, TemporaryRelation.Counter);
            }
        }

        public void UpdateHitbox()
        {
            float maxX = vertices.Max(x => x.X);
            float minX = vertices.Min(x => x.X);
            float maxY = vertices.Max(y => y.Y);
            float minY = vertices.Min(y => y.Y);

            Hitbox = new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        // new location of a vertex
        public void MoveVertex(int index, float x, float y)
        {
            int prev = index == 0 ? Count - 1 : index - 1;
            int next = (index + 1) % Count;
            this[index].MoveVertex(x, y);

            // MoveVertex
            if (this[next] is BezierVertex && this[index] is not BezierVertex)
            {
                (this[next] as BezierVertex).MoveByDifference(x, y);
            }
            if (this[prev] is BezierVertex && this[index] is not BezierVertex)
            {
                (this[prev] as BezierVertex).MoveByDifference(x, y);
            }

            relationManager.Changed.Add(this[index]);
            relationManager.First = this[index];
            // propagate relations on neighboring edges
            if (relationManager.Relations.ContainsKey((this[prev], this[index])))
            {
                foreach (Relation relation in relationManager.Relations[(this[prev], this[index])])
                {
                    relation.Propagate(false);
                } 
            }

            if (relationManager.Relations.ContainsKey((this[index], this[next])))
            {
                foreach (Relation relation in relationManager.Relations[(this[index], this[next])])
                {
                    relation.Propagate(true);
                }
            }
            relationManager.ResetChanged();
            relationManager.First = null;
            UpdateHitbox();
        }

        // move polygon by the difference
        public void MovePolygon(float x, float y)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i] is BezierVertex)
                {
                    (this[i] as BezierVertex).XParent += x;
                    (this[i] as BezierVertex).YParent += y;
                }
                this[i].MoveVertex(this[i].X + x, this[i].Y + y);
            }
            UpdateHitbox();
        }

        public void AddVertex(int index, Vertex vertex)
        {
            vertices.Insert(index, vertex);
            UpdateHitbox();
        }

        public void RemoveAt(int index)
        {
            vertices.RemoveAt(index);
        }
    }
}
