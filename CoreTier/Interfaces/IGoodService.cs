using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;

namespace Repository.Interfaces
{
    public interface IGoodService : IUnitService<GoodResource,GoodDTO>
    {
        void SetRest(ushort id, ushort rest);
        void SetRestToMany(List<GoodSetRestResouce> resource);
        void SetPrice(ushort id, decimal price);
        void SetPriceToMany(List<GoodSetPriceResouce> resource);
    }
}
