namespace Championship
{
    public class Meeting
    {
        public string FirstPlayer;
        public string SecondPlayer;
        public int[] Score;
        public Meeting NextStage;

        public Meeting()
        {
            Score = new int[2];
        }
    }
}
