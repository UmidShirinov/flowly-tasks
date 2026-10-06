using MediatR;
using Flowly.Domain.Entities;
using Flowly.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Flowly.Application.DTOs;
using Mapping;

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
            var task = _mapper.Map<Task>(request);

            return await _taskRepository.CreateAsync(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Task yaratma zamani xəta baş verdi");
            return 0;
        }
    }
}