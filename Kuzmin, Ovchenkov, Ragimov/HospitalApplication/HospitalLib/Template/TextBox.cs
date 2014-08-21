using System.Windows.Controls;
using HospitalLib.Interfaces;

namespace HospitalLib.Template
{
    public class TextBox : IElement
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Type { get; private set; }
        public string Text {get { return _control.Text; }}

        private readonly System.Windows.Controls.TextBox _control;

        public TextBox(string text, int height = 0, int width = 0)
        {
            _control = new System.Windows.Controls.TextBox { Text =  text };
            Height = height;
            Width = width;
            Type = "TextBox";
        }

        public Control GetControl()
        {
           _control.Width = Width;
           _control.Height = Height;

            return _control;
        }
    }
}
