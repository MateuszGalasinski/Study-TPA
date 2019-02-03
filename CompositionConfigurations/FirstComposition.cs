using Core.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CompositionConfigurations
{
    [Export(typeof(ICompositionConfiguration))]
    public sealed class FirstComposition : ICompositionConfiguration
    {
        private Lazy<FirstComposition> Instance { get; set; }
            = new Lazy<FirstComposition>(() => new FirstComposition());

        public IReadOnlyDictionary<string, string> ModuleVersions { get; set; } = new Dictionary<string, string>()
        {
            {
                "IStoreProvider", "DumpStoreProvider"
            }
        };

        public ICompositionConfiguration GetInstance() => Instance.Value;

        private FirstComposition()
        {

        }
    }
}
