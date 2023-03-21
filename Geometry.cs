using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD
{
    // various tools for hitboxes and basic geometric calculations
    public static class Geometry
    {
        public static bool VertexHitbox(Vertex vertex, MouseEventArgs e)
        {
            return Math.Abs(e.X - vertex.X) <= Form1.pointHitboxWidth && Math.Abs(e.Y - vertex.Y) <= Form1.pointHitboxWidth;
        }

        public static bool EdgeHitbox(Vertex v0, Vertex v1, MouseEventArgs e)
        {
            float dist = ((v1.X - v0.X) * (v1.X - v0.X) + (v1.Y - v0.Y) * (v1.Y - v0.Y)) * Form1.lineHitboxWidth * Form1.lineHitboxWidth;
            if (Math.Pow((v1.X - v0.X) * (v0.Y - e.Y) - (v0.X - e.X) * (v1.Y - v0.Y), 2) <= dist)
            {
                if (v1.X == v0.X) return (e.Y >= v0.Y && e.Y <= v1.Y) || (e.Y >= v1.X && e.X <= v0.X);
                float coefficient = (e.X - v0.X) / (v1.X - v0.X);
                return coefficient >= 0 && coefficient <= 1;
            }
            return false;
        }

        public static PointF MiddleOfEdge(Vertex v0, Vertex v1)
        {
            return new PointF((v0.X + v1.X) / 2, (v0.Y + v1.Y) / 2);
        }

        public static float EdgeLength(Vertex v0, Vertex v1)
        {
            return (float)Math.Sqrt((v1.X - v0.X) * (v1.X - v0.X) + (v1.Y - v0.Y) * (v1.Y - v0.Y));
        }

        public static float DotProduct(Vertex v0, Vertex v1, Vertex v10, Vertex v11)
        {
            float ret = (v1.X - v0.X) * (v11.Y - v10.Y) + (v1.Y - v0.Y) * (v11.X - v10.X);
            return (v1.X - v0.X) * (v11.Y-v10.Y) + (v1.Y - v0.Y) * (v11.X - v10.X);
        }
    }
}
