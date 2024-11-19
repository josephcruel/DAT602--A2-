using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandGame
{
    internal class DataAccessPlayer : DataAccess
    {
        // Method to update player position
        public void MovePlayer(int characterID, int newRow, int newCol)
        {
            try
            {
                // Open the connection
                if (DataAccess.mySqlConnection.State != ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Open();
                }

                // Prepare the list of parameters for the stored procedure
                List<MySqlParameter> parameters = new List<MySqlParameter>
        {
            new MySqlParameter("@CharacterID", MySqlDbType.Int32) { Value = characterID },
            new MySqlParameter("@NewRow", MySqlDbType.Int32) { Value = newRow },
            new MySqlParameter("@NewCol", MySqlDbType.Int32) { Value = newCol }
        };

                // Call the stored procedure to update player position
                string query = "CALL UpdatePlayerPosition(@CharacterID, @NewRow, @NewCol)";
                using (MySqlCommand cmd = new MySqlCommand(query, DataAccess.mySqlConnection))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating player position: {ex.Message}");
                MessageBox.Show($"Error updating player position: {ex.Message}");
            }
            finally
            {
                // Close the connection
                if (DataAccess.mySqlConnection.State == ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Close();
                }
            }
        }


        // Method to get player's starting position
        public (int, int) GetPlayerStartPosition(int playerID)
        {
            try
            {
                // Open the connection
                if (DataAccess.mySqlConnection.State != ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Open();
                }

                string query = "SELECT Row, Col FROM PlayerCharacter WHERE PlayerID = @PlayerID";
                using (MySqlCommand cmd = new MySqlCommand(query, DataAccess.mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@PlayerID", playerID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int row = reader.GetInt32("Row");
                            int col = reader.GetInt32("Col");
                            return (row, col);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving player start position: {ex.Message}");
                MessageBox.Show($"Error retrieving player start position: {ex.Message}");
            }
            finally
            {
                // Close the connection
                if (DataAccess.mySqlConnection.State == ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Close();
                }
            }

            return (0, 0); // Default to (0, 0) if retrieval fails
        }


        // Method to get a list of online players
        public List<string> GetOnlinePlayers()
        {
            List<string> onlinePlayers = new List<string>();

            try
            {
                // Open the connection
                if (DataAccess.mySqlConnection.State != ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Open();
                }

                string query = "SELECT Username FROM Player WHERE UserOnline = TRUE";
                using (MySqlCommand cmd = new MySqlCommand(query, DataAccess.mySqlConnection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string username = reader.GetString("Username");
                            onlinePlayers.Add(username);
                            Console.WriteLine($"Online Player: {username}"); // Debug log
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching online players: {ex.Message}");
                MessageBox.Show($"Error fetching online players: {ex.Message}");
            }
            finally
            {
                // Close the connection
                if (DataAccess.mySqlConnection.State == ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Close();
                }
            }

            return onlinePlayers;
        }

        public List<string> GetActiveGames()
        {
            List<string> activeGames = new List<string>();

            try
            {
                // Open the connection
                if (DataAccess.mySqlConnection.State != ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Open();
                }

                string query = "SELECT GameID, StartTime FROM Game WHERE GameStatus = 'active'";
                using (MySqlCommand cmd = new MySqlCommand(query, DataAccess.mySqlConnection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int gameId = reader.GetInt32("GameID");
                            DateTime startTime = reader.GetDateTime("StartTime");
                            string gameInfo = $"Game {gameId} - Started at {startTime}";
                            activeGames.Add(gameInfo);
                            Console.WriteLine($"Active Game: {gameInfo}"); // Debug log
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching active games: {ex.Message}");
                MessageBox.Show($"Error fetching active games: {ex.Message}");
            }
            finally
            {
                // Close the connection
                if (DataAccess.mySqlConnection.State == ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Close();
                }
            }

            return activeGames;
        }


        // Method to retrieve the game board setup
        public List<(int Row, int Col, string TileType)> GetGameBoard(int gameID)
        {
            List<(int Row, int Col, string TileType)> gameBoard = new List<(int, int, string)>();

            try
            {
                // Open the connection if not already open
                if (DataAccess.mySqlConnection.State != ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Open();
                }

                // Query to retrieve the tile data for the specified game
                string query = "SELECT `Row`, Col, TileType FROM GameBoard WHERE GameID = @GameID";
                using (MySqlCommand cmd = new MySqlCommand(query, DataAccess.mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@GameID", gameID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int row = reader.GetInt32("Row");
                            int col = reader.GetInt32("Col");
                            string tileType = reader.GetString("TileType");

                            // Add each tile's data to the list
                            gameBoard.Add((row, col, tileType));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving game board: {ex.Message}");
                MessageBox.Show($"Error retrieving game board: {ex.Message}");
            }
            finally
            {
                // Ensure connection is closed
                if (DataAccess.mySqlConnection.State == ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Close();
                }
            }

            return gameBoard;
        }


        public void UpdateTileType(int gameID, int row, int col, string newTileType)
        {
            try
            {
                // Open connection if closed
                if (DataAccess.mySqlConnection.State != ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Open();
                }

                string query = "UPDATE GameBoard SET TileType = @NewTileType WHERE GameID = @GameID AND `Row` = @Row AND Col = @Col";
                using (MySqlCommand cmd = new MySqlCommand(query, DataAccess.mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@NewTileType", newTileType);
                    cmd.Parameters.AddWithValue("@GameID", gameID);
                    cmd.Parameters.AddWithValue("@Row", row);
                    cmd.Parameters.AddWithValue("@Col", col);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating tile type: {ex.Message}");
            }
            finally
            {
                // Close the connection
                if (DataAccess.mySqlConnection.State == ConnectionState.Open)
                {
                    DataAccess.mySqlConnection.Close();
                }
            }
        }

    }
}
