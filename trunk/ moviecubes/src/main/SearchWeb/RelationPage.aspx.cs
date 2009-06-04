using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCube.RelationalDataAccess;
using MovieCube.Common.Data;
using Newtonsoft.Json;
using System.Text;

namespace MovieCube.SearchWeb
{
    public partial class RelationPage : System.Web.UI.Page
    {
        protected string query = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            query = Request.QueryString["query"];
            if (query == null)
                query = "";
            else
                query = HttpUtility.UrlDecode(query, Encoding.UTF8);
        }
    }
}
