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
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// 大小，文本格式，如10K
        /// </summary>
        public string Size = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary
        {
            get;
            set;
        }

        public string Cache
        {
            get;
            set;
        }

        public string Explain
        {
            get;
            set;
        }

        public string More
        {
            get;
            set;
        }
    }
}
