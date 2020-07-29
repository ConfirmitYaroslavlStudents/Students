namespace SkillApplication
{
    class Program
    {
        static void Main()
        {
            var manager = new SkillTreeManager.SkillTreeManager();
            SkillTreeBoard.Program.CreateAndRunMenu(manager);
        }
    }
}
