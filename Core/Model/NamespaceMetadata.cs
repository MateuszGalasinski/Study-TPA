using System.Collections.Generic;

namespace Core.Model
{
    public class NamespaceMetadata : BaseMetadata
    {
        public override string Name { get; set; }
        public IEnumerable<TypeMetadata> Types { get; set; }
    }
}