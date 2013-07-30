using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace mapDesigner
{
    public partial class newLevelDesigner : Form
    {
        private int _numberLines;
        private int _numberColumns;
        private List<Rectangle> _allRectanglesInMap;
        private List<Rectangle> _wallCoordinates;
        private Point _pacManCoordinate;
        private Point _enemyCoordinate;
        private Form _parentForm;

        public newLevelDesigner(int numberLines, int numberColumns, Form parentForm)
        {
            InitializeComponent();
            this._parentForm = parentForm;
            this.Width = numberColumns * 50 +18;
            this.Height = numberLines * 50 + 90;

            wall.Top = this.Height - 80;
            pacMan.Top = this.Height - 80;
            enemy.Top = this.Height - 80;
            save.Top = this.Height - 60;
            clear.Top = this.Height - 60;
            exit.Top = this.Height - 60;

            this._numberColumns = numberColumns;
            this._numberLines = numberLines;

            this._allRectanglesInMap = new List<Rectangle>();
            this._wallCoordinates = new List<Rectangle>();
            this._pacManCoordinate = new Point(-50, -50);
            this._enemyCoordinate = new Point(-50, -50);
            for (int i = 0; i < _numberLines; i++)
            {
                for (int j = 0; j < _numberColumns; j++)
                {
                    _allRectanglesInMap.Add(new Rectangle(50 * j , 50 * i , 50, 50));
                }
            }
        }
        private bool isNotWall(Rectangle rect)
        {
            foreach (Rectangle rectangle in _wallCoordinates)
            {
                if (rectangle == rect)
                    return false;
            }
            return true;
        }
        private void newLevelDesigner_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Rectangle rect in _allRectanglesInMap)
            {
                g.DrawRectangle(Pens.Blue, rect);
            }
            foreach (Rectangle rect in _wallCoordinates)
            {
                g.FillRectangle(Brushes.Aquamarine, rect);
            }
            g.FillEllipse(Brushes.Yellow, _pacManCoordinate.X, _pacManCoordinate.Y, 50, 50);
            g.FillEllipse(Brushes.Indigo, _enemyCoordinate.X, _enemyCoordinate.Y, 50, 50);

        }
        private void newLevelDesigner_MouseClick(object sender, MouseEventArgs e)
        {
            if (wall.Checked)
                MouseClickWall(e);
            if (pacMan.Checked)
                MouseClickPacManOrEnemy(e,true);
            if (enemy.Checked)
                MouseClickPacManOrEnemy(e, false);
        }
        private void MouseClickWall(MouseEventArgs e)
        {
            foreach (Rectangle rect in _allRectanglesInMap)
            {
                if (rect.Contains(new Point(e.X, e.Y)))
                {
                    if (isNotWall(rect))
                    {
                        _wallCoordinates.Add(rect);
                    }
                    else
                    {
                        _wallCoordinates.Remove(rect);
                    }
                    break;
                }
            }
            Invalidate();
        }
        private void MouseClickPacManOrEnemy(MouseEventArgs e,bool ItIsPacMan)
        {
            foreach (Rectangle rect in _allRectanglesInMap)
            {
                if (rect.Contains(new Point(e.X, e.Y)))
                {
                    if (isNotWall(rect) && ItIsPacMan)
                    {
                        _pacManCoordinate = new Point(rect.X, rect.Y);
                    }
                    if (isNotWall(rect) && !ItIsPacMan) 
                    {
                        _enemyCoordinate = new Point(rect.X, rect.Y);
                    }
                    break;
                }
            }
            Invalidate();
        }
        private void save_Click(object sender, EventArgs e)
        {
            var myFile = File.Create("newLevel.xml");
            myFile.Close();
            var file = new System.IO.StreamWriter("newLevel.xml");
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Rectangle>));
            _wallCoordinates.Add(new Rectangle(_pacManCoordinate.X, _pacManCoordinate.Y, _enemyCoordinate.X, _enemyCoordinate.Y));
            _wallCoordinates.Add(new Rectangle(_numberColumns, _numberLines, 0, 0));
            writer.Serialize(file, _wallCoordinates);
            file.Close();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            _enemyCoordinate = new Point(-50,-50);
            _pacManCoordinate = new Point();
            _wallCoordinates.Clear();
            Invalidate();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            _parentForm.Close();
        }

        private void newLevelDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parentForm.Close();
        }
    }
}
