namespace Repository.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        void Create(TEntity resource);
        void Update(TEntity resource);
        TEntity Get(int id);        
        IEnumerable<TEntity> GetList(int take, int skip);
    }
}
