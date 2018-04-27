using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace TestRasp
{
    public static class CallWatson
    {
        public static async void SynthetizeText(string text)
        {
            string url = "https://stream.watsonplatform.net/text-to-speech/api/v1/synthesize";
            string username = "-";
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

        public static void GetVoices()
        {
            string url = "https://stream.watsonplatform.net/text-to-speech/api/v1/voices";
            string username = "-";
            string password = "-";

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

            if (ret.IsSuccessStatusCode)
            {
                var test = ret.Content;
            }
        }
    }
}