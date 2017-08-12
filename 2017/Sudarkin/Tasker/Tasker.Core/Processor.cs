using System.Collections.Generic;
using Tasker.Core.Applets;
using Tasker.Core.Options;

namespace Tasker.Core
{
    public class Processor
    {
        private readonly List<IApplet<IOptions>> _applets;

        public Processor()
        {
            _applets = new List<IApplet<IOptions>>();
        }

        public Processor(IEnumerable<IApplet<IOptions>> applets) : this()
        {
            foreach (var applet in applets)
            {
                AddApplet(applet);
            }
        }

        public void AddApplet(IApplet<IOptions> applet)
        {
            _applets.Add(applet);
        }

        public void Start()
        {
            State previousState = State.NotRunning;
            foreach (var applet in _applets)
            {
                if (IsDisabledToRun(previousState, applet.Condition))
                {
                    continue;
                }

                previousState = applet.Execute();
            }
        }

        private bool IsDisabledToRun(State previousState, ExecutionCondition currentCondition)
        {
            return previousState == State.Failed
                   && currentCondition == ExecutionCondition.IfPreviousIsSuccessful;
        }
    }
}