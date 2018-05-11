using System;

namespace MonitorARM
{
    internal class Program
    {
        private static void Main()
        {
            Console.Clear();
            Console.WriteLine("Raspberry Bot Started - Checking your builds");
            Console.WriteLine("Connecting to RabbitMQ");
            var obj = new RabbitMQ.RabbitMQ("AppVeyor");
            obj.Connect();

            Console.WriteLine("Connected successfully with RabbitMQ!");

            Console.WriteLine("Press <ENTER> to exit");
            Console.ReadKey();
        }
    }
}