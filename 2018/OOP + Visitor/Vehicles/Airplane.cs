using System;
using Vehicles.Visitors;

namespace Vehicles
{
    public class Airplane : IFlyingVehicle
    {
        public int Capacity { get; set; }
        public int FuelConsumption { get; set; }
        public int CurrentFuel { get; set; }
        public int CountOfWings { get; set; }
        public int MaxFuel { get; }
        public int CurrentDistance { get; }
        public int MaxDistance { get; }
        public int TotalDistance { get; }

        public Airplane()
        {
            Capacity = 300;
            MaxFuel = 50000;
            CurrentFuel = MaxFuel;
            CountOfWings = 2;
        }

        public void Fly()
        {
            if (CurrentFuel > 99)
            {
                CurrentFuel -= 100;
            }
            else
            {
                throw new Exception("Please add fuel");
            }
        }


        public void Accept(IVehicleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

