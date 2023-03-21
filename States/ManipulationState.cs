using GK_CAD.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    public class ManipulationState : State
    {
        public ManipulationState(Form1 form, RelationManager relationManager) : base(form, relationManager) { }

        public override void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Open context menu
            if (e.Button == MouseButtons.Right)
            {

                for (int i = 0; i < form.Polygons.Count; i++)
                {
                    for (int j = 0; j < form.Polygons[i].Count; j++)
                    {
                        // If a movable edge is found, save it and change state to moving
                        if (Geometry.EdgeHitbox(form.Polygons[i][j],
                            form.Polygons[i][(j + 1) % form.Polygons[i].Count], e))
                        {
                            form.MovingEdge = (i, j);
                            form.contextMenuStrip1.Show(e.X + form.Location.X, e.Y + form.Location.Y);
                            return;
                        }
                    }
                }
                return;
            }
            // Left click
            for (int i = 0; i < form.Polygons.Count; i++)
            {
                for (int j = 0; j < form.Polygons[i].Count; j++)
                {
                    // If a movable vertex is found, save it and change state to moving
                    if (Geometry.VertexHitbox(form.Polygons[i][j], e) == true)
                    {
                        form.MovingVertex = (i, j);
                        form.State = new MovingVertexState(form, relationManager);
                        return;
                    }
                }
            }
            for (int i = 0; i < form.Polygons.Count; i++)
            {
                for (int j = 0; j < form.Polygons[i].Count; j++)
                {
                    // If a movable edge is found, save it and change state to moving
                    if (Geometry.EdgeHitbox(form.Polygons[i][j],
                        form.Polygons[i][(j + 1) % form.Polygons[i].Count], e))
                    {
                        Polygon polygon = form.Polygons[i];
                        int count = polygon.Count;
                        Vertex v0 = polygon[j];
                        Vertex v1 = polygon[(j + 1) % count];
                        form.MovingEdge = (i, j);
                        // Switch to moving the edge, pass the angle of edge with X axis
                        PointF initialV0 = new PointF(v0.X, v0.Y);
                        PointF initialV1 = new PointF(v1.X, v1.Y);
                        bool fixedLength = false;
                        // chceck if there is a FixedLengthRelation on this edge
                        if (relationManager.Relations.ContainsKey((v0, v1)))
                        {
                            foreach (Relation relation in relationManager.Relations[(v0, v1)])
                            {
                                if (relation.Exclusive())
                                {
                                    fixedLength = true;
                                    break;
                                }
                            }
                        }
                        form.State = new MovingEdgeState(form, relationManager, e.Location, initialV0, initialV1,
                            Math.Atan2(polygon[(j + 1) % count].Y - polygon[j].Y,
                            polygon[(j + 1) % count].X - polygon[j].X), fixedLength);
                        
                        return;
                    }
                }
            }
            // Scan polygons' hitboxes
            for (int i = 0; i < form.Polygons.Count; i++)
            {
                if (form.Polygons[i].Hitbox.Contains(e.X, e.Y))
                {
                    form.MovingPolygon = i;
                    form.State = new MovingPolygonState(form, relationManager, e.Location);
                    return;
                }
            }
        }

    }
}
