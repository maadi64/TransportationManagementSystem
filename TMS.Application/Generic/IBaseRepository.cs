using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.Generic
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAsync();
        Task<T?> GetByIdAsync(string idField, int id);
        Task<List<T>?> GetListByPropertyAsync(string propertyName, dynamic propertyValue);
        Task<T?> GetSingleByPropertyAsync(string propertyName, dynamic propertyValue);
        Task<int> PostAsync(T entity);
        Task<bool> UpdateAsync(string idField, T entity);
        Task<bool> DeleteAsync(string idField, int id);
    }
}
