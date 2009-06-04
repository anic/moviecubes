using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MovieCube.RelationalDataAccess;
using System.IO;
using SearchWeb;

namespace MovieCube.SearchWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            CommonQuery.Instance = new CommonQuery(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/starinfo"),
                System.Web.HttpContext.Current.Server.MapPath("~/App_Data/movieinfo"));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}