using System;
using Vehicles.Visitors;

namespace Vehicles
{
    public class Taxi : ICarVehicle
    {
        public int Capacity { get; set; }
        public int CurrentFuel { get; set; }
        public int FuelConsumption { get; set; }
        public int MaxFuel { get; }
        public int CurrentDistance { get; private set; }
        public int MaxDistance { get; }

        private IFortuneManager _fortuneManager;

        public Taxi(IFortuneManager fortuneManager)
        {
            _fortuneManager = fortuneManager;
            Capacity = 5;
            MaxFuel = 50;
            CurrentFuel = MaxFuel;
            CountOfWheels = 4;
            FuelConsumption = 10;
            CurrentDistance = 0;
            MaxDistance = 10000;
        }

        public void Accept(IVehicleVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int CountOfWheels { get; set; }

        public void Drive(int distance)
        {
            var currentFuelConsumption = distance / 100.0 * FuelConsumption;

            if (CurrentFuel >= currentFuelConsumption)
            {
                CurrentFuel -= (int)currentFuelConsumption;
            }
            else
            {
                throw new Exception("Please add fuel");
            }

            CurrentDistance += distance;
            CountOfWheels -= _fortuneManager.IsVehicleBroken(CurrentDistance, MaxDistance) ? 1 : 0;
        }

        public void Accept(IRoadVehicleVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool HasTruck { get; set; }
    }

    public interface IFortuneManager
    {
        bool IsVehicleBroken(int currentDistance, int maxDistance);
    }

    public class RealFortuneManager : IFortuneManager
    {
        private Random _generator = new Random();

        public bool IsVehicleBroken(int currentDistance, int maxDistance)
        {
            return _generator.Next(1, maxDistance) <= currentDistance;
        }
    }


}

