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

            Environment.Define("sqrt", new KTLangFunction(SquareRoot), true);
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
    }
}
