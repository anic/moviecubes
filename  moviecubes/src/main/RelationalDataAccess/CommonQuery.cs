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
                return extendedMovie;

            List<Movie> movieList = MovieQuery.GetMovieInfoByID(extendedMovie.ID);
            //根据id获得的movie，或者没有找到，或者是精确匹配
            if (movieList.Count > 0)
                return movieList[0];
            else
                return extendedMovie;

            int newLayer = layer - 1;
            if (newLayer > 0)
            {
                //扩展movie的star
                for (int i = 0; i < extendedMovie.Directors.Count; i++)
                {
                    extendedMovie.Directors[i] = ExtendStar(extendedMovie.Directors[i], newLayer);
                }
                for (int i = 0; i < extendedMovie.Writers.Count; i++)
                {
                    extendedMovie.Writers[i] = ExtendStar(extendedMovie.Writers[i], newLayer);
                }
                for (int i = 0; i < extendedMovie.Actors.Count; i++)
                {
                    extendedMovie.Actors[i] = ExtendStar(extendedMovie.Actors[i], newLayer);
                }
            }
        }

        public static Star ExtendStar(Star extendedStar, int layer)
        {
            if (layer == 0)
                return extendedStar;

            List<Star> starList = StarQuery.GetStarInfoByID(extendedStar.ID);
            //根据id获得的movie，或者没有找到，或者是精确匹配
            if (starList.Count > 0)
                return starList[0];
            else
                return extendedStar;

            int newLayer = layer - 1;
            if (newLayer > 0)
            {
                //扩展star的movie
                for (int i = 0; i < extendedStar.DirectMovies.Count; i++)
                {
                    extendedStar.DirectMovies[i] = ExtendMovie(extendedStar.DirectMovies[i], newLayer);
                }
                for (int i = 0; i < extendedStar.WriteMovies.Count; i++)
                {
                    extendedStar.WriteMovies[i] = ExtendMovie(extendedStar.WriteMovies[i], newLayer);
                }
                for (int i = 0; i < extendedStar.ActMovies.Count; i++)
                {
                    extendedStar.ActMovies[i] = ExtendMovie(extendedStar.ActMovies[i], newLayer);
                }
            }
            return extendedStar;
        }
    }
}
