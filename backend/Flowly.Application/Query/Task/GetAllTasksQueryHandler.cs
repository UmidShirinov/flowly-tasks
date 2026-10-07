using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Flowly.Application.DTOs;
using Flowly.Application.Interfaces;

namespace Flowly.Application.Query.Task.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<AddTaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<GetAllTasksQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository, ILogger<GetAllTasksQueryHandler> logger, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<AddTaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        try 
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<List<AddTaskDto>>(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Tasklar alinar zamanı xəta baş verdi");
            return new List<AddTaskDto>();
        }
    }
}
