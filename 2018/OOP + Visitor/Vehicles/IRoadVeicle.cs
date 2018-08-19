using Vehicles.Visitors;

namespace Vehicles
{
    public interface IRoadVeicle : IVehicle
    {
        int CountOfWheels { get; set; }

        void Drive(int distance);

        void Accept(IRoadVehicleVisitor visitor);
    }
}

