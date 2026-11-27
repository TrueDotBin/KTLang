using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Modules
{
    /// <summary>
    /// A module that can be imported into a KTLang script.
    /// </summary>
    public abstract class KTLangModule
    {
        /// <summary>
        /// The name of this module, used for identification and importing.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The environment for this module.
        /// </summary>
        public KTLangEnvironment Environment { get; } = new();

        /// <summary>
        /// Registers this module's methods, constants, etc.
        /// </summary>
        public abstract void Register();
    }
}
