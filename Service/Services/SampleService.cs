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
    public class SampleService : ISampleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SampleService> _logger;

        public SampleService(IUnitOfWork unitOfWork, ILogger<SampleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

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
    }
}
