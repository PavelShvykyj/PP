using DataTier.Models;

namespace Repository.Interfaces
{
    public interface IGoodRepository : IRepository<Good>
    {
        void SetRest(ushort id, ushort rest);
        void SetRestToMany(List<Good> resource);
        void SetPrice(ushort id, decimal price);
        void SetPriceToMany(List<Good> resource);
    }
}
