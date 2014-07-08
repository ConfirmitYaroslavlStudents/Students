using System;
using System.Drawing;

namespace GraphicalAdapter
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
}