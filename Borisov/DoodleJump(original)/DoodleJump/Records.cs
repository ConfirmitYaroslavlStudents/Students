using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoodleJump
{
    public partial class Records : Form
    {
        private List<Record> records;
        private DoodleJump DoodlJumper2;
        private int hight = 0;
        private Form menu;
        public Records(Form f)
        {
            this.menu = f;
            InitializeComponent();

            DoodlJumper2 = new DoodleJump(Doodle2.Location.X, Doodle2.Location.X + 26, Doodle2.Location.Y + 36, false, Doodle2, false);
            Step step1 = new Step(label5.Location.X, label5.Location.X + 45, label5.Location.Y, label5, true, false);
            records = Higtscore();
            records.Sort(Record.RecordCompare);
            if (records.Count <= 10)
            {
                for (int i = records.Count - 1; i >= 0; i--)
                {
                    listBox1.Items.Add(((records.Count - i).ToString() + " " + records.ElementAt(i).score + " " + records.ElementAt(i).name + " " + records.ElementAt(i).dt.ToShortDateString()));
                }
            }
            else 
            {
                for (int i = records.Count - 1; i >= records.Count - 10; i--)
                {
                    listBox1.Items.Add(((records.Count - i).ToString() + " " + records.ElementAt(i).score + " " + records.ElementAt(i).name + " " + records.ElementAt(i).dt.ToShortDateString()));
                }
            }

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
            g.DrawString("Records:", new Font("Segoe Script", 20), Brushes.Magenta, new RectangleF(20, 20, 200, 50));

            Doodle2.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            menu.Show();
        }

        private void Records_Activated(object sender, EventArgs e)
        {
            this.Refresh();
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
