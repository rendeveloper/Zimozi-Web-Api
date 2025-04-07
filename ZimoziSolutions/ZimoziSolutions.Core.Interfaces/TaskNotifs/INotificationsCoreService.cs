using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;

namespace ZimoziSolutions.Core.Interfaces.TaskNotifs
{
    public interface INotificationsCoreService
    {
        Task<PaginatedResponse<NotificationsModel>> GetAllAsync(PagerData pagerData);
        Task<NotificationsModel> GetAsync(int id);
        Task<GenericDataRecord> AddAsync(NotificationsModel model);
        Task<GenericDataRecord> UpdateAsync(NotificationsModel model);
        Task<GenericDataRecord> DeleteAsync(int id);
    }
}
