namespace Colors
{
    public interface IColor
    {
        void DoFirst(IColor second);
        void DoSecond(Red first);
        void DoSecond(Green first);
    }

    public abstract class MasterColor : IColor
    {
        public abstract void DoFirst(IColor second);
        public void DoSecond(Red first)
        {
            if (GetRed() != null) { ColorsOperator.Do(first, GetRed()); }
            if (GetGreen() != null) { ColorsOperator.Do(first, GetGreen()); }
        }

        public void DoSecond(Green first)
        {
            if (GetRed() != null) { ColorsOperator.Do(first, GetRed()); }
            if (GetGreen() != null) { ColorsOperator.Do(first, GetGreen()); }
        }

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
