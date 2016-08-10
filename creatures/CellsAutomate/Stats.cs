using System;

namespace CellsAutomate
{
    public static class Stats
    {
        public static int Up;
        public static int Right;
        public static int Down;
        public static int Left;

        public static void AddStats(DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.Up:
                    Up++;
                    break;
                case DirectionEnum.Right:
                    Right++;
                    break;
                case DirectionEnum.Down:
                    Down++;
                    break;
                case DirectionEnum.Left:
                    Left++;
                    break;
                case DirectionEnum.Stay:
                    break;
                default: throw new Exception();
            }
        }
    }
}
