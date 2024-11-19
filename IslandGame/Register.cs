using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandGame
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnBackButton_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            this.Hide();
            mainScreen.Show();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            DataAccessRegister dataAccessRegister = new DataAccessRegister();
            string result = dataAccessRegister.RegisterConnection();
            MessageBox.Show(result);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Get the user input from the textboxes
            string lcEmail = txtEmail.Text; 
            string lcName = txtUsername.Text; 
            string lcPassword = txtPassword.Text; 

            // Check if the email, username, or password is empty
            if (string.IsNullOrWhiteSpace(lcEmail) || string.IsNullOrWhiteSpace(lcName) || string.IsNullOrWhiteSpace(lcPassword))
            {
                MessageBox.Show("Fields cannot be empty.");
                return; // Stop further execution if inputs are empty
            }

            // Access the database to register the user
            DataAccessRegister dataAccessRegister = new DataAccessRegister();

            string result = dataAccessRegister.RegisterUser(lcEmail, lcName, lcPassword);

            // Check the result of the registration
            if (result == "ADDED USER NAME")
            {
                MessageBox.Show("Registration successful!");
                MainScreen mainScreen = new MainScreen();
                this.Hide();
                mainScreen.Show();
            }
            else
            {
                // Show the error message if registration fails
                MessageBox.Show(result);
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
