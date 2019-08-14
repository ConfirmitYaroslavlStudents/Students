namespace DirectorySynchronizationApp
{
    public class Enums
    {
        public enum LogVariants
        {
            Silent,
            Verbose,
            Summary
        };

        public enum Flags
        {
            Nothing = 1,
            NoDelete = 2
        };

        public enum WhereToPrint
        {
            Console
        };
    }
}
