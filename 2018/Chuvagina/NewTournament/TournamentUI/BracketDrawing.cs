using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TournamentUI
{
    internal static class BracketDrawing
    {
        public static void AddParticipantToCanvas(int rowIndex, int colIndex, UiParticipant node, Canvas canvas)
        {
            node.AddAlignment(rowIndex, colIndex);
            var participant = new TextBlock(new Run(node.Name));

            if (node.Winner?.Name == node.Name)
            {
                participant.Foreground = Brushes.Green;
                participant.FontWeight = FontWeights.Bold;
            }

            Canvas.SetTop(participant, rowIndex * 30);           
            Canvas.SetLeft(participant, 50 * colIndex);   
            canvas.Children.Add(participant);

            if (node.Left != null)
            {
                var firstLine = new Line
                {
                    Y1 = node.HorizontalAlignment * 30,
                    X1 = node.VerticalAlignment * 50,
                    Y2 = node.Left.HorizontalAlignment * 30,
                    X2 = node.VerticalAlignment * 50,
                    Stroke = Brushes.LightBlue

                };

                var secondLine = new Line
                {
                    Y1 = node.Left.HorizontalAlignment * 30,
                    X1 = node.VerticalAlignment * 50,
                    Y2 = node.Left.HorizontalAlignment * 30,
                    X2 = node.Left.VerticalAlignment * 50,
                    Stroke = Brushes.LightBlue
                };

                canvas.Children.Add(firstLine);
                canvas.Children.Add(secondLine);
            }

            if (node.Winner != null && node.Winner.HorizontalAlignment != 0)
            {
                var firstLine = new Line
                {
                    Y1 = node.HorizontalAlignment * 30 + 20,
                    X1 = node.VerticalAlignment * 50,
                    Y2 = node.HorizontalAlignment * 30 + 20,
                    X2 = node.Winner.VerticalAlignment * 50,
                    Stroke = Brushes.LightBlue

                };

                var secondLine = new Line
                {
                    Y1 = node.HorizontalAlignment * 30 + 20,
                    X1 = node.Winner.VerticalAlignment * 50,
                    Y2 = node.Winner.HorizontalAlignment * 30,
                    X2 = node.Winner.VerticalAlignment * 50,
                    Stroke = Brushes.LightBlue
                };

                canvas.Children.Add(firstLine);
                canvas.Children.Add(secondLine);
            }
        }
    }
}
