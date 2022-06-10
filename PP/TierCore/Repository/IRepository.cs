namespace PP.TierCore.Repository
{
    public interface IRepository<TEntity,TResouce> 
        where TResouce : class
        where TEntity : class
    {
        TEntity Create(TResouce resource);
        TEntity Update(TResouce resource);
        IEnumerable<TEntity> GetList(int take, int skip);
    }
}
