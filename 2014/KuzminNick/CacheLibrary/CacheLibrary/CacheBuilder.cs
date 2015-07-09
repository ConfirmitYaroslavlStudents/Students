namespace CacheLibrary
{
    public class CacheBuilder<T>
    {
        public Cache<T> Build(IDataBase<T> slowDataBase)
        {
            return new Cache<T>(10, 5, slowDataBase);
        }
        
        public Cache<T> Build(int capacity, IDataBase<T> slowDataBase)
        {
            return new Cache<T>(capacity, 5, slowDataBase);
        }
        
        public Cache<T> Build(int capacity, int timeOfExpirationInTicks, IDataBase<T> slowDataBase)
        {
            return new Cache<T>(capacity, timeOfExpirationInTicks, slowDataBase);
        }
    }
}
