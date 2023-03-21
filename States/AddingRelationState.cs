using GK_CAD.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    internal class AddingRelationState : State
    {
        private readonly Polygon polygon;
        private readonly Vertex v0, v1;
        private readonly int j0, j1;
        public AddingRelationState(Form1 form, RelationManager relationManager, Polygon polygon, int j0, int j1) : base(form, relationManager)
        {
            this.polygon = polygon;
            this.v0 = polygon[j0];
            this.v1 = polygon[j1];
            this.j0 = j0;
            this.j1 = j1;

            int counter = 0;
            if (relationManager.Relations.ContainsKey((v0, v1)))
            {
                counter = relationManager.Relations[(v0, v1)].Count;
            }
            // Paint the temporary relation
            polygon.TemporaryRelation = new PerpendicularityRelation(polygon, polygon, relationManager, j0, j1, j0, j1, counter);
            form.pictureBox1.Refresh();
        }

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
                        Vertex v2 = form.Polygons[i][j];
                        Vertex v3 = form.Polygons[i][(j + 1) % form.Polygons[i].Count];
                        // An edge cannot have a relation with itself
                        if (v0 == v2 && v1 == v3)
                            continue;
                        // Create relation and add it
                        PerpendicularityRelation relation = new PerpendicularityRelation(polygon, form.Polygons[i], relationManager,
                                j0, j1, j, (j + 1) % form.Polygons[i].Count, relationManager.Counter);

                        LinkedList<Relation> list;
                        if (relationManager.Relations.ContainsKey((v0, v1)))
                        {
                            list = relationManager.Relations[(v0, v1)];
                            if (list.First.ValueRef.Exclusive())
                                list.AddAfter(list.First, relation);
                            else
                                list.AddFirst(relation);
                        }   
                        else
                        {
                            list = new LinkedList<Relation>();
                            list.AddFirst(relation);
                            relationManager.Relations.Add((v0, v1), list);
                        }

                        // Create reverse relation (allows two-way movement)
                        PerpendicularityRelation relationReverse = new PerpendicularityRelation(form.Polygons[i], polygon, relationManager,
                                j, (j + 1) % form.Polygons[i].Count, j0, j1, relationManager.Counter);

                        LinkedList<Relation> reverseList;
                        if (relationManager.Relations.ContainsKey((v2, v3)))
                        {
                            reverseList = relationManager.Relations[(v2, v3)];
                            if (reverseList.First.ValueRef.Exclusive())
                                reverseList.AddAfter(reverseList.First, relationReverse);
                            else
                                reverseList.AddFirst(relationReverse);
                            relation.reverseList = reverseList;
                        }
                        else
                        {
                            reverseList = new LinkedList<Relation>();
                            reverseList.AddFirst(relationReverse);
                            relationManager.Relations.Add((v2, v3), reverseList);
                        }

                        // Fill references to nodes for easy deleting
                        relation.reverseList = reverseList;
                        relation.reverseNode = reverseList.Last;
                        relationReverse.reverseList = list;
                        relationReverse.reverseNode = list.Last;

                        // Total number of relations++
                        relationManager.Counter++;

                        relationManager.Changed.Add(v0);
                        // Immediately propagate the relation
                        relation.Propagate(true);
                        // Cleanup
                        relationManager.ResetChanged();
                        polygon.TemporaryRelation = null;
                        
                        form.pictureBox1.Refresh();

                        // Switch back to creating another relation
                        form.State = new ReadyToAddRelationState(form, relationManager);

                        return;
                    }
                }
            }
        }
    }
}
