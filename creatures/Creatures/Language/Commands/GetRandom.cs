using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class GetRandom : ICommand
    {
        private readonly string _nameTarget;
        private readonly string _maxValueName;

        public GetRandom(string nameTarget, string maxValueName)
        {
            _nameTarget = nameTarget;
            _maxValueName = maxValueName;
        }

        public string NameTarget
        {
            get { return _nameTarget; }
        }

        public string MaxValueName
        {
            get { return _maxValueName; }
        }

        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}
