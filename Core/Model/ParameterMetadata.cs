namespace Core.Model
{
    public class ParameterMetadata
    {
        public string Name { get; }
        public TypeMetadata Metadata { get; }

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            Metadata = typeMetadata;
        }
    }
}