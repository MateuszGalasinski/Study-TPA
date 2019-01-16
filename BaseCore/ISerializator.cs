namespace BaseCore
{
    public interface ISerializator<T>
    {
        void Serialize(T _object);
        T Deserialize();
    }
}
