using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime
{
    /// <summary>
    /// A variable in KTLang.
    /// </summary>
    public class KTLangVariable(KTLangType? value = null, bool isConstant = false)
    {
        /// <summary>
        /// The value of this variable.
        /// </summary>
        public KTLangType? Value { get; set; } = value;

        /// <summary>
        /// Whether this variable is mutable.
        /// </summary>
        public bool Constant { get; } = isConstant;
    }
}
