﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCube.Common.Data
{
    public class Star
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public List<string> Alias { get; set; }

        /// <summary>
        /// 参与电影（对应关系表，movieID，演的角色（导演，主演，编剧之类的））
        /// </summary>
        public List<Movie> Movies { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 官网（需要吗）
        /// </summary>
        public string Web { get; set; }

        /// <summary>
        /// 分值（用于排序）
        /// </summary>
        public double Rank { get; set; }

        /// <summary>
        /// 默认图片
        /// </summary>
        public string Image { get; set; }

    }
}
