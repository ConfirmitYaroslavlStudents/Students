using System;

namespace AutoProcessor
{
    public class StartBatFileStep: IStep
    {
        private string _filePath;

        public StartBatFileStep(string BatFilePath)
        {
            _filePath = BatFilePath;
        }

        public void Start()
        {
            if (IsBatFile(_filePath))
            {
                //Запустить .bat файл   
            }
            else
                throw new ArgumentException("File doesn't have .bat extension");
        }

        private bool IsBatFile(string filePath)
        {
            if (filePath.Length < 5)
                throw new ArgumentException("Wrong path length to file");

            var expectedIndex = filePath.Length - 4;

            var actualIndex = filePath.LastIndexOf(".bat");

            return expectedIndex == actualIndex;
        }
    }
}
