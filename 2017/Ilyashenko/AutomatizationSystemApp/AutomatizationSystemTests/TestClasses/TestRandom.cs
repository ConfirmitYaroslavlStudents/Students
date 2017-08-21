using System;

namespace AutomatizationSystemTests
{
    public class TestRandom : Random
    {
        public TestRandom() { }

        public TestRandom(int Seed) : base(Seed) { }

        public override int Next(int maxValue)
        {
            return 0;
        }
    }
}
