using System.Collections.Generic;

namespace MonitorARM.IBMWatson
{
    public class SupportedFeatures
    {
        public bool voice_transformation { get; set; }
        public bool custom_pronunciation { get; set; }
    }

    public class Voice
    {
        public string name { get; set; }
        public string language { get; set; }
        public bool customizable { get; set; }
        public string gender { get; set; }
        public string url { get; set; }
        public SupportedFeatures supported_features { get; set; }
        public string description { get; set; }
    }

    public class VoiceModel
    {
        public List<Voice> voices { get; set; }
    }
}