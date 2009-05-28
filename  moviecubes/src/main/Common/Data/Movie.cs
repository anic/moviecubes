using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StarMovie
    {
        [JsonProperty]
        public Movie Movie { get; set; }

        private string role;

        [JsonProperty]
        public string Role { 
            get{return role;}
            set { role = value.Trim(); }
        }

        public StarMovie()
        {
            Movie = new Movie();
        }

        public StarMovie(Movie movie, string role)
        {
            Movie = movie;
            Role = role;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Movie : ICloneable, IComparable<Movie>
    {
        public Movie()
        {
            Alias = new List<string>();
            Stars = new List<MovieStar>();
            Type = new List<string>();

            TotalStarNum = 0;
            //ID = 0;
            //Time = "";
            //Name = "";
            //Area = "";
            //Language = "";
            //Rank = 0;
            //Image = "";
        }


        public void AddStar(Star addStar, string role)
        {
            Stars.Add(new MovieStar(addStar, role));
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
        /// 明星
        /// </summary>
        [JsonProperty]
        public List<MovieStar> Stars { get; set; }

        /// <summary>
        /// 总明星个数
        /// </summary>
        [JsonProperty]
        public int TotalStarNum { get; set; }

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

        /// <summary>
        /// 对象类型
        /// </summary>
        [JsonProperty]
        public string ObjectType { get { return "MOVIE"; } }

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
                //TO DO
            }
            return result;
        }

        #region IComparable<Movie> 成员

        public int CompareTo(Movie other)
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
