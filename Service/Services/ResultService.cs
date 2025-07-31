using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        // ... (các phương thức cũ giữ nguyên) ...

        public async Task<IEnumerable<Result>> GetResultsByUserIdAsync(string userId)
        {
            _logger.LogInformation("Fetching results for User ID: {UserId}", userId);
            return await _unitOfWork.Repository<Result>()
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .Include(r => r.Service)
                .Include(r => r.Samples)
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

        // === For Staff/Admin (Thêm mới) ===
        public async Task<Result> CreateResultAsync(Result newResult, List<int> sampleIds)
        {
            // Bắt đầu một transaction để đảm bảo toàn vẹn dữ liệu
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Thêm kết quả mới vào DB
                _unitOfWork.Repository<Result>().Create(newResult);
                await _unitOfWork.CommitAsync();

                // Cập nhật các sample được chọn để liên kết với kết quả vừa tạo
                var samplesToUpdate = await _unitOfWork.Repository<Sample>()
                    .Where(s => sampleIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var sample in samplesToUpdate)
                {
                    sample.ResultId = newResult.Id;
                    _unitOfWork.Repository<Sample>().Update(sample);
                }
                await _unitOfWork.CommitAsync();

                // Nếu mọi thứ thành công, commit transaction
                await transaction.CommitAsync();
                _logger.LogInformation("Successfully created Result ID {ResultId} and linked {SampleCount} samples.", newResult.Id, sampleIds.Count);
                return newResult;
            }
            catch (System.Exception ex)
            {
                // Nếu có lỗi, rollback transaction
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating result.");
                throw;
            }
        }
    }
}