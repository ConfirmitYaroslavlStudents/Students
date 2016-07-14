using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class Plus : ICommandSetter
    {
        private readonly string _nameTarget;
        private readonly string _firstSource;
        private readonly string _secondSource;

        public Plus(string nameTarget, string firstSource, string secondSource)
        {
            _nameTarget = nameTarget;
            _firstSource = firstSource;
            _secondSource = secondSource;
        }

        public string TargetName
        {
            get { return _nameTarget; }
        }

        public string FirstSource
        {
            get { return _firstSource; }
        }

        public string SecondSource
        {
            get { return _secondSource; }
        }

        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }

        public bool ContainsAsArgument(string variable)
        {
            return _firstSource == variable || _secondSource == variable;
        }
    }
}