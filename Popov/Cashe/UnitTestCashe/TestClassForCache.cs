using System.Threading;
using Cache;

namespace UnitTestCashe
{
    class TestClassForCache : ICashe<int,string>
    {
        public string GetValue(int key)
        {
            
            return key + " HZ";
        }
    }
}