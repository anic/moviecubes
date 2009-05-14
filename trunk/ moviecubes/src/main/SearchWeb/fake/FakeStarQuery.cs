using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using MovieCube.Common.Interface;
using MovieCube.Common.Data;

namespace SearchWeb
{
    public class FakeStarQuery : IStarQuery
    {

     
        #region IStarQuery Members

        public List<Star> QueryStarByName(string name)
        {
            FakeDb db = new FakeDb();
            List<Star> result = new List<Star>();
            foreach (Star s in db.Stars)
            {
                if (s.Name == name)
                {
                    result.Add(s.Clone(1));

                }

            }
            return result;
        }

        public List<Star> QueryStarByMovie(string movieName)
        {
            FakeDb db = new FakeDb();
            List<Star> result = new List<Star>();
            foreach (Star s in db.Stars)
            {
                foreach (Movie m in s.ActMovies)
                {
                    if (m.Name == movieName)
                    {
                        result.Add(s.Clone(1));
                        break;
                    }
                }

            }
            return result;
        }

        #endregion
    }
}