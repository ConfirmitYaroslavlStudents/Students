using System;
using System.Threading;

namespace TimeMeasurer.Variant1
{
    public class PermissionsChecker
    {
        public bool CheckPermission(Mp3File file, UserRole role)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(50));

            return rnd.Next((int)UserRole.Administrator + 1) <= (int)role;
        }
    }
}