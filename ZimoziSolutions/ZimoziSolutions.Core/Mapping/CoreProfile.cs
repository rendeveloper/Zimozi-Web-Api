using AutoMapper;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Core.Mapping
{
    public class CoreProfile : Profile
    {
        #region Constructor
        public CoreProfile()
        {
            CreateMap<TaskModel, OTask>()
                .ForMember(d => d.Id, o => o.MapFrom(c => c.TaskId));

            CreateMap<UserModel, User>()
                .ForMember(d => d.Username, o => o.MapFrom(c => c.Username));
        }
        #endregion
    }
}
