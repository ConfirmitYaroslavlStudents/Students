namespace Colors
{
    public class ColorMixer
    {
        const int Colors = 2;
        private readonly Red[] _reds = new Red[Colors];
        private readonly Green[] _greens = new Green[Colors];

        private string _types = "";

        public void Add(Red color, int counter)
        {
            _reds[counter] = color;
            _types += "Red";
        }
        public void Add(Green color, int counter)
        {
            _greens[counter] = color;
            _types += "Green";
        }

        public void Do()
        {
            switch (_types)
            {
                case "RedRed":
                    ColorsOperator.Do(_reds[0],_reds[1]);
                    break;;
                case "RedGreen":
                    ColorsOperator.Do(_reds[0], _greens[1]);
                    break; ;
                case "GreenRed":
                    ColorsOperator.Do(_greens[0], _reds[1]);
                    break; ;
                case "GreenGreen":
                    ColorsOperator.Do(_greens[0], _greens[1]);
                    break; ;
            }
        }
    }
}
