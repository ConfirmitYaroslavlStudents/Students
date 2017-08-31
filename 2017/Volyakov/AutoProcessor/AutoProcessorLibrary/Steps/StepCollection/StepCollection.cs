using System.Collections;
using System.Collections.Generic;

namespace AutoProcessor
{
    public class StepCollection:IEnumerable<StepStatusPair>
    {
        private List<StepStatusPair> _stepList;

        public StepCollection()
        {
            _stepList = new List<StepStatusPair>();
        }

        public StepCollection(IEnumerable<IStep> steps)
        {
            _stepList = new List<StepStatusPair>();
            
            foreach(var step in steps)
                Add(step);
        }

        public int Count
        {
            get { return _stepList.Count; }
        }
        
        public void Add(IStep item)
        {
            _stepList.Add(new StepStatusPair(item, Status.NotStarted));
        }

        public void Clear()
        {
            _stepList = new List<StepStatusPair>();
        }

        public bool Contains(IStep item)
        {
            foreach(var pair in _stepList)
            {
                if (pair.Step == item)
                    return true;
            }
            return false;
        }
        
        public bool Remove(IStep item)
        {
            for (int index = 0; index < _stepList.Count; index++)
            {
                if(_stepList[index].Step == item)
                {
                    _stepList.RemoveAt(index);

                    return true;
                }
            }

            return false;
        }

        public IEnumerator GetEnumerator()
        {
            return _stepList.GetEnumerator();
        }

        IEnumerator<StepStatusPair> IEnumerable<StepStatusPair>.GetEnumerator()
        {
            return _stepList.GetEnumerator();
        }
    }
}
