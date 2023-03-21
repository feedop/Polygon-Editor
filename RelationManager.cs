using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK_CAD.Relations;

namespace GK_CAD
{
    public class RelationManager
    {
        public Dictionary<(Vertex, Vertex), LinkedList<Relation>> Relations { get; set; }
        public HashSet<Vertex> Changed { get; set; }
        public int Counter { get; set; }
        public Vertex? First { get; set; }

        public RelationManager()
        {
            Relations = new Dictionary<(Vertex, Vertex), LinkedList<Relation>>();
            Changed = new HashSet<Vertex>();
            Counter = 0;
        }

        // Resets all relations
        public void ResetRelations()
        {
            Relations = new Dictionary<(Vertex, Vertex), LinkedList<Relation>>();
            Counter = 0;
            ResetChanged();
        }
        // Resets changed vertices
        public void ResetChanged()
        {
            Changed = new HashSet<Vertex>();
            First = null;
        }

        // Delete all relations from an edge
        public void DeleteRelationsFromEdge(Vertex a, Vertex b)
        {
            if (Relations.ContainsKey((a, b)))
            {
                LinkedListNode<Relation>? node = Relations[(a, b)].First;
                // iterate over linked list
                while (node != null)
                {
                    LinkedListNode<Relation>? temp = node.Next;
                    node.ValueRef.Delete(node);
                    node = temp;
                }

            }
        }
    }
}
