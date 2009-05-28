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
            //GetMovieCommentXml(name);
            return GetMovieComment(name);
        }

        #endregion

        private List<MovieComment> GetMovieComment(string name)
        {
            List<MovieComment> result = new List<MovieComment>();
            string id = GetMovieIDByName(name);
            if(id != "")
            {
                string url = string.Format("http://api.douban.com/movie/subject/{0}/reviews?start-index=1&max-results={1}", id, Definition.Max_Comment_Num);

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

                    XmlDocument xmldoc = new XmlDocument();

                    xmldoc.Load(stream);

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                    nsmgr.AddNamespace("aa", "http://www.w3.org/2005/Atom");
                    XmlNodeList nodeList = xmldoc.SelectNodes("//aa:entry", nsmgr);

                    foreach (XmlNode xn in nodeList)
                    {
                        MovieComment mc = new MovieComment();
                        mc.Title = xn.SelectSingleNode("aa:title", nsmgr).InnerText;
                        mc.PublishDate = xn.SelectSingleNode("aa:published", nsmgr).InnerText;
                        mc.UpdateDate = xn.SelectSingleNode("aa:updated", nsmgr).InnerText;
                        mc.Summary = xn.SelectSingleNode("aa:summary", nsmgr).InnerText;

                        XmlNode authorNode = xn.SelectSingleNode("aa:author", nsmgr);

                        mc.AuthorLink = authorNode.SelectSingleNode("aa:link[@rel='self']", nsmgr).Attributes["href"].Value;
                        mc.AuthorIcon = authorNode.SelectSingleNode("aa:link[@rel='icon']", nsmgr).Attributes["href"].Value;
                        mc.AuthorName = authorNode.SelectSingleNode("aa:name", nsmgr).InnerText;

                        result.Add(mc);
                    }
                    return result;

                }
                catch (Exception e)
                {

                }
                finally
                {
                    if (reader != null) reader.Close();
                    if (stream != null) stream.Close();
                    if (response != null) response.Close();
                }
            }
            
            return result;
        }

        private string GetMovieIDByName(string name)
        {
            string url = string.Format("http://api.douban.com/movie/subjects?tag={0}&start-index=1&max-results=1", name);
            
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

                XmlDocument xmldoc = new XmlDocument();

                xmldoc.Load(stream);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("aa", "http://www.w3.org/2005/Atom");
                XmlNodeList nodeList = xmldoc.SelectNodes("//aa:entry", nsmgr);

                foreach (XmlNode xn in nodeList)
                {
                    XmlNode idnode = xn.SelectSingleNode("aa:id", nsmgr);
                    XmlNode titlenode = xn.SelectSingleNode("aa:title", nsmgr);

                    //http://api.douban.com/movie/subject/1424406
                    string idstring = idnode.InnerText;
                    int pos = idstring.LastIndexOf('/');
                    string id = idstring.Substring(pos + 1);
                    string title = titlenode.InnerText;

                    return id;
                }

            }
            catch (Exception e)
            {
 
            }
            finally
            {
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }

            return "";
        }
    }
}
