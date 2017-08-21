using AutoProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoProcessorTests
{
    [TestClass]
    public class StepCollectionTest
    {
        private StepCollection _collection;

        [TestInitialize]
        public void CollectionInit()
        {
            _collection = new StepCollection(
                new IStep[]
                {
                    new EmptyStep(),
                    new EmptyStep(),
                    new EmptyStep()
                }
                );
        }

        [TestMethod]
        public void CountIncreaseAfterAdd()
        {
            var expectedCount = _collection.Count + 1;

            _collection.Add(new EmptyStep());

            Assert.AreEqual(expectedCount, _collection.Count);
        }

        public void CountEqualsZeroAfterClear()
        {
            var expectedCount = 0;

            _collection.Clear();

            Assert.AreEqual(expectedCount, _collection.Count);
        }

        [TestMethod]
        public void ContainsReturnTrueIfCollectionContainsItem()
        {
            var item = new EmptyStep();

            _collection.Add(item);

            var expected = true;

            Assert.AreEqual(expected, _collection.Contains(item));
        }

        [TestMethod]
        public void RemoveReturnFalseIfCollectionDoesNotContainItem()
        {
            var item = new EmptyStep();

            var expected = false;

            Assert.AreEqual(expected, _collection.Remove(item));
        }

        [TestMethod]
        public void RemoveReturnTrueIfCollectionContainsItem()
        {
            var item = new EmptyStep();

            _collection.Add(item);

            var expected = true;

            Assert.AreEqual(expected, _collection.Remove(item));
        }

        [TestMethod]
        public void CollectionDoesNotContainItemAfterRemove()
        {
            var item = new EmptyStep();

            _collection.Add(item);

            _collection.Remove(item);

            var expected = false;

            Assert.AreEqual(expected, _collection.Contains(item));
        }

        [TestMethod]
        public void ContainsReturnFalseIfCollectionDoesNotContainItem()
        {
            var item = new EmptyStep();
            
            var expected = false;

            Assert.AreEqual(expected, _collection.Contains(item));
        }
    }
}
