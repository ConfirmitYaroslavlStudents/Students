using System.Collections;
using System.Collections.Generic;
using HospitalLib.Interfaces;

namespace HospitalLib.Template
{
    public class Line : IEnumerable<IElement>
    {
        private readonly IList<IElement> _listOfElementsInLine = new List<IElement>();

        public int Height { get; set; }
        public int Width { get; set; }

        public void AddElement(IElement element)
        {
            _listOfElementsInLine.Add(element);
        }

        public IEnumerator<IElement> GetEnumerator()
        {
            foreach (var element in _listOfElementsInLine)
                yield return element;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}