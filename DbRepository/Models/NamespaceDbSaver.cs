using System.Collections.Generic;
using System.Linq;
using BaseCore.Model;

namespace DbRepository.Models
{
    public class NamespaceDbSaver
    {
        private NamespaceDbSaver()
        {

        }
        public int NamespaceDbSaverId { get; set; }

        public NamespaceDbSaver(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(t => TypeDbSaver.GetOrAdd(t)).ToList();
        }

        public string Name { get; set; }

        public List<TypeDbSaver> Types { get; set; }
    }
}
