using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using MovieCube.RelationalDataAccess;
using MovieCube.Common.Data;
using Newtonsoft.Json;
using MovieCube.Common.Interface;
using SearchWeb;

namespace MovieCube.SearchWeb
{
    public partial class Relation : System.Web.UI.Page
    {
        protected const int NODE_LIMIT1 = 35;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = System.Web.HttpContext.Current.Request.Form["type"];
            string query = System.Web.HttpContext.Current.Request.Form["query"];
            string start = System.Web.HttpContext.Current.Request.Form["start"];
            string count = System.Web.HttpContext.Current.Request.Form["count"];
            string nodeCount = System.Web.HttpContext.Current.Request.Form["nodeCount"];

            string starInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/starinfo");
            string movieInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/movieinfo");

            IMovieQuery movieQuery = new MovieQuery(movieInfo);
            IStarQuery starQuery = new StarQuery(starInfo);

            int[] indices;
            int[] counts;
            int layer;

            List<Star> stars = new List<Star>();
            List<Movie> movies = new List<Movie>();


            if (type != null && type == "loadKeys")
            {
                List<QueryKey> result = KeywordHelper.Instance.GetTipList(query);
                Response.Write(JsonConvert.SerializeObject(result));
                return;
            }
            else if (type != null && query != null)
            {
                RelationResult result = new RelationResult();
                query = query.Trim();
                string[] subQuery = query.Split(' ');
                switch (type)
                {
                    case "queryByKey":
                        //先通过名称查找电影
                        movies = movieQuery.QueryMovieByName(query);

                        if (movies.Count != 0)
                        {
                            result.ResultMovie = movies;
                            result.RelatedStar = starQuery.QueryStarByKeyword(query);
                            result.RelatedMovie = movieQuery.QueryMovieByKeyword(query);
                            result.ClearRedundant();
                            Response.Write(JsonConvert.SerializeObject(result));
                            return;
                        }

                        //没有结果的话，通过名称查找明星
                        //foreach(string s in subQuery)
                        //    stars.AddRange(starQuery.QueryStarByName(s));
                        stars = starQuery.QueryStarByName(query);

                        if (stars.Count != 0)
                        {
                            result.ResultStar = stars;
                            result.RelatedStar = starQuery.QueryStarByKeyword(query);
                            result.RelatedMovie = movieQuery.QueryMovieByKeyword(query);
                            /*if (subQuery.Length == 1) //只有一个关键字，而且stars!=0表名搜索到了明星，停止
                                result.RelatedMovie = movieQuery.QueryMovieByKeyword(query);
                            else //如果不只一个关键字，而且Stars!=0，然后搜索电影，如（刘德华 黎明）、（刘德华 龙）
                                result.AddSingleMovie(movieQuery.QueryMovieByKeyword(query));
                            */
                            result.ClearRedundant();
                            Response.Write(JsonConvert.SerializeObject(result));
                            return;
                        }

                        //没有结果的话，通过关键字查找电影  和 通过关键字查找明星
                        movies = movieQuery.QueryMovieByKeyword(query);
                        stars = starQuery.QueryStarByKeyword(query);
                        result.AddSingleMovie(movies);
                        result.AddSingleStar(stars);
                        result.ClearRedundant();
                        Response.Write(JsonConvert.SerializeObject(result));
                        return;
                    case "queryStarByKeyword":
                        stars = starQuery.QueryStarByName(query);
                        if (stars.Count == 0)
                        {
                            result.AddSingleStar(starQuery.QueryStarByKeyword(query));
                        }
                        else if (stars.Count == 1)
                        {
                            result.ResultStar.Add(stars[0]);
                            GetRelatedStar(result.RelatedStar, stars[0].Name, stars[0].ID);
                        }
                        else //stars>0
                        {
                            result.AddSingleStar(stars);
                        }
                        result.ClearRedundant();
                        Response.Write(JsonConvert.SerializeObject(result));
                        return;
                    case "queryStarById":
                        BuildLayer(nodeCount, start, count, out indices, out counts, out layer);
                        stars = starQuery.QueryStarByID(Convert.ToInt32(query), 2, indices, counts);
                        if (stars.Count >= 1)
                        {
                            result.ResultStar.Add(stars[0]);
                            //GetRelatedStar(result.RelatedStar,stars[0].Name,stars[0].ID);
                        }
                        result.ClearRedundant();
                        Response.Write(JsonConvert.SerializeObject(result));
                        return;
                    case "queryMovieByKeyword":
                        movies = movieQuery.QueryMovieByKeyword(query);
                        if (movies.Count == 0)
                        {
                            result.AddSingleMovie(movieQuery.QueryMovieByKeyword(query));
                        }
                        else if (movies.Count == 1)
                        {
                            result.ResultMovie.Add(movies[0]);
                            GetRelatedMovie(result.RelatedMovie, movies[0].Name, movies[0].ID);
                        }
                        else
                        {
                            result.AddSingleMovie(movies);
                        }
                        result.ClearRedundant();
                        Response.Write(JsonConvert.SerializeObject(result));
                        return;
                    case "queryMovieById":
                        BuildLayer(nodeCount, start, count, out indices, out counts, out layer);
                        movies = movieQuery.QueryMovieByID(Convert.ToInt32(query), 2, indices, counts);
                        if (movies.Count >= 1)
                        {
                            result.ResultMovie.Add(movies[0]);
                            //GetRelatedMovie(result.RelatedMovie,movies[0].Name,movies[0].ID);
                        }
                        result.ClearRedundant();
                        Response.Write(JsonConvert.SerializeObject(result));
                        return;
                    default:
                        throw new Exception("未支持类型");
                        Response.Write("[]");
                        return;
                }

            }

        }

        private void GetRelatedMovie(List<Movie> result,string name,int id)
        {
            //只有返回为一个的时候才找相关
            string movieInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/movieinfo");

            IMovieQuery movieQuery = new MovieQuery(movieInfo);
            List<Movie> related = movieQuery.QueryMovieByKeyword(name);
            foreach (Movie m in related)
            {
                if (m.ID != id)
                    result.Add(m);
            }
        

        }

        private void GetRelatedStar(List<Star> result, string name, int id)
        {
            //只有返回为一个的时候才找相关
            string starInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/starinfo");
            IStarQuery starQuery = new StarQuery(starInfo);
            List<Star> related = starQuery.QueryStarByKeyword(name);
            foreach (Star s in related)
            {
                if (s.ID != id)
                    result.Add(s);
            }

        }

        private void BuildLayer(string nodeCount, string start, string count, out int[] indices, out int[] counts, out int layer)
        {
            int nodes = 0;
            if (nodeCount != null)
            {
                try { nodes = Convert.ToInt32(nodeCount); }
                catch { }
            }

            int nStart = 0;
            if (start != null && start != "")
            {
                try { nStart = Convert.ToInt32(start); }
                catch { }
            }

            int nCount = 5;
            if (count != null && count != "")
            {
                try { nCount = Convert.ToInt32(count); }
                catch { }
            }

            if (nodes < NODE_LIMIT1)
            {
                layer = 2;
                indices = new int[] { nStart, 0 };
                counts = new int[] { nCount, 3 };
            }
            else
            {
                layer = 1;
                indices = new int[] { nStart };
                counts = new int[] { nCount };
            }

        }

    }
}
