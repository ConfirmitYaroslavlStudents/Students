using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.Platform.Windows;
using CommonComponents;
using GraphicalAdapter;

namespace HelpKsyu
{
    public partial class HelpKsyuMainWindow : Form
    {
        DirectedGraph Graph;
        internal KosarajuAlgorithm StrongConnectedComponentGraph;
        Vertex2D[] VertexCoords;
        internal RGBColor[] StrongConnectedComponentColors;
        StrongConnectedComponentInfo strongConnectedComponentInfo;
        Random rnd = new Random();
        OpenGLAdapter glDrawer;

        public HelpKsyuMainWindow()
        {
            InitializeComponent();

            glDrawer = new OpenGLAdapter(DisplayForGraph);
        }

        private void ReadGraphFromAConsole_Click(object sender, EventArgs e)
        {
            Graph = DirectedGraphReader.ReadFromConsole();
        }

        private void ReadGraphFromATextFile_Click(object sender, EventArgs e)
        {
            openInputTxt.ShowDialog();
            string fileName = openInputTxt.FileName;

            Graph = DirectedGraphReader.ReadFromTxtFile(fileName);
        }

        private void ShowGraphs_Click(object sender, EventArgs e)
        {
            if (Graph != null)
            {
                GenStrongConnectedComponentGraph();

                glDrawer.StartDrawing();

                DrawStrongConnectedComponents();
                DrawEdgesBetweenConnectedComponents();

                for (int i = 0; i < Graph.VerticesCount; ++i)
                    glDrawer.DrawText2D(VertexCoords[i].X + 2, VertexCoords[i].Y + 2, (VertexCoords[i].Index).ToString());

                glDrawer.EndDrawing();
            }
            else
                MessageBox.Show("Graph == null", "Error");
        }

        private void GenStrongConnectedComponentGraph()
        {
            StrongConnectedComponentGraph = new KosarajuAlgorithm(Graph);
            GenStrongConnectedComponentColors();

            strongConnectedComponentInfo = new StrongConnectedComponentInfo(StrongConnectedComponentGraph, StrongConnectedComponentColors);

            GenVertexCoord();
        }

        private void GenStrongConnectedComponentColors()
        {
            StrongConnectedComponentColors = new RGBColor[StrongConnectedComponentGraph.StrongConnectedComponentCount];

            for (int i = 0; i < StrongConnectedComponentGraph.StrongConnectedComponentCount; ++i)
                StrongConnectedComponentColors[i] = new RGBColor((byte)rnd.Next(25, 255),
                                                                 (byte)rnd.Next(25, 255),
                                                                 (byte)rnd.Next(25, 255));
        }

        private void DrawStrongConnectedComponents()
        {
            for (int i = 0; i < StrongConnectedComponentGraph.StrongConnectedComponentCount; ++i)
            {
                glDrawer.SetOutputColor(StrongConnectedComponentColors[i]);

                DrawStrongConnectedComponentVertices(i);
                DrawStrongConnectedComponentEdges(i);
            }
        }

        private void DrawStrongConnectedComponentVertices(int i)
        {
            for (int j = 0; j < StrongConnectedComponentGraph.GetStrongConnegtionComponentVerticesCount(i); ++j)
                glDrawer.DrawPoint(VertexCoords[StrongConnectedComponentGraph.GetVertex(i, j)]);
        }

        private void DrawStrongConnectedComponentEdges(int i)
        {
            for (int j = 0; j < StrongConnectedComponentGraph.GetStrongConnectedComponentEdgesCount(i); ++j)
                glDrawer.DrawVector2D(VertexCoords[StrongConnectedComponentGraph.GetEdge(i, j).Begin],
                                      VertexCoords[StrongConnectedComponentGraph.GetEdge(i, j).End]);
        }

        private void DrawEdgesBetweenConnectedComponents()
        {
            glDrawer.SetOutputColor(new RGBColor(0, 0, 0));
            for (int i = 0; i < Graph.VerticesCount; ++i)
                for (int j = 0; j < Graph.GetVertexDegree(i); ++j)
                    if (!StrongConnectedComponentGraph.IsStrongConnected(Graph.GetEdge(i, j).Begin, Graph.GetEdge(i, j).End))
                        glDrawer.DrawVector2D(VertexCoords[Graph.GetEdge(i, j).Begin],
                                              VertexCoords[Graph.GetEdge(i, j).End]);
        }

        private void GenVertexCoord()
        {
            VertexCoords = new Vertex2D[Graph.VerticesCount];
            for (int i = 0; i < Graph.VerticesCount; ++i)
            {
                VertexCoords[i].Index = i + 1;
                VertexCoords[i].X = rnd.Next(8, DisplayForGraph.Height - 23);
                VertexCoords[i].Y = rnd.Next(8, DisplayForGraph.Width - 23);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitForm exitForm = new ExitForm();
            exitForm.ShowDialog();
        }

        private void fullSCCsInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strongConnectedComponentInfoForm = new StrongConnectedComponentInfoForm(strongConnectedComponentInfo);
            strongConnectedComponentInfoForm.Show();
        }
    }
}