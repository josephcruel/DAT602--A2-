using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGame
{
    internal class DataAccessLogin : DataAccess
    {
        public string LoginConnection()
        {
            try
            {
                DataAccessRegister.mySqlConnection.Open();
                return "Login Database connection is successful!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                DataAccessRegister.mySqlConnection.Close();
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
                    bool isAdmin = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["IsAdmin"]);

                    if (loginMessage == "Logged In")
                    {
                        return isAdmin ? "Logged In as Admin" : "Logged In";
                    }
                    return loginMessage; // "Logged in" or "Invalid username or password"
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
    }
}
