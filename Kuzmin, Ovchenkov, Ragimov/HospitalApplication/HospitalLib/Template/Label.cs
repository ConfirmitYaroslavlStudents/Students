using System.Windows.Controls;
using HospitalLib.Interfaces;

namespace HospitalLib.Template
{
    public class Label : IElement
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Text { get; private set; }
        public string Type { get; private set; }

        public Label(string text, int height = 0, int width = 0)
        {
            Text = text;
            Height = height;
            Width = width;

            Type = "Label";
        }

        public Control GetControl()
        {
            var newElement = new System.Windows.Controls.Label
            {
                Content = Text,
                Width = Width,
                Height = Height,
            };

            return newElement;
        }
    }
}
