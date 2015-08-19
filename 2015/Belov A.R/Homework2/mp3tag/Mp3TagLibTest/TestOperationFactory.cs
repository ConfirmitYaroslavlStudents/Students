using System;
using Mp3TagLib.Operations;

namespace Mp3TagTest
{
    public class TestOperationFactory:AbstractOperationFactory
    {

        public override Operation CreateOperation(int id)
        {
            if(id==TestOperation.ID)
                return new TestOperation();
            if (id == AnotherTestOperation.ID)
                return new AnotherTestOperation();
            throw new ArgumentException("Invalid command");
        }

        public override Operation CreateOperation(string name)
        {
            if (name == "test")
                return new TestOperation();
            
            if(name=="anothertest")
                return new AnotherTestOperation();
           
            int id;
            if (int.TryParse(name, out id))
            {
                return CreateOperation(id);
            }  
            throw new ArgumentException("Invalid command");
        }
    }
}