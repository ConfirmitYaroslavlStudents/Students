using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using DirectGraph;

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

        public HelpKsyuMainWindow()
        {
            InitializeComponent();

            DisplayForGraph.InitializeContexts();
            InitOpenGL();
        }

        private void InitOpenGL()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, DisplayForGraph.Width, DisplayForGraph.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0, DisplayForGraph.Height, 0, DisplayForGraph.Width);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
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

                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
                Gl.glLoadIdentity();

                DrawStrongConnectedComponents();
                DrawEdgesBetweenConnectedComponents();

                for (int i = 0; i < Graph.VerticesCount; ++i)
                    DrawText2D(VertexCoords[i].X + 2, VertexCoords[i].Y + 2, (VertexCoords[i].Index).ToString());

                Gl.glFlush();
                DisplayForGraph.Invalidate();
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
                Gl.glColor3ub(StrongConnectedComponentColors[i].R,
                              StrongConnectedComponentColors[i].G,
                              StrongConnectedComponentColors[i].B);

                DrawStrongConnectedComponentVertices(i);
                DrawStrongConnectedComponentEdges(i);
            }
        }

        private void DrawStrongConnectedComponentVertices(int i)
        {
            Gl.glPointSize(13);
            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glBegin(Gl.GL_POINTS);

            for (int j = 0; j < StrongConnectedComponentGraph.GetStrongConnegtionComponentVerticesCount(i); ++j)
                Gl.glVertex2i(VertexCoords[StrongConnectedComponentGraph.GetVertex(i, j)].X, VertexCoords[StrongConnectedComponentGraph.GetVertex(i, j)].Y);

            Gl.glEnd();
        }

        private void DrawStrongConnectedComponentEdges(int i)
        {
            Gl.glLineWidth(2);
            for (int j = 0; j < StrongConnectedComponentGraph.GetStrongConnectedComponentEdgesCount(i); ++j)
                DrawVector2D(StrongConnectedComponentGraph.GetEdge(i, j));
        }

        private void DrawEdgesBetweenConnectedComponents()
        {
            Gl.glColor3i(0, 0, 0);
            for (int i = 0; i < Graph.VerticesCount; ++i)
                for (int j = 0; j < Graph.GetVertexDegree(i); ++j)
                    if (!StrongConnectedComponentGraph.IsStrongConnected(Graph.GetEdge(i, j).Begin, Graph.GetEdge(i, j).End))
                        DrawVector2D(Graph.GetEdge(i, j));
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

        private void DrawText2D(int x, int y, string text)
        {
            Gl.glColor3i(0, 0, 0);
            Gl.glRasterPos2d(x, y);

            foreach (char charForDraw in text)
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_HELVETICA_18, charForDraw);
        }

        private void DrawVector2D(DirectedEdge edge)
        {
            Vector2DInfo vectorInfo = new Vector2DInfo(new Point(VertexCoords[edge.Begin].X, VertexCoords[edge.Begin].Y),
                                                       new Point(VertexCoords[edge.End].X, VertexCoords[edge.End].Y));

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(vectorInfo.Begin.X, vectorInfo.Begin.Y);
            Gl.glVertex2d(vectorInfo.End.X, vectorInfo.End.Y);

            Gl.glVertex2d(vectorInfo.End.X, vectorInfo.End.Y);
            Gl.glVertex2d(vectorInfo.End.X + vectorInfo.LeftArrowPoint.X,
                          vectorInfo.End.Y + vectorInfo.LeftArrowPoint.Y);

            Gl.glVertex2d(vectorInfo.End.X, vectorInfo.End.Y);
            Gl.glVertex2d(vectorInfo.End.X + vectorInfo.RightArrowPoint.X,
                          vectorInfo.End.Y + vectorInfo.RightArrowPoint.Y);
            Gl.glEnd();
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