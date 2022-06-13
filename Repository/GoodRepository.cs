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
            var Good = _db.Goods.SingleOrDefault(g => g.Id == id);
            if (Good is null)
            {
                throw new KeyNotFoundException(String.Format("Good with {Id} not found", id));    
            }
            Good.Price = price;
        }
        public void SetPriceToMany(List<Good> items)
        {
            foreach (var item in items)
            {
                _db.Goods
                    .Attach(item)
                    .Property(g => g.Rest)
                    .IsModified = true;
            }
        }
        public void SetRest(ushort id, ushort rest)
        {
            var Good = _db.Goods.SingleOrDefault(g => g.Id == id);
            if (Good is null)
            {
                throw new KeyNotFoundException(String.Format("Good with {Id} not found", id));
            }
            Good.Rest = rest;
        }
        public void SetRestToMany(List<Good> items)
        {
            foreach (var item in items)
            {
                _db.Goods
                    .Attach(item)
                    .Property(g => g.Rest)
                    .IsModified = true;
            }
        }
    }
}
