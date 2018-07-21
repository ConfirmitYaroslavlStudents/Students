namespace Championship
{
    class StageIndicator
    {
        public string Stage;
        public int CountStage = 2;
        private const string ConstFinal = "final";

        public StageIndicator(string beginStage)
        {
            Stage = beginStage;
        }

        public StageIndicator()
        {
            Stage = ConstFinal;
        }

        public void NextStage()
        {
            if (Stage == ConstFinal)
            {
                Stage = "1/2";
            }
            else
            {
                CountStage *= 2;
                Stage = "1/" + CountStage;
            }
        }
    }
}
