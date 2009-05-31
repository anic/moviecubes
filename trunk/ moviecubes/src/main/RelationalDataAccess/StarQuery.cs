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
    public class StarQuery : IStarQuery
    {

        string starInfo;

        public StarQuery(string starInfo)
        {
            this.starInfo = starInfo;
        }

        #region IStarQuery 成员

        public List<Star> QueryStarByKeyword(string keyword)
        {
            List<Star> resultStars = GetStarInfoByKeyword(keyword);

            if (resultStars.Count > 0)
            {
                //resultMovies根据rank、time等排序
                resultStars.Sort();

                //选取排名第一的Star进行扩展，扩展movie即可
                Star extendedStar = resultStars[0];

                int num = Math.Min(extendedStar.Movies.Count, Definition.Max_Surround_Node_Num);

                for (int i = 0; i < num; i++)
                {
                    extendedStar.Movies[i].Movie = CommonQuery.Instance.ExtendMovie(extendedStar.Movies[i].Movie, Definition.Max_Node_Layer - 1);
                }
            }

            return resultStars;
        }

        public List<Star> QueryStarByKeyword(string keyword, int index, int count)
        {
            List<Star> resultStars = GetStarInfoByKeyword(keyword);

            if (resultStars.Count > 0)
            {
                //resultMovies根据rank、time等排序
                resultStars.Sort();

                //选取排名第一的Star进行扩展，扩展movie即可
                Star extendedStar = resultStars[0];
                //int num = Math.Min(extendedStar.Movies.Count, Definition.Max_Surround_Node_Num);

                if (index >= extendedStar.Movies.Count)
                    return null;

                int num = Math.Min(count, extendedStar.Movies.Count - index);

                for (int i = 0; i < num; i++)
                {
                    extendedStar.Movies[i].Movie = CommonQuery.Instance.ExtendMovie(extendedStar.Movies[i].Movie, Definition.Max_Node_Layer - 1);
                }
            }

            return resultStars;
        }

        public List<Star> QueryStarByName(string name)
        {
            List<Star> resultStars = GetStarInfoByName(name);

            if (resultStars.Count > 0)
            {
                //resultMovies根据rank、time等排序
                resultStars.Sort();

                //选取排名第一的Star进行扩展，扩展movie即可
                Star extendedStar = resultStars[0];
                
                int num = Math.Min(extendedStar.Movies.Count, Definition.Max_Surround_Node_Num);

                for (int i = 0; i < num; i++)
                {
                    extendedStar.Movies[i].Movie = CommonQuery.Instance.ExtendMovie(extendedStar.Movies[i].Movie, Definition.Max_Node_Layer - 1);
                }
            }

            return resultStars;           
        }

        public List<Star> QueryStarByName(string name, int index, int count)
        {
            List<Star> resultStars = GetStarInfoByName(name, index, count);

            if (resultStars.Count > 0)
            {
                //resultMovies根据rank、time等排序
                resultStars.Sort();

                //选取排名第一的Star进行扩展，扩展movie即可
                Star extendedStar = resultStars[0];
                //int num = Math.Min(extendedStar.Movies.Count, Definition.Max_Surround_Node_Num);

                //if (index >= extendedStar.)
                //    return null;

                int num = Math.Min(count, extendedStar.Movies.Count - index);

                for (int i = 0; i < num; i++)
                {
                    extendedStar.Movies[i].Movie = CommonQuery.Instance.ExtendMovie(extendedStar.Movies[i].Movie, Definition.Max_Node_Layer - 1);
                }
            }

            return resultStars;
        }

        #endregion


        public List<Star> GetStarInfoByName(string name)
        {
            List<Star> result = new List<Star>();
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(starInfo);
            QueryParser queryParser = new QueryParser("Name", new StandardAnalyzer());

            queryParser.SetDefaultOperator(QueryParser.AND_OPERATOR);
            
            query = queryParser.Parse(name);
            hits = indexSearcher.Search(query);

            for (int i = 0; i < hits.Length(); i++)
            {
                Document hitDoc = hits.Doc(i);

                Star star = ConvertLuceneDocumentToStar(hitDoc, 0, Definition.Max_Surround_Node_Num);
                result.Add(star);
            }
            return result;
        }

        public List<Star> GetStarInfoByName(string name, int index, int count)
        {
            List<Star> result = new List<Star>();
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(starInfo);
            QueryParser queryParser = new QueryParser("Name", new StandardAnalyzer());

            queryParser.SetDefaultOperator(QueryParser.AND_OPERATOR);
            
            query = queryParser.Parse(name);
            hits = indexSearcher.Search(query);

            for (int i = 0; i < hits.Length(); i++)
            {
                Document hitDoc = hits.Doc(i);

                Star star = ConvertLuceneDocumentToStar(hitDoc, index, count);
                result.Add(star);
            }
            return result;
        }

        public List<Star> GetStarInfoByKeyword(string keyword)
        {
            List<Star> result = new List<Star>();
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(starInfo);
            QueryParser queryParser = new QueryParser("SearchField", new StandardAnalyzer());

            queryParser.SetDefaultOperator(QueryParser.AND_OPERATOR);

            query = queryParser.Parse(keyword);
            hits = indexSearcher.Search(query);

            for (int i = 0; i < hits.Length(); i++)
            {
                Document hitDoc = hits.Doc(i);

                Star star = ConvertLuceneDocumentToStar(hitDoc, 0, Definition.Max_Surround_Node_Num);
                result.Add(star);
            }
            return result;
        }

        public List<Star> GetStarInfoByKeyword(string keyword, int index, int count)
        {
            List<Star> result = new List<Star>();
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(starInfo);
            QueryParser queryParser = new QueryParser("SearchField", new StandardAnalyzer());

            queryParser.SetDefaultOperator(QueryParser.AND_OPERATOR);

            query = queryParser.Parse(keyword);
            hits = indexSearcher.Search(query);

            for (int i = 0; i < hits.Length(); i++)
            {
                Document hitDoc = hits.Doc(i);

                Star star = ConvertLuceneDocumentToStar(hitDoc, index, count);
                result.Add(star);
            }
            return result;
        }

        public Star GetStarInfoByID(int id)
        {
            Star star = null;
            Query query = null;
            Hits hits = null;
            IndexSearcher indexSearcher = new IndexSearcher(starInfo);
            QueryParser queryParser = new QueryParser("ID", new StandardAnalyzer());
            
            query = queryParser.Parse(id.ToString());
            hits = indexSearcher.Search(query);

            if (hits.Length() > 0)
            {
                Document hitDoc = hits.Doc(0);

                star = ConvertLuceneDocumentToStar(hitDoc, 0, Definition.Max_Surround_Node_Num);
            }
            return star;
        }

        private static Star ConvertLuceneDocumentToStar(Document doc, int index, int count)
        {
            Star result = new Star();

            result.ID = Convert.ToInt32(doc.Get("ID"));
            result.Name = doc.Get("Name");
            result.Rank = Convert.ToDouble(doc.Get("Rank"));
            result.Area = doc.Get("Area");

            Util.ProcessStringItem(doc.Get("Alias"), result.Alias);

            string[] movieIDs = doc.Get("MovieID").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] movieRoles = doc.Get("MovieRole").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] movieName = doc.Get("MovieName").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);


            int totalCount = Math.Min(movieIDs.Length, movieRoles.Length);


            if (movieIDs[movieIDs.Length - 1] == "")
                totalCount = totalCount - 1;


            result.TotalMovieNum = totalCount;

            if (index > totalCount)
                index = 0;

            int loopCount = Math.Min(count, totalCount - index);

            for (int i = index; i < index + loopCount; i++)
            {
                movieIDs[i] = movieIDs[i].Trim();
                if (movieIDs[i] != "")
                {
                    Movie addMovie = new Movie();
                    addMovie.ID = Convert.ToInt32(movieIDs[i].Trim());
                    addMovie.Name = movieName[i].Trim();
                    result.AddMovies(addMovie, movieRoles[i]);
                }
            }
           

            return result;
        }
    }
}
