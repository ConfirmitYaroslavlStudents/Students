namespace Vehicles
{
    public interface IFlyingVehicle : IVehicle
    {
        int CountOfWings { get; set; }

        void Fly();
    }
}

