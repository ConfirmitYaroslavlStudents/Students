using System;
using Mp3UtilConsole.Actions;
using Mp3UtilConsole.Arguments;
using Mp3UtilConsole.Logger;

namespace Mp3UtilConsole
{
    internal class Program
    {
        private static readonly ILogger Logger = new ConsoleLogger();

        private static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
            Args arguments = null;
            try
            {
                arguments = ArgumentsManager.Parse(args);
            }
            catch (ArgumentException ex)
            {
                Logger.Write(ex.Message, LogStatus.Error);
                return;
            }

            IActionStrategy action = GetActionStrategy(arguments.Action);

            foreach (string file in fileManager.GetFiles(arguments.Mask, arguments.Recursive))
            {
                try
                {
                    action.Process(new Mp3File(file));
                    Logger.Write($"{file} - The transformation is complete", LogStatus.Success);
                }
                catch(Exception ex)
                {
                    Logger.Write(ex.Message, LogStatus.Error);
                }
            }

            Logger.Write("Done!", LogStatus.Success);
        }

        private static IActionStrategy GetActionStrategy(ProgramAction action)
        {
            switch (action)
            {
                case ProgramAction.ToFileName:
                    return new FileNameAction();
                case ProgramAction.ToTag:
                    return new TagAction();
                default:
                    return null;
            }
        }
    }
}