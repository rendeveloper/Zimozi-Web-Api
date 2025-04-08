using AutoMapper;
using System.Data;
using System.Linq.Expressions;
using System.Net;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.ApiModels.UserTask;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Core.Extensions;
using ZimoziSolutions.Core.Interfaces.UserTask;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Domain.UserTask;
using ZimoziSolutions.Exceptions.Business;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Core.UserTask
{
    public class UserTasksCoreService : IUserTasksCoreService
    {
        private readonly IMapper _mapper;
        private readonly IUserTasksRepository _userTasksRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public UserTasksCoreService(IMapper mapper, IUserTasksRepository userTasksRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userTasksRepository = userTasksRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<TaskModel>> GetAllAsync(PagerData pagerData, int userId)
        {
            IQueryable<OTask> users;

            User checkUser = await _userRepository.GetByIdAsync(userId);

            if (userId > 0)
                users = await _userTasksRepository.GetUserTasks(checkUser);
            else
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));



            Func<UserTasks, UserTasksModel> expressUserTasks = e => new UserTasksModel
            {
                UserId = e.UserId,
                TaskId = e.TaskId
            };

            Expression<Func<OTask, TaskModel>> expression = e => new TaskModel
            {
                TaskId = e.Id,
                Description = e.Description,
                Status = e.Status,
                DueDate = e.DueDate,
                AssignedUserId = e.AssignedUserId,
                TaskCommentsId = e.TaskCommentsId,
                NotificationsId = e.NotificationsId,
                UserTasks = e.UserTasks
                        .Select(expressUserTasks)
                        .OrderBy(x => x.TaskId)
                        .ToList()
            };

            var paginatedList = users
            .Select(expression)
            .OrderBy(x => x.TaskId)
            .ToPaginatedList(pagerData.PageNumber, pagerData.PageSize);

            return paginatedList;
        }

        public async Task<GenericDataRecord> AddAsync(UserTasksModel model)
        {
            if (model is null)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.CustomValidation), Constants.AlreadyExistsUsername));


            UserTasks userTasks = _mapper.Map<UserTasks>(model);


            await _userTasksRepository.AddAsync(userTasks);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = userTasks.UserId
            };

            return response;
        }

        public async Task<GenericDataRecord> UpdateAsync(UserTasksModel model)
        {
            UserTasks userTasks = _mapper.Map<UserTasks>(model);
            _userTasksRepository.Update(userTasks);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = userTasks.UserId
            };

            return response;
        }

        public async Task<GenericDataRecord> DeleteAsync(int userId)
        {
            User data = await _userRepository.GetByIdAsync(userId);

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
