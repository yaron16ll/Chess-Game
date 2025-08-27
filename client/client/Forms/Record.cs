using client.classes;
using client.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client.Forms
{
    public partial class Record : Form
    {
        private const int BOARD_LENGTH = 8, BOARD_WIDTH = 4;
        private Piece[,] boardPieces = new Piece[BOARD_LENGTH, BOARD_WIDTH];
        private Bitmap boardBitmap;
        private Point? selectedSquare = null;
        private Point? threatenedKing = null; // Position of the threatened King
        private const int squareSize = 60; // Size of each square
        private int highlightIntensity = 0; // Intensity of the green highlight (0-255)
        private bool toggleRedHighlight = false; // Toggles red highlight
        private Player player;

        public Record(Player player)
        {
            InitializeComponent();
            this.player = player;
        }

        private void Record_Load(object sender, EventArgs e)
        {
            InitChessBoard();
 
        }

        private void DrawPiece(string resourceName, int x, int y, Graphics g)
        {
            var image = (Image)Properties.Resources.ResourceManager.GetObject(resourceName);
            if (image != null)
            {
                g.DrawImage(image, x, y, squareSize, squareSize);
            }
        }


        private void InitChessBoard()
        {
            boardBitmap = new Bitmap(ChessBoard.Width, ChessBoard.Height);
            InitPieces();
        }

        private void ChessBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CreateChessBoard(g);
        }

        private void InitPieces()
        {
            // White pieces
            boardPieces[0, 0] = new King("White");
            boardPieces[0, 1] = new Bishop("White");
            boardPieces[0, 2] = new Knight("White");
            boardPieces[0, 3] = new Rook("White");

            // Black pieces
            boardPieces[7, 0] = new King("Black");
            boardPieces[7, 1] = new Bishop("Black");
            boardPieces[7, 2] = new Knight("Black");
            boardPieces[7, 3] = new Rook("Black");

            // Pawn pieces
            for (int col = 0; col < BOARD_WIDTH; col++)
            {
                boardPieces[1, col] = new Pawn("White");
                boardPieces[6, col] = new Pawn("Black");
            }
        }

        private void Right_Click(object sender, EventArgs e)
        {

        }

        private void Left_Click(object sender, EventArgs e)
        {

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new Records(this.player).Show();

        }

        private void CreateChessBoard(Graphics g)
        {

            for (int row = 0; row < BOARD_LENGTH; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    // Determine the square's base color
                    Color squareColor = (row + col) % 2 == 0 ? Color.Wheat : Color.Black;

                    // Highlight the King's square if threatened and toggling is active
                    if (threatenedKing.HasValue && threatenedKing.Value.X == row && threatenedKing.Value.Y == col)
                    {
                        if (toggleRedHighlight)
                        {
                            squareColor = Color.Red; // Red highlight
                        }
                    }

                    // Highlight selected square with a fade-in effect
                    if (selectedSquare.HasValue && selectedSquare.Value.X == row && selectedSquare.Value.Y == col)
                    {
                        squareColor = Color.FromArgb(highlightIntensity, 144, 238, 144); // Gradual light green
                    }

                    // Draw the square
                    using (Brush squareBrush = new SolidBrush(squareColor))
                    {
                        g.FillRectangle(squareBrush, col * squareSize, row * squareSize, squareSize, squareSize);
                    }

                    // Draw the piece (if any)
                    if (boardPieces[row, col] != null)
                    {
                        string pieceImage = $"{boardPieces[row, col].Color}{boardPieces[row, col].GetType().Name}";
                        DrawPiece(pieceImage, col * squareSize, row * squareSize, g);
                    }
                }
            }

        }
    }
}
