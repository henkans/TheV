using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Models;

namespace TheV.Lib.Managers
{

    //TODO Check out http://colorfulconsole.com/
    public interface IOutputConsoleManager
    {
        void WriteVersion(IVersionChecker versionChecker, InputParameters inputParameters);
        void WriteHeader(InputParameters inputParameters);
        void WriteFooter(InputParameters inputParameters);
    }

    public class OutputConsoleManager : IOutputConsoleManager
    {
        public string AssemblyVersion => Assembly.GetEntryAssembly()?.GetName().Version?.ToString();


        public void WriteVersion(IVersionChecker versionChecker, InputParameters inputParameters)
        {
            if (inputParameters.Verbose)
            {
               // WriteTitle(versionChecker.Title);
                WriteSlimTitle(versionChecker.Title);
                WriteVersion(versionChecker.GetVersion(inputParameters));
            }
            else
            {
                WriteVersion(versionChecker.Title, versionChecker.GetVersion(inputParameters));
            }
            // Print Title
            //WriteTitle(versionChecker.Title);

            // Print Version

        }

        public void WriteHeader(InputParameters inputParameterse)
        {
            var stringBuilder = new StringBuilder();
            if (inputParameterse.Verbose)
            {

                var originalForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;


                stringBuilder.AppendLine(@"  _____ _      __   __");
                stringBuilder.AppendLine(@" |_   _| |_  __\ \ / /");
                stringBuilder.AppendLine(@"   | | | ' \/ -_) V / ");
                stringBuilder.AppendLine(@"   |_| |_||_\___|\_/  ");
                //stringBuilder.AppendLine(@" ---------------------");
                //stringBuilder.AppendLine("— Rubrik ————————————————————————————————————————————");
                //stringBuilder.AppendLine("═Rubrik═åäö══════════════");
               //Console.OutputEncoding = Encoding.GetEncoding(866);

                Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
                Console.WriteLine(stringBuilder.ToString());

                Console.ForegroundColor = originalForegroundColor;
            }
            else
            {
                var originalForegroundColor = Console.ForegroundColor;
                var maxLength = Console.WindowWidth;
                if (maxLength > 60) maxLength = 60;
                var line2 = new string('—', maxLength);
                var lineColor = ConsoleColor.Cyan;
                Console.ForegroundColor = lineColor;
                Console.WriteLine(line2);

                var titletext = $"TheV (The version) {AssemblyVersion}";
                Console.Write(new string(' ', maxLength - titletext.Length));
                Console.WriteLine(titletext);

                Console.ForegroundColor = originalForegroundColor;

                WriteSlimTitle("Test Machine");

                //WriteLineColored($"TheV (The version) {AssemblyVersion}", ConsoleColor.Black, ConsoleColor.DarkGray);
                //WriteLineColored($"Checked { DateTime.Now }", ConsoleColor.Black, ConsoleColor.DarkGray);
            }
        }

