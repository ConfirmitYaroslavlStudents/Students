namespace Colors
{
    public class Red : IColor
    {
        public void FirstProcess(IColor second)
        {
            second.SecondProcess(this, null);
        }

        public void SecondProcess(Red colorOne, Green green)
        {
            if (colorOne != null)
                ColorsProcessor.Process(colorOne, this);
            if (green != null)
                ColorsProcessor.Process(green, this);
        }
    }

    public class Green : IColor
    {
        public void FirstProcess(IColor second)
        {
            second.SecondProcess(null, this);
        }

        public void SecondProcess(Red colorOne, Green green)
        {
            if (colorOne != null)
                ColorsProcessor.Process(colorOne, this);
            if (green != null)
                ColorsProcessor.Process(green, this);
        }
    }
}
