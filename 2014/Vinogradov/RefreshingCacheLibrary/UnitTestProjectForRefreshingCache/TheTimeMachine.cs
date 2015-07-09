
using RefreshingCacheLibrary;

namespace UnitTestProjectForRefreshingCache
{
    public class TheTimeMachine:ITime
    {
        private int _now;
        public int CurrentTime 
        {
            get { return _now; } 
        }

        public int MaxTime
        {
            get { return int.MaxValue; }
        }

        public void ChangeTo(int value)
        {
            _now += value;
        }
    }
}
