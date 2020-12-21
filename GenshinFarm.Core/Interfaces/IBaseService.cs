using GenshinFarm.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(string id);
        Task Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(string id);
    }
}
