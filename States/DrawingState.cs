using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    public class DrawingState : State
    {
        private int index;
        public DrawingState(Form1 form, RelationManager relationManager) : base(form, relationManager)
        {
            index = 0;
        }

        public override void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Vertex initial = form.Vertices[0];
            // check hitbox to determine whether returned to point 0
            if (Geometry.VertexHitbox(initial, e) == true && form.Vertices.Count  >= 3)
            {
                form.Polygons.Add(new Polygon(form.Vertices, form.RelationManager));
                form.Vertices = new List<Vertex>();
                form.State = new ReadyToDrawState(form, relationManager);
                form.pictureBox1.Refresh();
                return;
            }

            form.Vertices.Add(new Vertex(e.X, e.Y, ++index));
            form.pictureBox1.Refresh();            
        }

    }
}
