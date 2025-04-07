using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;

namespace ZimoziSolutions.Core.Interfaces.TaskComments
{
    public interface ITaskCommentsCoreService
    {
        Task<PaginatedResponse<TaskCommentsModel>> GetAllAsync(PagerData pagerData);
        Task<TaskCommentsModel> GetAsync(int id);
        Task<GenericDataRecord> AddAsync(TaskCommentsModel model);
        Task<GenericDataRecord> UpdateAsync(TaskCommentsModel model);
        Task<GenericDataRecord> DeleteAsync(int id);
    }
}
