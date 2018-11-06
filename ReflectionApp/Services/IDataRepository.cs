using ReflectionApp.Models;

namespace ReflectionApp.Services
{
    public interface IDataRepository
    {
        TreeItem LoadTreeRoot(string dataSourcePath);
    }
}
