﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    /// <summary>
    /// 返回给flash的结果集合
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RelationResult
    {
        
        public RelationResult()
        {
            this.RelatedMovie = new List<Movie>();
            this.RelatedStar = new List<Star>();
            this.ResultMovie = new List<Movie>();
            this.ResultStar = new List<Star>();
            this.RelatedCount = 0;
        }

        [JsonProperty]
        public List<Movie> ResultMovie { get; set; }

        [JsonProperty]
        public List<Movie> RelatedMovie { get; set; }

        [JsonProperty]
        public List<Star> ResultStar { get; set; }

        [JsonProperty]
        public List<Star> RelatedStar { get; set; }

        [JsonProperty]
        public int RelatedCount { get; set; }

        public void HandleBeforeTransfer()
        {
            int max = Definition.Max_Result_Once;
            if (ResultMovie.Count > max)
            {
                List<Movie> other = ResultMovie.GetRange(max, ResultMovie.Count - max);
                RelatedMovie.InsertRange(0, other);
                ResultMovie.RemoveRange(max, ResultMovie.Count - max);
            }

            if (ResultStar.Count > max)
            {
                List<Star> other = ResultStar.GetRange(max, ResultStar.Count - max);
                RelatedStar.InsertRange(0, other);
                ResultStar.RemoveRange(max, ResultStar.Count - max);
            }


            List<Movie> removedMovie = new List<Movie>();
            foreach (Movie m in RelatedMovie)
                if (hasResultMovie(m))
                    removedMovie.Add(m);

            //删除相关电影中重复的部分
            foreach (Movie m in removedMovie)
                RelatedMovie.Remove(m);

    
            List<Star> removedStar = new List<Star>();
            foreach (Star s in RelatedStar)
                if (hasResultStar(s))
                    removedStar.Add(s);


            //删除相关明星中的重复部分
            foreach (Star s in removedStar)
                RelatedStar.Remove(s);

            //记录相关项总数
            this.RelatedCount = RelatedMovie.Count + RelatedStar.Count;

            //限制相关项个数
            if (RelatedStar.Count > Definition.Related_Count)
                RelatedStar = RelatedStar.GetRange(0, Definition.Related_Count);

            if (RelatedMovie.Count > Definition.Related_Count)
                RelatedMovie = RelatedMovie.GetRange(0, Definition.Related_Count);

            foreach (Movie m in RelatedMovie)
                m.Stars.Clear();

            foreach (Star s in RelatedStar)
                s.Movies.Clear();
        }

        private bool hasResultMovie(Movie movie)
        {
            foreach (Movie m in ResultMovie)
                if (m.ID == movie.ID)
                    return true;
            return false;
        }

        private bool hasResultStar(Star star)
        {
            foreach (Star s in ResultStar)
                if (s.ID == star.ID)
                    return true;
            return false;
        }

        public void AddSingleStar(List<Star> stars)
        {
            if (stars.Count > 0)
            {
                this.ResultStar.Add(stars[0]);
                for (int i = 1; i < stars.Count; ++i)
                    this.RelatedStar.Add(stars[i]);
            }
        }

        public void AddSingleMovie(List<Movie> movies)
        {
            if (movies.Count > 0)
            {
                this.ResultMovie.Add(movies[0]);
                for (int i = 1; i < movies.Count; ++i)
                    this.RelatedMovie.Add(movies[i]);
            }
        }
    }
}
