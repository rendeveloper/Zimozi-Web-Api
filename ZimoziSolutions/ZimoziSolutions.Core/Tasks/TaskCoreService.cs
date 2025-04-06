using AutoMapper;
using ZimoziSolutions.Core.Interfaces.Tasks;
using System.Linq.Expressions;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Core.Extensions;

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

        public async Task<PaginatedResponse<TaskModel>> GetAllAsync(PagerData pagerData, bool activeTasks)
        {
            IQueryable<OTask> tasks;

            if (activeTasks)
                tasks = await _taskRepository.GetListFilteredByActive(activeTasks);
            else
                tasks = await _taskRepository.GetAllAsync();

            Expression<Func<OTask, TaskModel>> expression = e => new TaskModel
            {
                TaskId = e.Id
            };

            var paginatedList = tasks
            .Select(expression)
            .OrderBy(x => x.TaskId)
            .ToPaginatedList(pagerData.PageNumber, pagerData.PageSize);

            return paginatedList;
        }
    }
}
