using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Core.Interfaces.Users;

namespace ZimoziSolutions.Controllers
{
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserCoreService _userCoreService;

        public UserController(IUserCoreService userCoreService)
        {
            _userCoreService = userCoreService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PagerData pagerData, [FromQuery] string? role = "")
        {
            var tasks = await _userCoreService.GetAllAsync(pagerData, role ?? "");
            return Ok(tasks);
        }

        [HttpGet("{id}"), ActionName("GetId")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var response = await _userCoreService.GetAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserCustomModel user)
        {
            var response = await _userCoreService.AddAsync(user);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] UserCustomModel user)
        {
            var response = await _userCoreService.UpdateAsync(user);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _userCoreService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
