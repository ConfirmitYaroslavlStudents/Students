using System;
using System.Windows.Media;

namespace ImpossibleCreatures
{
    public static class FillBrushes
    {
        private static readonly Random random = new Random();

        private static readonly Brush[] BackgroundShemes =
        {
            new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop {Color = Colors.Green, Offset = 0.25},
                    new GradientStop {Color = Colors.Gold, Offset = 0.33},
                    new GradientStop {Color = Colors.Blue, Offset = 0.66},
                    new GradientStop {Color = Colors.OrangeRed, Offset = 1}
                },
                StartPoint = new System.Windows.Point(0, 0),
                EndPoint = new System.Windows.Point(0, 1),
            },
            new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop {Color = Colors.Black, Offset = 0},
                    new GradientStop {Color = Colors.White, Offset = 1}
                },
                StartPoint = new System.Windows.Point(0, 0),
                EndPoint = new System.Windows.Point(0, 1)
            }
        };
    }

    public struct PairBrushNumber
    {
        public Brush BrushFill;
        public int NumberColor;

        public PairBrushNumber(Brush brush, int number)
        {
            BrushFill = brush;
            NumberColor = number;
        }
    }
}