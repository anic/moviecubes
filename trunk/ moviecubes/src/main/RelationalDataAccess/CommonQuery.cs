using System;
using System.Collections.Generic;
using System.Text;

using MovieCube.Common;
using MovieCube.Common.Data;

namespace MovieCube.RelationalDataAccess
{
    public class CommonQuery
    {
        public static Movie ExtendMovie(Movie extendedMovie, int layer)
        {
            if (layer == 0)
                return null;

            Movie movie = MovieQuery.GetMovieInfoByID(extendedMovie.ID);

            if (movie == null)
                return null;
            else
            {
                int newLayer = layer - 1;

                if (newLayer > 0)
                {
                    //扩展movie的star
                    for (int i = 0; i < movie.Stars.Count; i++)
                    {
                        MovieStar moviestar = movie.Stars[i];
                        moviestar.Star = ExtendStar(moviestar.Star, newLayer);
                    }
                }
                return movie;
            }
        }

        public static Star ExtendStar(Star extendedStar, int layer)
        {
            if (layer == 0)
                return null;

            Star star = StarQuery.GetStarInfoByID(extendedStar.ID);

            if (star == null)
                return null;
            else
            {
                int newLayer = layer - 1;

                if (newLayer > 0)
                {
                    //扩展star的movie
                    for (int i = 0; i < star.Movies.Count; i++ )
                    {
                        StarMovie starmovie = star.Movies[i];
                        starmovie.Movie = ExtendMovie(starmovie.Movie, newLayer);
                    }
                }
                return star;
            }
        }
    }
}
