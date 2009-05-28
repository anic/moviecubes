using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieCube.Common.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QueryKey
    {
        [JsonProperty]
        public string Key { get; set; }

        [JsonProperty]
        public string Type { get; set; }

        public QueryKey(string key, string type)
        {
            this.Key = key;
            this.Type = type;
        }
    }
}