        public void WriteSlimTitle(string title)
        {
            var textColor = ConsoleColor.Yellow;
            var lineColor = ConsoleColor.Cyan;

            var originalForegroundColor = Console.ForegroundColor;
            var originalBackgroundColor = Console.BackgroundColor;

            // Draw header
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            //Console.ForegroundColor = lineColor;
            //Console.Write("—");
            //Console.ForegroundColor = textColor;
            //Console.Write($"{title}");
            //Console.ForegroundColor = lineColor;
            //Console.WriteLine("————————————————————————————————————————————");
            //Console.WriteLine($" {title,-(40)}");

            //Console.ForegroundColor = originalForegroundColor;
            //Console.BackgroundColor = originalBackgroundColor;


            var maxLength = Console.WindowWidth;
            if (maxLength > 60) maxLength = 60;
            Console.ForegroundColor = lineColor;
            Console.Write("—");
            Console.ForegroundColor = textColor;
            Console.Write($"{title}");
            var line2 = new string('—', maxLength - (title.Length + 1));
            Console.ForegroundColor = lineColor;
            Console.WriteLine(line2);


            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;


            // Console.Write($" {title,-(40)}"); 
            // HACK: To set dynamic width
            //Console.Write("{0, -" + Console.WindowWidth / 2 + "}", $" {title} ");
            //Console.ForegroundColor = originalForegroundColor;
            //Console.BackgroundColor = originalBackgroundColor;
            //Console.WriteLine();

            //var stringBuilder = new StringBuilder();
            //if (inputParameterse.Verbose)
            //{
            //    stringBuilder.AppendLine(@"  _____ _      __   __");
            //    stringBuilder.AppendLine(@" |_   _| |_  __\ \ / /");
            //    stringBuilder.AppendLine(@"   | | | ' \/ -_) V / ");
            //    stringBuilder.AppendLine(@"   |_| |_||_\___|\_/  ");
            //    //stringBuilder.AppendLine(@" ---------------------");
            //    stringBuilder.AppendLine("— Rubrik ————————————————————————————————————————————");
            //    stringBuilder.AppendLine("═Rubrik═åäö══════════════");
            //    //Console.OutputEncoding = Encoding.GetEncoding(866);

            //    Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            //    Console.WriteLine(stringBuilder.ToString());
            //}
            //else
            //{
            //    WriteLineColored($"TheV (The version) {AssemblyVersion}", ConsoleColor.Black, ConsoleColor.DarkGray);
            //    WriteLineColored($"Checked { DateTime.Now }", ConsoleColor.Black, ConsoleColor.DarkGray);
            //}
        }


        public void WriteFooter(InputParameters inputParameterse)
        {
            var stringBuilder = new StringBuilder();
            if (inputParameterse.Verbose)
            {
                stringBuilder.AppendLine(@"  Fot");

                Console.WriteLine(stringBuilder.ToString());
            }
            else
            {
                // https://github.com/henkans/TheV
                WriteLineColored($"Checked { DateTime.Now }", ConsoleColor.Black, ConsoleColor.DarkGray);
            }
        }

        private void WriteTitle(string title, ConsoleColor? color = ConsoleColor.DarkGreen, bool verbose = false)
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

        private void WriteVersion(IEnumerable<VersionCheck> checkerResults)
        {
            if (Console.IsOutputRedirected)
            {
                foreach (var checkerResult in checkerResults)
                {
                    Console.WriteLine($"{checkerResult.Name} {checkerResult.Version}");
                }
                //Console.Out.WriteLine(version);
                return;
            }


            //ar paddingWithChar = new string('.', 10);
            //padding with dots


            foreach (var checkerResult in checkerResults)
            {

                Console.WriteLine($"{PaddingWithDots(checkerResult.Name)}{checkerResult.Version}");
                // Console.WriteLine($"{checkerResult.Name} {checkerResult.Version}");
            }


        }

        private void WriteVersion(string title, IEnumerable<VersionCheck> checkerResults)
        {
            if (Console.IsOutputRedirected)
            {
                foreach (var checkerResult in checkerResults)
                {
                    Console.WriteLine($"{checkerResult.Name} {checkerResult.Version}");
                }
                //Console.Out.WriteLine(version);
                return;
            }

            foreach (var checkerResult in checkerResults)
            {
                if (Equals(!string.IsNullOrEmpty(checkerResult.Name))) checkerResult.Name = checkerResult.Name + " ";
                Console.WriteLine($"{PaddingWithDots(title)}{checkerResult.Name}{checkerResult.Version}");
                title = string.Empty;

            }


        }


        private void WriteLineColored(string text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
        {
            var originalForegroundColor = Console.ForegroundColor;
            var originalBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            if (backgroundColor.HasValue) Console.BackgroundColor = backgroundColor.Value;

            Console.Write("{0, -" + (Console.WindowWidth - 1) + "}", $" {text} ");
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
            Console.WriteLine();
        }

        private string PaddingWithDots(string name)
        {
            var maxLength = 20;
            if (string.IsNullOrWhiteSpace(name)) return new string(' ', maxLength + 2);

            if (name.Length >= maxLength) name = name.Substring(0, maxLength);
            var paddingWithDots = new string('.', maxLength - name.Length);
            return $"{name}{paddingWithDots}: ";
        }

    }
}