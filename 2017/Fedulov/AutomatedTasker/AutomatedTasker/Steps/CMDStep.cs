namespace AutomatedTasker.Steps
{
    public class CMDStep : IStep
    {
        public string Command { set; get; }

        public CMDStep(string command)
        {
            Command = command;
        }
        
        public void Execute()
        {
            System.Diagnostics.Process.Start("cmd.exe", "/C " + Command);
        }       
    }
}
