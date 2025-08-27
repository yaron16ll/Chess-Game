using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    public class Knight : Piece
    {
        public Knight(string color) : base(color) { }

        public override bool IsValidMove(Point from, Point to, Piece[,] board)
        {
            int dx = Math.Abs(to.X - from.X);
            int dy = Math.Abs(to.Y - from.Y);

            // Check for L-shaped moves
            return (dx == 2 && dy == 1) || (dx == 1 && dy == 2);
        }
    }

}
