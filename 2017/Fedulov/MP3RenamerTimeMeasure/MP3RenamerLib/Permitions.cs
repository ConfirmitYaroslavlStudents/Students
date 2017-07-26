using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3RenamerTimeMeasure
{
    public interface IPermitionsChecker
    {
        bool Check(MP3File file, Permitions permitions);
    }

    public class PermitionsChecker : IPermitionsChecker
    {
        public bool Check(MP3File file, Permitions permitions)
        {
            return file.FilePermitions <= permitions;
        }
    }

    public enum Permitions
    {
        Guest = 0,
        User = 1,
        Administrator = 2
    }
}
