namespace Vehicles
{
    public interface IMotoVehicle : IRoadVeicle
    {
        bool HasSideCar { get; set; }
    }
}

