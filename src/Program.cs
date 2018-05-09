using System;
using System.Threading;

namespace MonitorARM
{
    internal class Program
    {
        // private static readonly ManualResetEvent QuitEvent = new ManualResetEvent(false);

        private static void Main()
        {
            Console.Clear();
            Console.WriteLine("Raspberry Bot Started - Checking your builds");

            var obj = new RabbitMQ.RabbitMQ("AppVeyor");

            obj.Connect();

            Console.ReadKey();
            //Console.CancelKeyPress += (sender, eArgs) =>
            //{
            //    QuitEvent.Set();
            //    eArgs.Cancel = true;
            //};

            //// kick off asynchronous stuff

            //QuitEvent.WaitOne();
        }
    }
}