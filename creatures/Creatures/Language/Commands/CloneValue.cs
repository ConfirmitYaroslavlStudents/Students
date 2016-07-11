using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class CloneValue : ICommand
    {
        private readonly string _targetName;
        private readonly string _sourceName;

        public CloneValue(string targetName, string sourceName)
        {
            _targetName = targetName;
            _sourceName = sourceName;
        }

        public string TargetName
        {
            get { return _targetName; }
        }

        public string SourceName
        {
            get { return _sourceName; }
        }

        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}