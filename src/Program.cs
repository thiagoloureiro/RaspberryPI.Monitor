using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace MonitorARM
{
    internal class Program
    {
        private static void Main()
        {
            Console.Clear();
            Console.WriteLine("Raspberry Bot Started - Checking your builds");
            Console.WriteLine("Connecting to RabbitMQ");
            var objRabbit = new RabbitMQ.RabbitMq("AppVeyor");

            objRabbit.Connect();

            Console.WriteLine("Connected successfully with RabbitMQ!");
            StartHost();
            Console.WriteLine("Press <ENTER> to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void StartHost()
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://+:888")

                .Build();

            host.Run();
        }
    }
}