using Core.Components;
using System;
using System.Collections.Generic;

namespace CompositionConfigurations
{
    public sealed class SecondComposition : ICompositionConfiguration
    {
        private Lazy<SecondComposition> Instance { get; set; } 
            = new Lazy<SecondComposition>(() => new SecondComposition());

        public IReadOnlyDictionary<string, string> ModuleVersions { get; set; } = new Dictionary<string, string>()
        {
            {
                "IStoreProvider", "Reflector"
            }
        };

        public ICompositionConfiguration GetInstance() => Instance.Value;

        private SecondComposition()
        {

        }
    }
}
