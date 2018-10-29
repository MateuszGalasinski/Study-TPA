namespace Core.Model
{
    public class PropertyMetadata
    {
        public string Name { get; }
        public TypeMetadata Metadata { get; }

        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
          Name = propertyName;
          Metadata = propertyType;
        }
    }
}