namespace Championship
{
    public class StageIndicator
    {
        public int Stage;
        public int CountStage = 2;
        private const int BeginStage = 1;

        public StageIndicator(int beginStage)
        {
            Stage = beginStage;
        }

        public StageIndicator()
        {
            Stage = BeginStage;
        }

        public void NextStage()
        {
            if (Stage == BeginStage)
            {
                Stage = 2;
            }
            else
            {
                CountStage *= 2;
                Stage = CountStage;
            }
        }
    }
}
