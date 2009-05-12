using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCube.RelationalDataAccess;
using MovieCube.Common.Data;
using Newtonsoft.Json;

namespace MovieCube.SearchWeb
{
    public partial class RelationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(MovieCube.SearchWeb.RelationPage));
        }

        [AjaxPro.AjaxMethod]
        public string GetMovieByName(string name)
        {
            MovieQuery query = new MovieQuery();
            List<Movie> movies = query.QueryMovieByName(name);
            string result = JsonConvert.SerializeObject(movies);
            return result;
        }

        [AjaxPro.AjaxMethod]
        public string GetStarByName(string name)
        {
            StarQuery query = new StarQuery();
            List<Star> stars = query.QueryStarByName(name);
            string result = JsonConvert.SerializeObject(stars);
            return result;
        }
    }
}
