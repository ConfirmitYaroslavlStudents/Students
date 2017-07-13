using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoublyLinkedList.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void List_Init_and_Count()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

           // assert

            Assert.AreEqual(arr.Length, lst.Count);
        }

        [TestMethod]
        public void List_Init_and_Clear()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            lst.Clear();;
            // assert
            IEnumerator<int> enumerator = lst.GetEnumerator();
            Assert.AreEqual(false, enumerator.MoveNext());
           
            Assert.AreEqual(0, lst.Count);
        }

        [TestMethod]
        public void List_Init_and_CopyTo()
        {
            // arange

            int[] arr = {5, 4, 3, 2, 1};

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            int[] arr1 = new int[5];
            lst.CopyTo(arr1, 0);

            // assert

            for (int i = 0; i < arr.Length; ++i)
            {
                Assert.AreEqual(arr[i], arr1[i]);
            }
        }

        [TestMethod]
        public void List_Init_and_CopyTo_Null_source()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            int[] arr1 = null;
            try
            {
                lst.CopyTo(arr1, 0);

                // assert
                Assert.Fail();
            }
            catch (NullReferenceException e)
            {
                // assert
                Assert.IsNotNull(e);

            }
        }

        [TestMethod]
        public void List_Init_and_CopyTo_Negative_Position()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            int[] arr1 = new int[5];
            try
            {
                lst.CopyTo(arr1, -5);

                // assert
                Assert.Fail();
            }
            catch (IndexOutOfRangeException e)
            {
                // assert
                Assert.IsNotNull(e);

            }
        }

        [TestMethod]
        public void List_Init_and_CopyTo_Less_Memory_allocated()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            int[] arr1 = new int[5];

            try
            {
                lst.CopyTo(arr1, 2);
                // assert
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                Assert.IsNotNull(e);
            }
        }

        [TestMethod]
        public void List_Init_and_IEnumerator()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            IEnumerator<int> enumerator = lst.GetEnumerator();

            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
        }


        [TestMethod]
        public void List_Add_and_CopyTo()
        { 
            // arange
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>();

            // act
            lst.Add(10);
            lst.Add(40);
            lst.Add(-10);

            int[] arr1 = new int[3];
            lst.CopyTo(arr1, 0);

            // assert

            Assert.AreEqual(3, lst.Count);
            Assert.AreEqual(arr1[0], 10);
            Assert.AreEqual(arr1[1], 40);
            Assert.AreEqual(arr1[2], -10);

            /*IEnumerator<int> enumerator = lst.GetEnumerator();
            //Assert.AreEqual(10, enumerator.Current);
            enumerator.MoveNext();
            //Assert.AreEqual(40, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(-10, enumerator.Current);
            */
        }

        [TestMethod]
        public void List_Add_and_Enumerate()
        {
            // arange
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>();

            // act
            lst.Add(10);
            lst.Add(40);
            lst.Add(-10);

            

            // assert

            IEnumerator<int> enumerator = lst.GetEnumerator();
            if (enumerator.MoveNext())
                Assert.AreEqual(10, enumerator.Current);
            else
                Assert.Fail();

            if (enumerator.MoveNext())
                Assert.AreEqual(40, enumerator.Current);
            else
                Assert.Fail();

            if (enumerator.MoveNext())
                Assert.AreEqual(-10, enumerator.Current);
            else
                Assert.Fail();
            
        }

        [TestMethod]
        public void List_Init_and_AddLast()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            lst.AddLast(-1);

            // assert

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);

            try
            {
                if (enumerator.MoveNext())
                {
                    Assert.AreEqual(-1, enumerator.Current);
                    if (enumerator.MoveNext())
                        Assert.Fail();
                }
                else
                    Assert.Fail();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void List_Init_and_AddFirst()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            lst.AddFirst(6);

            // assert
            
            IEnumerator<int> enumerator = lst.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(6, enumerator.Current);
            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
         }

        [TestMethod]
        public void List_Init_and_Contains()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            // assert

            Assert.AreEqual(arr.Contains(5), lst.Contains(5));
            Assert.AreEqual(arr.Contains(1), lst.Contains(1));
            Assert.AreEqual(arr.Contains(6), lst.Contains(6));
            Assert.AreEqual(arr.Contains(3), lst.Contains(3));
        }

        [TestMethod]
        public void List_Init_and_Remove()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1, 5, 4, 3, 2, 1 };
            int[] arr_no5 = { 4, 3, 2, 1, 4, 3, 2, 1 };
            int[] arr_no5_1 = { 4, 3, 2, 4, 3, 2 };

            int[] arr_5 = {5, 5, 5, 5, 5, 5, 5, 5, 5, 5};

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            DoublyLinkedList<int> lst_5 = new DoublyLinkedList<int>(arr_5);

            // assert

            if (!lst.Remove(5))
                Assert.Fail();

            if (!lst.Remove(5))
                Assert.Fail();

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr_no5.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr_no5[i], enumerator.Current);

            if (!lst.Remove(1))
                Assert.Fail();

            if (!lst.Remove(1))
                Assert.Fail();

            enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr_no5_1.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr_no5_1[i], enumerator.Current);

            for (int i = 0; i < arr_5.Length; ++i)
                if (!lst_5.Remove(5))
                    Assert.Fail();

            if(lst_5.Count != 0)
                Assert.Fail();

        }

        [TestMethod]
        public void List_Init_and_RemoveFirst()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            lst.RemoveFirst();

            // assert

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 1; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
        }

        [TestMethod]
        public void List_Init_and_RemoveLast()
        {
            // arange

            int[] arr = { 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            lst.RemoveLast();

            // assert

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr.Length - 1 && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
        }
    }
}
