using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandGame
{
    public partial class Gameplay : Form
    {
        private const int GridSize = 10; // Grid dimensions (10x10)
        private const int CellSize = 30; // Cell size in pixels
        private const int GridOffsetX = 20;  // Horizontal offset from the left edge of the form
        private const int GridOffsetY = 200; // Vertical offset from the top of the form
        private Button[,] gridButtons;
        private int playerID;
        private int gameID = 1; // Assume gameID is 1 
        private Player player; // Player object to track position
        private Dictionary<(int, int), string> tileTypes = new Dictionary<(int, int), string>();

        public Gameplay()
        {
            InitializeComponent();
            InitializeGameGrid();
            LoadGameBoard(); // Load tiles from SQL

            // Retrieve player position and set in `tileTypes`
            DataAccessGame dataAccessGame = new DataAccessGame();
            DataAccessPlayer dataAccessPlayer = new DataAccessPlayer();

            var (startRow, startCol) = dataAccessPlayer.GetPlayerStartPosition(playerID);
            player = new Player(playerID, "PlayerName", 100, startRow, startCol);
            tileTypes[(startRow, startCol)] = "player";

            HighlightAllObjects();
        }



        // Method to load the initial state of the game board
        private void LoadGameBoard()
        {
            // Initialize DataAccessGame class to interact with the database
            DataAccessGame dataAccessGame = new DataAccessGame();

            // Retrieve the game grid data from the database
            DataTable gameGrid = dataAccessGame.GetGameGrid(gameID);

            // Check if gameGrid data is not null
            if (gameGrid != null)
            {
                // Clear existing tile types in case we are reloading
                tileTypes.Clear();

                // Populate tileTypes dictionary with database data
                foreach (DataRow row in gameGrid.Rows)
                {
                    int gridRow = Convert.ToInt32(row["Row"]);
                    int gridCol = Convert.ToInt32(row["Col"]);
                    string tileType = row["TileType"].ToString();

                    // Store the tile type in the dictionary for easy reference
                    tileTypes[(gridRow, gridCol)] = tileType;

                    Console.WriteLine($"Loaded tile at Row {gridRow}, Col {gridCol} with type {tileType}"); // Debug info
                }

                // Highlight objects on the grid according to the loaded data
                HighlightAllObjects();
            }
            else
            {
                Console.WriteLine("Failed to load game grid from database.");
            }
        }

        // Method to create the game grid
        private void InitializeGameGrid()
        {
            gridButtons = new Button[GridSize, GridSize];
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    // Create a new button with the adjusted position
                    Button gridButton = new Button
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(GridOffsetX + col * CellSize, GridOffsetY + row * CellSize), // Apply offsets here
                        Text = "", // Optionally set initial text
                        Tag = (row, col) // Store coordinates in the tag
                    };

                    // Add a click event handler for the button
                    gridButton.Click += GridButton_Click;

                    // Add the button to the form and 2D array
                    this.Controls.Add(gridButton);
                    gridButtons[row, col] = gridButton;
                }
            }
        }

        // Method to highlight objects based on tile types
        private void HighlightAllObjects()
        {
            // Reset all buttons to default color
            foreach (var button in gridButtons)
            {
                button.BackColor = SystemColors.Control;
            }

            // Set colors based on object type in tileTypes dictionary
            foreach (var tile in tileTypes)
            {
                var (row, col) = tile.Key;  // Access row and col from the dictionary key
                string tileType = tile.Value; // Access tile type from the dictionary value

                Button gridButton = gridButtons[row, col];

                // Set the color based on tile type
                switch (tileType)
                {
                    case "player":
                        gridButton.BackColor = Color.Blue; // Player color
                        break;
                    case "enemy":
                        gridButton.BackColor = Color.Red; // Enemy color
                        break;
                    case "chest":
                        gridButton.BackColor = Color.Gold; // Chest color
                        break;
                    case "item":
                        gridButton.BackColor = Color.Green; // Item color
                        break;
                    default:
                        gridButton.BackColor = SystemColors.Control; // Empty tile color
                        break;
                }
                Console.WriteLine($"Tile at Row {row}, Col {col} is of type {tileType}"); // Log color info
            }
        }



        private string GetTileType(int row, int col)
        {
            // Look up the tile type in the dictionary. If it doesn't exist, default to "empty"
            return tileTypes.ContainsKey((row, col)) ? tileTypes[(row, col)] : "empty";
        }


        private bool BattleEnemy(int row, int col)
        {
            // Basic example of a battle outcome
            Random rng = new Random();
            bool defeated = rng.Next(0, 2) == 1; // 50% chance of defeating enemy

            if (defeated)
            {
                // Update database to mark enemy as defeated (optional)
                DataAccessPlayer dataAccessPlayer = new DataAccessPlayer();
                dataAccessPlayer.UpdateTileType(gameID, row, col, "empty");
            }

            return defeated;
        }

        private void CollectItem(int row, int col)
        {
            gridButtons[row, col].BackColor = SystemColors.Control;
            tileTypes[(row, col)] = "empty"; // Update the tile type in the dictionary

            // Optionally, update the database to mark the item as collected
            DataAccessPlayer dataAccessPlayer = new DataAccessPlayer();
            dataAccessPlayer.UpdateTileType(gameID, row, col, "empty");
        }

        private void RemoveEnemyFromGrid(int row, int col)
        {
            gridButtons[row, col].BackColor = SystemColors.Control; // Set to default color
            tileTypes[(row, col)] = "empty"; // Update the tile type in the dictionary

            // Optional: Update the database to mark the enemy as removed
            DataAccessPlayer dataAccessPlayer = new DataAccessPlayer();
            dataAccessPlayer.UpdateTileType(gameID, row, col, "empty");
        }

        private void CollectChest(int row, int col)
        {
            gridButtons[row, col].BackColor = SystemColors.Control;
            tileTypes[(row, col)] = "empty"; // Update the tile type in the dictionary

            // Optionally, update the database to mark the chest as collected
            DataAccessPlayer dataAccessPlayer = new DataAccessPlayer();
            dataAccessPlayer.UpdateTileType(gameID, row, col, "empty");
        }



        // Event handler for button clicks
        private void GridButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            var (row, col) = ((int, int))clickedButton.Tag;

            if (IsAdjacentTile(row, col))
            {
                string tileType = GetTileType(row, col);

                switch (tileType)
                {
                    case "chest":
                        MessageBox.Show("You found a chest with items!");
                        CollectChest(row, col);
                        break;

                    case "enemy":
                        bool defeated = BattleEnemy(row, col);
                        if (defeated)
                        {
                            MessageBox.Show("Enemy defeated!");
                            RemoveEnemyFromGrid(row, col);
                        }
                        else
                        {
                            MessageBox.Show("You were defeated by the enemy.");
                        }
                        break;

                    case "item":
                        MessageBox.Show("You picked up an item!");
                        CollectItem(row, col);
                        break;

                    case "empty":
                    default:
                        // Update tile type in `tileTypes` and SQL when player moves
                        tileTypes.Remove((player.Row, player.Col));
                        tileTypes[(row, col)] = "player";
                        // Replace with correct method to update the tile in the database
                        DataAccessGame dataAccessGame = new DataAccessGame();

                        // Set the old position to "empty"
                        dataAccessGame.UpdateGameGrid(gameID, player.Row, player.Col, "empty");

                        // Set the new position to "player"
                        dataAccessGame.UpdateGameGrid(gameID, row, col, "player");
                        player.Move(row, col);
                        HighlightAllObjects();
                        break;
                }
            }
            else
            {
                MessageBox.Show("You can only move to adjacent tiles.");
            }
        }




        // Check if a tile is adjacentq
        private bool IsAdjacentTile(int row, int col)
        {
            int rowDiff = Math.Abs(row - player.Row);
            int colDiff = Math.Abs(col - player.Col);

            // Adjacent if difference in row or column is 1
            return (rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Gameplay_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            GameLobby gameLobby = new GameLobby(false);
            this.Close();
            gameLobby.Show();
        }
    }
}
