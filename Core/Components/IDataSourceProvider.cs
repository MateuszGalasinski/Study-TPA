using System.Collections.Generic;
using Core.Model;

namespace Core.Components
{
    public interface IDataSourceProvider
    {
        Dictionary<string, BaseMetadata> GetAssemblyMetadata(string assemblyFile);
    }
}
