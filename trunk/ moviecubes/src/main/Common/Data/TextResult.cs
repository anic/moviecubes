using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCube.Common.Data
{
    /// <summary>
    /// 文本搜索结果
    /// </summary>
    public class TextResult
    {
        /// <summary>
        /// 结果页面
        /// </summary>
        public List<TextResultItem> Pages = new List<TextResultItem>();

        /// <summary>
        /// 结果描述
        /// </summary>
        public string Description = string.Empty;

        /// <summary>
        /// 搜索关键词，用于高亮内容
        /// </summary>
        public List<string> QueryKeys = new List<string>();

        /// <summary>
        /// 总共页数
        /// </summary>
        public int TotalPages = 0;

        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPage = 0;

        /// <summary>
        /// 搜索其他的上下文，可能是一些用户的设置，如每页多少个，用什么排序等
        /// </summary>
        public string Context = string.Empty;
    }
}
