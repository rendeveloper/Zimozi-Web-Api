using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;

namespace ZimoziSolutions.Core.Interfaces.Tasks
{
    public interface ITaskCoreService
    {
        Task<PaginatedResponse<TaskModel>> GetAllAsync(PagerData pagerData, bool activeTasks);
        /*Task<GenericDataRecord> AddAsync(TaskModel taskModel);
        Task<GenericDataRecord> UpdateAsync(TaskModel taskModel);*/
    }
}
