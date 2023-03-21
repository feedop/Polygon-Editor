using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    public class ReadyToDrawState : State
    {
        public ReadyToDrawState(Form1 form, RelationManager relationManager) : base(form, relationManager) { }

        // initiate drawing
        public override void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            form.Vertices.Add(new Vertex(e.X, e.Y, 0));
            form.State = new DrawingState(form, relationManager);
            form.pictureBox1.Refresh();
        }
    }
}
