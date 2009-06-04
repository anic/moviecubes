using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MovieCube.GeneralSearchAdapter;
using MovieCube.Common.Data;

namespace GeneralSearch
{
    public partial class _Default : System.Web.UI.Page
    {
        //private GeneralQuery gq;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //gq = new GeneralQuery();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            GeneralQuery gq = new GeneralQuery();
            TextResult result = gq.QueryText(TextBox1.Text, 20, 0, null);

            Repeater1.DataSource = result.Pages;
            Repeater1.DataBind();
        }
    }
}
