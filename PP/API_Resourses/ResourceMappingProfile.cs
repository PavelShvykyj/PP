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

            CreateMap<GoodSetRestResouce, Goods>()
                .BeforeMap((r, g) => { g.rest = r.rest; g.id = r.id; } )
                .ForAllMembers(x => x.Ignore());

            CreateMap<GoodSetPriceResouce, Goods>()
                .BeforeMap((r, g) => { g.price = r.price; g.id = r.id; })
                .ForAllMembers(x => x.Ignore());

            CreateMap<OrderRowsSetResource, OrderRows>()
                .ForMember(r=>r.id, x => x.Ignore());

            CreateMap<OrderRows, OrderRowsSetResource>();

            CreateMap<OrderSetResource, OrderSetResource>()
                .ForMember(o => o.rows, x => x.Ignore());

            CreateMap<OrderSetResource, Orders>()
                .ForMember(o => o.id, x => x.Ignore())
                .ForMember(o => o.rows, x => x.Ignore())
                .AfterMap((resourceData, orderData) => {
                    var ExistingRowsData = orderData.rows;
                    
                    var toRemove = ExistingRowsData
                        .Where(
                                r =>!resourceData.rows
                                .Select(resRow => resRow.goodid)
                                .ToArray()
                                .Contains(r.goodid)
                            )
                        .Select(r=> new { goodid = r.goodid })
                        .ToList();
                    
                    var toUpdate = ExistingRowsData
                        .Where(
                            r => resourceData.rows
                            .Select(resRow => resRow.goodid)
                            .ToArray()
                            .Contains(r.goodid)
                        )
                        .Select(r => new { goodid = r.goodid })
                        .ToList();
                     
                   
                    var toAdd = resourceData.rows
                        .Select(r=>r.goodid)
                        .Except(ExistingRowsData.Select(r => r.goodid))
                        .ToArray();

                    if (toRemove.Count() != 0)
                    {
                        foreach (var r in toRemove)
                        {
                            ExistingRowsData.Remove(
                                ExistingRowsData.Single(er=>er.goodid==r.goodid)
                                );
                        }

                        if (toUpdate.Count() != 0)
                        {
                            foreach (var item in toUpdate)
                            {
                                var resRow = resourceData.rows.First(r => r.goodid == item.goodid);
                                var uRow = ExistingRowsData.First(r => r.goodid == item.goodid);
                                uRow.quantity = resRow.quantity;
                                uRow.price = resRow.price;
                            }
                        }


                    }
                    if (toAdd.Count() != 0)
                    {
                        foreach (var goodid in toAdd)
                        {
                            var resRow = resourceData.rows.First(r => r.goodid == goodid);
                            OrderRows newRow = new OrderRows() {
                                goodid = resRow.goodid,
                                quantity = resRow.quantity,
                                price = resRow.price,
                                orderid = orderData.id
                            }; 
                            ExistingRowsData.Add(newRow);
                        }
                    }
                });
        }
    }
}
