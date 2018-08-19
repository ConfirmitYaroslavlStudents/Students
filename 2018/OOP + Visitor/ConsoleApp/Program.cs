using System;
using System.Collections.Generic;
using Vehicles;
using Vehicles.Visitors;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IVehicle taxi = new Taxi(new RealFortuneManager());

            var vehicles = new List<IVehicle>
            {
                taxi,
            };

            (taxi as IRoadVeicle).CountOfWheels -= 4;

            PrintVehicles(vehicles);

            ApplyVisitors(vehicles);

            PrintVehicles(vehicles);

            (taxi as ICarVehicle).HasTruck = true;

            ApplyVisitors(vehicles);

            PrintVehicles(vehicles);
        }

        private static void ApplyVisitors(List<IVehicle> vehicles)
        {
            var service = new ServiceStation();

            var visitors = new List<IVehicleVisitor>
            {
                service
            };

            foreach (var vehicle in vehicles)
            {
                foreach (var vehicleVisitor in visitors)
                {
                    vehicle.Accept(vehicleVisitor);
                }
            }
        }

        private static void PrintVehicles(List<IVehicle> vehicles)
        {
            var vehiclePrinter = new VehiclePrinter();

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"Type: {vehicle.GetType()} Capacity: {vehicle.Capacity} Fuel: {vehicle.CurrentFuel}");
                vehicle.Accept(vehiclePrinter);
            }

            Console.WriteLine();
        }
    }
}
