using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using client.models;
using client.classes;
using System.Collections.Generic;
using client.Models;

namespace client.forms
{
    public partial class Game : Form
    {
        private int turnTime, initialTime;
        private bool isPlayerOneTurn = false;
        private Random random = new Random();
        private const int PAINT_SIZE = 15;
        private const int BOARD_LENGTH = 8, BOARD_WIDTH = 4;
        private Piece[,] boardPieces = new Piece[BOARD_LENGTH, BOARD_WIDTH];
        private Bitmap boardBitmap;
        private Point? selectedSquare = null;
        private Point? threatenedKing = null; // Position of the threatened King
        private const int squareSize = 60; // Size of each square
        private int highlightIntensity = 0; // Intensity of the green highlight (0-255)
        private bool toggleRedHighlight = false; // Toggles red highlight
        private bool isPaintSymbolPressed = false;
        private int x = -20, y = -20, gameLength = 0;
        private Player player;
        private int gameID;

        public Game(int turnTime, Player player)
        {
            this.turnTime = turnTime;
            this.player = player;
            InitializeComponent();
        }


        private  void Game_Load(object sender, EventArgs e)
        {
            gameLengthTimer.Start();
            InitChessBoard();
            initialTime = this.turnTime;
            SetInitialPlayerTurn();
            UpdateTimerDisplay();
            turnTimer.Start();      
          //  await SetGameID(); // Ensure the GameID is set before proceeding

        }
 


        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            if (this.turnTime > 0)
            {
                if (!isPlayerOneTurn)
                {
                    PerformRandomMove("White");
                }

                this.turnTime--;
                UpdateTimerDisplay();
            }
            else
            {
                SwitchTurn();
            }
        }



        private void SwitchTurn()
        {
            turnTimer.Stop();
            EndTurn();
        }



        private async void PerformRandomMove(string playerColor)
        {
            var allMoves = GetAllLegalMoves(playerColor);
            if (allMoves.Count == 0)
            {
                MessageBox.Show($"{playerColor} has no legal moves!");
                return;
            }


            string relativePath = $"{HttpClientManager.Client.BaseAddress}api/PlayersTable/random-move";
            HttpResponseMessage response = await HttpClientManager.Client.PostAsJsonAsync(relativePath, allMoves.Count);

            if (response.IsSuccessStatusCode)
            {
                int result = await response.Content.ReadAsAsync<int>();
                var randomMove = allMoves[result];
                MovePiece(randomMove.from, randomMove.to);
                ChessBoard.Invalidate();
            }
            else
            {
                DeclareWinner("Black");
            }

        }



        private List<(Point from, Point to)> GetLegalMoves(Point piecePosition)
        {
            List<(Point from, Point to)> legalMoves = new List<(Point from, Point to)>();
            var piece = boardPieces[piecePosition.X, piecePosition.Y];

            if (piece == null)
                return legalMoves;

            for (int row = 0; row < BOARD_LENGTH; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    Point targetPosition = new Point(row, col);
                    if (IsValidMove(piecePosition, targetPosition))
                    {
                        legalMoves.Add((piecePosition, targetPosition));
                    }
                }
            }

