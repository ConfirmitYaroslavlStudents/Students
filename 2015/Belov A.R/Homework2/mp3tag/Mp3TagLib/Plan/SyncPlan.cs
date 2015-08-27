using System;
using System.Collections.Generic;
using System.Text;

namespace Mp3TagLib.Plan
{
    [Serializable]
    public class SyncPlan : IEnumerable<PlanItem>
    {
        private List<PlanItem> _body;

        public SyncPlan()
        {
            _body = new List<PlanItem>();

        }

        public void Add(PlanItem item)
        {
            _body.Add(item);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var planItem in _body)
            {
                builder.AppendLine(planItem.Message);
            }
            return builder.ToString();
        }

        public IEnumerator<PlanItem> GetEnumerator()
        {
            return _body.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
