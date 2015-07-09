namespace Colors.Helper
{
    public enum Colors
    {
        Red, Green
    }
    public class ColorKeeper<T>
    {
        public T Value { get; set; }
        public Colors Color { get; set; }
    }
}
