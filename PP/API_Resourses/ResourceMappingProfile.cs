using AutoMapper;
using PP.EF.models;

namespace PP.API_Resourses
{
    public class ResourceMappingProfile : Profile
    {
        public ResourceMappingProfile()
        {
            CreateMap<CustomerResource,Customers >()
                .ForMember(c=>c.id, x=>x.Ignore());

            CreateMap<GoodResource, Goods>()
                .ForMember(c => c.id, x => x.Ignore());

            CreateMap<GoodSetRestResouce, Goods>()
                .BeforeMap((r, g) => { g.rest = r.rest; g.id = r.id; } )
                .ForAllMembers(x => x.Ignore());

            CreateMap<GoodSetPriceResouce, Goods>()
                .BeforeMap((r, g) => { g.price = r.price; g.id = r.id; })
                .ForAllMembers(x => x.Ignore());
        }
    }
}
