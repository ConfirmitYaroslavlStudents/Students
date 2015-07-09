using Shared;

namespace TiffPrinter
{
    public class TiffPrinter : Printer
    {
        public override string Extension
        {
            get { return ".tiff"; }
        }
    }
}