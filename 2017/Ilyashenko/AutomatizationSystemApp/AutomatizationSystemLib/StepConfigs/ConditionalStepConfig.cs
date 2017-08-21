using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatizationSystemLib
{
    public class ConditionalStepConfig : StepConfig
    {
        private List<StepConfig> _stepsIfConditionIsTrue;
        private List<StepConfig> _stepsIfConditionIsFalse;
        private Random _rnd;

        public ConditionalStepConfig(IEnumerable<StepConfig> stepsIfTrue, IEnumerable<StepConfig> stepsIfFalse, Random rnd)
        {
            _stepsIfConditionIsTrue = stepsIfTrue.ToList();
            _stepsIfConditionIsFalse = stepsIfFalse.ToList();
            _rnd = rnd;
        }

        public override void Execute(ProcessorInfo info, int stepId)
        {
            try
            {
                if (_rnd.Next(2) % 2 == 0)
                {
                    info.Steps.InsertRange(stepId + 1, _stepsIfConditionIsTrue);
                    var stepStatuses = new List<Status>();
                    for (int i = 0; i < _stepsIfConditionIsTrue.Count; i++)
                        stepStatuses.Add(Status.Waiting);
                    info.StepStatus.InsertRange(stepId + 1, stepStatuses);
                }
                else
                {
                    info.Steps.InsertRange(stepId + 1, _stepsIfConditionIsFalse);
                    var stepStatuses = new List<Status>();
                    for (int i = 0; i < _stepsIfConditionIsFalse.Count; i++)
                        stepStatuses.Add(Status.Waiting);
                    info.StepStatus.InsertRange(stepId + 1, stepStatuses);
                }
                info.StepStatus[stepId] = Status.Successful;
            }
            catch
            {
                info.StepStatus[stepId] = Status.Failed;
            }
        }
    }
}
