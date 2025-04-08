using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.UserTask;

namespace ZimoziSolutions.Core.Interfaces.UserTask
{
    public interface IUserTasksCoreService
    {
        Task<PaginatedResponse<TaskModel>> GetAllAsync(PagerData pagerData, int userId);
        Task<GenericDataRecord> AddAsync(UserTasksModel model);
        Task<GenericDataRecord> UpdateAsync(UserTasksModel model);
        Task<GenericDataRecord> DeleteAsync(int userId);
    }
}
