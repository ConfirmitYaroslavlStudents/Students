using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args, IWorker worker)
        {
            var command = new CommandFactory().ChooseCommand(args);
            worker.WriteLine(command.Execute());

            if (!command.ShouldBeCompleted) return;

            worker.WriteLine("\n\nSave changes? Input y/n");
            var answer = worker.ReadLine();
            // TODO: how it should react in case of other letters??
            switch (answer)
            {
                case "y":
                    command.Complete();
                    worker.WriteLine("Command successfully executed.");
                    break;
                case "n":
                    worker.WriteLine("Command canceled");
                    break;
            }
        }
    }
}
