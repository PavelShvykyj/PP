using AutoMapper;
using PP.EF.models;

namespace PP.API_Resourses
{
    public static class ObjHelper {

        public static string[] GetPropNames<T>(T parametr) where T : class
        {
           return parametr.GetType()
                .GetProperties()
                .Select(e => e.Name)
                .ToArray();

        }

    }


    public class ResourceMappingProfile : Profile
    {
        public ResourceMappingProfile()
        {
            



            CreateMap<CustomerResource,Customers >()
                .ForMember(c=>c.id, x=>x.Ignore());

            CreateMap<GoodResource, Goods>()
                .ForMember(c => c.id, x => x.Ignore());
                //.ForAllMembers(x => x.Condition((srs, dest, srsMember) => {
                //    string[] fields = ObjHelper.GetPropNames<object>(srs);
                //    Console.WriteLine(x.DestinationMember.Name +" "+ fields.Contains(x.DestinationMember.Name));
                //    Console.WriteLine(srs.ToString());
                //    return srsMember != null;   
                //    //return fields.Contains(DestinationMember.Name);
                //}));

            CreateMap<GoodSetRestResouce, Goods>()
                .BeforeMap((r, g) => { g.rest = r.rest; g.id = r.id; } )
                .ForAllMembers(x => x.Ignore());

            CreateMap<GoodSetPriceResouce, Goods>()
                .BeforeMap((r, g) => { g.price = r.price; g.id = r.id; })
                .ForAllMembers(x => x.Ignore());
        }
    }
}
