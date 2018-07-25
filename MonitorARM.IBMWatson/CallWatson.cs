using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TestRasp
{
    public static class CallWatson
    {
        public static async void SynthetizeText(string text)
        {
            string url = "https://stream.watsonplatform.net/text-to-speech/api/v1/synthesize";
            string username = "deac8fe3-0590-4677-a667-150fa45b88d4";
            string password = "-";

            string authString = username + ":" + password;
            var byteArray = Encoding.ASCII.GetBytes(authString);

            var obj = new TextSpeechModel { text = text };
            string postBody = JsonConvert.SerializeObject(obj);

            HttpResponseMessage ret;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "audio/wav");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                ret = client.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
            }

            if (ret.IsSuccessStatusCode)
            {
                await ret.Content.ReadAsFileAsync("music.wav", true);
            }
        }

        public static async void SynthetizeText(string text, string voice)
        {
            string url = $"https://stream.watsonplatform.net/text-to-speech/api/v1/synthesize?voice={voice}";
            string username = "deac8fe3-0590-4677-a667-150fa45b88d4";
            string password = "-";

            string authString = username + ":" + password;
            var byteArray = Encoding.ASCII.GetBytes(authString);

            var obj = new TextSpeechModel { text = text };
            string postBody = JsonConvert.SerializeObject(obj);

            HttpResponseMessage ret;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "audio/wav");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                ret = client.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
            }

            if (ret.IsSuccessStatusCode)
            {
                await ret.Content.ReadAsFileAsync("music.wav", true);
            }
        }

        public static HttpContent GetVoices()
        {
            string url = "https://stream.watsonplatform.net/text-to-speech/api/v1/voices";
            string username = "deac8fe3-0590-4677-a667-150fa45b88d4";
            string password = "PdgBBkvpTC6s";

            string authString = username + ":" + password;
            var byteArray = Encoding.ASCII.GetBytes(authString);

            HttpResponseMessage ret;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "audio/wav");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                ret = client.GetAsync(url).Result;
            }
            HttpContent test = null;
            if (ret.IsSuccessStatusCode)
            {
                test = ret.Content;
            }

            return test;
        }
    }
}