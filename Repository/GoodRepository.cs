using DataTier;
using DataTier.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class GoodRepository : Repository<Good> , IGoodRepository
    {
        private ApplicationContext _db
        {
            get { return _context as ApplicationContext; }
        }

        public GoodRepository(ApplicationContext context)
            : base(context)
        {
        }
        public void SetPrice(ushort id, decimal price)
        {
            Good good = _db.Goods.SingleOrDefault(g => g.Id == id);
            if (good is null)
            {
                throw new KeyNotFoundException(String.Format("Good with {id} not found", id));    
            }
            good.Price = price;
            _db.Goods.Update(good);
        }
        public void SetPriceToMany(List<Good> items)
        {
            foreach (var item in items)
            {
                var atached = _db.Goods
                    .Attach(item);

                atached.Property(g => g.Rest)
                    .IsModified = false;

                atached.Property(g => g.Name)
                    .IsModified = false;

                atached.Property(g => g.Price)
                    .IsModified = true;
            }
        }
        public void SetRest(ushort id, ushort rest)
        {
            Good good = _db.Goods.SingleOrDefault(g => g.Id == id);
            if (good is null)
            {
                throw new KeyNotFoundException(String.Format("Good with {Id} not found", id));
            }
            good.Rest = rest;
            _db.Goods.Update(good);

        }
        public void SetRestToMany(List<Good> items)
        {
            foreach (var item in items)
            {
                var atached = _db.Goods
                    .Attach(item);
                atached.Property(g => g.Rest)
                    .IsModified = true;
                atached.Property(g => g.Name)
                    .IsModified = false;
                atached.Property(g => g.Price)
                    .IsModified = false;
            }
        }
    }
}
