using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System;
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

        // === Customer Methods ===
        public async Task<IEnumerable<Result>> GetResultsByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Result>()
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .Include(r => r.Service)
                .Include(r => r.Samples)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<Result?> GetResultDetailsAsync(int resultId, string userId)
        {
            return await _unitOfWork.Repository<Result>()
                .Where(r => r.Id == resultId && r.UserId == userId)
                .Include(r => r.Service)
                .Include(r => r.Samples)
                .FirstOrDefaultAsync();
        }

        // === Staff/Admin Methods ===
        public async Task<IEnumerable<Result>> GetAllResultsAsync()
        {
            return await _unitOfWork.Repository<Result>()
                .Include(r => r.User)
                .Include(r => r.Service)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<Result?> GetResultByIdAsync(int resultId)
        {
            return await _unitOfWork.Repository<Result>()
                .Where(r => r.Id == resultId)
                .Include(r => r.Samples)
                .FirstOrDefaultAsync();
        }

        public async Task<Result> CreateResultAsync(Result newResult, List<int> sampleIds)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Repository<Result>().Create(newResult);
                await _unitOfWork.CommitAsync();

                var samplesToUpdate = await _unitOfWork.Repository<Sample>()
                    .Where(s => sampleIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var sample in samplesToUpdate)
                {
                    sample.ResultId = newResult.Id;
                    _unitOfWork.Repository<Sample>().Update(sample);
                }
                await _unitOfWork.CommitAsync();

                await transaction.CommitAsync();
                _logger.LogInformation("Created Result ID {ResultId}", newResult.Id);
                return newResult;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating result.");
                throw;
            }
        }

        public async Task UpdateResultAsync(Result resultToUpdate, List<int> newSampleIds)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Cập nhật thông tin của Result
                resultToUpdate.LastUpdatedAt = DateTime.UtcNow;
                _unitOfWork.Repository<Result>().Update(resultToUpdate);

                // Lấy các sample cũ đang liên kết với result này
                var oldSamples = await _unitOfWork.Repository<Sample>()
                    .Where(s => s.ResultId == resultToUpdate.Id)
                    .ToListAsync();

                // Bỏ liên kết các sample cũ
                foreach (var sample in oldSamples)
                {
                    sample.ResultId = null;
                    _unitOfWork.Repository<Sample>().Update(sample);
                }

                // Tạo liên kết với các sample mới
                var newSamples = await _unitOfWork.Repository<Sample>()
                    .Where(s => newSampleIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var sample in newSamples)
                {
                    sample.ResultId = resultToUpdate.Id;
                    _unitOfWork.Repository<Sample>().Update(sample);
                }

                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                _logger.LogInformation("Updated Result ID {ResultId}", resultToUpdate.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating result.");
                throw;
            }
        }

        public async Task DeleteResultAsync(int resultId)
        {
            var result = await GetResultByIdAsync(resultId);
            if (result != null)
            {
                result.IsDeleted = true; // Soft delete
                _unitOfWork.Repository<Result>().Update(result);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Soft deleted Result ID: {ResultId}", resultId);
            }
        }
    }
}