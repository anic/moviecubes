﻿using System;
using System.Collections.Generic;
using System.Text;

using MovieCube.Common;
using MovieCube.Common.Data;
using MovieCube.Common.Interface;
using System.Data;

namespace MovieCube.RelationalDataAccess
{
    public class StarQuery : IStarQuery
    {

        #region IStarQuery 成员

        public List<Star> QueryStarByName(string name)
        {
            List<Star> resultStars = GetStarInfoByName(name);

            if (resultStars.Count > 0)
            {
                Star extendedStar = resultStars[0];
                int layer = Definition.Max_Node_Layer - 2;
                for (int i = 0; i < extendedStar.DirectMovies.Count; i++)
                {
                    extendedStar.DirectMovies[i] = CommonQuery.ExtendMovie(extendedStar.DirectMovies[i], layer);
                }
                for (int i = 0; i < extendedStar.WriteMovies.Count; i++)
                {
                    extendedStar.WriteMovies[i] = CommonQuery.ExtendMovie(extendedStar.WriteMovies[i], layer);
                }
                for (int i = 0; i < extendedStar.ActMovies.Count; i++)
                {
                    extendedStar.ActMovies[i] = CommonQuery.ExtendMovie(extendedStar.ActMovies[i], layer);
                }
            }

            return resultStars;
        }

        public List<Star> QueryStarByMovie(string movieName)
        {
            throw new NotImplementedException();
        }

        #endregion


        public static List<Star> GetStarInfoByName(string name)
        {
            DataSet ds = StarAccess.GetInfoByStarName(name);
            return GetInfoByDataSet(ds);
        }

        public static List<Star> GetStarInfoByID(int id)
        {
            DataSet ds = StarAccess.GetInfoByStarID(id);
            return GetInfoByDataSet(ds);
        }

        private static List<Star> GetInfoByDataSet(DataSet ds)
        {
            List<Star> resultStars = new List<Star>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                int id = Convert.ToInt32(dr["starID"].ToString());

                Movie movie = new Movie();
                movie.ID = Convert.ToInt32(dr["movieID"].ToString());
                movie.Name = dr["movieName"].ToString();
                movie.Image = dr["movieImage"].ToString();
                movie.Language = dr["movieLanguage"].ToString();
                movie.Area = dr["movieArea"].ToString();
                movie.Time = dr["movieTime"].ToString();
                movie.Rank = Convert.ToDouble(dr["movieRank"].ToString());

                string movieType = dr["movieType"].ToString();
                if (movieType != "")
                    Util.ProcessStringItem(movieType, movie.Type);

                string movieAlias = dr["movieAlia"].ToString();
                if (movieAlias != "")
                    Util.ProcessStringItem(movieAlias, movie.Alias);

                string role = dr["role"].ToString();

                Star star = Util.FindStar(id, resultStars);
                if (star == null)
                {
                    star = new Star();
                    star.ID = id;
                    star.Name = dr["starName"].ToString();
                    star.Rank = Convert.ToDouble(dr["starRank"].ToString());
                    star.Image = dr["starImage"].ToString();

                    string staralias = dr["starAlia"].ToString();
                    if (staralias != "")
                        Util.ProcessStringItem(staralias, star.Alias);

                    star.AddMovies(movie, role);
                    resultStars.Add(star);
                }
                else
                    star.AddMovies(movie, role);
            }

            return resultStars;
        }
    }
}