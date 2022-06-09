namespace PP.TierCore.Repository
{
    public interface IGoodRepository<TEntity, TResouce> : IRepository<TEntity, TResouce>
        where TResouce : class
        where TEntity : class
    {
        void SetRest(ushort id, ushort rest);
        void SetRestToMany(TResouce resource);
        void SetPrice(ushort id, decimal price);
        void SetPriceToMany(TResouce[] resource);
    }
}
