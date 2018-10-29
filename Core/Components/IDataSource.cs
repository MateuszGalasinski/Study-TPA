using Core.Model;

namespace Core.Components
{
    public interface IDataSource
    {
        AssemblyMetadata GetAssemblyMetadata(string assemblyFile);
    }
}
