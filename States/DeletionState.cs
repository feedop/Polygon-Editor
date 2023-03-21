using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    public class DeletionState : State
    {
        public DeletionState(Form1 form, RelationManager relationManager) : base(form, relationManager) { }

        public override void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            for (int i = 0; i < form.Polygons.Count; i++)
            {
                Polygon polygon = form.Polygons[i];
                for (int j = 0; j < polygon.Count; j++)
                {
                    if (Geometry.VertexHitbox(polygon[j], e) == true)
                    {
                        // delete all relations from neighboring edges
                        relationManager.DeleteRelationsFromEdge(polygon[j == 0 ? polygon.Count - 1 : j - 1], polygon[j]);
                        relationManager.DeleteRelationsFromEdge(polygon[j], polygon[(j + 1) % polygon.Count]);
                        // Delete vertex
                        if (polygon.Count == 3)
                        {
                            form.Polygons.RemoveAt(i);
                            for (int k = i; k < polygon.Count; k++)
                            {
                                polygon[k].Index--;
                            }
                            form.pictureBox1.Refresh();
                            return;
                        }
                        polygon.RemoveAt(j);
                        form.pictureBox1.Refresh();
                        return;
                    }
                }
            }
            for (int i = 0; i < form.Polygons.Count; i++)
            {
                Polygon polygon = form.Polygons[i];
                for (int j = 0; j < form.Polygons[i].Count; j++)
                {
                    // Check for relations
                    Vertex a = polygon[j];
                    Vertex b = polygon[(j + 1) % polygon.Count];
                    PointF middle = Geometry.MiddleOfEdge(a, b);

                    if (relationManager.Relations.ContainsKey((a, b)))
                    {
                        int k = 0;
                        // iterate over linked list
                        for (var node = relationManager.Relations[(a, b)].First; node != null; node = node.Next)
                        {
                            if (node.ValueRef.Hitbox(middle, k, e))
                            {
                                node.ValueRef.Delete(node);
                                form.pictureBox1.Refresh();
                                return;
                            }
                            k++;
                        }
                    }

                    if (Geometry.EdgeHitbox(a, b, e))
                    {
                        // Delete all relations from edge
                        relationManager.DeleteRelationsFromEdge(a, b);
                        // Delete edge
                        if (polygon.Count < 5)
                        {
                            form.Polygons.RemoveAt(i);
                            form.pictureBox1.Refresh();
                            return;
                        }

                        polygon.RemoveAt(j);
                        polygon.RemoveAt(j % form.Polygons[i].Count);

                        // Shift indices
                        for (int k = j; k < polygon.Count; k++)
                        {
                            polygon[k].Index -= 2;
                        }
                        form.pictureBox1.Refresh();
                        return;
                    }
                }
            }
        }
    }
}
