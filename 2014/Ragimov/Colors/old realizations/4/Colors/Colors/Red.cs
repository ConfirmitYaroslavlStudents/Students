namespace Colors
{
    public class Red : IColor
    {
        public void DoFirst(IColor second)
        {
            second.DoSecond(this,null);
        }

        public void DoSecond(Red red, Green green)
        {
            if(red != null)
            ColorsOperator.Do(red, this);
            if(green != null)
            ColorsOperator.Do(green, this);
        }

    }
}
