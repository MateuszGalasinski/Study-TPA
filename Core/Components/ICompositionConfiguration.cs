using System.Collections.Generic;

namespace Core.Components
{
    public interface ICompositionConfiguration
    {
        IReadOnlyDictionary<string, string> ModuleVersions { get; }

        ICompositionConfiguration GetInstance();
    }
}
