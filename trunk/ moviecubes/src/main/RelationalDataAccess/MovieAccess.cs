using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using System.Data;
using MySql.Data.MySqlClient;

namespace MovieCube.RelationalDataAccess
{
    public class MovieAccess
    {
        public static DataSet GetMovieByName(string name)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_GetMovieByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("name_", MySqlDbType.MediumText);
                cmd.Parameters["name_"].Direction = ParameterDirection.Input;
                cmd.Parameters["name_"].Value = name;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                conn.Close();
            }

            return ds;
        }
    }
}
