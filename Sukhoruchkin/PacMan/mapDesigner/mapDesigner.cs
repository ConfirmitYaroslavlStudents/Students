using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mapDesigner
{
    public partial class MapDesigner : Form
    {
        public MapDesigner()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (IsNumber(numberOfLines.Text) && IsNumber(numberOfColumns.Text))
            {
                NewLevelDesigner levelDesigner = new NewLevelDesigner(int.Parse(numberOfLines.Text), int.Parse(numberOfColumns.Text), this);
                this.Visible = false;
                levelDesigner.Show();
            }
            else
            {
                MessageBox.Show("Invalid data format");
            }
        }
        private bool IsNumber(string numberString)
        {
            int result;
            int.TryParse(numberString, out result);
            return result > 0;
        }
    }
}
