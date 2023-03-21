using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD
{
    [Serializable]
    public class Vertex
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public int Index { get; set; }

        public Vertex(float x, float y, int index)
        {
            X = x;
            Y = y;
            Index = index;
        }

        public Vertex(double x, double y, int index)
        {
            X = (float)x;
            Y = (float)y;
            Index = index;
        }

        public void MoveVertex(float x, float y)
        {
            X = x;
            Y = y;
        }

        // adapt to a FixedLengthRelation by moving this
        public void MoveToFitLength(Vertex v1, float oldLength, float newLength)
        {
            if (X == v1.X)
            {
                return;
            }
            float ratio = (newLength - oldLength) / oldLength;
            float dx = X - v1.X;
            float dy = Y - v1.Y;
            MoveVertex(X + dx * ratio, Y + dy * ratio);
        }
        // move this to make (this, v1) perpendicular to (v2, v3)
        public void MakePerpendicular(Vertex v1, Vertex v10, Vertex v11, int coefficient = 1)
        {
            float destinationLength = Geometry.EdgeLength(this, v1);
            float sourceLength = Geometry.EdgeLength(v10, v11);
            float dx = v11.X - v10.X;
            float dy = v11.Y - v10.Y;

            // Special case
            if (this == v10)
            {
                //float straightlength = Geometry.EdgeLength(this, v11);
                //destinationLength -= straightlength / sourceLength;
            }
            float ratio = destinationLength / sourceLength;
            MoveVertex(v1.X - coefficient * dy * ratio, v1.Y + coefficient * dx * ratio);
        }
    }
}
