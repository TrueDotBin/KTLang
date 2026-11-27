using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// An import to a module.
    /// </summary>
    public sealed class ImportStatement(Token? name) : Statement
    {
        /// <summary>
        /// The token containing the name of the module that should be imported.
        /// </summary>
        public Token? Name { get; } = name;
    }
}
