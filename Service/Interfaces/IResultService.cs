using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IResultService
    {
        // === For Customer ===
        Task<IEnumerable<Result>> GetResultsByUserIdAsync(string userId);
        Task<Result?> GetResultDetailsAsync(int resultId, string userId);

        // === For Staff ===
        Task<IEnumerable<Result>> GetAllResultsAsync();
        Task<Result?> GetResultByIdAsync(int resultId);
        Task<Result> CreateResultAsync(Result newResult, List<int> sampleIds);
        Task UpdateResultAsync(Result resultToUpdate, List<int> sampleIds);
        Task DeleteResultAsync(int resultId);
    }
}
