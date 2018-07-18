using System;

namespace Championship
{
    class StageIndicator
    {
        public string Stage;
        public int CountStage = 2;

        public StageIndicator(string beginStage)
        {
            Stage = beginStage;
        }

        public StageIndicator()
        {
            Stage = "final";
        }

        public void NextStage()
        {
            if (Stage == "final")
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
