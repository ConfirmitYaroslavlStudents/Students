using System;
using System.Threading;

namespace TimeMeasurer.Variant2
{
    public class PermissionsChecker
    {
        private readonly TimeMeasurer _timeMeasurer;

        public PermissionsChecker(TimeMeasurer timeMeasurer)
        {
            _timeMeasurer = timeMeasurer;
        }

        public bool CheckPermission(Mp3File file, UserRole role)
        {
            Random rnd = new Random();

            _timeMeasurer.Measure(() =>
            {
                Thread.Sleep(rnd.Next(50));
            }, "PermissionsChecker::CheckPermission.Sleep");

            return _timeMeasurer.Measure(
                () => rnd.Next((int)UserRole.Administrator + 1) <= (int)role, 
                "PermissionsChecker::CheckPermission.Return");
        }
    }
}