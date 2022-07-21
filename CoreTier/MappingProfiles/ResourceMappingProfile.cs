using AutoMapper;
using DTO.APIResourses;
using DataTier.Models;

namespace CoreTier.MappingProfiles
{
    public static class ObjHelper
    {
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

            CreateMap<SignInResource, User>()
                .ForMember(u => u.Customer, x => x.Ignore())
                .ForMember(u => u.UserName, r => r.MapFrom(x => x.Name));

            CreateMap<SignInResource, Customer>()
                .ForMember(c => c.Id, x => x.Ignore());
                




            CreateMap<CustomerResource, Customer>()
                .ForMember(c => c.Id, x => x.Ignore());

            CreateMap<GoodResource, Good>()
                .ForMember(c => c.Id, x => x.Ignore());

            CreateMap<GoodSetRestResouce, Good>()
                .BeforeMap((r, g) => { g.Rest = r.Rest; g.Id = r.Id; })
                .ForAllMembers(x => x.Ignore());

            CreateMap<GoodSetPriceResouce, Good>()
                .BeforeMap((r, g) => { g.Price = r.Price; g.Id = r.Id; })
                .ForAllMembers(x => x.Ignore());

            CreateMap<OrderGoodsSetResource, OrderRows>()
                .ForMember(r => r.Id, x => x.Ignore());

            CreateMap<OrderRows, OrderGoodsSetResource>();

            CreateMap<OrderSetResource, OrderSetResource>()
                .ForMember(o => o.Goods, x => x.Ignore());

            CreateMap<OrderSetResource, Order>()
                .ForMember(o => o.Id, x => x.Ignore())
                .ForMember(o => o.Goods, x => x.Ignore())
                .AfterMap((resourceData, orderData) =>
                {
                    var ExistingRowsData = orderData.Goods;

                    var toRemove = ExistingRowsData
                        .Where(
                                r => !resourceData.Goods
                                .Select(resRow => resRow.GoodId)
                                .ToArray()
                                .Contains(r.GoodId)
                            )
                        .Select(r => new { goodid = r.GoodId })
                        .ToList();

                    var toUpdate = ExistingRowsData
                        .Where(
                            r => resourceData.Goods
                            .Select(resRow => resRow.GoodId)
                            .ToArray()
                            .Contains(r.GoodId)
                        )
                        .Select(r => new { goodid = r.GoodId })
                        .ToList();


                    var toAdd = resourceData.Goods
                        .Select(r => r.GoodId)
                        .Except(ExistingRowsData.Select(r => r.GoodId))
                        .ToArray();

                    if (toRemove.Count() != 0)
                    {
                        foreach (var r in toRemove)
                        {
                            ExistingRowsData.Remove(
                                ExistingRowsData.Single(er => er.GoodId == r.goodid)
                                );
                        }

                        if (toUpdate.Count() != 0)
                        {
                            foreach (var item in toUpdate)
                            {
                                var resRow = resourceData.Goods.First(r => r.GoodId == item.goodid);
                                var uRow = ExistingRowsData.First(r => r.GoodId == item.goodid);
                                uRow.Quantity = resRow.Quantity;
                                uRow.Price = resRow.Price;
                            }
                        }
                    }
                    if (toAdd.Count() != 0)
                    {
                        foreach (var goodid in toAdd)
                        {
                            var resRow = resourceData.Goods.First(r => r.GoodId == goodid);
                            OrderRows newRow = new OrderRows()
                            {
                                GoodId = resRow.GoodId,
                                Quantity = resRow.Quantity,
                                Price = resRow.Price,
                                OrderId = orderData.Id
                            };
                            ExistingRowsData.Add(newRow);
                        }
                    }
                });
        }
    }
}
