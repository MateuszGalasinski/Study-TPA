using BaseCore.Model;

namespace Logic.Models
{

    public class ParameterModel
    {
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public ParameterModel(string name, TypeModel typeReader)
        {
            Name = name;
            Type = typeReader;
        }

        public ParameterModel(ParameterBase baseElement)
        {
            Name = baseElement.Name;
            Type = TypeModel.GetOrAdd( baseElement.Type);
        }
    }
}