            return legalMoves;
        }



        private List<(Point from, Point to)> GetAllLegalMoves(string playerColor)
        {
            List<(Point from, Point to)> allMoves = new List<(Point from, Point to)>();

            for (int row = 0; row < BOARD_LENGTH; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    var piece = boardPieces[row, col];
                    if (piece != null && piece.Color == playerColor)
                    {
                        var legalMoves = GetLegalMoves(new Point(row, col));
                        allMoves.AddRange(legalMoves);
                    }
                }
            }

            return allMoves;
        }



        private void SetInitialPlayerTurn()
        {
            int playerTurn = random.Next(0, 2);
            isPlayerOneTurn = playerTurn != 0;
        }



        private void UpdateTimerDisplay()
        {
            string timeFormatted = this.turnTime.ToString("D2");

            if (isPlayerOneTurn)
            {

                whiteTimer.Text = $"Time remaining 00:{timeFormatted}";
                blackTimer.Text = "Time remaining 00:00";
            }
            else
            {
                blackTimer.Text = $"Time remaining 00:{timeFormatted}";
                whiteTimer.Text = "Time remaining 00:00";
            }
        }



        private void InitChessBoard()
        {
            boardBitmap = new Bitmap(ChessBoard.Width, ChessBoard.Height);
            InitPieces();
        }



        private void MovePiece(Point from, Point to)
        {
            var movingPiece = boardPieces[from.X, from.Y];
            var targetPiece = boardPieces[to.X, to.Y];

            if (targetPiece != null && targetPiece.Color != movingPiece.Color)
            {
                boardPieces[to.X, to.Y] = null; // Remove the captured piece
            }

            // Move the piece
            boardPieces[to.X, to.Y] = movingPiece;
            boardPieces[from.X, from.Y] = null;

            // Check for pawn promotion
            if (movingPiece is Pawn pawn)
            {
                if ((pawn.Color == "White" && to.X == BOARD_LENGTH - 1) || (pawn.Color == "Black" && to.X == 0))
                {
                    PromotePawn(to);
                }
            }



            //AddMove(from, to, movingPiece);

            ChessBoard.Invalidate();
            SwitchTurn();
        }

 




        private void Game_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            CreateChessBoard(g);
            CreatePaintBrush(e);
        }



        private void CreatePaintBrush(PaintEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(boardBitmap))
            {
                g.FillEllipse(Brushes.Red, x, y, PAINT_SIZE, PAINT_SIZE);
            }

            e.Graphics.DrawImage(boardBitmap, 0, 0);
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



        private void DrawPiece(string resourceName, int x, int y, Graphics g)
        {
            var image = (Image)Properties.Resources.ResourceManager.GetObject(resourceName);
            if (image != null)
            {
                g.DrawImage(image, x, y, squareSize, squareSize);
            }
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



        private void ChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (isPlayerOneTurn && !isPaintSymbolPressed)
            {
                HandleChessBoard(e);
                CheckKingThreats();
            }
        }



        private void HandleChessBoard(MouseEventArgs e)
        {
            int col = e.X / squareSize;
            int row = e.Y / squareSize;

            // Check if the click is outside the board boundaries
            if (col < 0 || col >= BOARD_WIDTH || row < 0 || row >= BOARD_LENGTH)
                return;

            var selectedPiece = boardPieces[row, col];


            if (selectedSquare == null)
            {
                // First click: Select the piece
                if (selectedPiece != null && selectedPiece.Color == "Black")
                {
                    selectedSquare = new Point(row, col);
                    highlightIntensity = 0;
                    fadeInTimer.Start();
                }
            }
            else
            {
                // Second click: Attempt to move the piece
                Point targetSquare = new Point(row, col);

                if (IsValidMove(selectedSquare.Value, targetSquare))
                {
                    MovePiece(selectedSquare.Value, targetSquare);
                    selectedSquare = null;
                    fadeInTimer.Stop();
                }
                else
                {
                    // Invalid move: Clear selection
                    selectedSquare = null;
                    fadeInTimer.Stop();
                }

                ChessBoard.Invalidate();
            }
        }



        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if (highlightIntensity < 255)
            {
                highlightIntensity += 15; // Gradually increase intensity

                if (selectedSquare.HasValue)
                {
                    // Invalidate only the selected square
                    var selectedRect = new Rectangle(selectedSquare.Value.Y * squareSize, selectedSquare.Value.X * squareSize, squareSize, squareSize);
                    ChessBoard.Invalidate(selectedRect);
                }
            }
            else
            {
                fadeInTimer.Stop(); // Stop the timer when max intensity is reached
            }
        }



        private void RedToggleTimer_Tick(object sender, EventArgs e)
        {
            toggleRedHighlight = !toggleRedHighlight; // Toggle red highlight
            if (threatenedKing.HasValue)
            {
                var kingRect = new Rectangle(threatenedKing.Value.Y * squareSize, threatenedKing.Value.X * squareSize, squareSize, squareSize);
                ChessBoard.Invalidate(kingRect); // Redraw the threatened King's square
            }
        }



        private void CheckKingThreats()
        {
            Point? whiteKing = FindKing("White");
            Point? blackKing = FindKing("Black");

            if (whiteKing == null)
            {
                // White king is missing, Black wins
                DeclareWinner("Black");
                return;
            }

            if (blackKing == null)
            {
                // Black king is missing, White wins
                DeclareWinner("White");
                return;
            }

            // Highlight threatened kings
            if (IsKingThreatened("White"))
            {
                threatenedKing = whiteKing;
                redToggleTimer.Start();
            }
            else if (IsKingThreatened("Black"))
            {
                threatenedKing = blackKing;
                redToggleTimer.Start();
            }
            else
            {
                threatenedKing = null;
                redToggleTimer.Stop();
            }

            ChessBoard.Invalidate();
        }



        private void DeclareWinner(string winnerColor)
        {
            // Stop the game timer
            gameLengthTimer.Stop();
            turnTimer.Stop();
            redToggleTimer.Stop();
            AddGame();
            this.Hide();
            new Winner(winnerColor, this.player).Show();
        }


        private void AddGame()
        {
             AddGameToServerDB();
         }


        private async void AddGameToClientDB()
        {
            try
            {
                GamesTable payload = new GamesTable
                {
                    Length = this.gameLength,
                    StartDate = DateTime.Now,
                };

                using (var context = new MyDBEntities())
                {
                    context.GamesTable.Add(payload);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void AddGameToServerDB()
        {
            string relativePath = $"{HttpClientManager.Client.BaseAddress}api/GamesTable";

            var payload = new
            {
                Length = this.gameLength,
                StartDate = DateTime.Now,
                PlayerID = this.player.ID
            };

            try
            {
 
                HttpResponseMessage response = await HttpClientManager.Client.PostAsJsonAsync(relativePath, payload);
 
            }
            catch (Exception)
            {
                MessageBox.Show("The Game is not added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool IsValidMove(Point from, Point to)
        {
            if (to.X < 0 || to.X >= BOARD_LENGTH || to.Y < 0 || to.Y >= BOARD_WIDTH)
                return false;

            var movingPiece = boardPieces[from.X, from.Y];
            if (movingPiece == null)
                return false;

            // Check if the piece's movement is valid
            if (!movingPiece.IsValidMove(from, to, boardPieces))
                return false;

            var targetPiece = boardPieces[to.X, to.Y];
            if (targetPiece == null || targetPiece.Color != movingPiece.Color)
                return true;

            return false;
        }


        private bool IsKingThreatened(string kingColor)
        {
            Point? kingPosition = FindKing(kingColor);

            if (kingPosition == null)
                return false; // No King found (this shouldn't happen)

            // Check all pieces of the opposing color
            string opponentColor = kingColor == "White" ? "Black" : "White";

            for (int row = 0; row < BOARD_LENGTH; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    var piece = boardPieces[row, col];
                    if (piece != null && piece.Color == opponentColor)
                    {
                        if (piece.IsValidMove(new Point(row, col), kingPosition.Value, boardPieces))
                        {
                            return true; // King is threatened
                        }
                    }
                }
            }
            return false;
        }


        private Point? FindKing(string color)
        {
            for (int row = 0; row < BOARD_LENGTH; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    if (boardPieces[row, col] is King king && king.Color == color)
                    {
                        return new Point(row, col);
                    }
                }
            }
            return null;
        }


        private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPaintSymbolPressed && e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;

                // Trigger a redraw
                ChessBoard.Invalidate();
            }
        }


        private void PromotePawn(Point position)
        {
            // Ensure the piece being promoted is a pawn
            if (!(boardPieces[position.X, position.Y] is Pawn))
            {
                return;
            }

            string color = boardPieces[position.X, position.Y].Color;
            Piece promotedPawn = null;

            if (color == "Black")
            { // Show the PawnPromotion form as a modal dialog
                using (var promotionForm = new PawnPromotion(color, this))
                {
                    turnTimer.Enabled = false;

                    this.Hide();

                    if (promotionForm.ShowDialog() == DialogResult.OK)
                    {
                        int myChoice = promotionForm.PromotionChoice; // Get the choice from the form
                        promotedPawn = GetPromotedPawn(myChoice, color);
                    }
                }
            }
            else
            {
                int myChoice = random.Next(0, 3);
                promotedPawn = GetPromotedPawn(myChoice, color);

            }


            turnTimer.Enabled = true;

            // Replace the pawn with the promoted piece
            boardPieces[position.X, position.Y] = promotedPawn;
            ChessBoard.Invalidate();
        }


        private Piece GetPromotedPawn(int myChoice, String color)
        {
            Piece promotedPiece = null;

            switch (myChoice)
            {
                case 0:
                    promotedPiece = new Knight(color);
                    break;
                case 1:
                    promotedPiece = new Rook(color);
                    break;
                case 2:
                    promotedPiece = new Bishop(color);
                    break;
                default:
                    throw new InvalidOperationException("Invalid promotion choice");
            }

            return promotedPiece;
        }


        private void gameLengthTimer_Tick(object sender, EventArgs e)
        {
            this.gameLength++;
        }


        private void PaintSymbol_Click(object sender, EventArgs e)
        {

            isPaintSymbolPressed = !isPaintSymbolPressed;

            if (isPaintSymbolPressed == false)
            {
                ClearPaint();
            }
            turnTimer.Enabled = !turnTimer.Enabled;

        }


        private void ClearPaint()
        {
            // Clear the board bitmap
            using (Graphics g = Graphics.FromImage(boardBitmap))
            {
                g.Clear(Color.Transparent);
                x = y = -20;

            }

            // Redraw the chessboard
            ChessBoard.Invalidate();
        }


        private void EndTurn()
        {
            this.turnTime = initialTime;
            isPlayerOneTurn = !isPlayerOneTurn;
            UpdateTimerDisplay();
            turnTimer.Start();
        }

    }
}
