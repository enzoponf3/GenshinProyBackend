using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GenshinDbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(GenshinDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public virtual async Task<T> GetById(string id)
        {
            return await _entities.FindAsync(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            _context.Remove(entity);
        }
    }
}
