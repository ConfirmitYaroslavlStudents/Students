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
        string errorMsg = string.Empty;

        public MapDesigner()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (IsAllTextBoxesAreNotEmpty())
            {
                if (IsNotError())
                {
                    NewLevelDesigner levelDesigner = new NewLevelDesigner(int.Parse(numberOfLines.Text), int.Parse(numberOfColumns.Text), this);
                    this.Visible = false;
                    levelDesigner.Show();
                }
            }
            errorProvider.SetError(OK, errorMsg);
        }
        private bool IsNumber(string numberString)
        {
            int result;
            int.TryParse(numberString, out result);
            return result > 0;
        }
        public bool IsAllTextBoxesAreNotEmpty()
        {
            foreach (TextBox textBox in Controls.OfType<TextBox>())
            {
                if (textBox.Text == string.Empty)
                {
                    errorMsg = "Все поля должны быть заполнены";
                    return false;
                }
            }
            return true;
        }
        public bool IsNotError()
        {
            foreach (TextBox textBox in Controls.OfType<TextBox>())
            {
                if (errorProvider.GetError(textBox) != string.Empty)
                {
                    errorMsg = "Исправьте ошибки";
                    return false;
                }
            }
            return true;
        }
        private void numberOfColumns_Validating(object sender, CancelEventArgs e)
        {
            if (!IsNumber(numberOfColumns.Text))
            {
                e.Cancel = false;
                numberOfColumns.Select(0, numberOfColumns.Text.Length);
                errorMsg = "Число колонок должно быть целым числом больше нуля";
            }
            else
                errorMsg = string.Empty;
        }
        private void numberOfColumns_Validated(object sender, EventArgs e)
        {
            if (errorMsg == string.Empty)
                this.errorProvider.SetError(numberOfColumns, string.Empty);
            else
                this.errorProvider.SetError(numberOfColumns, errorMsg);
        }

        private void numberOfLines_Validating(object sender, CancelEventArgs e)
        {
            if (!IsNumber(numberOfLines.Text))
            {
                e.Cancel = false;
                numberOfLines.Select(0, numberOfLines.Text.Length);
                errorMsg = "Число строк должно быть целым числом больше нуля";
            }
            else
                errorMsg = string.Empty;
        }

        private void numberOfLines_Validated(object sender, EventArgs e)
        {
            if (errorMsg == string.Empty)
                this.errorProvider.SetError(numberOfLines, string.Empty);
            else
                this.errorProvider.SetError(numberOfLines, errorMsg);
        }
    }
}
