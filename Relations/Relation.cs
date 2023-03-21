using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.Relations
{
    [Serializable]
    public abstract class Relation
    {
        public Vertex V0 { get; private set; }
        public Vertex V1 { get; private set; }
        protected Polygon polygon;
        protected RelationManager relationManager;
        private int counter = 0;
        public int Counter
        {
            get
            {
                return counter;
            }
            protected set
            {
                counter = value;
            }
        }

        public abstract void Propagate(bool increasing);

        public virtual void Delete(LinkedListNode<Relation> node)
        {
            relationManager.Relations[(V0, V1)].Remove(node);
            if (relationManager.Relations[(V0, V1)].Count == 0) relationManager.Relations.Remove((V0, V1));
        }

        public Relation(Polygon polygon, RelationManager relationManager, int i, int j)
        {
            V0 = polygon[i];
            V1 = polygon[j];
            this.polygon = polygon;
            this.relationManager = relationManager;
        }

        public abstract void Draw(Graphics g, SolidBrush brush, Font font, PointF middle, int counter, int i);

        public abstract bool Hitbox(PointF middle, int k, MouseEventArgs e);

        public abstract bool Exclusive();
    }
}
