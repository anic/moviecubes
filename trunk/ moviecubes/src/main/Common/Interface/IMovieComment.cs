using System;
using System.Collections.Generic;
using System.Text;

using MovieCube.Common.Data;

namespace MovieCube.Common.Interface
{
    public interface IMovieComment
    {
        List<MovieComment> GetMovieCommentByName(string name);
    }
}
