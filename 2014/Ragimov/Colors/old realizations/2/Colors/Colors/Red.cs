using System;

namespace Colors
{
    public class Red : MasterColor
    {
        public delegate void DoDel(IColor second);

        public override void DoFirst(IColor second)
        {
            second.DoSecond(this);
        }

        public override Red GetRed()
        {
            Console.WriteLine("Really Red");
            return this;
        }

    }
}
