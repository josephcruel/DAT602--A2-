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
        public GameLobby()
        {
            InitializeComponent();
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
            DataAccess dataAccess = new DataAccess();
            string result = dataAccess.AdminConsole();
            MessageBox.Show(result);

            Administration administration = new Administration();
            this.Hide();
            administration.Show();
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
    }
}
