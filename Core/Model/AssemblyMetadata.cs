using System.Collections.Generic;

namespace Core.Model
{
    public class AssemblyMetadata : BaseMetadata
    {
        public IEnumerable<NamespaceMetadata> Namespaces { get; set; }
    }
}