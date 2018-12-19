using System.Collections.Generic;

namespace BaseCore.Model
{
    public class NamespaceBase
    {
        public string Name { get; set; }

        public List<TypeBase> Types { get; set; }
    }
}
