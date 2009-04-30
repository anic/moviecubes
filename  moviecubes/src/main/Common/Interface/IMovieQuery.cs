using System;
using System.Collections.Generic;
using System.Text;
using MovieCube.Common.Data;

namespace MovieCube.Common.Interface
{
    public interface IMovieQuery
    {
        /// <summary>
        /// 通过演员名字搜索电影
        /// </summary>
        /// <param name="actorname"></param>
        /// <returns></returns>
        List<Movie> QueryMovieByActor(string actorname);

        /// <summary>
        /// 通过电影名字搜索电影
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<Movie> QueryMovieByName(string name);


    }
}
