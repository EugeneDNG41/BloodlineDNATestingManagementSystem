using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SampleService : ISampleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SampleService> _logger;

        public SampleService(IUnitOfWork unitOfWork, ILogger<SampleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // === For Customer ===
        public async Task<IEnumerable<Sample>> GetSamplesByDonorIdAsync(string donorId)
        {
            _logger.LogInformation("Fetching samples for Donor ID: {DonorId}", donorId);
            return await _unitOfWork.Repository<Sample>()
                .Where(s => s.DonorId == donorId && !s.IsDeleted)
                .OrderByDescending(s => s.CollectionDate)
                .ToListAsync();
        }

        public async Task<Sample?> GetSampleDetailsAsync(int sampleId, string donorId)
        {
            _logger.LogInformation("Fetching details for Sample ID: {SampleId}", sampleId);
            return await _unitOfWork.Repository<Sample>()
                .Where(s => s.Id == sampleId && s.DonorId == donorId)
                .FirstOrDefaultAsync();
        }

        // === For Staff/Admin ===
        public async Task<IEnumerable<Sample>> GetAllSamplesAsync()
        {
            _logger.LogInformation("Fetching all samples for staff/admin.");
            return await _unitOfWork.Repository<Sample>()
                .Include(s => s.Donor) // Lấy thông tin người hiến mẫu
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.CollectionDate)
                .ToListAsync();
        }

        public async Task<Sample?> GetSampleByIdAsync(int sampleId)
        {
            return await _unitOfWork.Repository<Sample>().GetByIdAsync(sampleId);
        }

        public async Task UpdateSampleStatusAsync(int sampleId, SampleStatus newStatus)
        {
            var sample = await GetSampleByIdAsync(sampleId);
            if (sample != null)
            {
                sample.Status = newStatus;
                if (newStatus == SampleStatus.Received && !sample.ReceivedDate.HasValue)
                {
                    sample.ReceivedDate = System.DateTime.UtcNow;
                }
                _unitOfWork.Repository<Sample>().Update(sample);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}