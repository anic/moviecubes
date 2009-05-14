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
            
            
            //暂时使用假的Query
            IStarQuery starQuery = new FakeStarQuery();
            IMovieQuery movieQuery = new FakeMovieQuery();
            List<Star> stars;
            List<Movie> movies;
            if (type != null && type == "queryKeys")
            {
                List<QueryKey> result = new List<QueryKey>();
                FakeDb db = new FakeDb();
                foreach (Star s in db.Stars)
                    result.Add(new QueryKey(s.Name, "明星"));
                foreach(Movie m in db.Movies)
                    result.Add(new QueryKey(m.Name,"电影"));
 

                Response.Write(JsonConvert.SerializeObject(result));
                return;

            }
            else if (type != null && query !=null)
            {
                switch(type)
                {
                    case "queryStarByName":
                        stars = starQuery.QueryStarByName(query);
                        Response.Write(JsonConvert.SerializeObject(stars));
                        return;
                    case "queryStarByMovie":
                        stars = starQuery.QueryStarByMovie(query);
                        Response.Write(JsonConvert.SerializeObject(stars));
                        return;
                    case "queryMovieByName":
                        movies = movieQuery.QueryMovieByName(query);
                        Response.Write(JsonConvert.SerializeObject(movies));
                        return;
                    case "queryMovieByStar":
                        movies = movieQuery.QueryMovieByName(query);
                        Response.Write(JsonConvert.SerializeObject(movies));
                        return;
                    default:
                        return;
                }
                
            }
            
        }

    }
}
