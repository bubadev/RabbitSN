using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSN
{
    public enum ThemeStyle
    {
        Default,
        Light,
        Green,
    }
    public static class Theme
    {
        public static ConsoleColor UserName = ConsoleColor.White;
        public static ConsoleColor Text = ConsoleColor.Blue;
        public static ConsoleColor Time = ConsoleColor.Green;
        public static ConsoleColor System = ConsoleColor.Yellow;
        public static ConsoleColor Input = ConsoleColor.White;
        public static ConsoleColor Error = ConsoleColor.DarkRed;

        private static ThemeStyle _style = ThemeStyle.Default;
        public static ThemeStyle Style
        {
            get => _style;
            set
            {
                switch (value)
                {
                    case ThemeStyle.Default:
                        UserName = ConsoleColor.White;
                        Text = ConsoleColor.Blue;
                        Time = ConsoleColor.Green;
                        System = ConsoleColor.Yellow;
                        Input = ConsoleColor.White;
                        Error = ConsoleColor.DarkRed;
                        break;
                    case ThemeStyle.Light:
                        UserName = ConsoleColor.DarkGray;
                        Text = ConsoleColor.White;
                        Time = ConsoleColor.White;
                        System = ConsoleColor.DarkGray;
                        Input = ConsoleColor.White;
                        Error = ConsoleColor.DarkRed;
                        break;
                    case ThemeStyle.Green:
                        UserName = ConsoleColor.DarkGreen;
                        Text = ConsoleColor.Green;
                        Time = ConsoleColor.Green;
                        System = ConsoleColor.DarkGreen;
                        Input = ConsoleColor.Green;
                        Error = ConsoleColor.DarkRed;
                        break;
                }
            }
        }

    }
}
