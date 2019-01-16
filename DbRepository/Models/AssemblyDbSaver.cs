using System.Collections.Generic;
using System.Linq;
using BaseCore.Model;

namespace DbRepository.Models
{
    public class AssemblyDbSaver
    {
        private AssemblyDbSaver()
        {

        }

        public int AssemblyDbSaverId { get; set; }

        public AssemblyDbSaver(AssemblyBase assemblyBase)
        {
            this.Name = assemblyBase.Name;
            Namespaces = assemblyBase.Namespaces?.Select(ns => new NamespaceDbSaver(ns)).ToList();

        }

        public List<NamespaceDbSaver> Namespaces { get; set; }

        public string Name { get; set; }
    }
}
