using Shared;

namespace DocxPrinter
{
    public class DocxPrinter : Printer
    {
        public override string Extension
        {
            get { return ".docx"; }
        }
    }
}