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
        public async Task<ActionResult> GetAsync([FromQuery] PagerData pagerData, [FromQuery] bool? activeTasks = false)
        {
            var tasks = await _taskCoreService.GetAllAsync(pagerData, activeTasks ?? false);
            return Ok(tasks);
        }

        /*[HttpPost]
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
        }*/
    }
}
