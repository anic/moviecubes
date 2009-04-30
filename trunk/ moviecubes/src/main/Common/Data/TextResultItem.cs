using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCube.Common.Data
{
    public class TextResultItem
    {
        /// <summary>
        /// Id号
        /// </summary>
        public int Id = 0;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title = string.Empty;

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url = string.Empty;

        /// <summary>
        /// 大小，文本格式，如10K
        /// </summary>
        public string Size = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract = string.Empty;
    }
}
