using Mp3UtilLib.Actions;

namespace Mp3UtilLib
{
    public class ActionCreator
    {
        public IActionStrategy GetAction(ProgramAction action)
        {
            switch (action)
            {
                case ProgramAction.ToFileName:
                    return new FileNameAction();
                case ProgramAction.ToTag:
                    return new TagAction();
                default:
                    return null;
            }
        }
    }
}