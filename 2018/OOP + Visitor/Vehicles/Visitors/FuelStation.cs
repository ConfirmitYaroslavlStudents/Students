namespace Vehicles.Visitors
{
    public class FuelStation: IVehicleVisitor
    {

        public void Visit(IFlyingVehicle vehicle)
        {
            vehicle.CurrentFuel += 50000;
        }

        public void Visit(IRoadVeicle vehicle)
        {
            var fuelNeeded = vehicle.MaxFuel - vehicle.CurrentFuel;
            CurrentFuel -= fuelNeeded;
            vehicle.CurrentFuel += fuelNeeded;
        }

        public int CurrentFuel { get; set; } = 100000;
    }
}

