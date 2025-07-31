using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<int> CreateAsync(Service entity);
        Task<int> UpdateAsync(Service entity);
        Task<bool> DeleteAsync(int id);
    }
}
