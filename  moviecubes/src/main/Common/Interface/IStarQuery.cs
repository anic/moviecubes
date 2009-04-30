using System;
using System.Collections.Generic;
using System.Text;
using MovieCube.Common.Data;

namespace MovieCube.Common.Interface
{
    public interface IStarQuery
    {
        List<Star> QueryStarByName(string name);

        List<Star> QueryStarByMovie(string movieName);
    }
}
