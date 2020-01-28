using System;
using System.Collections.Generic;

namespace Pacman
{
    public struct Point
    {
        public int X, Y;

        public Point(int X, int Y)
        { this.X = X; this.Y = Y; }            

        public void Deconstruct(out int X, out int Y)
        { X = this.X; Y = this.Y; }

        public Point Up => new Point(X, Y - 1);

        public Point Down => new Point(X, Y + 1);

        public Point Right => new Point(X + 1, Y);

        public Point Left => new Point(X - 1, Y);

        public List<Point> NeighbourPoints 
            => new List<Point> { Up, Down, Right, Left };

        public override string ToString()
            => $"({X} ; {Y})";

        public static bool operator == (Point left, Point right)
            => (left.X == right.X && left.Y == right.Y);

        public static bool operator != (Point left, Point right)
            => !(left == right);

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;
            Point point = (Point)obj;
            if (point.X == X)
                return point.Y == Y;
            return false;
        }

        public override int GetHashCode()
            =>  X ^ Y;

        public static implicit operator PointF(Point p)       
            => new PointF((float)p.X, (float)p.Y);
    }

    public struct PointF
    {
        public float X, Y;

        public PointF(float X, float Y)
        { this.X = X; this.Y = Y; }

        public void Decounstruct(out float X, out float Y)
        { X = this.X; Y = this.Y; }

        public Point ToPoint()
            => new Point(Convert.ToInt32(X), Convert.ToInt32(Y));

        public bool IsOnX(PointF point, float ebsilon)
            => Math.Abs(X - point.X) < ebsilon;

        public bool IsOnY(PointF point, float ebsilon)
            => Math.Abs(Y - point.Y) < ebsilon;

        public bool IsOnXandY(PointF point, float ebsilon)
            => IsOnX(point, ebsilon) && IsOnY(point, ebsilon);

        public bool IsFarFrom(Point point, float ebsilon)
            => (Math.Abs(X - point.X) > ebsilon || Math.Abs(Y - point.Y) > ebsilon);

        public override string ToString()
            => $"({string.Format("{0:N2}", X)} ; {string.Format("{0:N2}", Y)})";

        public bool IsEmpty
            => (X == 0 || Y == 0);

        public static bool operator == (PointF left, PointF right)
            => (left.X == right.X && left.Y == right.Y);

        public static bool operator != (PointF left, PointF right)
            => !(left == right);

        public override bool Equals(object obj)
        {
            if (!(obj is PointF))
                return false;
            PointF pointF = (PointF)obj;
            if (pointF.X == X && pointF.Y == Y)
                return pointF.GetType().Equals(GetType());
            return false;
        }

        public override int GetHashCode()
            => base.GetHashCode();
    }
}
