using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class SetValue : ICommandSetter
    {
        private readonly string _name;
        private readonly int _value;

        public SetValue(string name, int value)
        {
            _name = name;
            _value = value;
        }

        public string TargetName
        {
            get { return _name; }
        }

        public int Value
        {
            get { return _value; }
        }

        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }

        public bool ContainsAsArgument(string variable)
        {
            return false;
        }
    }
}