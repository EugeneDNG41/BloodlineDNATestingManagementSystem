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
        Task<IEnumerable<Result>> GetResultsByUserIdAsync(string userId);
        Task<Result?> GetResultDetailsAsync(int resultId, string userId);
    }
}
