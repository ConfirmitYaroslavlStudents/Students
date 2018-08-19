namespace Vehicles.Visitors
{
    public interface IVehicleVisitor
    {
        void Visit(IFlyingVehicle vehicle);
        void Visit(IRoadVeicle vehicle);
    }
}

