using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SkillTree
{
    [Serializable]
    public class Vertex 
    {
        private event VertexDependence VertexDependences;

        [XmlIgnore]
        internal List<Vertex> dependences;

        [XmlIgnore]
        public Ellipse Ellipse { get; private set; }

        #region VertexInformation
        public double CenterX { get;  set; }

        public double CenterY { get;  set; }

        public int Id { get;  set; }

        public Skill Skill { get; set; }

        public bool Finish { get; set; }

        public bool Available { get; set; }
        #endregion

        public Vertex(Skill skill, int id , double x , double y)
        {
            Skill = skill;
            Id = id;           
            Available = true;
            dependences = new List<Vertex>();
            Ellipse = new Ellipse()
            {
                Tag = $"{Id.ToString()}",               
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 2,
                Height = 44,
                Width = 44,
            };
            CenterX = x + Ellipse.Width / 2;
            CenterY = y + Ellipse.Height / 2;
            PaintTheVertexInTheAppropriateColor();
        }

        public Vertex() { }

        internal void AddDependence(Vertex vertex)
        {            
            if (dependences.Contains(vertex))
                throw new InvalidOperationException("There was dependency.");
            if (vertex.Id == Id)
                throw new InvalidOperationException("You can't add a dependency on yourself.");
            if (!SeachingForCycles(vertex, this)) 
            {                    
                dependences.Add(vertex);
                vertex.VertexDependences += RemoveDependence;
                Available = false;
                PaintTheVertexInTheAppropriateColor();
            }
            else
                throw new InvalidOperationException("A cycle is formed.");

        }

        private bool SeachingForCycles(Vertex start , Vertex end)
        {
            var stack = new Stack<Vertex>();
            stack.Push(start);
            while (stack.Count != 0) 
            {
                if (stack.Peek().dependences.Count == 0)
                {
                    stack.Pop();
                    continue;
                }
                foreach(var ver in stack.Pop().dependences)
                {
                    if (ver.Id == end.Id)
                    {
                        return true;
                    }
                    stack.Push(ver);
                }

            }
            return false;
        }

        internal void RemoveDependence(Vertex vertex , bool flag = false)
        {
            if (!flag)
            {
                if (!dependences.Remove(vertex))
                    throw new InvalidOperationException("There was no dependency.");
                vertex.VertexDependences -= RemoveDependence;
                if (dependences.Count == 0)
                {
                    Available = true;
                    PaintTheVertexInTheAppropriateColor();
                }
            }
            else if(IsFinishForAllDependences())
            {
                Available = true;
                PaintTheVertexInTheAppropriateColor();
            }
        }              

        internal void InitializingAnEllipseDuringDeserialization()
        {
            Ellipse = new Ellipse()
            {
                Tag = $"{Id.ToString()}",
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 2,
                Height = 44,
                Width = 44,
            };
            PaintTheVertexInTheAppropriateColor();
        }

        private void PaintTheVertexInTheAppropriateColor()
        {           
            if(Finish)
            {
                Ellipse.Fill = System.Windows.Media.Brushes.Gray;
            }
            else if(Available)
            {
                Ellipse.Fill = System.Windows.Media.Brushes.Green;
            }
            else
            {
                Ellipse.Fill = System.Windows.Media.Brushes.Red;
            }
        }

        internal void MakeVertexInFocusColor()=> Ellipse.Stroke = System.Windows.Media.Brushes.Yellow;

        internal void MakeVertexInDefaultColor() => Ellipse.Stroke = System.Windows.Media.Brushes.Black;

        private bool IsFinishForAllDependences() => dependences.TrueForAll((i) => i.Finish);

        internal void DeletingAllLinks()
        {            
            foreach(var c in dependences)
            {
                c.VertexDependences -= RemoveDependence;
            }
            VertexDependences?.Invoke(this);            
        }

        public void Recognize()
        {
            if (IsFinishForAllDependences())
            {
                Finish = true;
                PaintTheVertexInTheAppropriateColor();
                VertexDependences?.Invoke(this, true);
            }
            else                
                throw new InvalidOperationException("Skill not Avalible");
        }

        internal int[] GetDependences() => dependences.Select((i) => i.Id).ToArray();

    }
}
