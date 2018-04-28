using System;
using System.Threading.Tasks;

namespace MonitorARM
{
    internal class Program
    {
        private static void Main()
        {
            Console.Clear();
            Console.WriteLine("Hello Raspberry PI!");
            var ret = AppVeyor.APICall.GetProjects().Result;
            var ret2 = AppVeyor.APICall.GetProjectLastBuild("dapper-crud-extension").Result;

            //CallWatson.SynthetizeText("Sent to cloud and got it back! and playing");

            //Console.ReadKey();

            //string command = "aplay /home/pi/ftp/files/music.wav";
            //var process = new Process()
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = "/bin/bash",
            //        Arguments = "-c \"" + command + "\"",
            //        RedirectStandardOutput = true,
            //        UseShellExecute = false,
            //        CreateNoWindow = true,
            //    }
            //};
            //process.Start();
        }
    }
}