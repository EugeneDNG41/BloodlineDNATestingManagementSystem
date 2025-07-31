using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> HasUserReviewedServiceAsync(string userId, int serviceId);
        Task<Review?> GetUserReviewForServiceAsync(string userId, int serviceId);
        Task<List<Review>> GetReviewsForServiceAsync(int serviceId);
        Task<int> CreateReviewAsync(Review review);
    }
}
