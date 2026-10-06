using MediatR;
using Flowly.Domain.Entities;

namespace Flowly.Application.Query.Task.GetAllTasks;

public class GetAllTasksQuery : IRequest<List<AddTaskDto>>
{
    
}