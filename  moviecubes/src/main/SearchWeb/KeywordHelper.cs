using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using MovieCube.Common.Data;

namespace SearchWeb
{
    public class KeywordHelper
    {
        public static KeywordHelper Instance
        {
            get;
            set;
        }

        private Hashtable keys = new Hashtable();

        public void Index(string filename,string label)
        {
            StreamReader sr = new StreamReader(filename, System.Text.Encoding.UTF8);
            while (!sr.EndOfStream)
            { 
                string word = sr.ReadLine().Trim();
                string key = word.Substring(0, 1);
                if (keys.ContainsKey(key))
                {
                    List<QueryKey> list = keys[key] as List<QueryKey>;
                    QueryKey newKey = new QueryKey(word, label);
                    list.Add(newKey);
                }
                else
                {
                    List<QueryKey> list = new List<QueryKey>();
                    QueryKey newKey = new QueryKey(word, label);
                    list.Add(newKey);
                    keys.Add(key, list);
                }
            }
            sr.Close();
        }

        public List<QueryKey> GetTipList(string input)
        {
            if (input == null || input.Length == 0)
                return new List<QueryKey>();

            string key = input.Substring(0, 1);
            if (keys.ContainsKey(key))
                return keys[key] as List<QueryKey>;
            else
                return new List<QueryKey>();

        }
            
    }
}