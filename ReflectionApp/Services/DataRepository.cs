using Core.Components;
using Core.Model;
using ReflectionApp.Models;

namespace ReflectionApp.Services
{
    public class DataRepository : IDataRepository
    {
        private IStoreProvider _dataSourceProvider;
        private TreeMapper _mapper;

        public DataRepository(IStoreProvider dataSourceProvider, TreeMapper treeMapper)
        {
            _dataSourceProvider = dataSourceProvider;
            _mapper = treeMapper;
        }

        public TreeItem LoadTreeRoot(string dataSourcePath)
        {
            AssemblyMetadataStore metaData = _dataSourceProvider.GetAssemblyMetadataStore(dataSourcePath);
            return _mapper.Map(metaData);
        }
    }
}
