using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Numerics;

namespace IslandGame
{
    class DataAccess
    {
        private static string connectionString
        {
            get { return "Server=localhost;Port=3306;Database=islandgamedb;Uid=Admin;password=root1234;"; }

        }

        private static MySqlConnection _mySqlConnection = null;
        public static MySqlConnection mySqlConnection
        {
            get
            {
                if (_mySqlConnection == null)
                {
                    _mySqlConnection = new MySqlConnection(connectionString);
                }

                return _mySqlConnection;

            }
        }

        public string GameLobby()
        {
            try
            {
                DataAccess.mySqlConnection.Open();
                return "Returing to lobby";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                DataAccess.mySqlConnection.Close();
            }
        }
    }
}
