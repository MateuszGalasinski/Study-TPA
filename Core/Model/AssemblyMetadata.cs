using System.Collections.Generic;

namespace Core.Model
{
    public class AssemblyMetadata : BaseMetadata
    {
        public override string Name { get; set; }
        public IEnumerable<NamespaceMetadata> Namespaces { get; set; }
    }
}