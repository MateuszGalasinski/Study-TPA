namespace Core.Model
{
    public class ParameterMetadata : BaseMetadata
    {
        public TypeMetadata TypeMetadata { get; set; }

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }
    }
}