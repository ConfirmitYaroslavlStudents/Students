using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;

namespace SkillTree
{ 
    public class Graph
    {             
        private Dictionary<int, Vertex> vertexs;

        public Action OpenSkillInformation;

        public Action CloseSkillInformation;

        public Canvas Board { get; private set; }

        public int MAXID { get; private set; }

        public Vertex FocusVertex { get; private set; }

        private Arrow FocusArrow { get; set; }

        public bool IsAddDependenceModActive;

        private bool IsFocusOnVertex;

        private bool ISPresentation;

        public Graph(Canvas board , bool flag =false)
        {
            vertexs = new Dictionary<int, Vertex>();
            Board = board;
            Board.MouseLeftButtonUp += CanvasLeftMouseUp;
            ISPresentation = flag;
        }        

        public Skill this[int Id]
        {
            get
            {
                return vertexs[Id].Skill;
            }
        }

        #region Graph Constructor

        public void AddVertex(Skill skill , double x,double y)
        {
            var vertex = new Vertex(skill, MAXID, x, y);
            vertex.Ellipse.MouseLeftButtonUp += EllipseLeftMouseUp;
            vertex.Ellipse.MouseUp += MakeEllipseOnFocus;
            vertexs.Add(MAXID, vertex);
            MAXID++;
            AddVertexsToCanvas(vertex);
        }
       
        private void AddVertex(Vertex vertex)
        {
            vertex.dependences = new List<Vertex>();
            vertex.Ellipse.MouseLeftButtonUp += EllipseLeftMouseUp;
            vertex.Ellipse.MouseUp += MakeEllipseOnFocus;
            vertexs.Add(vertex.Id, vertex);
            AddVertexsToCanvas(vertex);
        }

        public void RemoveVertex(int ID)
        {            
            var vertex = vertexs[ID];          
            vertex.DeletingAllLinks();
            vertexs.Remove(ID);
            RemoveVertexAndArrowsFromCanvas(vertex);
        }

        public void AddDependence(int beginID, int endId)
        {
            var beginVertex = vertexs[beginID];
            var endVertex = vertexs[endId];
            endVertex.AddDependence(beginVertex);
            AddArrowToCanvas(beginVertex, endVertex);
        }

        public void RemoveDependence(int beginID, int endId)
        {
            vertexs[endId].RemoveDependence(vertexs[beginID]);
        }

        private void AddVertexsToCanvas(Vertex vertex)
        {
            Canvas.SetLeft(vertex.Ellipse, vertex.CenterX - vertex.Ellipse.Width / 2);
            Canvas.SetTop(vertex.Ellipse, vertex.CenterY - vertex.Ellipse.Height / 2);
            Board.Children.Add(vertex.Ellipse);
        }

        private void RemoveVertexAndArrowsFromCanvas(Vertex vertex)
        {
            Board.Children.Remove(vertex.Ellipse);
            var arrowsToRemove = new List<Arrow>();
            foreach(var element in Board.Children)
            {
                var arrow = element as Arrow;
                if (arrow != null && IsTagContainsCurrentIndex((string)(arrow).Tag,vertex))
                {
                    arrowsToRemove.Add(arrow);
                }
            }
            foreach (var element in arrowsToRemove)
            {
                Board.Children.Remove(element);
            }
        }

        private bool IsTagContainsCurrentIndex(string Tag, Vertex vertex)
        {
            var Index = Tag.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            int begin = int.Parse(Index[0]);
            int end = int.Parse(Index[1]);
            if(vertex.Id == begin || vertex.Id == end)
            {
                return true;
            }
            return false;
        }

        private void AddArrowToCanvas(Vertex beginVertex, Vertex endVertex)
        {
            Ellipse elBegin = beginVertex.Ellipse;
            Ellipse elEnd = beginVertex.Ellipse;
            var pointBegin = new Point(beginVertex.CenterX, beginVertex.CenterY);
            var pointEnd = new Point(endVertex.CenterX, endVertex.CenterY);
            var arrow = new Arrow(pointBegin, pointEnd);
            arrow.Tag = $"{beginVertex.Id}_{endVertex.Id}";
            arrow.MouseEnter += Arrow_MouseEnter;
            arrow.MouseLeave += Arrow_MouseLeave;
            arrow.MouseRightButtonUp += ArrowMouseRightButtonUp;
            Board.Children.Add(arrow);
        }

