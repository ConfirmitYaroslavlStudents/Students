using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SkillTree
{
    internal class Arrow : Shape
    {
        public const int deflectionAngleOfBranches = 25;

        public const int lengthOfBranches = 10;
        
        public Point Begin { get; private set; }

        public Point End { get; private set; }

        public Arrow(Point begin,Point end)
        {
            Begin = begin;
            End = end;
            Stroke = System.Windows.Media.Brushes.Black;
            StrokeThickness = 2;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                var BranchesPoint = LeftAndRightBranches;
                Point p1 = BranchesPoint[0];
                Point p2 = BranchesPoint[1];
                List<PathSegment> segment1 = new List<PathSegment>()
                {
                    new LineSegment(End, true),
                    new LineSegment(p1, true)
                };
                List<PathSegment> segment2 = new List<PathSegment>()
                {
                    new LineSegment(End, true),
                    new LineSegment(p2, true)
                };
                List<PathSegment> segment3 = new List<PathSegment>()
                {
                    new LineSegment(End, true),
                    new LineSegment(Begin, true)
                };
                List<PathFigure> figures = new List<PathFigure>()
                {
                    new PathFigure(End, segment1, true),
                    new PathFigure(End, segment2, true),
                    new PathFigure(End, segment3, true)
                };                     
                Geometry geometry = new PathGeometry(figures, FillRule.EvenOdd, null);
                return geometry;
            }
        }

        private Point[] LeftAndRightBranches
        {
            get
            {
                var radianAngle = Math.PI * deflectionAngleOfBranches / 180.0;
                var vector = End - Begin;   
                var leftVector = new Vector(
                    Math.Cos(radianAngle) * vector.X + Math.Sin(radianAngle) * vector.Y,
                    -Math.Sin(radianAngle) * vector.X + Math.Cos(radianAngle) * vector.Y
                    );
                var rightVector = new Vector(
                    Math.Cos(-radianAngle) * vector.X + Math.Sin(-radianAngle) * vector.Y,
                    -Math.Sin(-radianAngle) * vector.X + Math.Cos(-radianAngle) * vector.Y
                    );
                leftVector.Normalize();
                rightVector.Normalize();

                var leftPoint = new Point(End.X - leftVector.X * lengthOfBranches, End.Y - leftVector.Y* lengthOfBranches); 
                var rightPoint = new Point(End.X - rightVector.X * lengthOfBranches, End.Y - rightVector.Y* lengthOfBranches); 

                return new Point[] { leftPoint, rightPoint };
            }
        }
    }
}
