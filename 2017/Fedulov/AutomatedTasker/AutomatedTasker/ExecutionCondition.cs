namespace AutomatedTasker
{
    public class ExecutionCondition
    {
        private bool isAlways;
        private bool isIfPreviousSucceded;

        public bool Always
        {
            set
            {
                isAlways = value;
                isIfPreviousSucceded = !value;
            }

            get { return isAlways; }
        }

        public bool IfPreviousSucceded
        {
            set
            {
                isIfPreviousSucceded = value;
                isAlways = !value;
            }

            get { return isIfPreviousSucceded; }
        }
    }
}
