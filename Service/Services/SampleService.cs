using Data.Entities;
using Data.Enum;
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
    public class SampleService : ISampleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SampleService> _logger;

        public SampleService(IUnitOfWork unitOfWork, ILogger<SampleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // === Customer Methods ===
        public async Task<IEnumerable<Sample>> GetSamplesByDonorIdAsync(string donorId)
        {
            return await _unitOfWork.Repository<Sample>()
                .Where(s => s.DonorId == donorId && !s.IsDeleted)
                .OrderByDescending(s => s.CollectionDate)
                .ToListAsync();
        }

        public async Task<Sample?> GetSampleDetailsAsync(int sampleId, string donorId)
        {
            return await _unitOfWork.Repository<Sample>()
                .Where(s => s.Id == sampleId && s.DonorId == donorId)
                .FirstOrDefaultAsync();
        }

        // === Staff/Admin Methods ===
        public async Task<IEnumerable<Sample>> GetAllSamplesAsync()
        {
            return await _unitOfWork.Repository<Sample>()
                .Include(s => s.Donor)
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.CollectionDate)
                .ToListAsync();
        }

        public async Task<Sample?> GetSampleByIdAsync(int sampleId)
        {
            return await _unitOfWork.Repository<Sample>()
                .Include(s => s.Donor)
                .Where(s => s.Id == sampleId)
                .FirstOrDefaultAsync();
        }
        
        public async Task<Sample> CreateSampleAsync(Sample newSample)
        {
            await _unitOfWork.Repository<Sample>().CreateAsync(newSample);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Created new sample for Donor ID: {DonorId}", newSample.DonorId);
            return newSample;
        }

        public async Task UpdateSampleAsync(Sample sampleToUpdate)
        {
            _unitOfWork.Repository<Sample>().Update(sampleToUpdate);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Updated Sample ID: {SampleId}", sampleToUpdate.Id);
        }

        public async Task DeleteSampleAsync(int sampleId)
        {
            var sample = await GetSampleByIdAsync(sampleId);
            if (sample != null)
            {
                sample.IsDeleted = true; // Soft delete
                _unitOfWork.Repository<Sample>().Update(sample);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Soft deleted Sample ID: {SampleId}", sampleId);
            }
        }

        public async Task UpdateSampleStatusAsync(int sampleId, SampleStatus newStatus)
        {
            var sample = await GetSampleByIdAsync(sampleId);
            if (sample != null)
            {
                sample.Status = newStatus;
                if (newStatus == SampleStatus.Received && !sample.ReceivedDate.HasValue)
                {
                    sample.ReceivedDate = DateTime.UtcNow;
                }
                _unitOfWork.Repository<Sample>().Update(sample);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}