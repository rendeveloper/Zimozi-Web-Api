using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;

namespace ZimoziSolutions.Core.Interfaces.Tasks
{
    public interface ITaskCoreService
    {
        Task<PaginatedResponse<TaskModel>> GetAllAsync(PagerData pagerData, int assignedUserId);
        Task<PaginatedResponse<TaskModel>> GetListFilteredByAssignedUserId(PagerData pagerData, int assignedUserId);
        Task<UserCustomModel> GetAsync(int id);
        Task<GenericDataRecord> AddAsync(TaskModel model);
        Task<GenericDataRecord> UpdateAsync(TaskModel model);
        Task<GenericDataRecord> DeleteAsync(int id);
    }
}
