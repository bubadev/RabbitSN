using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitSN
{
    public enum MessageType
    {
        Text,
        Connect,
        Disconnect,
        Start,
        Commands,
    }

    public class Message
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public MessageType Type { get; set; }


        public void Send()
        {
            using (Config config = new Config())
            {
                Time = DateTime.Now;
                string json;
                byte[] body;

                json = JsonSerializer.Serialize(this);
                body = Encoding.UTF8.GetBytes(json);

                config.Channel.BasicPublish(
                    exchange: "send",
                    routingKey: "info",
                    basicProperties: null,
                    body: body);
            }
        }

        public void Print()
        {
            switch (Type)
            {
                case MessageType.Text:
                    Console.ForegroundColor = Theme.Time;
                    Console.Write($"[{Time}] ");
                    Console.ForegroundColor = Theme.UserName;
                    Console.Write($"{Name}: ");
                    Console.ForegroundColor = Theme.Text;
                    Console.Write($"{Text}");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                case MessageType.Connect:
                    Console.ForegroundColor = Theme.System;
                    Console.Write($"[{Time}] ");
                    Console.Write($"* {Name} connected to chat *");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                case MessageType.Disconnect:
                    Console.ForegroundColor = Theme.Error;
                    Console.Write($"[{Time}] ");
                    Console.Write($"* {Name} disconnected from chat *");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                case MessageType.Start:
                    Console.ForegroundColor = Theme.System;
                    Console.WriteLine($"** Welcome to this chat **");
                    Console.WriteLine($"==========================");
                    Console.WriteLine($"Commands:");
                    Console.WriteLine($"_________");
                    foreach (var c in Command.Commands)
                    {
                        Console.WriteLine($"{c.Key} - {c.Value}");
                    }
                    Console.WriteLine($"==========================");
                    Console.WriteLine($"Write your name: ");
                    Console.ResetColor();
                    break;
                case MessageType.Commands:
                    Console.ForegroundColor = Theme.System;
                    Console.WriteLine($"==========================");
                    Console.WriteLine($"Commands:");
                    Console.WriteLine($"_________");
                    foreach (var c in Command.Commands)
                    {
                        Console.WriteLine($"{c.Key} - {c.Value}");
                    }
                    Console.WriteLine($"==========================");
                    Console.ResetColor();
                    break;
            }
        }

        public static string Validate(string text)
        {
            return text.Trim('*').Replace("/", "\\");
        }
    }


}
