namespace FizzBuzzLib
{
    public static class Int32Extension
    {
        public static bool IsDividableBy(this int number, int divider)
        {
            return number % divider == 0;
        }
    }
}
