using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Flowly.Application.Interfaces;
using Flowly.Application.DTOs;
using EntityTask = Flowly.Domain.Entities.Task;

namespace Flowly.Application.Commands.Task.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, ILogger<CreateTaskCommandHandler> logger, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = _mapper.Map<EntityTask>(request);

            return await _taskRepository.CreateAsync(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Task yaratma zamani xəta baş verdi");
            return false;
        }
    }
}