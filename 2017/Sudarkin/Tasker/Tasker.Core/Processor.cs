using System.Collections.Generic;
using Tasker.Core.Applets;

namespace Tasker.Core
{
    public class Processor
    {
        private readonly List<IApplet> _applets;
        private readonly List<State> _states;

        public Processor()
        {
            _applets = new List<IApplet>();
            _states = new List<State>();
        }

        public Processor(IEnumerable<IApplet> applets) : this()
        {
            foreach (var applet in applets)
            {
                AddApplet(applet);
            }
        }

        public void AddApplet(IApplet applet)
        {
            _applets.Add(applet);
        }

        public void Start()
        {
            _states.Clear();

            foreach (var applet in _applets)
            {
                if (ExecutionCondition.CanExecute(applet.Condition, _states))
                {
                    _states.Add(applet.Execute());
                }
                else
                {
                    _states.Add(State.NotRunning);
                }
            }
        }
    }
}