namespace Colors
{
    public class Green : IColor
    {
        public void DoFirst(IColor second)
        {
            second.DoSecond(null,this);
        }
        public void DoSecond(Red red, Green green)
        {
            if (red != null)
                ColorsOperator.Do(red, this);
            if (green != null)
                ColorsOperator.Do(green, this);
        }
    }
}
