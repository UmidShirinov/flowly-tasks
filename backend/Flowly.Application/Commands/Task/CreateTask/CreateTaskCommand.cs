using MediatR;
using Flowly.Domain.Entities;

namespace Flowly.Application.Commands.Task.CreateTask;

public class CreateTaskCommand : IRequest<bool>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public string Status { get; set; } = "Pending";
    public int? AssignedToId { get; set; }
    public int? DepartmentId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
}