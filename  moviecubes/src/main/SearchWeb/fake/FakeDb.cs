using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using MovieCube.Common.Interface;
using MovieCube.Common.Data;

namespace SearchWeb
{
    public class FakeDb
    {
        List<Movie> movies = new List<Movie>();
        List<Star> stars = new List<Star>();

        public List<Movie> Movies { get { return movies; } }
        public List<Star> Stars { get { return stars; } }

        public FakeDb()
        {
            Movie m1 = new Movie();
            m1.Name = "无间道";
            m1.ID = 1001;


            Star s1 = new Star();
            s1.Name = "刘德华";
            s1.ID = 1;

            Star s2 = new Star();
            s2.Name = "梁朝伟";
            s2.ID = 2;

            Star s3 = new Star();
            s3.Name = "黄秋生";
            s3.ID = 3;

            Star s4 = new Star();
            s4.Name = "曾志伟";
            s4.ID = 4;

            m1.Actors.Add(s1);
            m1.Actors.Add(s2);
            m1.Actors.Add(s3);
            m1.Actors.Add(s4);


            Movie m2 = new Movie();
            m2.Name = "暗战";
            m2.ID = 1002;

            Star s5 = new Star();
            s5.Name = "刘青云";
            s5.ID = 5;

            m2.Actors.Add(s1);
            m2.Actors.Add(s5);


            Movie m3 = new Movie();
            m3.Name = "无间道3";
            m3.ID = 1003;

            Star s6 = new Star();
            s6.Name = "黎明";
            s6.ID = 6;


            m3.Actors.Add(s1);
            m3.Actors.Add(s6);
            m3.Actors.Add(s2);

            this.stars.Add(s1);
            this.stars.Add(s2);
            this.stars.Add(s3);
            this.stars.Add(s4);
            this.stars.Add(s5);
            this.stars.Add(s6);

            this.movies.Add(m1);
            this.movies.Add(m2);
            this.movies.Add(m3);


            SetRelation();
        }

        private void SetRelation()
        {
            foreach (Movie m in movies)
            {
                foreach (Star s in m.Actors)
                {
                    s.ActMovies.Add(m);
                }
            }
        }
    }
}