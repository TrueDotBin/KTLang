using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Modules
{
    /// <summary>
    /// A built-in module for the random number generator.
    /// </summary>
    public sealed class RandomModule : KTLangModule
    {
        private Random _rand = new();

        /// <inheritdoc/>
        public override string Name { get; } = "random";

        /// <inheritdoc/>
        public override void Register()
        {
            Environment.Define("seed", new KTLangFunction(Seed), true);
            Environment.Define("next", new KTLangFunction(Next), true);
            Environment.Define("range", new KTLangFunction(Range), true);
            Environment.Define("boolean", new KTLangFunction(Boolean), true);
            Environment.Define("choice", new KTLangFunction(Choice), true);
            Environment.Define("shuffle", new KTLangFunction(Shuffle), true);
        }

        private KTLangNumber Seed(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 1)
                throw KTLangExceptionHelper.MissingArguments("random.seed", 1, args.Count);

            var first = args[0];

            if (first is KTLangNumber seed)
            {
                _rand = new Random((int)seed.Value);
                return seed;
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "random.seed",
                "number",
                first?.GetType().Name ?? "null"
            );
        }

        private KTLangNumber Next(List<KTLangType?> _, IHost __)
            => new(_rand.Next());

        private KTLangNumber Range(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("random.range", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangNumber min &&
                second is KTLangNumber max)
                return new(_rand.Next((int)min.Value, (int)max.Value + 1));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "random.range",
                "two numbers",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private KTLangBoolean Boolean(List<KTLangType?> _, IHost __)
            => new(_rand.Next(2) == 0);

        private KTLangType? Choice(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("random.choice", 1, args.Count);

            var first = args.First();

            if (first is KTLangArray arr)
                return arr.At(_rand.Next(0, (int)arr.GetLength().Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "random.choice",
                "array",
                first?.GetType().Name ?? "null"
            );
        }

        private KTLangType? Shuffle(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("random.shuffle", 1, args.Count);

            var first = args.First();

            if (first is KTLangArray arr)
            {
                var elements = arr.GetList().ToArray();
                _rand.Shuffle<KTLangType?>(elements);

                var outArr = new KTLangArray();

                foreach (var element in elements)
                {
                    outArr.Push(element);
                }

                return outArr;
            }

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "random.shuffle",
                "array",
                first?.GetType().Name ?? "null"
            );
        }
    }
}
