using System;

namespace AutoProcessor
{
    public class StartSMDStep:Step
    {
        private string _filePath;

        public StartSMDStep(string SMDFilePath)
        {
            _filePath = SMDFilePath;
        }

        public override bool Start()
        {
            if( IsSMD(_filePath) )
            {
                //Работа с файлами.
                return true;
            }
            return false;
        }

        private bool IsSMD(string filePath)
        {
            if (filePath.Length < 5)
                throw new ArgumentException("Wrong path length to file");

            var expectedIndex = filePath.Length - 4;

            var actualIndex = filePath.LastIndexOf(".smd");

            return expectedIndex == actualIndex;
        }
    }
}
