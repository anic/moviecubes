using System;
using System.Collections.Generic;
using System.Text;
using MovieCube.Common.Data;

namespace MovieCube.Common.Interface
{
    public interface IStarQuery
    {
        /// <summary>
        /// 根据name获取star所有信息，for lz, 优先
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<Star> QueryStarAllInfoByName(string name);

        /// <summary>
        /// 根据keyword获取star所有信息，for lz
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<Star> QueryStarAllInfoByKeyword(string keyword);

        List<Star> QueryStarByKeyword(string keyword);

        /// <summary>
        /// 根据keyword获取Star，且获取从index开始的count个movie
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Star> QueryStarByKeyword(string keyword, int index, int count);

        List<Star> QueryStarByName(string name);

        /// <summary>
        /// 根据name获取Star，且获取从index开始的count个movie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Star> QueryStarByName(string name, int index, int count);


        List<Star> QueryStarByID(int id, int layer, int[] index, int[] count);

        List<Star> QueryStarByName(string name, int layer, int[] index, int[] count);

        List<Star> QueryStarByKeyword(string keyword, int layer, int[] index, int[] count);
    }
}
