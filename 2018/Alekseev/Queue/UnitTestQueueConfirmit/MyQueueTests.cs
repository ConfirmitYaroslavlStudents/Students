using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueConfirmitClass;

namespace UnitTestQueueConfirmit
{
    public class Computer : IEquatable<Computer>
    {
        int serial;
        string name;

        public Computer(int s, string n)
        {
            serial = s;
            name = n;
        }
        public bool Equals(Computer compared)
        {
            if (serial == compared.serial && name == compared.name) return true;
            return false;
        }
    }
    [TestClass]
    public class MyQueueTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            MyQueue<int> intQueue = new MyQueue<int>();
            for (int i = 1; i <= 5; i++)
                intQueue.Enqueue(i);

            if (intQueue.Peek() != 1) throw new Exception();
            intQueue.Dequeue();
            if (intQueue.Peek() != 2) throw new Exception();
            if (intQueue.Count() != 4) throw new Exception();
            intQueue.Clear();
            if (intQueue.Count() != 0) throw new Exception();
            if (intQueue.Contains(1))
                throw new Exception();
        }
        [TestMethod]
        public void TestMethod2()
        {
            MyQueue<Computer> computers = new MyQueue<Computer>();
            computers.Enqueue(new Computer(10234, "HP-Pavilion"));
            computers.Enqueue(new Computer(0, "Test"));
            computers.Enqueue(new Computer(228, "ASUS"));

            if (!computers.Contains(new Computer(0, "Test"))) throw new Exception();
            if (!computers.Peek().Equals(new Computer(10234, "HP-Pavilion"))) throw new Exception();
            computers.Clear();
            computers.Enqueue(new Computer(228, "ASUS"));
            if (!computers.Contains(new Computer(228, "ASUS"))) throw new Exception();
        }
    }
}
