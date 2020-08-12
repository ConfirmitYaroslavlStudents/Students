namespace SkillApplication
{
    class Program
    {
        static void Main()
        {
            var manager = new SkillTreeManager();
            SkillTreeBoard.Program.RunMenu(manager);
        }
    }
}
