using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class Print : ICommandWithArgument
    {
        private readonly string _variable;

        public Print(string variable)
        {
            _variable = variable;
        }

        public string Variable
        {
            get { return _variable; }
        }

        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }

        public bool ContainsAsArgument(string variable)
        {
            return _variable == variable;
        }
    }
}
