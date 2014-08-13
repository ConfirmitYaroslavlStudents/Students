using System;
using Microsoft.CSharp.RuntimeBinder;

namespace Colors
{
    public class ColorHelper
    {
        public void Process(dynamic color1, dynamic color2, IProcessor processor)
        {
            try
            {
                processor.Process(color1, color2);
            }
            catch (RuntimeBinderException)
            {
                throw new ArgumentException("Cant't process this combination of colors!");
            }
        }
    }
}
