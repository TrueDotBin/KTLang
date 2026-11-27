using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Builtins
{
    /// <summary>
    /// The registry for built-in functions.
    /// </summary>
    public static class BuiltinsRegistry
    {
        /// <summary>
        /// The shape of a built-in function.
        /// </summary>
        public delegate KTLangType? KTLangNativeFunction(List<KTLangType?> args, IHost host);

        /// <summary>
        /// The registry for built-in functions (names that resolve to delegates)
        /// </summary>
        public static readonly Dictionary<string, KTLangNativeFunction> Registry = new()
        {
            { "println", CoreBuiltins.PrintLn },
            { "print", CoreBuiltins.Print },
            { "len", CoreBuiltins.Len },
            { "toString", CoreBuiltins.ToStr },
            { "toNumber", CoreBuiltins.ToNum }
        };
    }
}
