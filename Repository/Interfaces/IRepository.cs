namespace Repository.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        TEntity Create(TEntity resource);
        TEntity Update(TEntity resource);
        IEnumerable<TEntity> GetList(int take, int skip);
    }
}
