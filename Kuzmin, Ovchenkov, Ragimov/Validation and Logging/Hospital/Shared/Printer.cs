using System.IO;
using System.Text;
using GemBox.Document;
using LogService;

namespace Shared
{
    public abstract class Printer
    {
        public abstract string Extension { get; }
        public string PathToFile { get; set; }

        public void Print(string filledHtmlTemplate)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(filledHtmlTemplate));
            DocumentModel.Load(ms, LoadOptions.HtmlDefault).Save(PathToFile + Extension);
            //LOGGING
            Logger.Info("Template was printed");
        }
    }
}