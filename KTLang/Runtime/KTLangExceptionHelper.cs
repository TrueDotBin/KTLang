using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime
{
    /// <summary>
    /// Helper methods for throwing errors.
    /// </summary>
    public static class KTLangExceptionHelper
    {
        /// <summary>
        /// Used when invalid arguments are provided to a function.
        /// </summary>
        public static Exception InvalidArgumentType(string function, string expected, string actual)
            => new ArgumentException($"Function '{function}' expects {expected}, but got {actual}");

        /// <summary>
        /// Used when not enough arguments are provided to a function.
        /// </summary>
        public static Exception MissingArguments(string function, int expected, int actual)
            => new ArgumentException($"Function '{function}' expects {expected} arguments, but got {actual}");
    }
}
