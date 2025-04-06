using AutoMapper;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Domain.Models;

namespace ZimoziSolutions.Core.Mapping
{
    public class CoreProfile : Profile
    {
        #region Constructor
        public CoreProfile()
        {
            CreateMap<TaskModel, OTask>()
                .ForMember(d => d.Id, o => o.MapFrom(c => c.TaskId));
        }
        #endregion
    }
}
