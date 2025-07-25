using Data.Entities;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Service>().GetAllAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Service>().GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Service entity)
        {
            await _unitOfWork.Repository<Service>().CreateAsync(entity);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateAsync(Service entity)
        {
            await _unitOfWork.Repository<Service>().UpdateAsync(entity);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<Service>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;
            await repo.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
