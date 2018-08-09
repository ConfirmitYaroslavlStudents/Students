using System;

namespace Tournament
{
    [Serializable]
    public abstract class Grid
    {
        public Match[][] Matches;
        public string Winner;
    }
}
