using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Cli
{
    /// <summary>
    /// The default host for the KTLang interpreter app.
    /// </summary>
    public class DefaultHost : IHost
    {
        /// <inheritdoc/>
        public void Print(string text)
            => Console.Write(text);

        /// <inheritdoc/>
        public void PrintLn(string text)
            => Console.WriteLine(text);
    }
}
