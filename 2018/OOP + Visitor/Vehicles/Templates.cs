//namespace Vehicles
//{
//    public interface IVehicleVisitor
//    {
//        void Visit(IRoadVehicle vehicle);
//        void Visit(ISwimmingVehicle vehicle);
//        void Visit(IFlyingVehicle vehicle);
//    }

//    public interface IVehicle
//    {
//        int Fuel { get; set; }

//        int Сapacity { get; set; }

//        int Weight { get; set; }

//        void StartEngine();

//        void StopEngine();

//        void Accept(IVehicleVisitor visitor);
//    }

//    public interface ISwimmingVehicle : IVehicle
//    {
//        int CountOfPropellers { get; set; }

//        void Swim();
//    }

//    public interface IFlyingVehicle : IVehicle
//    {
//        int CountOfWings { get; set; }

//        void Fly();
//    }

//    public interface IRoadVehicle : IVehicle
//    {
//        int CountOfWheels { get; set; }

//        void Drive();
//    }

//    public class Car : IRoadVehicle
//    {
//        public int Fuel { get; set; }
//        public int Сapacity { get; set; }
//        public int Weight { get; set; }

//        public void StartEngine()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void StopEngine()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Accept(IVehicleVisitor visitor)
//        {
//            visitor.Visit(this);
//        }

//        public int CountOfWheels { get; set; }
//        public void Drive()
//        {
//            throw new System.NotImplementedException();
//        }
//    }

//    public class Airplane : IFlyingVehicle
//    {
//        public int Fuel { get; set; }
//        public int Сapacity { get; set; }
//        public int Weight { get; set; }
//        public void StartEngine()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void StopEngine()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Accept(IVehicleVisitor visitor)
//        {
//            visitor.Visit(this);
//        }

//        public int CountOfWings { get; set; }
//        public void Fly()
//        {
//            throw new System.NotImplementedException();
//        }
//    }

//    public class FishersBoat : ISwimmingVehicle
//    {
//        public int Fuel { get; set; }
//        public int Сapacity { get; set; }
//        public int Weight { get; set; }
//        public void StartEngine()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void StopEngine()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Accept(IVehicleVisitor visitor)
//        {
//            visitor.Visit(this);
//        }

//        public int CountOfPropellers { get; set; }
//        public void Swim()
//        {
//            throw new System.NotImplementedException();
//        }
//    }

//    public class ServiceStation : IVehicleVisitor
//    {
//        public void Visit(IRoadVehicle vehicle)
//        {
//            if (vehicle.CountOfWheels < 4)
//            {
//                vehicle.CountOfWheels = 4;
//            }
//        }

//        public void Visit(ISwimmingVehicle vehicle)
//        {
//            if (vehicle.CountOfPropellers < 1)
//            {
//                vehicle.CountOfPropellers = 1;
//            }
//        }

//        public void Visit(IFlyingVehicle vehicle)
//        {
//            if (vehicle.CountOfWings < 2)
//            {
//                vehicle.CountOfWings = 2;
//            }
//        }
//    }

//    public class PetrolStation
//    {
//        public void AddFuel(IVehicle vehicle, int count)
//        {
//            vehicle.Fuel += count;
//        }
//    }

//    public class TaxiDriver
//    {
//        public int Money { get; set; }

//        public IRoadVehicle Vehicle { get; set; }

//        public void SetVehicle(IRoadVehicle vehicle)
//        {
//            Vehicle = vehicle;
//        }

//        public void DriveFromSourceToDestination()
//        {
//            Vehicle.StartEngine();
//            Vehicle.Drive();
//            Vehicle.StopEngine();
//        }
//    }
//}
