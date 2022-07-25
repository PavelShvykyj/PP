using AutoMapper;
using DTO.DTO;
using DataTier.Models;

namespace CoreTier.MappingProfiles
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Good, GoodDTO>();
            CreateMap<OrderRows, OrderRowsDTO>();
            CreateMap<OrderPaymentProces, OrderPaymentProcesDTO>();
            CreateMap<OrderPaymentDitail, OrderPaymentDitailDTO>();


            CreateMap<Order, OrderDTO>()
                .ForMember(o => o.Goods, d => d.MapFrom(o => o.Goods.ToList()
                    ));



            CreateMap<Order, OrdersListDTO>()
                .ForMember(o => o.CustomerName,
                           d => d.MapFrom(o => o.Customer.Name)
                );


        }
    }
}
