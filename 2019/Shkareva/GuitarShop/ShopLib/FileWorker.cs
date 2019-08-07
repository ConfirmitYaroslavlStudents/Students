using System.IO;

namespace ShopLib
{
    public class FileWorker
    {
        public string[] ReadFile(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}
