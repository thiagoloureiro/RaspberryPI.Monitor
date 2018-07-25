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
    public class RabbitMq
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        public string QueueName { get; set; }

        public RabbitMq(string queuename)
        {
            QueueName = queuename;
        }

        public void Connect()
        {
            _factory = new ConnectionFactory() { HostName = "hound.rmq.cloudamqp.com", VirtualHost = "usldnewk", UserName = "usldnewk", Password = "-" };

            _connection = _factory.CreateConnection();
            _connection.ConnectionShutdown += Connection_ConnectionShutdown;
            Console.WriteLine($"Declaring Queue: {QueueName}");

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(QueueName, true, false, false, null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += Consumer_Received;
            _channel.BasicConsume(QueueName, true, _consumer);
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
                    Console.WriteLine($"Reconnect failed! {ex.Message}");
                    Thread.Sleep(3000);
                }
            }
        }

        private void Cleanup()
        {
            Console.WriteLine("Cleaning Up RabbitMQ");

            try
            {
                if (_channel != null && _channel.IsOpen)
                {
                    _channel.Close();
                    _channel = null;
                }

                if (_connection != null && _connection.IsOpen)
                {
                    _connection.Close();
                    _connection = null;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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