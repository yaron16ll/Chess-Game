namespace client.forms
{
    partial class PawnPromotion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PawnPromotion));
            this.Knight = new System.Windows.Forms.PictureBox();
            this.Rook = new System.Windows.Forms.PictureBox();
            this.Bishop = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Knight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bishop)).BeginInit();
            this.SuspendLayout();
            // 
            // Knight
            // 
            this.Knight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Knight.Location = new System.Drawing.Point(47, 77);
            this.Knight.Name = "Knight";
            this.Knight.Size = new System.Drawing.Size(98, 92);
            this.Knight.TabIndex = 0;
            this.Knight.TabStop = false;
            this.Knight.Click += new System.EventHandler(this.Knight_Click);
            // 
            // Rook
            // 
            this.Rook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rook.Location = new System.Drawing.Point(195, 77);
            this.Rook.Name = "Rook";
            this.Rook.Size = new System.Drawing.Size(98, 92);
            this.Rook.TabIndex = 1;
            this.Rook.TabStop = false;
            this.Rook.Click += new System.EventHandler(this.Rook_Click);
            // 
            // Bishop
            // 
            this.Bishop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Bishop.Location = new System.Drawing.Point(337, 77);
            this.Bishop.Name = "Bishop";
            this.Bishop.Size = new System.Drawing.Size(98, 92);
            this.Bishop.TabIndex = 2;
            this.Bishop.TabStop = false;
            this.Bishop.Click += new System.EventHandler(this.Bishop_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("Bauhaus 93", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(97, 20);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(291, 34);
            this.Title.TabIndex = 3;
            this.Title.Text = "Promote Your Pawn";
            // 
            // PawnPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::client.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(493, 234);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Bishop);
            this.Controls.Add(this.Rook);
            this.Controls.Add(this.Knight);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PawnPromotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PawnPromotion";
            this.Load += new System.EventHandler(this.PawnPromotion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Knight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bishop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Knight;
        private System.Windows.Forms.PictureBox Rook;
        private System.Windows.Forms.PictureBox Bishop;
        private System.Windows.Forms.Label Title;
    }
}