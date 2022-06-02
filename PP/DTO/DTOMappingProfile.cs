using AutoMapper;
using PP.EF.models;

namespace PP.DTO
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<Customers, CustomersDTO>();
            CreateMap<Goods, GoodsDTO>();
        }
    }
}
