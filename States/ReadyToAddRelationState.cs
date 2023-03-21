using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    public class ReadyToAddRelationState : State
    {
        public ReadyToAddRelationState(Form1 form, RelationManager relationManager) : base(form, relationManager) { }

        public override void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // Look for an edge
            for (int i = 0; i < form.Polygons.Count; i++)
            {
                for (int j = 0; j < form.Polygons[i].Count; j++)
                {
                    // If a movable edge is found, save it and change state to choosing a second edge
                    if (Geometry.EdgeHitbox(form.Polygons[i][j],
                        form.Polygons[i][(j + 1) % form.Polygons[i].Count], e))
                    {
                        Polygon polygon = form.Polygons[i];
                        int count = polygon.Count;
                        form.MovingEdge = (i, j);
                        // Switch to choosing a second edge
                        form.State = new AddingRelationState(form, relationManager, form.Polygons[i],
                            j, (j + 1) % form.Polygons[i].Count);

                        return;
                    }
                }
            }
        }
    }
}
