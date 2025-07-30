using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.UnitOfWork;
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

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork,
                           UserManager<User> userManager,

                           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

            _mapper = mapper;
        }
        public Task<List<Feedback>> GetAllFeedbackAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<Feedback?> GetFeedbackByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
        }
        public Task<int?> CreateFeedbackAsync(Feedback feedback)
        {
            throw new NotImplementedException();
        }
        public async Task<int?> UpdateFeedbackAsync(Feedback feedback)
        {
            await _unitOfWork.Repository<Feedback>().UpdateAsync(feedback);
            return await _unitOfWork.CommitAsync();
        }
        public Task<List<Post>> GetFeedbackByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
