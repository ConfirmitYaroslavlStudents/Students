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


        public void DoWith(Red red)
        {
            if (red != null)
            {
                _processor.Work(this, red);
            }
        }

        public void DoWith(Green green)
        {
            if (green != null)
            {
                _processor.Work(this, green);
            }
        }

        public void DoWith(IColor color)
        {
            if (color.ToGreen() != null)
            {
                DoWith(color.ToGreen());
            }
            if (color.ToRed() != null)
            {
                DoWith(color.ToRed());
            }
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
