using Mp3TagLib;
using Mp3TagLib.Operations;

namespace Mp3TagTest
{
    public class TestProcessor:Processor
    {

        public OperationsList OperationHistory { get { return OperationList; } }
        public TestProcessor(AbstractOperationFactory operationFactory) : base(operationFactory)
        {
        }
    }
}