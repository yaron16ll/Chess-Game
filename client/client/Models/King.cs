using client.models;
using System.Drawing;
using System;

public class King : Piece
{
    public King(string color) : base(color) { }

    public override bool IsValidMove(Point from, Point to, Piece[,] board)
    {
        int dx = Math.Abs(to.X - from.X);
        int dy = Math.Abs(to.Y - from.Y);

        // King moves one square in any direction
        return dx <= 1 && dy <= 1;
    }
}
