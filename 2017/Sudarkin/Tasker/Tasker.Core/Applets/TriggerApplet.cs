using System.Collections.Generic;

namespace Tasker.Core.Applets
{
    public class TriggerApplet : IApplet
    {
        private readonly State _expectedState;
        private readonly IApplet _conditionApplet;
        private readonly List<IApplet> _positiveApplets;
        private readonly List<IApplet> _negativeApplets;

        public int Condition { get; }

        private TriggerApplet()
        {
            _positiveApplets = new List<IApplet>();
            _negativeApplets = new List<IApplet>();
        }

        public TriggerApplet(State expectedState, IApplet conditionApplet)
            : this(ExecutionCondition.Always, expectedState, conditionApplet)
        {

        }

        public TriggerApplet(int condition, State expectedState, IApplet conditionApplet) : this()
        {
            Condition = condition;
            _expectedState = expectedState;
            _conditionApplet = conditionApplet;
        }

        public void AddPositiveApplet(IApplet applet)
        {
            _positiveApplets.Add(applet);
        }

        public void AddNegativeApplet(IApplet applet)
        {
            _negativeApplets.Add(applet);
        }

        public State Execute()
        {
            Processor processor;
            State returnState;

            if (_expectedState == _conditionApplet.Execute())
            {
                processor = new Processor(_positiveApplets);
                returnState = State.Successful;
            }
            else
            {
                processor = new Processor(_negativeApplets);
                returnState = State.Failed;
            }

            processor.Start();

            return returnState;
        }
    }
}