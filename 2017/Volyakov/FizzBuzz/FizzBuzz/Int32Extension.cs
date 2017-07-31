namespace FizzBuzzLibrary
{
    public static class Int32Extensions
    {
        public static bool IsDividableBy(this int number, int divider)
        {
            return number % divider == 0;
        }
    }
}
