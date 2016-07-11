namespace Creatures.Language.Commands.Interfaces
{
    public interface ICommand
    {
        void AcceptVisitor(ICommandVisitor visitor);
    }
}