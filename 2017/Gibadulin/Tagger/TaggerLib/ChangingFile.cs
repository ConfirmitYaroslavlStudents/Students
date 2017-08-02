using System;

namespace TaggerLib
{
    internal interface IChangingFile
    {
        void Change();
        File FileForChange { get; set; }
    }

    internal class ChangingFile
    {
        public static IChangingFile GetChange(string modifier)
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
