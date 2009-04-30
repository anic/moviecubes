using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace MovieCube.Crawler
{
    public delegate void CrawlerSuccessDelegate(Crawler crawler, StreamReader reader);

    public delegate void CrawlerFailDelegate(Crawler crawler, Exception ex);

    public class Crawler
    {
        public CrawlerSuccessDelegate OnSuccess;

        public CrawlerFailDelegate OnFail;


        string url;

        /// <summary>
        /// 获得当前的url
        /// </summary>
        public string URL
        {
            get { return url; }
        }

        /// <summary>
        /// 其他参数
        /// </summary>
        public object Parameter
        {
            get;
            set;
        }


        public Crawler(string url)
        {
            this.url = url;

        }

        /// <summary>
        /// 开始爬虫
        /// </summary>
        public void Start()
        {
            Uri uri = new Uri(url);
            WebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                response = request.GetResponse();
                stream = response.GetResponseStream();

                reader = new StreamReader(stream, System.Text.Encoding.UTF8);

                if (OnSuccess != null)
                    OnSuccess(this, reader);

            }
            catch (Exception ex)
            {
                if (OnFail != null)
                    OnFail(this, ex);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

        }
    }
}
