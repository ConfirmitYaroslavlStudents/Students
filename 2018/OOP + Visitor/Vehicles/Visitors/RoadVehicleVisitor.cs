namespace Vehicles.Visitors
{
    public class RoadVehicleVisitor : IRoadVehicleVisitor
    {
        public void Visit(ICarVehicle vehicle)
        {
            if (vehicle.HasTruck)
            {
                vehicle.CountOfWheels = 6;
            }
            else
            {
                vehicle.CountOfWheels = 4;
            }
        }

        public void Visit(IMotoVehicle vehicle)
        {
            if (vehicle.HasSideCar)
            {
                vehicle.CountOfWheels = 3;
            }
        }
    }
}

