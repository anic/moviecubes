using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCube.SearchWeb
{
    public class FooterNaviItem
    {
        public string Id
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public bool IsCurrent
        {
            get;
            set;
        }
    }
}
