using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;

using System.Diagnostics;

using MovieCube.Common;
using MovieCube.Common.Data;
using MovieCube.Common.Interface;
using MovieCube.RelationalDataAccess;


namespace MovieCube.GeneralSearchAdapter
{
    public class GeneralQuery : IQuery
    {
        private string nutchUrl = "http://166.111.80.110:8888/nutch-1.0";
        private string queryPage = "query.jsp";

        private string httpRequest;

        public GeneralQuery()
        { }

        public GeneralQuery(string nutchUrl, string queryPage)
        {
            this.nutchUrl = nutchUrl;
            this.queryPage = queryPage;
        }


        public TextResult QueryText(string query, int hitPages, int start, string context)
        {
            string responseXml = RemoteQuery(query, hitPages, start);

            return PareseResponse(responseXml);
        }


        private string RemoteQuery(string query, int hitPages, int start)
        {
            string[] originKeywords;
            string keywords = "";
            
            bool isFirst = true;
            if (query != null)
            {
                originKeywords = query.Split(' ');
                foreach (string s in originKeywords)
                {
                    if (s.Equals(" "))
                        continue;

                    if (isFirst)
                    {
                        keywords += ("\"" + s + "\"");
                        isFirst = false;
                    }
                    else
                        keywords += ("+\"" + s + "\"");
                }
            }

            httpRequest = nutchUrl + "/" + queryPage + "?" + "query=" + keywords;
            httpRequest += "&hitsPerSite=0";
            httpRequest += "&start=" + start.ToString();

            WebRequest request = WebRequest.Create(httpRequest + "&hitsPerPage=" + hitPages.ToString());

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Display the status.
            Console.WriteLine(response.StatusDescription);

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        private TextResult PareseResponse(string response)
        {
            TextResult result = new TextResult();
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(response);

            result.TotalPages = Convert.ToInt32(xmlDoc.GetElementsByTagName("total")[0].InnerText);
            result.StartPage = Convert.ToInt32(xmlDoc.GetElementsByTagName("start")[0].InnerText);
            result.EndPage = Convert.ToInt32(xmlDoc.GetElementsByTagName("end")[0].InnerText);

            XmlNodeList records = xmlDoc.GetElementsByTagName("Record");

            foreach (XmlNode node in records)
            {
                TextResultItem item = new TextResultItem();
                foreach (XmlNode attr in node.ChildNodes)
                {
                    if (attr.Name.Equals("t"))
                        item.Title = Util.Base64Decode(attr.InnerText);
                    else if (attr.Name.Equals("u"))
                        item.Url = Util.Base64Decode(attr.InnerText);
                    else if (attr.Name.Equals("summary"))
                        item.Summary = Util.Base64Decode(attr.InnerText);
                    else if (attr.Name.Equals("cache"))
                        item.Cache = nutchUrl + "/" + Util.Base64Decode(attr.InnerText).Substring(3);
                    else if (attr.Name.Equals("explain"))
                        item.Explain = nutchUrl + "/" + Util.Base64Decode(attr.InnerText).Substring(3);
                    else if (attr.Name.Equals("more"))
                        item.More = nutchUrl + "/" + Util.Base64Decode(attr.InnerText).Substring(3);
                    else
                        ;
    
                }

                result.Pages.Add(item);
            }

            result.Context = httpRequest;

            return result; 
        }


    }
}
