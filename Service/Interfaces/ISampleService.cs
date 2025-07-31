using Data.Entities;
using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISampleService
    {
        // === For Customer ===
        Task<IEnumerable<Sample>> GetSamplesByDonorIdAsync(string donorId);
        Task<Sample?> GetSampleDetailsAsync(int sampleId, string donorId);

        // === For Staff ===
        Task<IEnumerable<Sample>> GetAllSamplesAsync();
        Task<Sample?> GetSampleByIdAsync(int sampleId);
        Task<Sample> CreateSampleAsync(Sample newSample);
        Task UpdateSampleAsync(Sample sampleToUpdate);
        Task DeleteSampleAsync(int sampleId);
        Task UpdateSampleStatusAsync(int sampleId, SampleStatus newStatus);
    }
}
