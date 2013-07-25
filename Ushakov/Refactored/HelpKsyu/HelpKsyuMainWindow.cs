using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using DirectGraph;

namespace HelpKsyu
{
	public partial class HelpKsyuMainWindow : Form
	{
		#region LittleTrickWithConsole

		[DllImport("kernel32.dll")]
		public static extern Boolean AllocConsole();

		[DllImport("kernel32.dll")]
		public static extern Boolean FreeConsole();

		#endregion

		public struct RGBAColor
		{
			public byte R, G, B, A;

			public RGBAColor(byte r, byte g, byte b, byte a)
			{
				R = r;
				G = g;
				B = b;
				A = a;
			}
		};

		struct Vertex2D
		{
			public int v, x, y;

			public Vertex2D(int v, int x, int y)
			{
				this.v = v;
				this.x = x;
				this.y = y;
			}
		};

		DirectedGraph Graph;
		internal KosarajuAlgorithm StrongConnectedComponentGraph;
		Vertex2D[] VertexCoords;
		internal RGBAColor[] StrongConnectedComponentColors;
        StrongConnectedComponentInfo SCCInfo;

		public HelpKsyuMainWindow()
		{
			InitializeComponent();

			DisplayForGraph.InitializeContexts();
			// инициализация Glut 
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

			// очитка окна 
			Gl.glClearColor(255, 255, 255, 1);

			// установка порта вывода в соотвествии с размерами элемента DsplForGraph
			Gl.glViewport(0, 0, DisplayForGraph.Width, DisplayForGraph.Height);

			// настройка проекции 
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

			// теперь необходимо корректно настроить 2D ортогональную проекцию 
			Glu.gluOrtho2D(0, DisplayForGraph.Height, 0, DisplayForGraph.Width);

			// установка объектно-видовой матрицы 
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
		}

		private void ReadGraphFromAConsole_Click(object sender, EventArgs e)
		{
			AllocConsole();

			Graph = new DirectedGraph();
			Graph.ReadFromConsole();

			FreeConsole();
		}

		private void ReadGraphFromATextFile_Click(object sender, EventArgs e)
		{
			openInputTxt.ShowDialog();
			string fileName = openInputTxt.FileName;

			Graph = new DirectedGraph();
			Graph.ReadFromTxtFile(fileName);
		}

		private void ShowGraphs_Click(object sender, EventArgs e)
		{
			StrongConnectedComponentGraph = new KosarajuAlgorithm(Graph);
            StrongConnectedComponentColors = new RGBAColor[StrongConnectedComponentGraph.StrongConnectedComponentCount];

            SCCInfo = new StrongConnectedComponentInfo(StrongConnectedComponentGraph, StrongConnectedComponentColors);

			VertexCoords = new Vertex2D[Graph.VerticesCount];
			Random rndm = new Random();
			for (int i = 0; i < Graph.VerticesCount; ++i)
			{
				VertexCoords[i].v = i + 1;
				VertexCoords[i].x = rndm.Next(8, DisplayForGraph.Height - 23);
				VertexCoords[i].y = rndm.Next(8, DisplayForGraph.Width - 23);
			}

			// очищаем буфер цвета 
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

			// очищаем текущую матрицу 
			Gl.glLoadIdentity();
			
			for (int i = 0; i < StrongConnectedComponentGraph.StrongConnectedComponentCount; ++i)
			{
				byte r = (byte)rndm.Next(25, 255);
				byte g = (byte)rndm.Next(25, 255);
				byte b = (byte)rndm.Next(25, 255);
				byte a = 255;
				Gl.glColor4ub(r, g, b, a);
				StrongConnectedComponentColors[i] = new RGBAColor(r, g, b, a);

				Gl.glPointSize(13);
				Gl.glEnable(Gl.GL_POINT_SMOOTH);
				Gl.glBegin(Gl.GL_POINTS);

				for (int j = 0; j < StrongConnectedComponentGraph.SCCVerticesCount(i); ++j)
					Gl.glVertex2i(VertexCoords[StrongConnectedComponentGraph.GetVertex(i, j)].x, VertexCoords[StrongConnectedComponentGraph.GetVertex(i, j)].y);

				Gl.glEnd();

				Gl.glLineWidth(2);
				for (int j = 0; j < StrongConnectedComponentGraph.SCCEdgesCount(i); ++j)
					PrintVector2D(StrongConnectedComponentGraph.GetEdge(i, j));
			}

			Gl.glColor3i(0, 0, 0);
			for (int i = 0; i < Graph.VerticesCount; ++i)
				for (int j = 0; j < Graph.GetVertexDegree(i); ++j)
					if (!StrongConnectedComponentGraph.StrongConnect(Graph.GetEdge(i, j).Begin, Graph.GetEdge(i, j).End))
						PrintVector2D(Graph.GetEdge(i, j));

			// расставляем номера у точек
			for (int i = 0; i < Graph.VerticesCount; ++i)
				PrintText2D(VertexCoords[i].x + 2, VertexCoords[i].y + 2, (VertexCoords[i].v).ToString());

			// дожидаемся конца визуализации кадра 
			Gl.glFlush();

			// посылаем сигнал перерисовки элемента DsplForGraph. 
			DisplayForGraph.Invalidate();
		}

