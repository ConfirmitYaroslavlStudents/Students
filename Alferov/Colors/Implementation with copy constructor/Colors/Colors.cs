using System;

namespace Colors
{
    public class Red : IColored
    {
        public Color Color { get; private set; }
        public long Hash { get; private set; }

        public Red()
        {
            Color = Color.Red;
            Hash = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        internal Red(IColored red)
        {
            Hash = red.Hash;
            Color = red.Color;
        }
    }

    public class Blue : IColored
    {
        public Color Color { get; private set; }
        public long Hash { get; private set; }

        public Blue()
        {
            Color = Color.Blue;
            Hash = DateTime.Now.GetHashCode();
        }

        internal Blue(IColored blue)
        {
            Hash = blue.Hash;
            Color = blue.Color;
        }
    }
}
