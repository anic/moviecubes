﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Movie:ICloneable
    {
        public Movie()
        {
            Alias = new List<string>();
            Directors = new List<Star>();
            Writers = new List<Star>();
            Actors = new List<Star>();
            Type = new List<string>();

            //ID = 0;
            //Time = "";
            //Name = "";
            //Area = "";
            //Language = "";
            //Rank = 0;
            //Image = "";
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

        #region ICloneable Members

        public object Clone()
        {
            Movie result = new Movie();
            result.ID = ID;
            result.Time = Time;
            result.Name = Name;
            result.Area = Area;
            result.Language = Language;
            result.Rank = Rank;
            result.Image = Image;
            return result;
        }

        #endregion

        public Movie Clone(int level)
        {
            Movie result = this.Clone() as Movie;
            if (level > 0)
            {
                foreach (Star s in Actors)
                {
                    result.Actors.Add(s.Clone(level - 1));
                }

                foreach (Star s in Writers)
                {
                    result.Writers.Add(s.Clone(level - 1));
                }

                foreach (Star s in Directors)
                {
                    result.Directors.Add(s.Clone(level - 1));
                }
            }
            return result;
        }
    }
}