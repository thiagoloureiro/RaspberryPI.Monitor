using MonitorARM.Utils;
using Nancy;
using TestRasp;

namespace MonitorARM
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get("/hello", args =>
            {
                var name = this.Request.Query["name"];
                CallWatson.SynthetizeText($"Olá {name} seja bem vindo à Disruptiv", "pt-BR_IsabelaVoice");
                AudioUtils.PlayAudio();

                return "Service Status OK";
            });
        }
    }
}