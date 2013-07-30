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
    public partial class Lose : Form
    {
        private List<Record> records;
        private DoodleJump DoodlJumperLose;
        private int score;
        private int hight = 0;
        private int hightscore = 0;
        private Form menu;
        public Lose(int score, Form form)
        {
            this.score = score;
            this.menu = form;
            InitializeComponent();

            DoodlJumperLose = new DoodleJump(LoseDoodle.Location.X, LoseDoodle.Location.X + ApplicationSettings.DoodleLength, LoseDoodle.Location.Y + ApplicationSettings.DoodleHight, false, LoseDoodle, false);
         
            records = Higtscore();

            foreach (Record r in records)
            {
                if (r.score > hightscore)
                    hightscore = r.score;
            }


        }

        private List<Record> Higtscore()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Record>));
            System.IO.StreamReader file = new System.IO.StreamReader("Records.xml");
            records = (List<Record>)reader.Deserialize(file);
            return records;
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
            DoodlMove temp = new DoodlMove();
            temp.DoodlJumperMove(DoodlJumperLose, LoseDoodle, ref hight);

            DrawStrings(e);

            LoseDoodle.Refresh();

        }

        private void DrawStrings(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString("Game Over!", new Font("Segoe Script", 20), Brushes.Gold, new RectangleF(40, 20, 200, 50));
            g.DrawString("You score: " + (score).ToString(), new Font("Segoe Script", 20), Brushes.Indigo, new RectangleF(10, 60, 400, 100));
            g.DrawString("Hightscore: " + hightscore.ToString(), new Font("Segoe Script", 15), Brushes.Indigo, new RectangleF(10, 100, 400, 100));
            g.DrawString("You name: ", new Font("Segoe Script", 15), Brushes.Indigo, new RectangleF(10, 130, 400, 100));
        }

        private void PlayAgainButton_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
            GameMainWindow l = new GameMainWindow(menu);
            l.Show();
        }

        private void SaveRecord()
        {
            records.Add(new Record(NameBox.Text, score, DateTime.Now));
            if (!File.Exists("Records.xml"))
            {
                var myFile = File.Create("Records.xml");
                myFile.Close();
            }
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Record>));
            System.IO.StreamWriter file = new System.IO.StreamWriter("Records.xml");
            writer.Serialize(file, records);
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
            menu.Show();
        }
    }
}
