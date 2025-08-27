using client.models;
using System.Windows.Forms;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using client.classes;
using Newtonsoft.Json;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using client.forms;

namespace client
{
    public partial class Login : Form
    {
        private LoginDetails loginDetails;
        private Player player;

        public Login()
        {
            InitializeComponent();

        }


        private void Login_Load(object sender, EventArgs e)
        {
             // Initialize the data source
            loginDetails = new LoginDetails { ID = "", Password = "" };

            // Bind the username TextBox to the ID property with two-way binding
            usernameBox.DataBindings.Add("Text", loginDetails, "ID", true, DataSourceUpdateMode.OnPropertyChanged);

            // Bind the password TextBox to the Password property with two-way binding
            passwordBox.DataBindings.Add("Text", loginDetails, "Password", true, DataSourceUpdateMode.OnPropertyChanged);


        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {

            if (errorTxt1.Visible == false && errorTxt2.Visible == false)
            {
                string relativePath = $"{HttpClientManager.Client.BaseAddress}api/PlayersTable";
                IEnumerable<Player> players = await GetPlayersAsync(relativePath);

                if (IsPlayerFound(players))
                {
                    Details detailsForm = new Details(player);
                    detailsForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The username or password is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("The username or password you entered is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
 

        private bool IsPlayerFound(IEnumerable<Player> players)
        {

            foreach (var player in players)
            {
                if (loginDetails.ID.Equals(player.ID) && loginDetails.Password.Equals(player.Password))
                {
                    this.player = player;
                    return true;
                }
            }
            return false;

        }



        async Task<IEnumerable<Player>> GetPlayersAsync(string relativePath)
        {
            IEnumerable<Player> players = null;
            
                HttpResponseMessage response = await HttpClientManager.Client.GetAsync(relativePath);
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<IEnumerable<Player>>();
            }
            else {
                MessageBox.Show("The players are not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return players;
        }



        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^(1000|[1-9][0-9]{0,2})$";

            // Check if the TextBox is empty or contains only whitespace
            if (string.IsNullOrWhiteSpace(usernameBox.Text))
            {
                errorTxt1.Text = "This field cannot be empty.";
                errorTxt1.Visible = true; // Show label if TextBox is empty or whitespace
            }
            else if (Regex.IsMatch(usernameBox.Text, pattern))
            {
                // Hide the error label if the pattern matches
                errorTxt1.Visible = false;
            }
            else
            {
                // Show error message if pattern does not match
                errorTxt1.Text = "ID must be 1 - 1000.";
                errorTxt1.Visible = true;
            }
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^[A-Za-z]{3}\d{3}$";

            // Check if the TextBox is empty or contains only whitespace
            if (string.IsNullOrWhiteSpace(passwordBox.Text))
            {
                errorTxt2.Text = "This field cannot be empty.";
                errorTxt2.Visible = true; // Show label if TextBox is empty or whitespace
            }
            else if (Regex.IsMatch(passwordBox.Text, pattern))
            {
                // Hide the error label if the pattern matches
                errorTxt2.Visible = false;
            }
            else
            {
                // Show error message if pattern does not match
                errorTxt2.Text = "Password must be 3 letters followed by 3 digits.";
                errorTxt2.Visible = true;
            }
        }

    }
}
