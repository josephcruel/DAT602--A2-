using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGame
{
    internal class DataAccessAdmin : DataAccess
    {
        public string AddAdminUser(string pUserName, string pPassword)
        {
            try
            {
                // Prepare the list of parameters for the AddAdminUser procedure
                List<MySqlParameter> lcParameterList = new List<MySqlParameter>();

                var lcUsernameParameter = new MySqlParameter("@UserName", MySqlDbType.VarChar, 50);
                var lcPasswordParameter = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);

                lcUsernameParameter.Value = pUserName;
                lcPasswordParameter.Value = pPassword;

                lcParameterList.Add(lcUsernameParameter);
                lcParameterList.Add(lcPasswordParameter);

                // Execute the stored procedure for adding an admin user
                var dataSet = MySqlHelper.ExecuteDataset(DataAccess.mySqlConnection, "CALL AddAdminUser(@UserName, @Password)", lcParameterList.ToArray());

                // Check the result from the stored procedure
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string message = dataSet.Tables[0].Rows[0]["MESSAGE"].ToString();
                    return message; // Possible messages: 'ADMIN NAME EXISTS', 'ADMIN USER ADDED'
                }
                else
                {
                    return "Error during admin registration.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message; // Return any error that occurs
            }
        }

    }
}
