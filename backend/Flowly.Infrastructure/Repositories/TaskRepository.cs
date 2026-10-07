using Flowly.Application.Interfaces;
using Flowly.Domain.Entities;
using Npgsql;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using TaskEntity = Flowly.Domain.Entities.Task;

namespace Flowly.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly string _connectionString;

    public TaskRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    public async Task<bool> CreateAsync(TaskEntity task)
    {
        using var db = CreateConnection();
        var sql = @"INSERT INTO Tasks (Title, Description, Status, Priority, StartDate, DueDate, CompletedDate, CreatedById, AssignedToId, DepartmentId, IsDeleted, CreatedAt, UpdatedAt) 
                    VALUES (@Title, @Description, @Status, @Priority, @StartDate, @DueDate, @CompletedDate, @CreatedById, @AssignedToId, @DepartmentId, @IsDeleted, @CreatedAt, @UpdatedAt)";
        var rowsAffected = await db.ExecuteAsync(sql, task);
        return rowsAffected > 0;
    }

    public async Task<List<TaskEntity>> GetAllAsync()
    {
        using var db = CreateConnection();
        var sql = "SELECT * FROM Tasks WHERE IsDeleted = false";
        var result = await db.QueryAsync<TaskEntity>(sql);
        return result.ToList();
    }
}
