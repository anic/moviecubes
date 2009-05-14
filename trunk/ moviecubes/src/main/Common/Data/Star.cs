using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Star:ICloneable
    {
        public Star()
        {
            Alias = new List<string>();
            DirectMovies = new List<Movie>();
            WriteMovies = new List<Movie>();
            ActMovies = new List<Movie>();

            //ID = 0;
            //Name = "";
            //Area = "";
            //Web = "";
            //Rank = 0;
            //Image = "";
        }


        public void AddMovies(Movie addMovie, string role)
        {
            switch (role)
            {
                case Definition.Role_Director:
                    {
                        DirectMovies.Add(addMovie);
                        break;
                    }
                case Definition.Role_Writer:
                    {
                        WriteMovies.Add(addMovie);
                        break;
                    }
                case Definition.Role_Actor:
                    {
                        ActMovies.Add(addMovie);
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty]
        public int ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [JsonProperty]
        public List<string> Alias { get; set; }

        /// <summary>
        /// 作为导演的电影
        /// </summary>
        [JsonProperty]
        public List<Movie> DirectMovies { get; set; }

        /// <summary>
        /// 作为编剧的电影
        /// </summary>
        [JsonProperty]
        public List<Movie> WriteMovies { get; set; }

        /// <summary>
        /// 作为主演的电影
        /// </summary>
        [JsonProperty]
        public List<Movie> ActMovies { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty]
        public string Area { get; set; }

        /// <summary>
        /// 官网（需要吗）
        /// </summary>
        [JsonProperty]
        public string Web { get; set; }

        /// <summary>
        /// 分值（用于排序）
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
            Star result = new Star();
            result.ID = ID;
            result.Name = Name;
            result.Area = Area;
            result.Web = Web;
            result.Rank = Rank;
            result.Image = Image;
            return result;
        }

        #endregion

        public Star Clone(int level)
        {
            Star result = this.Clone() as Star;
            if (level > 0)
            {
                foreach (Movie m in ActMovies)
                {
                    result.ActMovies.Add(m.Clone(level - 1));
                }

                foreach (Movie m in DirectMovies)
                {
                    result.DirectMovies.Add(m.Clone(level - 1));
                }
               
            }
            return result;
        }
    }
}
