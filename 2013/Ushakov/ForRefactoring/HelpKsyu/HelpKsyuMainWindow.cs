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
using DirectedGraph;

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

		public struct Color
		{
			public int R, G, B, A;

			public Color(int r, int g, int b, int a)
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

		static DGraph G;
		static internal KosarajuAlgorithm Gscc;
		static Vertex2D[] VertexCoords;
		static internal Color[] SCCClrs;

		public HelpKsyuMainWindow()
		{
			InitializeComponent();

			DsplForGraph.InitializeContexts();
			// инициализация Glut 
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

			// очитка окна 
			Gl.glClearColor(255, 255, 255, 1);

			// установка порта вывода в соотвествии с размерами элемента DsplForGraph
			Gl.glViewport(0, 0, DsplForGraph.Width, DsplForGraph.Height);

			// настройка проекции 
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

			// теперь необходимо корректно настроить 2D ортогональную проекцию 
			Glu.gluOrtho2D(0, DsplForGraph.Height, 0, DsplForGraph.Width);

			// установка объектно-видовой матрицы 
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
		}

		private void ReadGraphFromAConsole_Click(object sender, EventArgs e)
		{
			AllocConsole();

			G = new DGraph();
			G.ReadFromConsole();

			FreeConsole();
		}

		private void ReadGraphFromATextFile_Click(object sender, EventArgs e)
		{
			openInputTxt.ShowDialog();
			string file = openInputTxt.FileName;

			G = new DGraph();
			G.ReadFromTxtFile(file);
		}

		private void ShowGraphs_Click(object sender, EventArgs e)
		{
			Gscc = new KosarajuAlgorithm(G);

			SCCClrs = new Color[Gscc.SCCCount];

			VertexCoords = new Vertex2D[G.VerticesCount];
			Random rndm = new Random();
			for (int i = 0; i < G.VerticesCount; ++i)
			{
				VertexCoords[i].v = i + 1;
				VertexCoords[i].x = rndm.Next(8, DsplForGraph.Height - 23);
				VertexCoords[i].y = rndm.Next(8, DsplForGraph.Width - 23);
			}

			// очищаем буфер цвета 
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

			// очищаем текущую матрицу 
			Gl.glLoadIdentity();
			
			for (int i = 0; i < Gscc.SCCCount; ++i)
			{
				byte r = (byte)rndm.Next(25, 255);
				byte g = (byte)rndm.Next(25, 255);
				byte b = (byte)rndm.Next(25, 255);
				byte a = 255;
				Gl.glColor4ub(r, g, b, a);
				SCCClrs[i] = new Color(r, g, b, a);

				Gl.glPointSize(13);
				Gl.glEnable(Gl.GL_POINT_SMOOTH);
				Gl.glBegin(Gl.GL_POINTS);

				for (int j = 0; j < Gscc.SCCVrtxCount(i); ++j)
					Gl.glVertex2i(VertexCoords[Gscc.GetVertex(i, j)].x, VertexCoords[Gscc.GetVertex(i, j)].y);

				Gl.glEnd();

				Gl.glLineWidth(2);
				for (int j = 0; j < Gscc.SCCEdgCount(i); ++j)
					PrintVector2D(Gscc.GetEdge(i, j));
			}

			Gl.glColor3i(0, 0, 0);
			for (int i = 0; i < G.VerticesCount; ++i)
				for (int j = 0; j < G.VertexDegree(i); ++j)
					if (!Gscc.StrongConnect(G.GetEdge(i, j).Begin, G.GetEdge(i, j).End))
						PrintVector2D(G.GetEdge(i, j));

			// расставляем номера у точек
			for (int i = 0; i < G.VerticesCount; ++i)
				PrintText2D(VertexCoords[i].x + 2, VertexCoords[i].y + 2, (VertexCoords[i].v).ToString());

			// дожидаемся конца визуализации кадра 
			Gl.glFlush();

			// посылаем сигнал перерисовки элемента DsplForGraph. 
			DsplForGraph.Invalidate();
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
			foreach (char char_for_draw in text)
				Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_HELVETICA_18, char_for_draw);
		}

		private void PrintVector2D(DEdge edg)
		{
			int x1 = VertexCoords[edg.Begin].x;
			int y1 = VertexCoords[edg.Begin].y;
			int x2 = VertexCoords[edg.End].x;
			int y2 = VertexCoords[edg.End].y;

			double tx = x1 - x2;
			double ty = y1 - y2;
			double sq = Math.Sqrt(tx * tx + ty * ty);
			tx /= sq;
			ty /= sq;

			double v1x = 6.5 * tx + ty;
			double v1y = 6.5 * ty - tx;
			double v2x = 6.5 * tx - ty;
			double v2y = 6.5 * ty + tx;
			sq = Math.Sqrt(v1x * v1x + v1y * v1y);
			v1x /= sq;
			v1y /= sq;
			sq = Math.Sqrt(v2x * v2x + v2y * v2y);
			v2x /= sq;
			v2y /= sq;

			Gl.glBegin(Gl.GL_LINES);
				Gl.glVertex2d(x1, y1);
				Gl.glVertex2d(x2, y2);
				Gl.glVertex2d(x2, y2);
				Gl.glVertex2d(x2 + 25 * v1x, y2 + 25 * v1y);
				Gl.glVertex2d(x2, y2);
				Gl.glVertex2d(x2 + 25 * v2x, y2 + 25 * v2y);
			Gl.glEnd();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PE pe = new PE();
			pe.ShowDialog();
		}

		private void fullSCCsInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SCCInfo scc_inf = new SCCInfo(Gscc, SCCClrs);
			scc_inf.Show();
		}
	}
}
