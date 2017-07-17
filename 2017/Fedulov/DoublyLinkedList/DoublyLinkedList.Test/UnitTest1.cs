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
            
            int[] arr = { 5, 4, 3, 2, 1 };
            
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            
            Assert.AreEqual(arr.Length, lst.Count);
        }

        [TestMethod]
        public void List_Init_and_Clear()
        {
            int[] arr = { 5, 4, 3, 2, 1 };
            
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            lst.Clear();
            
            IEnumerator<int> enumerator = lst.GetEnumerator();
            Assert.AreEqual(false, enumerator.MoveNext());
            Assert.AreEqual(0, lst.Count);
        }

        [TestMethod]
        public void List_Init_and_CopyTo()
        {
            int[] arr = {5, 4, 3, 2, 1};
            
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            int[] arr1 = new int[5];
            lst.CopyTo(arr1, 0);

            for (int i = 0; i < arr.Length; ++i)         
                Assert.AreEqual(arr[i], arr1[i]);
            
        }

        [TestMethod]
        public void List_Init_and_CopyTo_Null_source()
        {
            int[] arr = { 5, 4, 3, 2, 1 };
            
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            int[] arr1 = null;
            try
            {
                lst.CopyTo(arr1, 0);

                Assert.Fail("Succesfully copied to null source");
            }
            catch (NullReferenceException e)
            {
                Assert.IsNotNull(e);
            }
        }

        [TestMethod]
        public void List_Init_and_CopyTo_Negative_Position()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            int[] arr1 = new int[5];
            try
            {
                lst.CopyTo(arr1, -5);

                Assert.Fail("Successfully copied to negative position");
            }
            catch (IndexOutOfRangeException e)
            {
                Assert.IsNotNull(e);
            }
        }

        [TestMethod]
        public void List_Init_and_CopyTo_Less_Memory_allocated()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            int[] arr1 = new int[5];

            try
            {
                lst.CopyTo(arr1, 2);
            
                Assert.Fail("Succesfully copied to source with less memory allocated");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.IsNotNull(e);
            }
        }

        [TestMethod]
        public void List_Init_and_IEnumerator()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            IEnumerator<int> enumerator = lst.GetEnumerator();

            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
        }


        [TestMethod]
        public void List_Add_and_CopyTo()
        { 
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>();

            int[] arr = { 5, 4, 3, 2, 1 };
            foreach (var item in arr)
            {
                lst.Add(item);
            }

            int[] arr1 = new int[5];
            lst.CopyTo(arr1, 0);

            Assert.AreEqual(arr.Length, lst.Count);
            Assert.AreEqual(arr.Length, arr1.Length);
            for(int i = 0; i < arr.Length; ++i)
                Assert.AreEqual(arr[i], arr1[i]);
        }

        [TestMethod]
        public void List_Add_and_Enumerate()
        {
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>();

            int[] arr = { 5, 4, 3, 2, 1 };
            foreach (var item in arr)
                lst.Add(item);

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
        }

        [TestMethod]
        public void List_Init_and_AddLast()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            lst.AddLast(-1);

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);

           if (enumerator.MoveNext())
            {
                Assert.AreEqual(-1, enumerator.Current);
                if (enumerator.MoveNext())
                    Assert.Fail("Extra info in list");
            }
            else
                Assert.Fail("Not enough items in list");
        }

        [TestMethod]
        public void List_Init_and_AddFirst()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            lst.AddFirst(6);

            IEnumerator<int> enumerator = lst.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(6, enumerator.Current);
            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
         }

        [TestMethod]
        public void List_Init_and_Contains()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);

            Assert.AreEqual(arr.Contains(5), lst.Contains(5));
            Assert.AreEqual(arr.Contains(1), lst.Contains(1));
            Assert.AreEqual(arr.Contains(6), lst.Contains(6));
            Assert.AreEqual(arr.Contains(3), lst.Contains(3));
        }

        [TestMethod]
        public void List_Init_and_Remove()
        {
            int[] arr = { 5, 4, 3, 2, 1, 5, 4, 3, 2, 1 };
            int[] arr_no5 = { 4, 3, 2, 1, 4, 3, 2, 1 };
            int[] arr_no5_1 = { 4, 3, 2, 4, 3, 2 };
            int[] arr_5 = {5, 5, 5, 5, 5, 5, 5, 5, 5, 5};

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            DoublyLinkedList<int> lst_5 = new DoublyLinkedList<int>(arr_5);

            while (lst.Contains(5))
                Assert.IsTrue(lst.Remove(5));      
            
            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr_no5.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr_no5[i], enumerator.Current);

            while (lst.Contains(1))
                Assert.IsTrue(lst.Remove(1));

            enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr_no5_1.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr_no5_1[i], enumerator.Current);

            for (int i = 0; i < arr_5.Length; ++i)
                Assert.IsTrue(lst_5.Remove(5));

            if (lst_5.Count != 0)
                Assert.Fail("Not all items removed");

        }

        [TestMethod]
        public void List_Init_and_RemoveFirst()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            lst.RemoveFirst();

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 1; i < arr.Length && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
        }

        [TestMethod]
        public void List_Init_and_RemoveLast()
        {
            int[] arr = { 5, 4, 3, 2, 1 };

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            lst.RemoveLast();

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr.Length - 1 && enumerator.MoveNext(); ++i)
                Assert.AreEqual(arr[i], enumerator.Current);
        }
    }
}
