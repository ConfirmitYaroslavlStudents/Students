namespace Creatures.Language.Commands.Interfaces
{
    public interface ICommand
    {
        void AcceptVisitor(ICommandVisitor visitor);
    }

    public interface ICommandWithArgument : ICommand
    {
        bool ContainsAsArgument(string variable);
    }

    public interface ICommandWithConstruction : ICommand
    {
        
    }

    public interface ICommandDeclaration : ICommand
    {
        string Name { get; }
    }

    public interface ICommandSetter : ICommandWithArgument
    {
        string TargetName { get; }
    }

    public interface ICommandLonely : ICommand
    {
        
    }
}