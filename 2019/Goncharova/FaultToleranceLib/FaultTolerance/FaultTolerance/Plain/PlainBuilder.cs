using FaultTolerance.Plain;

namespace FaultTolerance
{
    public partial class Strategy
    {
        public static PlainStrategy Plain() => new PlainStrategy();
    }
}
