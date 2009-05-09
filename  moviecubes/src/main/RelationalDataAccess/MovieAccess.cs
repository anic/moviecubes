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
        /// <summary>
        /// 根据movieName获得movie信息和movie的star信息
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public static DataSet GetInfoByMovieName(string movieName)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_GetInfoByMovieName", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("movieName_", MySqlDbType.MediumText);
                cmd.Parameters["movieName_"].Direction = ParameterDirection.Input;
                cmd.Parameters["movieName_"].Value = movieName;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                conn.Close();
            }

            return ds;
        }


        /// <summary>
        /// 根据movieID获得movie信息和movie的star信息
        /// </summary>
        /// <param name="movieID"></param>
        /// <returns></returns>
        public static DataSet GetInfoByMovieID(int movieID)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_GetInfoByMovieID", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("movieID_", MySqlDbType.MediumText);
                cmd.Parameters["movieID_"].Direction = ParameterDirection.Input;
                cmd.Parameters["movieID_"].Value = movieID;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                conn.Close();
            }

            return ds;
        }

        /// <summary>
        /// 根据id获得movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetMovieByID(int id)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("sp_GetMovieByID", conn);
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

        /// <summary>
        /// 根据name获得movie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
