using KTLang.Runtime.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Types
{
    /// <summary>
    /// A module with its own environment.
    /// </summary>
    public sealed class KTLangModuleType(KTLangModule module) : KTLangType
    {
        /// <summary>
        /// The module that should be treated as a type.
        /// </summary>
        public KTLangModule Module { get; } = module;

        /// <inheritdoc/>
        public override KTLangType? CallMethod(Interpreter interpreter, string method, List<KTLangType?> args, IHost host)
        {
            var variable = Module.Environment.Get(method).Value;

            if (variable is not KTLangFunction func)
                throw new Exception($"'{method}' is not a method in the '{Module.Name}' module");

            return func.Call(interpreter, args, host);
        }

        /// <inheritdoc/>
        public override KTLangType? GetMember(string member)
        {
            var variable = Module.Environment.Get(member).Value;
            return variable is null ? throw new Exception($"'{member}' is not a member in the '{Module.Name}' module") : variable;
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"module {Module.Name}";
    }
}
