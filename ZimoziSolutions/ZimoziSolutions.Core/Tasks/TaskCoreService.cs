using AutoMapper;
using ZimoziSolutions.Core.Interfaces.Tasks;
using System.Linq.Expressions;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Core.Extensions;
using System.Net;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Exceptions.Business;
using ZimoziSolutions.Common.Constants;

namespace ZimoziSolutions.Core.Tasks
{
    public class TaskCoreService : ITaskCoreService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskCoreService(IMapper mapper, ITaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<TaskModel>> GetAllAsync(PagerData pagerData, int assignedUserId)
        {
            IQueryable<OTask> tasks;

            if (assignedUserId > 0)
                tasks = await _taskRepository.GetListFilteredByAssignedUserId(assignedUserId);
            else
                tasks = await _taskRepository.GetAllAsync();

            Expression<Func<OTask, TaskModel>> expression = e => new TaskModel
            {
                TaskId = e.Id,
                Description = e.Description,
                Status = e.Status,
                DueDate = e.DueDate,
                AssignedUserId = e.AssignedUserId,
            };

            var paginatedList = tasks
            .Select(expression)
            .OrderBy(x => x.TaskId)
            .ToPaginatedList(pagerData.PageNumber, pagerData.PageSize);

            return paginatedList;
        }

        public async Task<PaginatedResponse<TaskModel>> GetListFilteredByAssignedUserId(PagerData pagerData, int assignedUserId)
        {
            IQueryable<OTask> tasks;

            if (assignedUserId > 0)
                tasks = await _taskRepository.GetListFilteredByAssignedUserId(assignedUserId);
            else
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            Expression<Func<OTask, TaskModel>> expression = e => new TaskModel
            {
                TaskId = e.Id,
                Description = e.Description,
                Status = e.Status,
                DueDate = e.DueDate,
                AssignedUserId = e.AssignedUserId,
            };

            var paginatedList = tasks
            .Select(expression)
            .OrderBy(x => x.TaskId)
            .ToPaginatedList(pagerData.PageNumber, pagerData.PageSize);

            return paginatedList;
        }

        public async Task<UserCustomModel> GetAsync(int id)
        {
            OTask checkTask = await _taskRepository.GetByIdAsync(id);

            if (checkTask == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            UserCustomModel response = _mapper.Map<UserCustomModel>(checkTask);
            return response;
        }

        public async Task<GenericDataRecord> AddAsync(TaskModel taskModel)
        {
            OTask checkTask = await _taskRepository.GetByIdAsync(taskModel.TaskId);

            if (checkTask != null)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.TaskValidation), Constants.AlreadyExistsName));


            OTask task = _mapper.Map<OTask>(taskModel);

            await _taskRepository.AddAsync(task);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = task.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> UpdateAsync(TaskModel taskModel)
        {
            OTask task = _mapper.Map<OTask>(taskModel);
            _taskRepository.Update(task);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = task.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> DeleteAsync(int id)
        {
            OTask data = await _taskRepository.GetByIdAsync(id);

            if (data == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            _taskRepository.Delete(data);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = data.Id
            };

            return response;
        }
    }
}
