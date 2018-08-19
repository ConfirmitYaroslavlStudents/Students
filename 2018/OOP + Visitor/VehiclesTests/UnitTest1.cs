using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vehicles;
using Vehicles.Visitors;

namespace VehiclesTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void Taxi_Drive_CorrectCurrentFuel()
        {
            var taxi = new Taxi(new RealFortuneManager());
            var distance = 10;

            Assert.AreEqual(50, taxi.CurrentFuel);

            taxi.Drive(distance);

            Assert.AreEqual(49, taxi.CurrentFuel);
        }

        [TestMethod]
        public void FuelStation_FillVehicleTank_EqualsMaxFuel()
        {
            var taxi = new Taxi(new RealFortuneManager());
            var distance = 200;
            taxi.Drive(distance);
            FuelStation station = new FuelStation();
            taxi.Accept(station);

            Assert.AreEqual(taxi.MaxFuel, taxi.CurrentFuel);
        }

        [TestMethod]
        public void FuelStation_FillVehicleTank_StationSpendsFuel()
        {
            var taxi = new Taxi(new RealFortuneManager());
            var distance = 200;
            taxi.Drive(distance);
            FuelStation station = new FuelStation();
            taxi.Accept(station);

            Assert.AreEqual(99980, station.CurrentFuel);
        }

        [TestMethod]
        public void Taxi_Drive_Badluck_BreakWheel()
        {
            var taxi = new Taxi(new TestFortuneManager(new List<bool>
            {
                false, false, true
            }));
            var distance = 10;

            taxi.Drive(distance);
            Assert.AreEqual(4, taxi.CountOfWheels);
            taxi.Drive(distance);
            Assert.AreEqual(4, taxi.CountOfWheels);
            taxi.Drive(distance);
            Assert.AreEqual(3, taxi.CountOfWheels);
        }
    }
}
