using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TournamentUI
{
    internal static class BracketDrawing
    {
        private const int _rowDistance= 30;
        private const int _columnDistance = 80;
        private const int _heightOfText = 10;
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
            var fullNameToolTip = new ToolTip()
            {
                Content = node.Name
            };

            var participant = new TextBlock()
            {
                Text= node.Name,
                TextWrapping = TextWrapping.Wrap,
                TextTrimming = TextTrimming.CharacterEllipsis,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize=_heightOfText,
                ToolTip=fullNameToolTip
            };

            var border = new Border()
            {
                Width = _columnDistance,
                MinHeight = _rowDistance/2,
                MaxHeight = _rowDistance,
                CornerRadius = new CornerRadius(10,10,10,10),
                Background = Brushes.White,
                BorderBrush = _bracketColor,
                BorderThickness = new Thickness(1)
        };
            border.Child = participant;

            if (node.Winner?.Name == node.Name)
            {
                border.Background = Brushes.ForestGreen;
                participant.FontWeight = FontWeights.Bold;
                participant.Foreground = Brushes.White;
            }

            Canvas.SetTop(border, rowIndex * _rowDistance);
            Canvas.SetLeft(border, _columnDistance * colIndex);
            canvas.Children.Add(border);

            AddLine(node, canvas);
        }

        private static void AddLine(UiParticipant node, Canvas canvas)
        {
            if (node.Left != null)
            {
                var line = new Line
                {
                    Y1 = node.HorizontalAlignment * _rowDistance,
                    X1 = node.VerticalAlignment * _columnDistance,
                    Y2 = node.Left.HorizontalAlignment * _rowDistance + 10,
                    X2 = node.VerticalAlignment * _columnDistance,
                    Stroke = _bracketColor
                };

                canvas.Children.Add(line);
            }

            if (node.Winner != null && node.Winner.HorizontalAlignment != 0)
            {

                var line = new Line
                {
                    Y1 = node.HorizontalAlignment * _rowDistance + 10,
                    X1 = node.Winner.VerticalAlignment * _columnDistance,
                    Y2 = node.Winner.HorizontalAlignment * _rowDistance,
                    X2 = node.Winner.VerticalAlignment * _columnDistance,
                    Stroke = _bracketColor
                };

                canvas.Children.Add(line);
            }

        }
    }
}
