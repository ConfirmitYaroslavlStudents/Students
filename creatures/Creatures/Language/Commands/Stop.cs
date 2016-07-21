using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class Stop : ICommandLonely
    {
        public void AcceptVisitor(ICommandVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}
