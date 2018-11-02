using Core.Components;
using Core.Model;
using ReflectionApp.Models;

namespace ReflectionApp.Services
{
    public class DataRepository
    {
        private IDataSourceProvider _dataSourceProvider;

        public DataRepository(IDataSourceProvider dataSourceProvider)
        {
            _dataSourceProvider = dataSourceProvider;
        }

        public TreeItem LoadTreeRoot(string dataSourcePath)
        {
            AssemblyMetadataStore metaData = _dataSourceProvider.GetAssemblyMetadata(dataSourcePath);
        }
    }
}
