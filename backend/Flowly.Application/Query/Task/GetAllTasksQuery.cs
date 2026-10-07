using MediatR;
using Flowly.Application.DTOs;

namespace Flowly.Application.Query.Task.GetAllTasks;

public class GetAllTasksQuery : IRequest<List<AddTaskDto>>
{
    
}