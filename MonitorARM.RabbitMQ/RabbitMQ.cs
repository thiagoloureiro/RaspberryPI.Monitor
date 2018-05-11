using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading;
using MonitorARM.Model;
using MonitorARM.Utils;
using Newtonsoft.Json;
using TestRasp;

namespace MonitorARM.RabbitMQ
{
    public class RabbitMQ
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;
        public string QueueName { get; set; }

        public RabbitMQ(string queuename)
        {
            QueueName = queuename;
        }

        public void Connect()
        {
            factory = new ConnectionFactory() { HostName = "hound.rmq.cloudamqp.com", VirtualHost = "usldnewk", UserName = "usldnewk", Password = "urWrV6UeRu5vGgk8k7YJaqVwFCJyPSHc" };

            connection = factory.CreateConnection();
            connection.ConnectionShutdown += Connection_ConnectionShutdown;
            Console.WriteLine($"Declaring Queue: {QueueName}");

            channel = connection.CreateModel();
            channel.QueueDeclare(QueueName, true, false, false, null);

            consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(QueueName, true, consumer);
        }

        public void Disconnect()
        {
            Cleanup();
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connection broke!");

            Cleanup();

            while (true)
            {
                try
                {
                    Connect();

                    Console.WriteLine("Reconnected!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Reconnect failed!");
                    Thread.Sleep(3000);
                }
            }
        }

        private void Cleanup()
        {
            Console.WriteLine("Cleaning Up RabbitMQ");

            try
            {
                if (channel != null && channel.IsOpen)
                {
                    channel.Close();
                    channel = null;
                }

                if (connection != null && connection.IsOpen)
                {
                    connection.Close();
                    connection = null;
                }
            }
            catch (IOException ex)
            {
                // Close() may throw an IOException if connection
                // dies - but that's ok (handled by reconnect)
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine("Message Received!");

            var body = e.Body;
            var content = Encoding.UTF8.GetString(body);
            var objRootObject = JsonConvert.DeserializeObject<RootObject>(content);
            WriteInfo(objRootObject);
        }

        private void WriteInfo(RootObject objRootObject)
        {
            Console.WriteLine(objRootObject.eventName);
            Console.WriteLine(objRootObject.eventData.status);
            Console.WriteLine(objRootObject.eventData.branch);
            Console.WriteLine(objRootObject.eventData.duration);
            Console.WriteLine(objRootObject.eventData.repositoryName);
            Console.WriteLine(objRootObject.eventData.passed);
            Console.WriteLine(objRootObject.eventData.commitAuthor);

            CallWatson.SynthetizeText(
                $"A new build was completed, repository {objRootObject.eventData.repositoryName}, Status {objRootObject.eventData.status}, author from this build is {objRootObject.eventData.commitAuthor}");

            AudioUtils.PlayAudio();
        }
    }
}