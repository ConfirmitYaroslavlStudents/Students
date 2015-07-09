using System.IO;
using System.Text;
using GemBox.Document;
using Shared.Interfaces;

namespace Shared
{
    public abstract class Printer : IPrinter
    {
        public abstract string Extension { get; }
        public string PathToFile { get; set; }

        public void Print(string filledHtmlTemplate)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(filledHtmlTemplate));
            DocumentModel.Load(ms, LoadOptions.HtmlDefault).Save(PathToFile + Extension);
        }
    }
}