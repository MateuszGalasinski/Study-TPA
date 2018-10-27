namespace Core.Model
{
    internal class ParameterMetadata
    {
        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            _name = name;
            _typeMetadata = typeMetadata;
        }

        private string _name;
        private TypeMetadata _typeMetadata;
    }
}