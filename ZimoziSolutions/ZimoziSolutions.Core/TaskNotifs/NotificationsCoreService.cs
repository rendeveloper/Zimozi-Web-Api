using AutoMapper;
using System.Linq.Expressions;
using System.Net;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Core.Extensions;
using ZimoziSolutions.Core.Interfaces.TaskNotifs;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Exceptions.Business;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Core.TaskNotifs
{
    public class NotificationsCoreService : INotificationsCoreService
    {
        private readonly IMapper _mapper;
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsCoreService(IMapper mapper, INotificationsRepository notificationsRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _notificationsRepository = notificationsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<NotificationsModel>> GetAllAsync(PagerData pagerData)
        {
            IQueryable<Notifications> taskComments = await _notificationsRepository.GetAllAsync();

            Func<OTask, TaskModel> expressTasks = e => new TaskModel
            {
                TaskId = e.Id,
                Description = e.Description,
                Status = e.Status,
                DueDate = e.DueDate,
                AssignedUserId = e.AssignedUserId,
                TaskCommentsId = e.TaskCommentsId,
                NotificationsId = e.NotificationsId
            };

            Expression<Func<Notifications, NotificationsModel>> expression = e => new NotificationsModel
            {
                Id = e.Id,
                TaskUpdates = e.TaskUpdates,
                Tasks = e.Tasks
                    .Select(expressTasks)
                    .OrderBy(x => x.TaskId)
                    .ToList()
            };

            var paginatedList = taskComments
            .Select(expression)
            .OrderBy(x => x.Id)
            .ToPaginatedList(pagerData.PageNumber, pagerData.PageSize);

            return paginatedList;
        }

        public async Task<NotificationsModel> GetAsync(int id)
        {
            Notifications check = await _notificationsRepository.GetByIdAsync(id);

            if (check == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            NotificationsModel response = _mapper.Map<NotificationsModel>(check);
            return response;
        }

        public async Task<GenericDataRecord> AddAsync(NotificationsModel model)
        {
            Notifications check = await _notificationsRepository.GetByIdAsync(model.Id);

            if (check != null)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.TaskValidation), Constants.AlreadyExistsName));


            Notifications map = _mapper.Map<Notifications>(model);

            await _notificationsRepository.AddAsync(map);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = map.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> UpdateAsync(NotificationsModel model)
        {
            Notifications map = _mapper.Map<Notifications>(model);
            _notificationsRepository.Update(map);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = map.Id
            };

            return response;
        }

        public async Task<GenericDataRecord> DeleteAsync(int id)
        {
            Notifications data = await _notificationsRepository.GetByIdAsync(id);

            if (data == null)
                throw new BusinessSolutionException(HttpStatusCode.NotFound, ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.DataNotFound));

            _notificationsRepository.Delete(data);
            await _unitOfWork.SaveAsync();

            GenericDataRecord response = new()
            {
                RecordId = data.Id
            };

            return response;
        }
    }
}
