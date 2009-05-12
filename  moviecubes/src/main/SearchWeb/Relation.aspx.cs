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

namespace MovieCube.SearchWeb
{
    public partial class Relation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string star = System.Web.HttpContext.Current.Request.Form["star"];
            if (star != null)
            {
                //CreateXml();//创建Xml的方法，可使用XmlTextWriter、XmlDocument ，或者直接读取Xml文件等待
                StarQuery query = new StarQuery();
                List<Star> stars = query.QueryStarByName(star);
                string result = JsonConvert.SerializeObject(stars).ToString();
                Response.Write(result);
            }
        }

    }
}
