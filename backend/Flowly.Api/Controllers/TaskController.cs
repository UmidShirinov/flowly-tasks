using Flowly.Application.Commands.Task.CreateTask;
using Flowly.Application.Query.Task.GetAllTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flowly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaskController> _logger;

    public TaskController(IMediator mediator, ILogger<TaskController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("add-task")]
    public async Task<IActionResult> AddTask([FromBody] CreateTaskCommand command)
    {
        try 
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                _logger.LogInformation("Task uğurla əlavə edildi");
                return Ok(new { message = "Task uğurla əlavə edildi" });
            }

            _logger.LogWarning("Task əlavə edilərkən xəta baş verdi");
            return BadRequest(new { message = "Task əlavə edilərkən xəta baş verdi" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Task əlavə edilərkən xəta baş verdi");
            return BadRequest(new { message = "Task əlavə edilərkən xəta baş verdi" });
        }
    }

    [HttpGet("get-all-tasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        try 
        {
            var tasks = await _mediator.Send(new GetAllTasksQuery());
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Tasklar alinar zamanı xəta baş verdi");
            return BadRequest(new { message = "Tasklar alinar zamanı xəta baş verdi" });
        }
    }
}