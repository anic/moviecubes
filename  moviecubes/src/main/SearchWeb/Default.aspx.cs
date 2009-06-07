using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SearchWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected string query;
        protected string encodeQuery;
        protected string queryPageUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type == "1")
                {
                    this.rdbRelation.Checked = false;
                    this.rdbPage.Checked = true;
                }
                else
                {
                    this.rdbRelation.Checked = true;
                    this.rdbPage.Checked = false;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string query = TextBox1.Text;
            string encodeQuery = HttpUtility.UrlEncode(query);
            if (this.rdbPage.Checked)
                Response.Redirect("Search.aspx?query=" + encodeQuery);
            else
                Response.Redirect("RelationPage.aspx?query=" + encodeQuery);
        }
    }
}
