using Colors.Helper;

namespace Colors
{
    public class Red : IColor
    {
        public Red(){}
        public Red(IColor color){}

        public void Accept(ProcessHelper helper)
        {
            helper.SetColor(this);
        }
    }

    public class Green : IColor
    {
        public Green() { }
        public Green(IColor color){}

        public void Accept(ProcessHelper helper)
        {
            helper.SetColor(this);
        }
    }
}
