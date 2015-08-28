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

            while (!GetAnswerOnSavingChanges(worker, command))
            {
                worker.WriteLine("\nWrong input, please write y for Yes and n for No:\n");

            }           
        }

        private bool GetAnswerOnSavingChanges(IWorker worker, Command command)
        {
            var answer = worker.ReadLine();
            switch (answer)
            {
                case "y":
                    command.Complete();
                    worker.WriteLine("\nCommand successfully executed.");
                    return true;
                case "n":
                    worker.WriteLine("\nCommand canceled");
                    return true;
                default:
                    return false;
            }
        }
    }
}
