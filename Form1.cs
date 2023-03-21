using GK_CAD.States;
using GK_CAD.PaintingMethods;
using GK_CAD.Relations;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace GK_CAD
{
    public partial class Form1 : Form
    {
        public const int brushWidth = 4;
        public const int penWidth = 1;
        public const int pointHitboxWidth = 8;
        public const int lineHitboxWidth = 5;
        public const int relationHitboxWidth = 12;   
        public const double eps = 1e-10;
        

        public State State { get; set; }
        public Bitmap DrawArea { get; private set; }
        public Pen BlackPen { get; private set; }
        public Pen GrayPen { get; private set; }
        public SolidBrush DrawBrush { get; private set; }
        public SolidBrush RelationBrush { get; private set; }
        public SolidBrush LengthBrush { get; private set; }
        public Font TextFont { get; private set; }

        private readonly PaintingManager paintingManager;
        public RelationManager RelationManager { get; set; }

        // Complete polygons
        public List<Polygon> Polygons { get; set; }

        // Not yet a polygon but needs to be painted
        public List<Vertex> Vertices { get; set; }

        // Currently moved vertex or edge
        public (int, int) MovingVertex { get; set; }
        public (int, int) MovingEdge { get; set; }
        public int MovingPolygon { get; set; }
        public Form1()
        {
            InitializeComponent();
            paintingManager = new PaintingManager(new GDIPlusPainting(), this);
            RelationManager = new RelationManager();
            State = new ReadyToDrawState(this, RelationManager);

            Polygons = new List<Polygon>();
            Vertices = new List<Vertex>();

            DrawArea = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = DrawArea;

            BlackPen = new Pen(Brushes.Black, penWidth);
            GrayPen = new Pen(Brushes.Gray, penWidth);
            GrayPen.DashPattern = new float[] { 1.0F, 1.0F, 1.0F, 1.0F };
            DrawBrush = new SolidBrush(Color.Black);

            LengthBrush = new SolidBrush(Color.DarkGreen);
            RelationBrush = new SolidBrush(Color.DarkMagenta);
            TextFont = new Font("Arial", relationHitboxWidth);

            GDIPlusButton.Checked = true;
            BresenhamButton.Checked = false;
            PresetScene.SetScene(Polygons, RelationManager, TextFont);
        }

        private void RefreshPoints()
        {
            Vertices = new List<Vertex>();

            foreach (Polygon polygon in Polygons)
            {
                polygon.TemporaryRelation = null;
            }

            pictureBox1.Refresh();
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            paintingManager.Paint();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            State.pictureBox1_MouseDown(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            State.pictureBox1_MouseMove(sender, e);
        }
        private void drawingButton_Click(object sender, EventArgs e)
        {
            State = new ReadyToDrawState(this, RelationManager);
            RefreshPoints();
            drawingButton.BackColor = SystemColors.ActiveCaption;
            manipulationButton.BackColor = SystemColors.Control;
            deletionButton.BackColor = SystemColors.Control;
            relationButton.BackColor = SystemColors.Control;
        }
        private void manipulationButton_Click(object sender, EventArgs e)
        {
            State = new ManipulationState(this, RelationManager);
            RefreshPoints();
            drawingButton.BackColor = SystemColors.Control;
            manipulationButton.BackColor = SystemColors.ActiveCaption;
            deletionButton.BackColor = SystemColors.Control;
            relationButton.BackColor = SystemColors.Control;
        }

        private void deletionButton_Click(object sender, EventArgs e)
        {
            State = new DeletionState(this, RelationManager);
            RefreshPoints();
            drawingButton.BackColor = SystemColors.Control;
            manipulationButton.BackColor = SystemColors.Control;
            deletionButton.BackColor = SystemColors.ActiveCaption;
            relationButton.BackColor = SystemColors.Control;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {;
            Polygons = new List<Polygon>();
            RelationManager.ResetRelations();
            RefreshPoints();
            State = new ReadyToDrawState(this, RelationManager);
            drawingButton.BackColor = SystemColors.ActiveCaption;
            manipulationButton.BackColor = SystemColors.Control;
            deletionButton.BackColor = SystemColors.Control;
            relationButton.BackColor = SystemColors.Control;
        }

        private void relationButton_Click(object sender, EventArgs e)
        {
            State = new ReadyToAddRelationState(this, RelationManager);
            RefreshPoints();
            drawingButton.BackColor = SystemColors.Control;
            manipulationButton.BackColor = SystemColors.Control;
            deletionButton.BackColor = SystemColors.Control;
            relationButton.BackColor = SystemColors.ActiveCaption;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            State.pictureBox1_MouseUp(sender, e);
        }

        private void BresenhamButton_CheckedChanged(object sender, EventArgs e)
        {
            paintingManager.PaintingMethod = new BresenhamPaintning();
            pictureBox1.Refresh();
            pictureBox1.Refresh();
        }

        private void GDIPlusButton_CheckedChanged(object sender, EventArgs e)
        {
            paintingManager.PaintingMethod = new GDIPlusPainting();
            pictureBox1.Refresh();
            pictureBox1.Refresh();
        }

        // Add a vertex in the middle of an edge
        private void utwórzWierzcho³ekWŒrodkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = MovingEdge.Item1;
            int j = MovingEdge.Item2;
            int j1 = (j + 1) % Polygons[i].Count;
            Vertex a = Polygons[i][j];
            Vertex b = Polygons[i][j1];
            PointF middle = Geometry.MiddleOfEdge(a, b);

            // delete all relations
            RelationManager.DeleteRelationsFromEdge(a, b);

            // Add a new vertex
            Polygons[i].AddVertex((j + 1) % (Polygons[i].Count), new Vertex(middle.X, middle.Y, (j + 1) % (Polygons[i].Count)));
            
            // Shift indices
            for (int k = j1 + 1; k < Polygons[i].Count; k++)
            {
                Polygons[i][k].Index++;
            }
            pictureBox1.Refresh();
        }

        // Fix edge length
        private void ustalD³ugoœæKrawêdziToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = MovingEdge.Item1;
            int j = MovingEdge.Item2;
            Vertex v0 = Polygons[i][j];
            Vertex v1 = Polygons[i][(j + 1) % Polygons[i].Count];
            float length = (float)Math.Sqrt((v1.X - v0.X) * (v1.X - v0.X) + (v1.Y - v0.Y) * (v1.Y - v0.Y));
            using (Form2 form2 = new Form2(length))
            {
                form2.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = form2.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    float newLength = form2.Result;
                    Relation relation = new FixedLengthRelation(Polygons[i], RelationManager, j, (j + 1) % Polygons[i].Count, newLength, TextFont);
                    if (RelationManager.Relations.ContainsKey((v0, v1)))
                    {
                        bool flag = false;
                        LinkedListNode<Relation>? node = RelationManager.Relations[(v0, v1)].First;
                        while (node != null)
                        {
                            if (node.ValueRef.Exclusive())
                            {
                                flag = true;
                                break;
                            }
                            node = node.Next;
                        }
                        if (flag) // node is not null at this point
                        {
                            (node.ValueRef as FixedLengthRelation).Length = newLength;
                            relation.Propagate(true);
                        }
                        else
                        {
                            RelationManager.Relations[(v0, v1)].AddFirst(relation);
                        }
                    }
                    else
                    {
                        LinkedList<Relation> list = new LinkedList<Relation>();
                        list.AddFirst(relation);
                        RelationManager.Relations.Add((v0, v1), list);
                    }
                    RelationManager.First = v1;
                    relation.Propagate(true);
                    RelationManager.First = null;
                    pictureBox1.Refresh();
                }
            }     
        }

        // Insert two Bezier Vertices
        private void krzywaBezieraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = MovingEdge.Item1;
            int j = MovingEdge.Item2;
            int j1 = (j + 1) % Polygons[i].Count;
            Vertex a = Polygons[i][j];
            Vertex b = Polygons[i][j1];
            RelationManager.DeleteRelationsFromEdge(a, b);
            if (j != Polygons[i].Count - 1){
                BezierVertex bv1 = new BezierVertex(a.X + (b.X - a.X) / 3, a.Y + (b.Y - a.Y) / 3, (j + 1) % (Polygons[i].Count), a);
                Polygons[i].AddVertex((j + 1) % (Polygons[i].Count), bv1);
                BezierVertex bv2 = new BezierVertex(a.X + (b.X - a.X) * 2 / 3, a.Y + (b.Y - a.Y) * 2 / 3, (j + 2) % (Polygons[i].Count), b);
                Polygons[i].AddVertex((j + 2) % (Polygons[i].Count), bv2);
            }
            else
            {
                BezierVertex bv1 = new BezierVertex(a.X + (b.X - a.X) * 2 / 3, a.Y + (b.Y - a.Y) * 2 / 3, (j + 1) % (Polygons[i].Count), b);
                Polygons[i].AddVertex((j + 1) % (Polygons[i].Count), bv1);
                BezierVertex bv2 = new BezierVertex(a.X + (b.X - a.X) / 3, a.Y + (b.Y - a.Y) / 3, (j + 2) % (Polygons[i].Count), a);
                Polygons[i].AddVertex((j + 2) % (Polygons[i].Count), bv2);
            }

            for (int k = (j == 0 || j == Polygons[i].Count - 1) ? j1 + 2 % Polygons[i].Count : j1 + 2; k < Polygons[i].Count; k++)
            {
                Polygons[i][k].Index += 2;
            }
            pictureBox1.Refresh();
            pictureBox1.Refresh();
        }
    }
}