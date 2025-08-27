namespace client.Forms
{
    partial class Record
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Record));
            this.ChessBoard = new DoubleBufferedPanel();
            this.left = new System.Windows.Forms.PictureBox();
            this.right = new System.Windows.Forms.PictureBox();
            this.backBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.right)).BeginInit();
            this.SuspendLayout();
            // 
            // ChessBoard
            // 
            this.ChessBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ChessBoard.BackColor = System.Drawing.Color.Gainsboro;
            this.ChessBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ChessBoard.Location = new System.Drawing.Point(64, 20);
            this.ChessBoard.Name = "ChessBoard";
            this.ChessBoard.Size = new System.Drawing.Size(319, 591);
            this.ChessBoard.TabIndex = 26;
            this.ChessBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessBoard_Paint);
            // 
            // left
            // 
            this.left.BackColor = System.Drawing.Color.Transparent;
            this.left.BackgroundImage = global::client.Properties.Resources.leftArrow;
            this.left.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.left.Location = new System.Drawing.Point(110, 634);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(96, 35);
            this.left.TabIndex = 27;
            this.left.TabStop = false;
            this.left.Click += new System.EventHandler(this.Left_Click);
            // 
            // right
            // 
            this.right.BackColor = System.Drawing.Color.Transparent;
            this.right.BackgroundImage = global::client.Properties.Resources.rightArrow;
            this.right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.right.Location = new System.Drawing.Point(240, 634);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(96, 35);
            this.right.TabIndex = 28;
            this.right.TabStop = false;
            this.right.Click += new System.EventHandler(this.Right_Click);
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.SystemColors.Desktop;
            this.backBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.backBtn.FlatAppearance.BorderSize = 0;
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backBtn.ImageKey = "(none)";
            this.backBtn.Location = new System.Drawing.Point(12, 699);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(98, 34);
            this.backBtn.TabIndex = 29;
            this.backBtn.Text = "BACK";
            this.backBtn.UseVisualStyleBackColor = false;
            this.backBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImage = global::client.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(456, 745);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.ChessBoard);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Record";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Record";
            this.Load += new System.EventHandler(this.Record_Load);
            ((System.ComponentModel.ISupportInitialize)(this.left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.right)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBufferedPanel ChessBoard;
        private System.Windows.Forms.PictureBox left;
        private System.Windows.Forms.PictureBox right;
        private System.Windows.Forms.Button backBtn;
    }
}