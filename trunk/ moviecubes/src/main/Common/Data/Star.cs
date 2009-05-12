using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Star
    {
        public Star()
        {
            Alias = new List<string>();
            DirectMovies = new List<Movie>();
            WriteMovies = new List<Movie>();
            ActMovies = new List<Movie>();
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

    }
}
