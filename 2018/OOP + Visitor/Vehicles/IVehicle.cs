using Vehicles.Visitors;

namespace Vehicles
{
    public interface IVehicle
    {
        int Capacity { get; set; }

        int FuelConsumption { get; set; }

        int CurrentFuel { get; set; }

        int MaxFuel { get; }

        int CurrentDistance { get; }

        int MaxDistance { get; }

        void Accept(IVehicleVisitor visitor);
    }
}

