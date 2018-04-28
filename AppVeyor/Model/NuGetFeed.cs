using System;

namespace AppVeyor.Model
{
    public class NuGetFeed
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool publishingEnabled { get; set; }
        public DateTime created { get; set; }
    }
}