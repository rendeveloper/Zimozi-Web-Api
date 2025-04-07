using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Core.Interfaces.TaskNotifs;

namespace ZimoziSolutions.Controllers
{
    public class NotificationController : BaseApiController<NotificationController>
    {
        private readonly INotificationsCoreService _notificationsService;

        public NotificationController(INotificationsCoreService notificationService)
        {
            _notificationsService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PagerData pagerData)
        {
            var tasks = await _notificationsService.GetAllAsync(pagerData);
            return Ok(tasks);
        }

        [HttpGet("{id}"), ActionName("GetId")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var response = await _notificationsService.GetAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] NotificationsModel model)
        {
            var response = await _notificationsService.AddAsync(model);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] NotificationsModel model)
        {
            var response = await _notificationsService.UpdateAsync(model);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _notificationsService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
