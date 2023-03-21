using GK_CAD.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GK_CAD.States
{
    public class MovingEdgeState : State
    {
        private double slope;
        private double angle;
        private float sin;
        private float cos;

        private Polygon polygon;
        private int count;
        private int index;

        private Vertex vPrev, v0, v1, vNext;

        private PointF initialV0;
        private PointF initialV1;
        private PointF initialE;

        private bool prevPerpendicular = false;
        private bool nextPerpendicular = false;
        private bool fixedLength;
        public MovingEdgeState(Form1 form, RelationManager relationManager, PointF initialE, PointF initialV0, PointF initialV1,
            double slope, bool fixedLength = false) : base(form, relationManager)
        {
            this.slope = slope;

            angle = Math.PI / 2 - slope;
            sin = (float)Math.Sin(angle);
            cos = (float)Math.Cos(angle);

            polygon = form.Polygons[form.MovingEdge.Item1];
            count = polygon.Count;
            index = form.MovingEdge.Item2;

            vPrev = polygon[index == 0 ? count - 1 : index - 1];
            v0 = polygon[index];
            v1 = polygon[(index + 1) % count];
            vNext = polygon[(index + 2) % count];

            this.initialV0 = initialV0;
            this.initialV1 = initialV1;
            this.initialE = initialE;
            this.fixedLength = fixedLength;

            if (Math.Abs(Math.Atan2(v0.Y - vPrev.Y, v0.X - vPrev.X) - slope) <= Form1.eps) prevPerpendicular = true;
            if (Math.Abs(Math.Atan2(vNext.Y - v1.Y, vNext.X - v1.X) - slope) <= Form1.eps) nextPerpendicular = true;

        }

        public override void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (fixedLength)
            {
                FixedLength(e);
            }
            else
            {
                DynamicLength(e);
            }
            
            base.pictureBox1_MouseMove(sender, e);
            
        }
        public override void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            form.State = new ManipulationState(form, relationManager);
        }

        // When the thedge does not have a FixedLengthRelation
        private void DynamicLength(MouseEventArgs e)
        {
            // Points after rotating to match the new axis
            PointF ptPrevNew = new PointF(vPrev.X * cos - vPrev.Y * sin, vPrev.X * sin + vPrev.Y * cos);
            PointF pt0New = new PointF(v0.X * cos - v0.Y * sin, v0.X * sin + v0.Y * cos);
            PointF pt1New = new PointF(v1.X * cos - v1.Y * sin, v1.X * sin + v1.Y * cos);
            PointF ptNextNew = new PointF(vNext.X * cos - vNext.Y * sin, vNext.X * sin + vNext.Y * cos);

            // Cursor coordinates after rotation
            PointF eNew = new PointF(e.X * cos - e.Y * sin, e.X * sin + e.Y * cos);

            // Line equations
            float aPrevious = (pt0New.Y - ptPrevNew.Y) / (pt0New.X - ptPrevNew.X);
            float aNext = (ptNextNew.Y - pt1New.Y) / (ptNextNew.X - pt1New.X);
            float bPrevious = pt0New.Y - aPrevious * pt0New.X;
            float bNext = ptNextNew.Y - aNext * ptNextNew.X;

            // Points' new coordinates
            PointF pt0After, pt1After;

            // Check if neighboring edges are parallel
            if (!prevPerpendicular)
                pt0After = new PointF(eNew.X, eNew.X * aPrevious + bPrevious);
            else
                pt0After = new PointF(eNew.X, pt0New.Y);

            if (!nextPerpendicular)
                pt1After = new PointF(eNew.X, eNew.X * aNext + bNext);
            else
                pt1After = new PointF(eNew.X, pt1New.Y);

            // Points after returning to starting coordinates
            polygon.MoveVertex(index, pt0After.X * cos + pt0After.Y * sin, -pt0After.X * sin + pt0After.Y * cos);
            polygon.MoveVertex((index + 1) % count, pt1After.X * cos + pt1After.Y * sin, -pt1After.X * sin + pt1After.Y * cos);

        }

        // When the edge has a FixedLengthRelation
        private void FixedLength(MouseEventArgs e)
        {
            polygon.MoveVertex(index, initialV0.X + e.X - initialE.X, initialV0.Y + e.Y - initialE.Y);
            polygon.MoveVertex((index + 1) % count, initialV1.X + e.X - initialE.X, initialV1.Y + e.Y - initialE.Y);
        }
    }
}
