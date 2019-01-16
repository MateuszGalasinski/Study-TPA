using System.Collections.Generic;
using BaseCore.Model;

namespace DbRepository.Models
{
    public class ParameterDbSaver
    {
        private ParameterDbSaver()
        {
            MethodParameters = new HashSet<MethodDbSaver>();
            TypeFields = new HashSet<TypeDbSaver>();

        }
        public int ParameterDbSaverId { get; set; }

        public ParameterDbSaver(ParameterBase baseParameter)
        {

            this.Name = baseParameter.Name;
            this.Type = TypeDbSaver.GetOrAdd(baseParameter.Type);
        }

        public string Name { get; set; }

        public TypeDbSaver Type { get; set; }

        public ICollection<MethodDbSaver> MethodParameters { get; set; }

        public ICollection<TypeDbSaver> TypeFields { get; set; }
    }
}
