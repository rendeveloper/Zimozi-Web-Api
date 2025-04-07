using Microsoft.AspNetCore.Mvc;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Core.Interfaces.Tasks;

namespace ZimoziSolutions.Controllers
{
    public class TasksController : BaseApiController<TasksController>
    {
        private readonly ITaskCoreService _taskCoreService;

        public TasksController(ITaskCoreService taskCoreService)
        {
            _taskCoreService = taskCoreService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PagerData pagerData, [FromQuery] int? assignedUserId = 0)
        {
            var tasks = await _taskCoreService.GetAllAsync(pagerData, assignedUserId ?? 0);
            return Ok(tasks);
        }

        [HttpGet("{id}"), ActionName("GetId")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var response = await _taskCoreService.GetAsync(id);
            return Ok(response);
        }

        [HttpGet("User/{userId}"), ActionName("GetId")]
        public async Task<ActionResult> GetUserAsync(int userId,[FromQuery] PagerData pagerData)
        {
            var response = await _taskCoreService.GetListFilteredByAssignedUserId(pagerData, userId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TaskModel task)
        {
            var response = await _taskCoreService.AddAsync(task);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] TaskModel task)
        {
            var response = await _taskCoreService.UpdateAsync(task);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _taskCoreService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
