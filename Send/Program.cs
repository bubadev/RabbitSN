using System.Net.Http.Headers;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitSN
{
    public class RabbitSN
    {
        public static string _userName;
        private static Config config = new Config();

        public static void Main(string[] args)
        {
            config.Channel.ExchangeDeclare(
                exchange: "send", 
                type: ExchangeType.Direct);

            var severity = "info";
            var queueName = config.Channel.QueueDeclare().QueueName;
            config.Channel.QueueBind(queue: queueName,
                exchange: "send",
                routingKey: "info");

            Message start = new Message() {Type = MessageType.Start};
            start.Print();
            _userName = Console.ReadLine() ?? "*guest*";

            var consumer = new EventingBasicConsumer(config.Channel);
            config.Channel.BasicConsume(
                queue: queueName,
                autoAck: false, 
                consumer: consumer);
            
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string json = Encoding.UTF8.GetString(body);
                Message mes = JsonSerializer.Deserialize<Message>(json);
                //if(mes.Type != MessageType.Connect)
                    mes.Print();
            };

            Message connected = new Message()
            {
                Name = _userName, 
                Type = MessageType.Connect
            };
            connected.Send();

            while (true)
            {
                Message mes = new Message()
                {
                    Name = _userName,
                    Text = Console.ReadLine(),
                };
                if (!Command.IsCommand(mes.Text))
                {
                    mes.Text = Message.Validate(mes.Text);
                    mes.Type = MessageType.Text;
                    mes.Send();
                }
                else
                {
                    Command cmd = new Command(mes.Text);
                    cmd.Start();
                }
            }
        }
        
    }
}