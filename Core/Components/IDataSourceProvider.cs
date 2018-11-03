using Core.Model;

namespace Core.Components
{
    public interface IDataSourceProvider
    {
        AssemblyMetadataStore GetAssemblyMetadata(string assemblyFile);
    }
}