		private void PrintText2D(int x, int y, string text)
		{
			// устанавливаем чёрный цвет
			Gl.glColor3i(0, 0, 0);

			// устанавливаем позицию вывода растровых символов 
			// в переданных координатах x и y. 
			Gl.glRasterPos2d(x, y);

			// в цикле foreach перебираем значения из массива text, 
			// который содержит значение строки для визуализации 
			foreach (char charForDraw in text)
				Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_HELVETICA_18, charForDraw);
		}

		private void PrintVector2D(DirectedEdge edge)
		{
            double deltaX = VertexCoords[edge.Begin].x - VertexCoords[edge.End].x;
            double deltaY = VertexCoords[edge.Begin].y - VertexCoords[edge.End].y;
			double length = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
			deltaX /= length;
			deltaY /= length;

			double leftArrowPointX = 6.5 * deltaX + deltaY;
			double leftArrowPointY = 6.5 * deltaY - deltaX;
			double rightArrowPointX = 6.5 * deltaX - deltaY;
			double rightArrowPointY = 6.5 * deltaY + deltaX;
			
            length = Math.Sqrt(leftArrowPointX * leftArrowPointX + leftArrowPointY * leftArrowPointY);
			leftArrowPointX /= length;
			leftArrowPointY /= length;
			length = Math.Sqrt(rightArrowPointX * rightArrowPointX + rightArrowPointY * rightArrowPointY);
			rightArrowPointX /= length;
			rightArrowPointY /= length;

			Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(VertexCoords[edge.Begin].x, VertexCoords[edge.Begin].y);
                Gl.glVertex2d(VertexCoords[edge.End].x, VertexCoords[edge.End].y);

                Gl.glVertex2d(VertexCoords[edge.End].x, VertexCoords[edge.End].y);
                Gl.glVertex2d(VertexCoords[edge.End].x + 25 * leftArrowPointX, VertexCoords[edge.End].y + 25 * leftArrowPointY);
           
                Gl.glVertex2d(VertexCoords[edge.End].x, VertexCoords[edge.End].y);
                Gl.glVertex2d(VertexCoords[edge.End].x + 25 * rightArrowPointX, VertexCoords[edge.End].y + 25 * rightArrowPointY);
			Gl.glEnd();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExitForm exitForm = new ExitForm();
			exitForm.ShowDialog();
		}

		private void fullSCCsInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SCCInfoForm strongConnectedComponentInfo = new SCCInfoForm(SCCInfo);
			strongConnectedComponentInfo.Show();
		}
	}
}
