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
    public partial class Records : BaseForm
    {
        private List<Record> _records;
        private Form _menu;
        public Records(Form form)
        {
            this._menu = form;
            InitializeComponent();
            RecordsCreation();
        }

        private void RecordsCreation()
        {

            _records = GetHigtscore();

            _records.Sort(Record.RecordCompare);

            if (_records.Count <= 10)
            {
                for (int i = _records.Count - 1; i >= 0; i--)
                {
                    RecordsBox.Items.Add(((_records.Count - i).ToString() + " " + _records.ElementAt(i).score + " " + _records.ElementAt(i).name + " " + _records.ElementAt(i).now.ToShortDateString()));
                }
            }
            else
            {
                for (int i = _records.Count - 1; i >= _records.Count - 10; i--)
                {
                    RecordsBox.Items.Add(((_records.Count - i).ToString() + " " + _records.ElementAt(i).score + " " + _records.ElementAt(i).name + " " + _records.ElementAt(i).now.ToShortDateString()));
                }
            }
        }

        private List<Record> GetHigtscore()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Record>));
            System.IO.StreamReader file = new System.IO.StreamReader("Records.xml");
            _records = (List<Record>)reader.Deserialize(file);
            return _records;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.DrawString("Records:", new Font("Segoe Script", 20), Brushes.Magenta, new RectangleF(20, 20, 200, 50));

            base.DoodleBase.Refresh();

        }

        private void Records_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
            _menu.Show();
        }
    }
}
