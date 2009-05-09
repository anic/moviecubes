using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MovieCube.RelationalDataAccess
{
    public class StarAccess
    {
        /// <summary>
        /// 根据starName获得star信息和star的movie信息
        /// </summary>
        /// <param name="starName"></param>
        /// <returns></returns>
        public static DataSet GetInfoByStarName(string starName)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_GetInfoByStarName", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("starName_", MySqlDbType.MediumText);
                cmd.Parameters["starName_"].Direction = ParameterDirection.Input;
                cmd.Parameters["starName_"].Value = starName;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                conn.Close();
            }

            return ds;
        }

        /// <summary>
        /// 根据name获得star
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataSet GetStarByName(string name)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_GetStarByName", conn);
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

        /// <summary>
        /// 根据id获得star
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetStarByID(int id)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_GetStarByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("id_", MySqlDbType.Int32);
                cmd.Parameters["id_"].Direction = ParameterDirection.Input;
                cmd.Parameters["id_"].Value = id;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                conn.Close();
            }

            return ds;
        }
    }
}
