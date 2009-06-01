using System;
using System.Collections.Generic;
using System.Linq;
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

            List<Star> stars;
            List<Movie> movies;
            if (type != null && type == "loadKeys")
            {
                List<QueryKey> result = KeywordHelper.Instance.GetTipList(query);
                Response.Write(JsonConvert.SerializeObject(result));
                return;
            }
            else if (type != null && query != null)
            {
                switch (type)
                {
                    case "queryByKey":
                        //先通过名称查找电影
                        movies = movieQuery.QueryMovieByName(query);
                        if (movies.Count != 0)
                        {
                            Response.Write(JsonConvert.SerializeObject(movies));
                            return;
                        }

                        //没有结果的话，通过名称查找明星
                        stars = starQuery.QueryStarByName(query);
                        if (stars.Count != 0)
                        {
                            Response.Write(JsonConvert.SerializeObject(stars));
                            return;
                        }

                        //没有结果的话，通过关键字查找电影
                        movies = movieQuery.QueryMovieByKeyword(query);
                        if (movies.Count != 0)
                        {
                            Response.Write(JsonConvert.SerializeObject(movies));
                            return;
                        }

                        //没有结果的话，通过关键字查找明星
                        stars = starQuery.QueryStarByKeyword(query);
                        if (stars.Count != 0)
                        {
                            Response.Write(JsonConvert.SerializeObject(stars));
                            return;
                        }

                        Response.Write("[]");
                        return;

                    case "queryStarByKeyword":
                        stars = starQuery.QueryStarByKeyword(query);
                        Response.Write(JsonConvert.SerializeObject(stars));
                        return;
                    case "queryStarById":
                        BuildLayer(nodeCount, start, count, out indices, out counts, out layer);
                        stars = starQuery.QueryStarByID(Convert.ToInt32(query), 2, indices, counts);
                        Response.Write(JsonConvert.SerializeObject(stars));
                        return;
                    case "queryMovieByKeyword":
                        movies = movieQuery.QueryMovieByKeyword(query);
                        Response.Write(JsonConvert.SerializeObject(movies));
                        return;
                    case "queryMovieById":
                        BuildLayer(nodeCount,start,count,out indices,out counts,out layer);
                        movies = movieQuery.QueryMovieByID(Convert.ToInt32(query), 2, indices, counts);
                        Response.Write(JsonConvert.SerializeObject(movies));
                        return;
                    default:
                        throw new Exception("未支持类型");
                        Response.Write("[]");
                        return;
                }

            }

        }

        private void BuildLayer(string nodeCount,string start,string count,out int[] indices,out int[] counts,out int layer)
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
                indices = new int[] { nStart};
                counts = new int[] { nCount};
            }

        }

    }
}
