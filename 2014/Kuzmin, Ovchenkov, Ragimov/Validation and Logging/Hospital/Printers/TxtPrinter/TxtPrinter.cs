using Shared;

namespace TxtPrinter
{
    public class TxtPrinter : Printer
    {
        public override string Extension
        {
            get { return ".txt"; }
        }
    }
}