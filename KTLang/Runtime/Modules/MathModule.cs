using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Modules
{
    /// <summary>
    /// A built-in module for mathematical operations.
    /// </summary>
    public sealed class MathModule : KTLangModule
    {
        /// <inheritdoc/>
        public override string Name { get; } = "math";

        /// <inheritdoc/>
        public override void Register()
        {
            Environment.Define("PI", new KTLangNumber(Math.PI), true);
            Environment.Define("TAU", new KTLangNumber(Math.Tau), true);
            Environment.Define("E", new KTLangNumber(Math.E), true);
            Environment.Define("INF", new KTLangNumber(double.PositiveInfinity), true);
            Environment.Define("NEGINF", new KTLangNumber(double.NegativeInfinity), true);
            Environment.Define("NAN", new KTLangNumber(double.NaN), true);

            Environment.Define("sqrt", new KTLangFunction(SquareRoot), true);
            Environment.Define("abs", new KTLangFunction(Absolute), true);
            Environment.Define("floor", new KTLangFunction(Floor), true);
            Environment.Define("ceil", new KTLangFunction(Ceiling), true);
            Environment.Define("round", new KTLangFunction(Round), true);
            Environment.Define("pow", new KTLangFunction(Power), true);
            Environment.Define("min", new KTLangFunction(Min), true);
            Environment.Define("max", new KTLangFunction(Max), true);
            Environment.Define("clamp", new KTLangFunction(Clamp), true);
        }

        private static KTLangNumber SquareRoot(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("math.sqrt", 1, args.Count);

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Sqrt(number.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.sqrti",
                "number",
                first?.GetType().Name ?? "null"
            );
        }

        private static KTLangNumber Absolute(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("math.abs", 1, args.Count);

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Abs(number.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.abs",
                "number",
                first?.GetType().Name ?? "null"
            );
        }

        private static KTLangNumber Floor(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("math.floor", 1, args.Count);

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Floor(number.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.floor",
                "number",
                first?.GetType().Name ?? "null"
            );
        }

        private static KTLangNumber Ceiling(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("math.ceil", 1, args.Count);

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Ceiling(number.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.ceil",
                "number",
                first?.GetType().Name ?? "null"
            );
        }

        private static KTLangNumber Round(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw KTLangExceptionHelper.MissingArguments("math.round", 2, args.Count);

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Round(number.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.round",
                "number",
                first?.GetType().Name ?? "null"
            );
        }

        private static KTLangNumber Power(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("math.pow", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangNumber firstNum && second is KTLangNumber power)
                return new(Math.Pow(firstNum.Value, power.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.pow",
                "two numbers",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private static KTLangNumber Min(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("math.min", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangNumber firstNum && second is KTLangNumber secondNum)
                return new(Math.Min(firstNum.Value, secondNum.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.min",
                "two numbers",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private static KTLangNumber Max(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw KTLangExceptionHelper.MissingArguments("math.max", 2, args.Count);

            var first = args[0];
            var second = args[1];

            if (first is KTLangNumber firstNum && second is KTLangNumber secondNum)
                return new(Math.Max(firstNum.Value, secondNum.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.max",
                "two numbers",
                $"{first?.GetType().Name ?? "null"} and {second?.GetType().Name ?? "null"}"
            );
        }

        private static KTLangNumber Clamp(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 3)
                throw KTLangExceptionHelper.MissingArguments("math.clamp", 3, args.Count);

            var first = args[0];
            var second = args[1];
            var third = args[2];

            if (first is KTLangNumber firstNum && second is KTLangNumber min && third is KTLangNumber max)
                return new(Math.Clamp(firstNum.Value, min.Value, max.Value));

            throw KTLangExceptionHelper.InvalidArgumentType
            (
                "math.clamp",
                "three numbers",
                $"{first?.GetType().Name ?? "null"}, {second?.GetType().Name ?? "null"} and {third?.GetType().Name ?? "null"}"
            );
        }
    }
}
