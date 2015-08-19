using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mp3TagTest
{
    [TestClass]
    public class OperationsTestClass
    {
        private TestProcessor _testProcessor;

        [TestInitialize]
        public void Init()
        {
            _testProcessor=new TestProcessor(new TestOperationFactory());
        }
        [TestMethod]
        public void SingleOperationTest()
        {
            int expectedCallCount = 2;
            int expectedCancelCount = 2;
           
            _testProcessor.CallOperation(_testProcessor.CreateOperation("test"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("undo"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("redo"));
           
            Assert.AreEqual(TestOperation.ID, _testProcessor.OperationHistory.Current.Value.OperationId);
           
            var currentOperation = _testProcessor.OperationHistory.Current.Value as TestOperation;
           
            Assert.AreEqual(expectedCallCount,currentOperation.CallCount);
            Assert.IsTrue(currentOperation.CancelFlag);
           
            _testProcessor.CallOperation(_testProcessor.CreateOperation("undo"));
           
            Assert.AreEqual(expectedCancelCount,currentOperation.CancelCount);

        }
        [TestMethod]
        public void ManyOperationTest()
        {
            //Call history:                                          undo                undo
            //               testoperation ---> anothertestoperation <---> testoperation <---> anothertestoperation
            //                                           \
           //                                             \
            //                                             anothertestoperation--->anothertestoperation
            int[] expectedCallHistory =
            {TestOperation.ID, AnotherTestOperation.ID, AnotherTestOperation.ID, AnotherTestOperation.ID};
          
            _testProcessor.CallOperation(_testProcessor.CreateOperation("test"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("anothertest"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("test"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("anothertest"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("undo"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("undo"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("anothertest"));
            _testProcessor.CallOperation(_testProcessor.CreateOperation("anothertest"));
           
            var operation = _testProcessor.OperationHistory.First;
            foreach (var operationId in expectedCallHistory)
            {
                operation = operation.Next;
                Assert.AreEqual(operationId, operation.Value.OperationId);
            }
        }
    }
}
