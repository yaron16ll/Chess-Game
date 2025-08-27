using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    public abstract class Piece
    {
        public string Color { get; set; } // "White" or "Black"

        public Piece(string color)
        {
            Color = color;
        }

        public abstract bool IsValidMove(Point from, Point to, Piece[,] board);
    }
}
