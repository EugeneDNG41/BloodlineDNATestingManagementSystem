using Data.Entities;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> HasUserReviewedServiceAsync(string userId, int serviceId)
        {
            var repo = _unitOfWork.Repository<Review>();
            var review = await repo
                .AsQueryable()
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ServiceId == serviceId);
            return review != null;
        }

        public async Task<Review?> GetUserReviewForServiceAsync(string userId, int serviceId)
        {
            var repo = _unitOfWork.Repository<Review>();
            return await repo
                .AsQueryable()
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ServiceId == serviceId);
        }

        public async Task<List<Review>> GetReviewsForServiceAsync(int serviceId)
        {
            var repo = _unitOfWork.Repository<Review>();
            return await repo
                .AsQueryable()
                .Include(r => r.User)
                .Where(r => r.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<int> CreateReviewAsync(Review review)
        {
            await _unitOfWork.Repository<Review>().CreateAsync(review);
            return await _unitOfWork.CommitAsync();
        }
    }
}
