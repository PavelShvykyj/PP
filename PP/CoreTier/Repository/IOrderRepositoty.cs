namespace PP.TierCore.Repository
{
    public interface IOrderRepositoty<TEntity, TResouce> : IRepository<TEntity, TResouce>
        where TResouce : class
        where TEntity : class
    {

    }
}
