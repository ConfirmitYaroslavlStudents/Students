namespace Vehicles.Visitors
{
    public interface IRoadVehicleVisitor
    {
        void Visit(ICarVehicle vehicle);
        void Visit(IMotoVehicle vehicle);
    }
}

