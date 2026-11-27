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
                throw new Exception("math.sqrt() requires at least one argument");

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Sqrt(number.Value));

            throw new Exception("Cannot get square root of specified type, must be number");
        }

        private static KTLangNumber Absolute(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw new Exception("math.abs() requires at least one argument");

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Abs(number.Value));

            throw new Exception("Cannot get absolute value of specified type, must be number");
        }

        private static KTLangNumber Floor(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw new Exception("math.floor() requires at least one argument");

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Floor(number.Value));

            throw new Exception("Cannot round down specified type, must be number");
        }

        private static KTLangNumber Ceiling(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw new Exception("math.ceil() requires at least one argument");

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Ceiling(number.Value));

            throw new Exception("Cannot round up specified type, must be number");
        }

        private static KTLangNumber Round(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw new Exception("math.round() requires at least one argument");

            var first = args.First();

            if (first is KTLangNumber number)
                return new(Math.Round(number.Value));

            throw new Exception("Cannot round specified type, must be number");
        }

        private static KTLangNumber Power(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw new Exception("math.power() requires at least two arguments");

            var first = args[0];
            var second = args[1];

            if (first is KTLangNumber firstNum && second is KTLangNumber power)
                return new(Math.Pow(firstNum.Value, power.Value));

            throw new Exception("Cannot calculate power of specified type, must be number");
        }

        private static KTLangNumber Min(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw new Exception("math.min() requires at least two arguments");

            var first = args[0];
            var second = args[1];

            if (first is KTLangNumber firstNum && second is KTLangNumber secondNum)
                return new(Math.Min(firstNum.Value, secondNum.Value));

            throw new Exception("Cannot compare specified types, must be number");
        }

        private static KTLangNumber Max(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 2)
                throw new Exception("math.max() requires at least two arguments");

            var first = args[0];
            var second = args[1];

            if (first is KTLangNumber firstNum && second is KTLangNumber secondNum)
                return new(Math.Max(firstNum.Value, secondNum.Value));

            throw new Exception("Cannot compare specified types, must be number");
        }

        private static KTLangNumber Clamp(List<KTLangType?> args, IHost _)
        {
            if (args.Count < 3)
                throw new Exception("math.clamp() requires at least three arguments");

            var first = args[0];
            var second = args[1];
            var third = args[2];

            if (first is KTLangNumber firstNum && second is KTLangNumber min && third is KTLangNumber max)
                return new(Math.Clamp(firstNum.Value, min.Value, max.Value));

            throw new Exception("Cannot clamp specified types, must be number");
        }
    }
}
