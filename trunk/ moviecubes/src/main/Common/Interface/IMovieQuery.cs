using System;
using System.Collections.Generic;
using System.Text;
using MovieCube.Common.Data;

namespace MovieCube.Common.Interface
{
    public interface IMovieQuery
    {
        /// <summary>
        /// 根据关键词搜索电影
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<Movie> QueryMovieByKeyword(string keyword);


        /// <summary>
        /// 通过电影名字搜索电影
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<Movie> QueryMovieByName(string name);

        /// <summary>
        /// 根据电影名字搜索电影，获取从index开始的count个star
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Movie> QueryMovieByName(string name, int index, int count);

        /// <summary>
        /// 根据关键词搜索电影，获取从index开始的count个star
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Movie> QueryMovieByKeyword(string keyword, int index, int count);

        List<Movie> QueryMovieByID(int id, int layer, int[] index, int[] count);
        List<Movie> QueryMovieByName(string name, int layer, int[] index, int[] count);
        List<Movie> QueryMovieByKeyword(string keyword, int layer, int[] index, int[] count);
    }
}
