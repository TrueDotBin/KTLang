using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Modules
{
    /// <summary>
    /// A built-in module for manipulating strings.
    /// </summary>
    public sealed class StringModule : KTLangModule
    {
        /// <inheritdoc/>
        public override string Name { get; } = "string";

        /// <inheritdoc/>
        public override void Register()
        {
            Environment.Define("repeat", new KTLangFunction(Repeat), true);
            Environment.Define("upper", new KTLangFunction(Upper), true);
            Environment.Define("lower", new KTLangFunction(Lower), true);
            Environment.Define("reverse", new KTLangFunction(Reverse), true);
            Environment.Define("contains", new KTLangFunction(Contains), true);
            Environment.Define("replace", new KTLangFunction(Replace), true);
            Environment.Define("endsWith", new KTLangFunction(EndsWith), true);
            Environment.Define("startsWith", new KTLangFunction(StartsWith), true);
            Environment.Define("trim", new KTLangFunction(Trim), true);
            Environment.Define("split", new KTLangFunction(Split), true);
            Environment.Define("join", new KTLangFunction(Join), true);
        }

        private KTLangString Join(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("string.join", 1, args.Count);

            var first = args[0];
            var second = args.Count > 1 ? args[1] : new KTLangString(" ");

            if (first is KTLangArray target &&
                second is KTLangString separator)
            {
                var joined = string.Join(separator.Value, target.GetList());
                return new(joined);
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.join",
                "an array, and optionally a second string",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private KTLangArray Split(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("string.split", 1, args.Count);

            var first = args[0];
            var second = args.Count > 1 ? args[1] : new KTLangString(" ");

            if (first is KTLangString target &&
                second is KTLangString separator)
            {
                var arr = new KTLangArray();
                var split = target.Value.Split(separator.Value);

                foreach (var item in split)
                {
                    arr.Push(new KTLangString(item));
                }

                return arr;
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.repeat",
                "a string, and optionally a second string",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private KTLangString Repeat(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("string.repeat", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangString target &&
                second is KTLangNumber amount)
            {
                var repeated = Enumerable.Repeat(target.Value, (int)amount.Value);
                var concat = string.Concat(repeated);

                return new(concat);
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.repeat",
                "a string and a number",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private KTLangString Upper(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("string.upper", 1, args.Count);

            var first = args.First();

            if (first is KTLangString target)
                return new(target.Value.ToUpperInvariant());

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.upper",
                "a string",
                first?.GetType().Name ?? "null"
            );
        }

        private KTLangString Lower(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("string.lower", 1, args.Count);

            var first = args.First();

            if (first is KTLangString target)
                return new(target.Value.ToLowerInvariant());

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.lower",
                "a string",
                first?.GetType().Name ?? "null"
            );
        }

        private KTLangString Trim(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("string.trim", 1, args.Count);

            var first = args[0];
            var second = args.Count > 1 ? args[1] : new KTLangString(" ");

            if (first is KTLangString target &&
                second is KTLangString trimChar)
            {
                var trimmed = new string(target.Value.Trim(trimChar.Value));
                return new(trimmed);
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.trim",
                "a string",
                first?.GetType().Name ?? "null"
            );
        }

        private KTLangString Reverse(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("string.reverse", 1, args.Count);

            var first = args.First();

            if (first is KTLangString target)
            {
                var reversed = new string(target.Value.Reverse().ToArray());
                return new(reversed);
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.reverse",
                "a string",
                first?.GetType().Name ?? "null"
            );
        }

        private KTLangBoolean Contains(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("string.contains", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangString target &&
                second is KTLangString value)
                return new(target.Value.Contains(value.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.contains",
                "two strings",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private KTLangBoolean EndsWith(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("string.endsWith", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangString target &&
                second is KTLangString value)
                return new(target.Value.EndsWith(value.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.endsWith",
                "two strings",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private KTLangBoolean StartsWith(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("string.startsWith", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangString target &&
                second is KTLangString value)
                return new(target.Value.StartsWith(value.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.startsWith",
                "two strings",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private KTLangString Replace(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 3)
                throw KTLangExceptionHelper.MissingArguments("string.replace", 3, args.Count);

            var first = args[0];
            var second = args[1];
            var third = args[2];

            if (first is KTLangString target &&
                second is KTLangString oldStr &&
                third is KTLangString newStr)
                return new(target.Value.Replace(oldStr.Value, newStr.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "string.replace",
                "three strings",
                $"{first?.GetType().Name ?? "null"}, {second?.GetType().Name ?? "null"}, and {third?.GetType().Name ?? "null"}"
            );
        }
    }
}
