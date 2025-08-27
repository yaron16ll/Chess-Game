namespace client.forms
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.blackTimer = new System.Windows.Forms.Label();
            this.whiteTimer = new System.Windows.Forms.Label();
            this.turnTimer = new System.Windows.Forms.Timer(this.components);
            this.fadeInTimer = new System.Windows.Forms.Timer(this.components);
            this.redToggleTimer = new System.Windows.Forms.Timer(this.components);
            this.PaintSymbol = new System.Windows.Forms.PictureBox();
            this.ChessBoard = new DoubleBufferedPanel();
            this.gameLengthTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PaintSymbol)).BeginInit();
            this.SuspendLayout();
            // 
            // blackTimer
            // 
            this.blackTimer.AutoSize = true;
            this.blackTimer.BackColor = System.Drawing.Color.Transparent;
            this.blackTimer.Font = new System.Drawing.Font("Microsoft PhagsPa", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackTimer.ForeColor = System.Drawing.Color.Black;
            this.blackTimer.Location = new System.Drawing.Point(24, 24);
            this.blackTimer.Name = "blackTimer";
            this.blackTimer.Size = new System.Drawing.Size(59, 22);
            this.blackTimer.TabIndex = 22;
            this.blackTimer.Text = "label1";
            // 
            // whiteTimer
            // 
            this.whiteTimer.AutoSize = true;
            this.whiteTimer.BackColor = System.Drawing.Color.Transparent;
            this.whiteTimer.Font = new System.Drawing.Font("Microsoft PhagsPa", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteTimer.ForeColor = System.Drawing.Color.Black;
            this.whiteTimer.Location = new System.Drawing.Point(12, 678);
            this.whiteTimer.Name = "whiteTimer";
            this.whiteTimer.Size = new System.Drawing.Size(59, 22);
            this.whiteTimer.TabIndex = 23;
            this.whiteTimer.Text = "label2";
            // 
            // turnTimer
            // 
            this.turnTimer.Interval = 1000;
            this.turnTimer.Tick += new System.EventHandler(this.TurnTimer_Tick);
            // 
            // fadeInTimer
            // 
            this.fadeInTimer.Interval = 50;
            this.fadeInTimer.Tick += new System.EventHandler(this.FadeInTimer_Tick);
            // 
            // redToggleTimer
            // 
            this.redToggleTimer.Tick += new System.EventHandler(this.RedToggleTimer_Tick);
            // 
            // PaintSymbol
            // 
            this.PaintSymbol.BackColor = System.Drawing.Color.Transparent;
            this.PaintSymbol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PaintSymbol.Image = ((System.Drawing.Image)(resources.GetObject("PaintSymbol.Image")));
            this.PaintSymbol.Location = new System.Drawing.Point(410, 642);
            this.PaintSymbol.Name = "PaintSymbol";
            this.PaintSymbol.Size = new System.Drawing.Size(71, 67);
            this.PaintSymbol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PaintSymbol.TabIndex = 0;
            this.PaintSymbol.TabStop = false;
            this.PaintSymbol.Click += new System.EventHandler(this.PaintSymbol_Click);
            // 
            // ChessBoard
            // 
            this.ChessBoard.BackColor = System.Drawing.Color.Transparent;
            this.ChessBoard.Location = new System.Drawing.Point(84, 65);
            this.ChessBoard.Name = "ChessBoard";
            this.ChessBoard.Size = new System.Drawing.Size(320, 592);
            this.ChessBoard.TabIndex = 25;
            this.ChessBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.Game_Paint);
            this.ChessBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseClick);
            this.ChessBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseMove);
            // 
            // gameLengthTimer
            // 
            this.gameLengthTimer.Enabled = true;
            this.gameLengthTimer.Tick += new System.EventHandler(this.gameLengthTimer_Tick);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::client.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(493, 734);
            this.Controls.Add(this.PaintSymbol);
            this.Controls.Add(this.ChessBoard);
            this.Controls.Add(this.whiteTimer);
            this.Controls.Add(this.blackTimer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PaintSymbol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label blackTimer;
        private System.Windows.Forms.Label whiteTimer;
        private System.Windows.Forms.Timer turnTimer;
        private System.Windows.Forms.Timer fadeInTimer;
        private DoubleBufferedPanel ChessBoard;
        private System.Windows.Forms.Timer redToggleTimer;
        private System.Windows.Forms.PictureBox PaintSymbol;
        private System.Windows.Forms.Timer gameLengthTimer;
    }
}