using System;
using System.Threading.Tasks;

namespace FaultTolerance
{
    public class Timeout : FaultTolerance
    {
        public Timeout(int time)
        {
            timeLimit = time;
        }

        private int timeLimit;

        public Timeout SetTime(int time)
        {
            timeLimit = time;

            return this;
        }

        public override void Execute(Action action)
        {
            var task = new Task(action);

            task.Start();

            task.Wait(timeLimit);
        }
    }
}
