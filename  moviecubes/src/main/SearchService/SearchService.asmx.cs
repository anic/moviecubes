using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

using MovieCube.Common.Interface;
using MovieCube.Common.Data;

namespace MovieCube.SearchService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class SearchService : System.Web.Services.WebService,IQuery
    {

        [WebMethod]
        public TextResult QueryText(string query, int page, string context)
        {
            return new TextResult();
        }

        [WebMethod]
        public RelationResult QueryRelation(string item)
        {
            return new RelationResult();
        }
    }
}
