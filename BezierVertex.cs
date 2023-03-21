using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD
{
    public class BezierVertex : Vertex
    {
        private Vertex parent;
        public float XParent { get; set; }
        public float YParent { get; set; }
        public BezierVertex(float x, float y, int index, Vertex parent): base(x, y, index)
        {
            this.parent = parent;
            XParent = parent.X;
            YParent = parent.Y;
        }
        public void MoveByDifference(float x, float y)
        {


            this.X = (X + (parent.X - XParent));
            this.Y = (Y + (parent.Y - YParent));
            XParent = parent.X;
            YParent = parent.Y;
        }
    }
}
