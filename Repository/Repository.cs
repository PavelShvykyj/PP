using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context; 
        public Repository(DbContext context)
        {
            _context = context;
        }
        public void Create(TEntity resource)
        {
           _context
                .Set<TEntity>()
                .Add(resource);
        }
        public TEntity Get(int id)
        {
            var Entity = _context
                .Set<TEntity>()
                .Find(id);
            if (Entity is null)
            {
                return null;
            }
            return Entity;
        }

        public TEntity Get(ushort id)
        {
            var Entity = _context
                .Set<TEntity>()
                .Find(id);
            if (Entity is null)
            {
                return null;
            }
            return Entity;
        }


        public IEnumerable<TEntity> GetList(int take, int skip)
        {
            return _context
                .Set<TEntity>()
                .Skip(skip)
                .Take(take)
                .ToList();
        }
        public void Update(TEntity resource)
        {
            _context
                .Set<TEntity>()
                .Update(resource);
        }
    }
}
