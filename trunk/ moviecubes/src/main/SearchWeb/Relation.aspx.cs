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
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = System.Web.HttpContext.Current.Request.Form["type"];
            string query = System.Web.HttpContext.Current.Request.Form["query"];

            string starInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/starinfo");
            string movieInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/movieinfo");
            //暂时使用假的Query
            //IStarQuery starQuery = new FakeStarQuery();
            //IMovieQuery movieQuery = new FakeMovieQuery();
            IMovieQuery movieQuery = new MovieQuery(movieInfo);
            IStarQuery starQuery = new StarQuery(starInfo);


            List<Star> stars;
            List<Movie> movies;
            if (type != null && type == "loadKeys")
            {
                List<QueryKey> result = new List<QueryKey>();
                //FakeDb db = new FakeDb();
                //foreach (Star s in db.Stars)
                //    result.Add(new QueryKey(s.Name, "明星"));
                //foreach(Movie m in db.Movies)
                //    result.Add(new QueryKey(m.Name,"电影"));
 

                Response.Write(JsonConvert.SerializeObject(result));
                return;

            }
            else if (type != null && query != null)
            {
                switch (type)
                {
                    case "queryByKey":
                        //先通过名称查找电影
                        //movies = movieQuery.QueryMovieByName(query);
                        //if (movies.Count != 0)
                        //{
                        //    Response.Write(JsonConvert.SerializeObject(movies));
                        //    return;
                        //}

                        ////没有结果的话，通过名称查找明星
                        //stars = starQuery.QueryStarByName(query);
                        //if (stars.Count != 0)
                        //{
                        //    Response.Write(JsonConvert.SerializeObject(stars));
                        //    return;
                        //}

                        //没有结果的话，通过关键字查找电影
                        //movies = movieQuery.QueryMovieByKeyword(query);
                        //if (movies.Count != 0)
                        //{
                        //    Response.Write(JsonConvert.SerializeObject(movies));
                        //    return;
                        //}

                        ////没有结果的话，通过关键字查找明星
                        //stars = starQuery.QueryStarByKeyword(query);
                        //if (stars.Count != 0)
                        //{
                        //    Response.Write(JsonConvert.SerializeObject(stars));
                        //    return;
                        //}

                        Response.Write("[]");
                        return;

                    case "queryStarByName":
                        //stars = starQuery.QueryStarByName(query);
                        stars = starQuery.QueryStarByKeyword(query);
                        Response.Write(JsonConvert.SerializeObject(stars));
                        return;
                    case "queryMovieByStar":
                        //movies = movieQuery.QueryMovieByActor(query);
                        movies = movieQuery.QueryMovieByKeyword(query);
                        Response.Write(JsonConvert.SerializeObject(movies));
                        return;
                    case "queryMovieByName":
                        movies = movieQuery.QueryMovieByName(query);
                        Response.Write(JsonConvert.SerializeObject(movies));
                        return;
                    case "queryStarByMovie":
                        stars = starQuery.QueryStarByMovie(query);
                        Response.Write(JsonConvert.SerializeObject(stars));
                        return;
                    default:
                        return;
                }

            }
            
        }

    }
}
