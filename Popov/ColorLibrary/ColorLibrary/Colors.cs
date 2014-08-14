using System;

namespace ColorLibrary
{
    public class Red : IColor
    {
        public string DoWith(Red red)
        {
            return red != null ? "Red + Red" : null;
        }

        public string DoWith(Green green)
        {
            return green != null ? "Red + Green" : null;
        }

        public string DoWith(IColor color)
        {
            return DoWith(color.ToGreen()) ?? DoWith(color.ToRed());
        }

        public Red ToRed()
        {
            return this;
        }

        public Green ToGreen()
        {
            return null;
        }
    }

    public class Green : IColor
    {
        public string DoWith(Red red)
        {
            return red != null ? "Green + Red" : null;
        }

        public string DoWith(Green green)
        {
            return green != null ? "Green + Green" : null;
        }

        public string DoWith(IColor color)
        {
            return DoWith(color.ToGreen()) ?? DoWith(color.ToRed());
        }

        public Red ToRed()
        {
            return null;
        }

        public Green ToGreen()
        {
            return this;
        }
    }
}
