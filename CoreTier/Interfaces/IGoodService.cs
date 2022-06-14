using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoreTier.Interfaces
{
    public interface IGoodService : IUnitService<GoodResource,GoodDTO>
    {
        void SetRest(ushort id, ushort rest);
        void SetRestToMany(List<GoodSetRestResouce> resource);
        void SetPrice(ushort id, decimal price);
        void SetPriceToMany(List<GoodSetPriceResouce> resource);
        public  Task<GoodDTO> PatchAsync(int id, JsonPatchDocument<Good> resource, ModelStateDictionary modelState)

    }
}
