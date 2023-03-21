using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    public class MovingPolygonState : State
    {
        private Point previousLocation;
        public MovingPolygonState(Form1 form, RelationManager relationManager, Point location) : base(form, relationManager)
        {
            previousLocation = location;
        }

        public override void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            form.Polygons[form.MovingPolygon].MovePolygon(e.X - previousLocation.X, e.Y - previousLocation.Y);
            base.pictureBox1_MouseMove(sender, e);
            previousLocation = e.Location;
        }
        public override void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            form.State = new ManipulationState(form, relationManager);
        }
    }
}
