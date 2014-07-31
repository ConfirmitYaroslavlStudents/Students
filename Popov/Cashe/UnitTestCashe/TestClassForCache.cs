using Cache;

namespace UnitTestCashe
{
    class TestClassForCache : IGettingValue<int,string>
    {
        public string this[int key]
        {
           get { return key + " HZ"; }
        }
    }
}