using client.classes;
using client.forms;
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
    public partial class Records : Form
    {
        private Player player;
        private MyDBEntities db = new MyDBEntities();

        public Records(Player player)
        {
            this.player = player;
            InitializeComponent();
        }

        private async void Records_Load(object sender, EventArgs e)
        {
            MovesTable move = new MovesTable
            {
                GameId = 1, // Replace with a valid GameId
                FromX = 0,
                FromY = 1,
                ToX = 0,
                ToY = 2,
                PieceType = "Pawn"
            };

            using (var context = new MyDBEntities())
            {
                context.MovesTable.Add(move);
                await context.SaveChangesAsync();
             }
            try
            {
                // Project only the required properties from GamesTable
                var games = db.GamesTable.Select(g => new
                {
                    g.Id,
                    g.Length,
                    g.StartDate
                })
               .ToList();


                gamesBindingSource.DataSource = games;
                gamesDataGridView.DataSource = gamesBindingSource;
                gamesBindingNavigator.BindingSource = gamesBindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading games: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GamesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Hide();
            new Record(this.player).Show();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Details(this.player).Show();
        }
    }
}
