namespace ColorsLibrary
{
    public interface IColorable
    {
        void IdentifyItself(Visitor visitor);
    }

    public class RedColor : IColorable
    {
        public void IdentifyItself(Visitor visitor)
        {
            visitor.ChooseTypeOfColor(this);
        }
    }

    public class GreenColor : IColorable
    {
        public void IdentifyItself(Visitor visitor)
        {
            visitor.ChooseTypeOfColor(this);
        }
    }
}
