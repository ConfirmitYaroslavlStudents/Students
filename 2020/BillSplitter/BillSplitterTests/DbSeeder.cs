using System.Collections.Generic;
using BillSplitter.Data;
using BillSplitter.Models;

namespace BillSplitterTests
{
    public class DbSeeder
    {
        public void DbSeed(BillContext db)
        {
            int billId = SeedBill(db);

            var positionsIds = SeedPositions(db, billId);
            var customersIds = SeedCustomers(db, billId);
            SeedOrders(db, positionsIds, customersIds);
        }

        private int SeedBill(BillContext db)
        {
            var bill = new Bill
            {
                Name = "PizzaBill"
            };

            db.Bills.Add(bill);

            db.SaveChanges();

            return bill.Id;
        }

        private List<int> SeedPositions(BillContext db, int billId)
        {
            var pizza = new Position
            {
                BillId = billId,
                Name = "Pizza",
                Price = 100,
                Quantity = 1
            };

            var cola = new Position
            {
                BillId = billId,
                Name = "Cola",
                Price = 20,
                Quantity = 3
            };

            db.Positions.AddRange(pizza, cola);

            db.SaveChanges();

            return new List<int> { pizza.Id, cola.Id };
        }

        private List<int> SeedCustomers(BillContext db, int billId)
        {
            var obiWan = new Member
            {
                BillId = billId,
                Name = "ObiWan"
            };

            var anakin = new Member
            {
                BillId = billId,
                Name = "Anakin"
            };

            var padme = new Member
            {
                BillId = billId,
                Name = "Padme"
            };

            db.Members.AddRange(obiWan, anakin, padme);

            db.SaveChanges();

            return new List<int> { obiWan.Id, anakin.Id, padme.Id };
        }

        private void SeedOrders(BillContext db, List<int> positionsIds, List<int> customersIds)
        {
            var obiWanPizza = new Order
            {
                PositionId = positionsIds[0],
                MemberId = customersIds[0],
                Quantity = 0.25m
            };

            var obiWanCola = new Order
            {
                PositionId = positionsIds[1],
                MemberId = customersIds[0],
                Quantity = 2
            };

            var anakinPizza = new Order
            {
                PositionId = positionsIds[0],
                MemberId = customersIds[1],
                Quantity = null
            };

            var anakinCola = new Order
            {
                PositionId = positionsIds[1],
                MemberId = customersIds[1],
                Quantity = null
            };

            var padmePizza = new Order
            {
                PositionId = positionsIds[0],
                MemberId = customersIds[2],
                Quantity = null
            };

            var padmeCola = new Order
            {
                PositionId = positionsIds[1],
                MemberId = customersIds[2],
                Quantity = null
            };

            db.Orders.AddRange(obiWanCola, obiWanPizza, anakinCola, anakinPizza, padmeCola, padmePizza);

            db.SaveChanges();
        }
    }
}