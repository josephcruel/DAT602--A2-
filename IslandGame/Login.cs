using System;
using System.Windows.Forms;

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
            DataAccessLogin islandgamedb = new DataAccessLogin();

            // Call the Login method to validate the user
            string loginResult = islandgamedb.Login(lcName, lcPassword);

            // Check the result of the login
            if (loginResult == "Logged In as Admin")
            {
                // Admin login successful, show the admin GameLobby
                MessageBox.Show("Admin login successful");
                GameLobby gameLobby = new GameLobby(true); // Pass 'true' for admin user
                this.Hide();
                gameLobby.Show();
            }
            else if (loginResult == "Logged In")
            {
                // Regular user login successful
                MessageBox.Show("User login successful");
                GameLobby gameLobby = new GameLobby(false); // Pass 'false' for regular user
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

        private void Login_Load(object sender, EventArgs e)
        {
            DataAccessLogin dataAccessLogin = new DataAccessLogin();
            string result = dataAccessLogin.LoginConnection();
            MessageBox.Show(result);
        }
    }
}
