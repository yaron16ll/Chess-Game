using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    public class Pawn : Piece
    {
        public Pawn(string color) : base(color) { }

        public override bool IsValidMove(Point from, Point to, Piece[,] board)
        {
            int direction = Color == "White" ? 1 : -1; // White moves down, Black moves up
            int startRow = Color == "White" ? 1 : 6;   // Starting row for pawns

            // Moving forward by one square
            if (to.X == from.X + direction && to.Y == from.Y && board[to.X, to.Y] == null)
                return true;

            // Moving forward by two squares on the first move
            if (from.X == startRow && to.X == from.X + 2 * direction && to.Y == from.Y &&
                board[to.X, to.Y] == null && board[from.X + direction, from.Y] == null)
                return true;

            // Capturing diagonally
            if (to.X == from.X + direction && Math.Abs(to.Y - from.Y) == 1 &&
                board[to.X, to.Y] != null && board[to.X, to.Y].Color != Color)
                return true;

            // Moving horizontally (forward or backward) without eating
            if (to.X == from.X && Math.Abs(to.Y - from.Y) == 1 && board[to.X, to.Y] == null)
                return true;

            return false; // Invalid move
        }


    }
}
