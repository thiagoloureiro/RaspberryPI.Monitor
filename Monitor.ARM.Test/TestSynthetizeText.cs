using MonitorARM.IBMWatson;
using Newtonsoft.Json;
using TestRasp;
using Xunit;

namespace Monitor.ARM.Test
{
    public class TestSynthetizeText
    {
        [Fact]
        public void SynthetizeText()
        {
            CallWatson.SynthetizeText("Seja bem vindo Thiago", "pt-BR_IsabelaVoice");
        }

        [Fact]
        public void GetVoices()
        {
            var ret = CallWatson.GetVoices();
            var voiceList = JsonConvert.DeserializeObject<VoiceModel>(ret.ReadAsStringAsync().Result);
            Assert.True(voiceList.voices.Count > 0);
        }
    }
}