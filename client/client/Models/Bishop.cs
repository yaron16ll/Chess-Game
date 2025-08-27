using client.models;
using System.Drawing;
using System;

public class Bishop : Piece
{
    public Bishop(string color) : base(color) { }

    public override bool IsValidMove(Point from, Point to, Piece[,] board)
    {
        // Calculate the difference in x and y
        int dx = Math.Abs(to.X - from.X);
        int dy = Math.Abs(to.Y - from.Y);

        // Check if the move is diagonal
        if (dx == dy)
        {
            int xStep = to.X > from.X ? 1 : -1; // Determine step direction for X
            int yStep = to.Y > from.Y ? 1 : -1; // Determine step direction for Y

            int x = from.X + xStep;
            int y = from.Y + yStep;

            // Check for obstructions along the path
            while (x != to.X && y != to.Y)
            {
                // Check bounds before accessing the board array
                if (x < 0 || x >= board.GetLength(0) || y < 0 || y >= board.GetLength(1))
                    return false; // Out of bounds

                if (board[x, y] != null)
                    return false; // Path is blocked

                x += xStep;
                y += yStep;
            }

            // Final check: Ensure the target square is within bounds
            if (to.X < 0 || to.X >= board.GetLength(0) || to.Y < 0 || to.Y >= board.GetLength(1))
                return false;

            // The path is clear
            return true;
        }

        return false; // Not a diagonal move
    }

}