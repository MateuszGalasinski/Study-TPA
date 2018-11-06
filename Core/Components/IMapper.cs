namespace Core.Components
{
    public interface IMapper<TSource, TTarget>
    {
        TTarget Map(TSource objectToMAp);
    }
}
