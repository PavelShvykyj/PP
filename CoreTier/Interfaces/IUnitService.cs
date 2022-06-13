namespace CoreTier.Interfaces
{
    public interface IUnitService<R,T> 
        where R : class
        where T : class
    {
        T Create(R resource);
        T Update(R resource);
        T Get(int id);        
        IEnumerable<T> GetList(int take, int skip);
    }
}
