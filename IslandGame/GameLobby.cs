using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandGame
{
    public partial class GameLobby : Form
    {
        private bool _isAdmin; // To show if the user is an Admin
        public GameLobby(bool isAdmin)
        {
            InitializeComponent();
            _isAdmin = isAdmin;
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdminConsole_Click(object sender, EventArgs e)
        {
            // Check if the admin console is accessed correctly
            if (_isAdmin)
            {
                Administration administration = new Administration();
                administration.ShowDialog(); // Show as modal dialog
            }
            else
            {
                MessageBox.Show("You do not have admin privileges.");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            this.Hide();
            mainScreen.Show();
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            Gameplay gameplay = new Gameplay();
            this.Hide();
            gameplay.Show();
        }

        private void lsbCurrentGames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GameLobby_Load(object sender, EventArgs e)
        {
            LoadDataIntoListBoxes();
            btnAdminConsole.Visible = _isAdmin;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            // Start a new game by opening the Gameplay form
            Gameplay gameplay = new Gameplay();
            gameplay.Show();
        }

        private void LoadDataIntoListBoxes()
        {
            // Clear the list boxes
            lsbPlayersOnline.Items.Clear();
            lsbCurrentGames.Items.Clear();

            // Load online players
            DataAccessPlayer dataAccessPlayer = new DataAccessPlayer();
            List<string> onlinePlayers = dataAccessPlayer.GetOnlinePlayers();
            foreach (var player in onlinePlayers)
            {
                lsbPlayersOnline.Items.Add(player);
            }

            // Load active games
            List<string> activeGames = dataAccessPlayer.GetActiveGames(); // Ensure this call is present
            foreach (var game in activeGames)
            {
                lsbCurrentGames.Items.Add(game);
            }
        }

    }
}
