using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Users;

namespace ZimoziSolutions.Core.Interfaces.Users
{
    public interface IUserCoreService
    {
        Task<PaginatedResponse<UserCustomModel>> GetAllAsync(PagerData pagerData, string role);
        Task<UserCustomModel> GetAsync(int id);
        Task<GenericDataRecord> AddAsync(UserCustomModel model);
        Task<GenericDataRecord> UpdateAsync(UserCustomModel model);
        Task<GenericDataRecord> DeleteAsync(int id);
    }
}
