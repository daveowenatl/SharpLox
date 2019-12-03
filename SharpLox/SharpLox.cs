using System;
using System.IO;

namespace SharpLox
{
    class SharpLox
    {
        private static bool hadError = false;
        
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: SharpLox [script]");
                Environment.Exit(Exit.Usage);
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        private static void RunFile(string path)
        {
            Run(File.ReadAllText(path));

            if (hadError)
            {
                Environment.Exit(Exit.DataError);
            }
        }

        private static void RunPrompt()
        {
            while (true)
            {
                Console.Write("> ");
                Run(Console.ReadLine());
            }
        }

        private static void Run(string source)
        {
            var scanner = new Scanner(source);
            var tokens = scanner.ScanTokens();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
        
        internal static void Error(int line, string message) 
        {                       
            Report(line, "", message);                                        
        }

        private static void Report(int line, string where, string message) 
        {
            Console.WriteLine($"[line {line}] Error {where}: {message}");
            hadError = true;                                                  
        }        
    }
}