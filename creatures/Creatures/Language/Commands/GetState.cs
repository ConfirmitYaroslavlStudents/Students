using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Commands
{
    public class GetState : ICommandSetter
    {
        private readonly string _nameTarget;
        private readonly int _direction;

        public GetState(string nameTarget, int direction)
        {
            _nameTarget = nameTarget;
            _direction = direction;
        }

        public string TargetName
        {
            get { return _nameTarget; }
        }

        public int Direction
        {
            get { return _direction; }
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
