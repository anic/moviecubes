using System;
using System.Collections.Generic;
using System.Text;
using MovieCube.Common.Data;

namespace MovieCube.Common.Interface
{
    public interface IQuery
    {
        TextResult QueryText(string query, int page, string context);
    }
}
