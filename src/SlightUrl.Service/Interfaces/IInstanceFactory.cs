namespace SlightUrl.Service.Interfaces
{
    public interface IInstanceFactory
    {
        T CreateInstance<T>();
    }
}