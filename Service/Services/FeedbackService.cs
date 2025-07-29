using Data.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        public Task<List<Feedback>> GetAllFeedbackAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Feedback?> GetFeedbackByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<int?> CreateFeedbackAsync(Feedback feedback)
        {
            throw new NotImplementedException();
        }
        public Task<int?> UpdateFeedbackAsync(Feedback feedback)
        {
            throw new NotImplementedException();
        }
        public Task<List<Post>> GetFeedbackByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
