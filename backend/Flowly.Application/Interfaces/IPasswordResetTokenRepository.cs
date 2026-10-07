using Flowly.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Flowly.Application.Interfaces;

public interface IPasswordResetTokenRepository
{
    Task<PasswordResetToken?> GetByTokenAsync(string token);
    Task AddAsync(PasswordResetToken token);
    Task MarkAsUsedAsync(string token);
}
