using System;

namespace ColorLibrary
{
    public class Green : IColor
    {
        private readonly IProcessor _processor;

        public Green(IProcessor processor)
        {
            _processor = processor;
        }


        public void DoWithColor(Red red)
        {
            if (red != null)
            {
                _processor.Work(this, red);
            }
        }

        public void DoWithColor(Green green)
        {
            if (green != null)
            {
                _processor.Work(this, green);
            }
        }

        public void DoWith(IColor color)
        {
            DoWithColor(color.ToGreen());
            DoWithColor(color.ToRed());
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
