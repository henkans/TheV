using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
        private const ConsoleColor ErrorTextColor = ConsoleColor.Red;
        private const ConsoleColor LineColor = ConsoleColor.Cyan;
        private const ConsoleColor NameColor = ConsoleColor.DarkGray;
        private const ConsoleColor VersionColor = ConsoleColor.White;

        private readonly char _emDash = '—';

        public OutputConsoleManager()
        {
            Console.OutputEncoding = Encoding.GetEncoding(1252);
            // Hack: Set dash on... linux
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) _emDash = '-';
        }


        public void WriteVersion(IVersionChecker versionChecker, InputParameters inputParameters)
        {
            try
            {
                var checkerResults = versionChecker.GetVersion(inputParameters);
                if (checkerResults == null || !checkerResults.Any()) return;

                WriteTitle(versionChecker.Title);

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
                    Console.Write($"{PaddingWithDots((string.IsNullOrWhiteSpace(checkerResult.Name) ? versionChecker.Title : checkerResult.Name), maxlength)}");
                    Console.ForegroundColor = VersionColor;
                    Console.WriteLine($"{checkerResult.Version}");

                    Console.ForegroundColor = originalForegroundColor;
                    Console.BackgroundColor = originalBackgroundColor;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                WriteTitle(versionChecker.Title);
                WriteError(e.Message);
            }

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
                Console.WriteLine(new string(_emDash, AppWidth));
                var titletext = $"TheV (The version) {AssemblyVersion}";
                Console.Write(new string(' ', AppWidth - titletext.Length));
                Console.WriteLine(titletext);
                Console.ForegroundColor = originalForegroundColor;
            }
        }

        public void WriteFooter(InputParameters inputParameterse)
        {
            // TODO: include?
            //https://github.com/henkans/TheV
            var originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = LineColor;
            Console.WriteLine(new string(_emDash, AppWidth));
            var footertext = $"Checked { DateTime.Now }";
            Console.Write(new string(' ', AppWidth - footertext.Length));
            Console.WriteLine(footertext);
            Console.ForegroundColor = originalForegroundColor;
        }



        private static string AssemblyVersion => Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

        private static int AppWidth
        {
            get
            {
                var length = Console.WindowWidth;
                if (length > 60) length = 60;
                return length;
            }
        }



        private void WriteTitle(string title)
        {
            var originalForegroundColor = Console.ForegroundColor;
            var originalBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = LineColor;
            Console.Write(_emDash);
            Console.ForegroundColor = TitleColor;
            Console.Write($"{title}");
            Console.ForegroundColor = LineColor;
            Console.WriteLine(new string(_emDash, AppWidth - (title.Length + 1)));

            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }


        private void WriteError(string message)
        {
            var originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ErrorTextColor;
            Console.WriteLine(message);
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