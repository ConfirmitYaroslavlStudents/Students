using System.Diagnostics;

namespace TaggerLib
{
    internal abstract class Action
    {
        public abstract void Act();
    }

    internal class FileChanger : Action
    {
        public IChangingFile Changer;

        public FileChanger(IChangingFile changer)
        {
            Changer = changer;
        }

        public override void Act()
        {
            Changer.Change();
        }
    }

    internal abstract class ActionDecorator : Action
    {
        protected Action NextAction;

        public void SetAction(Action action)
        {
            NextAction = action;
        }

        public override void Act()
        {
            if (NextAction != null)
                NextAction.Act();
        }
    }

    internal class CheckPermission : ActionDecorator
    {
        public bool Permission { get; set; }

        public CheckPermission(Action action)
        {
            SetAction(action);
        }

        public override void Act()
        {
            Permission = true;
            base.Act();
        }
    }

    internal class TimeMeasurer : ActionDecorator
    {
        public Stopwatch Sw;

        public TimeMeasurer(Action action)
        {
            SetAction(action);
            Sw = new Stopwatch();
        }

        public override void Act()
        {
            Sw.Start();
            base.Act();
            Sw.Stop();
        }
    }
}
