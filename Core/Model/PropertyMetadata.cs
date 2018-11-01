namespace Core.Model
{
    public class PropertyMetadata : BaseMetadata
    {
        public override string Name { get; set; }
        public TypeMetadata Metadata { get; set; }
    }
}