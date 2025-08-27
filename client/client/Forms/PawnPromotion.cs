using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client.forms
{
    public partial class PawnPromotion : Form
    {
        private String color;
        private Game game;
        public int PromotionChoice { get; set; }

        public PawnPromotion(String color,Game game)
        {
            this.game = game;   
            this.color = color;
            InitializeComponent();
        }

        private void PawnPromotion_Load(object sender, EventArgs e)
        {
            if (color != null)
            {
                if (color == "White")
                {
                    Rook.Image = (Image)Properties.Resources.ResourceManager.GetObject("WhiteRook");
                    Rook.SizeMode = PictureBoxSizeMode.StretchImage;

                    Bishop.Image = (Image)Properties.Resources.ResourceManager.GetObject("WhiteBishop");
                    Bishop.SizeMode = PictureBoxSizeMode.StretchImage;

                    Knight.Image = (Image)Properties.Resources.ResourceManager.GetObject("WhiteKnight");
                    Knight.SizeMode = PictureBoxSizeMode.StretchImage;

                }
                else
                {
                    Rook.Image = (Image)Properties.Resources.ResourceManager.GetObject("BlackRook");
                    Rook.SizeMode = PictureBoxSizeMode.StretchImage;

                    Bishop.Image = (Image)Properties.Resources.ResourceManager.GetObject("BlackBishop");
                    Bishop.SizeMode = PictureBoxSizeMode.StretchImage;

                    Knight.Image = (Image)Properties.Resources.ResourceManager.GetObject("BlackKnight");
                    Knight.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void Knight_Click(object sender, EventArgs e)
        {
            this.Close();
            this.PromotionChoice = 0;
            this.DialogResult = DialogResult.OK;
            this.game.Show();
        }

        private void Rook_Click(object sender, EventArgs e)
        {
            this.Close();
            this.PromotionChoice = 1;
            this.DialogResult = DialogResult.OK;
            this.game.Show();
        }

        private void Bishop_Click(object sender, EventArgs e)
        {
            this.Close();
            this.PromotionChoice = 2;
            this.DialogResult = DialogResult.OK;
            this.game.Show();
        }
    }
}
