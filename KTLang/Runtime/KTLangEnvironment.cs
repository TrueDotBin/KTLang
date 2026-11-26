using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime
{
    /// <summary>
    /// Manages KTLang's variables.
    /// </summary>
    public class KTLangEnvironment(KTLangEnvironment? enclosing = null)
    {
        private readonly Dictionary<string, KTLangVariable> _vars = [];

        /// <summary>
        /// The parent scope's environment.
        /// </summary>
        public KTLangEnvironment? Enclosing { get; private set; } = enclosing;

        /// <summary>
        /// Gets a variable by its name.
        /// </summary>
        public KTLangVariable Get(string name)
        {
            if (_vars.TryGetValue(name, out var value))
                return value;

            if (Enclosing is not null)
                return Enclosing.Get(name);

            throw new Exception($"Undefined variable '{name}'");
        }

        /// <summary>
        /// Defines a variable.
        /// </summary>
        public void Define(string name, KTLangType? value, bool constant = false)
        {
            if (_vars.ContainsKey(name))
                throw new Exception($"Variable '{name}' is already defined");

            _vars[name] = new(value, constant);
        }

        /// <summary>
        /// Sets a variable's value by its name.
        /// </summary>
        public void Assign(string name, KTLangType? value)
        {
            if (_vars.ContainsKey(name))
            {
                var variable = _vars[name];
                if (variable.Constant)
                    throw new Exception($"Cannot modify constant variable '{name}'");

                variable.Value = value;
                return;
            }

            if (Enclosing is not null)
            {
                Enclosing.Assign(name, value);
                return;
            }

            throw new Exception($"Undefined variable '{name}'");
        }
    }
}
