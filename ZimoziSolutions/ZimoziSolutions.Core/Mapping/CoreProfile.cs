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
                .ForMember(d => d.Id, o => o.MapFrom(c => c.TaskId))
                .ForMember(d => d.Description, o => o.MapFrom(c => c.Description))
                .ForMember(d => d.Status, o => o.MapFrom(c => c.Status))
                .ForMember(d => d.DueDate, o => o.MapFrom(c => c.DueDate))
                .ForMember(d => d.AssignedUserId, o => o.MapFrom(c => c.AssignedUserId));

            CreateMap<UserModel, User>()
                .ForMember(d => d.Username, o => o.MapFrom(c => c.Username));

            CreateMap<UserCustomModel, User>()
                .ForMember(d => d.Id, o => o.MapFrom(c => c.Id))
                .ForMember(d => d.Guid, o => o.MapFrom(c => c.Guid))
                .ForMember(d => d.Username, o => o.MapFrom(c => c.Username))
                .ForMember(d => d.PasswordHash, o => o.MapFrom(c => c.Password))
                .ForMember(d => d.Role, o => o.MapFrom(c => c.Role))
                .ForMember(d => d.RefreshToken, o => o.MapFrom(c => c.RefreshToken))
                .ForMember(d => d.RefreshTokenExpiryTime, o => o.MapFrom(c => c.RefreshTokenExpiryTime))
                .ReverseMap();
        }
        #endregion
    }
}
