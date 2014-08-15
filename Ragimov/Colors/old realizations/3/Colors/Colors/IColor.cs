namespace Colors
{
    public interface IColor
    {
        Red GetRed();
        Green GetGreen();
    }
    public abstract class MasterColor : IColor
    {
        public virtual Red GetRed()
        {
            return null;
        }
        public virtual Green GetGreen()
        {
            return null;
        }
    }
}
