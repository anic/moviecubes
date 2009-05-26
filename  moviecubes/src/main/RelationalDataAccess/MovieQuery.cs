using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using MovieCube.Common;
using MovieCube.Common.Data;
using MovieCube.Common.Interface;

using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;

using Document = Lucene.Net.Documents.Document;
using Field = Lucene.Net.Documents.Field;
using Directory = Lucene.Net.Store.Directory;

namespace MovieCube.RelationalDataAccess
{
    public class MovieQuery: IMovieQuery
    {
        string movieInfo;
        public MovieQuery(string movieInfo)
        {
            this.movieInfo = movieInfo;
        }

        #region IMovieQuery 成员

        public List<Movie> QueryMovieByKeyword(string keyword)
        {
            List<Movie> resultMovies = GetMovieInfoByKeyword(keyword);

            if (resultMovies.Count > 0)
            {
                //resultMovies根据rank、time等排序
                resultMovies.Sort();

                //选取排名第一的movie进行扩展，扩展star即可
                Movie extendedMovie = resultMovies[0];

                int num = Math.Min(extendedMovie.Stars.Count, Definition.Max_Surround_Node_Num);

                for (int i = 0; i < num; i++)
                {
                    extendedMovie.Stars[i].Star = CommonQuery.Instance.ExtendStar(extendedMovie.Stars[i].Star, Definition.Max_Node_Layer - 1);
                }
            }

            return resultMovies;
        }

        public List<Movie> QueryMovieByName(string name)
        {
            List<Movie> resultMovies = GetMovieInfoByName(name);

            if (resultMovies.Count > 0)
            {
                //resultMovies根据rank、time等排序
                resultMovies.Sort();

                //选取排名第一的movie进行扩展，扩展star即可
                Movie extendedMovie = resultMovies[0];

                int num = Math.Min(extendedMovie.Stars.Count, Definition.Max_Surround_Node_Num);

                for (int i = 0; i < num; i++)
                {
                    extendedMovie.Stars[i].Star = CommonQuery.Instance.ExtendStar(extendedMovie.Stars[i].Star, Definition.Max_Node_Layer - 1);
                }
            }

            return resultMovies;
        }

        public List<Movie> QueryMovieByActor(string actorName)
        {
            return null;
        }

        #endregion

        public List<Movie> GetMovieInfoByName(string name)
        {
            List<Movie> result = new List<Movie>();
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(movieInfo);
            QueryParser queryParser = new QueryParser("Name", new StandardAnalyzer());
            query = queryParser.Parse(name);
            hits = indexSearcher.Search(query);

            for (int i = 0; i < hits.Length(); i++)
            {
                Document hitDoc = hits.Doc(i);

                Movie movie = ConvertLuceneDocumentToMovie(hitDoc, true);
                result.Add(movie);
            }
            return result;
        }

        public List<Movie> GetMovieInfoByKeyword(string keyword)
        {
            List<Movie> result = new List<Movie>();
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(movieInfo);
            QueryParser queryParser = new QueryParser("SearchField", new StandardAnalyzer());
            query = queryParser.Parse(keyword);
            hits = indexSearcher.Search(query);

            for (int i = 0; i < hits.Length(); i++)
            {
                Document hitDoc = hits.Doc(i);

                Movie movie = ConvertLuceneDocumentToMovie(hitDoc, true);
                result.Add(movie);
            }
            return result;
        }

        public Movie GetMovieInfoByID(int id)
        {
            Movie movie = null;
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(movieInfo);
            QueryParser queryParser = new QueryParser("ID", new StandardAnalyzer());
            query = queryParser.Parse(id.ToString());
            hits = indexSearcher.Search(query);

            if (hits.Length() > 0)
            {
                Document hitDoc = hits.Doc(0);

                movie = ConvertLuceneDocumentToMovie(hitDoc, true);
            }
            return movie;
        }

        private static Movie ConvertLuceneDocumentToMovie(Document doc, bool limited)
        {
            Movie result = new Movie();

            result.ID = Convert.ToInt32(doc.Get("ID"));
            result.Name = doc.Get("Name");
            result.Rank = Convert.ToDouble(doc.Get("Rank"));
            result.Area = doc.Get("Area");
            result.Language = doc.Get("Language");
            result.Time = doc.Get("Time");

            Util.ProcessStringItem(doc.Get("Alias"), result.Alias);
            Util.ProcessStringItem(doc.Get("Type"), result.Type);

            string[] starIDs = doc.Get("StarID").Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries);
            string[] starRoles = doc.Get("StarRole").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] starName = doc.Get("StarName").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);


            if (starIDs.Length == starRoles.Length && starRoles.Length == starName.Length)
            {
                int count = starIDs.Length;
                if (limited)
                    count = Math.Min(count, Definition.Max_Surround_Node_Num);

                for (int i = 0; i < count; i++)
                {
                    starIDs[i] = starIDs[i].Trim();
                    if (starIDs[i] != "")
                    {
                        Star addStar = new Star();
                        addStar.ID = Convert.ToInt32(starIDs[i].Trim());
                        addStar.Name = starName[i].Trim();
                        result.AddStar(addStar, starRoles[i]);
                    }
                }
            }
            else
            {
                throw new Exception("format error");
            }
 
            return result;
        }
    }
}
