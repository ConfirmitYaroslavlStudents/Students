using System;
using Vehicles;
using Vehicles.Visitors;

namespace ConsoleApp
{
    public class VehiclePrinter : IVehicleVisitor
    {
        public void Visit(IFlyingVehicle vehicle)
        {
            Console.WriteLine($"Count Of Wings: {vehicle.CountOfWings}");
        }

        public void Visit(IRoadVeicle vehicle)
        {
            Console.WriteLine($"Count Of Wheels: {vehicle.CountOfWheels}");
        }
    }
}