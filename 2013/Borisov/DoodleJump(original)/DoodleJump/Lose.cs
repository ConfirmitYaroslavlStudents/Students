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
        private DoodleJump DoodlJumper2;
        private int score;
        private int hight = 0;
        private int hightscore=0;
        private Form menu;
        public Lose(int s,Form f)
        {
            this.score = s;
            this.menu = f;
            InitializeComponent();

            DoodlJumper2 = new DoodleJump(Doodle2.Location.X, Doodle2.Location.X + 26, Doodle2.Location.Y + 36, false, Doodle2, false);
            Step step1 = new Step(label5.Location.X, label5.Location.X + 45, label5.Location.Y, label5, true, false);

            records = Higtscore();
            foreach (Record r in records)
            {
                if (r.score > hightscore)
                    hightscore = r.score;
            }

            
        }

        private void Lose_Paint(object sender, PaintEventArgs e)
        {
                
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
            ListTetradi l = new ListTetradi(menu);
            l.Show();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (DoodlJumper2.FlyFaza == false)
            {
                Doodle2.Top += 2;
                System.Threading.Thread.Sleep(4);
            }
            if ((Doodle2.Top + 36 == 294) && (DoodlJumper2.FlyFaza == false))
            {
                DoodlJumper2.FlyFaza = true;
            }
            if (DoodlJumper2.FlyFaza == true)
            {
                Doodle2.Top -= 2;
                System.Threading.Thread.Sleep(4);
                hight++;
            }
            if (hight == 45)
            {
                DoodlJumper2.FlyFaza = false;
                hight = 0;
            }
            Graphics g = e.Graphics;
            g.DrawString("Game Over!", new Font("Segoe Script", 20), Brushes.Gold, new RectangleF(40, 20, 200, 50));
            g.DrawString("You score: "+(score).ToString(), new Font("Segoe Script", 20), Brushes.Indigo, new RectangleF(10, 60, 400, 100));
            g.DrawString("Hightscore: "+hightscore.ToString(), new Font("Segoe Script", 15), Brushes.Indigo, new RectangleF(10, 100, 400, 100));
            g.DrawString("You name: ", new Font("Segoe Script", 15), Brushes.Indigo, new RectangleF(10, 130, 400, 100));
            Doodle2.Refresh();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
            menu.Show();
        }

        private void Lose_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = true;
            this.Refresh();
        }
        private void SaveRecord()
        {
            records.Add(new Record(textBox1.Text,score, DateTime.Now));
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
        private List<Record> Higtscore()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Record>));
            System.IO.StreamReader file = new System.IO.StreamReader("Records.xml");
            records = (List<Record>)reader.Deserialize(file);
            return records;
        }
    }
}
