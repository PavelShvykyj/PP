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
            CreateMap<OrderRows, OrderRowsDTO>();
            
            
            CreateMap<Orders, OrdersDTO>()
                .ForMember(o=>o.rows, d=>d.MapFrom(o=>o.rows.ToList()
                    ));



            CreateMap<Orders, OrdersListDTO>()
                .ForMember(o => o.customername,
                           d => d.MapFrom(o => o.customer.name)
                );


        }
    }
}
