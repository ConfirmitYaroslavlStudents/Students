using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace GraphicalAdapter
{
    public class OpenGLAdapter
    {
        SimpleOpenGlControl _viewport;

        public OpenGLAdapter(SimpleOpenGlControl viewport)
        {
            _viewport = viewport;
            _viewport.InitializeContexts();

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, _viewport.Width, _viewport.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0, _viewport.Height, 0, _viewport.Width);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        public void StartDrawing()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();
        }

        public void EndDrawing()
        {
            Gl.glFlush();
            _viewport.Invalidate();
        }

        public void SetOutputColor(RGBColor color)
        {
            Gl.glColor3ub(color.R, color.G, color.B);
        }

        public void DrawPoint(Vertex2D point)
        {
            Gl.glPointSize(13);
            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glBegin(Gl.GL_POINTS);

            Gl.glVertex2d(point.X, point.Y);

            Gl.glEnd();
        }

        public void DrawText2D(int x, int y, string text)
        {
            Gl.glColor3i(0, 0, 0);
            Gl.glRasterPos2d(x, y);

            foreach (char charForDraw in text)
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_HELVETICA_18, charForDraw);
        }

        public void DrawVector2D(Vertex2D begin, Vertex2D end)
        {
            Gl.glLineWidth(2);

            Vector2DInfo vectorInfo = new Vector2DInfo(new Point(begin.X, begin.Y),
                                                       new Point(end.X, end.Y));

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
    }
}
