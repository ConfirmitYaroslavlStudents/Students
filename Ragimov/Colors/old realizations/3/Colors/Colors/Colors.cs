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

            Do(first.GetRed(), second.GetRed());
            Do(first.GetRed(), second.GetGreen());
            Do(first.GetGreen(), second.GetRed());
            Do(first.GetGreen(), second.GetGreen());
        }

        public static bool HasNull(IColor first, IColor second)
        {
            return first == null || second == null;
        }

        public static void Do(Red first, Red second)
        {
            if (HasNull(first, second)) return;
            //Red, Red
            RedRed = true; //just for tests
        }
        public static void Do(Green first, Green second)
        {
            if (HasNull(first, second)) return;
            //Green, Green
            GreenGreen = true; //just for tests
        }

        public static void Do(Red first, Green second)
        {
            if (HasNull(first, second)) return;
            //Red, Green
            RedGreen = true; //just for tests
        }

        public static void Do(Green first, Red second)
        {
            if (HasNull(first, second)) return;
            //Green, Green
            GreenRed = true; //just for tests
        }
    }
}
