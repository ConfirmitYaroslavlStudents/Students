using System;
using System.Drawing;
using CellsAutomate.Creatures;

namespace CellsAutomate
{
    public enum ActionEnum
    {
        Die,
        MakeChild,
        Go,
        Eat
    }

    public enum DirectionEnum
    {
        Stay,
        Up,
        Right,
        Down,
        Left
    }
}