using System.Collections.Generic;

namespace MonitorARM.Model
{
    public class Job
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool passed { get; set; }
        public bool failed { get; set; }
        public string status { get; set; }
        public string started { get; set; }
        public string finished { get; set; }
        public string duration { get; set; }
        public List<object> messages { get; set; }
        public List<CompilationMessage> compilationMessages { get; set; }
        public List<Artifact> artifacts { get; set; }
    }
}