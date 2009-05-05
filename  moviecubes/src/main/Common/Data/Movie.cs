using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCube.Common.Data
{
    public class Movie
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID{get;set;}

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alia { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string[] Directors { get; set; }

        /// <summary>
        /// 编剧
        /// </summary>
        public string[] Writers { get; set; }

        /// <summary>
        /// 演员（有个关系表，演员ID，演员的角色（主演之类的））
        /// </summary>
        public string[] Actors { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        

        /// <summary>
        /// 地区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 分值
        /// </summary>
        public double Rank { get; set; }
    }
}
