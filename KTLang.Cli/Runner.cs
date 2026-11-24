using KTLang.Parsing;
using KTLang.Runtime;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Cli
{
    /// <summary>
    /// Runs a KTLang script, or starts a KTLang REPL.
    /// </summary>
    public static class Runner
    {
        private static Interpreter _globalInterpreter = new(new DefaultHost());

        private static void RunLine(string line, Interpreter interpreter)
        {
            var tokens = new Lexer(line).Parse();
            var statements = new Parser(tokens).Parse();

            interpreter.Interpret(statements);
        }

        /// <summary>
        /// Runs a KTLang script.
        /// </summary>
        /// <param name="path">The path of the script.</param>
        public static void RunScript(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Cannot open non-existent script file: {path}");

            var source = File.ReadAllText(path);

            var tokens = new Lexer(source).Parse();
            var statements = new Parser(tokens).Parse();

            _globalInterpreter.Interpret(statements);
        }

        /// <summary>
        /// Starts the KTLang REPL.
        /// </summary>
        public static void StartRepl()
        {
            Console.WriteLine($"KTLang version {KTLangVersionInfo.VersionString}");
            Console.WriteLine($"Type 'exit' to quit");
            Console.WriteLine();

            var interpreter = new Interpreter(new DefaultHost());

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input == "exit")
                    break;

                try
                {
                    RunLine(input, interpreter);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
        }
    }
}
