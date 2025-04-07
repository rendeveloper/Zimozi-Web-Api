using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Core.Extensions;
using ZimoziSolutions.Core.Interfaces.Users;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Exceptions.Business;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Core.Users
{
    public class UserCoreService : IUserCoreService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCoreService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<UserCustomModel>> GetAllAsync(PagerData pagerData, string role)
        {
            IQueryable<User> users;

            if (!string.IsNullOrEmpty(role))
                users = await _userRepository.GetListFilteredByRole(role);
            else
                users = await _userRepository.GetAllAsync();

            Func<OTask, TaskModel> expressTasks = e => new TaskModel
            {
                TaskId = e.Id,
                Description = e.Description,
                Status = e.Status,
                DueDate = e.DueDate,
                AssignedUserId = e.AssignedUserId,
                TaskCommentsId = e.TaskCommentsId,
                NotificationsId = e.NotificationsId
            };

            Expression<Func<User, UserCustomModel>> expression = e => new UserCustomModel
            {
                Id = e.Id,
                Guid = e.Guid,
                Username = e.Username,
                Password = e.PasswordHash,
                Role = e.Role,
                RefreshToken = e.RefreshToken,
                RefreshTokenExpiryTime = e.RefreshTokenExpiryTime,
                Tasks = e.Tasks
                    .Select(expressTasks)
                    .OrderBy(x => x.TaskId)
                    .ToList()
            };

            var paginatedList = users
            .Select(expression)
            .OrderBy(x => x.Id)
            .ToPaginatedList(pagerData.PageNumber, pagerData.PageSize);

            return paginatedList;
        }

        public async Task<UserCustomModel> GetAsync(int id)
        {
            User checkUser = await _userRepository.GetByIdAsync(id);

            if (checkUser == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            UserCustomModel response = _mapper.Map<UserCustomModel>(checkUser);
            return response;
        }

        public async Task<GenericDataRecord> AddAsync(UserCustomModel userModel)
        {
            User checkUser = await _userRepository.GetByNameAsync(userModel.Username);

            if (checkUser != null)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.CustomValidation), Constants.AlreadyExistsUsername));


            User user = _mapper.Map<User>(userModel);

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, userModel.Password);

            user.Guid = Guid.NewGuid();
            user.PasswordHash = hashedPassword;

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = user.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> UpdateAsync(UserCustomModel userModel)
        {
            User user = _mapper.Map<User>(userModel);
            _userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = user.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> DeleteAsync(int id)
        {
            User data = await _userRepository.GetByIdAsync(id);

            if (data == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            _userRepository.Delete(data);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = data.Id
            };

            return response;
        }
    }
}
