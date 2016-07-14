namespace Creatures.Language.Commands.Interfaces
{
    public interface ICommandVisitor
    {
        void Accept(NewInt command);
        void Accept(SetValue command);
        void Accept(Plus command);
        void Accept(Print command);
        void Accept(Minus command);
        void Accept(CloneValue command);
        void Accept(Condition command);
        void Accept(Stop command);
        void Accept(CloseCondition command);
        void Accept(GetState command);
        void Accept(GetRandom command);
    }
}