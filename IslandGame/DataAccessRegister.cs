using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGame
{
    internal class DataAccessRegister : DataAccess
    {
        public string RegisterConnection()
        {
            try
            {
                DataAccessRegister.mySqlConnection.Open();
                return "Register Database connection is successful!!";
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

        // Move RegisterUser inside the class
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
                var dataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL RegisterUser(@Email, @UserName, @Password)", lcParameterList.ToArray());

                // Check the result from the stored procedure
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string message = dataSet.Tables[0].Rows[0]["MESSAGE"].ToString();
                    return message; // 'Fields cannot be empty', 'NAME EXISTS', 'ADDED USER NAME'
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
    }
}
