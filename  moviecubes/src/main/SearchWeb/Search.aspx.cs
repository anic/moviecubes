﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MovieCube.GeneralSearchAdapter;
using MovieCube.Common.Data;
using MovieCube.Common.Interface;
using MovieCube.RelationalDataAccess;

namespace MovieCube.SearchWeb
{
    public partial class Search : System.Web.UI.Page
    {
        private const int HITSPERPAGE = 10;
        private const int MAXFOOTERNAVI = 9;
        private const int MAXRELATIVEMOVIE = 7;
        private const int MAXRELATIVESTAR = 7;

        protected string query;
        protected string encodeQuery;
        protected string queryPageUrl;

        protected FooterNaviItem prevPage;
        protected FooterNaviItem nextPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            prevPage = new FooterNaviItem();
            prevPage.Id = "";

            nextPage = new FooterNaviItem();
            nextPage.Id = "";

            if (!IsPostBack)
            {


                RelativeMoviePanel.Style.Add("display", "none");
                RelativeStarsPanel.Style.Add("display", "none");
                RecordPanel.Style.Add("display", "none");
                NoResultPanel.Style.Add("display", "none");

                query = Request.QueryString["query"];
                if (query == null || query.Trim() == "")
                {
                    Response.Redirect("Default.aspx?type=1");
                    return;
                }


                encodeQuery = HttpUtility.UrlEncode(query);

                string path = HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
                path = path.EndsWith("/") ? path.Substring(0, path.Length - 1) : path;
                queryPageUrl = "http://" + path + "/Search.aspx";

                string hits = Request.QueryString["hitsPerPage"];
                string start = Request.QueryString["start"];
                string perSite = Request.QueryString["hitsPerSite"];

                if (query == null || query.Equals(""))
                {
                    RelativeStarsPanel.Style.Add("display", "none");
                    RelativeMoviePanel.Style.Add("display", "none");

                    RecordPanel.Style.Add("display", "none");
                    return;
                }
                

                int nHits = HITSPERPAGE;
                if (hits != null)
                    nHits = Convert.ToInt32(hits);

                int nStart = 0;
                if (start != null)
                    nStart = Convert.ToInt32(start);

                DoQuery(query, nHits, nStart, perSite);
                TextBox1.Text = query;
            }
            
        }

