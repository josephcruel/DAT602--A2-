using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IslandGame
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnBackButton_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            this.Hide();
            mainScreen.Show();

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            // Get the user input from the textboxes
            string lcName = txtUsername.Text;
            string lcPassword = txtPassword.Text;

            // Check if the username or password is empty
            if (string.IsNullOrWhiteSpace(lcName) || string.IsNullOrWhiteSpace(lcPassword))
            {
                MessageBox.Show("Username and Password cannot be empty.");
                return; // If inputs are empty, stop
            }

            // Access the database
            DataAccess islandgamedb = new DataAccess();

            // Call the Login method to validate the user
            string loginResult = islandgamedb.Login(lcName, lcPassword);

            // Check the result of the login
            if (loginResult == "Logged In")
            {
                // Show the result
                MessageBox.Show(loginResult);
                // If login is successful, show the GameLobby
                GameLobby gameLobby = new GameLobby();
                this.Hide();
                gameLobby.Show();
            }
            else
            {
                // If login fails, show the error message 
                MessageBox.Show(loginResult);
            }
        }
    
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
