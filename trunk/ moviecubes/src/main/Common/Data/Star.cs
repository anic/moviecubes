using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    public class MovieStar
    {
        public Star Star { get; set; }
        public string Role { get; set; }

        public MovieStar()
        {
            Star = new Star();
        }

        public MovieStar(Star star, string role)
        {
            Star = star;
            Role = role;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Star : ICloneable, IComparable<Star>
    {
        public Star()
        {
            Alias = new List<string>();
            Movies = new List<StarMovie>();
            
            //ID = 0;
            //Name = "";
            //Area = "";
            //Web = "";
            //Rank = 0;
            //Image = "";
        }


        public void AddMovies(Movie addMovie, string role)
        {
            Movies.Add(new StarMovie(addMovie, role));
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
        /// 参与的电影
        /// </summary>
        [JsonProperty]
        public List<StarMovie> Movies { get; set; }

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
                //TODO
               
            }
            return result;
        }

        #region IComparable<Star> 成员

        public int CompareTo(Star other)
        {
            //throw new NotImplementedException();
            if (this.Rank < other.Rank)
                return 1;
            else if (this.Rank == other.Rank)
                return 0;
            else
                return -1;
        }

        #endregion
    }
}
