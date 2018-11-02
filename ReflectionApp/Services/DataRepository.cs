using Core.Components;
using Core.Model;
using ReflectionApp.Models;

namespace ReflectionApp.Services
{
    public class DataRepository : IDataRepository
    {
        private IDataSourceProvider _dataSourceProvider;
        private TreeMapper _mapper;

        public DataRepository(IDataSourceProvider dataSourceProvider, TreeMapper treeMapper)
        {
            _dataSourceProvider = dataSourceProvider;
            _mapper = treeMapper;
        }

        public TreeItem LoadTreeRoot(string dataSourcePath)
        {
            AssemblyMetadataStore metaData = _dataSourceProvider.GetAssemblyMetadata(dataSourcePath);
            return _mapper.Map(metaData);
        }
    }
}
