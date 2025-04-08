
using Microsoft.AspNetCore.Mvc;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.UserTask;
using ZimoziSolutions.Core.Interfaces.UserTask;

namespace ZimoziSolutions.Controllers
{
    public class UserTaskController : BaseApiController<UserTaskController>
    {
        private readonly IUserTasksCoreService _userTasksCoreService;
        
        public UserTaskController(IUserTasksCoreService userTasksCoreService)
        {
            _userTasksCoreService = userTasksCoreService;
        }

        [HttpGet("{userId}"), ActionName("GetId")]
        public async Task<ActionResult> GetUserAsync(int userId, [FromQuery] PagerData pagerData)
        {
            var response = await _userTasksCoreService.GetAllAsync(pagerData, userId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserTasksModel model)
        {
            var response = await _userTasksCoreService.AddAsync(model);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] UserTasksModel model)
        {
            var response = await _userTasksCoreService.UpdateAsync(model);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _userTasksCoreService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
