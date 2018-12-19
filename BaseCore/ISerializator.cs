namespace Core.Components
{
    public interface ISerializator<T>
    {
        void Serialize(T _object, string path);
        T Deserialize(string path);
    }
}
