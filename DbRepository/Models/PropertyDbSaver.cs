using System.Collections.Generic;
using BaseCore.Model;

namespace DbRepository.Models
{
    public class PropertyDbSaver
    {
        private PropertyDbSaver()
        {
            TypeProperties = new HashSet<TypeDbSaver>();
        }
        public int PropertyDbSaverId { get; set; }

        public PropertyDbSaver(PropertyBase baseProperty)
        {

            this.Name = baseProperty.Name ?? "default";
            this.Type = TypeDbSaver.GetOrAdd(baseProperty.Type);
        }

        public string Name { get; set; }

        public TypeDbSaver Type { get; set; }

        public ICollection<TypeDbSaver> TypeProperties { get; set; }
    }
}
