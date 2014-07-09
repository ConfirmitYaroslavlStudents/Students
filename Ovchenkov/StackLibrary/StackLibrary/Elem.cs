
namespace StackLibrary
{
    internal class Elem<T>
    {
        private T _data;
        private Elem<T> _next;

        public object Value
        {
            get { return _data; }
            set { _data = (T)value; }
        }

        public Elem<T> Next
        {
            get { return _next; }
            set { _next = value; }
        }
    }
}
