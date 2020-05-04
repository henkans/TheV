using System;
using TheV.Checkers.Interfaces;

namespace TheV.Managers
{
    public interface IOutputConsoleManager
    {
        void Write(IVersionChecker versionChecker);
    }

    public class OutputConsoleManager : IOutputConsoleManager
    {
        public void Write(IVersionChecker versionChecker)
        {
            // Print Title
            WriteTitle(versionChecker.Title);

            // Print Version
            WriteVersion(versionChecker.GetVersion());
        }

        private void WriteTitle(string title, ConsoleColor? color = ConsoleColor.DarkGreen)
        {
            if (Console.IsOutputRedirected)
            {
                Console.Out.WriteLine(title);
                return;
            }

            var originalForegroundColor = Console.ForegroundColor;
            var originalBackgroundColor = Console.BackgroundColor;

            if (color == null) // Invert color
            {
                Console.BackgroundColor = originalForegroundColor;
                Console.ForegroundColor = originalBackgroundColor;
            }
            else
            {
                Console.BackgroundColor = (ConsoleColor)color;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            // Console.Write($" {title,-(40)}"); 
            // HACK: To set dynamic width
            Console.Write("{0, -" + Console.WindowWidth / 2 + "}", $" {title} ");
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
            Console.WriteLine();
        }

        private void WriteVersion(string version)
        {
            if (Console.IsOutputRedirected)
            {
                Console.Out.WriteLine(version);
                return;
            }
            Console.WriteLine($"{version}");
        }
    }
}