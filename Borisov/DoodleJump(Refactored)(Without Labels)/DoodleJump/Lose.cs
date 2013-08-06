using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DoodleJump
{
    public partial class Lose : BaseForm
    {
        private List<Record> _records;
        private int _score;
        private int _hightscore = 0;
        private Form _menu;
        public Lose(int score, Form form)
        {
            this._score = score;
            this._menu = form;
            InitializeComponent();
            _records = GetHigtscore();

        }

        private List<Record> GetHigtscore()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Record>));
            System.IO.StreamReader file = new System.IO.StreamReader("Records.xml");
            _records = (List<Record>)reader.Deserialize(file);
            foreach (Record r in _records)
            {
                if (r.score > _hightscore)
                    _hightscore = r.score;
            }
            return _records;
        }

        private void Lose_Paint(object sender, PaintEventArgs e)
        {
            Invalidate();
        }

        private void Lose_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           
            DrawStrings(e);


        }

        private void DrawStrings(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString("Game Over!", new Font("Segoe Script", 20), Brushes.Gold, new RectangleF(40, 20, 200, 50));
            g.DrawString("You score: " + (_score).ToString(), new Font("Segoe Script", 20), Brushes.Indigo, new RectangleF(10, 60, 400, 100));
            g.DrawString("Hightscore: " + _hightscore.ToString(), new Font("Segoe Script", 15), Brushes.Indigo, new RectangleF(10, 100, 400, 100));
            g.DrawString("You name: ", new Font("Segoe Script", 15), Brushes.Indigo, new RectangleF(10, 130, 400, 100));
        }

        private void PlayAgainButton_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
            GameMainWindow l = new GameMainWindow(_menu);
            l.Show();
        }

        private void SaveRecord()
        {
            _records.Add(new Record(NameBox.Text, _score, DateTime.Now));
            if (!File.Exists("Records.xml"))
            {
                var myFile = File.Create("Records.xml");
                myFile.Close();
            }
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Record>));
            System.IO.StreamWriter file = new System.IO.StreamWriter("Records.xml");
            writer.Serialize(file, _records);
            file.Close();
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {
            NameLabel.Visible = false;
            NameBox.Visible = true;
            this.Refresh();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
            _menu.Show();
        }
    }
}
