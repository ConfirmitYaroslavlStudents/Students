using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillTree
{
    public class Vertex
    {
        private event VertexDependence VertexDependence;

        private readonly List<Vertex> _dependencies;

        private bool _finish;

        private bool _available;

        public Vertex(Skill skill, int id,bool available = true)
        {
            Skill = skill;
            Id = id;
            _dependencies = new List<Vertex>();
            _available = available;
        }

        public int Id { get; set; }

        public Skill Skill { get; set; }

        public bool Finish
        {
            get => _finish;
            set
            {
                if (_finish == value) return;
                if (value)
                {
                    if (_available)
                        _finish = true;
                    else
                        throw new ArgumentException("Vertex not available.");
                }
                else
                    throw new ArgumentException("Vertex is finished.");
            }
        }

        public bool Available
        {
            get => _available;
            set
            {
                if (_available == value) return;
                var flag = FinishForAllDependencies();
                if ((flag && !value) || (!flag && value))
                {
                    var argumentException = value
                        ? new ArgumentException("Vertex isn't available.")
                        : new ArgumentException("Vertex is available.");
                    throw argumentException;
                }
                else
                {
                    _available = value;
                }
            }
        }

        public void Recognize()
        {
            Finish = true;
            VertexDependence?.Invoke(this, true);
        }

        internal void AddDependence(Vertex vertex)
        {
            if (_dependencies.Contains(vertex))
                throw new GraphException("There was dependency.");
            if (vertex.Id == Id)
                throw new GraphException("You can't add a dependency on yourself.");
            if (!CycleWasFormed(vertex, this))
            {
                _dependencies.Add(vertex);
                vertex.VertexDependence += RemoveDependence;
                _available = false;
            }
            else
            {
                throw new GraphException("Cycle was formed.");
            }
        }

        internal void RemoveDependence(Vertex vertex, bool flag = false)
        {
            if (!flag)
            {
                if (!_dependencies.Remove(vertex))
                    throw new InvalidOperationException("There was no dependency.");
                vertex.VertexDependence -= RemoveDependence;
                if (_dependencies.Count == 0) _available = true;
            }
            else if (FinishForAllDependencies())
            {
                _available = true;
            }
        }

        internal void DeletingAllLinks()
        {
            foreach (var c in _dependencies) c.VertexDependence -= RemoveDependence;
            VertexDependence?.Invoke(this);
        }

        internal int[] GetVertexesDependenciesId() => _dependencies.Select((vertex) => vertex.Id).ToArray();

        private bool CycleWasFormed(Vertex start, Vertex end)
        {
            var stack = new Stack<Vertex>();
            stack.Push(start);
            while (stack.Count != 0)
            {
                if (stack.Peek()._dependencies.Count == 0)
                {
                    stack.Pop();
                    continue;
                }
                foreach (var ver in stack.Pop()._dependencies)
                {
                    if (ver.Id == end.Id) return true;
                    stack.Push(ver);
                }
            }
            return false;
        }

        private bool FinishForAllDependencies() => _dependencies.TrueForAll(i => i.Finish);

        public List<Vertex> GetDependencies() => _dependencies;

        public override string ToString()
        {
            return $"Skill{Id}";
        }
    }
}