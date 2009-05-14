using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using MovieCube.Common.Interface;
using MovieCube.Common.Data;

namespace SearchWeb
{
    public class FakeMovieQuery : IMovieQuery
    {
        
        #region IMovieQuery Members

        public List<MovieCube.Common.Data.Movie> QueryMovieByActor(string actorname)
        {
            FakeDb db = new FakeDb();
            foreach (Star s in db.Stars)
            {
                if (s.Name == actorname)
                {
                    List<Movie> result = new List<Movie>();
                    foreach (Movie m in s.ActMovies)
                    {
                        result.Add(m.Clone(1));
                    }
                    return result;
                }
            }
            return new List<Movie>();
        }

        public List<MovieCube.Common.Data.Movie> QueryMovieByName(string name)
        {
            FakeDb db = new FakeDb();
            List<Movie> result = new List<Movie>();
            foreach (Movie m in db.Movies)
            {
                if (m.Name == name)
                {
                    result.Add(m.Clone(1));
                }
            }
            return result;
        }

        #endregion
    }
}