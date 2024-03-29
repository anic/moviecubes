﻿using System;
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
                return extendedMovie;
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
                }
                return movie;
            }
        }

        public Movie ExtendMovie(Movie extendedMovie, int layer, int[] index, int[] count)
        {
            if (layer == 0)
                return null;

            int indexIndex = index.Length - layer;
            Movie movie = (new MovieQuery(movieInfo)).GetMovieInfoByID(extendedMovie.ID, index[indexIndex], count[indexIndex]);

            if (movie == null)
                return extendedMovie;
            else
            {
                int newLayer = layer - 1;

                if (newLayer > 0)
                {
                    //扩展movie的star
                    int num = Math.Min(count[indexIndex], movie.Stars.Count);
                    for (int i = 0; i < num; i++)
                    {
                        MovieStar moviestar = movie.Stars[i];
                        moviestar.Star = ExtendStar(moviestar.Star, newLayer, index, count);
                    }
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
                return extendedStar;
            else
            {
                int newLayer = layer - 1;

                if (newLayer > 0)
                {
                    //扩展star的movie
                    int num = Math.Min(Definition.Max_Surround_Node_Num, star.Movies.Count);

                    for (int i = 0; i < num; i++)
                    {
                        StarMovie starmovie = star.Movies[i];
                        starmovie.Movie = ExtendMovie(starmovie.Movie, newLayer);
                    }
 
                }
                return star;
            }
        }

        public Star ExtendStar(Star extendedStar, int layer, int[] index, int[] count)
        {
            if (layer == 0)
                return null;

            int indexIndex = index.Length - layer;

            Star star = (new StarQuery(starInfo)).GetStarInfoByID(extendedStar.ID, index[indexIndex], count[indexIndex]);

            if (star == null)
                return extendedStar;
            else
            {
                int newLayer = layer - 1;

                if (newLayer > 0)
                {
                    //扩展star的movie
                    int num = Math.Min(count[indexIndex], star.Movies.Count);

                    for (int i = 0; i < num; i++)
                    {
                        StarMovie starmovie = star.Movies[i];
                        starmovie.Movie = ExtendMovie(starmovie.Movie, newLayer, index, count);
                    }

                }
                return star;
            }
        }
    }
}
