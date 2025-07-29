using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetAllFeedbackAsync();
        Task<Feedback?> GetFeedbackByIdAsync(int id);
        Task<int?> CreateFeedbackAsync(Feedback post);
        Task<int?> UpdateFeedbackAsync(Feedback post);
      //  Task<bool> DeletePostAsync(int id);
        Task<List<Post>> GetFeedbackByUserIdAsync(int userId);

    }
}
