using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TaskManagementApi.Models.Domain;
using TaskManagementApi.Models.DTO;
using TaskManagementApi.Services;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService =taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks(){
            var tasks = await _taskService.GetAllTasksAsync();

            var taskDtos =tasks.Select(Task => new TaskDto
            {
                Id =Task.Id,
                Title = Task.Title,
                Description = Task.Description,
                isCompleted = Task.isCompleted
            });

            return Ok(taskDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await _taskService .GetTaskByIdAsync(id);
            if(task == null) return NotFound();

            return Ok(new TaskDto
            {
                Id =task.Id,
                Title = task.Title,
                Description = task.Description,
                isCompleted =task.isCompleted
            });
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            var task = new TaskItem
            {
                Title =taskDto.Title,
                Description = taskDto.Description,
                isCompleted = taskDto.isCompleted

            };

            var createdTask = await _taskService.CreateTaskAsync(task);

             return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, new TaskDto
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                isCompleted = createdTask.isCompleted
            });
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDto taskDto)
        {
            var existingTask = await _taskService.GetTaskByIdAsync(id);
            if (existingTask == null) return NotFound();

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.isCompleted = taskDto.isCompleted;

            var updatedTask = await _taskService.UpdateTaskAsync(existingTask);

            return Ok(new TaskDto
            {
                Id = updatedTask.Id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                isCompleted = updatedTask.isCompleted
            });
        }

        // 🔥 DELETE A TASK
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        







    }
}