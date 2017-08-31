using System;
using AutoProcessor;

namespace AutoProcessorTests
{
    public class ErrorStep : IStep
    {
        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
