namespace PP.TierCore.Repository
{
    public interface ICustomerRepository<TEntity, TResouce> : IRepository<TEntity, TResouce>
        where TResouce : class
        where TEntity : class
    {
    }
}
