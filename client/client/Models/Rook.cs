using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    public class Rook : Piece
    {
        public Rook(string color) : base(color) { }

        public override bool IsValidMove(Point from, Point to, Piece[,] board)
        {
            // Rook moves in straight lines
            if (from.X == to.X || from.Y == to.Y)
            {
                // Check if the path is clear
                int xStep = from.X == to.X ? 0 : (to.X > from.X ? 1 : -1);
                int yStep = from.Y == to.Y ? 0 : (to.Y > from.Y ? 1 : -1);

                int x = from.X + xStep, y = from.Y + yStep;
                while (x != to.X || y != to.Y)
                {
                    if (board[x, y] != null)
                        return false; // Path is blocked

                    x += xStep;
                    y += yStep;
                }

                return true;
            }

            return false;
        }
    }
}
