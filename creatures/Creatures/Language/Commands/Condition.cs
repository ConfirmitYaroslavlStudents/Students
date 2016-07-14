using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class Condition : ICommandWithConstruction,ICommandWithArgument
    {
        private readonly string _conditionName;

        public Condition(string conditionName)
        {
            _conditionName = conditionName;
        }

        public string ConditionName
        {
            get { return _conditionName; }
        }

        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }

        public bool ContainsAsArgument(string variable)
        {
            return variable == _conditionName;
        }
    }
}