        private void DoQuery(string query, int hitsPerPage, int start, string hitsPerSite)
        {
            GeneralQuery gq = new GeneralQuery();
            TextResult result = gq.QueryText(query, hitsPerPage, start, null);

            Repeater1.DataSource = result.Pages;
            Repeater1.DataBind();

            List<Movie> movies;
            List<Star> stars;

            if (result.TotalPages > 0)
            {
                NoResultPanel.Style.Add("display", "none");

                Label1.Text = " 约有 " + result.TotalPages.ToString() + " 项符合 "
                + query + " 的查询结果，以下是第" + result.StartPage.ToString()
                + "-" + (result.EndPage).ToString() + " 项";
                Label1.DataBind();

                movies = RelativeMovieQuery(query);
                Repeater4.DataSource = movies.GetRange(0, movies.Count > MAXRELATIVEMOVIE ? MAXRELATIVEMOVIE : movies.Count);
                Repeater4.DataBind();

                if (movies.Count == 0)
                    RelativeMoviePanel.Style.Add("display", "none");
                else
                {
                    RelativeMoviePanel.Style.Remove("display");
                    //List<MovieComment> comments = RelativeCommentQuery(query);
                }

                stars = RelativeStarQuery(query);
                Repeater3.DataSource = stars.GetRange(0, stars.Count > MAXRELATIVESTAR ? MAXRELATIVESTAR : stars.Count);
                Repeater3.DataBind();

                if (stars.Count == 0)
                {
                    RelativeStarsPanel.Style.Add("display", "none");
                }
                else
                {
                    RelativeStarsPanel.Style.Remove("display");    
                }

                RecordPanel.Style.Remove("display");

                List<FooterNaviItem> footer = GenerateFooterNavi(result.TotalPages, result.StartPage, result.EndPage, result.Context);

                if (footer != null)
                {
                    Repeater2.DataSource = footer;
                    Repeater2.DataBind();
                }

            }
            else
            {
                RecordPanel.Style.Add("display", "none");
                RelativeStarsPanel.Style.Add("display", "none");
                RelativeMoviePanel.Style.Add("display", "none");
                NoResultPanel.Style.Remove("display");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            query = TextBox1.Text;
            encodeQuery = HttpUtility.UrlEncode(query);
            DoQuery(query, HITSPERPAGE, 0, "0");
            TextBox1.Text = query;
        }

        private List<FooterNaviItem> GenerateFooterNavi(int total, int start, int end, string url)
        {
            int curSlot = (start + HITSPERPAGE - 1) / HITSPERPAGE;
            int startSlot;
            int endSlot;
            int lastSlot = -1;
            int nextSlot = -1;


            int nSlot = (total + HITSPERPAGE - 1) / HITSPERPAGE;
            //nSlot = nSlot > MAXFOOTERNAVI ? MAXFOOTERNAVI : nSlot;

            if (nSlot == 0)
                return null;

            endSlot = curSlot + (MAXFOOTERNAVI - 1) / 2;
            endSlot = endSlot > nSlot ? nSlot : endSlot;

            startSlot = curSlot - (MAXFOOTERNAVI - (endSlot - curSlot + 1));
            startSlot = startSlot < 1 ? 1 : startSlot;

            lastSlot = curSlot - 1 >= 1 ? curSlot - 1 : lastSlot;
            nextSlot = curSlot + 1 <= endSlot ? curSlot + 1 : endSlot;

            string path = HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            path = path.EndsWith("/") ? path.Substring(0, path.Length - 1) : path;

            if (lastSlot < 1)
                prevPage.Id = "";
            else 
            {
                prevPage.Id = lastSlot.ToString();
                //prevPage.Url = url + "&hitsPerPage=" + HITSPERPAGE.ToString() + "&start=" + ((lastSlot - 1) * HITSPERPAGE + 1).ToString();
                prevPage.Url = "http://" + path + "/Search.aspx?query=" + query + "&hitsPerPage=" + HITSPERPAGE.ToString() + "&start=" + ((lastSlot - 1) * HITSPERPAGE).ToString();
            }

            if (nextSlot == curSlot)
                nextPage.Id = "";
            else 
            {
                nextPage.Id = nextSlot.ToString();
                //nextPage.Url = url + "&hitsPerPage=" + HITSPERPAGE.ToString() + "&start=" + ((nextSlot - 1) * HITSPERPAGE + 1).ToString();
                nextPage.Url = "http://" + path + "/Search.aspx?query=" + query + "&hitsPerPage=" + HITSPERPAGE.ToString() + "&start=" + ((nextSlot - 1) * HITSPERPAGE).ToString();
            }
            

            List<FooterNaviItem> footer = new List<FooterNaviItem>();
            FooterNaviItem item;

            
            for (int i = startSlot; i <= endSlot; i++)
            {
                item = new FooterNaviItem();
                item.Id = i.ToString();
                item.IsCurrent = (i == curSlot);
                item.Url = "http://" + path + "/Search.aspx?query=" + query + "&hitsPerPage=" + HITSPERPAGE.ToString() + "&start=" + ((i - 1) * HITSPERPAGE).ToString();

                footer.Add(item);
            }

            return footer;
        }

        public List<Movie> RelativeMovieQuery(string query)
        {
            string movieInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/movieinfo");

            IMovieQuery movieQuery = new MovieQuery(movieInfo);

            List<Movie> result = movieQuery.QueryMovieAllInfoByName(query);
            if (result.Count < 1)
            {
                result = movieQuery.QueryMovieAllInfoByKeyword(query);
            }

            return result;
        }

        private List<Star> RelativeStarQuery(string query)
        {
            string starInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/starinfo");

            IStarQuery starQuery = new StarQuery(starInfo);

            List<Star> result = starQuery.QueryStarAllInfoByName(query);

            if (result.Count < 1)
                result = starQuery.QueryStarAllInfoByKeyword(query);

            return result;
        }

        private List<MovieComment> RelativeCommentQuery(string name)
        {
            string starInfo = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/movieinfo");

            IMovieComment movieCommentQuery = new DoubanComment();
            return movieCommentQuery.GetMovieCommentByName(name);
        }
    }
}
