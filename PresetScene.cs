using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK_CAD.Relations;

namespace GK_CAD
{
    public static class PresetScene
    {
        // Initialize the scene with two preset polygons
        public static void SetScene(List<Polygon> polygons, RelationManager relationManager, Font font)
        {
            Vertex[][] vertices = new Vertex[2][];
            vertices[0] = new Vertex[7];
            vertices[1] = new Vertex[8];

            vertices[0][0] = new Vertex(272, 247, 0);
            vertices[0][1] = new Vertex(334.673, 117.1455, 1);
            vertices[0][2] = new Vertex(450, 46, 2);
            vertices[0][3] = new Vertex(581.8887, 158.295929, 3);
            vertices[0][4] = new Vertex(674.470032, 285.89563, 4);
            vertices[0][5] = new Vertex(283.708069, 449.8516, 5);
            vertices[0][6] = new Vertex(125.535324, 315.176178, 6);

            vertices[1][0] = new Vertex(375.564636, 606.1591, 0);
            vertices[1][1] = new Vertex(527.6471, 427.542236, 1);
            vertices[1][2] = new Vertex(583.6883, 495.896729, 2);
            vertices[1][3] = new Vertex(672.911438, 354.806976, 3);
            vertices[1][4] = new Vertex(874.6952, 378.13913, 4);
            vertices[1][5] = new Vertex(832.132446, 428.12796, 5);
            vertices[1][6] = new Vertex(879.9703, 765.2317, 6);
            vertices[1][7] = new Vertex(708.654968, 619.36615, 7);

            Polygon polygon0 = new Polygon(vertices[0], relationManager);
            Polygon polygon1 = new Polygon(vertices[1], relationManager);
            polygons.Add(polygon0);
            polygons.Add(polygon1);

            LinkedList<Relation>[] relations = new LinkedList<Relation>[9];

            for (int i = 0; i < relations.Length; i++)
            {
                relations[i] = new LinkedList<Relation>();
            }

            relations[0].AddLast(new FixedLengthRelation(polygon0, relationManager, 2, 3, (float)173.219513, font));

            relations[0].AddLast(new PerpendicularityRelation(polygon0, polygon1, relationManager, 2, 3, 0, 1, 1));
            LinkedListNode<Relation> node1 = relations[0].Last;
            relations[0].AddLast(new PerpendicularityRelation(polygon0, polygon1, relationManager, 2, 3, 4, 5, 0));
            LinkedListNode<Relation> node0 = relations[0].Last;

            relations[1].AddLast(new PerpendicularityRelation(polygon1, polygon0, relationManager, 4, 5, 2, 3, 0));
            LinkedListNode<Relation> node0r = relations[1].Last;
            relations[1].AddLast(new PerpendicularityRelation(polygon1, polygon1, relationManager, 4, 5, 6, 7, 3));
            LinkedListNode<Relation> node4 = relations[1].Last;

            relations[2].AddLast(new FixedLengthRelation(polygon0, relationManager, 3, 4, (float)157.648346, font));

            relations[3].AddLast(new FixedLengthRelation(polygon0, relationManager, 1, 2, (float)135.506454, font));

            relations[4].AddLast(new FixedLengthRelation(polygon1, relationManager, 0, 1, (float)234.491263, font));
            relations[4].AddLast(new PerpendicularityRelation(polygon1, polygon0, relationManager, 0, 1, 2, 3, 1));
            LinkedListNode<Relation> node1r = relations[4].Last;
            relations[4].AddLast(new PerpendicularityRelation(polygon1, polygon1, relationManager, 0, 1, 1, 2, 2));
            LinkedListNode<Relation> node2 = relations[4].Last;

            relations[5].AddLast(new FixedLengthRelation(polygon1, relationManager, 1, 2, (float)88.39091, font));
            relations[5].AddLast(new PerpendicularityRelation(polygon1, polygon1, relationManager, 1, 2, 0, 1, 2));
            LinkedListNode<Relation> node2r = relations[5].Last;

            relations[7].AddLast(new FixedLengthRelation(polygon1, relationManager, 3, 4, (float)203.128189, font));

            relations[8].AddLast(new PerpendicularityRelation(polygon1, polygon1, relationManager, 6, 7, 4, 5, 3));
            LinkedListNode<Relation> node4r = relations[8].Last;

            (node0.ValueRef as PerpendicularityRelation).reverseNode = node0r;
            (node0.ValueRef as PerpendicularityRelation).reverseList = relations[1];
            (node0r.ValueRef as PerpendicularityRelation).reverseNode = node0;
            (node0r.ValueRef as PerpendicularityRelation).reverseList = relations[0];

            (node1.ValueRef as PerpendicularityRelation).reverseNode = node1r;
            (node1.ValueRef as PerpendicularityRelation).reverseList = relations[4];
            (node1r.ValueRef as PerpendicularityRelation).reverseNode = node1;
            (node1r.ValueRef as PerpendicularityRelation).reverseList = relations[0];

            (node2.ValueRef as PerpendicularityRelation).reverseNode = node2r;
            (node2.ValueRef as PerpendicularityRelation).reverseList = relations[5];
            (node2r.ValueRef as PerpendicularityRelation).reverseNode = node2;
            (node2r.ValueRef as PerpendicularityRelation).reverseList = relations[4];

            (node4.ValueRef as PerpendicularityRelation).reverseNode = node4r;
            (node4.ValueRef as PerpendicularityRelation).reverseList = relations[8];
            (node4r.ValueRef as PerpendicularityRelation).reverseNode = node4;
            (node4r.ValueRef as PerpendicularityRelation).reverseList = relations[1];


            relationManager.Relations.Add((vertices[0][2], vertices[0][3]), relations[0]);
            relationManager.Relations.Add((vertices[1][4], vertices[1][5]), relations[1]);
            relationManager.Relations.Add((vertices[0][3], vertices[0][4]), relations[2]);
            relationManager.Relations.Add((vertices[0][1], vertices[0][2]), relations[3]);
            relationManager.Relations.Add((vertices[1][0], vertices[1][1]), relations[4]);
            relationManager.Relations.Add((vertices[1][1], vertices[1][2]), relations[5]);
            relationManager.Relations.Add((vertices[1][3], vertices[1][4]), relations[7]);
            relationManager.Relations.Add((vertices[1][6], vertices[1][7]), relations[8]);
            relationManager.Counter = 4;
        }
    }

    
}
