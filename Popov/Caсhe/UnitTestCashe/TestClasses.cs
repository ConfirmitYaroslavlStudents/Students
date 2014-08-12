using System;
using Cache;

namespace UnitTestCashe
{
    class TestClasses : IGettingValue<int,string>
    {
        public string this[int key]
        {
           get { return key.ToString(); }
        }
    }

    class TimeForCache : ITime<string>
    {
        public TimeSpan TimeLive
        {
            get { return new TimeSpan(0,0,5);}
        }

        public DateTime CurrenTime
        {
            get { return DateTime.Now; }
        }
      
        public bool IsOldElement(Element<string> item)
        {
            return CurrenTime - item.TimeUsage > TimeLive;
        }
    }

    class TimeForCacheOldElement : ITime<string>
    {
        public TimeSpan TimeLive
        {
            get { return new TimeSpan(0, 0, 5); }
        }

        public DateTime CurrenTime
        {
            get { return DateTime.Now; }
        }

        public bool IsOldElement(Element<string> item)
        {
            return true;
        }
    }

    class TimeForCacheOverflowElement : ITime<string>
    {
        public TimeSpan TimeLive
        {
            get { return TimeSpan.MaxValue; }
        }

        public DateTime CurrenTime
        {
            get { return DateTime.Now; }
        }

        public bool IsOldElement(Element<string> item)
        {
            return false;
        }
    }
}