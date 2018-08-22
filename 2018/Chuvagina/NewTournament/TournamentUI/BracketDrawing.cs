using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TournamentUI
{
    internal static class BracketDrawing
    {
        private const int _rowDistance= 30;
        private const int _columnDistance = 80;
        private const int _heightOfText = 20;
        private static SolidColorBrush _bracketColor = Brushes.RoyalBlue;

        public static void AddLinkToCanvas(ref int rowIndex, int colIndex, UiParticipant node, Canvas canvas)
        {
            if (node.Left != null)
                AddLinkToCanvas(ref rowIndex, colIndex - 1, node.Left, canvas);

            AddParticipantToCanvas(rowIndex, colIndex, node, canvas);
            rowIndex++;

            if (node.Right != null)
                AddLinkToCanvas(ref rowIndex, colIndex - 1, node.Right, canvas);
        }

        private static void AddParticipantToCanvas(int rowIndex, int colIndex, UiParticipant node, Canvas canvas)
        {
            node.AddAlignment(rowIndex, colIndex);

            var participant = new TextBlock()
            {
                Text= node.Name,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var border = new Border()
            {
                Width = 70,
                Height = 20,
                CornerRadius = new CornerRadius(10,10,10,10),            
            };
            border.Child = participant;

            if (node.Winner?.Name == node.Name)
            {
                border.Background = Brushes.ForestGreen;
                participant.FontWeight = FontWeights.Bold;
                participant.Foreground = Brushes.White;
            }

            Canvas.SetTop(border, rowIndex * _rowDistance);
            Canvas.SetLeft(border, _columnDistance * colIndex+5);
            canvas.Children.Add(border);

            if (node.Left != null)
            {
                var firstLine = new Line
                {
                    Y1 = node.HorizontalAlignment * _rowDistance,
                    X1 = node.VerticalAlignment * _columnDistance,
                    Y2 = node.Left.HorizontalAlignment * _rowDistance,
                    X2 = node.VerticalAlignment * _columnDistance,
                    Stroke = _bracketColor

                };

                var secondLine = new Line
                {
                    Y1 = node.Left.HorizontalAlignment * _rowDistance,
                    X1 = node.VerticalAlignment * _columnDistance,
                    Y2 = node.Left.HorizontalAlignment * _rowDistance,
                    X2 = node.Left.VerticalAlignment * _columnDistance,
                    Stroke = _bracketColor
                };

                canvas.Children.Add(firstLine);
                canvas.Children.Add(secondLine);
            }

            if (node.Winner != null && node.Winner.HorizontalAlignment != 0)
            {
                var firstLine = new Line
                {
                    Y1 = node.HorizontalAlignment * _rowDistance + _heightOfText,
                    X1 = node.VerticalAlignment * _columnDistance,
                    Y2 = node.HorizontalAlignment * _rowDistance + _heightOfText,
                    X2 = node.Winner.VerticalAlignment * _columnDistance,
                    Stroke = _bracketColor

                };

                var secondLine = new Line
                {
                    Y1 = node.HorizontalAlignment * _rowDistance + _heightOfText,
                    X1 = node.Winner.VerticalAlignment * _columnDistance,
                    Y2 = node.Winner.HorizontalAlignment * _rowDistance,
                    X2 = node.Winner.VerticalAlignment * _columnDistance,
                    Stroke = _bracketColor
                };

                canvas.Children.Add(firstLine);
                canvas.Children.Add(secondLine);
            }
        }
    }
}
