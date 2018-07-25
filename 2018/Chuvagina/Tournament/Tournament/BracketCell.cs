using System;

namespace Tournament
{
    internal class BracketCell
    {
        public string Text;
        public ConsoleColor Color;

        public BracketCell()
        {
            Text = "";
            Color = ConsoleColor.White;
        }

        public BracketCell(string text, ConsoleColor color)
        {
            Text = text;
            Color = color;
        }

        public BracketCell(string text)
        {
            Text = text;
            Color = ConsoleColor.White;
        }
    }
}