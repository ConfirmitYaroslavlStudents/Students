namespace Vehicles.Visitors
{

    public class ServiceStation : IVehicleVisitor
    {
        private readonly IRoadVehicleVisitor _roadVehicleVisitor = new RoadVehicleVisitor();

        public void Visit(IFlyingVehicle vehicle)
        {
            if (vehicle.CountOfWings < 2)
            {
                vehicle.CountOfWings = 2;
            }
        }

        public void Visit(IRoadVeicle vehicle)
        {
            if (vehicle.CountOfWheels < 1)
            {
                vehicle.CountOfWheels = 1;
            }

            vehicle.Accept(_roadVehicleVisitor);
        }
    }
}

