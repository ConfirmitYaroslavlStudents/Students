using System;

namespace AutoProcessor
{
    public class StartBatFileStep: Step
    {
        private string _filePath;

        public StartBatFileStep(string BatFilePath)
        {
            _filePath = BatFilePath;

            StepStatus = Status.NotStarted;
        }

        public override void Start()
        {
            StepStatus = Status.Launched;

            try
            {
                if (IsBatFile(_filePath))
                {
                    //Запустить .bat файл
                    StepStatus = Status.Finished;
                }
                else
                    StepStatus = Status.Error;
            }
            catch
            {
                StepStatus = Status.Error;
            }
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
