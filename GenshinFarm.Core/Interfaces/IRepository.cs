using GenshinFarm.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(string id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(string id);

    }
}
