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
    public partial class Administration : Form
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void lblPlayersOnline_Click(object sender, EventArgs e)
        {

        }

        private void btnAdminConsole_Click(object sender, EventArgs e)
        {
            
            this.Close();
            GameLobby gameLobby = new GameLobby(true); 
            gameLobby.Show(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            this.Hide();
            mainScreen.Show();
        }
    }
}
