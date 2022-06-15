using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSN
{
    public class Command
    {
        public Command(string cmd)
        {
            if (IsCommand(cmd))
                _text = cmd;
        }
        private string _text;

        public static Dictionary<string, string> Commands = new Dictionary<string, string>()
        {
            {"/end", "leave chat"},
            {"/cmd", "show commands"},
            { "/cht-d","default theme" },
            { "/cht-l","light theme" },
            { "/cht-g","green theme" },
        };
        public static bool IsCommand(string input)
        {
            foreach (var c in Commands.Keys)
            {
                if (c == input)
                    return true;
            }
            return false;
        }

        public void Start()
        {
            switch (_text)
            {
                case "/end":
                    Message mes = new Message()
                    {
                        Name = RabbitSN._userName,
                        Type = MessageType.Disconnect,
                    };
                    mes.Send();
                    Environment.Exit(0);
                    break;
                case "/cmd":
                    Message cmd = new Message()
                    {
                        Type = MessageType.Commands,
                    };
                    cmd.Print();
                    break;
                case "/cht-d":
                    Theme.Style = ThemeStyle.Default;
                    break;
                case "/cht-l":
                    Theme.Style = ThemeStyle.Light;
                    break;
                case "/cht-g":
                    Theme.Style = ThemeStyle.Green;
                    break;
            }
        }

    }
}
