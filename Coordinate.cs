using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Coordinate operator +(Coordinate left, Coordinate right) 
            => new Coordinate(left.X + right.X, left.Y + right.Y);
        public static Coordinate operator -(Coordinate left, Coordinate right) 
            => new Coordinate(left.X - right.X, left.Y - right.Y);
    }
}
