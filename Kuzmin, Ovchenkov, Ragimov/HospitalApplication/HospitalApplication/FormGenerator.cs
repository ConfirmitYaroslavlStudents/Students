using System.Windows;
using System.Windows.Controls;
using HospitalLib.Template;

namespace HospitalApplication
{
    public class FormGenerator
    {
        public static Template Template;
        public int FormHeight { get; private set; }
        public double FormWidth { get; private set; }
        public void GenerateForm(Grid grid, Window window)
        {
            FormWidth = 0.0;
            var currentLine = 0;

            foreach (var line in Template.Lines)
            {
                var lineWidth = 0;
                var lineHeight = GetLineHeight(line);
                var stackLine = CreateRow(grid, lineHeight);

                FormHeight += lineHeight;

                foreach (var element in line)
                {
                    lineWidth += element.Width;
                    
                    var control = element.GetControl();
                    stackLine.Children.Add(control);
                }
                if (lineWidth > FormWidth)
                    FormWidth = lineWidth;

                Grid.SetRow(stackLine, currentLine++);
            }

            SetWindowSize(grid, window);
        }

        private static StackPanel CreateRow(Grid grid, int lineHeight)
        {
            var row = new RowDefinition {Height = new GridLength(lineHeight)};
            grid.RowDefinitions.Add(row);

            var stack = new StackPanel {Orientation = Orientation.Horizontal};
            grid.Children.Add(stack);

            return stack;
        }

        private static int GetLineHeight(Line line)
        {
            return line.Height == 0 ? 50 : line.Height;
        }

        private void SetWindowSize(Grid grid, Window window)
        {
            grid.Height = FormHeight;
            grid.Width = FormWidth + 20;
            window.Width = FormWidth + 20;
            window.Height = FormHeight + 200;
        }
    }
}
