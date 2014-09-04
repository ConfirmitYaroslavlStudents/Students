using Shared;

namespace PngPrinter
{
    public class PngPrinter : Printer
    {
        public override string Extension
        {
            get { return ".png"; }
        }
    }
}