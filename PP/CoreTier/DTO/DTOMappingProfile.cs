using AutoMapper;
using DataTier.Models;

namespace PP.CoreTier.DTO
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Good, GoodDTO>();
            CreateMap<OrderRows, OrderRowsDTO>();
            
            
            CreateMap<Order, OrderDTO>()
                .ForMember(o=>o.Goods, d=>d.MapFrom(o=>o.Goods.ToList()
                    ));



            CreateMap<Order, OrdersListDTO>()
                .ForMember(o => o.CustomerName,
                           d => d.MapFrom(o => o.Customer.Name)
                );


        }
    }
}
