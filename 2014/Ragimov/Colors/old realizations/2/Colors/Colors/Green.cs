using System;

namespace Colors
{
    public class Green : MasterColor
    {
        public override void DoFirst(IColor second)
        {
            second.DoSecond(this);
        }

        public override Green GetGreen()
        {
            Console.WriteLine("Really Green");
            return this;
        }


    }
}
