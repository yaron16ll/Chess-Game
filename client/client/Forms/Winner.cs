using client.classes;
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
    public partial class Winner : Form
    {
        String color;
        Player player;
        public Winner(String color, Player player)
        {
            this.player = player;   
            this.color = color;
            InitializeComponent();
        }


        private void Winner_Load(object sender, EventArgs e)
        {
            if (color != null)
            {
                if (this.color == "White")
                    whiteStar.Visible = true;
                else
                    blackStar.Visible = true;

            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Details(this.player).Show();
        }

        
    }
}
