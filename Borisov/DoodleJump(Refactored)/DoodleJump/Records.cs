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
        private DoodleJump DoodlJumperRecords;
        private int hight = 0;
        private Form menu;
        public Records(Form form)
        {
            this.menu = form;
            InitializeComponent();
            DoodlJumperRecords = new DoodleJump(DoodleRecords.Location.X, DoodleRecords.Location.X + 26, DoodleRecords.Location.Y + 36, false, DoodleRecords, false);

            records = Higtscore();

            records.Sort(Record.RecordCompare);

            if (records.Count <= 10)
            {
                for (int i = records.Count - 1; i >= 0; i--)
                {
                    RecordsBox.Items.Add(((records.Count - i).ToString() + " " + records.ElementAt(i).score + " " + records.ElementAt(i).name + " " + records.ElementAt(i).now.ToShortDateString()));
                }
            }
            else
            {
                for (int i = records.Count - 1; i >= records.Count - 10; i--)
                {
                    RecordsBox.Items.Add(((records.Count - i).ToString() + " " + records.ElementAt(i).score + " " + records.ElementAt(i).name + " " + records.ElementAt(i).now.ToShortDateString()));
                }
            }


        }

        private List<Record> Higtscore()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Record>));
            System.IO.StreamReader file = new System.IO.StreamReader("Records.xml");
            records = (List<Record>)reader.Deserialize(file);
            return records;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DoodlMove temp = new DoodlMove();
            temp.DoodlJumperMove(DoodlJumperRecords, DoodleRecords, ref hight);

            Graphics g = e.Graphics;
            g.DrawString("Records:", new Font("Segoe Script", 20), Brushes.Magenta, new RectangleF(20, 20, 200, 50));

            DoodleRecords.Refresh();

        }

        private void Records_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
            menu.Show();
        }
    }
}
