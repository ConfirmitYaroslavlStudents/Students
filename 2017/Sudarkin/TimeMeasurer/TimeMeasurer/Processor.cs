using System;

namespace TimeMeasurer
{
    public class Processor
    {
        public void Process()
        {
            TimeMeasurer timeMeasurer = new TimeMeasurer(true);
            Mp3Renamer renamer = new Mp3Renamer(timeMeasurer);
            PermissionsChecker checker = new PermissionsChecker(timeMeasurer);
            var renamerDecorator 
                = new WithPermissionDecorator(renamer, checker, UserRole.Journalist);

            for (int i = 0; i < 100; i++)
            {
                renamerDecorator.Rename(new Mp3File());
            }

            Console.WriteLine(timeMeasurer.GetResults());
        }
    }
}