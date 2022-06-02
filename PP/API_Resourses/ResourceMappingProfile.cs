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
        }
    }
}
