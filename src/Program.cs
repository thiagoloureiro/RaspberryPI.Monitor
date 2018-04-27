using System;
using System.Diagnostics;

namespace TestRasp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Hello Raspberry PI!");

            CallWatson.SynthetizeText("Sent to cloud and got it back! and playing");

            Console.ReadKey();

            string command = "aplay /home/pi/ftp/files/music.wav";
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
        }
    }
}