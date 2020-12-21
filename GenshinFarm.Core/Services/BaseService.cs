using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
