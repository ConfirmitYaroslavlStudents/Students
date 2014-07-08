using System;
using System.Drawing;

namespace HelpKsyu
{
    public struct RGBColor
    {
        public byte R, G, B;

        public RGBColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    };

    public struct Vertex2D
    {
        public int Index, X, Y;

        public Vertex2D(int index, int x, int y)
        {
            this.Index = index;
            this.X = x;
            this.Y = y;
        }
    };

    public class Vector2DInfo
    {
        internal struct DoublePoint
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        public Point Begin { get; private set; }
        public Point End { get; private set; }
        internal DoublePoint LeftArrowPoint { get; private set; }
        internal DoublePoint RightArrowPoint { get; private set; }

        public Vector2DInfo(Point begin, Point end)
        {
            Begin = begin;
            End = end;

            CalculateArrowPoints(begin, end);
        }

        private void CalculateArrowPoints(Point begin, Point end)
        {
            double ARROW_SIZE = 25;

            double deltaX = begin.X - end.X;
            double deltaY = begin.Y - end.Y;
            double length = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            deltaX /= length;
            deltaY /= length;

            double leftArrowPointX = 6.5 * deltaX + deltaY;
            double leftArrowPointY = 6.5 * deltaY - deltaX;
            double rightArrowPointX = 6.5 * deltaX - deltaY;
            double rightArrowPointY = 6.5 * deltaY + deltaX;

            length = Math.Sqrt(leftArrowPointX * leftArrowPointX + leftArrowPointY * leftArrowPointY);
            leftArrowPointX /= length;
            leftArrowPointY /= length;
            rightArrowPointX /= length;
            rightArrowPointY /= length;

            LeftArrowPoint = new DoublePoint() { X = leftArrowPointX * ARROW_SIZE, Y = leftArrowPointY * ARROW_SIZE };
            RightArrowPoint = new DoublePoint() { X = rightArrowPointX * ARROW_SIZE, Y = rightArrowPointY * ARROW_SIZE };
        }
    };
}