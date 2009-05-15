using System;
using System.Collections.Generic;
using System.Text;

using MovieCube.Common;
using MovieCube.Common.Data;

namespace MovieCube.RelationalDataAccess
{
    public class CommonQuery
    {
        string starInfo;
        string movieInfo;

        public CommonQuery(string starInfo, string movieInfo)
        {
            this.starInfo = starInfo;
            this.movieInfo = movieInfo;
        }

        public static CommonQuery Instance
        {
            get;
            set;
        }

        public Movie ExtendMovie(Movie extendedMovie, int layer)
        {
            if (layer == 0)
                return null;

            Movie movie = (new MovieQuery(movieInfo)).GetMovieInfoByID(extendedMovie.ID);

            if (movie == null)
                return null;
            else
            {
                int newLayer = layer - 1;

                if (newLayer > 0)
                {
                    //扩展movie的star
                    int num = Math.Min(Definition.Max_Surround_Node_Num, movie.Stars.Count);
                    for (int i = 0; i < num; i++)
                    {
                        MovieStar moviestar = movie.Stars[i];
                        moviestar.Star = ExtendStar(moviestar.Star, newLayer);
                    }
                    if (num < movie.Stars.Count)
                        movie.Stars.RemoveRange(num, movie.Stars.Count - num - 1);
                }
                return movie;
            }
        }

        public Star ExtendStar(Star extendedStar, int layer)
        {
            if (layer == 0)
                return null;

            Star star = (new StarQuery(starInfo)).GetStarInfoByID(extendedStar.ID);

            if (star == null)
                return null;
            else
            {
                int newLayer = layer - 1;

                if (newLayer > 0)
                {
                    //扩展star的movie
                    int num = Math.Min(Definition.Max_Surround_Node_Num, star.Movies.Count);
                    for (int i = 0; i < num; i++ )
                    {
                        StarMovie starmovie = star.Movies[i];
                        starmovie.Movie = ExtendMovie(starmovie.Movie, newLayer);
                    }
                    if (num < star.Movies.Count)
                        star.Movies.RemoveRange(num, star.Movies.Count - num - 1);
                }
                return star;
            }
        }
    }
}
