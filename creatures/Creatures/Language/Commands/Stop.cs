using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class Stop : ICommand
    {
        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}
