﻿using System;
using System.Collections.Generic;
using System.Text;

using MovieCube.Common.Data;

namespace MovieCube.Common
{
    public class Util
    {
        /// <summary>
        /// 将用" , "隔开的数据添加到result中
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public static void ProcessStringItem(string s, List<string> result)
        {
            string[] resultArray = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < resultArray.Length; i++)
            {
                resultArray[i] = resultArray[i].Trim();
                if (resultArray[i] != "")
                    result.Add(resultArray[i]);
            }
        }

        /// <summary>
        /// 给定id在List<Movie>中查找movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieList"></param>
        /// <returns>返回找到的movie或者null</returns>
        public static Movie FindMovie(int id, List<Movie> movieList)
        {
            for (int i = 0; i < movieList.Count; i++)
            {
                if (movieList[i].ID == id)
                    return movieList[i];
            }
            return null;
        }

        /// <summary>
        /// 给定id在List<Star>中查找star
        /// </summary>
        /// <param name="id"></param>
        /// <param name="starList"></param>
        /// <returns>返回找到的star或者null</returns>
        public static Star FindStar(int id, List<Star> starList)
        {
            for(int i=0; i< starList.Count; i++)
            {
                if (starList[i].ID == id)
                    return starList[i];
            }
            return null;
        }
    }
}