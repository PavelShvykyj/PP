namespace CoreTier.Interfaces
{
    public interface IUnitService<R,T> 
        where R : class
        where T : class
    {
        Task<T> CreateAsync(R resource);
        Task<T> UpdateAsync(int id, R resource);
        T Get(int id);        
        IEnumerable<T> GetList(int take, int skip);
    }
}
