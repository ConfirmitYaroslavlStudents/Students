using System.Collections.Generic;
using Vehicles;

namespace VehiclesTests
{
    internal class TestFortuneManager : IFortuneManager
    {
        private readonly List<bool> _values;
        private int _index;

        public TestFortuneManager(List<bool> values)
        {
            _index = 0;
            _values = values;
        }

        public bool IsVehicleBroken(int currentDistance, int maxDistance)
        {
            return _values[_index++];
        }
    }
}