namespace Colors
{
    public class ColorsOperator
    {
        //just for tests because our methods do nothing
        public static bool RedRed = false;
        public static bool GreenGreen = false;
        public static bool RedGreen = false;
        public static bool GreenRed = false;
        //

        public static void Do(IColor first, IColor second)
        {
            first.DoFirst(second);
        }
        public static void Do(Red first, Red second)
        {
            //Red, Red
            RedRed = true; //just for tests
        }
        public static void Do(Green first, Green second)
        {
            //Green, Green
            GreenGreen = true; //just for tests
        }

        public static void Do(Red first, Green second)
        {
            //Red, Green
            RedGreen = true; //just for tests
        }

        public static void Do(Green first, Red second)
        {
            //Green, Green
            GreenRed = true; //just for tests
        }
    }
}
