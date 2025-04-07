using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Core.Interfaces.TaskComments;

namespace ZimoziSolutions.Controllers
{
    public class CommentController : BaseApiController<TasksController>
    {
        private readonly ITaskCommentsCoreService _taskCommentsCoreService;

        public CommentController(ITaskCommentsCoreService taskCommentsCoreService)
        {
            _taskCommentsCoreService = taskCommentsCoreService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PagerData pagerData)
        {
            var tasks = await _taskCommentsCoreService.GetAllAsync(pagerData);
            return Ok(tasks);
        }

        [HttpGet("{id}"), ActionName("GetId")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var response = await _taskCommentsCoreService.GetAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TaskCommentsModel model)
        {
            var response = await _taskCommentsCoreService.AddAsync(model);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] TaskCommentsModel model)
        {
            var response = await _taskCommentsCoreService.UpdateAsync(model);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _taskCommentsCoreService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
