using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace IslandGame
{
    internal class DataAccessGame : DataAccess
    {

        // Method to retrieve game grid data from the database
        public DataTable GetGameGrid(int gameId)
        {
            try
            {
                // Prepare the list of parameters for the stored procedure
                List<MySqlParameter> lcParameterList = new List<MySqlParameter>
                {
                    new MySqlParameter("@GameId", MySqlDbType.Int32) { Value = gameId }
                };

                // Execute the stored procedure to get the game grid data
                DataTable gameGridTable = MySqlHelper.ExecuteDataset(
                    DataAccess.mySqlConnection,
                    "CALL GetGameGrid(@GameId)",
                    lcParameterList.ToArray()
                ).Tables[0];

                return gameGridTable; // Return the grid data as a DataTable
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving game grid: {ex.Message}");
                return null;
            }
        }

        // Method to update the game grid in the database
        public bool UpdateGameGrid(int gameId, int row, int col, string newValue)
        {
            try
            {
                List<MySqlParameter> lcParameterList = new List<MySqlParameter>
        {
            new MySqlParameter("@GameId", MySqlDbType.Int32) { Value = gameId },
            new MySqlParameter("@Row", MySqlDbType.Int32) { Value = row },
            new MySqlParameter("@Col", MySqlDbType.Int32) { Value = col },
            new MySqlParameter("@NewValue", MySqlDbType.VarChar, 50) { Value = newValue }
        };

                int rowsAffected = MySqlHelper.ExecuteNonQuery(
                    DataAccess.mySqlConnection,
                    "CALL UpdateGameGrid(@GameId, @Row, @Col, @NewValue)",
                    lcParameterList.ToArray()
                );

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating game grid: {ex.Message}");
                return false;
            }
        }


        // Method to save a new game state
        public int CreateNewGame(string playerName)
        {
            try
            {
                // Prepare the list of parameters for the stored procedure
                List<MySqlParameter> lcParameterList = new List<MySqlParameter>
                {
                    new MySqlParameter("@PlayerName", MySqlDbType.VarChar, 50) { Value = playerName }
                };

                // Execute the stored procedure to create a new game
                var dataSet = MySqlHelper.ExecuteDataset(
                    DataAccess.mySqlConnection,
                    "CALL CreateNewGame(@PlayerName)",
                    lcParameterList.ToArray()
                );

                // Get the new game ID from the result
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    int newGameId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["GameId"]);
                    return newGameId;
                }
                else
                {
                    Console.WriteLine("Error creating new game.");
                    return -1; // Return -1 to indicate failure
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating new game: {ex.Message}");
                return -1;
            }
        }

        // Method to get a list of active games
        public List<string> GetActiveGames()
        {
            List<string> activeGames = new List<string>();

            try
            {
                // Query to get the active games
                string query = "SELECT GameID, StartTime FROM Game WHERE GameStatus = 'active'";

                using (MySqlDataReader reader = MySqlHelper.ExecuteReader(DataAccess.mySqlConnection, query))
                {
                    while (reader.Read())
                    {
                        int gameId = reader.GetInt32("GameID");
                        DateTime startTime = reader.GetDateTime("StartTime");
                        activeGames.Add($"Game {gameId} - Started at {startTime}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching active games: {ex.Message}");
            }

            return activeGames;
        }
    }
}
