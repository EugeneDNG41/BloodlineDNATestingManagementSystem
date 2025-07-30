using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ResultService : IResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResultService> _logger;

        public ResultService(IUnitOfWork unitOfWork, ILogger<ResultService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Result>> GetResultsByUserIdAsync(string userId)
        {
            _logger.LogInformation("Fetching results for User ID: {UserId}", userId);
            return await _unitOfWork.Repository<Result>()
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .Include(r => r.Service) // Lấy kèm thông tin dịch vụ
                .Include(r => r.Samples) // Lấy kèm thông tin các mẫu thử
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<Result?> GetResultDetailsAsync(int resultId, string userId)
        {
            _logger.LogInformation("Fetching details for Result ID: {ResultId}", resultId);
            return await _unitOfWork.Repository<Result>()
                .Where(r => r.Id == resultId && r.UserId == userId)
                .Include(r => r.Service)
                .Include(r => r.Samples)
                .FirstOrDefaultAsync();
        }
    }
}
