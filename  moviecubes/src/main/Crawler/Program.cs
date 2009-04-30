using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace MovieCube.Crawler
{
    class Program
    {

        static void Main(string[] args)
        {
            Program p = new Program();
            p.GetPages();
            //ExtarctInformationFromTxt();
        }

        public void GetPages()
        {

            int totalpage = 100;

            for (int i = 1; i <= totalpage; i++)
            {
                string url = string.Format("http://www.mdbchina.cn/cmd/combschstar.aspx?g=0&c=0&con=&s=2&page={0}", i);

                Crawler c = new Crawler(url);
                
                c.Parameter = i;
                c.OnSuccess += new CrawlerSuccessDelegate(this.HandleSuccess);
                c.OnFail += new CrawlerFailDelegate(this.HandleFailure);
                c.Start();
                //ExtractInfomation(buffer, i);

                System.Threading.Thread.Sleep(1 * 1000);
            }

        }

        private static void ExtarctInformationFromTxt()
        {
            //String s = "hahaha<ul class=\"mdbhb_content\">blablabla\r\nblablabla</ul>heiheihei";

            using (StreamWriter sw = new StreamWriter("persons.txt"))
            {
                for (int i = 1; i <= 100; i++)
                {
                    string filename = string.Format("pages/result_{0}.txt", i);

                    using (StreamReader sr = new StreamReader(filename))
                    {
                        String s = sr.ReadToEnd();
                        Match m = Regex.Match(s, "<ul class=\"mdbhb_content\">((?s:.*?))</ul>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        if (m.Success)
                        {
                            String content = m.Result("$1");

                            m = Regex.Match(content, "<A class=\"mdbhb_img\" href=\"/persons/([0-9]+)/\"(?s:.*?)<IMG alt=\"(.*)\" src=\"((?s:.*?))\"/>(?s:.*?)</A>", RegexOptions.IgnoreCase);

                            while (m.Success)
                            {
                                string personID = m.Result("$1");
                                string personName = m.Result("$2");
                                string personImage = m.Result("$3");

                                sw.WriteLine(string.Format("{0}\t{1}\t{2}", personID, personName, personImage));

                                m = m.NextMatch();
                            }
                        }
                    }
                }
            }
        }

        private static void ExtractInfomation(string buffer, int i)
        {
            using (StreamWriter sw = new StreamWriter(string.Format("persons/person_{0}.txt", i)))
            {
                sw.WriteLine("test");
                //<ul class="mdbhb_content"></ul>
                //string mdb_content = "";
                //Regex regex = new Regex(string.Format(@"<ul class='mdbhb_content'>*</ul>"));
                //while (regex.IsMatch(buffer))
                //{
                //    //regex.get
                //}
            }
        }

        private void HandleSuccess(Crawler crawler, StreamReader reader)
        {
            int i = (int)crawler.Parameter;
            string file = string.Format("pages/result_{0}.txt", i);
            string directory = file.Substring(file.LastIndexOf("/"));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if(File.Exists(file))
            {
                File.Create(file);
            }

            //string file = Util.Util.GenerateFilename(crawler.URL);
            StreamWriter outStream = new StreamWriter(file, true);
            while (!reader.EndOfStream)
                outStream.WriteLine(reader.ReadLine());
            outStream.Close();

            Console.WriteLine(string.Format("Page{0} is done.", i));
        }

        private void HandleFailure(Crawler crawler, Exception ex)
        {
            System.Console.WriteLine("下载失败：" + crawler.URL + "\n错误：" + ex);
        }
    }
}
