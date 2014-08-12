namespace Colors
{
    public class Green : IColor
    {
        public void DoFirst(IColor second)
        {
            second.DoSecond(this);
        }

        public void DoSecond(Red first)
        {
            ColorsOperator.Do(first, this);
        }

        public void DoSecond(Green first)
        {
            ColorsOperator.Do(first, this);
        }

    }
}
