using System;

namespace TaggerLib
{
    internal interface IActing
    {
        void Act(File file);
    }

    internal class Acting
    {
        public static IActing Act(string modifier)
        {
            switch (modifier)
            {
                case Consts.ToTag:
                    return new ToTag();
                case Consts.ToName:
                    return new ToName();
                default:
                    throw new ArgumentException("Wrong modifier");
            }
        }
    }
}
