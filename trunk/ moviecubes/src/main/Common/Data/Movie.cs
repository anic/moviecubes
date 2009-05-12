﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Movie
    {
        public Movie()
        {
            Alias = new List<string>();
            Directors = new List<Star>();
            Writers = new List<Star>();
            Actors = new List<Star>();
            Type = new List<string>();
        }


        public void AddStars(Star addStar, string role)
        {
            switch (role)
            {
                case Definition.Role_Director:
                    {
                        Directors.Add(addStar);
                        break;
                    }
                case Definition.Role_Writer:
                    {
                        Writers.Add(addStar);
                        break;
                    }
                case Definition.Role_Actor:
                    {
                        Actors.Add(addStar);
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty]
        public int ID{get;set;}

        /// <summary>
        /// 上映时间
        /// </summary>
        [JsonProperty]
        public string Time { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [JsonProperty]
        public List<string> Alias { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        [JsonProperty]
        public List<Star> Directors { get; set; }

        /// <summary>
        /// 编剧
        /// </summary>
        [JsonProperty]
        public List<Star> Writers { get; set; }

        /// <summary>
        /// 演员（有个关系表，演员ID，演员的角色（主演之类的））
        /// </summary>
        [JsonProperty]
        public List<Star> Actors { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty]
        public List<string> Type { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty]
        public string Area { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        [JsonProperty]
        public string Language { get; set; }

        /// <summary>
        /// 分值
        /// </summary>
        [JsonProperty]
        public double Rank { get; set; }

        /// <summary>
        /// 默认图片
        /// </summary>
        [JsonProperty]
        public string Image { get; set; }
    }
}