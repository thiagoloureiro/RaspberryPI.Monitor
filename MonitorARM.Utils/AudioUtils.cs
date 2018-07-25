using System.Diagnostics;

namespace MonitorARM.Utils
{
    public static class AudioUtils
    {
        public static void PlayAudio()
        {
            string command = "aplay /home/pi/Desktop/monitor2/music.wav";
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