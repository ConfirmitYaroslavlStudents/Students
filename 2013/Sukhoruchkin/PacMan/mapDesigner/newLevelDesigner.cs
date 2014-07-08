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
using Level;

namespace mapDesigner
{
    public partial class NewLevelDesigner : Form
    {
        private int _numberLines;
        private int _numberColumns;
        private List<Rectangle> _allRectanglesInMap;
        private List<Rectangle> _wallCoordinates;
        private Point _pacManCoordinate;
        private List<Point> _enemyCoordinates;
        private Form _parentForm;
        private int _numerLevel;

        public NewLevelDesigner(int numberLines, int numberColumns, Form parentForm)
        {
            InitializeComponent();
            this._parentForm = parentForm;
            this.Width = numberColumns * MapDesignerSettings.CellSize + 18;
            this.Height = numberLines * MapDesignerSettings.CellSize + 90;

            wall.Top = this.Height - 80;
            pacMan.Top = this.Height - 80;
            enemy.Top = this.Height - 80;
            save.Top = this.Height - 60;
            clear.Top = this.Height - 60;
            exit.Top = this.Height - 60;

            this._numberColumns = numberColumns;
            this._numberLines = numberLines;
            this._numerLevel = 1;

            this._allRectanglesInMap = new List<Rectangle>();
            this._wallCoordinates = new List<Rectangle>();
            this._pacManCoordinate = new Point();
            this._enemyCoordinates = new List<Point>();
            for (int i = 0; i < _numberLines; i++)
            {
                for (int j = 0; j < _numberColumns; j++)
                {
                    _allRectanglesInMap.Add(new Rectangle(MapDesignerSettings.CellSize * j, MapDesignerSettings.CellSize * i, MapDesignerSettings.CellSize, MapDesignerSettings.CellSize));
                }
            }
        }
        private bool IsNotWall(Rectangle rect)
        {
            foreach (Rectangle rectangle in _wallCoordinates)
            {
                if (rectangle == rect)
                    return false;
            }
            return true;
        }
        private bool IsNotEnemy(Point pointCoordinate)
        {
            foreach (Point point in _enemyCoordinates)
            {
                if (point == pointCoordinate)
                    return false;
            }
            return true;
        }
        private void NewLevelDesigner_Paint(object sender, PaintEventArgs e)
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
            g.FillEllipse(Brushes.Yellow, _pacManCoordinate.X, _pacManCoordinate.Y, MapDesignerSettings.CellSize, MapDesignerSettings.CellSize);
            foreach (Point enemyCoordinate in _enemyCoordinates)
            {
                g.FillEllipse(Brushes.Indigo, enemyCoordinate.X, enemyCoordinate.Y, MapDesignerSettings.CellSize, MapDesignerSettings.CellSize);
            }
        }
        private void NewLevelDesigner_MouseClick(object sender, MouseEventArgs e)
        {
            if (wall.Checked)
                MouseClickWall(e);
            if (pacMan.Checked)
                MouseClickPacManOrEnemy(e, true);
            if (enemy.Checked)
                MouseClickPacManOrEnemy(e, false);
        }
        private void MouseClickWall(MouseEventArgs e)
        {
            foreach (Rectangle rect in _allRectanglesInMap)
            {
                if (rect.Contains(new Point(e.X, e.Y)))
                {
                    if (IsNotWall(rect))
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
        private void MouseClickPacManOrEnemy(MouseEventArgs e, bool ItIsPacMan)
        {
            foreach (Rectangle rect in _allRectanglesInMap)
            {
                if (rect.Contains(new Point(e.X, e.Y)))
                {
                    if (IsNotWall(rect) && ItIsPacMan)
                    {
                        _pacManCoordinate = new Point(rect.X, rect.Y);
                    }
                    if (IsNotWall(rect) && !ItIsPacMan)
                    {
                        Point point = new Point(rect.X, rect.Y); 
                        if (IsNotEnemy(point))
                            _enemyCoordinates.Add(point);
                        else
                            _enemyCoordinates.Remove(point);
                    }
                    break;
                }
            }
            Invalidate();
        }
        private void save_Click(object sender, EventArgs e)
        {
            Level.Level result = new Level.Level();
            result.wall = _wallCoordinates;
            result.enemys = _enemyCoordinates;
            result.pacMan = _pacManCoordinate;
            result.numberColumns = _numberColumns;
            result.numberLines = _numberLines;
            using (Stream fStream = new FileStream(_numerLevel.ToString() + ".xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(Level.Level));
                writer.Serialize(fStream, result);
            }
            _numerLevel++;
        }
        private void clear_Click(object sender, EventArgs e)
        {
            _enemyCoordinates = new List<Point>();
            _pacManCoordinate = new Point();
            _wallCoordinates.Clear();
            Invalidate();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            _parentForm.Close();
        }

        private void NewLevelDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parentForm.Close();
        }
    }
}
