using System.IO;

namespace WebApp
{
    public class SaverAndLoader
    {
        private string _filename = "save.txt";

        public void Save(int length)
        {
            File.WriteAllText(_filename,length.ToString());
        }

        public int Load()
        {
            if (!File.Exists(_filename))
                return 0;

            return  int.Parse(File.ReadAllText(_filename));
        }

        public void DeleteFile()
        {
            if (File.Exists(_filename))
                File.Delete(_filename);
        }
    }
}