        private void ArrowMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ISPresentation) return;
            var arrow = (Arrow)sender;
            RemoveArrowFromCanvas(arrow);
        }

        private void Arrow_MouseLeave(object sender, MouseEventArgs e)
        {
            var arrow = (Arrow)sender;
            arrow.StrokeThickness = 2;                  
        }

        private void Arrow_MouseEnter(object sender, MouseEventArgs e)
        {
            var arrow = (Arrow)sender;
            arrow.StrokeThickness = 4;
        }

        private void RemoveArrowFromCanvas(Arrow arrow)
        {
            Board.Children.Remove(arrow);
            var tag = ((string)arrow.Tag).Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            int begin = int.Parse(tag[0]);
            int end = int.Parse(tag[1]);
            RemoveDependence(begin, end);
        }
        #endregion

        public void Save(string path)
        {
            var graphInformation = new GraphInformation() { MAXId = MAXID };
            graphInformation.AddVertexs(vertexs.Values.ToArray());
            XmlSerializer serializer = new XmlSerializer(typeof(GraphInformation));
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(stream, graphInformation);
            }
        }

        public static Graph Load(string path, Canvas canvas , bool flag = false)
        {
            var graph = new Graph(canvas, flag);
            XmlSerializer serializer = new XmlSerializer(typeof(GraphInformation));
            var graphInformation = new GraphInformation();
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                graphInformation = (GraphInformation)serializer.Deserialize(stream);
            }
            if (graphInformation == null)
                throw new ArgumentNullException("Failed to load values.");

            graph.MAXID = graphInformation.MAXId;
            foreach (var vertex in graphInformation.Vertexs)
            {
                vertex.InitializingAnEllipseDuringDeserialization();
                graph.AddVertex(vertex);
            }

            int index = 0;
            foreach (var dependences in graphInformation.Dependences)
            {
                var currentVertex = graphInformation.Vertexs[index];
                foreach (var dependenceId in dependences)
                {
                    graph.AddDependence(dependenceId, currentVertex.Id);
                }
                index++;
            }

            return graph;
        }

        public static int GetIDOfEllipse(Ellipse ellipse) => int.Parse((string)(ellipse.Tag));

        private void MakeEllipseOnFocus(object sender, MouseButtonEventArgs args)
        {
            if(!IsAddDependenceModActive)
            {
                if (FocusVertex != null)
                {
                    FocusVertex.MakeVertexInDefaultColor();
                }
                var ID = int.Parse((string)(((Ellipse)sender).Tag));
                FocusVertex = vertexs[ID];
                FocusVertex.MakeVertexInFocusColor();
                IsFocusOnVertex = true;
                OpenSkillInformation();
            }
        }

        private void EllipseLeftMouseUp(object sender, MouseButtonEventArgs args)
        {
            if (IsAddDependenceModActive)
            {
                try
                {
                    var endId = GetIDOfEllipse((Ellipse)sender);
                    AddDependence(FocusVertex.Id, endId);
                }
                catch(InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {                   
                }                
            }
        }

        public void DeleteFocusVertex()
        {
            if (FocusVertex != null)
            {
                FocusVertex.MakeVertexInDefaultColor();
                IsAddDependenceModActive = false;
                IsFocusOnVertex = false;
                FocusVertex = null;
                CloseSkillInformation();
            }
        }

        private void CanvasLeftMouseUp(object sender, MouseButtonEventArgs args)
        {
            if (IsFocusOnVertex && !IsAddDependenceModActive) 
            {
                IsFocusOnVertex = false;
            }
            else
            {
                DeleteFocusVertex();
            }
        }

    }
}
