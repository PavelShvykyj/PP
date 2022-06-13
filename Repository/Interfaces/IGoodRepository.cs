using DataTier.Models;

namespace Repository.Interfaces
{
    public interface IGoodRepository : IRepository<Good>
    {
        void SetRest(ushort id, ushort rest);
        void SetRestToMany(Dictionary<ushort,ushort>[] resource);
        void SetPrice(ushort id, decimal price);
        void SetPriceToMany(Dictionary<ushort, ushort>[] resource);
    }
}
