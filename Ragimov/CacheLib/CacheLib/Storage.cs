namespace CacheLib
{
    public interface IStorage<in TKey, out TValue>
    {
        TValue Get (TKey key);
    }

    public class PiStorage:IStorage<int,string>
    {
        public string Get(int key)
        {
            return "Pi"; //Teoretically you can find anything you want in "Pi" number
        }
    }
}
