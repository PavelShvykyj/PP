using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoreTier.Interfaces
{
    public interface IGoodService : IUnitService<GoodResource,GoodDTO>
    {
        Task<int> SetRestAsync(ushort id, ushort rest);
        Task<int> SetRestToManyAsync(List<GoodSetRestResouce> resource);
        Task<int> SetPriceAsync(ushort id, decimal price);
        Task<int> SetPriceToManyAsync(List<GoodSetPriceResouce> resource);
        public Task<GoodDTO> PatchAsync(ushort id, JsonPatchDocument<Good> resource, ModelStateDictionary modelState);

    }
}
