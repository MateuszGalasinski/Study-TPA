using Core.Model;

namespace Core.Components
{
    public interface IStoreProvider
    {
        AssemblyMetadataStore GetAssemblyMetadataStore(string assemblyFile);
    }
}
