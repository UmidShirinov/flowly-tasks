using Flowly.Domain.Entities;
using TaskEntity = Flowly.Domain.Entities.Task;

namespace Flowly.Application.Interfaces;

public interface ITaskRepository
{
    Task<bool> CreateAsync(TaskEntity task);
    Task<List<TaskEntity>> GetAllAsync();
}
