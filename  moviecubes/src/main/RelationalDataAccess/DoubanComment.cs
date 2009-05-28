using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;

using MovieCube.Common.Data;
using MovieCube.Common.Interface;


namespace MovieCube.RelationalDataAccess
{
    public class DoubanComment : IMovieComment
    {
        
        #region IMovieComment 成员

        public List<MovieComment> GetMovieCommentByName(string name)
        {
            GetMovieCommentXml(name);
            return null;
        }

        #endregion

        private string GetMovieCommentXml(string name)
        {
            string id = GetMovieIDByName(name);
            if(id != "")
            {
                string url = string.Format("http://api.douban.com/movie/subject/{0}/reviews?start-index=1&max-results={1}", id, Definition.Max_Comment_Num);

                // xmlStream = GetPages(url);
                //XmlDocument xmldoc = new XmlDocument();
                //if (xmlStream != null)
                //{
                //    xmldoc.Load(xmlStream);
                //}
            }
            
            return "";
        }

        private string GetMovieIDByName(string name)
        {
            string url = string.Format("http://api.douban.com/movie/subjects?tag={0}&start-index=1&max-results=2", name);

            //string xmlstring = GetPages(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(response.GetResponseStream());

            //if (xmlstring != "")
            {
                //xmldoc.LoadXml(xmlstring);

                XmlNodeList nodeList = xmldoc.SelectNodes("feed/entry");
                XmlNode root = xmldoc.ChildNodes[1];

                foreach(XmlNode xn in root.ChildNodes)
                {
                    if (xn.Name == "entry")
                    {
                        XmlNode idnode = xn.SelectSingleNode(@"/id");
                        XmlNode titlenode = xn.SelectSingleNode("/title");
                    }
                }
            }

            return "";
        }

        private static string GetPages(string url)
        {
            Uri uri = null;
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                url = HttpUtility.HtmlEncode(url);
                uri = new Uri(url);

                request = (HttpWebRequest)WebRequest.Create(uri);

                response = request.GetResponse();
                stream = response.GetResponseStream();

                
                string buffer = "";
                string line;

                reader = new StreamReader(stream, System.Text.Encoding.UTF8);

                while ((line = reader.ReadLine()) != null)
                {
                    buffer += line + "\r\n";
                }
                return buffer;
            }
            catch (WebException e)
            {
                //System.Console.WriteLine("下载失败，错误：" + e);
            }
            catch (IOException e)
            {
                //System.Console.WriteLine("下载失败，错误：" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }
            return null;
        }
    }
}
