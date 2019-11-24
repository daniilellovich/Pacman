using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public struct Point
    {
        public int X, Y;

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void Deconstruct(out int X, out int Y)
        {
            X = this.X;
            Y = this.Y;
        }

        public bool IsEmpty
            => (X == 0 || Y == 0);

        public override string ToString()
            => $"({X} ; {Y})";

        public static bool operator ==(Point left, Point right)
            => (left.X == right.X && left.Y == right.Y);

        public static bool operator !=(Point left, Point right)
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
        {
            this.X = X;
            this.Y = Y;
        }

        public void Decounstruct(out float X, out float Y)
        {
            X = this.X;
            Y = this.Y;
        }

        public Point ToPoint()
            => new Point(Convert.ToInt32(X), Convert.ToInt32(Y));

        public bool IsOnX(PointF point)
            => Math.Abs(X - point.X) < 0.08;

        public bool IsOnY(PointF point)
            => Math.Abs(Y - point.Y) < 0.08;

        public bool IsOn(PointF point)
            => IsOnY(point) && IsOnY(point);

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
