using CRUD.Api.Interfaces;
using CRUD.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _service;

        public TaskItemController(ITaskItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var taskItem = await _service.GetById(id);
            if (taskItem == null)
                return NotFound();

            return Ok(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItemDto taskItem)
        {
            if (taskItem == null)
                return BadRequest("TaskItem cannot be null.");

            await _service.Create(taskItem);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItemDto taskItem)
        {
            if (taskItem == null)
                return BadRequest("Updated TaskItem cannot be null.");

            await _service.Update(id, taskItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
