using client.classes;
using client.Forms;
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
    public partial class Details : Form
    {

        private Player player; // Store the player object for use in the form

        // Custom constructor that accepts a Player object
        public Details(Player player)
        {
            InitializeComponent();
            this.player = player;
        }

        private void Details_Load(object sender, EventArgs e)
        {
            firstNameData.Text = player.FirstName;
            lastNameDate.Text = player.LastName;
            phoneNumberData.Text = player.PhoneNumber;
            countryData.Text = player.Country;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            int turnTime = int.Parse(turnCombo.SelectedItem.ToString());

            Game gameForm = new Game(turnTime, this.player);
            gameForm.Show();
            this.Hide();
        }

 

        private void Records_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Records(this.player).Show();
        }
    }
}
