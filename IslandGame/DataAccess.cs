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
        protected static MySqlConnection mySqlConnection
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


        public string MakeBoard(int pMaxRows, int pMaxCols)
        {
            List<MySqlParameter> lcParameterList = new List<MySqlParameter>();
            var lcRowParameter = new MySqlParameter("@MaxRow", MySqlDbType.Int32);
            var lcColParameter = new MySqlParameter("@MaxCol", MySqlDbType.Int32);

            lcRowParameter.Value = pMaxRows;
            lcColParameter.Value = pMaxCols;    

            lcParameterList.Add(lcRowParameter);
            lcParameterList.Add(lcColParameter);

            var dataSet = MySqlHelper.ExecuteDataset(DataAccess.mySqlConnection, "call make_a_board(@MaxRow, @MaxCol)", lcParameterList.ToArray());
            // expecting one table with one row
            return (dataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }


        public string RegisterConnection()
        {
            try
            {
                DataAccess.mySqlConnection.Open();
                return "Register Database connection is successful!!";
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

        
        public string Login(string pUserName, string pPassword)
        {
            try
            {
                // Prepare the list of parameters for the login procedure
                List<MySqlParameter> lcParameterList = new List<MySqlParameter>();
                var lcUsernameParameter = new MySqlParameter("@UserName", MySqlDbType.VarChar, 50);
                var lcPasswordParameter = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);

                lcUsernameParameter.Value = pUserName;
                lcPasswordParameter.Value = pPassword;

                lcParameterList.Add(lcUsernameParameter);
                lcParameterList.Add(lcPasswordParameter);

                // Execute the stored procedure for login
                var dataSet = MySqlHelper.ExecuteDataset(DataAccess.mySqlConnection, "CALL Login(@UserName, @Password)", lcParameterList.ToArray());

                // Check if the login was successful
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string loginMessage = dataSet.Tables[0].Rows[0]["MESSAGE"].ToString();
                    return loginMessage; // messages "Logged In" and "Invalid username or password"
                }
                else
                {
                    return "Error during login process.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message; // Return any error that occurs
            }
        }


        public string RegisterUser(string pEmail, string pUserName, string pPassword)
        {
            try
            {
                // Prepare the list of parameters for the AddUserName procedure
                List<MySqlParameter> lcParameterList = new List<MySqlParameter>();

                var lcEmailParameter = new MySqlParameter("@Email", MySqlDbType.VarChar, 50);
                var lcUsernameParameter = new MySqlParameter("@UserName", MySqlDbType.VarChar, 50);
                var lcPasswordParameter = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);

                lcEmailParameter.Value = pEmail;
                lcUsernameParameter.Value = pUserName;
                lcPasswordParameter.Value = pPassword;

                lcParameterList.Add(lcEmailParameter);
                lcParameterList.Add(lcUsernameParameter);
                lcParameterList.Add(lcPasswordParameter);

                // Execute the stored procedure for adding a user
                var dataSet = MySqlHelper.ExecuteDataset(DataAccess.mySqlConnection, "CALL RegisterUser(@Email, @UserName, @Password)", lcParameterList.ToArray());

                // Check the result from the stored procedure
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string message = dataSet.Tables[0].Rows[0]["MESSAGE"].ToString();
                    return message; //  'Fields cannot be empty'
                }
                else
                {
                    return "Error during registration.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message; // Return any error that occurs
            }
        }


        public string AdminConsole()
        {
            try
            {
                DataAccess.mySqlConnection.Open();
                return "Connected to the Admin Console";
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
