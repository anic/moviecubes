using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCube.Crawler.Util
{
    class Util
    {
        /// <summary>
        /// 生成路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerateFilename(string url)
        {
            int index1 = url.IndexOf("//");
            int index2 = url.LastIndexOf("/");
            if (index1 == -1 || index2 == -1)
                return string.Empty;
            else
                return url.Substring(index1 + 1, index2 - index1);
        }
    }
}
