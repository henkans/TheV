using System;
using System.Collections.Generic;
using System.Linq;
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
        // TODO extract to conf
        private const ConsoleColor AppTextColor = ConsoleColor.Cyan;
        private const ConsoleColor TitleColor = ConsoleColor.Yellow;
        private const ConsoleColor LineColor = ConsoleColor.Cyan;
        private const ConsoleColor NameColor = ConsoleColor.DarkGray;
        private const ConsoleColor VersionColor = ConsoleColor.White;


        private string AssemblyVersion => Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

        public int AppWidth {
            get
            {
                var length = Console.WindowWidth;
                if (length > 60) length = 60;
                return length;
            }
        }
        
        public void WriteVersion(IVersionChecker versionChecker, InputParameters inputParameters)
        {
            // TODO check encoding for Linux
            Console.OutputEncoding = Encoding.GetEncoding(1252); 
            WriteTitle(versionChecker.Title);
            WriteVersion(versionChecker.GetVersion(inputParameters), versionChecker.Title);
        }


        public void WriteHeader(InputParameters inputParameterse)
        {
            var stringBuilder = new StringBuilder();
            if (inputParameterse.Verbose)
            {
                var originalForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = AppTextColor;
                stringBuilder.AppendLine(@$"  _____ _      __   __");
                stringBuilder.AppendLine(@$" |_   _| |_  __\ \ / /     The Version");
                stringBuilder.AppendLine(@$"   | | | ' \/ -_) V /      {AssemblyVersion}");
                stringBuilder.AppendLine(@$"   |_| |_||_\___|\_/  ");
                Console.WriteLine(stringBuilder.ToString());
                Console.ForegroundColor = originalForegroundColor;
            }
            else
            {
                var originalForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = LineColor;
                Console.WriteLine(new string('—', AppWidth));
                var titletext = $"TheV (The version) {AssemblyVersion}";
                Console.Write(new string(' ', AppWidth - titletext.Length));
                Console.WriteLine(titletext);
                Console.ForegroundColor = originalForegroundColor;
            }
        }

        public void WriteTitle(string title)
        {
            var originalForegroundColor = Console.ForegroundColor;
            var originalBackgroundColor = Console.BackgroundColor;
            
            Console.ForegroundColor = LineColor;
            Console.Write("—");
            Console.ForegroundColor = TitleColor;
            Console.Write($"{title}");
            Console.ForegroundColor = LineColor;
            Console.WriteLine(new string('—', AppWidth - (title.Length + 1)));
            
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        
        private void WriteVersion(IEnumerable<VersionCheck> checkerResults, string title = "")
        {
            if (Console.IsOutputRedirected)
            {
                foreach (var checkerResult in checkerResults)
                {
                    Console.WriteLine($"{checkerResult.Name} {checkerResult.Version}");
                }
                return;
            }

            var maxlength = checkerResults.Max(c => c.Name.Length);
            if (maxlength < 20) maxlength = 20;

            foreach (var checkerResult in checkerResults)
            {
                var originalForegroundColor = Console.ForegroundColor;
                var originalBackgroundColor = Console.BackgroundColor;

                Console.ForegroundColor = NameColor;
                Console.Write($"{PaddingWithDots((string.IsNullOrWhiteSpace(checkerResult.Name)? title : checkerResult.Name), maxlength)}");
                Console.ForegroundColor = VersionColor;
                Console.WriteLine($"{checkerResult.Version}");

                Console.ForegroundColor = originalForegroundColor;
                Console.BackgroundColor = originalBackgroundColor;
            }
        }

        public void WriteFooter(InputParameters inputParameterse)
        {
            // TODO: include?
            //https://github.com/henkans/TheV
            var originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = LineColor;
            Console.WriteLine(new string('—', AppWidth));
            var footertext = $"Checked { DateTime.Now }";
            Console.Write(new string(' ', AppWidth - footertext.Length));
            Console.WriteLine(footertext);
            Console.ForegroundColor = originalForegroundColor;
        }

        private string PaddingWithDots(string name, int length = 20)
        {
            if (string.IsNullOrWhiteSpace(name)) return new string(' ', length + 2);
            if (name.Length >= length) name = name.Substring(0, length);
            var paddingWithDots = new string('.', length - name.Length);
            return $"{name}{paddingWithDots}: ";
        }

        // Saved for future use...?
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
    }
}