using System;
using System.Threading;

namespace TimeMeasurer
{
    public class PermissionsChecker : IPermissionsChecker
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
                () => role >= UserRole.Journalist, 
                "PermissionsChecker::CheckPermission.Return");
        }
    }
}