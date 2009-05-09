using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using MovieCube.Common;
using MovieCube.Common.Data;
using MovieCube.Common.Interface;

namespace MovieCube.RelationalDataAccess
{
    public class MovieQuery: IMovieQuery
    {
        #region IMovieQuery 成员

        public List<Movie> QueryMovieByName(string name)
        {
            return GetMovieByName(name);
        }

        public List<Movie> QueryMovieByActor(string actorName)
        {
            return null;
        }

        #endregion

        private List<Movie> GetMovieByName(string name)
        {
            List<Movie> resultMovies = new List<Movie>();
            DataSet ds = MovieAccess.GetInfoByMovieName(name);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++ )
            {
                DataRow dr = ds.Tables[0].Rows[i];

                int id = Convert.ToInt32(dr["movieID"].ToString());

                Star star = new Star();
                star.ID = Convert.ToInt32(dr["starID"].ToString());
                star.Name = dr["starName"].ToString();
                star.Rank = Convert.ToDouble(dr["starRank"].ToString());
                star.Image = dr["starImage"].ToString();

                string staralias = dr["starAlia"].ToString();
                if(staralias != "")
                    Util.ProcessStringItem(staralias, star.Alias);

                string role = dr["role"].ToString();

                Movie movie = Util.FindMovie(id, resultMovies);
                if (movie == null)
                {
                    movie = new Movie();
                    movie.ID = id;
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

                    movie.AddStars(star, role);
                    resultMovies.Add(movie);
                }
                else
                    movie.AddStars(star, role);
            }

            return resultMovies;
        }
    }
}
