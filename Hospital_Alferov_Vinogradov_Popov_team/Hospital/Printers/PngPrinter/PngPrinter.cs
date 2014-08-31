using System.IO;
using System.Text;
using GemBox.Document;
using Shared.Interfaces;

namespace PngPrinter
{
    public class PngPrinter : IPrinter
    {
        public string PathToFile { get; set; }

        public void Print(string filledHtmlTemplate)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(filledHtmlTemplate));
            DocumentModel.Load(ms, LoadOptions.HtmlDefault).Save(PathToFile + ".png");
        }
    }
}