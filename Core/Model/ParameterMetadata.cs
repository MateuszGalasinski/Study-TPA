namespace Core.Model
{
    public class ParameterMetadata : BaseMetadata
    {
        public override string Name { get; set;  }
        public TypeMetadata Metadata { get; set; }

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            Metadata = typeMetadata;
        }
    }
}