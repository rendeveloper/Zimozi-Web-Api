using AutoMapper;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using System.Linq.Expressions;
using ZimoziSolutions.Core.Extensions;
using ZimoziSolutions.Exceptions.Business;
using System.Net;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Core.Interfaces.TaskComments;

namespace ZimoziSolutions.Core.TaskComment
{
    public class TaskCommentsCoreService : ITaskCommentsCoreService
    {
        private readonly IMapper _mapper;
        private readonly ITaskCommentsRepository _taskCommentsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskCommentsCoreService(IMapper mapper, ITaskCommentsRepository taskCommentsRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _taskCommentsRepository = taskCommentsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<TaskCommentsModel>> GetAllAsync(PagerData pagerData)
        {
            IQueryable<TaskComments> taskComments = await _taskCommentsRepository.GetAllAsync();

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

            Expression<Func<TaskComments, TaskCommentsModel>> expression = e => new TaskCommentsModel
            {
                Id = e.Id,
                Comments = e.Comments,
                Tasks = e.Tasks
                    .Select(expressTasks)
                    .OrderBy(x => x.TaskId)
                    .ToList()
            };

            var paginatedList = taskComments
            .Select(expression)
            .OrderBy(x => x.Id)
            .ToPaginatedList(pagerData.PageNumber, pagerData.PageSize);

            return paginatedList;
        }

        public async Task<TaskCommentsModel> GetAsync(int id)
        {
            TaskComments checkTask = await _taskCommentsRepository.GetByIdAsync(id);

            if (checkTask == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            TaskCommentsModel response = _mapper.Map<TaskCommentsModel>(checkTask);
            return response;
        }

        public async Task<GenericDataRecord> AddAsync(TaskCommentsModel model)
        {
            TaskComments checkTask = await _taskCommentsRepository.GetByIdAsync(model.Id);

            if (checkTask != null)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.TaskValidation), Constants.AlreadyExistsName));


            TaskComments task = _mapper.Map<TaskComments>(model);

            await _taskCommentsRepository.AddAsync(task);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = task.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> UpdateAsync(TaskCommentsModel model)
        {
            TaskComments task = _mapper.Map<TaskComments>(model);
            _taskCommentsRepository.Update(task);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = task.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> DeleteAsync(int id)
        {
            TaskComments data = await _taskCommentsRepository.GetByIdAsync(id);

            if (data == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            _taskCommentsRepository.Delete(data);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = data.Id
            };

            return response;
        }
    }
}
