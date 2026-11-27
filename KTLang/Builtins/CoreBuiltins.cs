using KTLang.Runtime;
using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Builtins
{
    /// <summary>
    /// Standard built-in classes.
    /// </summary>
    public static class CoreBuiltins
    {
        /// <summary>
        /// Writes a message to the output with a trailing line break.
        /// </summary>
        public static KTLangType? PrintLn(List<KTLangType?> args, IHost host)
        {
            var text = string.Join(" ", args);
            host.PrintLn(text);

            return null;
        }

        /// <summary>
        /// Writes a message to the output.
        /// </summary>
        public static KTLangType? Print(List<KTLangType?> args, IHost host)
        {
            var text = string.Join(" ", args);
            host.Print(text);

            return null;
        }

        /// <summary>
        /// Converts the provided argument to a string.
        /// </summary>
        public static KTLangType? ToStr(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("toString", 1, args.Count);

            var first = args.First();
            var stringified = first?.ToString();

            if (string.IsNullOrEmpty(stringified))
                throw new Exception("Failed to convert provided argument to string");

            return new KTLangString(stringified);
        }

        /// <summary>
        /// Converts a string to a number.
        /// </summary>
        public static KTLangType? ToNum(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("toNumber", 1, args.Count);

            var first = args.First();

            if (first is KTLangString str)
            {
                if (double.TryParse(str.Value, out var result))
                    return new KTLangNumber(result);

                throw new Exception("Failed to convert string to number");
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "toNumber",
                "string",
                first?.GetType().Name ?? "null"
            );
        }
    }
}
