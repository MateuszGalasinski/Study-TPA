using Core.Model;

namespace ReflectionApp.Models
{
    public class TreeMapper
    {
        public TreeItem Map(AssemblyMetadataStore store)
        {
            TreeItem root = new TreeItem(store.AssemblyMetadata.Name);

            // TODO: implement mapping above
            return root;
        }
    }
}
