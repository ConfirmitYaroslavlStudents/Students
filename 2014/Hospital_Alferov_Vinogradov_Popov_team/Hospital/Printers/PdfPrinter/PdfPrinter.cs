using Shared;

namespace PdfPrinter
{
    public class PdfPrinter : Printer
    {
        public override string Extension
        {
            get { return ".pdf"; }
        }
    }
}